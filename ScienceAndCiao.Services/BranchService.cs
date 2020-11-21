using RedBadgeProject.Data;
using ScienceAndCiao.Data;
using ScienceAndCiao.Models.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Services
{
    public class BranchService
    {
        private readonly Guid _userId;
        public BranchService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBranch(BranchCreate model)
        {
            var entity = new Branch()
            {
                BranchName = model.BranchName,

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Branches.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BranchListItem> GetConsole()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Branches
                        .Select(
                            e =>
                                new BranchListItem
                                {
                                    BranchName = e.BranchName,
                                }
                        );
                return query.ToArray();
            }
        }

        public BranchDetail GetBranchByName(string branchName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Branches
                        .Single(e => e.BranchName == branchName);
                return
                    new BranchDetail
                    {
                        BranchName = entity.BranchName
                    };
            }
        }

        public bool UpdateBranch(BranchEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Branches
                        .Single(e => e.BranchName == model.BranchName);

                entity.BranchName = model.BranchName;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBranch(int BranchId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Branches
                        .Single(e => e.BranchId == BranchId);

                ctx.Branches.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BranchListItem> SortBranches(string sortOrder, string searchString)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var branches = from s in ctx.Branches
                               select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    branches = branches.Where(s => s.BranchName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "consoleID_desc":
                        branches = branches.OrderByDescending(s => s.BranchId);
                        break;
                    case "consoleName":
                        branches = branches.OrderBy(s => s.BranchName);
                        break;
                    case "consoleName_desc":
                        branches = branches.OrderByDescending(s => s.BranchName);
                        break;
                }

                return (branches.Select(
                            e =>
                                new BranchListItem
                                {
                                    BranchId = e.BranchId,
                                    BranchName = e.BranchName,

                                }
                        ).ToList());
            }
        }
    }
}
