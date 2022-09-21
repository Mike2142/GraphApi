using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphApi.Models
{
    public class Node
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set;}
        public string? Name { get; set; }

        public ICollection<Edge>? SrcEdges { get;}
        public ICollection<Edge>? DestEdges { get;}
    }
}