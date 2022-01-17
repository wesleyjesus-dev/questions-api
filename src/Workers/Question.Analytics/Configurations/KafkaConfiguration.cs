using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question.Analytics.Configurations
{
    public class KafkaConfiguration
    {
        public string? GroupId { get; set; }
        public string? BootstrapServer { get; set; }
    }
}
