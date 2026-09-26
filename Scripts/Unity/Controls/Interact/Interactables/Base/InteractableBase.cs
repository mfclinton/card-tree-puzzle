using System;
using CardGame.Unity.Controls.Input.Enums;
using UnityEngine;

namespace CardGame.Unity.Controls.Interact.Interactables.Base
{
    public abstract class InteractableBase : MonoBehaviour
    {
        public bool IsInteractable { get; set; } = true;
    }
}