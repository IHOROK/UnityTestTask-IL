using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Analytics;
using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class ScoreMediator : MonoBehaviour
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private Board board;
        [SerializeField] private ScoreBoosterView boosterView;
        
        private readonly ScoreBoostSystem _scoreBoostSystem = new();

        private void Awake()
        {
            board.OnFigurePlaced += FigurePlaced;
        }

        private void Start()
        {
            ScoreSystem.LoadScore();
            _scoreBoostSystem.IsBoosted = ScoreSystem.IsBoosterEnabled;
            boosterView.SetCombo(_scoreBoostSystem.ComboCount, _scoreBoostSystem.State);
            boosterView.SetBoosterEnabled(ScoreSystem.IsBoosterEnabled);
            scoreView.UpdateScore(false);
        }

        public void ResetScore()
        {
            ScoreSystem.ResetScore();
            _scoreBoostSystem.Reset();
            boosterView.SetCombo(_scoreBoostSystem.ComboCount, _scoreBoostSystem.State);
            UpdateView();
        }

        private void FigurePlaced(ClearResult result)
        {
            bool wasBoosterEnabled = _scoreBoostSystem.IsBoosted;
            int previousComboCount = _scoreBoostSystem.ComboCount;
            _scoreBoostSystem.FigurePlaced(result.FiguresRemovedCount);
            Debug.Log($"Combo: {_scoreBoostSystem.ComboCount} ({_scoreBoostSystem.State})");
            if (result.FiguresRemovedCount > 0)
            {
                boosterView.SetCombo(previousComboCount, _scoreBoostSystem.State);
                boosterView.SetArrivalComboState(_scoreBoostSystem.State);
            }
            else
            {
                boosterView.SetCombo(_scoreBoostSystem.ComboCount, _scoreBoostSystem.State);
            }
            boosterView.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            ScoreSystem.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            if (!wasBoosterEnabled && _scoreBoostSystem.IsBoosted)
                AnalyticsService.Instance?.Track(new BonusReceivedEvent("combo_booster", _scoreBoostSystem.ComboCount));
            int comboMultiplier = _scoreBoostSystem.State == ComboState.Inactive
                ? 1
                : _scoreBoostSystem.ComboCount;
            ScoreSystem.AddSetScore(result.ClearedCount, comboMultiplier);
            scoreView.UpdateScore();
        }

        private void UpdateView()
        {
            boosterView.SetBoosterEnabled(false);
            _scoreBoostSystem.Reset();
            boosterView.SetCombo(_scoreBoostSystem.ComboCount, _scoreBoostSystem.State);
            scoreView.UpdateScore(false);
        }
    }
}
