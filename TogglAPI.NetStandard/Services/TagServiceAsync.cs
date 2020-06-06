using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Toggl.Interfaces;

namespace Toggl.Services
{
    public class TagServiceAsync : ITagServiceAsync
    {
        private readonly string TagsUrl = ApiRoutes.Tag.TagsUrl;
        private IApiServiceAsync ToggleSrv { get; set; }


        public TagServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {
        }

        public TagServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }


        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_tags
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tag>> List()
        {
            var results = new List<Tag>();
            var workspaces = (await ToggleSrv.Get(ApiRoutes.Workspace.ListWorkspaceUrl)).GetData<List<Workspace>>();
            foreach (var e in workspaces)
            {
                var tags = await ForWorkspace(e.Id.Value);
                results.AddRange(tags);
            }
            return results;
        }

        public async Task<List<Tag>> ForWorkspace(int id)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceTagsUrl, id);
            return (await ToggleSrv.Get(url)).GetData<List<Tag>>();
        }

        public async Task<Tag> Add(Tag tag)
        {
            if (tag.Name == null) throw new InvalidDataException("Name is required");
            if(tag.WorkspaceId == null) throw new InvalidDataException("WorkspaceId is required");
            return (await ToggleSrv.Post(TagsUrl, tag.ToJson())).GetData<Tag>();
        }
    }
}
