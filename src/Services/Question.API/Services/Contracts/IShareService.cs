namespace Question.API.Services.Contracts
{
    public interface IShareService
    {
        Task ByEmail(string destinationEmail, string contentUrl);
    }
}
