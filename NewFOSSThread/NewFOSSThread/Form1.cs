using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace NewFOSSThread
{
    public partial class Form1 : Form
    {
        private Files new_files;
        private Thread thread = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(textBoxPath.Text);

                new_files = new Files(dir, comboBox1.Text);
                new_files.writeFileInfo += writeFileIformation;
                new_files.WorkCompleted += _worker_WorkCompleted;
                buttonStart.Enabled = false;
                
                thread = new Thread(new_files.loadFiles);
                thread.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            thread.Abort();
            buttonStart.Enabled = true;
        }

        private void _worker_WorkCompleted()
        {
            Action action = () =>
            {
                richTextBox1.Clear();
                foreach(FileInfo file in new_files.myFiles)
                richTextBox1.AppendText(file.ToString()+"\n");
                buttonStart.Enabled = true;
            };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void writeFileIformation()
        {
            Action action = () =>
            {
                richTextBox1.AppendText(new_files.writeOneFile()+"\n");
                CountFilesLabel.Text = new_files.getCountOfFiles().ToString();
            };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
