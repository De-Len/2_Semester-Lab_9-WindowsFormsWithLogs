using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

// Комменты и маненечко кода от Persona Grata - ChatGPT (или claude 3 Haiku)
namespace Lab_8_WindowsForms
{
    internal class Model
    {
        public List<string> SyncDirectory(string Path1, string Path2, DirectoryChoice directoryChoice)
        {
            List<string> result = new List<string>();

            List<string> createdFiles = new List<string>();
            List<string> deletedFiles = new List<string>();
            List<string> replacedFiles = new List<string>();
            List<string> filesPath1;
            List<string> filesPath2;

            try
            {
                filesPath1 = Directory.GetFiles(Path1).ToList<string>();
            }
            catch (Exception)
            {
                result.Add("Ошибка: бяка в пути 1 директории");
                return result;
            }

            try
            {
                filesPath2 = Directory.GetFiles(Path2).ToList<string>();
            }
            catch (Exception)
            {
                result.Add("Ошибка: бяка в пути 2 директории");
                return result;
            }

            // Сравниваем имена файлов в директориях
            var fileNames1 = filesPath1.Select(file => Path.GetFileName(file));
            var fileNames2 = filesPath2.Select(file => Path.GetFileName(file));

            if (fileNames1.SequenceEqual(fileNames2))
            {
                result.Add("Ошибка: директории уже синхронизированы");
                return result;
            }

            // Файлы, которые есть в filesPath1, но нет в filesPath2
            List<string> missingFilesInPath2 = fileNames1.Except(fileNames2).ToList();
            // Файлы, которые есть в filesPath2, но нет в filesPath1
            List<string> missingFilesInPath1 = fileNames2.Except(fileNames1).ToList();

            // Файлы, которые есть в обеих директориях, но имеют разное содержимое 
            List<string> modifiedFiles = filesPath1.Intersect(filesPath2)
                                                 .Where(file => !FilesAreEqual(file, Path.Combine(Path2, Path.GetFileName(file))))
                                                 .ToList();

            if (directoryChoice == DirectoryChoice.FirstDirectory)
            {
                CopyMissingFiles(missingFilesInPath1, Path2, Path1, createdFiles, "CREATED");
                DeleteFiles(missingFilesInPath2, Path1, deletedFiles, "DELETED");
                ReplaceFiles(modifiedFiles, Path2, Path1, replacedFiles, "REPLACED");
            }
            else if (directoryChoice == DirectoryChoice.SecondDirectory)
            {
                CopyMissingFiles(missingFilesInPath2, Path1, Path2, createdFiles, "CREATED");
                DeleteFiles(missingFilesInPath1, Path2, deletedFiles, "DELETED");
                ReplaceFiles(modifiedFiles, Path1, Path2, replacedFiles, "REPLACED");
            }

            result.AddRange(createdFiles);
            result.AddRange(deletedFiles);
            result.AddRange(replacedFiles);

            return result;
        }

        private void CopyMissingFiles(List<string> missingFiles, string sourceDir, string destinationDir, List<string> operation, string operationType)
        {
            foreach (string fileName in missingFiles)
            {
                string sourceFile = Path.Combine(sourceDir, fileName);
                string destinationFile = Path.Combine(destinationDir, fileName);

                File.Copy(sourceFile, destinationFile, true); // Переписываем файл, если уже существует
                operation.Add($"{operationType}: {fileName}");
            }
        }

        private void DeleteFiles(List<string> filesToDelete, string directoryPath, List<string> operation, string operationType)
        {
            foreach (string fileName in filesToDelete)
            {
                string filePath = Path.Combine(directoryPath, fileName);
                File.Delete(filePath);
                operation.Add($"{operationType}: {fileName}");
            }
        }

        private void ReplaceFiles(List<string> modifiedFiles, string sourceDir, string destinationDir, List<string> operation, string operationType)
        {
            foreach (string fileName in modifiedFiles)
            {
                string sourceFile = Path.Combine(sourceDir, fileName);
                string destinationFile = Path.Combine(destinationDir, fileName);

                File.Delete(destinationFile);
                File.Copy(sourceFile, destinationFile, true); // Переписываем файл, если уже существует
                operation.Add($"{operationType}: {fileName}");
            }
        }

        static bool FilesAreEqual(string filePath1, string filePath2)
        {
            byte[] bytes1 = File.ReadAllBytes(filePath1);
            byte[] bytes2 = File.ReadAllBytes(filePath2);
            return bytes1.SequenceEqual(bytes2);
        }

        public static void WriteLog(Quote q)
        {
            var settings = new System.Xml.XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            using (StreamWriter streamWriter = new StreamWriter($"C:\\Users\\princedelen\\source\\repos\\Lab_9-WindowsFormsApp1Update\\Lab_8-WindowsForms\\Logs\\{DateTime.Now.ToString("dd.MM.yyyy")}.xml", true))
            {
                using (XmlWriter writer = XmlWriter.Create(streamWriter, settings))
                {
                    writer.WriteStartElement("Log");
                    writer.WriteAttributeString("DateTime", q.Date);
                    writer.WriteElementString("Type", q.Type);
                    writer.WriteElementString("Message", q.Message);
                    writer.WriteEndElement();
                }
            }


            using (FileStream fs = new FileStream($"C:\\Users\\princedelen\\source\\repos\\Lab_9-WindowsFormsApp1Update\\Lab_8-WindowsForms\\Logs\\{DateTime.Now.ToString("dd.MM.yyyy")}.json", FileMode.Append))
            {
                using (StreamWriter streamWriter = new StreamWriter(fs))
                {
                    using (JsonWriter writer = new JsonTextWriter(streamWriter))
                    {
                        if (fs.Length > 0)
                        {
                            streamWriter.WriteLine(","); // Разделитель
                        }
                        writer.WriteStartObject();
                        writer.WritePropertyName("Log");
                        writer.WriteStartObject();
                        writer.WritePropertyName("DateTime");
                        writer.WriteValue(q.Date);
                        writer.WritePropertyName("Type");
                        writer.WriteValue(q.Type);
                        writer.WritePropertyName("Message");
                        writer.WriteValue(q.Message);
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
                }
            }
        }
    }
    public class Quote
    {
        public string Date;
        public string Message;
        public string Type;
    }

    // "Кичливость философии не совмещается со смирением"
    public enum DirectoryChoice
    {
        FirstDirectory,
        SecondDirectory
    }
}




