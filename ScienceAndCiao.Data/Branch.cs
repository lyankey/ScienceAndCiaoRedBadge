using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Data
{
    public class Branch
    {
        [Required, DisplayName("Branch of Science")]
        [Key]
        public int BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
