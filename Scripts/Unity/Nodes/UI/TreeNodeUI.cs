using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Unity.Nodes.UI
{
    public class TreeNodeUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void SetConfig(TreeNodeUIConfig config)
        {
            SetColor(config.color);
        }

        private void SetColor(Color color)
        {
            image.color = color;
        }
    }
}