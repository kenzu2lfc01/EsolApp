using EsolApp.Data.Model;
using EsolApp.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsolApp.Services
{
    public interface IImageService
    {
        List<Images> GetImage();
        bool Add(Images image);
        bool Delete(int imageId);
        void MoveFile(IFormFile file, string webRoot);
        string ConvertPdfToBase64(string FilePath);
        List<ImageViewModel> GetImageByTodoId(int todoId);

    }
}
