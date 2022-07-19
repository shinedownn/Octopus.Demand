using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace WebAPI.Models
{
    public class FGSIncomingModel
    {
        public string Token { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Event_type Event_Type { get; set; }
        public string Time { get; set; }
        public int Call_id { get; set; }
        public string agent { get; set; }
        public string queue_name { get; set; }
        public string caller { get; set; }
        public string dialed { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction direction { get; set; }
    }

    public enum Event_type
    {
        Ringing,
        CallBack,
        Connected,
        Transferred,        
        Disconnected
    }

    public enum Direction
    {
        INBOUND,
        OUTBOUND,
    }
}
