namespace MyExpenses.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        protected CategoryModel() { }
    }
}
