using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

namespace Assets.Scripts.Game.GridSystem
{
    public class GameDebug
    {
        private Grid _grid;

        public GameDebug(Grid grid) =>_grid = grid;
        
        public void ShowDebug(Transform parent)
        {
            for (int x = 0; x < _grid.Wigth; x++)
            {
                for (int i = 0; i < _grid.Height; i++)
                {
                    var text = x + " " + i;
                    CreateDebugText(parent, text, _grid.GridToWorld(x,i));
                }
            }            
        }

        private void CreateDebugText(Transform parent, string text, Vector3 position)
        {
            var debugText = new GameObject(name: "DebugText", typeof(TextMeshPro));

            debugText.transform.SetParent(parent, false);
            debugText.transform.position = position+new Vector3(0f,0f,-3f);
            debugText.transform.forward = Vector3.forward;

            var TMP = debugText.GetComponent<TextMeshPro>();
            TMP.text = text;

            TMP.fontSize = 3f;
            TMP.color = Color.white;
            TMP.alignment = (TextAlignmentOptions)TextAlignment.Center;
        }
    }
}
