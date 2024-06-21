using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace MudBlazorUI.Auth_Service.Services
{


    public class DatabaseService : IDatabaseService
    {
        private readonly IHttpClientFactory _factory;

        public DatabaseService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<string> BackupSqlServer()
        {
            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-api/database/backup-sqlserver", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> BackupPostgreSql()
        {
            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-api/database/backup-postgresql", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task DownloadBackup(string fileName)
        {
            var response = await _factory.CreateClient("ServerApi").GetAsync($"ApiGateWay/Auth-api/database/download?fileName={fileName}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var contentDisposition = response.Content.Headers.ContentDisposition;
            var fileNameFromResponse = contentDisposition?.FileNameStar ?? contentDisposition?.FileName ?? "backup.bak";

            using (var fileStream = new FileStream(fileNameFromResponse, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        public async Task UploadSqlServerBackup(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-api/database/upload", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UploadPostgreSqlBackup(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-api/database/upload", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RestoreSqlServerBackup(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-api/database/restore-sqlserver", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RestorePostgreSqlBackup(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _factory.CreateClient("ServerApi").PostAsync("ApiGateWay/Auth-apie/database/restore-postgresql", content);
            response.EnsureSuccessStatusCode();
        }
    }


}
