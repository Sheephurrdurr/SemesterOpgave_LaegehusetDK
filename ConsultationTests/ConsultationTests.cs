using Domain.Entities;

namespace ConsultationTests
{
    public class ConsultationTests
    {
        [Fact]
        public void ValidateStartTime_StartTimeIsInThePast_ShouldThrowArgumentException()
        {
            // Arrange
            var doctor = new Doctor();
            var patient = new Patient();
            var regularConsultation = new RegularConsultation();

            var consultation = new Consultation(regularConsultation, doctor, patient, DateTime.Now.AddDays(1));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => consultation.ChangeStartTime(DateTime.Now.Subtract(TimeSpan.FromDays(5))));
            
            
        }
    }
}
