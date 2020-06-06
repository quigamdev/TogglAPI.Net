using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggl;

namespace TogglAPI.NetStandard.Tests
{
    public class TogglApiTestKey
    {
        public const string apikey = "0d2aa15b691c68132f2b6016c6024dba";
    }
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test2()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                var a = new TogglAsync(TogglApiTestKey.apikey);
                var service = new Toggl.Toggl(TogglApiTestKey.apikey);
                var workspaces = await a.Workspace.List();
                var addedTag = await a.Tag.Add(new Tag() { Name = "Tag 1", WorkspaceId = workspaces.FirstOrDefault().Id });
                var list = await a.Tag.List();
            }).GetAwaiter().GetResult();
        }

    }
}
