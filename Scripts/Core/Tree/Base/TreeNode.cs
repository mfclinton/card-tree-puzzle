using System.Collections.Generic;
using CardGame.Core.Tree.Enums;

namespace CardGame.Core.Tree.Base
{
    public class TreeNode
    {
        public NodeType Type { get; set; }
        public List<TreeNode> Children { get; } = new();
        public TreeNode? Parent { get; private set; }
        public bool IsRevealed { get; protected set; }
        
        public TreeNode(NodeType type)
        {
            Type = type;
        }

        public void AddChild(TreeNode child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void Reveal()
        {
            IsRevealed = true;
        }
        
        public virtual void OnEnter() { }
    }
}