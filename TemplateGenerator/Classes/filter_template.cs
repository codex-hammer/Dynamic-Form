using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateGenerator.Classes
{
    public class filter_template
    {
        public filter_template_header ft_head { get; set; }
        public List<filter_template_details> ft_detail { get; set; }
    }
}