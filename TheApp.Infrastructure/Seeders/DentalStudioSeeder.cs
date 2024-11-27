﻿using TheApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Infrastructure.Seeders
{
    public class DentalStudioSeeder
    {
        private readonly TheAppDbContext _dbContext;

        public DentalStudioSeeder(TheAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (!await _dbContext.Database.CanConnectAsync())
            {
                return;
            }
            if (_dbContext.DentalStudios.Any())
            {
                return;
            }

            var exampleDentalStudio = new Domain.Entities.DentalStudio()
            {
                Name = "No More Candy",
                Description = "No imagination",
                ContactDetails = new()
                {
                    City = "Katowice",
                    Street = "Zabawna 1",
                    PostalCode = "30-000",
                    PhoneNumber = "+48666555444",
                    Link = "https://9gag.com/gag/ae95nVQ"
                }
            };
            exampleDentalStudio.EncodeName();

            _dbContext.DentalStudios.Add(exampleDentalStudio);
            await _dbContext.SaveChangesAsync();
        }
    }
}