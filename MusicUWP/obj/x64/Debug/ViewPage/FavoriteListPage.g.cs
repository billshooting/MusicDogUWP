﻿#pragma checksum "C:\Users\BILL\Desktop\MusicUWP\MusicUWP\ViewPage\FavoriteListPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "18E77F8C332A64E4179D2B4B80DDD0C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicUWP.ViewPage
{
    partial class FavoriteListPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Foreground(global::Windows.UI.Xaml.Controls.TextBlock obj, global::Windows.UI.Xaml.Media.Brush value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), targetNullValue);
                }
                obj.Foreground = value;
            }
            public static void Set_Windows_UI_Xaml_UIElement_Visibility(global::Windows.UI.Xaml.UIElement obj, global::Windows.UI.Xaml.Visibility value)
            {
                obj.Visibility = value;
            }
        };

        private class FavoriteListPage_obj4_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IFavoriteListPage_Bindings
        {
            private global::MusicUWP.ViewModels.Song dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private global::Windows.UI.Xaml.ResourceDictionary localResources;
            private global::System.WeakReference<global::Windows.UI.Xaml.FrameworkElement> converterLookupRoot;
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj5;
            private global::Windows.UI.Xaml.Controls.TextBlock obj6;
            private global::Windows.UI.Xaml.Controls.TextBlock obj7;
            private global::Windows.UI.Xaml.Controls.TextBlock obj8;
            private global::Windows.UI.Xaml.Controls.TextBlock obj9;
            private global::Windows.UI.Xaml.Controls.TextBlock obj10;

            private FavoriteListPage_obj4_BindingsTracking bindingsTracking;

            public FavoriteListPage_obj4_Bindings()
            {
                this.bindingsTracking = new FavoriteListPage_obj4_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 5:
                        this.obj5 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 6:
                        this.obj6 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 7:
                        this.obj7 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 8:
                        this.obj8 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 9:
                        this.obj9 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 10:
                        this.obj10 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::MusicUWP.ViewModels.Song data = args.NewValue as global::MusicUWP.ViewModels.Song;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::MusicUWP.ViewModels.Song was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = 1;
                        this.SetDataRoot(args.Item as global::MusicUWP.ViewModels.Song);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.Grid)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                    case 1:
                        Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj7);
                        nextPhase = 2;
                        break;
                    case 2:
                        Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj8);
                        Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj9);
                        nextPhase = -1;
                        break;
                }
                this.Update_((global::MusicUWP.ViewModels.Song) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                this.bindingsTracking.ReleaseAllListeners();
                Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj7);
                Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj8);
                Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj9);
            }

            // IFavoriteListPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // FavoriteListPage_obj4_Bindings

            public void SetDataRoot(global::MusicUWP.ViewModels.Song newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }
            public void SetConverterLookupRoot(global::Windows.UI.Xaml.FrameworkElement rootElement)
            {
                this.converterLookupRoot = new global::System.WeakReference<global::Windows.UI.Xaml.FrameworkElement>(rootElement);
            }

            public global::Windows.UI.Xaml.Data.IValueConverter LookupConverter(string key)
            {
                if (this.localResources == null)
                {
                    global::Windows.UI.Xaml.FrameworkElement rootElement;
                    this.converterLookupRoot.TryGetTarget(out rootElement);
                    this.localResources = rootElement.Resources;
                    this.converterLookupRoot = null;
                }
                return (global::Windows.UI.Xaml.Data.IValueConverter) (this.localResources.ContainsKey(key) ? this.localResources[key] : global::Windows.UI.Xaml.Application.Current.Resources[key]);
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::MusicUWP.ViewModels.Song obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_IsFavorite(obj.IsFavorite, phase);
                        this.Update_IsPlaying(obj.IsPlaying, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Title(obj.Title, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0) | (1 << 1))) != 0)
                    {
                        this.Update_Artist(obj.Artist, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0) | (1 << 2))) != 0)
                    {
                        this.Update_Album(obj.Album, phase);
                        this.Update_Duration(obj.Duration, phase);
                    }
                }
            }
            private void Update_IsFavorite(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj5, (global::System.String)this.LookupConverter("FavBtnIconConverter").Convert(obj, typeof(global::System.String), null, null), null);
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Foreground(this.obj5, (global::Windows.UI.Xaml.Media.Brush)this.LookupConverter("FavBtnForegroundConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Media.Brush), null, null), null);
                }
            }
            private void Update_IsPlaying(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_Visibility(this.obj5, (global::Windows.UI.Xaml.Visibility)this.LookupConverter("IdConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Visibility), null, null));
                    XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_Visibility(this.obj6, (global::Windows.UI.Xaml.Visibility)this.LookupConverter("IsPlayingConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Visibility), null, null));
                }
            }
            private void Update_Artist(global::System.String obj, int phase)
            {
                if((phase & ((1 << 1) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj7, obj, null);
                }
            }
            private void Update_Album(global::System.String obj, int phase)
            {
                if((phase & ((1 << 2) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj8, obj, null);
                }
            }
            private void Update_Duration(global::System.TimeSpan obj, int phase)
            {
                if((phase & ((1 << 2) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj9, (global::System.String)this.LookupConverter("TimeSpanConverter").Convert(obj, typeof(global::System.String), null, null), null);
                }
            }
            private void Update_Title(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj10, obj, null);
                }
            }

            private class FavoriteListPage_obj4_BindingsTracking
            {
                global::System.WeakReference<FavoriteListPage_obj4_Bindings> WeakRefToBindingObj; 

                public FavoriteListPage_obj4_BindingsTracking(FavoriteListPage_obj4_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<FavoriteListPage_obj4_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    FavoriteListPage_obj4_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::MusicUWP.ViewModels.Song obj = sender as global::MusicUWP.ViewModels.Song;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_IsFavorite(obj.IsFavorite, DATA_CHANGED);
                                    bindings.Update_IsPlaying(obj.IsPlaying, DATA_CHANGED);
                                    bindings.Update_Artist(obj.Artist, DATA_CHANGED);
                                    bindings.Update_Album(obj.Album, DATA_CHANGED);
                                    bindings.Update_Duration(obj.Duration, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "IsFavorite":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_IsFavorite(obj.IsFavorite, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IsPlaying":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_IsPlaying(obj.IsPlaying, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Artist":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_Artist(obj.Artist, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Album":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_Album(obj.Album, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Duration":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_Duration(obj.Duration, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::MusicUWP.ViewModels.Song obj)
                {
                    FavoriteListPage_obj4_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        if (bindings.dataRoot != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
                        }
                    }
                }
            }
        }

        private class FavoriteListPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IFavoriteListPage_Bindings
        {
            private global::MusicUWP.ViewPage.FavoriteListPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj2;

            public FavoriteListPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2:
                        this.obj2 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IFavoriteListPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // FavoriteListPage_obj1_Bindings

            public void SetDataRoot(global::MusicUWP.ViewPage.FavoriteListPage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::MusicUWP.ViewPage.FavoriteListPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_FavoriteSongs(obj.FavoriteSongs, phase);
                    }
                }
            }
            private void Update_FavoriteSongs(global::System.Collections.ObjectModel.ObservableCollection<global::MusicUWP.ViewModels.Song> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj2, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 9 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.FavMusicListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 42 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.FavMusicListView).DoubleTapped += this.FavMusicListView_DoubleTapped;
                    #line 43 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.FavMusicListView).Tapped += this.FavMusicListView_Tapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.FavMusicLoadingRing = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Grid element4 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 50 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element4).DoubleTapped += this.Grid_DoubleTapped;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element7 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 111 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element7).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element8 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 116 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element8).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 9:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element9 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 121 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element9).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element10 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 87 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element10).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 11:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element11 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 98 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element11).Click += this.PlayMenu_Click;
                    #line default
                }
                break;
            case 12:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element12 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 99 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element12).Click += this.UnFavMenu_Click;
                    #line default
                }
                break;
            case 13:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element13 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 100 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element13).Click += this.AddPlayListMenu_Click;
                    #line default
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element14 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 102 "..\..\..\ViewPage\FavoriteListPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element14).Click += this.Download_Click;
                    #line default
                }
                break;
            case 15:
                {
                    this.TiteText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16:
                {
                    this.SongsCountText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    FavoriteListPage_obj1_Bindings bindings = new FavoriteListPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Grid element4 = (global::Windows.UI.Xaml.Controls.Grid)target;
                    FavoriteListPage_obj4_Bindings bindings = new FavoriteListPage_obj4_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::MusicUWP.ViewModels.Song) element4.DataContext);
                    bindings.SetConverterLookupRoot(this);
                    element4.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element4, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}
