using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Domain.Entities;

namespace Plaground.Infrastructure.Persistence.Configurations;

public class CodeBarConfiguration : IEntityTypeConfiguration<CodeBar>
{
    public void Configure(EntityTypeBuilder<CodeBar> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(CodeBar.Rules.CODE_MAX_LENGTH);
    }
}