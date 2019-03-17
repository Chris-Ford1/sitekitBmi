using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace BMI.Service.Commands
{
    public class ImportBmiRecordsCommand : IRequest<bool>
    {
        public IFormFile File { get; }

        public ImportBmiRecordsCommand(IFormFile file)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }
    }
}
