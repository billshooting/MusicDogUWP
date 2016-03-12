using MusicUWP.Models;
using MusicUWP.ViewModels;
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
using System.Net;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BandListPage : Page
    {
        public ObservableCollection<WebSong> WebSongsList { get; set; } = new ObservableCollection<WebSong>();

        private SongResponseBandList WebReqResult;
        private MainPage mainPage;
        private BandListCover bandMessage;

        public BandListPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HttpRequestRing.IsActive = true;
            try
            {
                await Task.Run(() =>
                 {
                     var task =  WebSongProxy.GetBandListAsync(bandMessage.Id);
                     WebReqResult = task.GetAwaiter().GetResult();
                    // http请求返回错误
                    if (WebReqResult.showapi_res_code == -1)
                     {
                         throw new HttpRequestException(WebReqResult.showapi_res_error);
                     }
                 });
                SongFileManager.SetWebSongsByBandList(WebSongsList, WebReqResult.showapi_res_body.pagebean.songlist);
            }
            catch (HttpRequestException ex)
            {
                TitleText.Text = ex.Message;
            }
            catch (Exception ex)
            {
                TitleText.Text = ex.Message;
            }
            finally
            {
                HttpRequestRing.IsActive = false;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PageParameters para = (PageParameters)e.Parameter;

            mainPage = para.MainPage;
            bandMessage = para.BandList;

            base.OnNavigatedTo(e);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            WebSong song = (WebSong)e.ClickedItem;

            mainPage.OpenWebSong(song);
        }

        private void PlayAllBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
