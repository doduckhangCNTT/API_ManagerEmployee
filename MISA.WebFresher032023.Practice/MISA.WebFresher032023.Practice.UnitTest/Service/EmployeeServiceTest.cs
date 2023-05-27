using AutoMapper;
using Castle.Core.Configuration;
using MISA.WebFresher032023.Pactice.BL.DTO.Emoloyees;
using MISA.WebFresher032023.Pactice.BL.Service.Employees;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Employees;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.UnitTest.Service
{
    public class EmployeeServiceTest
    {
        [Test]
        public async Task GetAsync_NotFound_ReturnException()
        {
            // Arrange
            //var id = Guid.NewGuid();
            var id = Guid.Parse("d21b4aaf-9ab3-4617-ad8f-64e41e7a7c43");

            // Tạo ra 1 instance (object) được triển khai từ IEmployeeRepository
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAsync(id).ReturnsNull();

            var mapper = Substitute.For<IMapper>();
            var employeeService = new EmployeeService(employeeRepository, mapper);


            // Action
            //var actual = await  employeeService.GetAsync(id);
            //var ex = Assert.ThrowsAsync<InternalException>(() => employeeService.GetAsync(id));
            var ex = Assert.ThrowsAsync<InternalException>(async () => await employeeService.GetAsync(id));
            Assert.That(ex.Message, Is.EqualTo("Khong tim thay nhung baor server loi"));

            // Assert
        }

        [Test]
        public async Task GetAsync_ValidInput_ReturnsEmployee()
        {
            // Arrange
            //var id = Guid.NewGuid();
            var id = Guid.Parse("d21b4aaf-9ab3-4617-ad8f-64e41e7a7c43");

            // Tạo ra 1 instance (object) được triển khai từ IEmployeeRepository
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var employee = new Employee()
            {
                EmployeeId = id,
                EmployeeCode = "Test",
                FullName = "Do Duc Khang"
            };

            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmployeeId = id,
                EmployeeCode = "Test",
                FullName = "Do Duc Khang"
            };

            employeeRepository.GetAsync(id).Returns(employee);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<EmployeeDto>(employee).Returns(employeeDto);

            var employeeService = new EmployeeService(employeeRepository, mapper);


            // Action
            var actualResult = await employeeService.GetAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(employeeDto));

        }

        [Test]
        public async Task DeleteTaskAsync_NotFound_ReturnException()
        {
            // Arrange
            //var id = Guid.NewGuid();
            var id = Guid.Parse("d21b4aaf-9ab3-4617-ad8f-64e41e7a7c43");

            // Tạo ra 1 instance (object) được triển khai từ IEmployeeRepository
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAsync(id).ReturnsNull();

            var mapper = Substitute.For<IMapper>();
            var employeeService = new EmployeeService(employeeRepository, mapper);


            // Action
            //var actual = await  employeeService.GetAsync(id);
            //var ex = Assert.ThrowsAsync<InternalException>(() => employeeService.GetAsync(id));
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.DeleteTaskAsync(id));
            Assert.That(ex.Message, Is.EqualTo("Khong tim thay nhung baor server loi"));

            // Assert
        }

        [Test]
        public async Task DeleteTaskAsync_ValidInput_DeleteSuccess()
        {
            // Arrange
            //var id = Guid.NewGuid();
            var id = Guid.Parse("d21b4aaf-9ab3-4617-ad8f-64e41e7a7c43");

            // Tạo ra 1 instance (object) được triển khai từ IEmployeeRepository
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var employee = new Employee()
            {
                EmployeeId = id,
                EmployeeCode = "Test",
                FullName = "Do Duc Khang"
            };

            employeeRepository.GetAsync(id).Returns(employee);
            employeeRepository.DeleteAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();

            var employeeService = new EmployeeService(employeeRepository, mapper);


            // Action
            await employeeService.DeleteTaskAsync(id);

            // Assert
            await employeeRepository.Received(1).DeleteAsync(id);

        }

        //[Test]
        //public async Task DeleteTaskAsync_FemaleValid_ReturnException()
        //{
        //    // Arrange
        //    //var id = Guid.NewGuid();
        //    var id = Guid.Parse("d21b4aaf-9ab3-4617-ad8f-64e41e7a7c43");

        //    // Tạo ra 1 instance (object) được triển khai từ IEmployeeRepository
        //    var employeeRepository = Substitute.For<IEmployeeRepository>();

        //    var employee = new Employee()
        //    {
        //        EmployeeId = id,
        //        EmployeeCode = "Test",
        //        FullName = "Do Duc Khang",
        //        Gender = (int?)GenderEnum.Female
        //    };

        //    employeeRepository.GetAsync(id).Returns(employee);

        //    var mapper = Substitute.For<IMapper>();

        //    var employeeService = new EmployeeService(employeeRepository, mapper);


        //    // Action
        //    await employeeService.DeleteTaskAsync(id);

        //    // Assert
        //    var ex = Assert.ThrowsAsync<Exception>(async () => await employeeService.DeleteTaskAsync(id));
        //    Assert.That(ex.Message, Is.EqualTo("Không xóa phụ nữ"));

        //}
    }
}
