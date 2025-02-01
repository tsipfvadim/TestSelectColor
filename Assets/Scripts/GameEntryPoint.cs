using Models;
using Scriptables;
using UnityEngine;
using ViewModels;
using Views;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private ColorSettings settings;
    [SerializeField] private GameView gameView;
    [SerializeField] private UIView uiView;

    private Game game;
    private GameViewModel gameViewModel;
    private UIViewModel uiViewModel;

    private void Start()
    {
        game = new Game(3);
        var generator = new RoundGenerator(settings);
        uiViewModel = new UIViewModel(game);
        gameViewModel = new GameViewModel(game, generator);
        gameViewModel.StartRound();
        
        uiView.Init(uiViewModel);
        gameView.Init(gameViewModel);
    }

    private void OnDestroy()
    {
        uiView.Release();
        gameView.Release();
        
        uiViewModel.Dispose();
        gameViewModel.Dispose();
    }
}
