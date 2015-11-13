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
    
}
