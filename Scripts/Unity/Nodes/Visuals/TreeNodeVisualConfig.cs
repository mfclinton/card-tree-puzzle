using System;
using UnityEngine;

namespace CardGame.Unity.Nodes.Visuals
{
    [Serializable]
    public class TreeNodeVisualConfig
    {
        [Header("Transition Values")]
        public float doorAlpha = 1f;
        public float roadAlpha = 1f;
        public float lightIntensity = 1f;
    }
} 