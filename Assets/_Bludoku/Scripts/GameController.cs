using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Analytics;
using _Bludoku.Scripts.Core;
using _Bludoku.Scripts.Score;
using _Bludoku.Scripts.UI;
using UnityEngine;

namespace _Bludoku.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [SerializeField] private ScoreMediator scoreMediator;
        [SerializeField] private UIMediator uiMediator;
        [SerializeField] private Board board;
        [SerializeField] private FiguresController figuresController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            figuresController.OnGameOver += HandleGameOver;

            board.LoadGrid();
            figuresController.LoadFigures();
            AnalyticsService.Instance?.Track(new GameStartedEvent(false));
        }

        public void NewGame()
        {
            BoardSaveLoad.Delete();
            board.ResetBoard();
            figuresController.ResetFigures();
            uiMediator.HideGameOver();
            scoreMediator.ResetScore();
            AnalyticsService.Instance?.Track(new GameStartedEvent(true));
        }

        public void SecondChance()
        {
            uiMediator.HideGameOver();
            figuresController.UpdateToEasyFigures();
            AnalyticsService.Instance?.Track(new SecondChanceUsedEvent());
        }

        private void HandleGameOver()
        {
            AnalyticsService.Instance?.Track(new GameOverEvent(ScoreSystem.Score, ScoreSystem.HighScore));
            uiMediator.ShowGameOver();
        }
    }
}
