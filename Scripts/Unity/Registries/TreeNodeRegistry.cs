using CardGame.Core.Tree.Base;
using CardGame.Core.DataStructures;
using CardGame.Unity.Nodes.UI;
using CardGame.Unity.Nodes.Visuals;
using UnityEngine;

namespace CardGame.Unity.Registries
{
    public static class TreeNodeRegistry
    {
        public static readonly Map<TreeNode, TreeNodeVisual> NodeVisualMap = new();
        public static readonly Map<TreeNode, TreeNodeUI> NodeUIMap = new();
    }
}