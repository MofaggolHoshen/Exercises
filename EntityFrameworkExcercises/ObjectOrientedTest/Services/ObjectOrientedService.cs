using Microsoft.EntityFrameworkCore;
using ObjectOrientedTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedTest.Services
{
    internal class ObjectOrientedService
    {
        private readonly ObjectOrientedDbContext _context;

        public ObjectOrientedService(ObjectOrientedDbContext context)
        {
            _context=context;
        }

        internal async  Task<List<Applicant>> Init(int applicantId)
        {
            var applicant = await _context.Applicants
                                     .Include(a=> a.Applications)
                                        .ThenInclude(ap=> ap.Educations)
                                    .Include(a => a.Applications)
                                        .ThenInclude(ap=> ap.Experiences)
                                    .Where(a=> a.Id == applicantId)
                                    .ToListAsync();

            return applicant;

        }

        internal async Task<int> Persist()
        {

            return await _context.SaveChangesAsync();
        }
    }
}
