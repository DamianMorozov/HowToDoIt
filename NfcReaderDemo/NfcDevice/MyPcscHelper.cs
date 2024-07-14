namespace NfcReaderDemo.NfcDevice;

public sealed class MyPcscHelper : ObservableRecipient
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static MyPcscHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static MyPcscHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private readonly string _empty = "<Empty>";
    private SmartCardReader _cardReader;
    public SmartCardReader CardReader
    {
        get => _cardReader;
        set
        {
            SetProperty(ref _cardReader, value);
            IsCardReaderActive = value is not null;
        }
    }
    private string _cardReaderName;
    public string CardReaderName { get => _cardReaderName; set => SetProperty(ref _cardReaderName, value); }
    private bool _isCardReaderActive = false;
    public bool IsCardReaderActive { get => _isCardReaderActive; set => SetProperty(ref _isCardReaderActive, value); }
    private string _message;
    public string Message { get => _message; set => SetProperty(ref _message, value); }
    private string _exceptionMessage;
    public string ExceptionMessage { get => _exceptionMessage; set => SetProperty(ref _exceptionMessage, value); }
    private string _exceptionStackTrace;
    public string ExceptionStackTrace { get => _exceptionStackTrace; set => SetProperty(ref _exceptionStackTrace, value); }
    private Exception _exceptionHandleCard;
    public Exception ExceptionHandleCard
    {
        get => _exceptionHandleCard;
        set
        {
            SetProperty(ref _exceptionHandleCard, value);
            ExceptionMessage = value is not null ? value.Message : _empty;
            ExceptionStackTrace = value is not null ? value.StackTrace : _empty;
        }
    }
    private Microsoft.UI.Dispatching.DispatcherQueue _dispatcherQueue;

    public ICommand ConnectCommand => new DelegateCommand(async () => await ConnectNfcReaderAsync());
    public ICommand DisconnectCommand => new DelegateCommand(DisconnetNfcReader);

    public MyPcscHelper()
    {
        CardReaderName = _empty;
        Message = _empty;
        ExceptionMessage = _empty;
        ExceptionStackTrace = _empty;
    }

    #endregion

    #region Public and private methods

    private void SetCardReaderName(string name)
    {
        _dispatcherQueue ??= Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        if (_dispatcherQueue is null) return;
        _dispatcherQueue.TryEnqueue(() => { CardReaderName = name; });
    }

    private void SetMessage(string message)
    {
        _dispatcherQueue ??= Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        if (_dispatcherQueue is null) return;
        _dispatcherQueue.TryEnqueue(() => { Message = message; });
    }

    private void AddMessage(string message)
    {
        _dispatcherQueue ??= Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        if (_dispatcherQueue is null) return;
        _dispatcherQueue.TryEnqueue(() => { Message += message; });
    }

    private void SetExceptionHandleCard(Exception exception)
    {
        _dispatcherQueue ??= Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        if (_dispatcherQueue is null) return;
        _dispatcherQueue.TryEnqueue(() => { ExceptionHandleCard = exception; });
    }

    public async Task ConnectNfcReaderAsync()
    {
        // First try to find a reader that advertises as being NFC
        var deviceInfo = await SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Nfc);
        if (deviceInfo is null)
        {
            // If we didn't find an NFC reader, let's see if there's a "generic" reader meaning we're not sure what type it is
            deviceInfo = await SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Any);
            SetCardReaderName(deviceInfo is not null ? deviceInfo.Name : _empty);
        }

        if (deviceInfo is null)
        {
            SetMessage("NFC card reader mode not supported on this device");
            return;
        }

        if (!deviceInfo.IsEnabled)
        {
            var msgbox = new Windows.UI.Popups.MessageDialog("Your NFC proximity setting is turned off, you will be taken to the NFC proximity control panel to turn it on");
            msgbox.Commands.Add(new Windows.UI.Popups.UICommand("OK"));
            await msgbox.ShowAsync();

            // This URI will navigate the user to the NFC proximity control panel
            MyPcscUtils.LaunchNfcProximitySettingsPage();
            return;
        }

        if (CardReader is null)
        {
            CardReader = await SmartCardReader.FromIdAsync(deviceInfo.Id);
            CardReader.CardAdded += cardReader_CardAdded;
            CardReader.CardRemoved += cardReader_CardRemoved;
        }
    }

    public void DisconnetNfcReader()
    {
        SetCardReaderName(_empty);
        SetExceptionHandleCard(null);
        if (CardReader is not null)
        {
            CardReader.CardAdded -= cardReader_CardAdded;
            CardReader.CardRemoved -= cardReader_CardRemoved;
            CardReader = null;
        }
    }

    private async void cardReader_CardAdded(SmartCardReader sender, CardAddedEventArgs args) => 
        await HandleCard(args.SmartCard);

    private void cardReader_CardRemoved(SmartCardReader sender, CardRemovedEventArgs args) =>
        AddMessage(Environment.NewLine + "Card removed");

    /// <summary> Sample code to handle a couple of different cards based on the identification process </summary>
    private async Task HandleCard(SmartCard card)
    {
        try
        {
            // Clear the messages
            SetMessage(string.Empty);

            // Connect to the card
            using var connection = await card.ConnectAsync();
            // Try to identify what type of card it was
            IccDetection cardIdentification = new(card, connection);
            //IccDetection cardIdentification = new(card);
            await cardIdentification.DetectCardTypeAync();
            SetMessage($"Connected to card.{Environment.NewLine}PC/SC device class: " + cardIdentification.PcscDeviceClass.ToString());
            AddMessage(Environment.NewLine + "Card name: " + cardIdentification.PcscCardName.ToString());
            AddMessage(Environment.NewLine + "ATR: " + BitConverter.ToString(cardIdentification.Atr));

            if ((cardIdentification.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass) &&
                (cardIdentification.PcscCardName == Pcsc.CardName.MifareUltralightC
                || cardIdentification.PcscCardName == Pcsc.CardName.MifareUltralight
                || cardIdentification.PcscCardName == Pcsc.CardName.MifareUltralightEV1))
            {
                // Handle MIFARE Ultralight
                var mifareULAccess = new MifareUltralight.AccessHandler(connection);
                // Each read should get us 16 bytes/4 blocks, so doing
                // 4 reads will get us all 64 bytes/16 blocks on the card
                for (byte i = 0; i < 4; i++)
                {
                    var response = await mifareULAccess.ReadAsync((byte)(4 * i));
                    AddMessage(Environment.NewLine + "Block " + (4 * i).ToString() + " to Block " + (4 * i + 3).ToString() + " " + BitConverter.ToString(response));
                }

                var responseUid = await mifareULAccess.GetUidAsync();
                AddMessage(Environment.NewLine + "UID:  " + BitConverter.ToString(responseUid));
            }
            else if (cardIdentification.PcscDeviceClass == Pcsc.Common.DeviceClass.MifareDesfire)
            {
                // Handle MIFARE DESfire
                var desfireAccess = new Desfire.AccessHandler(connection);
                var desfire = await desfireAccess.ReadCardDetailsAsync();

                AddMessage("DesFire Card Details:  " + Environment.NewLine + desfire.ToString());
            }
            else if (cardIdentification.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                && cardIdentification.PcscCardName == Pcsc.CardName.FeliCa)
            {
                // Handle Felica
                AddMessage(Environment.NewLine + "Felica card detected");
                var felicaAccess = new Felica.AccessHandler(connection);
                var uid = await felicaAccess.GetUidAsync();
                AddMessage(Environment.NewLine + "UID:  " + BitConverter.ToString(uid));
            }
            else if (cardIdentification.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                && (cardIdentification.PcscCardName == Pcsc.CardName.MifareStandard1K || cardIdentification.PcscCardName == Pcsc.CardName.MifareStandard4K))
            {
                // Handle MIFARE Standard/Classic
                AddMessage(Environment.NewLine + "MIFARE Standard/Classic card detected");
                var mfStdAccess = new MifareStandard.AccessHandler(connection);
                var uid = await mfStdAccess.GetUidAsync();
                AddMessage(Environment.NewLine + "UID:  " + BitConverter.ToString(uid));

                ushort maxAddress = 0;
                switch (cardIdentification.PcscCardName)
                {
                    case Pcsc.CardName.MifareStandard1K:
                        maxAddress = 0x3f;
                        break;
                    case Pcsc.CardName.MifareStandard4K:
                        maxAddress = 0xff;
                        break;
                }
                await mfStdAccess.LoadKeyAsync(MifareStandard.DefaultKeys.FactoryDefault);

                for (ushort address = 0; address <= maxAddress; address++)
                {
                    var response = await mfStdAccess.ReadAsync(address, Pcsc.GeneralAuthenticate.GeneralAuthenticateKeyType.MifareKeyA);
                    AddMessage(Environment.NewLine + "Block " + address.ToString() + " " + BitConverter.ToString(response));
                }
            }
            else if (cardIdentification.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                && (cardIdentification.PcscCardName == Pcsc.CardName.ICODE1 ||
                    cardIdentification.PcscCardName == Pcsc.CardName.ICODESLI ||
                    cardIdentification.PcscCardName == Pcsc.CardName.iCodeSL2))
            {
                // Handle ISO15693
                AddMessage(Environment.NewLine + "ISO15693 card detected");
                var iso15693Access = new Iso15693.AccessHandler(connection);
                var uid = await iso15693Access.GetUidAsync();
                AddMessage(Environment.NewLine + "UID:  " + BitConverter.ToString(uid));
            }
            else
            {
                // Unknown card type
                // Note that when using the XDE emulator the card's ATR and type is not passed through, so we'll
                // end up here even for known card types if using the XDE emulator

                // Some cards might still let us query their UID with the PC/SC command, so let's try:
                var apduRes = await connection.TransceiveAsync(new Pcsc.GetUid());
                if (!apduRes.Succeeded)
                {
                    AddMessage(Environment.NewLine + "Failure getting UID of card, " + apduRes.ToString());
                }
                else
                {
                    AddMessage(Environment.NewLine + "UID:  " + BitConverter.ToString(apduRes.ResponseData));
                }
            }
            SetExceptionHandleCard(null);
        }
        catch (Exception ex)
        {
            SetExceptionHandleCard(ex);
        }
    }

    #endregion
}