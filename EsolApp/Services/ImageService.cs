using AutoMapper;
using EsolApp.Data.Model;
using EsolApp.Data.Repositories.Images;
using EsolApp.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace EsolApp.Services
{
    public class ImageService : IImageService
    {
        private IImageRepository _imageRepository;
        private IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public bool Add(Images image)
        {
            try
            {
                _imageRepository.Add(image);
                _imageRepository.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int imageId)
        {
            var image = _imageRepository.FindById(imageId);
            if (image == null)
            {
                return false;
            }
            try
            {
                _imageRepository.Remove(image);
                _imageRepository.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Images> GetImage()
        {
            return _imageRepository.FindAll().ToList();
        }

        public List<ImageViewModel> GetImageByTodoId(int todoId)
        {
            List<Images> images = _imageRepository.FindAll().Where(x => x.Todo.Id == todoId).ToList();
            List<ImageViewModel> imageViewModels = new List<ImageViewModel>();
            foreach(var item in images)
            {
                imageViewModels.Add(_mapper.Map<Images, ImageViewModel>(item));
            }
            var temp = imageViewModels;
            return imageViewModels;
        }

        public void MoveFile(IFormFile file, string webRoot)
        {
            string folderName = "Upload";
            string newPath = Path.Combine(webRoot, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        public string ConvertPdfToBase64(string FilePath)
        {
            byte[] pdfBytes = File.ReadAllBytes(FilePath);
            string pdfBase64 = Convert.ToBase64String(pdfBytes);
            return pdfBase64;
        }
    }
}