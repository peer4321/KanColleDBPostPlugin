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
    public class UrlAttr : Attribute
    {
        public string Url { get; }
        public UrlAttr(string url) { this.Url = url; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class DetailAttr : Attribute
    {
        public string Detail { get; }
        public DetailAttr(string detail) { this.Detail = detail; }
    }

    public enum Api
    {
        [UrlAttr("api_port/port")]
        [Description("母港情報")]
        [DetailAttr("司令部、艦隊、ログ、保有資源、入渠、保有艦娘データ")]
        api_port_port,

        [UrlAttr("api_get_member/ship2")]
        [Description("旧所持艦情報？")]
        api_get_member_ship2,

        [UrlAttr("api_get_member/ship3")]
        [Description("装備換装")]
        [DetailAttr("装備換装時に呼ばれる")]
        api_get_member_ship3,

        [UrlAttr("api_get_member/slot_item")]
        [Description("装備データ")]
        [DetailAttr("保有装備の詳しい情報")]
        api_get_member_slot_item,

        [UrlAttr("api_get_member/kdock")]
        [Description("工廠ドック情報")]
        api_get_member_kdock,

        [UrlAttr("api_get_member/mapinfo")]
        [Description("出撃マップ情報")]
        api_get_member_mapinfo,

        [UrlAttr("api_req_hensei/change")]
        [Description("艦隊編成変更")]
        api_req_hensei_change,

        [UrlAttr("api_req_kousyou/createship")]
        [Description("建造開始")]
        api_req_kousyou_createship,

        [UrlAttr("api_req_kousyou/getship")]
        [Description("建造完了")]
        api_req_kousyou_getship,

        [UrlAttr("api_req_kousyou/createitem")]
        [Description("装備開発")]
        api_req_kousyou_createitem,

        [UrlAttr("api_req_map/start")]
        [Description("出撃")]
        api_req_map_start,

        [UrlAttr("api_req_map/next")]
        [Description("進撃")]
        api_req_map_next,

        [UrlAttr("api_req_map/select_eventmap_rank")]
        [Description("イベ難易度")]
        api_req_map_select_eventmap_rank,

        [UrlAttr("api_req_sortie/battle")]
        [Description("昼戦")]
        api_req_sortie_battle,

        [UrlAttr("api_req_battle_midnight/battle")]
        [Description("夜戦")]
        api_req_battle_midnight_battle,

        [UrlAttr("apt_req_battle_midnight/sp_midnight")]
        [Description("開幕夜戦")]
        apt_req_battle_midnight_sp_midnight,

        [UrlAttr("api_req_sortie/night_to_day")]
        [Description("昼戦移行")]
        api_req_sortie_night_to_day,

        [UrlAttr("api_req_sortie/battleresult")]
        [Description("戦闘結果")]
        api_req_sortie_battleresult,

        [UrlAttr("api_req_combined_battle/battle")]
        [Description("戦闘(機動部隊)")]
        api_req_combined_battle_battle,

        [UrlAttr("api_req_combined_battle/airbattle")]
        [Description("航空戦(機動部隊)")]
        api_req_combined_battle_airbattle,

        [UrlAttr("api_req_combined_battle/midnight_battle")]
        [Description("夜戦(連合部隊)")]
        api_req_combined_battle_midnight_battle,

        [UrlAttr("api_req_combined_battle/battleresult")]
        [Description("戦闘結果(連合艦隊)")]
        api_req_combined_battle_battleresult,

        [UrlAttr("api_req_sortie/airbattle")]
        [Description("航空戦")]
        api_req_sortie_airbattle,

        [UrlAttr("api_req_combined_battle/battle_water")]
        [Description("戦闘(水上部隊)")]
        api_req_combined_battle_battle_water,

        [UrlAttr("api_req_combined_battle/sp_midnight")]
        [Description("開幕夜戦(連合艦隊)")]
        api_req_combined_battle_sp_midnight,
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

        public static string GetDetail(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DetailAttr attr = Attribute.GetCustomAttribute(field,
                        typeof(DetailAttr)) as DetailAttr;
                    if (attr != null) { return attr.Detail; }
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
                    UrlAttr attr = Attribute.GetCustomAttribute(field,
                        typeof(UrlAttr)) as UrlAttr;
                    if (attr != null) { return attr.Url; }
                }
            }
            return null;
        }
    }
}
