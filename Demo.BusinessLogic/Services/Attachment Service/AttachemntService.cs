using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.Attachment_Service
{
    public class AttachemntService : IAttachmentService
    {
        List<string> AllowedExtensions = [".jpg", ".png", ".jpeg", ".gif", ".pdf", ".doc", ".docx"];

        #region AttachemntService Steps
        // Check Extension
        // max file size 5 MB ==> 5 * 1024 * 1024 = 5242880 bytes
        // Folder Path => wwwroot/Attachments/{folderName}
        // Make the name of the file unique by adding a GUID to the name of the file
        // File Path => wwwroot/Attachments/{folderName}/{uniqueFileName}
        // Create file stream to copy the file to the server [Unmanged]
        // use stream using to dispose the stream after use it [Managed]
        // return the file Name to store it in the database 
        #endregion

        public string? Upload(IFormFile file, string folderName)
        {
            // Check Extension
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension)) return null;
            // check file size
            const long MaxSize = 5_242_880; // 5 MB
            if (file.Length > MaxSize || file.Length == 0) return null;
            // Folder Path 
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments", folderName);
            // make the name of the file unique
            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            // File Path
            var FilePath = Path.Combine(folderPath, FileName);
            // Create File Stream
            using FileStream fs = new FileStream(FilePath, FileMode.Create); // initialize the file stream
            // copy the file to the server
            file.CopyTo(fs); // copy the file to the file stream
            // return File Name
            return FileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
