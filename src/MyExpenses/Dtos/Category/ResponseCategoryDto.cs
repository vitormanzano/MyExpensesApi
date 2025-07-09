namespace MyExpenses.Dtos.Category
{
    public record ResponseCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public ResponseCategoryDto(Guid id, string name, Guid userId)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }
    }
}
