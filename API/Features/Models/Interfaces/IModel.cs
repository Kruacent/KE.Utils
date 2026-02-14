using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.Features.Models.Interfaces
{
    public interface IModel
    {

        bool Create(Transform transform);


        void Destroy(Transform parent);
    }
}
