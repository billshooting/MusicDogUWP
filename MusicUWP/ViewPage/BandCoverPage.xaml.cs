using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MusicUWP.Models;
using MusicUWP.ViewModels;
using Windows.UI.Xaml.Media.Imaging;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BandCoverPage : Page
    {
        public SongResponseBandList TopList { get; set; }

        private List<BandListCover> bandListCover;
        private MainPage mainPage;
        public BandCoverPage()
        {
            this.InitializeComponent();
            bandListCover = CoverInitialize();
        }


        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            BandListCover band = (BandListCover)e.ClickedItem;
            mainPage.ContentFrame.Navigate(typeof(BandListPage), new PageParameters() { MainPage = mainPage, BandList = band });
        }
        
        private List<BandListCover>CoverInitialize()
        {
            List<BandListCover> list = new List<BandListCover>();
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/欧美.jpg", Text = "欧美榜单", Id = 3 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/内地.jpg", Text = "内地榜单", Id = 5 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/港台.jpg", Text = "港台榜单", Id = 6 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/热歌.jpg", Text = "热歌榜单", Id = 26 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/销量.jpg", Text = "销量榜单", Id = 23 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/陈粒.jpg", Text = "民谣榜单", Id = 18 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/摇滚.jpg", Text = "摇滚榜单", Id = 19 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/韩国.jpg", Text = "韩国榜单", Id = 16 });
            list.Add(new BandListCover { ImageUri = "/Assets/BandListCover/日本.jpg", Text = "日本榜单", Id = 17 });

            return list;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var mainpage = (MainPage)e.Parameter;
            mainPage = mainpage;
            base.OnNavigatedTo(e);
        }
    }
}
