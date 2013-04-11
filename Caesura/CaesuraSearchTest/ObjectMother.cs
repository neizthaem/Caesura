using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caesura;
using System.Threading.Tasks;
using Rhino.Mocks;

namespace CaesuraSearchTest
{
    public class ObjectMother
    {
        public static IDatabase EmptyDatabase()
        {
            MockRepository mocks = new MockRepository();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            return mockDatabase;
        }

        public static IDatabase PopulatedDatabase()
        {
            MockRepository mocks = new MockRepository();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            mockDatabase.Tags.InsertOnSubmit(movie1);
            mockDatabase.Tags.InsertOnSubmit(movie2);
            mockDatabase.Tags.InsertOnSubmit(music1);
            mockDatabase.Tags.InsertOnSubmit(music2);
            mockDatabase.Tags.InsertOnSubmit(anime1);
            mockDatabase.Tags.InsertOnSubmit(anime2);
            mockDatabase.SubmitChanges();
            return mockDatabase;
        }

        private static Tag movie1 = new Tag
        {
            FilePath = "video.avi",
            TagName = "video"
        };

        private static Tag movie2 = new Tag
        {
            FilePath = "video.avi",
            TagName = "avi"
        };

        private static Tag music1 = new Tag
        {
            FilePath = "music.mp3",
            TagName = "audio"
        };

        private static Tag music2 = new Tag
        {
            FilePath = "music.mp3",
            TagName = "mp3"
        };

        private static Tag anime1 = new Tag
        {
            FilePath = "anime.mkv",
            TagName = "video"
        };

        private static Tag anime2 = new Tag
        {
            FilePath = "anime.mkv",
            TagName = "anime"
        };

    }
}
