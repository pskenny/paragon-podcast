using AccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Paragon_Podcast
{
    class XmlHandler
    {
        public static Channel GetChannel(string url)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            return GetChannel(xmlDoc);
        }

        public static Channel GetChannel(XmlDocument xmlDoc)
        {
            Channel channel = new Channel();
            XNamespace ns = "http://www.itunes.com/dtds/podcast-1.0.dtd";

            XmlNode channelNode = xmlDoc.SelectSingleNode("rss/channel");
            XmlNode channelSubNode = channelNode.SelectSingleNode("title");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Title = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("description");
            string channelDescription = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Description = channelDescription;

            channelSubNode = channelNode.SelectSingleNode("language");
            string channelLanguage = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Language = channelLanguage;

            channelSubNode = channelNode.SelectSingleNode("copyright");
            string channelCopyright = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Copyright = channelCopyright;

            channelSubNode = channelNode.SelectSingleNode("lastBuildDate");
            channel.LastBuildDate = channelSubNode != null ? DateTime.Parse(channelSubNode.InnerText) : new DateTime();

            channelSubNode = channelNode.SelectSingleNode("pubDate");
            channel.PubDate = channelSubNode != null ? DateTime.Parse(channelSubNode.InnerText) : new DateTime();

            channelSubNode = channelNode.SelectSingleNode("docs");
            string channelDocs = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Docs = channelDocs;

            channelSubNode = channelNode.SelectSingleNode("webMaster");
            string channelWebMaster = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Webmaster = channelWebMaster;

            //ITEMS
            // Parse the items in the RSS file
            XmlNodeList itemNodes = xmlDoc.SelectNodes("rss/channel/item");

            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in itemNodes)
            {
                Episode episode = new Episode();

                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Title = title;

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Link = link;

                rssSubNode = rssNode.SelectSingleNode("guid");
                string guid = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Guid = guid;

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Description = description;

                rssSubNode = rssNode.SelectSingleNode("enclosure/@url");
                string enclosureUrl = rssSubNode.Value != null ? rssSubNode.Value : "";
                episode.EnclosureUrl = enclosureUrl;

                rssSubNode = rssNode.SelectSingleNode("enclosure/@type");
                string enclosureType = rssSubNode.Value != null ? rssSubNode.Value : "";
                episode.EnclosureType = enclosureType;

                rssSubNode = rssNode.SelectSingleNode("enclosure/@length");
                string enclosureLength = rssSubNode.Value != null ? rssSubNode.Value : "0";
                episode.EnclosureLength = Int32.Parse(enclosureLength);

                rssSubNode = rssNode.SelectSingleNode("category");
                string category = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Category = category;

                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.PubDate = DateTime.Parse(pubDate);

                rssSubNode = rssNode.SelectSingleNode("keywords");
                string keywords = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Keywords = keywords;

                channel.EpisodeList.Add(episode);
            }
            return channel;
        }
    }
}
