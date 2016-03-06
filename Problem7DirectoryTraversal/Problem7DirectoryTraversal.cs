using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

    class Problem7DirectoryTraversal
    {
        static void Main()
        {
            string[] filesInCurrentDirectory = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            // Create dictionary with file extension as key and list of fileinfos as value which will contain fileinfo objects for each file with the current extension
            Dictionary<string, List<FileInfo>> files = new Dictionary<string, List<FileInfo>>();
            for (int i = 0; i < filesInCurrentDirectory.Length; i++)
            {
                FileInfo currentFileInfo = new FileInfo(filesInCurrentDirectory[i]);
                if (files.ContainsKey(currentFileInfo.Extension))
                {
                    files[currentFileInfo.Extension].Add(currentFileInfo);
                }
                else
                {
                    files[currentFileInfo.Extension] = new List<FileInfo>();
                    files[currentFileInfo.Extension].Add(currentFileInfo);
                }
            }

            // Create new dictionary which will contain all extensions and list of fileinfos for the current extension sorted by fileinfo.Lenght(fileSize)
            Dictionary<string, List<FileInfo>> filesSortedBySize = new Dictionary<string, List<FileInfo>>();
            foreach (var item in files)
            {
                filesSortedBySize[item.Key] = (from entry in item.Value orderby entry.Length ascending select entry).ToList();
            }

            // Sort the previous dictionary by extension name in ascending order to have the extension with the same file count sorted
            var sortedFilesByExtensionName = from entry in filesSortedBySize orderby entry.Key ascending select entry;
            // Sort the previous dictionary descending by the count of each list with fileinfos(the extension with the most files will be on top)
            var sortedFilesByExtensionFilesCount = from entry in sortedFilesByExtensionName orderby entry.Value.Count descending select entry;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileStream = File.Create(desktopPath + @"\result.txt");
            fileStream.Close();

            using (var writer = new StreamWriter(desktopPath + @"\result.txt"))
            {
                foreach (var file in sortedFilesByExtensionFilesCount)
                {
                    //Console.WriteLine("Extension: {0} Files with this extension:", file.Key);
                    writer.WriteLine(file.Key);
                    foreach (var fileInfo in file.Value)
                    {
                        double fileSize = (double)fileInfo.Length / 1024.0;
                        //Console.WriteLine("{0} - {1:0.000}kb", fileInfo.Name, fileSize);
                        writer.WriteLine("{0} - {1:0.000}kb", fileInfo.Name, fileSize);
                    }
                    //Console.WriteLine();
                    writer.WriteLine();
                }
            }
        }
    }
