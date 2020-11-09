using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.NFE.Interfaces
{
    public interface INfeDownloadListService
    {
        Task<byte[]> DownloadList();
    }
}
