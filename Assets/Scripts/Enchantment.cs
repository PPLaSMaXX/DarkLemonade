public class Enchantment
{
    public string name;
    public string description;
    public float damageModifier;

    public Enchantment(string name, string description, float damageModifier)
    {
        this.name = name;
        this.description = description;
        this.damageModifier = damageModifier;
    }
}