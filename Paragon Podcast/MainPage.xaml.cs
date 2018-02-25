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
            LoadAudio();

            try
            {

                

                //test data
                Channel ch1 = new Channel();
                Channel ch2 = new Channel();
                Channel ch3 = new Channel();
                Channel ch4 = new Channel();
                Channel ch5 = new Channel();
                Channel ch6 = new Channel();
                Channel ch7 = new Channel();
                Channel ch8 = new Channel();
                Channel ch9 = new Channel();

                ch1.title = "title1";
                ch2.title = "title2";
                ch3.title = "title3";
                ch4.title = "title4";
                ch5.title = "title5";
                ch6.title = "title6";
                ch7.title = "title7";
                ch8.title = "title8";
                ch9.title = "title9";

                channelList.Add(ch1);
                channelList.Add(ch2);
                channelList.Add(ch3);
                channelList.Add(ch4);
                channelList.Add(ch5);
                channelList.Add(ch6);
                channelList.Add(ch7);
                channelList.Add(ch8);
                channelList.Add(ch9);

                LVchannelList.ItemsSource = channelList;
               // LVepisodeList.ItemsSource = channelList;//place holder

            }
            catch (Exception e)
            {
                //todo add exception handleing
            }
        }

        private void LoadAudio()
        {
            try
            {
                Uri pathUri = new Uri("ms-appx:///Audio/songOfStorms.mp3");
                player.Source = MediaSource.CreateFromUri(pathUri);
            }
            catch (Exception ex)
            {
                //TODO catch exception here
            }
        }

        private void LVchannelList_ItemClick(object sender, ItemClickEventArgs e)
        {
            LVepisodeList.ItemsSource = channelList;
        }
    }
}
