using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Controllers
{
    public class SyncController : Controller
    {
        private readonly AppDbContext _context;

        public SyncController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncTasks(int educationId)
        {
            if (!await _context.Educations.AnyAsync(e => e.Id == educationId))
                return BadRequest("Education does not exist");

            var url = "https://uddannelser.stil.dk/uddannelser-api/api/uddannelser/18737/Regelgrundlag/28703/fagtabel";

            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            using var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;
            int addedCounter = 0;

            foreach (var speciale in root.GetProperty("specialer").EnumerateArray())
            {
                foreach (var fagtilknytning in speciale.GetProperty("fagtilknytninger").EnumerateArray())
                {
                    var fag = fagtilknytning.GetProperty("fag");

                    foreach (var maalpind in fag.GetProperty("maalpinde").EnumerateArray())
                    {
                        int id = maalpind.GetProperty("id").GetInt32();
                        string text = maalpind.GetProperty("tekst").GetString();

                        Console.WriteLine($"{id} - {text}");

                        bool exists = await _context.EducationalStandarts
                            .AnyAsync(m => m.ExternalId == id);

                        if (!exists)
                        {
                            _context.EducationalStandarts.Add(new EducationalStandarts
                            {
                                ExternalId = id,
                                Description = text,
                                EducationId = educationId,
                            });
                            addedCounter++;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok(addedCounter + " new entries added");
        }
    }
}
