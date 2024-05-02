using System;
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

            List<string> result = _model.SyncDirectory(path1, path2, directoryChoice);

            if (result[0] == "Ошибка: директории уже синхронизированы")
            {
                MessageBox.Show("Директории уже синхронизированы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", "Директории уже синхронизированы");
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 1 директории")
            {
                MessageBox.Show("У вас бяка в пути 1 директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", "У вас бяка в пути 1 директории");
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 2 директории")
            {
                MessageBox.Show("У вас бяка в пути 2 директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XmlAndJsonLog("Error", "У вас бяка в пути 2 директории");
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
                _iView.LogOutput += $"Файл \"{fileInList}\" создан в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
                XmlAndJsonLog("Info", $"Файл \"{fileInList}\" создан в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n");
            }
            foreach (string fileInList in deletedFiles)
            {
                _iView.LogOutput += $"Файл \"{fileInList}\" удалён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
                XmlAndJsonLog("Info", $"Файл \"{fileInList}\" удалён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n");
            }
            foreach (string fileInList in replacedFiles)
            {
                _iView.LogOutput += $"Файл \"{fileInList}\" изменён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
                XmlAndJsonLog("Info", $"Файл \"{fileInList}\" изменён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n");
            }
            NLog.LogManager.Shutdown();
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

