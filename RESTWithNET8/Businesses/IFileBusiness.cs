using RESTWithNET8.Data.ValueObjects;

namespace RESTWithNET8.Businesses
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);

        public Task<FileVO> SaveFileToDisk(IFormFile file);

        public Task<List<FileVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
