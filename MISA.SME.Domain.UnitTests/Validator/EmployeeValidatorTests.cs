using NSubstitute;

namespace MISA.SME.Domain.UnitTests
{
    [TestFixture]
    public class EmployeeValidatorTests
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private EmployeeValidator EmployeeValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            UnitOfWork = Substitute.For<IUnitOfWork>();
            EmployeeValidator = Substitute.For<EmployeeValidator>(UnitOfWork);
        }

        #region CheckExistEmployeeCodeAsync

        /// <summary>
        /// Test CheckExistEmployeeCodeAsync với đầu vào là mã đã tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task CheckExistEmployeeCodeAsync_EmployeeCodeExists_ThrowsConflictException()
        {
            // Arrange
            var employeeCode = "NV-1111";
            var existingEmployeeDto = new EmployeeDto();

            UnitOfWork.EmployeeRepository.GetByCodeAsync(employeeCode).Returns(existingEmployeeDto);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ConflictException>(async () => await EmployeeValidator.CheckExistEmployeeCodeAsync(employeeCode));
            Assert.That(ex.Message, Is.EqualTo($"Mã nhân viên <{employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại."));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByCodeAsync(employeeCode);
            UnitOfWork.DidNotReceive().Commit();
        }

        /// <summary>
        /// Test CheckExistEmployeeCodeAsync với đầu vào là mã không tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task CheckExistEmployeeCodeAsync_EmployeeCodeNotExists_DoesNotThrowException()
        {
            // Arrange
            var employeeCode = "NV-1111";
            EmployeeDto existingEmployeeDto = null;

            UnitOfWork.EmployeeRepository.GetByCodeAsync(employeeCode).Returns(existingEmployeeDto);

            // Act
            await EmployeeValidator.CheckExistEmployeeCodeAsync(employeeCode);

            // Verify that Commit is called once
            await UnitOfWork.EmployeeRepository.Received(1).GetByCodeAsync(employeeCode);
            UnitOfWork.Received(1).Commit();
        }

        #endregion
    }
}
