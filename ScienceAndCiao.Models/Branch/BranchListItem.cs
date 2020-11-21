using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Models.Branch
{
    public class BranchListItem
    {
        [Required, DisplayName("Branch of Science")]
        public string BranchName { get; set; }
        public int BranchId { get; set; }
    }
}
