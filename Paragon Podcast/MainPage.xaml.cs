using AccessLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Paragon_Podcast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Channel> channelList = new ObservableCollection<Channel>();
        public MainPage()
        {
            this.InitializeComponent();
            LoadAudio("https://www.computerhope.com/jargon/m/example.mp3");
#if DEBUG
            channelList.Add(XmlHandler.LoadSampleData());
#endif
            try
            {
                //ui test data
                Channel ch1 = new Channel();
                Channel ch2 = new Channel();
                Episode e1 = new Episode();
                Episode e2 = new Episode();

                ch1.Title = "Channel 1";
                ch2.Title = "Channel 2";
                ch1.EpisodeList = new List<Episode>();
                ch2.EpisodeList = new List<Episode>();
                e1.Title  = "Episode 1";
                e2.Title  = "Episode 2";
                e1.EnclosureUrl = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3";
                e2.EnclosureUrl = "https://www.computerhope.com/jargon/m/example.mp3";

                ch1.EpisodeList.Add(e1);
                ch1.EpisodeList.Add(e2);
                ch2.EpisodeList.Add(e1);
                ch2.EpisodeList.Add(e2);

                channelList.Add(ch1);
                channelList.Add(ch2);

                LVchannelList.ItemsSource = channelList;
                // LVepisodeList.ItemsSource = channelList;//place holder

            }
            catch (Exception e)
            {
                //todo add exception handleing
            }
        }

        private void LoadAudio(string path)
        {
            try
            {
                
                //creates new uri from episode path
                Uri pathUri = new Uri(path);
                //sets players mediasource to the specified path
                player.Source = MediaSource.CreateFromUri(pathUri);
                
            }
            catch (Exception ex)
            {
                //TODO catch exception here
            }
        }

        

        private void LVchannelList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //when channel is clicked from channel list view load the episode list for the channel to the episode list view
            Channel temp = (Channel)e.ClickedItem;
            LVepisodeList.ItemsSource = temp.EpisodeList;

        }

        private void LVepisodeList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //when an episode is clicked in the episode list view load the audio from the episodes enclosure Url
            Episode temp = (Episode)e.ClickedItem;
            LoadAudio(temp.EnclosureUrl);
        }

        //buttons that change the media player playback speed
        private void PlaybackRate1_Click(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.PlaybackSession.PlaybackRate = 1;
        }

        private void PlaybackRate15_Click(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.PlaybackSession.PlaybackRate = 1.5;
        }

        private void PlaybackRate2_Click(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.PlaybackSession.PlaybackRate = 2;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string userInput = newChannelText.Text;
            newChannelText.Text = "";

            try
            {
                channelList.Add(XmlHandler.GetChannel(userInput));
            }
            catch
            {
                //error handleing code
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            addFlyout.Hide();
        }
    }
}
