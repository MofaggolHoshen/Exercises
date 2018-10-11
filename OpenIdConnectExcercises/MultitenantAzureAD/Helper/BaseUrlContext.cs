using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication5.Helper
{
    public class BaseUrlContext : IBaseUrlContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseUrlContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
           HttpContext = _httpContextAccessor.HttpContext;
        }
        public HttpContext HttpContext { get; set; }
    }

    public interface IBaseUrlContext
    {
         HttpContext HttpContext { get; set; }
    }
}
