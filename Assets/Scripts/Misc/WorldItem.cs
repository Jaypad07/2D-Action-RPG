using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private float spawnForce;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioClip pickupSFX;

    private ItemData itemToGive;

    private void Start()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        
        rig.AddForce(Random.insideUnitCircle * spawnForce, ForceMode2D.Impulse);
    }

    public void SetItem(ItemData item)
    {
        itemToGive = item;
        _spriteRenderer.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemToGive);
            AudioManager.Instance.PlayPlayerSound(pickupSFX);
            Destroy(gameObject);
        }
    }
}
