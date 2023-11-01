using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MISA.SME.Domain;
using NSubstitute;

namespace MISA.SME.Application.UnitTests
{
    [TestFixture]
    public class EmployeeServiceQueryTests
    {
        #region Property

        private IWebHostEnvironment WebHostEnvironment { get; set; }

        private IUnitOfWork UnitOfWork { get; set; }

        private IMapper Mapper { get; set; }

        private EmployeeServiceQuery EmployeeServiceQuery { get; set; }

        #endregion

        [SetUp]
        public void Setup()
        {
            UnitOfWork = Substitute.For<IUnitOfWork>();
            Mapper = Substitute.For<IMapper>();
            EmployeeServiceQuery = Substitute.For<EmployeeServiceQuery>(WebHostEnvironment, UnitOfWork, Mapper);
        }

        #region GetAllAsync

        /// <summary>
        /// Test GetAllAsync khi có danh sách trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetAllAsync_ValidData_ReturnsEmployeeDtoList()
        {
            // Arrange
            var employeeList = new List<Employee> { new Employee(), new Employee() };
            var employeeDtoList = new List<EmployeeDto> { new EmployeeDto(), new EmployeeDto() };

            UnitOfWork.EmployeeRepository.GetAllAsync().Returns(employeeList);
            Mapper.Map<List<EmployeeDto>>(employeeList).Returns(employeeDtoList);

            // Act
            var result = await EmployeeServiceQuery.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<EmployeeDto>>());
            Assert.That(result, Is.EqualTo(employeeDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetAllAsync();
            Mapper.Received(1).Map<List<EmployeeDto>>(employeeList);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetAllAsync khi không có danh sách trả về 
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetAllAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            List<Employee> employeeList = null; // Simulate null data

            UnitOfWork.EmployeeRepository.GetAllAsync().Returns(employeeList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetAllAsync());
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách nhân viên"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetAllAsync();
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetPaginationAsync

        /// <summary>
        /// Test GetPaginationAsync khi có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetPaginationAsync_ValidData_ReturnsEmployeeDtoList()
        {
            // Arrange
            var limit = 20;
            var offset = 0;
            var employeeDtoList = new List<EmployeeDto> { new EmployeeDto(), new EmployeeDto() };

            UnitOfWork.EmployeeRepository.GetPaginationAsync(limit, offset).Returns(employeeDtoList);

            // Act
            var result = await EmployeeServiceQuery.GetPaginationAsync(limit, offset);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<EmployeeDto>>());
            Assert.That(result, Is.EqualTo(employeeDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetPaginationAsync(limit, offset);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetPaginationAsync khi không có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetPaginationAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            var limit = 20;
            var offset = 0;
            List<EmployeeDto> employeeDtoList = null; // Simulate null data

            UnitOfWork.EmployeeRepository.GetPaginationAsync(limit, offset).Returns(employeeDtoList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetPaginationAsync(limit, offset));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách nhân viên"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetPaginationAsync(limit, offset);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetFilteringAsync

        /// <summary>
        /// Test GetFilteringAsync khi có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetFilteringAsync_ValidData_ReturnsEmployeeDtoList()
        {
            // Arrange
            var keyword = "searchKeyword";
            var employeeDtoList = new List<EmployeeDto> { new EmployeeDto(), new EmployeeDto() };

            UnitOfWork.EmployeeRepository.GetFilteringAsync(keyword).Returns(employeeDtoList);

            // Act
            var result = await EmployeeServiceQuery.GetFilteringAsync(keyword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<EmployeeDto>>());
            Assert.That(result, Is.EqualTo(employeeDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetFilteringAsync(keyword);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetFilteringAsync khi không có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetFilteringAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            var keyword = "searchKeyword";
            List<EmployeeDto> employeeDtoList = null; // Simulate null data

            UnitOfWork.EmployeeRepository.GetFilteringAsync(keyword).Returns(employeeDtoList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetFilteringAsync(keyword));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách nhân viên"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetFilteringAsync(keyword);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetFilteringAndPaginationAsync

        /// <summary>
        /// Test GetFilteringAndPaginationAsync khi có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetFilteringAndPaginationAsync_ValidData_ReturnsEmployeeDtoList()
        {
            // Arrange
            var keyword = "searchKeyword";
            var limit = 20;
            var offset = 0;
            var employeeDtoList = new List<EmployeeDto> { new EmployeeDto(), new EmployeeDto() };

            UnitOfWork.EmployeeRepository.GetFilteringAndPaginationAsync(keyword, limit, offset).Returns(employeeDtoList);

            // Act
            var result = await EmployeeServiceQuery.GetFilteringAndPaginationAsync(keyword, limit, offset);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<EmployeeDto>>());
            Assert.That(result, Is.EqualTo(employeeDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetFilteringAndPaginationAsync(keyword, limit, offset);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetFilteringAndPaginationAsync khi không có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetFilteringAndPaginationAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            var keyword = "searchKeyword";
            var limit = 20;
            var offset = 0;
            List<EmployeeDto> employeeDtoList = null; // Simulate null data

            UnitOfWork.EmployeeRepository.GetFilteringAndPaginationAsync(keyword, limit, offset).Returns(employeeDtoList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetFilteringAndPaginationAsync(keyword, limit, offset));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách nhân viên"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetFilteringAndPaginationAsync(keyword, limit, offset);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetByIdAsync

        /// <summary>
        /// Test GetByIdAsync khi có bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetByIdAsync_ValidData_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee(); // Replace with your test data
            var employeeDto = new EmployeeDto(); // Replace with expected DTO

            UnitOfWork.EmployeeRepository.GetByIdAsync(employeeId).Returns(employee);
            Mapper.Map<EmployeeDto>(employee).Returns(employeeDto);

            // Act
            var result = await EmployeeServiceQuery.GetByIdAsync(employeeId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<EmployeeDto>());
            Assert.That(result, Is.EqualTo(employeeDto));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByIdAsync(employeeId);
            Mapper.Received(1).Map<EmployeeDto>(employee);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetByIdAsync khi không có bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetByIdAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            Employee employee = null; // Simulate null data

            UnitOfWork.EmployeeRepository.GetByIdAsync(employeeId).Returns(employee);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetByIdAsync(employeeId));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy nhân viên"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByIdAsync(employeeId);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetByCodeAsync

        /// <summary>
        /// Test GetByCodeAsync khi có bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetByCodeAsync_ValidData_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeCode = "NV-1111";
            var employeeDto = new EmployeeDto();

            UnitOfWork.EmployeeRepository.GetByCodeAsync(employeeCode).Returns(employeeDto);

            // Act
            var result = await EmployeeServiceQuery.GetByCodeAsync(employeeCode);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<EmployeeDto>());
            Assert.That(result, Is.EqualTo(employeeDto));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByCodeAsync(employeeCode);
            UnitOfWork.Received(1).Commit();
        }

        /// <summary>
        /// Test GetByCodeAsync khi không có bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetByCodeAsync_NullData_ThrowsNotFoundException()
        {
            // Arrange
            var employeeCode = "NV-1111";
            EmployeeDto employeeDto = null;

            UnitOfWork.EmployeeRepository.GetByCodeAsync(employeeCode).Returns(employeeDto);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await EmployeeServiceQuery.GetByCodeAsync(employeeCode));
            Assert.That(ex.Message, Is.EqualTo($"Không tìm thấy nhân viên {employeeCode}"));

            // Kiểm tra number of calls
            await UnitOfWork.EmployeeRepository.Received(1).GetByCodeAsync(employeeCode);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion
    }
}
