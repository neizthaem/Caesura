using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    public class Search
    {

        public static IDatabase database = null;

        // Get Files that have ALL of the tags in the list
        public static List<String> getFilesWithTags(params string[] tags)
        {
            List<String> list = new List<String>();
            return list;
        }

        private List<String> getFilesWithTag(string tags)
        {
            return null;
        }

        // Get Files that have AT LEAST ONE of the tags in the list
        public static List<String> getFilesContainingTags(params string[] tags)
        {
            List<String> list = new List<String>();
            return list;
        }

        // Get Files that DO NOT HAVE ANY of the tags in the list
        public static List<String> getFilesNotContainingTags(params string[] tags)
        {
            List<String> list = new List<String>();
            return list;
        }

    }
}
