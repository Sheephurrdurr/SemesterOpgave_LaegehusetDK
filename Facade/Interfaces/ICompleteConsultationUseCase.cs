using Facade.DTOs;

namespace Facade.Interfaces
{
    public interface ICompleteConsultationUseCase
    {
        // CQS says that commands should not return data. This is handled by UseCase , which will throw exceptions if the command is invalid. 
        // Exceptions thrown by the UseCase will be caught by the UI, which will display them appropriately.
        Task ExecuteAsync(CompleteConsultationRequest request);
    }
}
