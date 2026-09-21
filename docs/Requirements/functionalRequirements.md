# Functional Requirements (FR)
Description of the capabilities and actions users can perform in the MyExpenses system.

### Authentication & User Management
* **FR01 (User Registration):** User should be able to register an account by providing Name, Email, CPF, and Password.
* **FR02 (User Login):** User should be able to authenticate using Email and Password, receiving a JWT Bearer token.
* **FR03 (Profile Management):** User should be able to view and update their profile information.
* **FR04 (Account Deletion):** User should be able to delete their account upon confirming their password.

---

### Category Management
* **FR05 (Create Category):** User should be able to create custom categories.
* **FR06 (Manage Categories):** User should be able to list, edit, and delete their own categories.

---

### Expense Management
* **FR07 (Create Expense):** User should be able to record a new expense specifying Amount, Description, Date, and Category.
* **FR08 (List & Filter Expenses):** User should be able to view all expenses, with options to filter by:
  * Specific month and year;
  * Category;
  * Minimum/maximum value.
* **FR09 (View Expense Details):** User should be able to view the full details of a specific expense by ID.
* **FR10 (Edit Expense):** User should be able to update an existing expense's value, date, description, or category.
* **FR11 (Delete Expense):** User should be able to remove an expense from the system.
