using System.ComponentModel.DataAnnotations;

namespace Database;

public class Notifications
{
    public int Id { get; set; }

    public required int UserId { get; set; }

    public bool HasBeenRead { get; set; } = false;

    [StringLength(256)]
    public string Message { get; set; }
}
