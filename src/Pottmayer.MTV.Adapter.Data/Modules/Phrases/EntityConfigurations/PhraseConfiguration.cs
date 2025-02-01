using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;

namespace Pottmayer.MTV.Adapter.Data.Modules.Phrases.EntityConfigurations
{
    public class PhraseConfiguration : IEntityTypeConfiguration<Phrase>
    {
        public void Configure(EntityTypeBuilder<Phrase> builder)
        {
            builder.ToTable("phr001_phrase");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasColumnName("description").IsRequired();
            builder.Property(p => p.Author).HasColumnName("author");
            builder.Property(p => p.IsVisible).HasColumnName("is_visible").IsRequired();
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by").IsRequired();
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

            builder.HasOne(p => p.Creator)
                   .WithMany(u => u.CreatedPhrases)
                   .HasForeignKey(p => p.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Updater)
                   .WithMany(u => u.UpdatedPhrases)
                   .HasForeignKey(p => p.UpdatedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
