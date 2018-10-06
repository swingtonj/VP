using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VP.Models
{
    public class Specify : Common
    {
        public List<droplist> Lst_Industry { get; set; }
        public List<droplist> Lst_BusinessImperative { get; set; }
        public List<droplist> Lst_TypesOfAnalytics { get; set; }
    }
}