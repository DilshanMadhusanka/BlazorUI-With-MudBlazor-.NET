using Microsoft.AspNetCore.Components.Forms;

namespace MudBlazorUI.Auth_Service.Services
{
    public interface IDatabaseService
    {
        Task<string> BackupPostgreSql();
        Task<string> BackupSqlServer();
        Task DownloadBackup(string fileName);
        Task RestorePostgreSqlBackup(IBrowserFile file);
        Task RestoreSqlServerBackup(IBrowserFile file);
        Task UploadPostgreSqlBackup(IBrowserFile file);
        Task UploadSqlServerBackup(IBrowserFile file);
    }
}