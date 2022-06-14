// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;
using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitorDemo;

Console.WriteLine("LibreHardwareMonitor Demo.");
Console.WriteLine("For more info visit this url: https://github.com/LibreHardwareMonitor/LibreHardwareMonitor");
Console.WriteLine();

int menu = GetConsoleMenu();
while (menu > 0)
{
    Console.WriteLine();
    switch (menu)
    {
        case 1:
            Monitor();
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

    foreach (IHardware hardware in computer.Hardware)
    {
        Console.WriteLine("Hardware: {0}", hardware.Name);
        foreach (IHardware subhardware in hardware.SubHardware)
        {
            Console.WriteLine("\tSubhardware: {0}", subhardware.Name);
            foreach (ISensor sensor in subhardware.Sensors)
            {
                Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
            }
        }
        foreach (ISensor sensor in hardware.Sensors)
        {
            Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
        }
    }

    computer.Close();
}
