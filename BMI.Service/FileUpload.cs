using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using BMI.Service.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BMI.Service
{
    public class FileUpload : IFileUpload
    {
        public IEnumerable<BmiModel> UploadFile(IFormFile file)
        {
            if(file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            List<BmiModel> records;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader))
                {
                    records = csv.GetRecords<BmiModel>().ToList();

                }
            }

            return records;
        }
    }
}
