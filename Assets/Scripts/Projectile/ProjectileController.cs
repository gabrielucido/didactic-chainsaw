using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Sprites;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] public bool goingLeft;
    [SerializeField] private float velocity = 20f;
    [SerializeField] private float autoDestroyTimer = 10f;

    private Rigidbody2D _rigidiBody2D;
    private Animator _animator;
    private bool _hit;

    void Awake()
    {
        _rigidiBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.flipX = !goingLeft;
        }
        _animator.SetTrigger("MuzzleFlashTrigger");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            _hit = true;
        }
    }

    void Update()
    {
        if (_hit)
        {
            _rigidiBody2D.velocity = Vector2.zero;
            // _animator.SetTrigger("ExplosionTrigger");     
            DestroyMe();
        }
        else
        {
            _rigidiBody2D.velocity = new Vector2(goingLeft ? -velocity : velocity, _rigidiBody2D.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        // Prevent the projectile from living forever if not hit anything
        if (autoDestroyTimer >= 0)
        {
            autoDestroyTimer -= Time.fixedDeltaTime;
        }
        else
        {
            DestroyMe();
        }
    }
    
    

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}