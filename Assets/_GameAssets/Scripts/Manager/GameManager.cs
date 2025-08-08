using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Pause }
public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    public void ChangeState(GameState state)
    {
        this._gameState = state;
    }
    public bool IsState(GameState state)
    {
        return this._gameState == state;
    }
}
