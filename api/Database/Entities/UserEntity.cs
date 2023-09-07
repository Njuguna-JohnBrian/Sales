using System.ComponentModel.DataAnnotations;

namespace api.Database.Entities;

public sealed class UserEntity
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpiryDtm { get; set; }
    public DateTime RegistrationDtm { get; set; }
    public long? UserRoleId { get; set; }
    public DateTime? UpdatedDtm { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedDtm { get; set; }
#pragma warning disable CS8618
    [Timestamp] public byte[] RowVersion { get; set; }

    public RoleEntity RoleEntity { get; set; }
}