using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Paragon_Podcast
{
    public class Episode : INotifyPropertyChanged
    {

        public String title { get; set; }           //title information
        public String link { get; set; }            //URL to main podcast website
        public String guid { get; set; }            //optional tag for audio file location
        public String description { get; set; }     //detailed description of podcast show
        public String enclosureUrl { get; set; }    //audio file location
        public int enclosureLength { get; set; }    //audio file length in bytes
        public String enclosureType { get; set; }   //audio file type e.g audio/mpeg
        public String category { get; set; }        //podcast category
        public DateTime pubDate { get; set; }       //date when podcast RSS was published, see here for formatting
        public String itunesAuthor { get; set; }    //Author information
        public String itunesExplicit { get; set; }  //Whether podcast episode has explicit content e.g Yes/No
        public String itunesSubtitle { get; set; }  //description of podcast show
        public String itunesSummary { get; set; }   //description of podcast show
        public String itunesDuration { get; set; }  //length of podcast episode in HH:MM:SS formatting
        public String keywords { get; set; }        //keywords associated with podcast content

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
