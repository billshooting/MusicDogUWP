﻿#pragma checksum "C:\Users\BILL\Documents\Visual Studio 2015\MusicUWP\MusicUWP\ViewPage\SearchSongPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5BD1052749C2AA37C47D8E964A96BDC0"
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
    partial class SearchSongPage : 
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
            public static void Set_Windows_UI_Xaml_UIElement_Visibility(global::Windows.UI.Xaml.UIElement obj, global::Windows.UI.Xaml.Visibility value)
            {
                obj.Visibility = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Foreground(global::Windows.UI.Xaml.Controls.TextBlock obj, global::Windows.UI.Xaml.Media.Brush value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), targetNullValue);
                }
                obj.Foreground = value;
            }
        };

        private class SearchSongPage_obj7_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ISearchSongPage_Bindings
        {
            private global::MusicUWP.ViewModels.WebSong dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private global::Windows.UI.Xaml.ResourceDictionary localResources;
            private global::System.WeakReference<global::Windows.UI.Xaml.FrameworkElement> converterLookupRoot;
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj8;
            private global::Windows.UI.Xaml.Controls.TextBlock obj9;
            private global::Windows.UI.Xaml.Controls.TextBlock obj10;
            private global::Windows.UI.Xaml.Controls.TextBlock obj11;
            private global::Windows.UI.Xaml.Controls.TextBlock obj12;
            private global::Windows.UI.Xaml.Controls.TextBlock obj13;

            private SearchSongPage_obj7_BindingsTracking bindingsTracking;

            public SearchSongPage_obj7_Bindings()
            {
                this.bindingsTracking = new SearchSongPage_obj7_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 8:
                        this.obj8 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 9:
                        this.obj9 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 10:
                        this.obj10 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 11:
                        this.obj11 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 12:
                        this.obj12 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 13:
                        this.obj13 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::MusicUWP.ViewModels.WebSong data = args.NewValue as global::MusicUWP.ViewModels.WebSong;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::MusicUWP.ViewModels.WebSong was expected.");
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
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::MusicUWP.ViewModels.WebSong);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.Grid)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::MusicUWP.ViewModels.WebSong) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                this.bindingsTracking.ReleaseAllListeners();
            }

            // ISearchSongPage_Bindings

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

            // SearchSongPage_obj7_Bindings

            public void SetDataRoot(global::MusicUWP.ViewModels.WebSong newDataRoot)
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
            private void Update_(global::MusicUWP.ViewModels.WebSong obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Id(obj.Id, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_IsPlaying(obj.IsPlaying, phase);
                        this.Update_IsFavorite(obj.IsFavorite, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Artist(obj.Artist, phase);
                        this.Update_Album(obj.Album, phase);
                        this.Update_Title(obj.Title, phase);
                    }
                }
            }
            private void Update_Id(global::System.Int32 obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj8, obj.ToString(), null);
                }
            }
            private void Update_IsPlaying(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_Visibility(this.obj8, (global::Windows.UI.Xaml.Visibility)this.LookupConverter("IdConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Visibility), null, null));
                    XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_Visibility(this.obj9, (global::Windows.UI.Xaml.Visibility)this.LookupConverter("IsPlayingConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Visibility), null, null));
                }
            }
            private void Update_IsFavorite(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj10, (global::System.String)this.LookupConverter("FavBtnIconConverter").Convert(obj, typeof(global::System.String), null, null), null);
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Foreground(this.obj10, (global::Windows.UI.Xaml.Media.Brush)this.LookupConverter("FavBtnForegroundConverter").Convert(obj, typeof(global::Windows.UI.Xaml.Media.Brush), null, null), null);
                }
            }
            private void Update_Artist(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj11, obj, null);
                }
            }
            private void Update_Album(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj12, obj, null);
                }
            }
            private void Update_Title(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj13, obj, null);
                }
            }

            private class SearchSongPage_obj7_BindingsTracking
            {
                global::System.WeakReference<SearchSongPage_obj7_Bindings> WeakRefToBindingObj; 

                public SearchSongPage_obj7_BindingsTracking(SearchSongPage_obj7_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<SearchSongPage_obj7_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    SearchSongPage_obj7_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::MusicUWP.ViewModels.WebSong obj = sender as global::MusicUWP.ViewModels.WebSong;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_IsPlaying(obj.IsPlaying, DATA_CHANGED);
                                    bindings.Update_IsFavorite(obj.IsFavorite, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "IsPlaying":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_IsPlaying(obj.IsPlaying, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IsFavorite":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_IsFavorite(obj.IsFavorite, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::MusicUWP.ViewModels.WebSong obj)
                {
                    SearchSongPage_obj7_Bindings bindings;
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

        private class SearchSongPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ISearchSongPage_Bindings
        {
            private global::MusicUWP.ViewPage.SearchSongPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj2;

            public SearchSongPage_obj1_Bindings()
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

            // ISearchSongPage_Bindings

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

            // SearchSongPage_obj1_Bindings

            public void SetDataRoot(global::MusicUWP.ViewPage.SearchSongPage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::MusicUWP.ViewPage.SearchSongPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_QueryList(obj.QueryList, phase);
                    }
                }
            }
            private void Update_QueryList(global::System.Collections.ObjectModel.ObservableCollection<global::MusicUWP.ViewModels.WebSong> obj, int phase)
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
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.ListView element2 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 31 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)element2).Tapped += this.ListView_Tapped;
                    #line 32 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)element2).DoubleTapped += this.ListView_DoubleTapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.HttpRequestRing = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 4:
                {
                    this.ErrorPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 5:
                {
                    this.ErrorTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.ErrorComfirmBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 129 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ErrorComfirmBtn).Click += this.ErrorComfirmBtn_Click;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.Grid element7 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 38 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element7).DoubleTapped += this.Grid_DoubleTapped;
                    #line default
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element10 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 71 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element10).PointerPressed += this.IsFavBtn_PointerPressed;
                    #line default
                }
                break;
            case 11:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element11 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 104 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element11).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 12:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element12 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 108 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element12).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 13:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element13 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 81 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element13).DoubleTapped += this.TextBlock_DoubleTapped;
                    #line default
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element14 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 92 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element14).Click += this.PlayMenu_Click;
                    #line default
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element15 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 93 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element15).Click += this.FavMenu_Click;
                    #line default
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element16 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 94 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element16).Click += this.AddPlayListMenu_Click;
                    #line default
                }
                break;
            case 17:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element17 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 96 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element17).Click += this.Download_Click;
                    #line default
                }
                break;
            case 18:
                {
                    this.AutoSugBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 26 "..\..\..\ViewPage\SearchSongPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.AutoSugBox).QuerySubmitted += this.AutoSugBox_QuerySubmitted;
                    #line default
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
                    SearchSongPage_obj1_Bindings bindings = new SearchSongPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.Grid element7 = (global::Windows.UI.Xaml.Controls.Grid)target;
                    SearchSongPage_obj7_Bindings bindings = new SearchSongPage_obj7_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::MusicUWP.ViewModels.WebSong) element7.DataContext);
                    bindings.SetConverterLookupRoot(this);
                    element7.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element7, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

