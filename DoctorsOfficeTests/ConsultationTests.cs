using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities; 
using Xunit;

namespace DoctorsOfficeTests
{
    public class ConsultationTests
    {
        [Fact]
        public void CreateConsultation_ShouldSucceed()
        {
            // Arrange
            var doctor = new Doctor(); 
            var patient = new Patient();
            var regularConsultation = new RegularConsultation();

            var consultation = new Consultation(regularConsultation, doctor, patient);
        }
    }
}
