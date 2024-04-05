using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_Connector
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Datum
    {
        public string message { get; set; }
        public From from { get; set; }
        public DateTime created_time { get; set; }
        public To to { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string email { get; set; }
        public string id { get; set; }
    }

    public class Messages
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
    }

    public class Participants
    {
        public List<Datum> data { get; set; }
    }

    public class FbMessage
    {
        public Messages messages { get; set; }
        public int unread_count { get; set; }
        public int message_count { get; set; }
        public Senders senders { get; set; }
        public Participants participants { get; set; }
        public string id { get; set; }
    }

    public class Senders
    {
        public List<Datum> data { get; set; }
    }

    public class To
    {
        public List<Datum> data { get; set; }
    }


}
