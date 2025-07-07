namespace MyExpenses.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        protected CategoryModel() { }

        public CategoryModel(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetUserId(userId);
        }

        public void SetName(string name)
        {
            ValidateName(name);
            Name = name;
        }

        private void ValidateName(string name)
        {
            switch (name.Length)
            {
                case < 4:
                    throw new ArgumentOutOfRangeException("Name must have at least 4 characters");
                case > 255:
                    throw new ArgumentOutOfRangeException("Name must have less than 255 characters");
            }
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}
