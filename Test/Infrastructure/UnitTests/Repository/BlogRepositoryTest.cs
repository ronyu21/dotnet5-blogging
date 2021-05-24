using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Infrastructure.UnitTests.Repository
{
    public class BlogRepositoryTest : TestBase
    {
        private IBlogRepository _blogRepository;
        private BloggingContext _context;

        [SetUp]
        public void Setup()
        {
            _blogRepository = new BlogRepository(new BloggingContext(databaseOptions));

            _context = new BloggingContext(databaseOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task ShouldAbleToCreateBlogRecord()
        {
            var expected = await _blogRepository.CreateAsync(new Blog
            {
                Url = "http://localhost/blog1"
            });

            var blogs = _context.Blogs.FromSqlRaw("select * from blogging.blogs").ToList();

            Assert.IsTrue(blogs.Count == 1);
            Assert.AreEqual(expected.Url, blogs.First().Url);
        }

        [Test]
        public async Task ShouldAbleToGetAllBlogs()
        {
            var expected = new List<Blog>();

            for (var i = 0; i < 3; i++)
                expected.Add(await _blogRepository.CreateAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                }));

            var actual = await _blogRepository.GetAllAsync();

            actual.ToList().ForEach(blog => Assert.Contains(blog, expected));
        }

        [Test]
        public async Task ShouldAbleToGetBlogById()
        {
            var expected = new List<Blog>();

            for (var i = 0; i < 2; i++)
                expected.Add(await _blogRepository.CreateAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                }));

            foreach (var blog in expected)
            {
                var actual = await _blogRepository.GetAsync(blog.BlogId);
                Assert.AreEqual(actual.BlogId, blog.BlogId);
                Assert.AreEqual(actual.Url, blog.Url);
            }
        }

        [Test]
        public async Task ShouldAbleToUpdateBlog()
        {
            var blogs = new List<Blog>();

            for (var i = 0; i < 2; i++)
                blogs.Add(await _blogRepository.CreateAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                }));

            var expected = blogs.Last();
            expected.Url = "changed";

            var actual = await _blogRepository.UpdateAsync(expected);
            Assert.AreEqual(expected.BlogId, actual.BlogId);
            Assert.AreEqual(expected.Url, actual.Url);

            actual = _context.Blogs.FromSqlInterpolated(
                $"SELECT * FROM blogging.blogs WHERE blog_id = {expected.BlogId}").FirstOrDefault();

            Assert.NotNull(actual);
            Assert.AreEqual(expected.BlogId, actual.BlogId);
            Assert.AreEqual(expected.Url, actual.Url);
        }

        [Test]
        public async Task ShouldAbleToDeleteBlog()
        {
            var blogs = new List<Blog>();

            for (var i = 0; i < 2; i++)
                blogs.Add(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                });

            _context.Blogs.AddRange(blogs);
            await _context.SaveChangesAsync();

            blogs = _context.Blogs.FromSqlRaw("select * from blogging.blogs").ToList();

            Assert.AreEqual(2, blogs.Count);

            await _blogRepository.DeleteAsync(blogs.First().BlogId);

            Assert.AreEqual(1, _context.Blogs.Count());

            blogs.RemoveAt(0);
            var expected = blogs.First();
            var actual = _context.Blogs.FromSqlRaw("select * from blogging.blogs").ToList().First();
            Assert.AreEqual(expected.BlogId, actual.BlogId);
            Assert.AreEqual(expected.Url, actual.Url);
        }
    }
}