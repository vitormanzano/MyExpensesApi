namespace MyExpenses.Errors.Categories;

public static class CategoriesErrors
{
    public static readonly Error AlreadyExists = new(409, "Category already exists.", ErrorType.Conflict);
    public static readonly Error NotFound      = new(404, "Category not found.", ErrorType.NotFound);
    public static readonly Error HasExpense    = new(409, "Delete the expense first.", ErrorType.Conflict);
    public static readonly Error CreateFailed  = new(500, "Could not create category.", ErrorType.Unexpected);
    public static readonly Error UpdateFailed  = new(500, "Could not update category.", ErrorType.Unexpected);
    public static readonly Error DeleteFailed  = new(500, "Could not delete category.", ErrorType.Unexpected);
}
