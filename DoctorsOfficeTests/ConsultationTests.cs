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

        [Fact] 
        public void Constructor_CalculateEndTime_ShouldSucceed()
        {
            // Arrange
            var doctor = new Doctor();
            var patient = new Patient();
            var consultationType = new RegularConsultation();

            int addedDays = 3;
            DateTime startTime = DateTime.Now.AddDays(addedDays);

            DateTime calculateTestdEndTime = startTime + consultationType.Duration;

            var consultation = new Consultation(consultationType, doctor, patient, startTime);

            // Act & Assert
            Assert.Equal(calculateTestdEndTime, consultation.EndTime);
        }

        [Fact]
        public void ChangeConsultationType_ToAcceptedType_ShouldSucceed()
        {
            // Arrange
            var doctor = new Doctor();
            var patient = new Patient();
            var consultationType = new RegularConsultation();

            var consultation = new Consultation(consultationType, doctor, patient, DateTime.Now.AddDays(2));

            // Act
            var newConsultationType = new PerscriptionRenewal();
            consultation.ChangeConsultationType(newConsultationType, DateTime.Now.AddDays(1));

            // Assert 
            Assert.True(consultation.ConsultationType ==  newConsultationType);
        }
    }
}
