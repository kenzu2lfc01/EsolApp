using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EsolApp.Data.Model;
using EsolApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _imageService;
        private IHostingEnvironment _hostingEnvironment;
        private ITodoService _todoService;

        public ImageController(IImageService imageService, IHostingEnvironment hostingEnvironment, ITodoService todoService)
        {
            _imageService = imageService;
            _hostingEnvironment = hostingEnvironment;
            _todoService = todoService;
        }
        [HttpGet]
        public ActionResult<List<Images>> GetImages()
        {
           return Ok(_imageService.GetImage());
        }
        [HttpPost]
        public ActionResult PostImages()
        {
            var image = Request.Form.Files[0];
            int todoId = Convert.ToInt32(Request.Form["todoid"]);
            var todoItem =_todoService.GetAllTodos().Where(x => x.Id == todoId).LastOrDefault();
            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
            string filePath = @".\wwwroot\Upload\";
            
            FileInfo fileInfo = new FileInfo(Path.Combine(filePath, fileName));
            _imageService.MoveFile(image, webRootPath);
            string Base64 = _imageService.ConvertPdfToBase64(fileInfo.FullName);
            Images images = new Images()
            {
                Base64Image = Base64,
                Todo = todoItem
            };
            if (_imageService.Add(images) == false)
                return BadRequest("Some thing wrong");
            return Ok("Add success");

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult DeleteImage(int id)
        {
            if(_imageService.Delete(id) == false)
               return BadRequest("Some thing wrong");
            return Ok("Delete success");
        }
    }
}