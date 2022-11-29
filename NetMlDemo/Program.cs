using NetMlDemo;

string getSentiment(float predictedLabel) => predictedLabel == 1 ? "Positive" : "Negative";

// Input data.
List<string> list = new() {
    "This restaurant was wonderful.",
    "This restaurant was not wonderful.",
    "I like this restaurant.",
    "I don't like this restaurant.",
};
foreach (string item in list)
{
    SentimentModel.ModelInput sampleData = new() { Col0 = item };
    // Load model and predict output of sample data.
    SentimentModel.ModelOutput result = SentimentModel.Predict(sampleData);
    // Result.
    Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {getSentiment(result.PredictedLabel)}");
    Console.WriteLine();
}
