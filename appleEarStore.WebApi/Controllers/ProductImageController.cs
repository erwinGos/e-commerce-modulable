using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.ProductDto;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Brands;
using System.IO;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        private readonly IProductService _productService;

        public ProductImageController(IProductImageService productImageService, IProductService productService)
        {
            _productImageService = productImageService;
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addimage")]
        public async Task<IActionResult> AddImagesToProduct([FromForm] List<IFormFile> imagesList, int productId)
        {
            try
            {
                ProductRead product = await _productService.FindOne(productId) ?? throw new Exception("Ce produit n'existe pas.");
                const int maxImageSize = 2 * 1024 * 1024;
                List<string> allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
                List<ProductImage> productImagesList = [];
                foreach(IFormFile image in imagesList)
                {
                    string fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        throw new Exception("Format invalide. Seulement les formats JPG et PNG sont acceptés.");
                    }

                    if (image.Length > maxImageSize)
                    {
                        throw new Exception("La taille du fichier excède la taille maximum autorisée.");
                    }
                    var memoryStream = new MemoryStream();
                    await image.CopyToAsync(memoryStream);
                    ProductImage productImage = new()
                    {
                        ProductId = productId,
                        Image = memoryStream.ToArray()
                    };
                    productImagesList.Add(productImage);
                }
                List<ProductImage> CreateProductImages = await _productImageService.CreateProductImages(productImagesList);
                return Ok(CreateProductImages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
