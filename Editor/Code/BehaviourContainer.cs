using System.Collections.Generic;
using UnityEngine;

namespace StateForgeX.Editor.Code
{
    [CreateAssetMenu(menuName = "Behaviour Editor/Container")]
    public class BehaviourContainer : ScriptableObject
    {
        public List<BaseNode> Nodes;
    }
}