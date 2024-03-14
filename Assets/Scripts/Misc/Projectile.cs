using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private Character.Team _team;
    private Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        rig.velocity = transform.up * speed;
    }

    public void SetTeam(Character.Team t)
    {
        _team = t;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null && damageable.GetTeam() != _team)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
