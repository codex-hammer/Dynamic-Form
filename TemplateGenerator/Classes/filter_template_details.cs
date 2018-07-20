using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateGenerator.Classes
{
    public class filter_template_details
    {
        public string DBColumnName { get; set; }
        public string DisplayColumnName { get; set; }
        public string IsHidden { get; set; }
        public string IsAvailableForSearch { get; set; }
        public string IsNavigateParameter { get; set; }
        public string NavigateParameterName { get; set; }
        public string IsDateRange { get; set; }
        public string SequenceId { get; set; }
        public string IsYesNo { get; set; }
        public string IsAmount { get; set; }
        public string IsSearchValue { get; set; }
        public string IsLookUp { get; set; }
        public string LookupDBColName { get; set; }
        public string LookupPage { get; set; }
        public string Method { get; set; }
        public List<searchcombobox> options { get; set; }
    }
}