namespace MusicProduction.Models
{
    public static class ProductCategories
    {
        public static readonly ProductCategory Guitars = new ProductCategory { ProductCategoryId = 1, Name = "Guitars" };
        public static readonly ProductCategory GuitarsAmps = new ProductCategory { ProductCategoryId = 2, Name = "GuitarsAmps" };
        public static readonly ProductCategory Keyboards = new ProductCategory { ProductCategoryId = 3, Name = "Keyboards" };
        public static readonly ProductCategory Percussion = new ProductCategory { ProductCategoryId = 4, Name = "Percussion" };
        public static readonly ProductCategory BassAmps = new ProductCategory { ProductCategoryId = 5, Name = "BassAmps" };
        public static readonly ProductCategory BassCabs = new ProductCategory { ProductCategoryId = 6, Name = "BassCabs" };
        public static readonly ProductCategory Drums = new ProductCategory { ProductCategoryId = 7, Name = "Drums" };
        public static readonly ProductCategory KickDrums = new ProductCategory { ProductCategoryId = 8, Name = "KickDrums" };
        public static readonly ProductCategory TomDrums = new ProductCategory { ProductCategoryId = 9, Name = "TomDrums" };
        public static readonly ProductCategory SnareDrums = new ProductCategory { ProductCategoryId = 10, Name = "SnareDrums" };
        public static readonly ProductCategory DrumAccessories = new ProductCategory { ProductCategoryId = 11, Name = "DrumAccessories" };
    }
}
