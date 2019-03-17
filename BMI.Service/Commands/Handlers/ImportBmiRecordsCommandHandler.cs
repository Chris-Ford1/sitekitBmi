using BMI.Service.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BMI.Service.Commands.Handlers
{
    public class ImportBmiRecordsCommandHandler : IRequestHandler<ImportBmiRecordsCommand, bool>
    {
        private readonly IFileUpload _fileUpload;
        private readonly IBmiRepository _bmiRepository;

        public ImportBmiRecordsCommandHandler(IFileUpload fileUpload, IBmiRepository bmiRepository)
        {
            _bmiRepository = bmiRepository ?? throw new ArgumentNullException(nameof(bmiRepository));
            _fileUpload = fileUpload ?? throw new ArgumentNullException(nameof(fileUpload));
        }

        public async Task<bool> Handle(ImportBmiRecordsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var bmiRecords = _fileUpload.UploadFile(request.File);

            return await _bmiRepository.InsertRecords(bmiRecords);

        }
    }
}
