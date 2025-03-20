using Core.Service;
using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _imagesFolder = "img/imoveis";

        public FileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<string>> SaveImagesAsync(List<IFormFile> files)
        {
            List<string> urls = new List<string>();

            if (files == null || files.Count == 0)
                return urls;

            // Garantir que o diretório existe
            string uploadPath = Path.Combine(_environment.WebRootPath, _imagesFolder);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // Gerar nome único para o arquivo
                    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    string filePath = Path.Combine(uploadPath, fileName);

                    // Salvar o arquivo
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Adicionar URL relativa à lista
                    string fileUrl = $"/{_imagesFolder}/{fileName}";
                    urls.Add(fileUrl);
                }
            }

            return urls;
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return Task.CompletedTask;

            try
            {
                // Remover a barra inicial se existir
                if (imageUrl.StartsWith("/"))
                    imageUrl = imageUrl.Substring(1);

                string fullPath = Path.Combine(_environment.WebRootPath, imageUrl);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception)
            {
                // Log de erro seria ideal aqui
            }

            return Task.CompletedTask;
        }
    }
}

