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

        public static Channel LoadSampleData()
        {
            XmlDocument sampleXmlDoc = new XmlDocument();
            // TODO set to sample.xml
            sampleXmlDoc.Load("http://where");
            return GetChannel(sampleXmlDoc);
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
            string channelLastBuildDate = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.LastBuildDate = DateTime.Parse(channelLastBuildDate);

            channelSubNode = channelNode.SelectSingleNode("pubDate");
            string channelPubDate = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.PubDate = DateTime.Parse(channelPubDate);

            channelSubNode = channelNode.SelectSingleNode("docs");
            string channelDocs = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Docs = channelDocs;

            channelSubNode = channelNode.SelectSingleNode("webMaster");
            string channelWebMaster = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.Webmaster = channelWebMaster;

            /* TODO fix, iTunes tags, uses namespaces
            channelSubNode = channelNode.SelectSingleNode("");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesAuthor = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesSubtitle");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesSubtitle = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesSummary");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesSummary = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesOwner_name");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesOwner_name = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesOwner_email");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesOwner_email = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesExplicit");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesExplicit = channelTitle;

            channelSubNode = channelNode.SelectSingleNode("itunesCategory");
            string channelTitle = channelSubNode != null ? channelSubNode.InnerText : "";
            channel.itunesCategory = channelTitle;
            */

            //ITEMS
            // Parse the items in the RSS file
            XmlNodeList itemNodes = xmlDoc.SelectNodes("rss/channel/item");

            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in itemNodes)
            {
                Episode episode = new Episode();
                // TODO add id and channel id

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

                /* TODO fix, these are attributes
                 rssSubNode = rssNode.SelectSingleNode("enclosureUrl");
                string enclosureUrl = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.enclosureUrl = enclosureUrl;

                rssSubNode = rssNode.SelectSingleNode("enclosureLength");
                string enclosureLength = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.enclosureLength = enclosureLength;

                rssSubNode = rssNode.SelectSingleNode("enclosureType");
                string enclosureType = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.enclosureType = enclosureType;
                */

                rssSubNode = rssNode.SelectSingleNode("category");
                string category = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Category = category;

                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.PubDate = DateTime.Parse(pubDate);

                /* TODO fix, these use a namespace
                rssSubNode = rssNode.SelectSingleNode("itunesAuthor");
                string itunesAuthor = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.itunesAuthor = itunesAuthor;

                rssSubNode = rssNode.SelectSingleNode("itunesExplicit");
                string itunesExplicit = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.itunesExplicit = itunesExplicit;

                rssSubNode = rssNode.SelectSingleNode("itunesSubtitle");
                string itunesSubtitle = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.itunesSubtitle = itunesSubtitle;

                rssSubNode = rssNode.SelectSingleNode("itunesSummary");
                string itunesSummary = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.itunesSummary = itunesSummary;

                rssSubNode = rssNode.SelectSingleNode("itunesDuration");
                string itunesDuration = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.itunesDuration = itunesDuration;
                */

                rssSubNode = rssNode.SelectSingleNode("keywords");
                string keywords = rssSubNode != null ? rssSubNode.InnerText : "";
                episode.Keywords = keywords;

                channel.EpisodeList.Add(episode);
            }
            return channel;
        }
    }
}
