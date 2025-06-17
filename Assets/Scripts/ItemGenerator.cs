using UnityEngine;
using System.Collections.Generic;

public static class ItemGenerator
{
    private static float baseDamage = 10f;
    private static string[] baseNames = { "Sword", "Axe", "Dagger", "Mace" };

    private static List<Enchantment> possibleEnchantments = new List<Enchantment>
    {
        new Enchantment("Fire", "Adds fire damage to attacks", 1.2f),
        new Enchantment("Ice", "Slows enemies on hit", 1.1f),
        new Enchantment("Poison", "Applies damage over time", 1.15f)
    };

    public static List<Item> GenerateRandomItems(int maxCount)
    {
        int actualCount = Random.Range(1, maxCount + 1);
        List<Item> items = new List<Item>();

        for (int i = 0; i < actualCount; i++)
        {
            items.Add(GenerateSingleItem());
        }

        return items;
    }

    public static Item GenerateSingleItem()
    {
        Item item = new Item();

        item.name = baseNames[Random.Range(0, baseNames.Length)];
        float variation = Random.Range(-0.1f, 0.1f);
        item.damage = baseDamage + baseDamage * variation;

        foreach (var enchantment in possibleEnchantments)
        {
            if (Random.value < 0.3f)
            {
                item.enchantments.Add(enchantment);
                item.damage *= enchantment.damageModifier;
            }
        }

        return item;
    }
}