using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.API.Structs;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization.NamingConventions;
using Light = Exiled.API.Features.Toys.Light;

namespace KE.Utils.API.Features.Models
{
    public abstract class ModelBase
    {

        private HashSet<Transform> Transform = new();

        public void Create(Transform parent)
        {
            try
            {
                CreateModel(parent);
                Transform.Add(parent);
            }
            catch(Exception e)
            {
                Log.Error(e);
            }
        }

        public void Destroy(Transform parent)
        {
            NetworkServer.Destroy(parent.gameObject);
        }


        protected abstract void CreateModel(Transform parent);




        private static PrimitiveSettings baseSettings = new(PrimitiveType.Cube, AdminToys.PrimitiveFlags.Visible, Color.white, Vector3.zero, Vector3.zero, Vector3.one, false);
        protected static Primitive CreatePrimitive(Transform parent, PrimitiveType type, Vector3 localPosition,Quaternion localRotation,Vector3 localScale,Color32 color)
        {
            Primitive prim = Primitive.Create(baseSettings);
            

            prim.Transform.parent = parent;
            prim.Type = type;
            prim.Transform.localPosition = localPosition;
            prim.Transform.localRotation = localRotation;
            prim.Transform.localScale = localScale;
            prim.Color = color;
            prim.MovementSmoothing = 0;
            prim.Spawn();

            return prim;
        }

        protected static Light CreateLight(Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Color32 color,LightType lightType,float intensity)
        {
            Light light = Light.Create(null, null, null, false);


            light.Transform.parent = parent;
            light.Intensity = intensity;
            light.LightType = lightType;
            light.Transform.localPosition = localPosition;
            light.Transform.localRotation = localRotation;
            light.Transform.localScale = localScale;
            light.Color = color;
            light.MovementSmoothing = 0;

            light.Spawn();

            return light;
        }


    }
}
