using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Services
{
    public class IndivButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }
        public int BranchId { get; set; }
        public int KitId { get; set; }
        public int CustomerId { get; set; }
        public int MembershipTypeId { get; set; }
        public string UserId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");
                if(KitId !=null && KitId > 0)
                {
                    param.Append(String.Format("{0}", KitId));
                }

           
                if (BranchId != null && BranchId > 0)
                {
                    param.Append(String.Format("{0}", BranchId));
                }

                
                if (CustomerId != null && CustomerId > 0)
                {
                    param.Append(String.Format("{0}", CustomerId));
                }

           
                if (MembershipTypeId != null && MembershipTypeId > 0)
                {
                    param.Append(String.Format("{0}", MembershipTypeId));
                }

                if (UserId != null && UserId.Trim().Length > 0)
                {
                    param.Append(String.Format("{0}", UserId));
                }
                return param.ToString();

            }
        }
  
    }
}
