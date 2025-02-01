using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Game/ColorSettings")]
    public class ColorSettings : ScriptableObject
    {
        [SerializeField] private ColorSetup[] colors;

        public IReadOnlyList<ColorSetup> Colors => colors;
    }
}