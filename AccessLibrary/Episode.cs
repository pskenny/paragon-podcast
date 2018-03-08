using System;
using System.Collections.Generic;
using System.Text;

namespace AccessLibrary
{
    class Episode
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Description { get; set; }
        public string EnclosureUrl { get; set; }
        public int EnclosureLength { get; set; }
        public string EnclosureType { get; set; }
        public string Category { get; set; }
        public string PublishDate { get; set; }
        public string ItunesAuthor { get; set; }
        public string ItunesExplicit { get; set; }
        public string ItuneSubtitles { get; set; }
        public string ItunesSummary { get; set; }
        public string ItunesDuration { get; set; }
        public string Keywords { get; set; }
    }
}
