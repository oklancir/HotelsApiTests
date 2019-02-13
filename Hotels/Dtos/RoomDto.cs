using System.ComponentModel.DataAnnotations;

namespace Hotels.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Required]
        public int RoomTypeId { get; set; }

    }
}