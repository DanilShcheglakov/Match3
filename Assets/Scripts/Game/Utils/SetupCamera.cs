using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Game.Utils
{
    public class SetupCamera
    {
        private bool _isVertical;

        public void SetCameta(int wight, int hight, bool isVertical)
        {
            _isVertical = isVertical;

            var xPos = wight / 2f - 0.5f;
            var yPos = hight / 2f;

            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOrtoSize(wight, hight);
        }

        private float GetOrtoSize(int wight, int hight)
        {
            return _isVertical 
                ? (wight + 10f) * Screen.height / Screen.width * 0.5f 
                :(hight + 25f ) * Screen.height / Screen.width * 0.5f;
        }
    }
}
