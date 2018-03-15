using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            "webmaster, itunes_author, itunes_subtitle, itunes_summary, itunes_owner_name, itunes_owner_email, itunes_explicit, " +
            "itunes_image, itunes_category)]  " +
            "VALUES(@id, @title, @description, @link, @language, @copyright, @lastbuilddate, @pubdate, @docs, " +
            "@webmaster, @itunes_author, @itunes_subtitle, @itunes_summary, @itunes_owner_name, @itunes_owner_email, @itunes_explicit, " +
            "@itunes_image, @itunes_category);";
        // Delete channel
        private static string SQL_DELETE_CHANNEL = "delete from channels where id = @id;";

        // Episode statements
        // Create episodes table
        private static string SQL_CREATE_EPISODES_TABLE = "create table if not exists episodes (id int primary key not null, " +
            "int channel_id not null, title text, link text, guid text, description text, enclosure_url text, enclosure_length integer, " +
            "enclosure_type text, category text, pubdate text, itunes_author text, itunes_explicit text, " +
            "itunes_subtitle text, itunes_summary text, itunes_duration text, keywords text, file_location text);";
        // Insert episode
        private static string SQL_INSERT_EPISODE_TABLE = "insert into episodes " +
            "[(id, channel_id, title, link, guid, description, enclosure_url, enclosure_length, enclosure_type, " +
            "category, pubdate, itunes_author, itunes_explicit, itunes_subtitle, itunes_summary, itunes_duration, keywords, file_location)]" +
            "VALUES(@id, @channel_id, @title, @link, @guid, @description, @enclosure_url, @enclosure_length, @enclosure_type, " +
            "@category, @pubdate, @itunes_author, @itunes_explicit, @itunes_subtitle, @itunes_summary, @itunes_duration, @keywords, @file_location);";
        // Delete channel
        private static string SQL_DELETE_EPISODE = "delete from episodes where id = @id;";

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                // create channels and episodes table
                SqliteCommand createChannelsTable = new SqliteCommand(SQL_CREATE_CHANNELS_TABLE, db);
                SqliteCommand createEpisodesTable = new SqliteCommand(SQL_CREATE_EPISODES_TABLE, db);

                createChannelsTable.ExecuteReader();
                createEpisodesTable.ExecuteReader();

                db.Close();
            }
        }

        public static void AddChannel(Channel channel)
        {
            // insert command
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand insertChannel = new SqliteCommand(SQL_INSERT_CHANNEL_TABLE, db);
                // add parameters
                // TODO important, fix id (check db, +1)
                //insertChannel.Parameters.Add(new SqliteParameter("@id", channel.id));
                insertChannel.Parameters.Add(new SqliteParameter("@title", channel.Title));
                insertChannel.Parameters.Add(new SqliteParameter("@description", channel.Description));
                insertChannel.Parameters.Add(new SqliteParameter("@link", channel.Link));
                insertChannel.Parameters.Add(new SqliteParameter("@language", channel.Language));
                insertChannel.Parameters.Add(new SqliteParameter("@copyright", channel.Copyright));
                insertChannel.Parameters.Add(new SqliteParameter("@lastbuilddate", channel.LastBuildDate));
                insertChannel.Parameters.Add(new SqliteParameter("@pubdate", channel.PubDate));
                insertChannel.Parameters.Add(new SqliteParameter("@docs", channel.Docs));
                insertChannel.Parameters.Add(new SqliteParameter("@webmaster", channel.Webmaster));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_author", channel.ItunesAuthor));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_subtitle", channel.ItunesSubtitle));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_summary", channel.ItunesSummary));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_owner_name", channel.ItunesOwnerName));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_owner_email", channel.ItunesOwnerEmail));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_explicit", channel.ItunesExplicit));
                //insertChannel.Parameters.Add(new SqliteParameter("@itunes_image", channel.itunesImage));
                insertChannel.Parameters.Add(new SqliteParameter("@itunes_category", channel.ItunesCategory));

                insertChannel.ExecuteReader();

                db.Close();
            }
        }

        public static void DeleteChannel(int id)
        {
            // delete commands
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand deleteChannel = new SqliteCommand(SQL_DELETE_CHANNEL, db);
                SqliteCommand deleteEpisode = new SqliteCommand(SQL_DELETE_EPISODE, db);
                // add parameters
                deleteChannel.Parameters.Add(new SqliteParameter("@id", id));
                deleteEpisode.Parameters.Add(new SqliteParameter("@id", id));

                deleteChannel.ExecuteReader();
                deleteEpisode.ExecuteReader();

                db.Close();
            }
        }

        public static void AddEpisode(Episode episode)
        {
            // insert command
            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand insertEpisode = new SqliteCommand(SQL_INSERT_EPISODE_TABLE, db);
                // add parameters
                // TODO important, fix id (check db, +1)
                //insertChannel.Parameters.Add(new SqliteParameter("@id", id));
                insertEpisode.Parameters.Add(new SqliteParameter("@channel_id", episode.channelId));
                insertEpisode.Parameters.Add(new SqliteParameter("@title", episode.title));
                insertEpisode.Parameters.Add(new SqliteParameter("@link", episode.link));
                insertEpisode.Parameters.Add(new SqliteParameter("@guid", episode.guid));
                insertEpisode.Parameters.Add(new SqliteParameter("@description", episode.description));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_url", episode.enclosureUrl));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_length", episode.enclosureLength));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_type", episode.enclosureType));
                insertEpisode.Parameters.Add(new SqliteParameter("@category", episode.category));
                insertEpisode.Parameters.Add(new SqliteParameter("@pubdate", episode.pubDate));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_author", episode.itunesAuthor));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_explicit", episode.itunesExplicit));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_subtitle", episode.itunesSubtitle));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_summary", episode.itunesSummary));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_duration", episode.itunesDuration));
                insertEpisode.Parameters.Add(new SqliteParameter("@keywords", episode.keywords));
                insertEpisode.Parameters.Add(new SqliteParameter("@file_location", episode.fileLocation));

                insertEpisode.ExecuteReader();

                db.Close();
            }
        }

        public static ObservableCollection<Channel> GetChannels()
        {
            ObservableCollection<Channel> channelList = new ObservableCollection<Channel>();
            

            using (SqliteConnection db = new SqliteConnection("Filename=paragonpodcast.db"))
            {
                db.Open();

                SqliteCommand selectChannelsCommand = new SqliteCommand("select * from channels", db);
                SqliteDataReader channelsQuery = selectChannelsCommand.ExecuteReader();

                while (channelsQuery.Read())
                {
                    // Add new channel
                    Channel channel = new Channel();
                    // Populate channel

                    // Add channel to list

                    SqliteCommand selectEpisodesCommand = new SqliteCommand("select * from episodes where id = " + channel.Id, db);
                    SqliteDataReader episodesQuery = selectEpisodesCommand.ExecuteReader();

                    while (episodesQuery.Read())
                    {
                        // Add episode to channel
                    }
                }
                db.Close();
            }
            return channelList;
        }
    }

}
