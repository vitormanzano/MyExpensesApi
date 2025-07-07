namespace MyExpenses.Models
{
    public class ExpenseModel
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public DateOnly Date {  get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        protected ExpenseModel() { }
    }
}
