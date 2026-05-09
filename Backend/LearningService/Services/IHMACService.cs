namespace LearningService.Services;

public interface IHMACService
{
    string CreateSignature(string message);
    bool VerifySignature(string message, string signature);
}