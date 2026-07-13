namespace LlamaDashboard.Models;

public class ChatMessage
{
    public string Role { get; set; } = "user";
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class ChatCompletionRequest
{
    public string Model { get; set; } = string.Empty;
    public List<ChatMessage> Messages { get; set; } = new();
    public double Temperature { get; set; } = 0.7;
    public int MaxTokens { get; set; } = -1;
    public bool Stream { get; set; } = false;
}

public class ChatCompletionResponse
{
    public string Id { get; set; } = string.Empty;
    public string Object { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Model { get; set; } = string.Empty;
    public List<ChatChoice> Choices { get; set; } = new();
    public Usage Usage { get; set; } = new();
}

public class ChatChoice
{
    public int Index { get; set; }
    public Message Message { get; set; } = new();
    public string? FinishReason { get; set; }
}

public class Message
{
    public string Role { get; set; } = "assistant";
    public string Content { get; set; } = string.Empty;
}

public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}
