namespace Mastery.Career.Domain.Categories;

public static class CategoryErrors
{
    public static Error NotFound = new("Category.Found", "Category with specified identifier not found");

    public static Error InvalidValue = new("Category.Value", "Provided value is invalid");

    public static Error InvalidColor = new("Category.Color", "Provided color is invalid");
}
