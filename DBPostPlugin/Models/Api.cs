using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.Models
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ApiUrl : Attribute
    {
        public string Url { get; }
        public ApiUrl(string url)
        {
            this.Url = url;
        }
    }

    public enum Api
    {
        [ApiUrl("api_port/port")]
        api_port_port,
        [ApiUrl("api_get_member/ship2")]
        api_get_member_ship2,
        [ApiUrl("api_get_member/ship3")]
        api_get_member_ship3,
        [ApiUrl("api_get_member/slot_item")]
        api_get_member_slot_item,
        [ApiUrl("api_get_member/kdock")]
        api_get_member_kdock,
        [ApiUrl("api_get_member/mapinfo")]
        api_get_member_mapinfo,
        [ApiUrl("api_req_hensei/change")]
        api_req_hensei_change,
        [ApiUrl("api_req_kousyou/createship")]
        api_req_kousyou_createship,
        [ApiUrl("api_req_kousyou/getship")]
        api_req_kousyou_getship,
        [ApiUrl("api_req_kousyou/createitem")]
        api_req_kousyou_createitem,
        [ApiUrl("api_req_map/start")]
        api_req_map_start,
        [ApiUrl("api_req_map/next")]
        api_req_map_next,
        [ApiUrl("api_req_map/select_eventmap_rank")]
        api_req_map_select_eventmap_rank,
        [ApiUrl("api_req_sortie/battle")]
        api_req_sortie_battle,
        [ApiUrl("api_req_battle_midnight/battle")]
        api_req_battle_midnight_battle,
        [ApiUrl("apt_req_battle_midnight/sp_midnight")]
        apt_req_battle_midnight_sp_midnight,
        [ApiUrl("api_req_sortie/night_to_day")]
        api_req_sortie_night_to_day,
        [ApiUrl("api_req_sortie/battlereqult")]
        api_req_sortie_battlereqult,
        [ApiUrl("api_req_combined_battle/battle")]
        api_req_combined_battle_battle,
        [ApiUrl("api_req_combined_battle/airbattle")]
        api_req_combined_battle_airbattle,
        [ApiUrl("api_req_combined_battle/midnight_battle")]
        api_req_combined_battle_midnight_battle,
        [ApiUrl("api_req_combined_battle/battleresult")]
        api_req_combined_battle_battleresult,
        [ApiUrl("api_req_sortie/airbattle")]
        api_req_sortie_airbattle,
        [ApiUrl("api_req_combined_battle/battle_water")]
        api_req_combined_battle_battle_water,
        [ApiUrl("api_req_combined_battle/sp_battle")]
        api_req_combined_battle_sp_battle,
    }

    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null) { return attr.Description; }
                }
            }
            return null;
        }

        public static string GetUrl(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    ApiUrl attr = Attribute.GetCustomAttribute(field,
                        typeof(ApiUrl)) as ApiUrl;
                    if (attr != null) { return attr.Url; }
                }
            }
            return null;
        }
    }
}
