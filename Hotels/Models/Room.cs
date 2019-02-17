using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter room name"), Display(Name = "Room name"), StringLength(50)]
        public string Name { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }
    }
}