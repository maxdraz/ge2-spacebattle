using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vector
{
    public struct VectorFunctions
    {
        public static float GetMagnitude(Vector3 me, Vector3 target)
        {
            float d = Mathf.Sqrt(Mathf.Pow(target.x - me.x, 2) 
                + Mathf.Pow(target.y - me.y, 2) 
                + Mathf.Pow(target.z - me.z, 2));
            return d;
        }

        public static float GetDotProduct(Vector3 me, Vector3 target)
        {
            float meMag;
            float targetMag;
            float dot;

            return dot;
        }
    }

}

