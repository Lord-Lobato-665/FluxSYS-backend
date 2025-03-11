namespace FluxSYS_backend.Application.DTOs.CategoriesProducts
{
    public class CategoryProductsReadDTO
    {
        public int Id_category_product { get; set; }
        public string Name_category_product { get; set; }
        public string Name_company { get; set; }
        public bool Delete_log_category_product { get; set; }
    }
}