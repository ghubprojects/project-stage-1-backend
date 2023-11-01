using FluentValidation;
using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp thực hiện xử lý validation trước khi thực hiện xử lý yêu cầu (request) bằng MediatR
    /// </summary>
    /// <typeparam name="TRequest">Loại yêu cầu (request) đầu vào</typeparam>
    /// <typeparam name="TResponse">Loại phản hồi (response) đầu ra</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) và thực hiện validation trước khi gọi tiếp theo
        /// </summary>
        /// <param name="request">Yêu cầu (request) đầu vào</param>
        /// <param name="next">Hàm xử lý tiếp theo trong pipeline</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Kết quả xử lý yêu cầu (request)</returns>
        /// <exception cref="ValidateException">Ngoại lệ ValidateException nếu có lỗi validation</exception>
        /// Created by: ttanh (20/09/2023)
        /// Modified by: ttanh (02/10/2023)
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidateException(failures);
            }
            return await next();
        }
    }
}
