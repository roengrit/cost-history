using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostHistory.Models
{
 
    public class ProductSave
    {
        public string key { get; set; }
        public string doc_no { get; set; }
        public string item_code { get; set; }
        public string unit_code { get; set; }
        public string rebate { get; set; }       
        public string vat_add { get; set; }
        public string transport_bkk_nk { get; set; }
        public string transport_nk_vt { get; set; }
        public string transport_bkk_vt { get; set; }
        public string import_value { get; set; }
        public string other_value { get; set; }
        public string remark { get; set; }
        
    }
}