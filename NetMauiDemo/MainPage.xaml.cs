// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetMauiDemo;

public partial class MainPage : ContentPage
{
    #region Public and private fields, properties, constructor

    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    #endregion

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        CounterBtn.Text = count == 1 ? $"Clicked {count} time" : $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void buttonSetNewUser_Clicked(object sender, EventArgs e)
    {

    }
}
