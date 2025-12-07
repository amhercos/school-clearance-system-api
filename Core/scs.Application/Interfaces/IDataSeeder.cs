using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAllAsync ();   
    }
}
