using ClashRoyale.Database;
using ClashRoyale.Models;
using System;
using System.Linq;

namespace ClashRoyale.DatabaseAccessObjects
{
    public class ClanMemberDAO
    {
        WarParticipationContext db;

        public ClanMemberDAO(WarParticipationContext context)
        {
            db = context;
        }

        public void Create()
        {
            Console.WriteLine("Creating a new clan member");
            db.Add(new ClanMember { Tag = "Temp Tag", Name = "Temp Name", Donations = 0, DonationsReceived = 0 });
            db.SaveChanges();
        }

        public ClanMember Read()
        {
            Console.WriteLine("Querying for a clan member");
            return db.ClanMembers.First();
        }

        public void Update()
        {
            Console.WriteLine("Updating a clan member");
            ClanMember member = Read();
            member.Name = "Updated Name";
            db.SaveChanges();
        }

        public void Delete()
        {
            Console.WriteLine("Deleting a clan member");
            ClanMember member = Read();
            db.Remove(member);
            db.SaveChanges();
        }
    }
}
