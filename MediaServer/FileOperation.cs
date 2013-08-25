using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    class FileOperation
    {
        public static List<FileInfo> getFile(string path, string extName)
        {
            List<FileInfo> lst = new List<FileInfo>();
            getdir(path, extName, lst);
            return lst;
        }
        public static List<String> getPatternFileList(String path, String partten) {
            List<String> fileList = new List<string>();
            getPatternFiles(path, partten, fileList);
            return fileList;
        }
        public static List<String> getExtendFileList(String path, String extName) {
            List<String> fileList = new List<string>();
            getExtendFiles(path, extName, fileList);
            return fileList;
        }
        private static void getPatternFiles(String path, String partten, List<String> fileList)
        {
            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        if (f.Name.ToLower().IndexOf(partten.ToLower()) >= 0)
                        {
                            fileList.Add(f.Name);
                        }
                    }
                    foreach (string d in dir)
                    {
                        getPatternFiles(d, partten, fileList);//递归   
                    }
                }
            }
            catch { };
        }
        private static void getExtendFiles(String path, String extName, List<String> fileList) 
        {
            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            fileList.Add(f.Name);
                        }
                    }
                    foreach (string d in dir)
                    {
                        getExtendFiles(d, extName, fileList);//递归   
                    }
                }
            }
            catch { };
        }
        private static void getdir(string path, string extName, List<FileInfo> lst)
        {
            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }
                    foreach (string d in dir)
                    {
                        getdir(d, extName, lst);//递归   
                    }
                }
            }
            catch { };
        }
    }
}
