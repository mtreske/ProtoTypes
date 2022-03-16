using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VariousTests
{
    class PartialInterfacesTests
    {
        [Test]
        public void Interface_resolves()
        {
            IUser user = new User();
        }
    }

    class User : IUser
    {
        public string GetFamilyName()
        {
            throw new NotImplementedException();
        }

        public string GetSurName()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSurNameAsync()
        {
            throw new NotImplementedException();
        }

        
    }

   

    

}
