using System.ComponentModel.DataAnnotations;

namespace api.Database.Entities;

public class UserEntity
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpiryDTM { get; set; }
    public DateTime RegistrationDTM { get; set; }
    public DateTime? UpdatedDTM { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime DeletedDTM { get; set; }
    [Timestamp] public virtual byte[] RowVersion { get; set; }
}