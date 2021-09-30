using System.Threading.Tasks;

namespace BLL.NFE.Interfaces
{
    public interface INfeDownloadListService
    {
        Task<byte[]> DownloadList();
    }
}
