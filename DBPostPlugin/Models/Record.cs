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
        public string ApiStr { get
            {
                switch (Api)
                {
                    default:
                        return "分からん";
                }
            } }

        public string Description { get; }

        public Record (DateTime timespamp, Api api, string description)
        {
            this.Timestamp = timespamp;
            this.Api = api;
            this.Description = description;
        }
    }

    public enum Api
    {
        api_port_port,
        api_get_member_ship2,
        api_get_member_ship3,
        api_get_member_slot_item,
        api_get_member_kdock,
        api_get_member_mapinfo,
        api_req_hensei_change,
        api_req_kousyou_createship,
        api_req_kousyou_getship,
        api_req_kousyou_createitem,
        api_req_map_start,
        api_req_map_next,
        api_req_map_select_eventmap_rank,
        api_req_sortie_battle,
        api_req_battle_midnight_battle,
        apt_req_battle_midnight_sp_midnight,
        api_req_sortie_night_to_day,
        api_req_sortie_battlereqult,
        api_req_combined_battle_battle,
        api_req_combined_battle_airbattle,
        api_req_combined_battle_midnight_battle,
        api_req_combined_battle_battleresult,
        api_req_sortie_airbattle,
        api_req_combined_battle_battle_water,
        api_req_combined_battle_sp_battle,
    }
}
