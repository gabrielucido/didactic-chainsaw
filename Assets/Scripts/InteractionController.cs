// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
// using Utils;
//
// public class InteractionController : MonoBehaviour
// {
//     [SerializeField] private Sprite enabledSprite;
//     [SerializeField] private Sprite disabledSprite;
//     private Sprite _overlaySprite;
//     [SerializeField] private Vector3 offset = new Vector3(0, 1.2f, 0);
//     [SerializeField] private EventCodes eventCode = EventCodes.Unknown;
//
//     private bool _isInteractable = false;
//     private GameObject _imageObject;
//     private GameObject _overlayCanvas;
//     private Camera _mainCamera;
//     private CallbackController _callbackController;
//     private InventoryController _invetoryController;
//
//     private void Start()
//     {
//         if (enabledSprite == null || disabledSprite == null)
//         {
//             throw new NotImplementedException("Interactable Objects must have a overlay sprite set!");
//         }
//
//         if (Camera.main == null)
//         {
//             throw new NotImplementedException("Main Camera not found!");
//         }
//
//         if (eventCode == EventCodes.Unknown)
//         {
//             throw new NotImplementedException("Event Code not set!");
//         }
//
//         _invetoryController = GameObject.Find("GameController").GetComponent<InventoryController>();
//         if (_invetoryController == null)
//         {
//             throw new NotImplementedException("Cannot Find Inventory Controller");
//         }
//
//         _mainCamera = Camera.main;
//         _overlayCanvas = GetComponentInChildren<Canvas>().gameObject;
//         _imageObject = _overlayCanvas.gameObject.GetComponentInChildren<Image>().gameObject;
//         _imageObject.GetComponent<Image>().sprite = enabledSprite;
//         _overlayCanvas.SetActive(false);
//         _callbackController = GetComponent<CallbackController>();
//     }
//
//     private void Update()
//     {
//         HandleInteraction();
//         UpdateLayoutHintPosition();
//         UpdateDisabledInteractionSprite();
//     }
//
//     public void UpdateDisabledInteractionSprite()
//     {
//         if (_invetoryController.getEquipedItem() != null)
//         {
//             _overlaySprite = disabledSprite;
//         }
//         else
//         {
//             _overlaySprite = enabledSprite;
//         }
//         _imageObject.GetComponent<Image>().sprite = _overlaySprite;
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (ValidateTrigger(other))
//         {
//             var playerController = other.gameObject.GetComponent<PlayerController>()!;
//             playerController.interactablesWithDistances.Add(getNewInteractableWithDistanceDict(other));
//
//             var closestInteractableGameObject = playerController.getClosestInteractableGameObject();
//             if (closestInteractableGameObject == gameObject)
//             {
//                 EnableInteraction();
//             }
//         }
//     }
//
//     private void OnTriggerStay2D(Collider2D other)
//     {
//         if (ValidateTrigger(other))
//         {
//             var playerController = other.gameObject.GetComponent<PlayerController>()!;
//             playerController.interactablesWithDistances.RemoveAll(x => x.ContainsKey(gameObject));
//             playerController.interactablesWithDistances.Add(getNewInteractableWithDistanceDict(other));
//
//             var closestInteractableGameObject = playerController.getClosestInteractableGameObject();
//             if (closestInteractableGameObject == gameObject)
//             {
//                 EnableInteraction();
//             }
//             else
//             {
//                 DisableInteraction();
//             }
//         }
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (ValidateTrigger(other))
//         {
//             var playerController = other.gameObject.GetComponent<PlayerController>()!;
//             playerController.interactablesWithDistances.RemoveAll(x => x.ContainsKey(gameObject));
//             DisableInteraction();
//         }
//     }
//
//
//     private bool ValidateTrigger(Collider2D other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             var playerController = other.gameObject.GetComponent<PlayerController>();
//             if (playerController == null)
//             {
//                 throw new NotImplementedException("PlayerController not found on Player!");
//             }
//
//             return true;
//         }
//
//         return false;
//     }
//
//     private Vector3 GetUiActionHintPosition()
//     {
//         return _mainCamera.WorldToScreenPoint(transform.position + offset);
//     }
//
//     private void EnableInteraction()
//     {
//         _overlayCanvas.SetActive(true);
//         _isInteractable = true;
//     }
//
//     private void DisableInteraction()
//     {
//         _overlayCanvas.SetActive(false);
//         _isInteractable = false;
//     }
//
//     private void UpdateLayoutHintPosition()
//     {
//         if (_isInteractable)
//         {
//             _imageObject.transform.position = GetUiActionHintPosition();
//         }
//     }
//
//     private void HandleInteraction()
//     {
//         if (_isInteractable && Input.GetButtonDown("Interact"))
//         {
//             _callbackController.InteractionCallback(eventCode, gameObject);
//         }
//     }
//
//     private Dictionary<GameObject, float> getNewInteractableWithDistanceDict(Collider2D other)
//     {
//         return new Dictionary<GameObject, float>()
//         {
//             {
//                 gameObject,
//                 Vector3.Distance(gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.position,
//                     other.gameObject.transform.position)
//             }
//         };
//     }
// }