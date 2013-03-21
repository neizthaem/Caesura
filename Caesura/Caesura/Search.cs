using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    public static class Search
    {

        /**
         * Returns true if the file exists false otherwise
         */
        public static Boolean FileExists(String path, String file)
        {
            return File.Exists(path + file);
        }

        /**
         * Returns all of tags associated with the file at path
         **/
        public static List<String> GetTags(String path, String file)
        {
            List<String> tags = new List<String>();

            String line; String found = "notfound untagged";
            System.IO.StreamReader fh = new System.IO.StreamReader(path + "info.tags");
            while ((line = fh.ReadLine()) != null)
            {
                if (line.Split('\t')[0] == file) { found = line; break; }
                found = line;
            }

            String[] values = found.Split('\t');
            for (int i = 1; i < values.Length; i++)
            {
                tags.Add(values[i]);
            }
            fh.Close();
            return tags;
        }

        private static bool FileExists(string p)
        {
            throw new NotImplementedException();
        }

        /**
         * Returns a list of files at the path that contain each tag(s)
         **/
        public static List<String> GetFilesTaggedAs(String path, params String[] tags) {
            return null;
        }

        /**
         * Returns a list of files at the path that at least one of the tag(s)
         **/
        public static List<String> GetFilesContainingTags(String path, params String[] tags)
        {
            return null;
        }



    }
}
