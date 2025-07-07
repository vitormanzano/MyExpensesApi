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

        public ExpenseModel(decimal value, DateOnly date, Guid userId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            SetValue(value);
            SetDate(date);
            SetUserId(userId);
            SetCategoryId(categoryId);
        }

        public void SetValue(decimal value)
        {
            ValidateValue(value);
            Value = value;
        }

        private void ValidateValue(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero");
        }

        public void SetDate(DateOnly date)
        {
            Date = date;
        }
        
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetCategoryId(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
