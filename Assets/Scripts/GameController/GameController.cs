using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameModeController _gameModeController;
    
    void Start()
    {
        _gameModeController = GetComponent<GameModeController>();
    }

    void Update()
    {
    }
}