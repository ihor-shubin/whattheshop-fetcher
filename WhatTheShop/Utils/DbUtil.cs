using Microsoft.EntityFrameworkCore;

namespace WhatTheShop.Utils;

public static class DbUtil
{
    public static void RemoveIfExists<TEntity>(this DbSet<TEntity> set, TEntity? entity) where TEntity : class
    {
        if (entity != null)
        {
            set.Remove(entity);
        }
    }
}