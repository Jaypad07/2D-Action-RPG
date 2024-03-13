public interface IDamageable
{
    void TakeDamage(int damageToTake);
    void Die();
    Character.Team GetTeam();
}