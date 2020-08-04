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
    [AddComponentMenu("UI/Effects/Blend Color")]
    [RequireComponent(typeof(Graphic))]
    public class BlendColor : BaseMeshEffect
    {
        public enum BLEND_MODE
        {
            Multiply,
            Additive,
            Subtractive,
            Override,
        }

        public BLEND_MODE blendMode = BLEND_MODE.Multiply;
        public Color color = Color.grey;

       

        public void ModifyVertices(List<UIVertex> vList)
        {

        }

        /// <summary>
        /// Refresh Blend Color on playing.
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

            UIVertex tempVertex = vList[0];
            for (int i = 0; i < vList.Count; i++)
            {
                tempVertex = vList[i];
                byte orgAlpha = tempVertex.color.a;
                switch (blendMode)
                {
                    case BLEND_MODE.Multiply:
                        tempVertex.color *= color;
                        break;
                    case BLEND_MODE.Additive:
                        tempVertex.color += color;
                        break;
                    case BLEND_MODE.Subtractive:
                        tempVertex.color -= color;
                        break;
                    case BLEND_MODE.Override:
                        tempVertex.color = color;
                        break;
                }
                tempVertex.color.a = orgAlpha;
                //vList[i] = tempVertex;
                vh.SetUIVertex(tempVertex, i);
            }


        }
    }
}
