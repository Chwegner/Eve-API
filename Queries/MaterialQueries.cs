using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eve_API.Queries
{
    public class MaterialQueries
    {
        public string GetProductionMaterials(int groupID)
        {
            return
                "SELECT industryActivityMaterials.* \r\n" +
                "FROM industryActivityMaterials, industryActivityProducts, invTypes \r\n" +
                "WHERE industryActivityMaterials.typeID = industryActivityProducts.typeID \r\n" +
                "   AND industryActivityMaterials.typeID = invTypes.typeID \r\n" +
                "   AND industryActivityMaterials.activityID = 1 \r\n" +
                "   AND industryActivityProducts.activityID = 1 \r\n" +
                "   AND invTypes.groupID = " + groupID + " \r\n" +
                "   AND invTypes.published = 1 \r\n";
        }


    }
}