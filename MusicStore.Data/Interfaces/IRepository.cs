﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task Create(T item);
        Task Update(T item);
        Task Remove(int id);
        IQueryable<T> GetAll();
    }
}