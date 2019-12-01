﻿using System.Threading.Tasks;
using Uploadcare.DTO;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Clients
{
    internal class ProjectsClient : IProjectsClient
    {
        private readonly RequestHelper _requestHelper;

        public ProjectsClient(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<UploadcareProject> GetAsync()
        {
            var url = Urls.ApiProject;

            var result = await _requestHelper.Get<ProjectData>(url, default);

            return new UploadcareProject(result);
        }
    }
}
