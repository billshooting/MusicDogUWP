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
using Windows.Storage;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BandListPage : Page
    {
        public ObservableCollection<Song> WebSongsList { get; set; } = new ObservableCollection<Song>();

        private SongResponseBandList WebReqResult;
        private MainPage mainPage;
        private BandListCover bandMessage;
        private int _listSelectedIndex = -1;

        public BandListPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HttpRequestRing.IsActive = true;
            try
            {
                await Task.Run(() =>
                 {
                     var task = WebSongProxy.GetBandListAsync(bandMessage.Id);
                     WebReqResult = task.GetAwaiter().GetResult();
                     // http请求返回错误
                     if (WebReqResult.showapi_res_code == -1)
                     {
                         throw new HttpRequestException(WebReqResult.showapi_res_error);
                     }
                 });
                SongFileManager.SetWebSongsByBandList(WebSongsList, WebReqResult.showapi_res_body.pagebean.songlist, mainPage.FavoriteSongsList.Where(s => s.IsLoaclSong == false).ToList());
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
                this.NavigationCacheMode = NavigationCacheMode.Required;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            PageParameters para = (PageParameters)e.Parameter;

            mainPage = para.MainPage;
            bandMessage = para.BandList;

            base.OnNavigatedTo(e);
        }

        private void PlayAllBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listview = (ListView)sender;
            Button button;
            //取消上一次点击的效果
            if (_listSelectedIndex >= 0)
            {
                button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[_listSelectedIndex]).ContentTemplateRoot).Children[3])).Children[1];//获取点击的条目的隐藏按钮
                button.Visibility = Visibility.Collapsed;
                button.IsEnabled = false;
            }
            base.OnTapped(e);
            e.Handled = true;
            button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[listview.SelectedIndex]).ContentTemplateRoot).Children[3])).Children[1];//获取点击的条目的隐藏按钮
            if (button.Visibility == Visibility.Collapsed)
            {
                button.Visibility = Visibility.Visible;
                button.IsEnabled = true;
            }
            else
            {
                button.Visibility = Visibility.Collapsed;
                button.IsEnabled = false;
            }
            _listSelectedIndex = listview.SelectedIndex;
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((ListViewItemPresenter)e.OriginalSource).Content;
            e.Handled = true;

            mainPage.OpenWebSong(song);
        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((Grid)sender).DataContext;
            e.Handled = true;
            mainPage.OpenWebSong(song);
        }

        private void IsFavBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Song song = (Song)((TextBlock)sender).DataContext;
            e.Handled = true;
            if (song.IsFavorite)
                mainPage.UnFavorite(song);
            else
                mainPage.Favorite(song);
        }

        private void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((TextBlock)sender).DataContext;
            e.Handled = true;
            mainPage.OpenWebSong(song);
        }

        private void PlayMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.OpenWebSong(song);
        }

        private void FavMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.Favorite(song);
        }

        private void AddPlayListMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.AddToPlayingList(song);
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            string url = song.DownUrl;
            string title = song.Title;
            await mainPage.HandleDownload(title, url);
        }
    }
}
