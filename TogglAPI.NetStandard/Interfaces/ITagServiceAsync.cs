 using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface ITagServiceAsync
    {
        Task<Tag> Add(Tag tag);
        Task<List<Tag>> ForWorkspace(int id);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md
        /// </summary>
        /// <returns></returns>
        Task<List<Tag>> List();
    }
}