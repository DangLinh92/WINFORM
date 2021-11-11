using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wisol.Common;

namespace Wisol
{
    public static class CommonRoleControl
    {
        public static DataTable RollControls
        {
            get; set;
        }
        public static bool GetActiveWithRole(string formId, string controlId)
        {
            try
            {
                if (formId.NullString() == "" || RollControls == null || RollControls.Rows.Count == 0) return false;

                var lstControl = RollControls.AsEnumerable().Where(row => row["Name"].NullString() == controlId && row["FORM"].NullString() == formId);
                bool active = true;
                if (lstControl.Count() > 0)
                {
                    if (lstControl.Count() == 1)
                    {
                        active = bool.Parse(lstControl.FirstOrDefault()["IsActive"].NullString());
                    }
                    else
                    {
                        foreach (var item in lstControl)
                        {
                            if (bool.Parse(item["IsActive"].NullString()))
                            {
                                active = true;
                                break;
                            }
                            else
                            {
                                active = bool.Parse(item["IsActive"].NullString());
                            }
                        }
                    }
                }
                return active;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
