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



    }
}
