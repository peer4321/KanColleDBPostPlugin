using DBPostPlugin.Models.Settings;
using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBPostPlugin.Models
{
    public class Proxy : NotificationObject
    {
        private DBPostPlugin plugin;

        public Proxy(DBPostPlugin plugin)
        {
            this.plugin = plugin;

            var proxy = KanColleClient.Current.Proxy;
            
            string[] urls =
            {
                "api_port/port",
                "api_get_member/ship2",
                "api_get_member/ship3",
                "api_get_member/slot_item",
                "api_get_member/kdock",
                "api_get_member/mapinfo",
                "api_req_hensei/change",
                "api_req_kousyou/createship",
                "api_req_kousyou/getship",
                "api_req_kousyou/createitem",
                "api_req_map/start",
                "api_req_map/next",
                "api_req_map/select_eventmap_rank",
                "api_req_sortie/battle",
                "api_req_battle_midnight/battle",
                "api_req_battle_midnight/sp_midnight",
                "api_req_sortie/night_to_day",
                "api_req_sortie/battleresult",
                "api_req_combined_battle/battle",
                "api_req_combined_battle/airbattle",
                "api_req_combined_battle/midnight_battle",
                "api_req_combined_battle/battleresult",
                "api_req_sortie/airbattle",
                "api_req_combined_battle/battle_water",
                "api_req_combined_battle/sp_midnight",
            };
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
                            post.Add("Token", ToolSettings.DbAccessKey);
                            post.Add("agent", "LZXNXVGPejgSnEXLH2ur");
                            post.Add("url", x.Request.PathAndQuery);
                            string requestBody = System.Text.RegularExpressions.Regex.Replace(x.Request.BodyAsString, @"&api(_|%5F)token=[0-9a-f]+|api(_|%5F)token=[0-9a-f]+&?", "");
                            post.Add("requestbody", requestBody);
                            post.Add("responsebody", x.Response.BodyAsString);
#if DEBUG
                            MessageBox.Show(string.Join("\n", post.AllKeys.Select(key => key + ": " + post[key])), "この内容を送信します");
#else
                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.UploadValuesAsync(new Uri("http://api.kancolle-db.net/2/"), post);
#endif
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
