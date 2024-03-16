using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class Character : MonoBehaviour, IDamageable
{
    
    public enum Team
    {
        Player,
        Enemy
    }
    public string DisplayName;
    public int CurHp;
    public int MaxHp;

    [SerializeField] protected Team team;

    [Header("Audio")]
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioClip hitSFX;

    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;
    
    public virtual void TakeDamage(int damageToTake)
    {
        CurHp -= damageToTake;
        
        _audioSource.PlayOneShot(hitSFX);
        
        onTakeDamage?.Invoke();

        if (CurHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public Team GetTeam()
    {
        return team;
    }

    public virtual void Heal(int healAmount)
    {
        CurHp += healAmount;

        if (CurHp > MaxHp)
        {
            CurHp = MaxHp;
        }
        
        onHeal?.Invoke();
    }
}
