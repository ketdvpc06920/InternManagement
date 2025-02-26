using InternManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/interns")]
    public class InternController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InternController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-interns")]
        public async Task<IActionResult> GetInterns()
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid or missing User ID in token.");
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return Unauthorized();

            var allowedColumns = await _context.AllowAccesses
                .Where(a => a.RoleId == user.RoleId && a.TableName == "Intern")
                .Select(a => a.AccessProperties)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(allowedColumns))
            {
                return Forbid();
            }

            var columnList = allowedColumns.Split(',').Select(c => c.Trim()).ToList();

            var internsQuery = _context.Interns.AsQueryable();

            var interns = await internsQuery
                .Select(i => new Dictionary<string, object>
                {
                { "Id", columnList.Contains("Id") ? i.Id : null },
                { "InternName", columnList.Contains("InternName") ? i.InternName : null },
                { "InternAddress", columnList.Contains("InternAddress") ? i.InternAddress : null },
                { "ImageData", columnList.Contains("ImageData") ? i.ImageData : null },
                { "DateOfBirth", columnList.Contains("DateOfBirth") ? i.DateOfBirth : null },
                { "InternMail", columnList.Contains("InternMail") ? i.InternMail : null },
                { "InternMailReplace", columnList.Contains("InternMailReplace") ? i.InternMailReplace : null },
                { "University", columnList.Contains("University") ? i.University : null },
                { "CitizenIdentification", columnList.Contains("CitizenIdentification") ? i.CitizenIdentification : null },
                { "CitizenIdentificationDate", columnList.Contains("CitizenIdentificationDate") ? i.CitizenIdentificationDate : null },
                { "Major", columnList.Contains("Major") ? i.Major : null },
                { "Internable", columnList.Contains("Internable") ? i.Internable : null },
                { "FullTime", columnList.Contains("FullTime") ? i.FullTime : null },
                { "Cvfile", columnList.Contains("Cvfile") ? i.Cvfile : null },
                { "InternSpecialized", columnList.Contains("InternSpecialized") ? i.InternSpecialized : null },
                { "TelephoneNum", columnList.Contains("TelephoneNum") ? i.TelephoneNum : null },
                { "InternStatus", columnList.Contains("InternStatus") ? i.InternStatus : null },
                { "RegisteredDate", columnList.Contains("RegisteredDate") ? i.RegisteredDate : null },
                { "HowToKnowAlta", columnList.Contains("HowToKnowAlta") ? i.HowToKnowAlta : null },
                { "InternPassword", columnList.Contains("InternPassword") ? i.InternPassword : null },
                { "ForeignLanguage", columnList.Contains("ForeignLanguage") ? i.ForeignLanguage : null },
                { "YearOfExperiences", columnList.Contains("YearOfExperiences") ? i.YearOfExperiences : null },
                { "PasswordStatus", columnList.Contains("PasswordStatus") ? i.PasswordStatus : null },
                { "ReadyToWork", columnList.Contains("ReadyToWork") ? i.ReadyToWork : null },
                { "InternEnabled", columnList.Contains("InternEnabled") ? i.InternEnabled : null },
                { "EntranceTest", columnList.Contains("EntranceTest") ? i.EntranceTest : null },
                { "Introduction", columnList.Contains("Introduction") ? i.Introduction : null },
                { "Note", columnList.Contains("Note") ? i.Note : null },
                { "LinkProduct", columnList.Contains("LinkProduct") ? i.LinkProduct : null },
                { "JobFields", columnList.Contains("JobFields") ? i.JobFields : null },
                { "HiddenToEnterprise", columnList.Contains("HiddenToEnterprise") ? i.HiddenToEnterprise : null }
                })
                .ToListAsync();

            var cleanedInterns = interns.Select(dict =>
                dict.Where(pair => pair.Value != null)
                    .ToDictionary(pair => pair.Key, pair => pair.Value)
            ).ToList();

            return Ok(cleanedInterns);
        }


    }
}
