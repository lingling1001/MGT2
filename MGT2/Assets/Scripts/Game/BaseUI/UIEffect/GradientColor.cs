/*
uGui-Effect-Tool
Copyright (c) 2015 WestHillApps (Hironari Nishioka)
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UiEffect
{
    [AddComponentMenu("UI/Effects/Gradient Color")]
    [RequireComponent(typeof(Graphic))]
    public class GradientColor : BaseMeshEffect
    {
        public enum DIRECTION
        {
            Vertical,
            Horizontal,
            Both,
        }

        public DIRECTION direction = DIRECTION.Both;
        public Color colorTop = Color.white;
        public Color colorBottom = Color.black;
        public Color colorLeft = Color.red;
        public Color colorRight = Color.blue;


        /// <summary>
        /// Refresh Gradient Color on playing.
        /// </summary>
        public void Refresh()
        {
            
            if (graphic != null)
            {
                graphic.SetVerticesDirty();
            }
        }
        int tempCount = 0;
        public override void ModifyMesh(VertexHelper vh)
        {
            tempCount = vh.currentVertCount;
            if (IsActive() == false || tempCount == 0)
            {
                return;
            }
            var vList = new List<UIVertex>();
            for (var i = 0; i < tempCount; i++)
            {
                var vertex = new UIVertex();
                vh.PopulateUIVertex(ref vertex, i);
                vList.Add(vertex);
            }

            float topX = 0f, topY = 0f, bottomX = 0f, bottomY = 0f;
            for (int cnt = 0; cnt < vList.Count; cnt++)
            {
                topX = Mathf.Max(topX, vList[cnt].position.x);
                topY = Mathf.Max(topY, vList[cnt].position.y);
                bottomX = Mathf.Min(bottomX, vList[cnt].position.x);
                bottomY = Mathf.Min(bottomY, vList[cnt].position.y);
            }

            float width = topX - bottomX;
            float height = topY - bottomY;

            UIVertex tempVertex = vList[0];
            for (int i = 0; i < vList.Count; i++)
            {
                tempVertex = vList[i];
                byte orgAlpha = tempVertex.color.a;
                Color colorOrg = tempVertex.color;
                Color colorV = Color.Lerp(colorBottom, colorTop, (tempVertex.position.y - bottomY) / height);
                Color colorH = Color.Lerp(colorLeft, colorRight, (tempVertex.position.x - bottomX) / width);
                switch (direction)
                {
                    case DIRECTION.Both:
                        tempVertex.color = colorOrg * colorV * colorH;
                        break;
                    case DIRECTION.Vertical:
                        tempVertex.color = colorOrg * colorV;
                        break;
                    case DIRECTION.Horizontal:
                        tempVertex.color = colorOrg * colorH;
                        break;
                }
                tempVertex.color.a = orgAlpha;
                //vList[i] = tempVertex;
                vh.SetUIVertex(tempVertex, i);

            }
        }
    }
}
