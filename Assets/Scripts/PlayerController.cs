using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    public List<Dictionary<GameObject, float>> interactablesWithDistances;
    private void Start()
    {
        interactablesWithDistances = new List<Dictionary<GameObject, float>>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        var horizontalVelocity = Input.GetAxis("Horizontal");
        var verticalVelocity = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalVelocity * speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalVelocity * speed * Time.deltaTime);
    }

    public GameObject getClosestInteractableGameObject()
    {
        if (interactablesWithDistances.Count == 0)
        {
            return null;
        }

        var closestDistance = float.MaxValue;
        GameObject closestGameObject = null;
        foreach (var interactableWithDistance in interactablesWithDistances)
        {
            foreach (var keyValuePair in interactableWithDistance)
            {
                if (keyValuePair.Value < closestDistance)
                {
                    closestDistance = keyValuePair.Value;
                    closestGameObject = keyValuePair.Key;
                }
            }
        }

        return closestGameObject;
    }
}