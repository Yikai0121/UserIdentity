using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserIdentityPr.Models;
using UserIdentityPr.Services;
using UserIdentityPr.ViewModels;

namespace UserIdentityPr.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumService albumService, IMapper mapper)
        {
            this._albumService = albumService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _albumService.GetAllAsync();

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
            {
                return RedirectToAction("Index");
            }
            return View(album);
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var album = await _albumService.GetByIdAsync(id);
        //    if (album == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(album);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
            {
                return RedirectToAction(nameof(Index));
                // return NotFound();
            }

            try
            {
                await _albumService.DeleteAsync(album);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AlbumCreateViewModel albumCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "創建失敗");
                return View();
            }

            var entity = _mapper.Map<Album>(albumCreateViewModel);
            await _albumService.AddAsync(entity);

            return RedirectToAction("Details", new { id = entity.Id });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
            {
                return RedirectToAction("Index");
            }
           

            return View(album);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlbumUpdateViewModel albumUpdateViewModel)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
            {
                return RedirectToAction("Index");
            }
            _mapper.Map(albumUpdateViewModel, album);
            await _albumService.UpdateAsync(album);

            return RedirectToAction("Details", new { id = album.Id });
        }

    }
}
