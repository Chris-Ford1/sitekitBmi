using BMI.Service.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BMI.Service
{
    public interface IFileUpload
    {
        IEnumerable<BmiModel> UploadFile(IFormFile file);
    }
}
