using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISKI.IBKS.Domain.Entities;

namespace ISKI.IBKS.Persistence.Features.Configuration;

public sealed class ChannelInformationConfiguration : IEntityTypeConfiguration<ChannelInformation>
{
    public void Configure(EntityTypeBuilder<ChannelInformation> builder)
    {
        builder.ToTable("ChannelInformations");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Parameter)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(x => x.ParameterText)
            .HasMaxLength(100);
            
        builder.Property(x => x.Brand)
            .HasMaxLength(100);
            
        builder.Property(x => x.BrandModel)
            .HasMaxLength(100);
            
        builder.Property(x => x.UnitText)
            .HasMaxLength(50);
            
        builder.Property(x => x.SerialNumber)
            .HasMaxLength(100);

        builder.HasIndex(x => x.StationId);
        builder.HasIndex(x => new { x.StationId, x.Parameter }).IsUnique();
    }
}

public sealed class StationSettingsConfiguration : IEntityTypeConfiguration<StationSettings>
{
    public void Configure(EntityTypeBuilder<StationSettings> builder)
    {
        builder.ToTable("StationSettings");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Code)
            .HasMaxLength(50);
            
        builder.Property(x => x.ConnectionDomainAddress)
            .HasMaxLength(200);
            
        builder.Property(x => x.ConnectionPort)
            .HasMaxLength(10);
            
        builder.Property(x => x.ConnectionUser)
            .HasMaxLength(100);
            
        builder.Property(x => x.ConnectionPassword)
            .HasMaxLength(200);
            
        builder.Property(x => x.Company)
            .HasMaxLength(200);
            
        builder.Property(x => x.Address)
            .HasMaxLength(500);
            
        builder.Property(x => x.Software)
            .HasMaxLength(100);
            
        builder.Property(x => x.PlcIpAddress)
            .HasMaxLength(50);

        builder.HasIndex(x => x.StationId).IsUnique();
    }
}

public sealed class DiagnosticTypeConfiguration : IEntityTypeConfiguration<DiagnosticType>
{
    public void Configure(EntityTypeBuilder<DiagnosticType> builder)
    {
        builder.ToTable("DiagnosticTypes");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.DiagnosticTypeName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.DiagnosticLevelTitle)
            .HasMaxLength(50);

        builder.HasIndex(x => x.DiagnosticTypeNo).IsUnique();
    }
}
