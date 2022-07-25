using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Abstractions
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
