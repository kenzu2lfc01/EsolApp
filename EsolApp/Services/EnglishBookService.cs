using Google.Cloud.Translation.V2;
using System.Collections.Generic;

namespace EsolApp.Services
{
    public class EnglishBookService : IEnglishBookService
    {
       public string AnyMethod(string word)
        {
            TranslationClient client = TranslationClient.Create();
            TranslationResult result = client.TranslateText(word, LanguageCodes.Vietnamese);
            return result.TranslatedText;
        }
    }
}