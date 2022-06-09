// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;

Console.WriteLine("Copilot Demo!");
Console.WriteLine("For more info visit this url: https://github.com/github/copilot-docs/blob/main/docs/visualstudio/gettingstarted.md");
Console.WriteLine();

int menu = GetConsoleMenu();
while (menu > 0)
{
    Console.WriteLine();
    switch (menu)
    {
        case 1:
            Console.WriteLine("Example of code generation by method name.");
            Console.WriteLine($"Type the method: {nameof(CalculateDaysBetweenDates)}");
            Console.Write("Enter the start date: ");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter the end date: ");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"Result of {nameof(CalculateDaysBetweenDates)}: {CalculateDaysBetweenDates(startDate, endDate)}");
            break;
        case 2:
            Console.WriteLine("Example of code generation by comments.");
            string fileName = "index.xhtml";
            if (!File.Exists(fileName))
                Console.WriteLine($"The file '{fileName}' is not found!");
            else
            {
                Console.WriteLine($"The file '{fileName}' is found.");
                string comment = "// Find all images.";
                Console.WriteLine($"Type the comment: {comment}");
                XDocument doc = XDocument.Load(fileName);
                Console.WriteLine($"Document size: {doc.ToString().Length}");
                // Find all images.
                IEnumerable<XElement> images = doc.Descendants("img");
                Console.WriteLine($"Found images: {images.Count()}");
                // And give them a red border.
                foreach (XElement image in images)
                {
                    image.SetAttributeValue("style", "border: 2px solid red;");
                    Console.WriteLine($"{nameof(image)}: {image}");
                }
            }
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
    Console.WriteLine("1. Calculate days between dates");
    Console.WriteLine("2. Get images from XDocument");
    Console.Write("Enter your choice: ");
    return Convert.ToInt32(Console.ReadLine());
}

int CalculateDaysBetweenDates(DateTime startDt, DateTime endDt)
{
    DateTime start = new(startDt.Year, startDt.Month, startDt.Day);
    DateTime end = new(endDt.Year, endDt.Month, endDt.Day);
    TimeSpan result = end - start;
    return result.Days;
}
