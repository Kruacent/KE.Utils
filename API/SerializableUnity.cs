using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API
{
    [Serializable]
    public struct Vec3
    {
        public float x, y, z;

        public static Vec3 From(Vector3 v) =>
            new Vec3 { x = v.x, y = v.y, z = v.z };

        public Vector3 ToUnity() =>
            new Vector3(x, y, z);
    }

    [Serializable]
    public struct Quat
    {
        public float x, y, z, w;

        public static Quat From(Quaternion q) =>
            new Quat { x = q.x, y = q.y, z = q.z, w = q.w };

        public Quaternion ToUnity() =>
            new Quaternion(x, y, z, w);
    }


}
