using AutoMapper;
using MISA.SME.Domain;
using NSubstitute;

namespace MISA.SME.Application.UnitTests
{
    [TestFixture]
    public class DepartmentServiceQueryTests
    {
        #region Property

        private IUnitOfWork UnitOfWork { get; set; }

        private IMapper Mapper { get; set; }

        private DepartmentServiceQuery DepartmentServiceQuery { get; set; }

        #endregion

        [SetUp]
        public void Setup()
        {
            UnitOfWork = Substitute.For<IUnitOfWork>();
            Mapper = Substitute.For<IMapper>();
            DepartmentServiceQuery = Substitute.For<DepartmentServiceQuery>(UnitOfWork, Mapper);
        }

        #region GetAllAsync

        /// <summary>
        /// Test GetAllAsync khi có danh sách trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (25/09/2023)
        [Test]
        public async Task GetAllAsync_ValidData_ReturnsDepartmentDtoList()
        {
            // Arrange
            var departmentList = new List<Department> { new Department(), new Department() };
            var departmentDtoList = new List<DepartmentDto> { new DepartmentDto(), new DepartmentDto() };

            UnitOfWork.DepartmentRepository.GetAllAsync().Returns(departmentList);
            Mapper.Map<List<DepartmentDto>>(departmentList).Returns(departmentDtoList);

            // Act
            var result = await DepartmentServiceQuery.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<DepartmentDto>>());
            Assert.That(result, Is.EqualTo(departmentDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetAllAsync();
            Mapper.Received(1).Map<List<DepartmentDto>>(departmentList);
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
            List<Department> departmentList = null; // Simulate null data

            UnitOfWork.DepartmentRepository.GetAllAsync().Returns(departmentList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await DepartmentServiceQuery.GetAllAsync());
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách đơn vị"));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetAllAsync();
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion

        #region GetFilteringAsync

        /// <summary>
        /// Test GetFilteringAsync khi có danh sách bản ghi trả về
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (28/09/2023)
        [Test]
        public async Task GetFilteringAsync_ValidData_ReturnsDepartmentDtoList()
        {
            // Arrange
            var keyword = "searchKeyword";
            var departmentDtoList = new List<DepartmentDto> { new DepartmentDto(), new DepartmentDto() };

            UnitOfWork.DepartmentRepository.GetFilteringAsync(keyword).Returns(departmentDtoList);

            // Act
            var result = await DepartmentServiceQuery.GetFilteringAsync(keyword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<DepartmentDto>>());
            Assert.That(result, Is.EqualTo(departmentDtoList));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetFilteringAsync(keyword);
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
            List<DepartmentDto> departmentDtoList = null; // Simulate null data

            UnitOfWork.DepartmentRepository.GetFilteringAsync(keyword).Returns(departmentDtoList);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await DepartmentServiceQuery.GetFilteringAsync(keyword));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy danh sách đơn vị"));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetFilteringAsync(keyword);
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
        public async Task GetByIdAsync_ValidData_ReturnsDepartmentDto()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department(); // Replace with your test data
            var departmentDto = new DepartmentDto(); // Replace with expected DTO

            UnitOfWork.DepartmentRepository.GetByIdAsync(departmentId).Returns(department);
            Mapper.Map<DepartmentDto>(department).Returns(departmentDto);

            // Act
            var result = await DepartmentServiceQuery.GetByIdAsync(departmentId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<DepartmentDto>());
            Assert.That(result, Is.EqualTo(departmentDto));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetByIdAsync(departmentId);
            Mapper.Received(1).Map<DepartmentDto>(department);
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
            var departmentId = Guid.NewGuid();
            Department department = null; // Simulate null data

            UnitOfWork.DepartmentRepository.GetByIdAsync(departmentId).Returns(department);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await DepartmentServiceQuery.GetByIdAsync(departmentId));
            Assert.That(ex.Message, Is.EqualTo("Không tìm thấy đơn vị"));

            // Kiểm tra number of calls
            await UnitOfWork.DepartmentRepository.Received(1).GetByIdAsync(departmentId);
            UnitOfWork.DidNotReceive().Commit();
        }

        #endregion
    }
}
