using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentityPr.Data;
using UserIdentityPr.Models;

namespace UserIdentityPr.Services
{
    public class AlbumEfService : IAlbumService
    {
        private readonly AlbumDbcontext _context;

        public AlbumEfService(AlbumDbcontext context)
        {
            this._context = context;
        }
        public async Task<Album> AddAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task DeleteAsync(Album model)
        {
            _context.Albums.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Album>> GetAllAsync()
        {
            return  await _context.Albums.ToListAsync();
        }


        public async Task<Album> GetByIdAsync(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public async Task UpdateAsync(Album model)
        {
           
            await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
