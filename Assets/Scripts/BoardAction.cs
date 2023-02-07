public record BoardAction(ActionType type, string player, int gridIndex, int count, int[] targets);

public enum ActionType
{
    Turn,
    Boom,
    Change,
    GameStarted
}