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

        public static String videoFile = "video.avi";
        public static String musicFile = "music.mp3";
        public static String animeFile = "anime.mkv";
        public static String soundFile = "bell.wav";

        public static IDatabase EmptyDatabase()
        {
            MockRepository mocks = new MockRepository();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            mockDatabase.Tags = new List<Tag> {
            }.AsQueryable<Tag>();
            return mockDatabase;
        }

        public static IDatabase PopulatedDatabase()
        {
            MockRepository mocks = new MockRepository();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            mockDatabase.Tags = new List<Tag> {
                new Tag {FilePath = musicFile, TagName = "audio"},
                new Tag {FilePath = musicFile, TagName = "mp3"},

                new Tag {FilePath = soundFile, TagName = "audio"},
                new Tag {FilePath = soundFile, TagName = "wav"},

                new Tag {FilePath = animeFile, TagName = "video"},
                new Tag {FilePath = animeFile, TagName = "anime"},
                new Tag {FilePath = animeFile, TagName = "mkv"},

                new Tag {FilePath = videoFile, TagName = "video"},
                new Tag {FilePath = videoFile, TagName = "avi"}
            }.AsQueryable<Tag>();

            return mockDatabase;
        }

    }
}
