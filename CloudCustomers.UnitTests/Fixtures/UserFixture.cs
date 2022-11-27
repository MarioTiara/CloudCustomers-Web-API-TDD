using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public class UserFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new()
                {
                    Name="Mario",
                    Email="mario@gmail.com",
                    Address= new Address()
                    {
                        Street="15 Flamboyan",
                        City="Bnegkulu",
                        ZipCode="5374"
                    }
                   
                },
                 new()
                {
                    Name="Pratama",
                    Email="pratama@gmail.com",
                    Address= new Address()
                    {
                        Street="16 Flamboyan",
                        City="Bnegkulu Tengah",
                        ZipCode="5375"
                    },

                },
                   new()
                {
                    Name="Ramayana",
                    Email="Ramayana@gmail.com",
                    Address= new Address()
                    {
                        Street="17 Flamboyan",
                        City="Bnegkulu Utara",
                        ZipCode="5377"
                    }

                }
            };
    }
}
