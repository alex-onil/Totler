using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trade_MVC6.Models._1C;
using Trade_MVC6.Services._1С.Interfaces;

namespace Trade_MVC6.Services._1С.Providers
{
    public class ProviderUsers1C : IUsers1C
    {
        public Task<ICollection<User1C>> QueryAsync()
        {
            return Task.Run(() =>
            {
                return (ICollection<User1C>) new List<User1C>
                {
                    new User1C { Id = "asd", Name = "Копыта", NameFull = "ООО рога и копыта" },
                    new User1C { Id = "sdfs", Name = "Корыто", NameFull = "ОАО Корыто энтерпрайз" },
                    new User1C { Id = "fas", Name = "Москва", NameFull = "СП Москва Энтерпрайз" },
                    new User1C { Id = "sdd", Name = "Колени", NameFull = "РУП Колени Инкорпорейтед" },
                    new User1C { Id = "fgh", Name = "Лезгин", NameFull = "ИП Грузинские парни" }
                };
            });
        }

        public Task<User1C> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
