using UnityEngine;

namespace StateForgeX.Editor.Code
{
    [System.Serializable]
    public class BaseNode
    {
        public Rect NodeRect;
        public string title;
        
        public DrawNode drawNode;
        
        public BaseNode nextNode;
        public BaseNode previousNode;
        
        public BaseNode(Vector3 mousePosition, DrawNode drawNode)
        {
            NodeRect = new Rect(mousePosition.x, mousePosition.y, 200, 140);
            this.drawNode = drawNode;
        }
        public void DrawNode()
        {
            if (drawNode != null)
            {
                drawNode.DrawWindow(this);
            }
        }
    }
}