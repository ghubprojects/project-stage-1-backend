using AutoMapper;
using MISA.SME.Domain;
using NSubstitute;

namespace MISA.SME.Application.UnitTests
{
    [TestFixture]
    public class EmployeeServiceCommandTests
    {
        #region Property

        private IUnitOfWork UnitOfWork { get; set; }

        private IMapper Mapper { get; set; }

        private IEmployeeValidator EmployeeValidator { get; set; }

        private EmployeeServiceCommand EmployeeServiceCommand { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            UnitOfWork = Substitute.For<IUnitOfWork>();
            Mapper = Substitute.For<IMapper>();
            EmployeeValidator = Substitute.For<IEmployeeValidator>();
            EmployeeServiceCommand = Substitute.For<EmployeeServiceCommand>(UnitOfWork, Mapper, EmployeeValidator);
        }

        #region AddAsync

        /// <summary>
        /// Test AddAsync với đầu vào là nhân viên có ID trống
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task AddAsync_EmployeeWithEmptyId_ReturnsEmployeeWithNewId()
        {
            // Arrange
            var createEmployeeDto = new EmployeeCreateDto();
            var employeeEntity = new Employee() { EmployeeID = Guid.Empty };

            Mapper.Map<Employee>(createEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.AddAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.AddAsync(createEmployeeDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(1));
                Assert.That(employeeEntity.GetId(), Is.Not.EqualTo(Guid.Empty));
            });

            // Kiểm tra number of calls
            await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(createEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).AddAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test AddAsync với đầu vào là nhân viên chưa có thông tin khởi tạo, cập nhật bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task AddAsync_EmployeeWithEmptyAuditFields_ReturnsEmployeeWithNotEmptyAuditFields()
        {
            // Arrange
            var createEmployeeDto = new EmployeeCreateDto();
            var employeeEntity = new Employee();

            Mapper.Map<Employee>(createEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.AddAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.AddAsync(createEmployeeDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(1));
                Assert.That(employeeEntity.CreatedBy, Is.EqualTo("Tăng Thế Anh"));
                Assert.That(employeeEntity.ModifiedBy, Is.EqualTo("Tăng Thế Anh"));
            });

            // Kiểm tra number of calls
            await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(createEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).AddAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test AddAsync với đầu vào hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task AddAsync_ValidEmployee_ReturnsSuccess()
        {
            // Arrange
            var createEmployeeDto = new EmployeeCreateDto();
            var employeeEntity = new Employee();

            Mapper.Map<Employee>(createEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.AddAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.AddAsync(createEmployeeDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));

            // Kiểm tra number of calls
            await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(createEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).AddAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        #endregion

        #region UpdateAsync

        /// <summary>
        /// Test UpdateAsync với đầu vào là nhân viên có mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task UpdateAsync_EmployeeWithNewCode_ReturnsEmployeeWithUniqueCode()
        {
            // Arrange
            var updateEmployeeDto = new EmployeeUpdateDto();
            var employeeEntity = new Employee();

            Mapper.Map<Employee>(updateEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.UpdateAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.UpdateAsync(updateEmployeeDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));

            // Kiểm tra number of calls
            if (updateEmployeeDto.CurrentEmployeeCode != updateEmployeeDto.EmployeeCode)
                await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(updateEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).UpdateAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test UpdateAsync với nhân viên chưa có thông tin khởi tạo, cập nhật bản ghi 
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task UpdateAsync_EmployeeWithEmptyAuditFields_ReturnsEmployeeWithNotEmptyAuditFields()
        {
            // Arrange
            var updateEmployeeDto = new EmployeeUpdateDto();
            var employeeEntity = new Employee();

            Mapper.Map<Employee>(updateEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.UpdateAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.UpdateAsync(updateEmployeeDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(1));
                Assert.That(employeeEntity.ModifiedBy, Is.EqualTo("Tăng Thế Anh"));
            });

            // Kiểm tra number of calls
            if (updateEmployeeDto.CurrentEmployeeCode != updateEmployeeDto.EmployeeCode)
                await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(updateEmployeeDto.EmployeeCode);
            else
                await EmployeeValidator.DidNotReceive().CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(updateEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).UpdateAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test AddAsync với đầu vào là nhân viên hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task UpdateAsync_ValidEmployee_ReturnsSuccess()
        {
            // Arrange
            var updateEmployeeDto = new EmployeeUpdateDto();
            var employeeEntity = new Employee();

            Mapper.Map<Employee>(updateEmployeeDto).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.UpdateAsync(employeeEntity).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.UpdateAsync(updateEmployeeDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));

            // Kiểm tra number of calls
            if (updateEmployeeDto.CurrentEmployeeCode != updateEmployeeDto.EmployeeCode)
                await EmployeeValidator.Received(1).CheckExistEmployeeCodeAsync(updateEmployeeDto.EmployeeCode);
            else
                await EmployeeValidator.DidNotReceive().CheckExistEmployeeCodeAsync(Arg.Any<string>());
            Mapper.Received(1).Map<Employee>(updateEmployeeDto);
            await UnitOfWork.EmployeeRepository.Received(1).UpdateAsync(employeeEntity);
            UnitOfWork.Received(1).Commit();
        }

        #endregion

        #region DeleteAsync

        /// <summary>
        /// Test DeleteAsync với đầu vào là nhân viên không tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task DeleteAsync_NonExistingEmployee_ThrowsNotFoundException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            UnitOfWork.EmployeeRepository.GetByIdAsync(employeeId).Returns((Employee)null);

            // Act and Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceCommand.DeleteAsync(employeeId));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByIdAsync(employeeId);
            await UnitOfWork.EmployeeRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
            UnitOfWork.DidNotReceive().Commit();
        }

