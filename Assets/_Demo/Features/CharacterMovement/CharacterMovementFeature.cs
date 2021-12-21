public sealed class CharacterMovementFeature : Feature
{
    public CharacterMovementFeature(Contexts contexts)
    {
        Add(new InitializeControlsSystem(contexts));
    }
}