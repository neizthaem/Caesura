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
        public static List<string> getFilesWithTags(params String[] tags)
        {

            if (database == null || tags.Length == 0)
                return new List<String>();

            List<String> temp;
            List<String> list = getFilesWithTag(tags[0]);

            for (int i = 1; i < tags.Length; i++)
            {
                temp = getFilesWithTag(tags[i]);
                list = list.Intersect(temp).ToList<String>();
            }
            return list;
        }

        private static List<String> getFilesWithTag(String tag)
        {
            IQueryable<String> list = from t in database.Tags
                                where t.TagName == tag
                                select t.FilePath;
            return list.ToList<String>();
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
