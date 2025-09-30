using System.Text.RegularExpressions;

namespace MyExpenses.ValueObjects
{
    public class EmailVo
    {
        public string EmailAddress { get; private set; }

        protected EmailVo() { }

        public EmailVo(string emailAddress )
        {
            SetEmail(emailAddress);
        }

        public void SetEmail(string emailAddress)
        {
            ValidateEmail(emailAddress);
            EmailAddress = emailAddress; 
        }

        private void ValidateEmail(string emailAddress)
        {
            switch (emailAddress.Length)
            {
                case < 4:
                    throw new ArgumentException("Email must be at least 4 characters");

                case > 255:
                    throw new ArgumentException("Email address cannot exceed 255 characters");
            }

            RegexEmailCheck(emailAddress);
        }

        private void RegexEmailCheck(string emailAddress)
        {
            var emailIsFormatted = Regex.IsMatch(emailAddress,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (!emailIsFormatted)
                throw new Exception("Email is not valid!");
        }
    }
}
