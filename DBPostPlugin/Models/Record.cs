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
            this.Detail = api.GetDetail() ?? GenDetail(api, s);
        }

        public static string GenDetail(Api api, Session s)
        {
            switch (api)
            {
                default:
                    return "";
            }
        }
    }
    
}
