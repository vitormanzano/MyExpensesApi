namespace MyExpenses.Dtos.Category
{
    public record CreateCategoryDto
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
