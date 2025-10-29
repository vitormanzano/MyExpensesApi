﻿namespace MyExpenses.Data.UnitOfWork
{
    public interface IUnitOfWork 
    {
        public Task<bool> CommitAsync();
    }
}
