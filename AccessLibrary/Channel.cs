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
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }  //detailed description of podcast
        public String Link { get; set; } //link to main podcast website
        public String Language { get; set; } //podcast language e.g en-us
        public String Copyright { get; set; } //copyright information
        public DateTime LastBuildDate { get; set; } //date when podcast RSS was generated, see here for formatting
        public DateTime PubDate { get; set; } //date when podcast RSS was published, see here for formatting
        public String Docs { get; set; } //URL that points to documentation for RSS
        public String Webmaster { get; set; } //email for technical questions
        public String ItunesAuthor { get; set; } //author information
        public String ItunesSubtitle { get; set; } //description of podcast show
        public String ItunesSummary { get; set; } //description of podcast show
        public String ItunesOwnerName { get; set; } //owner information
        public String ItunesOwnerEmail { get; set; } //owner email address
        public String ItunesExplicit { get; set; } //Whether podcast episode has explicit content e.g Yes/No
        //private itunes_image { get; set; } //podcast image TODO store image
        public String ItunesCategory { get; set; } //podcast category
        public List<Episode> EpisodeList { get; set; } //list of episodes for this channel

        public Channel()
        {
            EpisodeList = new List<Episode>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
