using UnityEngine;

namespace StateForgeX.Editor.Code
{
    [CreateAssetMenu(menuName = "Behaviour Editor/Draw/Text Node")]
    public class TextNode : DrawNode
    {
        public override void DrawWindow(BaseNode node)
        {
            node.NodeRect.height = 140;
            node.NodeRect.width = 200;
        }
    }
}