namespace GraphApi.Models
{
    public class Node
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public ICollection<Edge> Edges { get; set; }
    }
}