using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;

namespace CaesuraTest
{
    public class ObjectMother
    {

        public static String videoFile = "video.avi";
        public static String musicFile = "music.mp3";
        public static String animeFile = "anime.mkv";
        public static String soundFile = "bell.wav";

        public static IDatabase mockEmptyDatabase()
        {
            MockRepository mocks = new MockRepository();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            mockDatabase.Tags = new List<Tag>
            {
            }.AsQueryable<Tag>();
            return mockDatabase;
        }

        public static LINQDatabase EmptyDatabase()
        {
            LINQDatabase db = new LINQDatabase(true);
            ClearDatabase(db);
            return db;
        }

        public static void ClearDatabase(LINQDatabase db)
        {

            // Pending Mail Table
            var mrows = from row in db.PendingMail
                        select row;
            foreach (var row in mrows)
                db.PendingMail.DeleteOnSubmit(row);
            db.SubmitChanges();

            // Owns Table
            var orows = from row in db.Owns
                        select row;
            foreach (var row in orows)
                db.Owns.DeleteOnSubmit(row);
            db.SubmitChanges();

            // Users Table
            var urows = from row in db.Users
                        select row;
            foreach (var row in urows)
                db.Users.DeleteOnSubmit(row);
            db.SubmitChanges();

            // Tags Table
            var trows = from row in db.Tags
                        select row;
            foreach (var row in trows)
                db.Tags.DeleteOnSubmit(row);
            db.SubmitChanges();

            // Files Table
            var frows = from row in db.Files
                        select row;
            foreach (var row in frows)
                db.Files.DeleteOnSubmit(row);
            db.SubmitChanges();

            // TagName Table
            var tnrows = from row in db.TagNames
                         select row;
            foreach (var row in tnrows)
                db.TagNames.DeleteOnSubmit(row);
            db.SubmitChanges();

        }

        public static IDatabase mockPopulatedDatabase()
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

        public static LINQDatabase PopulatedDatabase()
        {
            LINQDatabase db = new LINQDatabase(true);
            ClearDatabase(db);

            CaesFile music = new CaesFile();
            music.Path = musicFile;
            db.Files.InsertOnSubmit(music);

            CaesFile sound = new CaesFile();
            sound.Path = soundFile;
            db.Files.InsertOnSubmit(sound);

            CaesFile anime = new CaesFile();
            anime.Path = animeFile;
            db.Files.InsertOnSubmit(anime);

            CaesFile video = new CaesFile();
            video.Path = videoFile;
            db.Files.InsertOnSubmit(video);

            db.SubmitChanges();

            TagNames tn1 = new TagNames();
            tn1.TagName = "audio";

            TagNames tn2 = new TagNames();
            tn2.TagName = "mp3";

            TagNames tn3 = new TagNames();
            tn3.TagName = "wav";

            TagNames tn4 = new TagNames();
            tn4.TagName = "anime";

            TagNames tn5 = new TagNames();
            tn5.TagName = "video";

            TagNames tn6 = new TagNames();
            tn6.TagName = "mkv";

            TagNames tn7 = new TagNames();
            tn7.TagName = "avi";

            db.TagNames.InsertOnSubmit(tn1);
            db.TagNames.InsertOnSubmit(tn2);
            db.TagNames.InsertOnSubmit(tn3);
            db.TagNames.InsertOnSubmit(tn4);
            db.TagNames.InsertOnSubmit(tn5);
            db.TagNames.InsertOnSubmit(tn6);
            db.TagNames.InsertOnSubmit(tn7);

            db.SubmitChanges();

            // Music File
            Tag m1 = new Tag();
            m1.FilePath = musicFile;
            m1.TagName = "audio";
            db.Tags.InsertOnSubmit(m1);

            Tag m2 = new Tag();
            m2.FilePath = musicFile;
            m2.TagName = "mp3";
            db.Tags.InsertOnSubmit(m2);

            // Sound File
            Tag s1 = new Tag();
            s1.FilePath = soundFile;
            s1.TagName = "audio";
            db.Tags.InsertOnSubmit(s1);

            Tag s2 = new Tag();
            s2.FilePath = soundFile;
            s2.TagName = "wav";
            db.Tags.InsertOnSubmit(s2);

            // Anime File
            Tag a1 = new Tag();
            a1.FilePath = animeFile;
            a1.TagName = "anime";
            db.Tags.InsertOnSubmit(a1);

            Tag a2 = new Tag();
            a2.FilePath = animeFile;
            a2.TagName = "video";
            db.Tags.InsertOnSubmit(a2);

            Tag a3 = new Tag();
            a3.FilePath = animeFile;
            a3.TagName = "mkv";
            db.Tags.InsertOnSubmit(a3);

            // Video File
            Tag v1 = new Tag();
            v1.FilePath = videoFile;
            v1.TagName = "video";
            db.Tags.InsertOnSubmit(v1);

            Tag v2 = new Tag();
            v2.FilePath = videoFile;
            v2.TagName = "avi";
            db.Tags.InsertOnSubmit(v2);

            db.SubmitChanges();

            return db;
        }

    }
}
