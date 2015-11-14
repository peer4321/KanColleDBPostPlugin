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
        
        private List<Record> Records;

        private IReadOnlyCollection<RecordViewModel> _Rows;
        public IReadOnlyCollection<RecordViewModel> Rows
        {
            get { return this._Rows; }
            set
            {
                if (this._Rows != value)
                {
                    this._Rows = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public RecordSortWorker SortWorker { get; }

        public void SortRows(RecordSortWorker.SortableColumn column)
        {
            this.SortWorker.SetFirst(column);
            this.UpdateRows();
        }

        private void UpdateRows()
        {
            this.Rows = this.SortWorker.Sort(this.Records)
                .Select((x, i) => new RecordViewModel(i + 1, x))
                .ToList();
        }

        public Proxy(DBPostPlugin plugin)
        {
            this.plugin = plugin;
            this.SortWorker = new RecordSortWorker();
            this.Records = new List<Record>();

            var proxy = KanColleClient.Current.Proxy;
            
            var apis = Enum.GetValues(typeof(Api)).Cast<Api>().ToList();
            foreach (var api in apis)
            {
                var url = api.GetUrl();
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
                            Records.Add(new Record(DateTime.Now, api, x));
                            this.UpdateRows();

                            if (ToolSettings.NotifyLog)
                                this.Notify(Notification.Types.Test, "送信しました", x.Request.PathAndQuery);
                        }
                    });
            }
            
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
