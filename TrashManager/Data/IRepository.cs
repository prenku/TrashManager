using TrashManager.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TrashManager.Data
{
    public interface IRepository
    {
        public bool CheckForTurn(Appointment appointment);
        public IdentityUser UpdateNextTurn(Appointment appointment);
        public Appointment GetCurrentTurn();
        public IdentityUser CheckNextTurn(Appointment appointment);
    }

    public class Repository : IRepository
    {
        private ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CheckForTurn(Appointment appointment)
        {
            return _context.Entry(appointment).Entity.Turn;
        }

        public Appointment GetCurrentTurn()

        {

            var current = _context.Appointments.Select(app => app).Where(t => t.Turn == true).FirstOrDefault();
            return current;
        }

        public IdentityUser UpdateNextTurn(Appointment appointment)
        {
            var users = _context.Appointments.ToList();
            var next = users.SkipWhile(x => x != appointment).Skip(1).DefaultIfEmpty(users[0]).FirstOrDefault();

            next.Turn = true;
            appointment.Turn = false;

            _context.Appointments.UpdateRange(appointment, next);
            _context.SaveChanges();

            return next;
        }
        public IdentityUser CheckNextTurn(Appointment appointment)
        {
            var users = _context.Appointments.ToList();
            var next = users.SkipWhile(x => x != appointment).Skip(1).DefaultIfEmpty(users[0]).FirstOrDefault();

            return next;

        }
    }
}
