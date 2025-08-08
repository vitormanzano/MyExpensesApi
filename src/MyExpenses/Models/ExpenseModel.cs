namespace MyExpenses.Models
{
    public class ExpenseModel
    {
        public Guid Id { get; private set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public DateOnly Date {  get; private set; }
        public Guid UserId { get; private set; }
        public UserModel User { get; }
        public Guid CategoryId { get; private set; }
        public CategoryModel Category { get; }

        protected ExpenseModel() { }

        public ExpenseModel(decimal value, string description, DateOnly date, Guid userId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            SetValue(value);
            SetDescription(description);
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

        public void SetDescription(string description)
        {
            ValidateDescription(description);
            Description = description;
        }

        private void ValidateDescription(string description)
        {
            if (description.Length > 255)
                throw new ArgumentException("Description shouldn't have more than 255 characters");
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
