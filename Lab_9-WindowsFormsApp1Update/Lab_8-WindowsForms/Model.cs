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

        private void CopyMissingFiles(List<string> missingFiles, string sourceDir, string destinationDir, List<string> operations, string operationType)
        {
            foreach (string fileName in missingFiles)
            {
                string sourceFile = Path.Combine(sourceDir, fileName);
                string destinationFile = Path.Combine(destinationDir, fileName);

                File.Copy(sourceFile, destinationFile, true); // Переписываем файл, если уже существует
                operations.Add($"{operationType}: {fileName}");
            }
        }

        private void DeleteFiles(List<string> filesToDelete, string directoryPath, List<string> operations, string operationType)
        {
            foreach (string fileName in filesToDelete)
            {
                string filePath = Path.Combine(directoryPath, fileName);
                File.Delete(filePath);
                operations.Add($"{operationType}: {fileName}");
            }
        }

        private void ReplaceFiles(List<string> modifiedFiles, string sourceDir, string destinationDir, List<string> operations, string operationType)
        {
            foreach (string fileName in modifiedFiles)
            {
                string sourceFile = Path.Combine(sourceDir, fileName);
                string destinationFile = Path.Combine(destinationDir, fileName);

                File.Delete(destinationFile);
                File.Copy(sourceFile, destinationFile, true); // Переписываем файл, если уже существует
                operations.Add($"{operationType}: {fileName}");
            }
        }

        static bool FilesAreEqual(string filePath1, string filePath2)
        {
            byte[] bytes1 = File.ReadAllBytes(filePath1);
            byte[] bytes2 = File.ReadAllBytes(filePath2);
            return bytes1.SequenceEqual(bytes2);
        }
    }

    // "Кичливость философии не совмещается со смирением"
    public enum DirectoryChoice
    {
        FirstDirectory,
        SecondDirectory
    }
}




