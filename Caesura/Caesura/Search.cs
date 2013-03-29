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

        // Folder to store all tagging information in
        // Note Currently changes to this mandate changes in some code below
        public static String tagDir = "C:\\Caesura\\tags";

        /* Creates a the required tag sub-dir from a valid absolute path */
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

        /* Reverses the process of buildTagSubDir */
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

        /* Adds a a tag entry for a given file so it can be searched */
        public static void addSearchTagEntry(String dirPath, String fileName, params String[] tags)
        {
            String path = buildTagSubDir(dirPath);
            Directory.CreateDirectory(path);

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

        /* Removes a tagging entry for the given file if it exists */
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

        /* Find content that has been tagged with any of the given tags */
        public static List<String> findContentContainingTags(params String[] tags)
        {
            List<String> file = findContentContainingRec(tagDir, tags.ToList());
            return file;
        }

        public static List<String> findContentContainingRec(String path, List<String> tags)
        {
            List<String> files = new List<String>();

            if (File.Exists(path + "\\taginfo"))
            {
                String[] lines = File.ReadAllLines(path + "\\taginfo");
                foreach (String s in lines)
                {
                    String[] pieces = s.Split('\t');
                    for (int i = 1; i < pieces.Length; i++)
                    {
                        if(tags.Contains(pieces[i]))
                        {
                            files.Add(Search.restoreTagSubDir(path + '\\' + pieces[0]));
                            break;
                        }
                    }
                }
            }
            foreach (String dir in Directory.GetDirectories(path))
            {
                files.AddRange(findContentContainingRec(dir, tags));
            }
            return files;
        }

        /* Find content that has been tagged with all of the given tags */
        public static List<String> findContentWithTags(params String[] tags)
        {
            List<String> file = findContentWithRec(tagDir, tags.ToList());
            return file;
        }

        public static List<String> findContentWithRec(String path, List<String> tags)
        {
            List<String> files = new List<String>();

            if (File.Exists(path + "\\taginfo"))
            {
                String[] lines = File.ReadAllLines(path + "\\taginfo");
                foreach (String s in lines)
                {
                    Boolean toAdd = true;
                    List<String> pieces = s.Split('\t').ToList();
                    String fName = pieces.First();
                    pieces.RemoveAt(0);
                    foreach (String tag in tags)
                    {
                        if (!pieces.Contains(tag))
                        {
                            toAdd = false;
                        }
                    }
                    if (toAdd && tags.Count > 0 || pieces.Count == 0 && tags.Count == 0)
                    {
                        files.Add(Search.restoreTagSubDir(path + '\\' + fName));
                    }
                }
            }
            foreach (String dir in Directory.GetDirectories(path))
            {
                files.AddRange(findContentWithRec(dir, tags));
            }
            return files;
        }

    }
}
