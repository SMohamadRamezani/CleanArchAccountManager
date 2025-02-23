using FluentValidation;
using MediatR;
using App.Wrapper;

namespace App
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(v => v.Validate(context))
                                      .SelectMany(result => result.Errors)
                                      .Where(f => f != null)
                                      .ToList();

            if (failures.Count != 0)
            {
                var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
                return (TResponse)(object)new ApiResponse<List<string>>(errorMessages);
            }

            return await next();
        }
    }

}
