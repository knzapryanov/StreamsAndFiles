using System;
using System.IO;

class Problem4CopyBinaryFile
{
    public static void Main()
    {
        FileStream fileStreamRead = new FileStream("photo.jpg", FileMode.Open);
        FileStream fileStreamWrite = new FileStream("copiedPhoto.jpg", FileMode.Create);

        using (fileStreamRead)
        {
            using (fileStreamWrite)
            {
                byte[] buffer = new Byte[4096];
                while (true)
                {
                    int readBytes = fileStreamRead.Read(buffer, 0, buffer.Length);
                    if (readBytes == 0)
                    {
                        break;
                    }
                    fileStreamWrite.Write(buffer, 0, readBytes);
                }
            }
        }

        Console.Write("Press any key to continue . . . ");
        Console.ReadKey(true);
    }
}