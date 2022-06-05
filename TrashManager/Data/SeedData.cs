
using System;
using System.Collections.Generic;
using System.Linq;
using TrashManager.Models;

namespace TrashManager.Data
{
    public  class SeedData
    {
        private readonly ApplicationDbContext _db;
        public SeedData(ApplicationDbContext db)
        {
            _db = db;
        }
        public  void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {

                var users = new List<Appointment>()
               {
                new Appointment{  PhoneNumber = "17742088486"  ,UserName = "Tim" ,Email = "prenkufitim@gmail.com" , Turn = true},
                new Appointment{  PhoneNumber = "18575445491"  ,UserName = "Lazzo" ,Email = "prenkufitim@gmail.com" , Turn = false},
                new Appointment{  PhoneNumber = "19095321093"  ,UserName = "Alli" ,Email = "prenkufitim@gmail.com" , Turn = false},
                new Appointment { PhoneNumber = "16039884772" , UserName = "Alex" ,Email = "prenkufitim@gmail.com",Turn = false},
                };

                context.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}