namespace ToDoList.Interfaces
{
    public interface IAzureBlobService
    {
        public Task<string> UploadFileAsync(string containerName, IFormFile file, string fileName);
    }
}
