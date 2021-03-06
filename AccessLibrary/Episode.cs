﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AccessLibrary
{
    public class Episode : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public String Title { get; set; }           //title information
        public String Link { get; set; }            //URL to main podcast website
        public String Guid { get; set; }            //optional tag for audio file location
        public String Description { get; set; }     //detailed description of podcast show
        public String EnclosureUrl { get; set; }    //audio file location
        public int EnclosureLength { get; set; }    //audio file length in bytes
        public String EnclosureType { get; set; }   //audio file type e.g audio/mpeg
        public String Category { get; set; }        //podcast category
        public DateTime PubDate { get; set; }       //date when podcast RSS was published, see here for formatting
        public String Keywords { get; set; }        //keywords associated with podcast content

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
