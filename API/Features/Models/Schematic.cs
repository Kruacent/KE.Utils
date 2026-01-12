using KE.Utils.API.Features.Models.Blueprints;
using KE.Utils.Extensions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.Features.Models
{
    public class Schematic
    {


        public string name;
        public Vector3 center;
        public Vector3 rotation;


        //private List<SpawnedObject> 
        private HashSet<PrimitiveBlueprint> Primitives = new();




        public BlueprintObject CreateBlueprint(SpawnedObject spawnedObject)
        {
            return null;
        }


        public SpawnedObject SpawnObject<T>(T blueprint) where T : BlueprintObject
        {


            GameObject obj = blueprint.Create();
            return null;

                        
        }




        public bool TryAddElement<T>(T blueprint) where T : BlueprintObject
        {
            if (Primitives.TryAdd(blueprint))
            {
                return true;
            }

            return false;
        }



    }
}
