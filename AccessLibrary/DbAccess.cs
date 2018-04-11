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
            "(id integer primary key autoincrement, title text not null, description text, link text, " +
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
        private static string SQL_CREATE_EPISODES_TABLE = "create table if not exists episodes (id integer primary key autoincrement, " +
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
                insertEpisode.Parameters.Add(new SqliteParameter("@channel_id", episode.ChannelId));
                insertEpisode.Parameters.Add(new SqliteParameter("@title", episode.Title));
                insertEpisode.Parameters.Add(new SqliteParameter("@link", episode.Link));
                insertEpisode.Parameters.Add(new SqliteParameter("@guid", episode.Guid));
                insertEpisode.Parameters.Add(new SqliteParameter("@description", episode.Description));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_url", episode.EnclosureUrl));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_length", episode.EnclosureLength));
                insertEpisode.Parameters.Add(new SqliteParameter("@enclosure_type", episode.EnclosureType));
                insertEpisode.Parameters.Add(new SqliteParameter("@category", episode.Category));
                insertEpisode.Parameters.Add(new SqliteParameter("@pubdate", episode.PubDate));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_author", episode.ItunesAuthor));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_explicit", episode.ItunesExplicit));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_subtitle", episode.ItunesSubtitle));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_summary", episode.ItunesSummary));
                insertEpisode.Parameters.Add(new SqliteParameter("@itunes_duration", episode.ItunesDuration));
                insertEpisode.Parameters.Add(new SqliteParameter("@keywords", episode.Keywords));
                insertEpisode.Parameters.Add(new SqliteParameter("@file_location", episode.FileLocation));

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
                SqliteDataReader channelsReader = selectChannelsCommand.ExecuteReader();

                while (channelsReader.Read())
                {
                    Channel channel = new Channel();

                    // Populate channel information
                    channel.Id = (int)channelsReader["id"];
                    channel.Title = channelsReader["title"].ToString();
                    channel.Description = channelsReader["description"].ToString();
                    channel.Link = channelsReader["link"].ToString();
                    channel.Language = channelsReader["language"].ToString();
                    channel.Copyright = channelsReader["copyright"].ToString();
                    channel.LastBuildDate = DateTime.Parse(channelsReader["lastbuilddate"].ToString());
                    channel.PubDate = DateTime.Parse(channelsReader["pubdate"].ToString());
                    channel.Docs = channelsReader["docs"].ToString();
                    channel.Webmaster = channelsReader["webmaster"].ToString();
                    channel.ItunesAuthor = channelsReader["itunes_author"].ToString();
                    channel.ItunesSubtitle = channelsReader["itunes_subtitle"].ToString();
                    channel.ItunesSummary = channelsReader["itunes_summary"].ToString();
                    channel.ItunesOwnerName = channelsReader["itunes_owner_name"].ToString();
                    channel.ItunesOwnerEmail = channelsReader["itunes_owner_email"].ToString();
                    channel.ItunesExplicit = channelsReader["itunes_explicit"].ToString();
                    //channel.ItunesImage = channelsReader["itunes_image"].ToString();
                    channel.ItunesCategory = channelsReader["itunes_category"].ToString();

                    // Add channel to list
                    SqliteCommand selectEpisodesCommand = new SqliteCommand("select * from episodes where id = " + channel.Id, db);

                    using (SqliteDataReader episodesReader = selectEpisodesCommand.ExecuteReader())
                    {
                        while (episodesReader.Read())
                        {
                            Episode episode = new Episode();

                            // Populate episode information
                            episode.Id = (int)episodesReader["id"];
                            episode.ChannelId = (int)episodesReader["channel_id"];
                            episode.Title = episodesReader["title"].ToString();
                            episode.Link = episodesReader["link"].ToString();
                            episode.Guid = episodesReader["guid"].ToString();
                            episode.Description = episodesReader["description"].ToString();
                            episode.EnclosureUrl = episodesReader["enclosure_url"].ToString();
                            episode.EnclosureLength = (int)episodesReader["enclosure_length"];
                            episode.EnclosureType = episodesReader["enclosure_type"].ToString();
                            episode.Category = episodesReader["category"].ToString();
                            episode.PubDate = DateTime.Parse(episodesReader["pubdate"].ToString());
                            episode.ItunesAuthor = episodesReader["itunes_author"].ToString();
                            episode.ItunesExplicit = episodesReader["itunes_explicit"].ToString();
                            episode.ItunesSubtitle = episodesReader["itunes_subtitle"].ToString();
                            episode.ItunesSummary = episodesReader["itunes_summary"].ToString();
                            episode.ItunesDuration = episodesReader["itunes_duration"].ToString();
                            episode.Keywords = episodesReader["keywords"].ToString();
                            episode.FileLocation = episodesReader["file_location"].ToString();

                            // Add episode associated with channel
                            channel.EpisodeList.Add(episode);
                        }
                    }
                    // Add channel to channel list
                    channelList.Add(channel);
                }
                db.Close();
            }
            return channelList;
        }
    }
}
