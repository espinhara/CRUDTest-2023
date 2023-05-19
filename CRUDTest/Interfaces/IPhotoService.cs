using CloudinaryDotNet.Actions;

namespace CRUDTest.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> RemovePhotoAsync(string publicId);

    }
}
