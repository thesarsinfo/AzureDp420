namespace Dp420Conexao.DTO
{
    public class ProductReadDTO
    {
        public ProductReadDTO(Guid id, String categoryId)
        {
            Id = id;
            CategoryId = categoryId;
        }

        public Guid Id { get; set; }
        public String CategoryId { get; set; }

    }
}