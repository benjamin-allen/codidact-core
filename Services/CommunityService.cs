using Codidact.Domain.Entities;
using Codidact.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Linq;

namespace Services
{
	public interface ICommunityService
	{
		Community GetCommunity();
	}

	public class CommunityService : ICommunityService
	{
		private readonly ApplicationDbContext _db;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CommunityService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
		{
			_db = db;
			_httpContextAccessor = httpContextAccessor;
		}

		/// <summary>
		/// Gets the community from the current URL.
		/// </summary>
		/// <returns></returns>
		public Community GetCommunity()
		{
			// Use the URL to look up the community. Assumes uniqueness.
			string url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
			string domain = new Uri(url).Host;
			return _db.Communities.Where(c => c.Url.Equals(domain)).Single();
		}
	}
}
