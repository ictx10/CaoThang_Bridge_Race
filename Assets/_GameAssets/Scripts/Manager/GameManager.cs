using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Pause }
public class GameManager : Singleton<GameManager>
{
    private GameState _gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    public void ChangeState(GameState state)
    {
        this._gameState = state;
    }
    public bool IsState(GameState state)
    {
        return this._gameState == state;
    }
}
