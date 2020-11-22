using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Models.Kit
{
    public static class KitThumbnailListItem
    {
        //using this extensiont to get 4 thumbnails at a time
        //must be static class to work
        public static IEnumerable<KitThumbnail> GetBookThumbnail(this List<KitThumbnail> thumbnails, ApplicationDbContext db = null, string search = null)
        {
            try
            {
                if (db == null) db = ApplicationDbContext.Create();

                thumbnails = (from k in db.Kits
                              select new KitThumbnail
                              {
                                  KitId = k.KitId,
                                  Title = k.Title,
                                  Description = k.Description,
                                  ImageUrl = k.ImageUrl,
                                  Link = "/KitDetail/Index/" + k.KitId,
                              }).ToList();
                //converting the output to a list and storing it in the kitthumbnails
                //searching the thumbnails from the home page
                if (search != null)
                {
                    return thumbnails.Where(s => s.Title.ToLower().Contains(search.ToLower())).OrderBy(s => s.Title);
                }
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(t => t.Title);
            //then bring this in the home index view
        }
    }
}
