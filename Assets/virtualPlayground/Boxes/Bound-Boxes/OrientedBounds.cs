using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DimBoxes
{
    public struct VertexData
    {
        public Vector3[] vertices;
        public Matrix4x4 matrix;
        public VertexData(Vector3[] vert, Matrix4x4 m)
        {
            vertices = vert;
            matrix = m;
        }
    }
    public class OrientedBounds
    {
        #region Fields

        public Vector3 Center;
        public Vector3 Extent;
        public Vector3 Axis1;
        public Vector3 Axis2;
        public Vector3 Axis3;

        #endregion

        #region Constructors

        public OrientedBounds()
        {
            // for serialization
        }

        public OrientedBounds(Vector3 center, Vector3 extent, Vector3 axis1, Vector3 axis2, Vector3 axis3)
        {
            Center = center;
            Extent = extent;
            Axis1 = axis1;
            Axis2 = axis2;
            Axis3 = axis3;
            Debug.Log("ob");
            float a1min = Center.x - extent.x, a1max = Center.x + extent.x,
                a2min = Center.y - extent.y, a2max = Center.y + extent.y,
                a3min = Center.z - extent.z, a3max = Center.z + extent.z;
        }

        #endregion

        public void Enclose(Vector3 point)
        {
            Vector3 newPoint = point - Center;
            Vector3 difc = Vector3.zero;
            float e1 = Vector3.Dot(newPoint, Axis1);
            if (Mathf.Abs(e1) > Extent.x)
            {
                difc.x = 0.5f * (e1 + ((e1 < 0) ? Extent.x : -Extent.x));
                Extent.x = 0.5f * (Extent.x + Mathf.Abs(e1));
            }
            float e2 = Vector3.Dot(newPoint, Axis2);
            if (Mathf.Abs(e2) > Extent.y)
            {

                difc.y = 0.5f * (e2 + ((e2 < 0) ? Extent.y : -Extent.y));
                Extent.y = 0.5f * (Extent.y + Mathf.Abs(e2));
            }
            float e3 = Vector3.Dot(newPoint, Axis3);
            if (Mathf.Abs(e3) > Extent.z)
            {

                difc.z = 0.5f * (e3 + ((e3 < 0) ? Extent.z : -Extent.z));
                Extent.z = 0.5f * (Extent.z + Mathf.Abs(e3));
            }
            Center += difc.x * Axis1 + difc.y * Axis2 + difc.z * Axis3;
        }

        public static Bounds OBB(VertexData[] vdata, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            DateTime dt = DateTime.Now;
            OrientedBounds obb = new OrientedBounds();
            Debug.Log("meshesCount " + vdata.Length.ToString());
            for (int i = 0; i < vdata.Length; i++)
            {
                VertexData ms = vdata[i];
                int vc = vdata[i].vertices.Length;
                //Debug.Log("vertices " + vc.ToString());
                for (int j = 0; j < vc; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        obb = new OrientedBounds(vdata[i].matrix.MultiplyPoint3x4(vdata[i].vertices[j]), Vector3.zero, v1, v2, v3);
                    }
                    else
                    {
                        obb.Enclose(vdata[i].matrix.MultiplyPoint3x4(vdata[i].vertices[j]));
                    }
                }
            }

            TimeSpan ts = DateTime.Now - dt;
            //Debug.Log(ts.ToString());
            Bounds _bounds = new Bounds(obb.Center, 2.0f * obb.Extent);
            return _bounds;
        }

    }
}
