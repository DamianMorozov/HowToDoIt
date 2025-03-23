﻿namespace WinUIDemo.Views;

public sealed partial class FusionCachePage : Page
{
    public FusionCacheViewModel ViewModel { get; }

    public FusionCachePage()
    {
        InitializeComponent();
        ViewModel = App.GetService<FusionCacheViewModel>();
    }
}