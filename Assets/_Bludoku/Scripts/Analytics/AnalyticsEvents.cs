namespace _Bludoku.Scripts.Analytics
{
    public sealed class PieceMoveStartedEvent : IAnalyticsEvent
    {
        public string Name => "piece_move_started";
        public int PieceId { get; }
        public PieceMoveStartedEvent(int pieceId) => PieceId = pieceId;
        public string ToLogString() => $"pieceId={PieceId}";
    }

    public sealed class PieceMoveCompletedEvent : IAnalyticsEvent
    {
        public string Name => "piece_move_completed";
        public int PieceId { get; }
        public bool WasPlaced { get; }
        public PieceMoveCompletedEvent(int pieceId, bool wasPlaced)
        {
            PieceId = pieceId;
            WasPlaced = wasPlaced;
        }
        public string ToLogString() => $"pieceId={PieceId}, wasPlaced={WasPlaced}";
    }

    public sealed class BonusReceivedEvent : IAnalyticsEvent
    {
        public string Name => "bonus_received";
        public string BonusType { get; }
        public int Value { get; }
        public BonusReceivedEvent(string bonusType, int value)
        {
            BonusType = bonusType;
            Value = value;
        }
        public string ToLogString() => $"bonusType={BonusType}, value={Value}";
    }

    public sealed class GameStartedEvent : IAnalyticsEvent
    {
        public string Name => "game_started";
        public bool IsNewGame { get; }
        public GameStartedEvent(bool isNewGame) => IsNewGame = isNewGame;
        public string ToLogString() => $"isNewGame={IsNewGame}";
    }

    public sealed class GameOverEvent : IAnalyticsEvent
    {
        public string Name => "game_over";
        public int Score { get; }
        public int HighScore { get; }
        public GameOverEvent(int score, int highScore)
        {
            Score = score;
            HighScore = highScore;
        }
        public string ToLogString() => $"score={Score}, highScore={HighScore}";
    }

    public sealed class SecondChanceUsedEvent : IAnalyticsEvent
    {
        public string Name => "second_chance_used";
        public string ToLogString() => "no parameters";
    }
}
