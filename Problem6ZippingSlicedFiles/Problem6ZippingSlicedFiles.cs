using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Problem5SlicingFiles
{
    class Problem6ZippingSlicedFiles
    {
        // PROBLEM ASSIGNMENT
        //
        // Write a program that takes any file and slices it to n parts and compress them. Write the following methods:
        //  - Slice(string sourceFile, string destinationDirectory, int parts) - slices the given source file into n parts compress and saves them in destinationDirectory.
        //  - Assemble(List<string> files, string destinationDirectory) - decompress and combines all files into one, in the order they are passed, and saves the result in destinationDirectory.
        
        
        // SOLUTION:

        public static void Main()
        {
            //SliceAndCompress("photo.jpeg", @"C:\Users\User\Desktop\SoftUni Fundamentals level\Advanced C#\6. StreamsAndFiles\StreamsAndFiles\Problem6ZippingSlicedFiles\bin\Debug\", 4);
            List<string> filesToAssemble = new List<string>();
            filesToAssemble.Add("photo part-0.gz");
            filesToAssemble.Add("photo part-1.gz");
            filesToAssemble.Add("photo part-2.gz");
            filesToAssemble.Add("photo part-3.gz");
            DecompressAndAssemble(filesToAssemble, @"C:\Users\User\Desktop\SoftUni Fundamentals level\Advanced C#\6. StreamsAndFiles\StreamsAndFiles\Problem6ZippingSlicedFiles\bin\Debug\");
        }

        public static void SliceAndCompress(string sourceFile, string destinationDirectory, int parts)
        {
            FileStream fileReader = new FileStream(sourceFile, FileMode.Open);
            using (fileReader)
            {
                long partByteSize = fileReader.Length / parts;
                byte[] buffer = new Byte[4096];
                for (int i = 0; i < parts; i++)
                {
                    string compressedFile = sourceFile.Replace(".jpeg",".gz");
                    FileStream partCreate = new FileStream(destinationDirectory
                                                           + compressedFile.Insert(compressedFile.IndexOf('.'), " part-" + i)
                                                           , FileMode.Create);
                    GZipStream compressSream = new GZipStream(partCreate, CompressionMode.Compress, false);
                    using (partCreate)
                    {
                        using (compressSream)
                        {
                            if (i < parts - 1)
                            {
                                while (fileReader.Position < partByteSize * (i + 1))
                                {
                                    int readBytes = fileReader.Read(buffer, 0, buffer.Length);
                                    compressSream.Write(buffer, 0, readBytes);
                                }
                            }
                            else
                            {
                                while (fileReader.Position < fileReader.Length)
                                {
                                    int readBytes = fileReader.Read(buffer, 0, buffer.Length);
                                    compressSream.Write(buffer, 0, readBytes);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void DecompressAndAssemble(List<string> files, string destinationDirectory)
        {
            string fileExtension = ".jpeg";
            FileStream creator = new FileStream(destinationDirectory + "AssembledDecompressedFile" + fileExtension, FileMode.Create);
            FileStream reader = new FileStream(files[0], FileMode.Open);
            GZipStream decompressStream = new GZipStream(reader, CompressionMode.Decompress, false);
            byte[] buffer = new Byte[4096];
            using (reader)
            {
                using (decompressStream)
                {
                    using (creator)
                    {
                        while (true)
                        {
                            int readBytes = decompressStream.Read(buffer, 0, buffer.Length);
                            if (readBytes == 0)
                            {
                                break;
                            }
                            creator.Write(buffer, 0, readBytes);
                        }
                    }
                }
            }
            for (int i = 1; i < files.Count; i++)
            {
                FileStream partReader = new FileStream(files[i], FileMode.Open);
                FileStream writer = new FileStream(destinationDirectory + "AssembledDecompressedFile" + fileExtension, FileMode.Append);
                GZipStream decompress = new GZipStream(partReader, CompressionMode.Decompress, false);
                using (partReader)
                {
                    using (decompress)
                    {
                        using (writer)
                        {
                            while (true)
                            {
                                int readBytes = decompress.Read(buffer, 0, buffer.Length);
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
}
