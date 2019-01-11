using System;
using System.Collections.Generic;
using System.Text;

namespace Rememberall.Domains
{
    public class Alarms
    {
        public int Id { get; set; }
        public string Alarmname { get; set; }
        public DateTime DateId { get; set; }
        public TimeSpan Alarmtime { get; set; }

    }
}
