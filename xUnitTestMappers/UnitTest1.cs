using AutoMapper;
using DomainModels;
using DTOs.RssFeed;
using Mappers;

namespace xUnitTestMappers
{
    public class UnitTest1
    {
        private readonly IMapper _mapper;

        public UnitTest1()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void RssFeed_ShouldMapToRssFeedDto()
        {
            // Arrange
            var rssFeed = new RssFeed
            {
                Id = 1,
                Source = "Example Source",
                SourceUrl = "http://example.com",
                FeedUrl = "http://example.com/feed",
                Title = "Example Title",
                Description = "Example Description",
                Link = "http://example.com/article",
                Author = "Author Name",
                PubDate = "2024-01-01"
            };

            // Act
            var dto = _mapper.Map<RssFeedDto>(rssFeed);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(rssFeed.Title, dto.Title);
            Assert.Equal(rssFeed.Source, dto.Source);
            Assert.Equal(rssFeed.SourceUrl, dto.SourceUrl);
            Assert.Equal(rssFeed.Title, dto.Title);
            Assert.Equal(rssFeed.Description, dto.Description);
            Assert.Equal(rssFeed.Link, dto.Link);
            Assert.Equal(rssFeed.FeedUrl, dto.FeedUrl);
            Assert.Equal(rssFeed.Author, dto.Author);
            Assert.Equal(rssFeed.PubDate, dto.PubDate);
            // Add more assertions as needed
        }
    }
}