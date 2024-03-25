﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Abstractions;

public interface IConfigService
{
    ConfigModel Model { get; set; }
    string GetAlertMessage();
}