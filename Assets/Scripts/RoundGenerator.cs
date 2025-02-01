using System.Collections.Generic;
using System.Linq;
using Models;
using Scriptables;
using UnityEngine;

public class RoundGenerator
{
    private readonly ColorSettings colorSettings;

    public RoundGenerator(ColorSettings settings)
    {
        colorSettings = settings;
    }

    public Round Generate(int count)
    {
        var colors = new HashSet<ColorSetup>();

        for (var i = 0; i < count; i++)
        {
            var color = GetRandomColor(colors);
            colors.Add(color);
        }

        var items = colors.Select(c => new Item(c.ColorName, c.Color)).ToList();
        var correctItem = items[Random.Range(0, items.Count)];
        return new Round(correctItem, items);
    }

    private ColorSetup GetRandomColor(HashSet<ColorSetup> items)
    {
        var colorSetting = colorSettings.Colors[Random.Range(0, colorSettings.Colors.Count)];

        return items.Contains(colorSetting)
            ? GetRandomColor(items)
            : colorSetting;
    }
}