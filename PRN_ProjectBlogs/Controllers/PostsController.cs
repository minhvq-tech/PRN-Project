using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using PRN_ProjectBlogs.Helpers;
using PRN_ProjectBlogs.Models;

namespace PRN_ProjectBlogs.Controllers
{
    public class PostsController : Controller
    {
        private readonly dbBlogsContext _context;

        public PostsController(dbBlogsContext context)
        {
            _context = context;
        }
        
        // GET: List
        [Route("{Alias}", Name = "ListTin")]
        public IActionResult List(string Alias, string searchString, int? page)
        {
            if (string.IsNullOrEmpty(Alias)) return RedirectToAction("Home", "Index");
            var danhmuc =_context.Categories.FirstOrDefault(x => x.Alias == Alias);
            if (danhmuc == null) return RedirectToAction("Home", "Index");
            var pageNumber =page == null || page < -0 ? 1 : page.Value;
            var pagesize = Utilities.PAGE_SIZE;

            List<Post> lsPosts = new List<Post>();
            

            if (!string.IsNullOrEmpty(Alias) && !string.IsNullOrEmpty(searchString))
            {
                lsPosts = _context.Posts
                    .Include(x => x.Cat)
                    .Where(x => x.CatId == danhmuc.CatId )
                    .Where(x => x.Title.Contains(searchString))
                    .Where(x => x.Published == true)
                    .AsNoTracking()
                   .OrderByDescending(x => x.CreatedDate)
                   .ToList();
            }
            else
            {
                lsPosts = _context.Posts.Include(x => x.Cat)
                        .Where(x => x.CatId == danhmuc.CatId)
                        .Where(x => x.Published == true)
                        .AsNoTracking()
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();

            }
             
            PagedList<Post> models = new PagedList<Post>(lsPosts.AsQueryable(), pageNumber, pagesize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.DanhMuc = danhmuc;
            return View(models);
        }

        // GET: Posts/Details/5
        [Route("/{Alias}.html", Name = "PostsDetails")]
        public async Task<IActionResult> Details(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Account)
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.Alias == alias);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Contents,Thumb,Published,Alias,CreatedDate,AccountId,Author,Tags,Scontents,CatId,IsHot,IsNewfeed")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", post.CatId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", post.CatId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Contents,Thumb,Published,Alias,CreatedDate,AccountId,Author,Tags,Scontents,CatId,IsHot,IsNewfeed")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", post.CatId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Account)
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
