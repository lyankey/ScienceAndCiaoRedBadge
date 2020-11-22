using ScienceAndCiao.Data;
using ScienceAndCiao.Models.Rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Services
{
    public class RentalService
    {
        private readonly Guid _userId;
        public RentalService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRental(RentalCreate model)
        {
            var entity = new Rental()
            {
                Id = model.Id,
                KitId = model.KitId,
                RentalPrice = model.RentalPrice,
                StartDate= model.StartDate,
                EndDate = model.EndDate,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Rentals.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }


        public IEnumerable<RentalListItem> GetRental()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Rentals
                        .Select(
                            e =>
                                new RentalListItem
                                {
                                    Id = e.Id,
                                    KitId = e.KitId,
                                    RentalPrice = e.RentalPrice,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                }
                        );
                return query.ToArray();
            }
        }

        public RentalDetail GetRentalById(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Rentals
                        .Single(e => e.Id == Id);
                return
                    new RentalDetail
                    {
                        Id = entity.Id,
                        KitId = entity.KitId,
                        RentalPrice = entity.RentalPrice,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                    };
            }
        }

        public bool UpdateRental(RentalEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Rentals
                        .Single(e => e.Id == model.Id);

                entity.Id = model.Id;
                entity.KitId = model.KitId;
                entity.RentalPrice = model.RentalPrice;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRental(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Rentals
                        .Single(e => e.Id == Id);

                ctx.Rentals.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}