using SalonManager.Domain.Entities;

namespace SalonManager.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        //[Fact]
        //public async Task InsertAsync_ShouldReturnAppointment_WhenDataIsValid()
        //{
        //    // Arrange
        //    var inputModel = new InputAppointmentModel
        //    {
        //        CustomerAppointmentId = 1,
        //        ServiceAppointmentId = 2,
        //        Description = "Test appointment",
        //        Date = new DateTime(2023, 7, 20)
        //    };

        //    var existingCustomer = new Customer
        //    {
        //        Id = 1
        //    };

        //    var existingService = new Service
        //    {
        //        Id = 2
        //    };

        //    // Mock
        //    _customerRepository.GetByIdAsync.Setup(x => x(inputModel.CustomerAppointmentId))
        //        .Returns(existingCustomer);

        //    _serviceRepository.GetByIdAsync.Setup(x => x(inputModel.ServiceAppointmentId))
        //        .Returns(existingService);

        //    // Act
        //    var appointment = await _appointmentService.InsertAsync(inputModel);

        //    // Assert
        //    Assert.NotNull(appointment);
        //    Assert.Equal(existingCustomer, appointment.CustomerAppointment);
        //    Assert.Equal(existingService, appointment.ServiceAppointment);
        //}
    }
}