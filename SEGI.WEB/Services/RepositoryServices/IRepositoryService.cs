using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Services.RepositoryServices
{
    public interface IRepositoryService
    {
        Guid IntToGuid(int value);
        int GuidToInt(Guid guid);
        Task TruncateTableAsync(string tableName);
    }

}
