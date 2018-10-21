using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models
{
    public class ExtensionType
    {
        public long ExtensionTypeId { get; set; }
        public string Ext { get; set; }
        public Nullable<long> MediaTypeId { get; set; }
    }
}
