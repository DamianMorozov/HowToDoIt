// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("LibreHardwareMonitor Demo.");
Console.WriteLine("For more info visit this url: https://github.com/LibreHardwareMonitor/LibreHardwareMonitor");
Console.WriteLine();

var menu = GetConsoleMenu();
while (menu > 0)
{
    Console.WriteLine();
    switch (menu)
    {
        case 1:
            Monitor();
            break;
        case 2:
            MonitorCpuAndMemoryUntilEnter();
            break;
    }
    Console.WriteLine();
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    Console.Clear();
    menu = GetConsoleMenu();
}

int GetConsoleMenu()
{
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Run Demo");
    Console.WriteLine("2. Monitor CPU and memory");
    Console.Write("Enter your choice: ");
    return Convert.ToInt32(Console.ReadLine());
}

void Monitor()
{
    Computer computer = new()
    {
        IsCpuEnabled = true,
        IsGpuEnabled = true,
        IsMemoryEnabled = true,
        IsMotherboardEnabled = true,
        IsControllerEnabled = true,
        IsNetworkEnabled = true,
        IsStorageEnabled = true
    };
    computer.Open();
    computer.Accept(new UpdateVisitor());

    foreach (var hardware in computer.Hardware)
    {
        Console.WriteLine("Hardware: {0}", hardware.Name);
        foreach (var subhardware in hardware.SubHardware)
        {
            Console.WriteLine("\tSubhardware: {0}", subhardware.Name);
            foreach (var sensor in subhardware.Sensors)
            {
                Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
            }
        }
        foreach (var sensor in hardware.Sensors)
        {
            Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
        }
    }

    computer.Close();
}

void MonitorCpuAndMemoryUntilEnter()
{
    var computer = new Computer() { IsCpuEnabled = true, IsMemoryEnabled = true };
    computer.Open();

    Console.WriteLine("Press Enter for exit...");
    var readTask = Task.Run(Console.ReadLine);
    var lastUpdate = DateTime.MinValue;
    var currentProcess = Process.GetCurrentProcess();
    float? totalMemory = null;

    while (!readTask.IsCompleted)
    {
        computer.Accept(new UpdateVisitor());

        float? cpuLoad = null;
        float? usedMemory = null;

        foreach (var hardware in computer.Hardware)
        {
            if (hardware.HardwareType == HardwareType.Cpu)
            {
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Load && sensor.Name == "CPU Total")
                        cpuLoad = sensor.Value;
                }
            }
            else if (hardware.HardwareType == HardwareType.Memory)
            {
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Data)
                    {
                        if (sensor.Name.Contains("Memory Used"))
                            usedMemory = sensor.Value;
                        if (sensor.Name.Contains("Memory Available"))
                            totalMemory = usedMemory + sensor.Value;
                    }
                }
            }
        }

        var processCpuPercent = GetProcessCpuUsage(currentProcess);

        Console.WriteLine(
            $"CPU total: {cpuLoad?.ToString("F1") ?? "?"}% | " +
            $"CPU app: {processCpuPercent:F1}% | " +
            $"Memory used: {usedMemory?.ToString("F1") ?? "?"} GB / " +
            $"{totalMemory?.ToString("F1") ?? "?"} GB"
        );

        Thread.Sleep(1000);
    }

    computer.Close();

}

double GetProcessCpuUsage(Process process)
{
    var processorCount = Environment.ProcessorCount;

    var startCpu = process.TotalProcessorTime;
    var startTime = DateTime.UtcNow;

    Thread.Sleep(500);

    var endCpu = process.TotalProcessorTime;
    var endTime = DateTime.UtcNow;

    var cpuUsedMs = (endCpu - startCpu).TotalMilliseconds;
    var totalMs = (endTime - startTime).TotalMilliseconds * processorCount;

    return (cpuUsedMs / totalMs) * 100;
}
