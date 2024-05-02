using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8_WindowsForms
{
    internal interface IView
    {
        string LogOutput { get; set; }
        string FirstPath();
        string SecondPath();

        event EventHandler<EventArgs> SyncFirstDirectory;
        event EventHandler<EventArgs> SyncSecondDirectory;
    }
}