using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IFileStorageService
    {
        Task<List<string>> SaveImagesAsync(List<IFormFile> files);
        Task DeleteImageAsync(string imageUrl);
    }
}
