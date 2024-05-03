using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_8_WindowsForms
{
    public partial class View : Form, IView
    {
        private Presenter _presenter;

        string IView.LogOutput
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        string IView.FirstPath()
        {
            return textBox1.Text;
        }
        string IView.SecondPath()
        {
            return textBox2.Text;
        }

        public View()
        {
            InitializeComponent();
            _presenter = new Presenter(this);

            syncFirstDirectoryButton.Click += syncFirstDirectoryButton_Click;
            syncSecondDirectoryButton.Click += SyncFirstDirectoryButton_Click;
        }

        public event EventHandler<EventArgs> SyncFirstDirectory;
        public event EventHandler<EventArgs> SyncSecondDirectory;

        private void syncFirstDirectoryButton_Click(object sender, EventArgs e)
        {
            SyncFirstDirectory(sender, e);
        }

        private void SyncFirstDirectoryButton_Click(object sender, EventArgs e)
        {
            SyncSecondDirectory(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
