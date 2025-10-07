using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.Attachment_Service
{
    public interface IAttachmentService
    {
        //Upload Attachment
        public string? Upload(IFormFile file, string folderName); 
        //Delete Attachment
        public bool Delete(string filePath);
    }
}
