using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Node Src { get; set; }
        public Node Dest { get; set; }
    }
}