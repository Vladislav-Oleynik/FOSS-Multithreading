using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace NewFOSSThread
{
    class Files
    {
        public event Action WorkCompleted;
        public delegate void MySecondDelegate();
        public event MySecondDelegate writeFileInfo;
        public List<FileInfo> myFiles = new List<FileInfo>();
        public DirectoryInfo myDir = null;
        private FileInfo oneFile = null;
        private string fileExtension;
        int countFiles = 0;

        public Files(DirectoryInfo dir, string extension)
        {
            myDir = dir;
            fileExtension = extension;
        }

        public void loadFiles()
        {
            myDirs(myDir);
            WorkCompleted();
        }
        
        public int getCountOfFiles()
        {
            countFiles++;
            return countFiles;
        }

        public string writeOneFile()
        {
            return oneFile.Name;
        }

        private void myDirs(DirectoryInfo dir)
        {
            FileInfo[] files = null;
            DirectoryInfo[] directories = null;
            try
            {
                files = dir.GetFiles("*" + fileExtension);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    myFiles.Add(file);
                    oneFile = file;
                    writeFileInfo();
                }
                directories = dir.GetDirectories();
                foreach (DirectoryInfo subDir in directories)
                {
                    myDirs(subDir);
                }
            }
        }
    }
}
