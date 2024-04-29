namespace HealthInsurePro.Presentation.Data
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            ErrorMessages = [];
        }

        public bool IsSuccess { get; set; }
        public object? Result { get; set; }
        public string? Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}