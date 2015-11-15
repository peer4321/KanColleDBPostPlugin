using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper.Models.Raw;
using Nekoxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.Models
{
    public class Record
    {
        public DateTime Timestamp { get; }
        public string TimestampStr { get { return Timestamp.ToString("yyyy-MM-dd HH:mm:ss"); } }

        public Api Api { get; }
        public string Type { get { return Api.GetDescription() ?? "知らない子"; } }

        public string Detail { get; }

        public Record (DateTime timestamp, Api api, Session s)
        {
            this.Timestamp = timestamp;
            this.Api = api;
            this.Detail = GenDetail(api, s);
        }

        public static string GenDetail(Api api, Session s)
        {
            bool showDefaultDescription = false;

            switch (api)
            {
                case Api.api_req_kousyou_createship:
                    {
                        var req = SvData.Parse<kcsapi_createship>(s).Request;
                        return string.Format("第{0}建造ドック ({1}/{2}/{3}/{4}/{5}) で建造開始",
                            req["api_kdock_id"],
                            req["api_item1"],
                            req["api_item2"],
                            req["api_item3"],
                            req["api_item4"],
                            req["api_item5"]);
                    }
                case Api.api_req_kousyou_getship:
                    {
                        var svdata = SvData.Parse<kcsapi_kdock_getship>(s);
                        var req = svdata.Request;
                        var res = svdata.Data;
                        return string.Format("第{0}建造ドックで {1} を入手",
                            req["api_kdock_id"],
                            KanColleClient.Current.Master.Ships[res.api_ship_id].Name);
                    }
                case Api.api_req_kousyou_createitem:
                    {
                        var svdata = SvData.Parse<kcsapi_createitem>(s);
                        var req = svdata.Request;
                        var slotitem = new CreatedSlotItem(svdata.Data);
                        return string.Format("({0}/{1}/{2}/{3}){4} 開発{5}",
                            req["api_item1"], 
                            req["api_item2"],
                            req["api_item3"],
                            req["api_item4"],
                            slotitem.Succeed ? 
                                " " + slotitem.SlotItemInfo.Name : "",
                            slotitem.Succeed ? "成功" : "失敗");
                    }
                case Api.api_req_sortie_battleresult:
                    {
                        var data = SvData.Parse<kcsapi_battleresult>(s).Data;
                        return string.Format("{0} {1}：ランク{2} {3}",
                            data.api_quest_name,
                            data.api_enemy_info.api_deck_name,
                            data.api_win_rank,
                            data.api_get_flag[1] == 1 ?
                                "ドロップ：" + data.api_get_ship.api_ship_name : "");
                    }
                case Api.api_req_combined_battle_battleresult:
                    {
                        var data = SvData.Parse<kcsapi_combined_battle_battleresult>(s).Data;
                        return string.Format("{0} {1}：ランク{2} {3}",
                            data.api_quest_name,
                            data.api_enemy_info.api_deck_name,
                            data.api_win_rank,
                            data.api_get_flag[1] == 1 ?
                                "ドロップ：" + data.api_get_ship.api_ship_name : "");
                    }
                default:
                    return showDefaultDescription ? api.GetDetail() ?? "" : "";
            }
        }
    }
    
}
