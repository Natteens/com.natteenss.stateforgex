using UnityEngine;

namespace StateForgeX.Editor.Code
{
    public abstract class DrawNode : ScriptableObject
    {
        public abstract void DrawWindow(BaseNode node);
    }
}