using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;

namespace Evergreen_Persistence.Concretes
{
    public class BlogService : IBlogService
    {
        AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(BlogVm blogvm)
        {
            Blog blog = new Blog()
            {
                Id = blogvm.Id,
                Title = blogvm.Title,
                Description = blogvm.Description,
                Date = blogvm.Date,
                ImgUrl = blogvm.ImgUrl,
            };
            _context.Blogs.Add(blog);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        public BlogVm Get(int id)
        {
           var blog= _context.Blogs.FirstOrDefault(blog => blog.Id == id);
            BlogVm blogVm = new BlogVm()
            { Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Date = blog.Date,
                ImgUrl = blog.ImgUrl,
            };
            return blogVm;
        }

        public List<Blog> Getall()
        {
          return  _context.Blogs.ToList();
        }

        public void Update(BlogVm newblogvm)
        {
            var oldblog= _context.Blogs.FirstOrDefault(x=>x.Id==newblogvm.Id);
            oldblog.Id = newblogvm.Id;
            oldblog.Date = newblogvm.Date;
            oldblog.Title = newblogvm.Title;
            oldblog.Description = newblogvm.Description;
            oldblog.ImgUrl = newblogvm.ImgUrl;
            _context.SaveChanges();
        }
    }
}
