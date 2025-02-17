public class Player {
    public string Name;
    public string Role;
    public int MaxHealth;
    public int CurrHealth;
    public int AttackDamage = 10;
    
    public Player(string name, string role, int maxHealth=100, int attackDamage=10) {
        Name = name;
        Role = role;
        MaxHealth = maxHealth;
        CurrHealth = maxHealth;
        AttackDamage = attackDamage;
    }

    public void TakeDamage(int ReceivedDamage) => CurrHealth -= ReceivedDamage;
    public void Heal(int ReceivedHealing) {
        if ((CurrHealth + ReceivedHealing) > MaxHealth) {
            CurrHealth = MaxHealth;
        } else {
            CurrHealth += ReceivedHealing;
        }
    }

    public override string ToString()
    {
        return $"Name: {Name}, Role: {Role}, Max Health: {MaxHealth}, Current Health: {CurrHealth}, Attack Damage: {AttackDamage}";
    }
}