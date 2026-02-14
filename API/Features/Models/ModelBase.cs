using AdminToys;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.API.Structs;
using KE.Utils.API.Features.Models.Interfaces;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Light = Exiled.API.Features.Toys.Light;

namespace KE.Utils.API.Features.Models
{
    public abstract class ModelBase : IModel
    {

        protected HashSet<Transform> Transform = new();

        public virtual bool Create(Transform parent)
        {
            try
            {
                CreateModel(parent);
                
                return Transform.Add(parent);
            }
            catch(Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public virtual void Destroy(Transform parent)
        {
            NetworkServer.Destroy(parent.gameObject);
        }


        protected abstract void CreateModel(Transform parent);




        public static readonly PrimitiveSettings baseSettings = new(PrimitiveType.Cube, AdminToys.PrimitiveFlags.Visible, Color.white, Vector3.zero, Vector3.zero, Vector3.one, false);
        protected static PrimitiveObjectToy CreatePrimitive(Transform parent, PrimitiveType type, Vector3 localPosition,Quaternion localRotation,Vector3 localScale,Color32 color)
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

            return prim.Base;
        }

        protected static PrimitiveObjectToy CreateEmptyPrimitive(Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
        {
            Primitive prim = Primitive.Create(baseSettings);


            prim.Transform.parent = parent;
            prim.Transform.localPosition = localPosition;
            prim.Transform.localRotation = localRotation;
            prim.Transform.localScale = localScale;
            prim.MovementSmoothing = 0;
            prim.Flags = PrimitiveFlags.None;

            prim.Spawn();

            return prim.Base;
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
