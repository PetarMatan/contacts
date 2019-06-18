using manageContacts.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace manageContacts.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<MobileNumber> mobileNumbers { get; set; }
        public DbSet<SimContact> simContacts { get; set; }
        public DbSet<ContactTag> contactTags { get; set; }
        public DbSet<PhoneContact> phoneContacts { get; set; }

        public DbSet<PhoneContactTags> PhoneContactTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MobileNumber>() //TODO: migrate to EntitiyNameRepositoryContext
           .HasAlternateKey(mb => mb.number);

            modelBuilder.Entity<ContactTag>().HasAlternateKey(ct => ct.tag);

            modelBuilder.Entity<PhoneContactTags>()
        .HasKey(pc => new { pc.phoneContactId, pc.tagId });

            modelBuilder.Entity<PhoneContactTags>()
                .HasOne(pc => pc.phoneContact)
                .WithMany(p => p.contactTags)
                .HasForeignKey(pc => pc.phoneContactId);
            modelBuilder.Entity<PhoneContactTags>()
                .HasOne(pc => pc.contactTag)
                .WithMany(p => p.PhoneContacts)
                .HasForeignKey(pc => pc.tagId);


            modelBuilder.Entity<SimContact>().HasKey(s => s.id);
            modelBuilder.Entity<SimContact>().HasMany(s => s.MobileNumbers).WithOne(s => s.contact);
        }


    }
}