using EsolApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Repositories.Images
{
    public class ImageRepository : Repository<Model.Images, int>, IImageRepository
    {
        EsolAppDbContext _context;
        public ImageRepository(EsolAppDbContext context) : base (context)
        {
            _context = context;
        }
    }
}
