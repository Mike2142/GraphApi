namespace GraphApi.Models
{
    public class Edge
    {
        public int EdgeID { get; set; }
        public int SrcID { get; set; }
        public int Distance { get; set; }
        public int DestID { get; set; }


        public Node Src { get; set; }
        public Node Dest { get; set; }
    }
}