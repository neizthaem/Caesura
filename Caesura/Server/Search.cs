using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Server;

namespace Server
{
    public class Search
    {

        public static LINQDatabase database = null;

        private static List<String> getFilesWithTag(String tag)
        {
            IQueryable<String> list = from t in database.Tags
                                      where t.TagName == tag
                                      select t.FilePath;
            return list.ToList<String>();
        }

        // Get Files that have ALL of the tags in the list
        public static List<string> getFilesWithTags(params String[] tags)
        {
            if (database == null || tags.Length == 0)
                return new List<String>();

            IQueryable<Tag> table = database.Tags;
            IQueryable<String> lastQuery = null;
            IQueryable<String> temp = null;
            foreach (String tag in tags)
            {
                temp = table.Where(p => p.TagName.Equals(tag)).Select(p => p.FilePath);
                if (lastQuery != null)
                    temp = lastQuery.Join(temp, a => a, b => b, (a, b) => a);
                lastQuery = temp;
            }

            return temp.ToList<String>();
        }

        // Get Files that have AT LEAST ONE of the tags in the list
        public static List<String> getFilesContainingTags(params String[] tags)
        {
            if (database == null || tags.Length == 0)
                return new List<String>();

            var predicate = PredicateBuilder.False<Tag>();
            foreach (String tag in tags)
            {
                predicate = predicate.Or(p => p.TagName.Equals(tag));
            }

            return database.Tags.Where(predicate).Select(p => p.FilePath).Distinct().ToList<String>();

        }

        // Get Files that DO NOT HAVE ANY of the tags in the list
        public static List<String> getFilesNotContainingTags(params string[] tags)
        {
            if (database == null)
                return new List<String>();

            IQueryable<String> allItems = from t in database.Tags
                                          select t.FilePath;
            List<String> all = allItems.Distinct().ToList<String>();

            if (tags.Length == 0)
                return all;

            List<String> temp;
            List<String> list = getFilesWithTag(tags[0]);

            for (int i = 1; i < tags.Length; i++)
            {
                temp = getFilesWithTag(tags[i]);
                list = list.Union(temp).ToList<String>();
            }
            return all.Except(list).ToList<String>();
        }

    }
}
