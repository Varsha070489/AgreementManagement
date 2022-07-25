using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SoftwareManagement.Web.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private ILogger<T> _loggerInstance;
        private IViewRenderService _viewRenderInstance;
        private IMapper _mapperInstance;
        private INotyfService _notifyInstance;
        private IHTTPClientHelper _httpClientInstance;

        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IHTTPClientHelper _httpClient => _httpClientInstance ??= HttpContext.RequestServices.GetService<IHTTPClientHelper>();
        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}
