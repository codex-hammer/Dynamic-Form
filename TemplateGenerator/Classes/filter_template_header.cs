using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateGenerator.Classes
{
    public class filter_template_header
    {
        public string pageid { get; set; }
        public string pagetitle { get; set; }
        public string exiturl { get; set; }
        public string navigateurl { get; set; }
        public string modelname { get; set; }
        public string methodname { get; set; }
        public string pagesize { get; set; }
        public string exportexcel { get; set; }
        public string apipath { get; set; }
        public string action { get; set; }
    }
}