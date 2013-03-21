using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public class Search
    {

        public static String tagDir = "C:\\Caesura\\tags";

        public static String buildTagSubDir(String path)
        {
            if (path == "")
            {
                throw new Exception("Invalid Path: empty path");
            }
            String[] pieces = path.Split('\\');
            StringBuilder str = new StringBuilder();
            if (pieces[0] == "C:" && pieces.Length > 1 && pieces[1] == "Caesura")
            {
                throw new Exception("Invalid Path: Recursive");
            }
            for (int i = 1; i < pieces.Length; i++)
            {
                str.Append('\\' + pieces[i]);
            }
            Directory.CreateDirectory(tagDir + str.ToString());
            return tagDir + str.ToString();
        }

        public static String restoreTagSubDir(String path)
        {
            String[] pieces = path.Split('\\');
            StringBuilder str = new StringBuilder();

            if (path == "" || pieces.Length < 2 || pieces[0] != "C:" || pieces[1] != "Caesura")
            {
                throw new Exception("Invalid Path");
            }

            for (int i = 3; i < pieces.Length; i++)
            {
                str.Append('\\' + pieces[i]);
            }
            return "C:" + str.ToString();
        }

        public static void addSearchTagEntry(String dirPath, String fileName, params String[] tags)
        {
            String path = buildTagSubDir(dirPath);
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            String file = path + "\\taginfo";
            StringBuilder str = new StringBuilder();
            str.Append(fileName);
            foreach (String s in tags)
            {
                str.Append('\t' + s);
            }
            str.Append('\n');

            if (!(File.Exists(file)))
            {
                using (FileStream fs = File.Create(file))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(str.ToString());
                    fs.Write(info, 0, info.Length);
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(file))
                {
                    w.Write(str.ToString());
                }
            }
        }

        public static void removeSearchTagEntry(String dirPath, String fileName)
        {
            String tagpath = buildTagSubDir(dirPath);

            String taginfo = tagpath + "\\taginfo";
            StringBuilder str = new StringBuilder();

            if (File.Exists(taginfo))
            {
                String[] lines = File.ReadAllLines(taginfo);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (!(lines[i].Split('\t')[0] == fileName))
                    {
                        str.Append(lines[i] + '\n');
                    }
                }

                File.Delete(taginfo);
                using (FileStream fs = File.Create(taginfo))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(str.ToString());
                    fs.Write(info, 0, info.Length);
                }

            }
            
        }

    }
}
