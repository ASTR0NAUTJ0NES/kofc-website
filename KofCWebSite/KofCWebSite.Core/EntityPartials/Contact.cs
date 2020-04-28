using System;
using System.Collections.Generic;
using System.Text;
using KofCWebSite.Core.Extensions;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class Contact    
    {
        public bool IsFullNameTrimmedEqual(string fname, string lname, string suffix = null, string mname = null)
        {
            bool isNoSuffix = string.IsNullOrWhiteSpace(suffix);
            bool isNoMName = string.IsNullOrWhiteSpace(mname);
            bool isNoFName = string.IsNullOrWhiteSpace(fname);
            bool isNoLName = string.IsNullOrWhiteSpace(lname);

            if (isNoMName && isNoSuffix && isNoFName && isNoLName) return false;

            if (isNoMName && isNoSuffix)
            {
                if (isNoFName || isNoLName) return false;

                return Firstname.IsTrimmedEquals(fname) && Lastname.IsTrimmedEquals(lname);
            }

            return false;
        }

    }

}
