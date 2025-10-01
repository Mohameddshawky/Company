using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.AttachmentService
{
    
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtensions = [".png", ".jpg", ".jpeg"];

        public bool Delete(string imagename)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files","images", imagename);

            if (File.Exists(folderpath))
            {
                File.Delete(folderpath);
                return true;
            }
            return false;
        }

        public string? Upload(IFormFile file, string folderName)
        {
           var exe= Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(exe)) { 
                    return null;    
            }
            var folderpath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files",folderName);

            var filename = $"{Guid.NewGuid()}{file.FileName}";

            var filepath=Path.Combine(folderpath,filename);

            using FileStream f = new FileStream(filepath, FileMode.Create);

            file.CopyTo(f);
            return filename;
        }
    }
}
