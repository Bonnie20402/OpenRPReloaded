using OpenRPReloaded.Enums.Lgbt;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenRPReloaded.Models
{
    [Table("Characters")]
    public class Character
    {
        public Account Account { get; set; }

        public Guid AccountID { get; set; }

        [Key]
        public Guid CharacterID { get; set; }

        public GenderIdentity GenderIdentity { get; set; }
        public Pronouns Pronouns { get; set; }
        public SexualOrientation SexualOrientation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int Respect { get; set; }



    }
}
