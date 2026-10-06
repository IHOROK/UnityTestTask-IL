namespace _Bludoku.Scripts.Score
{
    public enum ComboState
    {
        Inactive,
        Active,
        Warning,
        Critical
    }

    public class ScoreBoostSystem
    {
        private int _movesCount;
        private int _comboCount;
        
        private const int MovesThreshold = 3;
        private const int BoostCombo = 2;
        
        public bool IsBoosted
        {
            get => _comboCount >= BoostCombo;
            set
            {
                if (value)
                {
                    _comboCount = BoostCombo;
                }
                else
                {
                    _comboCount = 0;
                }
            }
        }

        public int ComboCount => _comboCount;

        public ComboState State => _comboCount < BoostCombo
            ? ComboState.Inactive
            : _movesCount switch
            {
                0 => ComboState.Active,
                1 => ComboState.Warning,
                _ => ComboState.Critical
            };

        public void FigurePlaced(int clearedGroups)
        {
            if (clearedGroups <= 0)
            {
                _movesCount++;
            }
            else
            {
                _movesCount = 0;
                _comboCount += clearedGroups;
            }
            
            if (_movesCount >= MovesThreshold)
            {
                _comboCount = 0;
            }
        }

        public void Reset()
        {
            _movesCount = 0;
            _comboCount = 0;
        }
    }
}
