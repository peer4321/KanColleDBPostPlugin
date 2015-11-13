using DBPostPlugin.Models.Settings;
using DBPostPlugin.ViewModels;
using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace DBPostPlugin.Models
{
    public class Proxy : NotificationObject
    {
        private DBPostPlugin plugin;

        private IEnumerable<RecordViewModel> _Records;
        public IEnumerable<RecordViewModel> Records
        {
            get { return this._Records; }
            set
            {
                if (this._Records != value)
                {
                    this._Records = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public Proxy(DBPostPlugin plugin)
        {
            this.plugin = plugin;

            var proxy = KanColleClient.Current.Proxy;
            
            var urls = Enum.GetValues(typeof(Api)).Cast<Api>().Select(v => v.GetUrl()).ToList();
            foreach (var url in urls)
            {
                proxy.ApiSessionSource.Where(x => x.Request.PathAndQuery
                    .StartsWith("/kcsapi/" + url))
                    .Subscribe(x =>
                    {
                        if (ToolSettings.SendDb && !string.IsNullOrEmpty(ToolSettings.DbAccessKey))
                        {
                            System.Collections.Specialized.NameValueCollection post
                                = new System.Collections.Specialized.NameValueCollection();
                            post.Add("token", ToolSettings.DbAccessKey);
                            post.Add("agent", "LZXNXVGPejgSnEXLH2ur");
                            post.Add("url", x.Request.PathAndQuery);
                            string requestBody = System.Text.RegularExpressions.Regex.Replace(
                                x.Request.BodyAsString,
                                @"&api(_|%5F)token=[0-9a-f]+|api(_|%5F)token=[0-9a-f]+&?", "");
                            post.Add("requestbody", requestBody);
                            post.Add("responsebody", x.Response.BodyAsString);
#if DEBUG
                            MessageBox.Show(
                                string.Join(
                                    "\n", post.AllKeys.Select(key => key + ": " + post[key])),
                                    "この内容を送信します");
#else
                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.UploadValuesAsync(new Uri("http://api.kancolle-db.net/2/"), post);
#endif
                            if (ToolSettings.NotifyLog)
                                this.Notify(Notification.Types.Test, "送信しました", x.Request.PathAndQuery);
                        }
                    });
            }


            this.Records = new List<RecordViewModel> {
                new RecordViewModel(1, new Record(DateTime.Now, Api.api_port_port, "母港の色々")),
                new RecordViewModel(2, new Record(DateTime.Now, Api.api_get_member_mapinfo, "マップの情報")),
            };
        }

        private void Notify(string type, string header, string body)
        {
            if (ToolSettings.NotifyLog)
            {
                this.plugin.InvokeNotifyRequested(new NotifyEventArgs(type, header, body)
                {
                    Activated = () =>
                    {
                        DispatcherHelper.UIDispatcher.Invoke(() =>
                        {
                            var window = Application.Current.MainWindow;
                            if (window.WindowState == WindowState.Minimized)
                                window.WindowState = WindowState.Normal;
                            window.Activate();
                        });
                    }
                });
            }
        }
    }
}
