using MusicUWP.Models;
using MusicUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchSongPage : Page
    {
        public ObservableCollection<WebSong> QueryList { get; set; } = new ObservableCollection<WebSong>();

        private SongResponseByName WebReqResult;
        private string queryWord;
        private MainPage mainPage;

        public SearchSongPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var mainpage = (MainPage)e.Parameter;
            mainPage = mainpage;
            base.OnNavigatedTo(e);
        }

        private async void AutoSugBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            HttpRequestRing.IsActive = true;

            QueryList.Clear();
            queryWord = sender.Text;

            #region tryRegion
            try
            {
                await Task.Run(()=>
                {
                    var task = WebSongProxy.GetSongByNameAsync(queryWord);
                    WebReqResult = task.GetAwaiter().GetResult();
                    if(WebReqResult.showapi_res_code == -1)
                    {
                        throw new HttpRequestException(WebReqResult.showapi_res_error);
                    }
                });
                SongFileManager.SetWebSongByNameList(QueryList, WebReqResult.showapi_res_body.pagebean.contentlist);
            }
            catch (HttpRequestException ex)
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "网络不给力啊" + "\n" + ex.Message;
                ErrorComfirmBtn.Visibility = Visibility.Visible;
                ErrorPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "出现错误" + "\n" + ex.Message;
                ErrorComfirmBtn.Visibility = Visibility.Visible;
                ErrorPanel.Visibility = Visibility.Visible;
            }
            finally
            {
                HttpRequestRing.IsActive = false;
            }
            #endregion
        }

        private void ErrorComfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ErrorComfirmBtn.Visibility = Visibility.Collapsed;
            ErrorPanel.Visibility = Visibility.Collapsed;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (WebSong)e.ClickedItem;

            mainPage.OpenWebSong(song);
        }
    }
}
