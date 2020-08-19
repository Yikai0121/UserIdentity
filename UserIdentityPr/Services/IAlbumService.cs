using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentityPr.Models;

namespace UserIdentityPr.Services
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAllAsync();
        Task<Album> GetByIdAsync(int id);
        Task<Album> AddAsync(Album model);
        Task UpdateAsync(Album model);
        Task DeleteAsync(Album model);
        Task<bool> SaveAsync();

    }
}
