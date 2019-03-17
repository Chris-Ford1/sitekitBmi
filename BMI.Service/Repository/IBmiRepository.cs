using BMI.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMI.Service.Repository
{
    public interface IBmiRepository
    {
        Task<bool> InsertRecords(IEnumerable<BmiModel> records);

        Task<IEnumerable<BmiModel>> GetRecords();
    }
}
