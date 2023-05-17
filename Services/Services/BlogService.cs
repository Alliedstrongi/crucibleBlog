using crucibleBlog.Data;
using crucibleBlog.Helpers;
using crucibleBlog.Models;
using crucibleBlog.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace crucibleBlog.Services
{
	public class BlogService : IBlogService
	{
		private readonly ApplicationDbContext _context;
		public BlogService()
		{
		}

		public BlogService(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task AddTagsToBlogPostAsync(IEnumerable<int>? tagIds, int? blogPostId)
		{
			throw new NotImplementedException();
		}

		public async Task AddTagsToBlogPostAsync(string tagNames, int? blogPostId)
		{
			try
			{
				BlogPost? blogPost = await _context.BlogPost.FirstOrDefaultAsync(b => b.Id == blogPostId);
				if (blogPost == null) return;

				List<string> tags = tagNames.Split(',').ToList();

				foreach (string? tagName in tags)
				{
					if (string.IsNullOrWhiteSpace(tagName)) continue;

					Tag? tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name!.Trim() == tagName.Trim().ToLower());

					if (tag == null)
					{
						tag = new Tag()
						{
							Name = tagName.Trim(),
						};

						_context.Tags.Add(tag);
					}
					blogPost.Tags.Add(tag);

				}
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync()
		{
			try
			{
				IEnumerable<Category> categories = await _context.Categories.ToListAsync();

				return categories;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public Task<IEnumerable<BlogPost>> GetPopularBlogPostsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost>> GetPopularBlogPostsAsync(int? count)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost>> GetRecentBlogPostAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost>> GetRecentBlogPostAsync(int? count)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync(int? count)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsTagOnBlogPostAsync(int? tagId, int? blogPostId)
		{
			throw new NotImplementedException();
		}

		public Task RemoveAllBlogPostTagsAsync(int? blogPostId)
		{
			throw new NotImplementedException();
		}

		public Task RemoveAllBlogPostTagsAsync(int blogPostId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BlogPost> SearchBlogPosts(string? searchString)
		{
			try
			{
				IEnumerable<BlogPost> blogPosts = new List<BlogPost>();

				if (string.IsNullOrEmpty(searchString))
				{
					return blogPosts;
				}
				else
				{
					searchString = searchString.Trim().ToLower();

					blogPosts = _context.BlogPosts.Where(b => b.Title!.ToLower().Contains(searchString) ||
														 b.Abstract!.ToLower().Contains(searchString) ||
														 b.Content!.ToLower().Contains(searchString) ||
														 b.Category.Name!.ToLower().Contains(searchString) ||
														 b.Comments.Any(c => c.Body.ToLower().Contains(searchString) ||
																		c.Author!.FirstName!.ToLower().Contains(searchString) ||
																		c.Author!.LastName!.ToLower().Contains(searchString) ||
														 b.Tags.Any(t => t.Name!.ToLower().Contains(searchString)))
													.Inclide(b => b.Comments)
													 .ThenInclude(c => c.Author)
													.Include(b => b.Category)
													.Include(b => b.Tags)
													.Where(b => b.IsDeleted == false && b.IsPublished == true)
													.AsNoTracking()
													.OrderByDescending(b=b.CreatedDate)
													.AsEnumerable();

					return blogPosts;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		public Task<bool> ValidateSlugAsync(string? title, int? blogPostId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ValidSlugAsync(string? title, int? blogPostId)
		{
			try
			{
				string? newSlug = StringHelper.BlogPostSlug(title);

				if (blogPostId == null)
				{
					//Creating BlogPost
					return !await _context.BlogPost.AnyAsync(b => b.Slug == newSlug);
				}
				else
				{
					//Editing BlogPost
					BlogPost? blogPost = await _context.BlogPost.AsNoTracking().FirstOrDefaultAsync(b => b.Id == blogPostId);

					string? oldSlug = blogPost?.Slug;

					if (!string.Equals(oldSlug, newSlug))
					{
						return !await _context.BlogPost.AnyAsync(b => b.Id != blogPost!.Id && b.Slug == newSlug);
					}
				}
				return true;
			}
			catch (Exception)
			{

				throw;
			}
			throw new NotImplementedException();
		}

		IEnumerable<BlogPost> IBlogService.SearchBlogPosts(string? searchString)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Tag>> GetTagsAsync()
		{

		}
			;
	}
	
}