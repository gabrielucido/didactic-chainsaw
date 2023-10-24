using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CallbackController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    private InventoryController _inventoryController;

    private void Start()
    {
        var gameControllerGameObject = GameObject.Find("GameController");
        _gameController = gameControllerGameObject.GetComponent<GameController>();
        if (_gameController == null)
        {
            throw new NotImplementedException();
        }

        _inventoryController = gameControllerGameObject.GetComponent<InventoryController>();
        if (_inventoryController == null)
        {
            throw new NotImplementedException();
        }
    }

    public void InteractionCallback(EventCodes eventCode, GameObject interactedObject)
    {
        switch (eventCode)
        {
            case EventCodes.PickUpItem:
                _inventoryController.PickUpItem(interactedObject);
                break;
            case EventCodes.EquipItem:
                _inventoryController.EquipItem(interactedObject);
                break;
        }
    }
}