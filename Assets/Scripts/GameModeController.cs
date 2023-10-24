using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameModeController : MonoBehaviour
{
    [SerializeField] private GameModes _gameMode = GameModes.Unknown;

    void Start()
    {
        _gameMode = GameModes.Cruising;
    }

    public void SetGamemode(GameModes gameMode)
    {
        _gameMode = gameMode;
    }

    public GameModes GetGamemode(GameModes gameMode)
    {
        return _gameMode;
    }
}