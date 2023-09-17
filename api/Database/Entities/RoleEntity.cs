using System.ComponentModel.DataAnnotations;

namespace api.Database.Entities;

public sealed class RoleEntity
{
    public long Id { get; set; }
    public Guid RoleId { get; set; }
    public required string RoleName { get; set; }
    public required string RoleDescription { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedDtm { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedDtm { get; set; }
#pragma warning disable CS8618
    [Timestamp] public byte[] RowVersion { get; set; }
}