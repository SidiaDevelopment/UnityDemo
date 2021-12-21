public sealed class EditmodeFeature : Feature
{
    public EditmodeFeature(Contexts contexts)
    {
        Add(new InitializeEditmodeSystem(contexts));
    }
}