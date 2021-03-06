﻿using System;
using DemoDAL.Context;
using Microsoft.EntityFrameworkCore;
using DemoDAL.Repositories;


namespace DemoDAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; internal set; }

        public IIngredientRepository IngredientRepository { get; internal set; }



        private EASVContext context;

        private static DbContextOptions<EASVContext> optionsStatic;

        public UnitOfWork(DbOptions opt)
        {
            /*if (opt.Environment == "Development" && String.IsNullOrEmpty(opt.ConnectionString))
            {*/
                optionsStatic = new DbContextOptionsBuilder<EASVContext>()
                   .UseInMemoryDatabase("TheDB")
                   .Options;
                context = new EASVContext(optionsStatic);
           /* }
            else
            {
                var options = new DbContextOptionsBuilder<EASVContext>()
                .UseSqlServer(opt.ConnectionString)
                    .Options;
                context = new EASVContext(options);
            }*/
            /*
            //Ensure sql queries are created
            context.Database.EnsureCreated();
            */
            ProductRepository = new ProductRepository(context);
            IngredientRepository = new IngredientRepository(context);

        }

        public int Complete()
        {
            //The number of objects written to the underlying database.
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
