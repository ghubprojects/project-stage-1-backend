﻿using FluentValidation;
using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp yêu cầu (request) để cập nhật thông tin nhân viên
    /// </summary>
    public class UpdateEmployeeCommand : EmployeeUpdateDto, IRequest<Response<int>>
    {
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để cập nhật thông tin nhân viên
    /// </summary>
    internal sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IEmployeeServiceCommand _employeeServiceCommands;

        public UpdateEmployeeCommandHandler(IEmployeeServiceCommand employeeServiceCommand)
        {
            _employeeServiceCommands = employeeServiceCommand;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="request">Yêu cầu (request) để cập nhật thông tin nhân viên</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Số dòng bị ảnh hưởng bởi việc cập nhật thông tin nhân viên</returns>
        /// Created by: ttanh (20/09/2023)
        public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _employeeServiceCommands.UpdateAsync(request);
            return new Response<int>(affectedRows);
        }
    }

    /// <summary>
    /// Validator để kiểm tra thông tin khi cập nhật nhân viên
    /// </summary>
    /// <remarks>Created by: ttanh (20/09/2023)</remarks>
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.EmployeeCode)
                .NotEmpty().WithMessage(ValidationResource.Empty_EmployeeCode)
                .MaximumLength(20)
                .WithMessage(ValidationResource.Maxlength_EmployeeCode);

            RuleFor(e => e.FullName)
                .NotEmpty().WithMessage(ValidationResource.Empty_FullName)
                .MaximumLength(100)
                .WithMessage(ValidationResource.Maxlength_FullName);

            RuleFor(e => e.DateOfBirth)
                .Must(dateOfBirth =>
                {
                    if (dateOfBirth.HasValue)
                        return DateTimeOffset.Compare(dateOfBirth.Value, DateTimeOffset.Now) <= 0;
                    return true;
                })
                .WithMessage(ValidationResource.GreaterNow_DateOfBirth);

            RuleFor(e => e.IdentityNumber)
                .MaximumLength(25)
                .WithMessage(ValidationResource.Maxlength_IdentityNumber);

            RuleFor(e => e.IdentityIssuedPlace)
                .MaximumLength(255)
                .WithMessage(ValidationResource.Maxlength_IdentityIssuedPlace);

            RuleFor(e => e.IdentityIssuedDate)
                .Must(identityIssuedDate =>
                {
                    if (identityIssuedDate.HasValue)
                        return DateTimeOffset.Compare(identityIssuedDate.Value, DateTimeOffset.Now) <= 0;
                    return true;
                })
                .WithMessage(ValidationResource.GreaterNow_IdentityIssuedDate);

            RuleFor(e => e.PositionName)
                .MaximumLength(100)
                .WithMessage(ValidationResource.Maxlength_PositionName);

            RuleFor(e => e.Address)
                .MaximumLength(255)
                .WithMessage(ValidationResource.Maxlength_Address);

            RuleFor(e => e.Email)
               .MaximumLength(100)
               .WithMessage(ValidationResource.Maxlength_Email);

            RuleFor(e => e.MobilePhone)
                .MaximumLength(50)
                .WithMessage(ValidationResource.Maxlength_MobilePhone);

            RuleFor(e => e.LandlinePhone)
                .MaximumLength(50)
                .WithMessage(ValidationResource.Maxlength_LandlinePhone);

            RuleFor(e => e.BankAccount)
                .MaximumLength(25)
                .WithMessage(ValidationResource.Maxlength_BankAccount);

            RuleFor(e => e.BankName)
                .MaximumLength(255)
                .WithMessage(ValidationResource.Maxlength_BankName);

            RuleFor(e => e.BankBranch)
                .MaximumLength(255)
                .WithMessage(ValidationResource.Maxlength_BankBranch);
        }
    }
}
