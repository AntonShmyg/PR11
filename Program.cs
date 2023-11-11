using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.IO.Compression;

namespace PR11
{
    internal class Program
    {
        static string path = @"C:\TempProgs\";
        static string fileName = "test";
        static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            while (true)
            {
                menu();
            }
        }

        public static void menu()
        {
            Console.Clear();
            int i;
            Console.WriteLine(
            "1.Работа с файлом\n" +
            "2.Работа с JSON\n" +
            "3.Работа с XML\n" +
            "4.Работа с ZIP\n" +
            "5.Информация о дисках\n" +
            "Нажмите любую клавишу для выхода\n");
            try
            {
                i = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                return;
            }
            Console.Clear();
            switch (i)
            {
                case 1:
                    Console.WriteLine("1");
                    Console.WriteLine(
                    "1.Создать файл\n" +
                    "2.Прочитать файл\n" +
                    "3.Удалить файл\n" +
                    "4.Назад\n");
                    i = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("1");
                            create_file();
                            break;
                        case 2:
                            Console.WriteLine("3");
                            read_file();
                            break;
                        case 3:
                            Console.WriteLine("3");
                            delete_file(".txt");
                            break;
                        case 4:
                            Console.Clear();
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine(
                    "1.Записать в файл\n" +
                    "2.Прочитать файл\n" +
                    "3.Удалить файл\n" +
                    "4.Назад\n");
                    i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("1");
                            create_json();
                            break;
                        case 2:
                            Console.WriteLine("2");
                            read_json();
                            break;
                        case 3:
                            Console.WriteLine("3");
                            delete_file(".json");
                            break;
                        case 4:
                            Console.Clear();
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine(
                    "1.Записать в файл\n" +
                    "2.Прочитать файл\n" +
                    "3.Удалить файл\n" +
                    "4.Назад\n");
                    i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("1");
                            create_xml();
                            break;
                        case 2:
                            Console.WriteLine("2");
                            read_xml();
                            break;
                        case 3:
                            Console.WriteLine("3");
                            delete_file(".xml");
                            break;
                        case 4:
                            Console.Clear();
                            break;
                    }
                    break;
                case 4:
                    Console.WriteLine(
                    "1.Создать архив\n" +
                    "2.Разархивировать файл\n" +
                    "3.Удалить архив\n" +
                    "4.Назад\n");
                    i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("1");
                            create_zip();
                            break;
                        case 2:
                            Console.WriteLine("2");
                            read_zip();
                            break;
                        case 3:
                            Console.WriteLine("3");
                            delete_file(".zip");
                            break;
                        case 4:
                            Console.Clear();
                            break;
                    }
                    break;
                case 5:
                    drives_info();
                    break;
                default:
                    Console.WriteLine("Вы нажали что-то другое...");
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Environment.Exit(0);
                    break;

            }
            Console.Clear();
            
        }

        public static void drives_info()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    Console.WriteLine("Имя диска: " + drive.Name);
                    Console.WriteLine("Файловая система: " + drive.DriveFormat);
                    Console.WriteLine("Тип диска: " + drive.DriveType);
                    Console.WriteLine("Объем доступного свободного места (в байтах): " + drive.AvailableFreeSpace);
                    Console.WriteLine("Готов ли диск: " + drive.IsReady);
                    Console.WriteLine("Корневой каталог диска: " + drive.RootDirectory);
                    Console.WriteLine("Общий объем свободного места, доступного на диске (в байтах): " + drive.TotalFreeSpace);
                    Console.WriteLine("Размер диска (в байтах): " + drive.TotalSize);
                    Console.WriteLine("Нажмите любую кнопку для выхода");
                    Console.ReadLine();
                }
                catch { }
            }
        }

        public static void create_file()
        {
            Console.WriteLine("Введите имя файла");
            string filePath = path + Console.ReadLine() + ".txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл создан");
                string text = "";
                Console.WriteLine("Введите текст");
                text = Console.ReadLine();
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(text);
                }
            }
            else
            {
                Console.WriteLine("Произошла ошибка");
            }
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        public static void read_file()
        {
            Console.WriteLine("Введите имя файла");
            fileName = Console.ReadLine();
            string filePath = path + fileName + ".txt";
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        public static void delete_file(string type)
        {
            Console.WriteLine("Введите имя файла");
            fileName = Console.ReadLine();
            string filePath = path + fileName + type;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Файл удален");
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        public static void create_json()
        {
            Console.WriteLine("Введите имя файла");
            String fileName = path + Console.ReadLine() + ".json";
            Console.WriteLine("Введите данные для записи");
            String text = Console.ReadLine();
            Data data = new Data(text);
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine("Запись произведена");
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadLine();
        }

        public static void read_json()
        {
            Console.WriteLine("Введите имя файла");
            string fileName = path + Console.ReadLine() + ".json";
            string jsonText = File.ReadAllText(fileName);
            Data mydata = JsonSerializer.Deserialize<Data>(jsonText);
            Console.WriteLine(mydata.text);
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        class Data
        {
            public string text { get; set; }
            public Data(string text)
            {
                this.text = text;
            }
        }

        public static void create_xml()
        {
            Console.WriteLine("Введите имя файла");
            String fileName = path + Console.ReadLine() + ".xml";
            Console.WriteLine("Введите данные для записи");
            String text = Console.ReadLine();
            XDocument doc = new XDocument(
                new XElement("RootElement",
                   new XElement("text", text)
                )
            );
            doc.Save(fileName);
            Console.WriteLine("XML файл создан успешно");
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        public static void read_xml()
        {
            Console.WriteLine("Введите имя файла");
            string fileName = path + Console.ReadLine() + ".xml";
            XDocument doc = XDocument.Load(fileName);
            XElement root = doc.Root;
            foreach (XElement element in root.Elements())
            {
                Console.WriteLine($"{element.Name}: {element.Value}");
            }
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }
        
        public static void create_zip()
        {
            Console.WriteLine("Введите имя файла для архивации");
            string fileName = Console.ReadLine();
            Console.WriteLine("Введите тип файла для архивации");
            string fileType = Console.ReadLine();
            string sourceFile = path + fileName + fileType;
            string compressedFile = path + fileName +".zip";
            using (FileStream sourseStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using(GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourseStream.CopyTo(compressionStream);
                        Console.WriteLine($"Сжатие файла {fileName} завершено.");
                    }
                }
            }
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        public static void read_zip()
        {
            Console.WriteLine("Введите имя архива для чтения");
            string fileName = Console.ReadLine();
            string sourceFile = path + fileName + ".zip";
            string decompressedFile = path + fileName + "_decompressed.txt";
            using (FileStream sourceStream = new(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(decompressedFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
            Console.WriteLine("Данные успешно извлечены в файл");
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }
    }
}
    
