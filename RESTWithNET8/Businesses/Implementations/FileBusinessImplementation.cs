using RESTWithNET8.Data.ValueObjects;

namespace RESTWithNET8.Businesses.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;

        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\Uploads\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileVO> SaveFileToDisk(IFormFile file)
        {
            FileVO fileVO = new FileVO();
            var fileType = Path.GetExtension(file.FileName).ToLower();

            if (file != null && file.Length > 0 &&
               (fileType == ".pdf" || fileType == ".png" ||
                fileType == ".jpg" || fileType == ".jpeg"))
            {
                fileVO.DocumentType = fileType;
                fileVO.DocumentName = Path.GetFileName(file.FileName);
                fileVO.DocumentUrl = Path.Combine(_context.HttpContext.Request.Host + "/api/file/v1/" + fileVO.DocumentName);
                var destination = Path.Combine(_basePath, "", fileVO.DocumentName);

                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }

            return fileVO;
        }

        public async Task<List<FileVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileVO> fileVOs = new List<FileVO>();

            foreach (var file in files)
            {
                fileVOs.Add(await SaveFileToDisk(file));
            }

            return fileVOs;
        }
    }
}
