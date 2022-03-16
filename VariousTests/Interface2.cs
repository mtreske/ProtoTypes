using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests
{
    partial interface IUser
    {
        Task<string> GetSurNameAsync();
        string GetFamilyName();
    }
}
