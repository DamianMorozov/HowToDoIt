namespace NfcReaderDemo.NfcDevice;

public sealed class MyPcscUtils
{
    public static async Task<IBackgroundTaskRegistration> GetOrRegisterHceBackgroundTask(string bgTaskName, string taskEntryPoint, SmartCardTriggerType triggerType)
    {
        try
        {
            // Check if there's an existing background task already registered
            var bgTask = (from t in BackgroundTaskRegistration.AllTasks
                          where t.Value.Name.Equals(bgTaskName)
                          select t.Value).SingleOrDefault();
            if (bgTask is not null)
            {
                LogMessage("Background task already registered", EonPcscNotifyType.NotifyStatusMessage);
            }
            else
            {
                if (!(await DoBackgroundRequestAccess()))
                {
                    LogMessage("Background task access denied, task won't fire", EonPcscNotifyType.NotifyErrorMessage);
                    throw new Exception("Failed to get background access");
                }

                var taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = bgTaskName;
                taskBuilder.TaskEntryPoint = taskEntryPoint;
                taskBuilder.SetTrigger(new SmartCardTrigger(triggerType));
                bgTask = taskBuilder.Register();
                LogMessage("Background task registered", EonPcscNotifyType.NotifyStatusMessage);
            }

            bgTask.Completed += BgTask_Completed;
            bgTask.Progress += BgTask_Progress;

            return bgTask;
        }
        catch (Exception ex)
        {
            LogMessage("Failed to register background task: " + ex.ToString(), EonPcscNotifyType.NotifyStatusMessage);
            throw;
        }
    }

    private static async Task<bool> DoBackgroundRequestAccess()
    {
        var appVersion = string.Format("{0}.{1}.{2}.{3}",
                Package.Current.Id.Version.Build,
                Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Revision);

        if ((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["AppVersion"] != appVersion)
        {
            // Our app has been updated
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["AppVersion"] = appVersion;

            // Call RemoveAccess
            BackgroundExecutionManager.RemoveAccess();
        }

        var status = await BackgroundExecutionManager.RequestAccessAsync();

        return status == BackgroundAccessStatus.AlwaysAllowed
            || status == BackgroundAccessStatus.AllowedSubjectToSystemPolicy;
    }

    private static void BgTask_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
    {
        LogMessage("BgTask Progress: " + args.Progress, EonPcscNotifyType.NotifyStatusMessage);
    }

    private static void BgTask_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
    {
        LogMessage("BgTask Completed", EonPcscNotifyType.NotifyStatusMessage);
    }

    public static async Task<SmartCardAppletIdGroupRegistration> RegisterAidGroup(string displayName, IList<IBuffer> appletIds, SmartCardEmulationCategory? emulationCategory, SmartCardEmulationType emulationType, bool automaticEnablement)
    {
        try
        {
            var cardAidGroup = new SmartCardAppletIdGroup(
                displayName,
                appletIds,
                (SmartCardEmulationCategory)emulationCategory,
                emulationType);
            cardAidGroup.AutomaticEnablement = automaticEnablement;

            var reg = await SmartCardEmulator.RegisterAppletIdGroupAsync(cardAidGroup);
            LogMessage("AID group successfully registered", EonPcscNotifyType.NotifyStatusMessage);

            return reg;
        }
        catch (Exception ex)
        {
            LogMessage("Failed to register AID group: " + ex.ToString(), EonPcscNotifyType.NotifyStatusMessage);
            throw;
        }
    }

    public static async Task<SmartCardAppletIdGroupRegistration> GetAidGroupById(Guid id)
    {
        try
        {
            // Find registration
            var reg = (from r in await SmartCardEmulator.GetAppletIdGroupRegistrationsAsync()
                       where r.Id == id
                       select r).SingleOrDefault();
            if (reg is null)
            {
                LogMessage("No matching AID group found for specified ID", EonPcscNotifyType.NotifyErrorMessage);
            }

            return reg;
        }
        catch (Exception ex)
        {
            LogMessage("Failed to get AID group by ID: " + ex.ToString(), EonPcscNotifyType.NotifyStatusMessage);
            throw;
        }
    }

    public static async Task<SmartCardActivationPolicyChangeResult> SetCardActivationPolicy(SmartCardAppletIdGroupRegistration reg, SmartCardAppletIdGroupActivationPolicy policy)
    {
        LogMessage("Previous AID group activation policy: " + reg.ActivationPolicy, EonPcscNotifyType.NotifyStatusMessage);

        // Now try to set the policy on the card, this may pop UI to the user asking them to confirm
        var res = await reg.RequestActivationPolicyChangeAsync(policy);
        if (res == SmartCardActivationPolicyChangeResult.Allowed)
        {
            LogMessage("AID group successfully set to " + policy.ToString(), EonPcscNotifyType.NotifyStatusMessage);
        }
        else
        {
            LogMessage("AID group policy change was disallowed (perhaps due to user declining the UI popup)", EonPcscNotifyType.NotifyErrorMessage);
        }

        return res;
    }

    public static async Task<bool> CheckHceSupport()
    {
        // Check if the SmartCardEmulator API exists on this currently running SKU of Windows
        if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Devices.SmartCards.SmartCardEmulator"))
        {
            LogMessage("This SKU of Windows does not support NFC card emulation, only phones/mobile devices support NFC card emulation", EonPcscNotifyType.NotifyErrorMessage);
            return false;
        }

        // Check if any NFC emulation is supported on this device
        var sce = await SmartCardEmulator.GetDefaultAsync();
        if (sce is null)
        {
            LogMessage("NFC card emulation is not supported on this device, probably the device does not have NFC at all", EonPcscNotifyType.NotifyErrorMessage);
            return false;
        }

        // Check if the NFC emulation support on this device includes HCE
        if (!sce.IsHostCardEmulationSupported())
        {
            LogMessage("This device's NFC does not support HCE-mode", EonPcscNotifyType.NotifyErrorMessage);
            return false;
        }
        return true;
    }

    public static async void LaunchNfcPaymentsSettingsPage()
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-nfctransactions:"));
    }

    public static async void LaunchNfcProximitySettingsPage()
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-proximity:"));
    }

    public static byte[] HexStringToBytes(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException();
        }
        var bytes = new byte[hexString.Length / 2];
        for (var i = 0; i < hexString.Length; i += 2)
        {
            bytes[i / 2] = byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return bytes;
    }

    public static void LogMessage(string message, EonPcscNotifyType type = EonPcscNotifyType.NotifyStatusMessage)
    {
        Debug.WriteLine(message);
        //SDKTemplate.MainPage.Current.NotifyUser(message, type);
    }
}
