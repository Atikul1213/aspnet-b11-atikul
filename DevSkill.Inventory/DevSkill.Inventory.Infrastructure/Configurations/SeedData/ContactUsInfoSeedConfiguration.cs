using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations.SeedData
{
    public class ContactUsInfoSeedConfiguration : IEntityTypeConfiguration<ContactUsInfo>
    {
        public void Configure(EntityTypeBuilder<ContactUsInfo> builder)
        {
            builder.HasData(new ContactUsInfo
            {
                Id = Guid.Parse("12345678-1234-1234-1234-123456789abc"),
                HeaderTitle = "Contact Us",
                HeaderSubtitle = "We're here to help you succeed.",
                HeaderDescription = "Any question or remark? Just write us a message.",
                Address = "House # 184 (8th Floor), uttam para, kachinia, Dinajpur, Bangladesh",
                Phone = "+8801722248512",
                Email = "atikuldpi@gmail.com",
                OpenHours = "Sun-Thu: 9AM - 11PM",
                MapEmbedUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d627.157029892025!2d90.36982840964166!3d23.804157847728483!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3755c796bbbcadcf%3A0xc14e0a242f03896!2sDev%20Skill!5e1!3m2!1sen!2sbd!4v1755592523100!5m2!1sen!2sbd"

            });
        }
    }
}
