using Codidact.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Linq;

namespace Services
{
	public interface ICommunityService
	{
		long GetID();
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

		public long GetID()
		{
			string url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
			string domain = new Uri(url).Host;
			return _db.Communities.Where(c => c.Url.Equals(domain)).Select(c => c.Id).Single();
		}
	}
}
