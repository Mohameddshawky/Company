using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.AttachmentService
{
    internal class AttachmentService : IAttachmentService
    {
        public bool Delete(string folderPath)
        {
            throw new NotImplementedException();
        }

        public string? Upload(IFormFile file, string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
