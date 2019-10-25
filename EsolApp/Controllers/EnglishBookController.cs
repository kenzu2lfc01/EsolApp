using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsolApp.Services;
using EsolApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishBookController : ControllerBase
    {
        IEnglishBookService _englishBookService;
        public EnglishBookController(IEnglishBookService englishBookService)
        {
            _englishBookService = englishBookService;
        }
        [HttpGet]
        [Route("getword")]
        public string GetEnglishBook(string word)
        {
            return _englishBookService.AnyMethod(word);
        }
    }
}