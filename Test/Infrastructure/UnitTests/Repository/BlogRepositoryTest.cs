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
        private IRepository<Blog> _blogRepository;
        private BloggingContext _context;

        [SetUp]
        public void Setup()
        {
            _blogRepository = new Repository<Blog>(new BloggingContext(databaseOptions));

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
            var expected = await _blogRepository.InsertAsync(new Blog
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
                expected.Add(await _blogRepository.InsertAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                }));

            var actual = _blogRepository.GetAllAsync();
            await foreach (var blog in actual)
            {
                Assert.That(expected, Contains.Item(blog));
            }
        }

        [Test]
        public async Task ShouldAbleToGetBlogById()
        {
            var expected = new List<Blog>();

            for (var i = 0; i < 2; i++)
                expected.Add(await _blogRepository.InsertAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}",
                    Posts = new List<Post>()
                    {
                        new Post()
                        {
                            Content = Faker.Lorem.Paragraph(),
                            Title = Faker.Lorem.Sentence()
                        }
                    }
                }));

            foreach (var blog in expected)
            {
                var actual = await _blogRepository.GetAsync(blog.Id);
                Assert.AreEqual(actual.Id, blog.Id);
                Assert.AreEqual(actual.Url, blog.Url);
                Assert.AreEqual(actual.Posts.Count, blog.Posts.Count);
            }
        }

        [Test]
        public async Task ShouldAbleToUpdateBlog()
        {
            var blogs = new List<Blog>();

            for (var i = 0; i < 2; i++)
                blogs.Add(await _blogRepository.InsertAsync(new Blog
                {
                    Url = $"http://localhost/blog{i + 1}"
                }));

            var expected = blogs.Last();
            expected.Url = "changed";

            var actual = await _blogRepository.UpdateAsync(expected);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Url, actual.Url);

            actual = _context.Blogs.FromSqlInterpolated(
                $"SELECT * FROM blogging.blogs WHERE id = {expected.Id}").FirstOrDefault();

            Assert.NotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
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

            await _blogRepository.DeleteAsync(blogs.First());

            Assert.AreEqual(1, _context.Blogs.Count());

            blogs.RemoveAt(0);
            var expected = blogs.First();
            var actual = _context.Blogs.FromSqlRaw("select * from blogging.blogs").ToList().First();
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Url, actual.Url);
        }
    }
}