using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD2API.Domain;

namespace SD2API.Persistence.Configurations
{
    public class ReplayConfiguration : IEntityTypeConfiguration<Replay>
    {
        public void Configure(EntityTypeBuilder<Replay> builder)
        {
            builder.HasKey(e => e.ReplayId); // maybe... https://github.com/taikuukaits/SimpleWordlists

            builder.Property(e => e.ReplayHashStub).IsRequired().HasMaxLength(10);
            builder.HasIndex(e => e.ReplayHashStub);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            builder.Property(e => e.BinaryUrl).HasMaxLength(256);
            builder.Property(e => e.ReplayRawFooter).HasMaxLength(2048);
            builder.Property(e => e.ReplayRawHeader).HasMaxLength(4096);

            builder.HasOne(e => e.ReplayFooter)
                .WithOne(f => f.Replay)
                .IsRequired()
                .HasForeignKey<ReplayFooter>(f => f.ReplayId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ReplayHeader)
                .WithOne(f => f.Replay)
                .IsRequired()
                .HasForeignKey<ReplayHeader>(f => f.ReplayId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

    public class ReplayFooterConfiguration : IEntityTypeConfiguration<ReplayFooter>
    {
        public void Configure(EntityTypeBuilder<ReplayFooter> builder)
        {
            builder.HasKey(e => e.ReplayFooterId);

            builder.HasOne(e => e.result)
                .WithOne(r => r.ReplayFooter)
                .HasForeignKey<ReplayFooter.ReplayFooterResult>(x => x.ReplayFooterId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

    public class ReplayFooterResultConfiguration : IEntityTypeConfiguration<ReplayFooter.ReplayFooterResult>
    {
        public void Configure(EntityTypeBuilder<ReplayFooter.ReplayFooterResult> builder)
        {
            builder.HasKey(e => e.ReplayFooterResultId);

            builder.Property(e => e.Duration).HasMaxLength(10);
            builder.Property(e => e.Score).HasMaxLength(10);
            builder.Property(e => e.Victory).HasMaxLength(10);
        }
    }

    public class ReplayHeaderConfiguration : IEntityTypeConfiguration<ReplayHeader>
    {
        public void Configure(EntityTypeBuilder<ReplayHeader> builder)
        {
            builder.HasKey(e => e.ReplayHeaderId);

            builder.HasOne(e => e.Game)
                .WithOne(g => g.ReplayHeader)
                .HasForeignKey<ReplayHeader.ReplayHeaderGame>(x => x.ReplayHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Players)
                .WithOne(p => p.ReplayHeader)
                .HasForeignKey(x => x.ReplayHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
