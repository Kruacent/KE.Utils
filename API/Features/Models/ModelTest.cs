using Exiled.API.Features.Toys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Light = Exiled.API.Features.Toys.Light;

namespace KE.Utils.API.Features.Models
{
    public class ModelTest : ModelBase
    {
        private static Vector3 pillar = new Vector3(.15f, 1f, 0.15f);
        private static Vector3 support = new Vector3(1, .25f, 1);
        private static Vector3 glass = new Vector3(1, 1.75f, 1);
        private static float positionPillars = 2.6f;

        private static Color32 centralColor = new Color32(60, 255, 255, 255);
        private static Color32 colorSupport = new Color32(80, 80, 80, 255);
        private static Color32 glassColor = new Color32(74, 232, 255, 50);
        protected override void CreateModel(Transform parent)
        {

            var glassPrim = CreatePrimitive(parent, PrimitiveType.Cube, Vector3.zero, Quaternion.identity, glass, glassColor);

            var topSupport = CreatePrimitive(parent, PrimitiveType.Cube, Vector3.up, Quaternion.identity, support, colorSupport);
            var bottomSupport = CreatePrimitive(parent, PrimitiveType.Cube, Vector3.down, Quaternion.identity, support, colorSupport);

            var centralPillar = CreatePrimitive(parent, PrimitiveType.Cylinder, Vector3.zero, Quaternion.identity, pillar, centralColor);
            var pillar1 = CreatePrimitive(centralPillar.transform, PrimitiveType.Cylinder, positionPillars* new Vector3(1, 0, 1), Quaternion.identity, Vector3.one, colorSupport);
            var pillar2 = CreatePrimitive(centralPillar.transform, PrimitiveType.Cylinder, positionPillars * new Vector3(-1, 0, 1), Quaternion.identity, Vector3.one, colorSupport);
            var pillar3 = CreatePrimitive(centralPillar.transform, PrimitiveType.Cylinder, positionPillars * new Vector3(-1, 0, -1), Quaternion.identity, Vector3.one, colorSupport);
            var pillar4 = CreatePrimitive(centralPillar.transform, PrimitiveType.Cylinder, positionPillars * new Vector3(1, 0, -1), Quaternion.identity, Vector3.one, colorSupport);

            Light light = CreateLight(parent, Vector3.zero, Quaternion.identity, Vector3.one, centralColor, LightType.Point, .1f);
        }
    }
}
