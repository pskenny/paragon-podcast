using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibrary
{
    public class DbAccess
    {
        // Connection string for DB
        private const string CONNECTION_STRING = "Filename=paragonpodcast.db";
        // Create channels table
        // Channel statements
        private static string SQL_CREATE_CHANNELS_TABLE = "create table if not exists channels" +
            "(id integer primary key autoincrement, title text not null, description text, link text," +
                "language text, copyright text, lastbuilddate text, pubdate text, docs text," +
                "webmaster text);";
        // Insert channel
        private static string SQL_INSERT_CHANNEL_TABLE = "INSERT INTO channels" +
                "(title, description, link, language, copyright, lastbuilddate, pubdate, docs," +
                "webmaster)" +
                "VALUES(@title, @description, @link, @language, @copyright, @lastbuilddate, @pubdate, @docs," +
                "@webmaster);";
        // Delete channel
        private static string SQL_DELETE_CHANNEL = "delete from channels where id = @id;";

        // Episode statements
        // Create episodes table
        private static string SQL_CREATE_EPISODES_TABLE = "create table if not exists episodes (id integer primary key autoincrement," +
                "channel_id int not null, title text, link text, guid text, description text, enclosure_url text, enclosure_length integer," +
                "enclosure_type text, category text, pubdate text, keywords text);";
        // Insert episode
        private static string SQL_INSERT_EPISODE_TABLE = "insert into episodes" +
                "(channel_id, title, link, guid, description, enclosure_url, enclosure_length, enclosure_type," +
                "category, pubdate, keywords)" +
                "VALUES(@channel_id, @title, @link, @guid, @description, @enclosure_url, @enclosure_length, @enclosure_type," +
                "@category, @pubdate, @keywords);";
        // Delete channel
        private static string SQL_DELETE_EPISODE = "delete from episodes where id = @id;";

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                // Create channels and episodes table
                SqliteCommand createChannelsTable = new SqliteCommand(SQL_CREATE_CHANNELS_TABLE, db);
                SqliteCommand createEpisodesTable = new SqliteCommand(SQL_CREATE_EPISODES_TABLE, db);

                createChannelsTable.ExecuteNonQuery();
                createEpisodesTable.ExecuteNonQuery();

                db.Close();
            }
        }

        public static void AddChannel(Channel channel)
        {
            // Insert command
            using (SqliteConnection db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                SqliteCommand insertChannel = new SqliteCommand(SQL_INSERT_CHANNEL_TABLE, db);
                // Add parameters
                insertChannel.Parameters.Add(new SqliteParameter("@title", channel.Title));
                insertChannel.Parameters.Add(new SqliteParameter("@description", channel.Description));
                insertChannel.Parameters.Add(new SqliteParameter("@link", channel.Link));
                insertChannel.Parameters.Add(new SqliteParameter("@language", channel.Language));
                insertChannel.Parameters.Add(new SqliteParameter("@copyright", channel.Copyright));
                insertChannel.Parameters.Add(new SqliteParameter("@lastbuilddate", channel.LastBuildDate));
                insertChannel.Parameters.Add(new SqliteParameter("@pubdate", channel.PubDate));
                insertChannel.Parameters.Add(new SqliteParameter("@docs", channel.Docs));
                insertChannel.Parameters.Add(new SqliteParameter("@webmaster", channel.Webmaster));

                insertChannel.ExecuteNonQuery();

                foreach (Episode e in channel.EpisodeList)
                {
                    AddEpisode(e, db);
                }

                db.Close();
            }
        }

        public static void AddEpisode(Episode episode, SqliteConnection db)
        {
            // Insert command
            SqliteCommand insertEpisode = new SqliteCommand(SQL_INSERT_EPISODE_TABLE, db);
            // Add parameters
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
            insertEpisode.Parameters.Add(new SqliteParameter("@keywords", episode.Keywords));

            insertEpisode.ExecuteNonQuery();
        }

        public static void DeleteChannel(int id)
        {
            // Delete commands
            using (SqliteConnection db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                SqliteCommand deleteChannel = new SqliteCommand(SQL_DELETE_CHANNEL, db);
                SqliteCommand deleteEpisode = new SqliteCommand(SQL_DELETE_EPISODE, db);
                // Add parameters
                deleteChannel.Parameters.Add(new SqliteParameter("@id", id));
                deleteEpisode.Parameters.Add(new SqliteParameter("@id", id));

                deleteChannel.ExecuteNonQuery();
                deleteEpisode.ExecuteNonQuery();

                db.Close();
            }
        }

        public static ObservableCollection<Channel> GetChannels()
        {
            ObservableCollection<Channel> channelList = new ObservableCollection<Channel>();

            using (SqliteConnection db = new SqliteConnection(CONNECTION_STRING))
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
                            episode.Keywords = episodesReader["keywords"].ToString();

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
