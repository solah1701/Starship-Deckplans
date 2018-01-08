using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Extenders
{
    public static class VectorExtender
    {
        public static bool IsZero(this Vect3 item)
        {
            const double TOLERANCE = 0.0001f;
            return Math.Abs(item.x) < TOLERANCE && Math.Abs(item.y) < TOLERANCE && Math.Abs(item.z) < TOLERANCE;
        }

        public static Vect3 Map(this Vector3 item)
        {
            return new Vect3 {x = item.x, y = item.y, z = item.z};
        }

        public static Vector3 Map(this Vect3 item)
        {
            return new Vector3 {x = item.x, y = item.y, z = item.z};
        }

        public static Vector3 TransformPoint(this Vector3 item, Transform transform)
        {
            return transform.TransformPoint(item);
        }
    }
}
