using UnityEngine;
using UnityEditor;

namespace StateForgeX.Editor.Code
{
    public class StateForgeXWindow : EditorWindow
    { 
        public BehaviourContainer container;
        Vector3 mousePos;
        BaseNode selectedNode;
        public DrawNode textNode;
        private BaseNode link;
        Vector2 scrollPos;
        private readonly Rect scrollAreaSize = new Rect(0, 0, 2000, 2000);
        static EditorWindow window;
        
        enum UserAction
        {
            AddNode,
            DeleteNode,
        }
        
        [MenuItem("Window/StateForgeX")]
        public static void ShowWindow()
        { 
            window = GetWindow<StateForgeXWindow>("StateForgeX Editor");
            window.minSize = new Vector2(800, 600);
        }

        private void OnGUI()
        {
            container = (BehaviourContainer)EditorGUILayout.ObjectField(container, typeof(BehaviourContainer),false);

            GUILayout.BeginArea(new Rect(0,0, window.position.width, window.position.height));
            scrollPos = GUI.BeginScrollView(new Rect(0,0, window.position.width, window.position.height), scrollPos, scrollAreaSize);
            Event e = Event.current;
            mousePos = e.mousePosition;
            UserInput(e);
            DrawLines();
            DrawEditor();
            
            GUI.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawLines()
        {
            if (container != null)
            {
                for (int i = 0; i < container.Nodes.Count; i++)
                {
                    if (container.Nodes[i] != null && container.Nodes[i].nextNode != null)
                    {
                        ConnectLine(container.Nodes[i].NodeRect, container.Nodes[i].nextNode.NodeRect);
                    }
                }

                if (link != null)
                {
                    Vector3 startPos = new Vector3(link.NodeRect.x + link.NodeRect.width,
                        link.NodeRect.y + (link.NodeRect.height * 0.5f), 0);
                    ConnectLineDraw(startPos, mousePos);
                    Repaint();
                }
            }
        }


        private void ConnectLine(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width, start.y + (start.height * 0.5f), 0);
            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            
            ConnectLineDraw(startPos, endPos);
        }

        private static void ConnectLineDraw(Vector3 startPos, Vector3 endPos)
        {
            Vector3 startTan = startPos + (Vector3.right * 50f);
            Vector3 endTan = endPos + (Vector3.right * 50f);
            
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 3);
        }

        private void UserInput(Event e)
        {
            if (container != null)
            {
                if (e.type == EventType.MouseDown && e.button == 1)
                {
                    RightClick(e);
                }

                if (e.type == EventType.MouseUp && e.button == 0)
                {
                    LeftClick(e);
                }
            }
        }
        
        private void RightClick(Event e)
        {
            link = null;
            selectedNode = null;
            CheckClickNode(e);

            if (selectedNode == null)
            {
                ContextMenu(e);
            }
            else
            {
                NodeContextMenu(e);
            }
        }

        private void LeftClick(Event e)
        {
            if (link != null)
            {
                CheckClickNode(e);
                if (selectedNode != null && selectedNode != link)
                {
                    Link();
                }
                else
                {
                    link = null;
                }
            }
        }
        private void NodeContextMenu(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Link Node"), false, () => { BeginLink(); });
            menu.AddItem(new GUIContent("Delete Node"), false, () => { DeleteNode(); });
            menu.ShowAsContext();
            e.Use();
        }
        
        private void ContextMenu(Event e)
        {
           GenericMenu menu = new GenericMenu();
           menu.AddItem(new GUIContent("Add Node"), false, () => { CreateNode(); });
           menu.ShowAsContext();
           e.Use();
        }
        
        void CreateNode()
        {
            container.Nodes.Add(new BaseNode(mousePos, textNode));
        }
        
        void DeleteNode()
        {
            if (selectedNode.nextNode != null)
            {
                selectedNode.nextNode.previousNode = null;
                selectedNode.nextNode = null;
            }

            if (selectedNode.previousNode != null)
            {
                selectedNode.previousNode.nextNode = null;
                selectedNode.previousNode = null;
            }

            container.Nodes.Remove(selectedNode);
        }
        
        private void CheckClickNode(Event e)
        {
            for (int i = 0; i < container.Nodes.Count; i++)
            {
                if (container.Nodes[i].NodeRect.Contains(e.mousePosition))
                {
                    selectedNode = container.Nodes[i];
                    break;
                }
            }
        }
        
        private void DrawEditor()
        {
            BeginWindows();
            if (container != null)
            {
                for (int i = 0; i < container.Nodes.Count; i++)
                {
                    container.Nodes[i].NodeRect = GUI.Window(i, container.Nodes[i].NodeRect, DrawNode, container.Nodes[i].title);
                }
            }
            EndWindows();
        }

        void BeginLink()
        {
            link = selectedNode;
        }

        void Link()
        {
            if (link != selectedNode)
            {
                if(selectedNode.previousNode != null)
                {
                    selectedNode.previousNode.nextNode = null;
                    selectedNode.previousNode = null;
                }
                link.nextNode = selectedNode;
                selectedNode.previousNode = link;
            }
            link = null;
        }
        
        void DrawNode(int id)
        {
            container.Nodes[id].DrawNode();
            GUI.DragWindow();
        }
    }
}
