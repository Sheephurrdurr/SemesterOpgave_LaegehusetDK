using Domain.Entities;
using Domain.ValueObjects;

namespace ConsultationTests
{
    public class ConsultationTests
    {
        [Fact]
        public void TimeSlot_StartTimeIsInThePast_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new TimeSlot(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
            // The above line attempts to create a TimeSlot with a start time in the past, which should throw an ArgumentException.
            // Since TimeSlot handles the validation of start time, we directly test the TimeSlot constructor with an invalid start time. As opposed to the ChangeStartTime method of Consultation
        }

        [Fact]
        public void Constructor_CalculateTimeSlotEndTime_ShouldSucceed()
        {
            // Arrange
            var doctor = new Doctor("Test Doctor");
            var patient = new Patient("Test Patient", "1234567890");
            var consultationType = new RegularConsultation(Guid.NewGuid());

            int addedDays = 3;
            DateTime startTime = DateTime.Now.AddDays(addedDays);

            DateTime calculateTestdEndTime = startTime + consultationType.Duration;

            var consultation = new Consultation(consultationType, doctor, patient, startTime);

            // Act & Assert
            Assert.Equal(calculateTestdEndTime, consultation.TimeSlot.EndTime);
        }

        [Fact]
        public void ChangeConsultationType_ToAcceptedType_ShouldSucceed()
        {
            // Arrange
            var doctor = new Doctor("Test Doctor");
            var patient = new Patient("Test Patient", "1234567890");
            var consultationType = new RegularConsultation(Guid.NewGuid());

            var consultation = new Consultation(consultationType, doctor, patient, DateTime.Now.AddDays(2));

            // Act
            var newConsultationType = new PerscriptionRenewal(Guid.NewGuid());
            consultation.ChangeConsultationType(newConsultationType.Id);

            // Assert 
            Assert.True(consultation.ConsultationTypeId == newConsultationType.Id);
        }

        [Fact]
        public void Consultation_CancelCancelledConsultation_ShouldThrowExeption()
        {
            // Arrange
            var doctor = new Doctor("Test Doctor");
            var patient = new Patient("Test Patient", "1234567890");
            var consultationType = new RegularConsultation(Guid.NewGuid());

            var consultation = new Consultation(consultationType, doctor, patient, DateTime.Now.AddDays(2));

            // Act
            consultation.Cancel();

            // Assert
            Assert.Throws<InvalidOperationException>(() => consultation.Cancel());
        }

        [Fact]
        public void Consultation_MarkArriedCancelledConsultation_ShouldThrowExeption()
        {
            // Arrange
            var doctor = new Doctor("Test Doctor");
            var patient = new Patient("Test Patient", "1234567890");
            var consultationType = new RegularConsultation(Guid.NewGuid());
            var consultation = new Consultation(consultationType, doctor, patient, DateTime.Now.AddDays(2));

            // Act
            consultation.Cancel();

            // Assert
            Assert.Throws<InvalidOperationException>(() => consultation.MarkArrived());
        }

        [Fact]
        public void Consultation_CompleteNonArrivedConsultation_ShouldThrowExeption()
        {
            // Arrange
            var doctor = new Doctor("Test Doctor");
            var patient = new Patient("Test Patient", "1234567890");
            var consultationType = new RegularConsultation(Guid.NewGuid());
            var consultation = new Consultation(consultationType, doctor, patient, DateTime.Now.AddDays(2));
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => consultation.Complete("Test Note"));
        }
    }
}
