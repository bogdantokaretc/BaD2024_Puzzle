using System.Text;

namespace BaD2024_Puzzle
{
    static class Menu
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n\tМеню для роботи з текстовими файлами:");
                Console.WriteLine("1. Вибрати .txt файл з поточної папки програми");
                Console.WriteLine("2. Вийти");
                Console.WriteLine($"\nУВАГА! Для коректної роботи програми помістіть файл в поточну папку:\n{Directory.GetCurrentDirectory()}\n");
                Console.Write("Введіть свій вибір: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListFilesInDirectory();
                        break;
                    case "2":
                        Console.WriteLine("Завершення роботи програми...");
                        return;
                    default:
                        Console.WriteLine("Даного пункту меню не існує!");
                        break;
                }
            }
        }

        static void ListFilesInDirectory()
        {
            string filePath = Directory.GetCurrentDirectory();

            string[] txtFiles = Directory.GetFiles(filePath, "*.txt");

            if (txtFiles.Length == 0)
            {
                Console.WriteLine("У поточній директорії файл з розширенням (*.txt) не знайдено!");
                return;
            }

            Console.WriteLine("\nУ папці знайдено такі файли з розширенням (*.txt):");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
            }

            Console.Write("Введіть номер файлу з списку для його подальшого опрацювання\n" +
                "Для повернення до головного меню натисність Enter на клавіатурі:");

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index > 0 && index <= txtFiles.Length)
            {
                ProcessFile(txtFiles[index - 1]);
            }
            else
            {
                Console.WriteLine("Даного пункту меню не існує!");
            }
        }

        static void ProcessFile(string filePath)
        {
            Console.WriteLine($"\nВідкрито файл: {filePath}");
            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine("\nВміст файлу:");
                Console.WriteLine(content);
                Console.WriteLine("\nРезультат роботи програми:");
                Program.GetFragmentChain(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка під час обробки файлу: {ex.Message}");
            }
        }
    }
}
