using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GraphApi.Models
{
    public class Edge
    {   
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EdgeID { get; private set;}
        public int SrcID { get; set; }
        public int Distance { get; set; }
        public int DestID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Node Src { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public virtual Node Dest { get; set; }
    }
}