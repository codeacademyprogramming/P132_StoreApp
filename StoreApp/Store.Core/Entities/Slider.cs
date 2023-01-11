using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class Slider:BaseEntity
    {
        public int Order { get; set; }
        public string Image { get; set; }
        public string RedirectUrl { get; set; }
    }
}
