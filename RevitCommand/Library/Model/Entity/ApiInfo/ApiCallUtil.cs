using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ApiCallUtil
    {
        public static ApiCall Get(string domain)
        {
            return new ApiCall
            {
                Domain = domain
            };
        }
    }
}
