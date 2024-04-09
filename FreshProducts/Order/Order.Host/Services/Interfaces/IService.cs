﻿using Order.Host.Data.Entities;

namespace Order.Host.Services.Interfaces
{
	public interface IService<T>
	{
		Task<int?> AddAsync(T entity);
		Task<int?> UpdateAsync(int id, T entity);
		Task<string?> DeleteAsync(int id);
	}
}
