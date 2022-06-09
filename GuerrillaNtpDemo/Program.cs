﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


// https://github.com/robertvazan/guerrillantp

using GuerrillaNtp;
using System;
using System.Net;
using static GuerrillaNtpDemo.TimeHelper;

Console.WriteLine("GuerrillaNtp demo.");
Console.Write("Input NTP server: ");
string? str = Console.ReadLine();
if (str is string ntp && !string.IsNullOrEmpty(ntp))
{
    TimeSpan offset;
    IPAddress ip = Dns.GetHostAddresses(ntp)[0];
    Console.WriteLine($"{nameof(ip)}: {ip}");
    Console.WriteLine("1 - just view");
    Console.WriteLine("2 - update local Windows time");
    Console.Write("Select mode: ");
    str = Console.ReadLine();

    using NtpClient ntpClient = new(ip);
    offset = ntpClient.GetCorrectionOffset();
    Console.WriteLine($"{nameof(offset)}: {offset}");
    DateTime localTime = DateTime.Now;
    DateTime localUtcTime = DateTime.UtcNow;
    DateTime accurateTime = localTime + offset;
    DateTime accurateUtcTime = localUtcTime + offset;

    switch (str)
    {
        case "2":
            SystemTime systemTime = new()
            {
                wYear = (short)accurateTime.Year,
                wMonth = (short)accurateTime.Month,
                wDay = (short)accurateTime.Day,
                wHour = (short)accurateTime.Hour,
                wMinute = (short)accurateTime.Minute,
                wSecond = (short)accurateTime.Second,
                wMilliseconds = (short)accurateTime.Millisecond,
            };
            SetSystemTime(ref systemTime);
            break;
    }
    Console.WriteLine($"{nameof(localTime)}:\t\t{localTime:yyyy-MM-dd HH:mm:ss.fff}");
    Console.WriteLine($"{nameof(accurateTime)}:\t\t{accurateTime:yyyy-MM-dd HH:mm:ss.fff}");
    Console.WriteLine($"{nameof(localUtcTime)}:\t\t{localUtcTime:yyyy-MM-dd HH:mm:ss.fff}");
    Console.WriteLine($"{nameof(accurateUtcTime)}:\t{accurateUtcTime:yyyy-MM-dd HH:mm:ss.fff}");
}
else
{
    Console.WriteLine("Invalid string NTP server!");
}