        /// <summary>
        /// Test DeleteAsync với đầu vào là nhân viên có tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task DeleteAsync_ExistingEmployee_ReturnsSuccess()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employeeEntity = new Employee();

            UnitOfWork.EmployeeRepository.GetByIdAsync(employeeId).Returns(employeeEntity);
            UnitOfWork.EmployeeRepository.DeleteAsync(employeeId).Returns(1);

            // Act
            var result = await EmployeeServiceCommand.DeleteAsync(employeeId);

            // Assert
            Assert.That(result, Is.EqualTo(1));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByIdAsync(employeeId);
            await UnitOfWork.EmployeeRepository.Received(1).DeleteAsync(employeeId);
            UnitOfWork.Received(1).Commit();
        }

        #endregion

        #region DeleteMultipleAsync

        /// <summary>
        /// Test DeleteMultipleAsync với đầu vào là 1 nhân viên không tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_NonExistingEmployee_ThrowsNotFoundException()
        {
            // Arrange
            var employeeIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            UnitOfWork.EmployeeRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns((Employee)null);

            // Act and Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceCommand.DeleteMultipleAsync(employeeIds));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received().GetByIdAsync(Arg.Any<Guid>());
            await UnitOfWork.EmployeeRepository.DidNotReceive().DeleteMultipleAsync(Arg.Any<List<Guid>>());
            UnitOfWork.DidNotReceive().Commit();
        }

        /// <summary>
        /// Test DeleteMultipleAsync với đầu vào hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_ExistingEmployees_ReturnsSuccess()
        {
            // Arrange
            var employeeIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var employeeEntities = employeeIds.Select(id => new Employee()).ToList();

            UnitOfWork.EmployeeRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns(employeeEntities[0], employeeEntities[1]);

            UnitOfWork.EmployeeRepository.DeleteMultipleAsync(employeeIds).Returns(2);

            // Act
            var result = await EmployeeServiceCommand.DeleteMultipleAsync(employeeIds);

            // Assert
            Assert.That(result, Is.EqualTo(2));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(2).GetByIdAsync(Arg.Any<Guid>());
            await UnitOfWork.EmployeeRepository.Received(1).DeleteMultipleAsync(employeeIds);
            UnitOfWork.Received(1).Commit();
        }

        #endregion
    }
}
