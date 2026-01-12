using AdminToys;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Extensions;
using UnityEngine;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;

namespace KE.Utils.API.Features.Models.Blueprints
{
    public class PrimitiveBlueprint : BlueprintObject
    {

        public PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Cube;

        public Color32 Color { get; set; } = new Color32(255,0,0,0);

        public PrimitiveFlags PrimitiveFlags { get; set; } = PrimitiveFlags.Collidable | PrimitiveFlags.Visible;
        public override GameObject Create()
        {
            PrimitiveObjectToy prim = PrimitiveObjectToy.Create(null, false);


            Vector3 position = Position;
            Vector3 rotation = Rotation;

            prim.Transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
            prim.Transform.localScale = Scale;
            prim.MovementSmoothing = 60;
            prim.Color = Color;
            prim.Type = PrimitiveType;
            prim.Flags = PrimitiveFlags;




            return prim.GameObject;


        }
    }
}
