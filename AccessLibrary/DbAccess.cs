using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * https://stackoverflow.com/questions/26824464/parsing-itunes-podcast-xml-in-c-sharp-using-system-xml
 * https://stackoverflow.com/questions/43565598/uwp-create-database-with-sqlite
 * https://translate.google.com/translate?hl=en&sl=zh-CN&tl=en&u=http%3A%2F%2Fwww.cnblogs.com%2Fms-uap%2Fp%2F4798269.html&sandbox=1
 * 
 */

namespace AccessLibrary
{
    public class DbAccess
    {
        // Create channels table
        // Channel statements
        private static string SQL_CREATE_CHANNELS_TABLE = "create table if not exists channels " +
            "(id int primary key not null, title text not null, description text, link text, " +
            "language text, copyright text, lastbuilddate text, pubdate text, docs text, " +
            "webmaster text, itunes_author text, itunes_subtitle text, itunes_summary text, " +
            "itunes_owner_name text, itunes_owner_email text, itunes_explicit text, itunes_image text, " +
            "itunes_category text);";
        // Insert channel
        private static string SQL_INSERT_CHANNEL_TABLE = "INSERT INTO channels " +
            "[(id, title, description, link, language, copyright, lastbuilddate, pubdate, docs, " +
            "webmaster, itunes_author, itunes_subtitle, itunes_summary, itunes_owner_name, itunes_explicit, " +
            "itunes_image, itunes_category)]  " +
            "VALUES(id, title, description, link, language, copyright, lastbuilddate, pubdate, docs, " +
            "webmaster, itunes_author, itunes_subtitle, itunes_summary, itunes_owner_name, itunes_explicit, " +
            "itunes_image, itunes_category);";
        //Delete channel
        private static string SQL_DELETE_CHANNEL = "delete from channels where id = ?;";

        // Episode statements
        // Create episodes table
        private static string SQL_CREATE_EPISODES_TABLE = "create table if not exists episodes (id int primary key not null, " +
            "foreign key(channel_id) REFERENCES channels(_id) ON UPDATE CASCADE ON DELETE SET NULL, " +
            "title text, link text, guid text, description text, enclosure_url text, enclosure_length integer, " +
            "enclosure_type text, category text, pubdate text, itunes_author text, itunes_explicit text, " +
            "itunes_subtitle text, itunes_summary text, itunes_duration text, keywords text, file_location text);";
        // Insert episode
        private static string SQL_INSERT_EPISODE_TABLE = "insert into episodes " +
            "[(id, channel_id, title, link, guid, description, enclosure_url, enclosure_length, enclosure_type, " +
            "category, pubdate, itunes_author, itunes_explicit, itunes_subtitle, itunes_summary, itunes_duration, keywords, file_location)]" +
            "VALUES(id, channel_id, title, link, guid, description, enclosure_url, enclosure_length, enclosure_type, " +
            "category, pubdate, itunes_author, itunes_explicit, itunes_subtitle, itunes_summary, itunes_duration, keywords, file_location);";

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();
                // create channels and episodes table
                

                SqliteCommand createChannelsTable = new SqliteCommand(SQL_CREATE_CHANNELS_TABLE, db);
                SqliteCommand createEpisodesTable = new SqliteCommand(SQL_CREATE_EPISODES_TABLE, db);

                createChannelsTable.ExecuteReader();
                //createEpisodesTable.ExecuteReader();
            }
        }

        // Add channel command, default values blank for non-null fields
        // get channels
        // read in sample data (channel, episodes)
        // 

        public static void AddChannel(int id, string description, string link, string language,
            string copyright, string lastbuilddate, string pubdate, string docs, string webmaster,
            string itunesAuthor, string itunesSubtitle, string ituneSummary, string itunesOwnerName,
            string itunesOwnerEmail, string itunesExplicit, string itunesImage, string itunesCategory) // include defaults
        {
            // insert command
        }

        public static void AddEpisode(int id, string title, string link, string guid, string description, string
            enclosureUrl, string enclosureLength, string enclosureType, string category, string puDate, string ItunesAuthor,
            string itunesExplicit, string itunesSummary, string itunesDuration, string keywords, string fileLocation)
        {
            // insert command
        }


        public static void AddData(string inputText)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from MyTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }

        public static void AddSampleData()
        {
            // Add channel
            AddChannel(1,"Podcast description", "htto://www.example.com", "en-us", "copyright", "build date", 
                "Sat, 02 Jan 2016 16:00:00 PDT", "docs", "webmaster", "itunes author", "itunes subtitles", "itunes summary",
                "itunes owner name 1", "itunes owner @email.com", "No", "itunes image loc", "itunes category");

            // Add two episodes
        }
    }

}
