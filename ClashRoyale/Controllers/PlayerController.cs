using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClashRoyale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ClanParticipationContext _context;

        public PlayerController(ClanParticipationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: /api/player
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var players = await _context.ClanMembers
                .Include(c => c.WarParticipations)
                .Include(c => c.DonationRecords)
                .AsNoTracking()
                .ToListAsync();

            return players;
        }
    }
}