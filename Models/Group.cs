using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eve_API.Models
{
    public class Group
    {
        public int groupID;
        public int categoryID;
        public string groupName;
        public int iconID;
        public Boolean useBasePrice;
        public Boolean anchored;
        public Boolean anchorable;
        public Boolean fittableNonSingleton;
        public Boolean published;
    }
}