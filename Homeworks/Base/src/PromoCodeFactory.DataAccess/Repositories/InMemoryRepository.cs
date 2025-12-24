using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        /// <summary>
        /// Добавляет новый элемент в репозиторий.
        /// </summary>
        /// <param name="item">Новый элемент</param>
        /// <returns></returns>
        public async Task CreateAsync(T item)
        {
            Data.Add(item);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Обновляет существующий элемент в репозитории.
        /// </summary>
        /// <param name="item">Обновленный элемент</param>
        /// <returns></returns>
        public async Task UpdateAsync(T item)
        {
            var existedItem = Data.FirstOrDefault(i => i.Id == item.Id);

            if (existedItem != null)
                existedItem = item;

            await Task.CompletedTask;
        }

        /// <summary>
        /// Удаляет существующий элемент из репозитория по id.
        /// </summary>
        /// <param name="id">Идентификатор существующего элемента</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var existedItem = Data.FirstOrDefault(i => i.Id == id);

            if (existedItem != null)
                Data.Remove(existedItem);

            await Task.CompletedTask;
        }
    }
}