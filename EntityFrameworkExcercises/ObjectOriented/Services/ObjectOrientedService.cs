using Microsoft.EntityFrameworkCore;
using ObjectOriented.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOriented.Services
{
    public class ObjectOrientedService
    {
        private readonly ObjectOrientedDbContext _context;

        public ObjectOrientedService(ObjectOrientedDbContext context)
        {
            _context=context;
        }

        public async Task<Applicant> Init(int applicantId)
        {
            var applicant = await _context.Applicants
                                     .Include(a => a.Applications)
                                        .ThenInclude(ap => ap.Educations)
                                    .Include(a => a.Applications)
                                        .ThenInclude(ap => ap.Experiences)
                                    .SingleAsync(a => a.Id == applicantId);

            return applicant;

        }

        public async Task<int> Persist()
        {

            return await _context.SaveChangesAsync();
        }
    }
}
