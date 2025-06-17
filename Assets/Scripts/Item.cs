using System.Collections.Generic;
using System.Text;

public class Item
{
    public string name;
    public float damage;
    public List<Enchantment> enchantments = new List<Enchantment>();

    public string GetFullName()
    {
        if (enchantments.Count == 0)
            return name;

        StringBuilder fullName = new StringBuilder(name);
        foreach (Enchantment ench in enchantments)
        {
            fullName.Append(" of ").Append(ench.name);
        }

        return fullName.ToString();
    }

    public string GetDescription()
    {
        if (enchantments.Count == 0)
            return $"Damage: {damage:F1}";

        StringBuilder description = new StringBuilder($"Damage: {damage:F1}\nEnchantments:");
        foreach (Enchantment ench in enchantments)
        {
            description.Append($"\n- {ench.name}: {ench.description}");
        }

        return description.ToString();
    }
}