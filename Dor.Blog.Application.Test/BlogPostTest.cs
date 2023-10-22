

namespace Dor.Blog.Application.Test
{
    [TestFixture]
    public  class BlogPostTest
    {
        Mock<IUnitOfWork> _UnitOfWorkMock;
        Mock<IUserService> _UserServiceMock ;
        Mock<ILogger<BlogService>> _LoggerMock;

        [SetUp]
        public void SetUp() 
        {
            _UnitOfWorkMock = new Mock<IUnitOfWork>();
            _UserServiceMock = new Mock<IUserService>();
            _LoggerMock = new Mock<ILogger<BlogService>>();
            
        }

        /// <summary>
        ///  Trying to create a post and the user does not exist
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreatePost_UserDoesNotExist_SuccessfulFalse()
        {
            _UnitOfWorkMock.Setup(u => u.SaveAsync());
            _UnitOfWorkMock.Setup(u => u.BlogRepository.AddAsync(It.IsAny<BlogPost>()));

            var post = new BlogPost { UserId = "1" };
        
            _UserServiceMock.Setup(s => s.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value:null);

            var blogService = new BlogService(_UnitOfWorkMock.Object,_UserServiceMock.Object,_LoggerMock.Object);

            var res = await blogService.CreateAsync(post);
            Assert.NotNull(res);
            Assert.That(res.Successful, Is.False);
            Assert.That(res.DataResponse, Is.Null);

        }

        /// <summary>
        ///  Trying to create a post and the user exist then post created
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreatePost_UserExists_Successful()
        {

            _UnitOfWorkMock.Setup(u => u.SaveAsync());
            _UnitOfWorkMock.Setup(u => u.BlogRepository.AddAsync(It.IsAny<BlogPost>()));

            var post = new BlogPost { UserId = "1" };
            var user = new User { Id = "1" };

            _UserServiceMock.Setup(s => s.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            var blogService = new BlogService(_UnitOfWorkMock.Object, _UserServiceMock.Object, _LoggerMock.Object);

            var res = await blogService.CreateAsync(post);
            Assert.NotNull(res);
            Assert.That(res.Successful, Is.True);
            Assert.That(res.DataResponse,Is.Not.Null );            

        }

        /// <summary>
        /// get all posts 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllPosts_PostsExist_ReturnPostList()
        {
            var manyPosts = new List<BlogPost>();
            manyPosts.Add(new BlogPost { Id = 1 });
            manyPosts.Add(new BlogPost { Id = 2 });

            _UnitOfWorkMock.Setup(u => u.BlogRepository.GetAllAsync()).ReturnsAsync(manyPosts);

            var blogService = new BlogService(_UnitOfWorkMock.Object, _UserServiceMock.Object, _LoggerMock.Object);

            var res = await blogService.GetAsync();
            Assert.NotNull(res);
            Assert.That(res.Successful, Is.True);
            Assert.That(res.DataResponse, Is.Not.Null);
            Assert.That(res.DataResponse.Count(), Is.EqualTo(2));
        }

        /// <summary>
        /// get all posts and no post in DB
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllPosts_PostsDoNotExist_ReturnEmptyList()
        {
            var manyPosts = new List<BlogPost>();            

            _UnitOfWorkMock.Setup(u => u.BlogRepository.GetAllAsync()).ReturnsAsync(manyPosts);

            var blogService = new BlogService(_UnitOfWorkMock.Object, _UserServiceMock.Object, _LoggerMock.Object);

            var res = await blogService.GetAsync();
            Assert.NotNull(res);
            Assert.That(res.Successful, Is.True);
            Assert.That(res.DataResponse, Is.Not.Null);
            Assert.That(res.DataResponse.Count(), Is.EqualTo(0));
        }

        /// <summary>
        /// get post by id successfully
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetByIdPost_PostFound_ReturnPost()
        {
            var post = new BlogPost();

            var postFound = _UnitOfWorkMock.Setup(u => u.BlogRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

            var blogService = new BlogService(_UnitOfWorkMock.Object, _UserServiceMock.Object, _LoggerMock.Object);

            var res = await blogService.GetAsync();
            Assert.NotNull(res);
            Assert.That(res.Successful, Is.True);
            Assert.That(res.DataResponse, Is.Not.Null);
            
        }

        /// <summary>
        /// /// get post by id without success
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetByIdPost_PostNotFound_ReturnNullPost()
        {            

            var postFound = _UnitOfWorkMock.Setup(u => u.BlogRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value:null);

            var blogService = new BlogService(_UnitOfWorkMock.Object, _UserServiceMock.Object, _LoggerMock.Object);

            var res = await blogService.GetByIdAsync(It.IsAny<int>());

            Assert.NotNull(res);
            Assert.That(res.Successful, Is.True);
            Assert.That(res.DataResponse, Is.Null);

        }



    }
}
