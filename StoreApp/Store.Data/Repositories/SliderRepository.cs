using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    public class SliderRepository:Repository<Slider>,ISliderRepository
    {
        public SliderRepository(StoreDbContext context):base(context)
        {

        }


    }
}
