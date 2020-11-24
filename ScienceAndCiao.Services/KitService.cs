using ScienceAndCiao.Data;
using ScienceAndCiao.Models.Kit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Services
{
    public class KitService
    {
        private readonly Guid _userId;
        public KitService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateKit(KitCreate model)
        {
            var entity = new Kit()
            {
             Title = model.Title,
             Description = model.Description,
             Grade = model.Grade,
             ImageUrl = model.ImageUrl,
             Price = model.Price,
             DateAdded = model.DateAdded,
             BranchId = model.BranchId,
             PublicationDate = model.PublicationDate,
             LengthInMinutes = model.LengthInMinutes
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Kits.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<KitListItem> GetKits()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Kits
                        .Select(
                            e =>
                                new KitListItem
                                {
                                    Title = e.Title,
                                    Description = e.Description,
                                    Grade = e.Grade,
                                    ImageUrl = e.ImageUrl,
                                    Price = e.Price,
                                    DateAdded = e.DateAdded,
                                    BranchId = e.BranchId,
                                    PublicationDate = e.PublicationDate,
                                    LengthInMinutes = e.LengthInMinutes

                                }
                        );
                return query.ToArray();
            }
        }

        public KitDetail GetKitByID(int kitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kits
                        .Single(e => e.KitId == kitId);
                return
                    new KitDetail
                    {
                        Title = entity.Title,
                        Description = entity.Description,
                        Grade = entity.Grade,
                        ImageUrl = entity.ImageUrl,
                        Price = entity.Price,
                        DateAdded = entity.DateAdded,
                        BranchId = entity.BranchId,
                        PublicationDate = entity.PublicationDate,
                        LengthInMinutes = entity.LengthInMinutes
                    };
            }
        }

        public bool UpdateKit(KitEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kits
                        .Single(e => e.KitId == model.KitId);

                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.Grade = model.Grade;
                entity.ImageUrl = model.ImageUrl;
                entity.Price = model.Price;
                entity.DateAdded = model.DateAdded;
                entity.BranchId = model.BranchId;
                entity.PublicationDate = model.PublicationDate;
                entity.LengthInMinutes = model.LengthInMinutes;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteKit(int KitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kits
                        .Single(e => e.KitId == KitId);

                ctx.Kits.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<KitListItem> SortKits(string sortOrder, string searchString)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var kits = from s in ctx.Kits                          
                            select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    kits = kits.Where(s => s.Title.Contains(searchString)
                    || s.Branch.BranchName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "kitId_desc":
                        kits = kits.OrderByDescending(s => s.KitId);
                        break;
                    case "kitTitle":
                        kits = kits.OrderBy(s => s.Title);
                        break;
                    case "kitTitle_desc":
                        kits = kits.OrderByDescending(s => s.Title);
                        break;
                    case "kitBranch":
                        kits = kits.OrderBy(s => s.Branch);
                        break;
                    case "grade":
                        kits = kits.OrderBy(s => s.Grade);
                        break;
                    case "grade_desc":
                        kits = kits.OrderByDescending(s => s.Grade);
                        break;
                    case "DateAdded":
                        kits = kits.OrderBy(s => s.DateAdded);
                        break;
                    case "LengthInMinutes":
                        kits = kits.OrderByDescending(s => s.LengthInMinutes);
                        break;
                    default:
                        kits = kits.OrderBy(s => s.KitId);
                        break;
                }

                return (kits.Select(
                            e =>
                                new KitListItem
                                {
                                    Title = e.Title,
                                    Description = e.Description,
                                    Grade = e.Grade,
                                    ImageUrl = e.ImageUrl,
                                    Price = e.Price,
                                    DateAdded = e.DateAdded,
                                    BranchId = e.BranchId,
                                    PublicationDate = e.PublicationDate,
                                    LengthInMinutes = e.LengthInMinutes
                                }
                        ).ToList());
            }
        }
    }
}
