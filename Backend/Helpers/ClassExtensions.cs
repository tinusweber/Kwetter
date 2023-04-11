using FluentValidation;

namespace Helpers
{
    public static class ClassExtensions
    {
        public static void Validate<T, T2>(this T type) where T2 : AbstractValidator<T>, new()
        {
            T2 validator = new T2();
            validator.ValidateAndThrow(type);
        }
    }
}