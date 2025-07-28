namespace MyExpenses.Dtos.Category
{
    public record ResponseCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ResponseCategoryDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
