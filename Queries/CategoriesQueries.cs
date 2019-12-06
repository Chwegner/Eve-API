using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eve_API.Queries
{
    public class CategoriesQueries
    {
        public string GetCategories()
        {
            return
                "SELECT * \r\n" +
                "FROM dbo.invCategories \r\n" +
                "WHERE published = 1 \r\n";
        }
    }
}