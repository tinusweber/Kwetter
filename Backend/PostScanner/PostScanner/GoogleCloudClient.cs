using Google.Cloud.Language.V1;

namespace PostScanner;

public class GoogleCloudClient
{
    public static List<Entity> AnalyzeEntities(string content)
    {
        var client = LanguageServiceClient.Create();
        var document = Document.FromPlainText(content);
        var response = client.AnalyzeEntitySentiment(document);

        return response.Entities.ToList();
    }

    public static bool IsApproved(string body)
    {
        var analyze = GoogleCloudClient.AnalyzeEntities(body);
        return analyze.Where(x => x.Sentiment.Score < 0.5)
            .Any(x => x.Type is Entity.Types.Type.Organization or Entity.Types.Type.Person);
    }
}