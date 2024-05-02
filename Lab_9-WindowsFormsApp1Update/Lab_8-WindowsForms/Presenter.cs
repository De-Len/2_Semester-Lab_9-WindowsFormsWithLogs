﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;



namespace Lab_8_WindowsForms
{
    internal class Presenter
    {
        List<string> createdFiles = new List<string>();
        List<string> deletedFiles = new List<string>();
        List<string> replacedFiles = new List<string>();
        public IView _iView;
        private View _view;
        private Model _model;

        public Presenter(IView newView)
        {
            _iView = newView;
            _model = new Model();
            _iView.SyncFirstDirectory += new EventHandler<EventArgs>((sender, e) => Sync(sender, e, DirectoryChoice.FirstDirectory));
            _iView.SyncSecondDirectory += new EventHandler<EventArgs>((sender, e) => Sync(sender, e, DirectoryChoice.SecondDirectory));
        }

        private void Sync(object sender, EventArgs e, DirectoryChoice directoryChoice)
        {
            string path1 = _iView.FirstPath();
            string path2 = _iView.SecondPath();
            string logMessage;

            List<string> result = _model.SyncDirectory(path1, path2, directoryChoice);

            if (result[0] == "Ошибка: директории уже синхронизированы")
            {
                logMessage = "Директории уже синхронизированы";
                MessageBox.Show(logMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", logMessage);
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 1 директории")
            {
                logMessage = "У вас бяка в пути 1 директории";
                MessageBox.Show(logMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", logMessage);
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 2 директории")
            {
                logMessage = "У вас бяка в пути 2 директории";
                MessageBox.Show(logMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", logMessage);
                return;
            }

            List<string> createdFiles = new List<string>();
            List<string> deletedFiles = new List<string>();
            List<string> replacedFiles = new List<string>();

            foreach (string file in result)
            {
                if (file.StartsWith("CREATED: "))
                {
                    createdFiles.Add(file.Substring(9));
                }
                else if (file.StartsWith("DELETED: "))
                {
                    deletedFiles.Add(file.Substring(9));
                }
                else if (file.StartsWith("REPLACED: "))
                {
                    replacedFiles.Add(file.Substring(10));
                }
            }

            foreach (string fileInList in createdFiles)
            {
                string action = "создан";

                logMessage = $"Файл \"{fileInList}\" {action} в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории";
                _iView.LogOutput += logMessage + "\n";
                XmlAndJsonLog("Info", logMessage);
            }

            foreach (string fileInList in deletedFiles)
            {
                string action = "удалён";

                logMessage = $"Файл \"{fileInList}\" {action} в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории";
                _iView.LogOutput += logMessage + "\n";
                XmlAndJsonLog("Info", logMessage);
            }

            foreach (string fileInList in replacedFiles)
            {
                string action = "изменён";

                logMessage = $"Файл \"{fileInList}\" {action} в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории";
                _iView.LogOutput += logMessage + "\n";
                XmlAndJsonLog("Info", logMessage);
            }
        }


        public static void XmlAndJsonLog(string type, string message)
        {
            Quote quote = new Quote
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy|HH:mm:ss"),
                Type = type,
                Message = message,
            };
            Model.WriteLog(quote);
        }

    }
}

