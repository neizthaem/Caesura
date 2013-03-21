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

        /**
         * Returns true if the file exists false otherwise
         */
        public static Boolean FileExists(String path, String file)
        {
            return false;
        }

        /**
         * Returns all of tags associated with the file at path
         **/
        public static List<String> GetTags(String path, String file)
        {
            return null;
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
