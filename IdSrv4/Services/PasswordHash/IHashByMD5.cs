using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdSrv4.Services.PasswordHash
{
    public interface IHashByMD5
    {
        string GetMd5Hash(string input);
        bool VerifyMd5Hash(string input, string hash);
    }
}
