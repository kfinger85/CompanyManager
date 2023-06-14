namespace  MusicProduction.Models
{
    public class StageProduct
    {
        public int StageProductId { get; set; }
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}