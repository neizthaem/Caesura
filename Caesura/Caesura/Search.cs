﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    class Search
    {

        public IDatabase database = null;

        // Get Files that have ALL of the tags in the list
        static List<String> getFilesWithTags(params string[] tags)
        {
            return null;
        }

        // Get Files that have AT LEAST ONE of the tags in the list
        static List<String> getFilesContainingTags(params string[] tags)
        {
            return null;
        }

        // Get Files that DO NOT HAVE ALL of the tags in the list
        static List<String> getFilesWithoutTags(params string[] tags)
        {
            return null;
        }

        // Get Files that DO NOT HAVE ANY of the tags in the list
        static List<String> getFilesNotContainingTags(params string[] tags)
        {
            return null;
        }

    }
}
