using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problem5SlicingFiles
{
    class Problem5SlicingFiles
    {
        public static void Main()
        {
            //Slice("BBC.Planet.Dinosaur.1of6.Lost.World.PDTV.XviD.AC3.MVGroup.org.avi", @"C:\Users\User\Desktop\SoftUni Fundamentals level\Advanced C#\6. StreamsAndFiles\StreamsAndFiles\Problem5SlicingFiles\bin\Debug\", 4);
            List<string> filesToAssemble = new List<string>();
            filesToAssemble.Add("BBC part-0.Planet.Dinosaur.1of6.Lost.World.PDTV.XviD.AC3.MVGroup.org.avi");
            filesToAssemble.Add("BBC part-1.Planet.Dinosaur.1of6.Lost.World.PDTV.XviD.AC3.MVGroup.org.avi");
            filesToAssemble.Add("BBC part-2.Planet.Dinosaur.1of6.Lost.World.PDTV.XviD.AC3.MVGroup.org.avi");
            filesToAssemble.Add("BBC part-3.Planet.Dinosaur.1of6.Lost.World.PDTV.XviD.AC3.MVGroup.org.avi");
            //filesToAssemble.Add("photo part-4.jpg");
            //filesToAssemble.Add("photo part-5.jpg");
            //filesToAssemble.Add("photo part-6.jpg");
            Assemble(filesToAssemble, @"C:\Users\User\Desktop\SoftUni Fundamentals level\Advanced C#\6. StreamsAndFiles\StreamsAndFiles\Problem5SlicingFiles\bin\Debug\");
            //Console.Write("Press any key to continue . . . ");
            //Console.ReadKey(true);
        }

        public static void Slice(string sourceFile, string destinationDirectory, int parts)
        {
            FileStream fileReader = new FileStream(sourceFile, FileMode.Open);
            using (fileReader)
            {
                long partByteSize = fileReader.Length / parts;
                //				long lastPartSize = fileReader.Length % parts;
                //				Console.WriteLine("The file is {0} bytes. The part size will be {1} bytes. The last part size is {2} bytes."
                //				                  , fileReader.Length, partByteSize, lastPartSize);
                byte[] buffer = new Byte[4096];
                for (int i = 0; i < parts; i++)
                {
                    FileStream partCreate = new FileStream(destinationDirectory
                                                           + sourceFile.Insert(sourceFile.IndexOf('.'), " part-" + i)
                                                           , FileMode.Create);
                    using (partCreate)
                    {
                        if (i < parts - 1)
                        {
                            while (fileReader.Position < partByteSize * (i + 1))
                            {
                                int readBytes = fileReader.Read(buffer, 0, buffer.Length);
                                partCreate.Write(buffer, 0, readBytes);
                            }
                        }
                        else
                        {
                            while (fileReader.Position < fileReader.Length)
                            {
                                int readBytes = fileReader.Read(buffer, 0, buffer.Length);
                                partCreate.Write(buffer, 0, readBytes);
                            }
                        }
                    }
                }
            }
        }

        public static void Assemble(List<string> files, string destinationDirectory)
        {
            string fileExtension = files[0].Substring(files[0].IndexOf('.'));
            FileStream creator = new FileStream(destinationDirectory + "AssembledFile" + fileExtension, FileMode.Create);
            FileStream reader = new FileStream(files[0], FileMode.Open);
            byte[] buffer = new Byte[4096];
            using (reader)
            {
                using (creator)
                {
                    while (true)
                    {
                        int readBytes = reader.Read(buffer, 0, buffer.Length);
                        if (readBytes == 0)
                        {
                            break;
                        }
                        creator.Write(buffer, 0, readBytes);
                    }
                }
            }
            for (int i = 1; i < files.Count; i++)
            {
                FileStream partReader = new FileStream(files[i], FileMode.Open);
                FileStream writer = new FileStream(destinationDirectory + "AssembledFile" + fileExtension, FileMode.Append);
                using (partReader)
                {
                    using (writer)
                    {
                        while (true)
                        {
                            int readBytes = partReader.Read(buffer, 0, buffer.Length);
                            if (readBytes == 0)
                            {
                                break;
                            }
                            writer.Write(buffer, 0, readBytes);
                        }
                    }
                }
            }
        }
    }
}
