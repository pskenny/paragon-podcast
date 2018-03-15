using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AccessLibrary
{
    public class Channel : INotifyPropertyChanged
    {
        public int id { get; set; }
        public String title { get; set; }
        public String description { get; set; }  //detailed description of podcast
        public String link { get; set; } //link to main podcast website
        public String language { get; set; } //podcast language e.g en-us
        public String copyright { get; set; } //copyright information
        public DateTime lastBuildDate { get; set; } //date when podcast RSS was generated, see here for formatting
        public DateTime pubDate { get; set; } //date when podcast RSS was published, see here for formatting
        public String docs { get; set; } //URL that points to documentation for RSS
        public String webMaster { get; set; } //email for technical questions
        public String itunesAuthor { get; set; } //author information
        public String itunesSubtitle { get; set; } //description of podcast show
        public String itunesSummary { get; set; } //description of podcast show
        public String itunesOwner_name { get; set; } //owner information
        public String itunesOwner_email { get; set; } //owner email address
        public String itunesExplicit { get; set; } //Whether podcast episode has explicit content e.g Yes/No
        //private itunes_image { get; set; } //podcast image TODO store image
        public String itunesCategory { get; set; } //podcast category
        public List<Episode> EpisodeList { get; set; } //list of episodes for this channel

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
