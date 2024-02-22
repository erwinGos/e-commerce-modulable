using Data.DTO.Brands;
using Microsoft.AspNetCore.Http;


namespace Data.Managers
{
    public class ImageManager
    {
        public async static Task<byte[]> ConvertImage(IFormFile image, List<string> allowedExtensions, int maxSizeInMb)
        {
            try
            {
                int maxImageSize = maxSizeInMb * 1024 * 1024;
                var memoryStream = new MemoryStream();

                await image.CopyToAsync(memoryStream);
                string fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new Exception("Format invalide. Seulement les formats JPG et PNG sont acceptés.");
                }

                if (image.Length > maxImageSize)
                {
                    throw new Exception("La taille du fichier excède la taille maximum autorisée.");
                }

                return memoryStream.ToArray();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
