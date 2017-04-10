using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattleEnd.SharedModels.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int WarriorId { get; set; }

        [ForeignKey("WarriorId")]
        public virtual Warrior Warrior { get; set; }

        public DateTime GuardDate { get; set; }
    }
}
