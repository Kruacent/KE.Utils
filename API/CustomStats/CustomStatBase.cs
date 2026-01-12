using Exiled.API.Features;
using KE.Utils.API.Displays.DisplayMeow;
using KE.Utils.API.Displays.DisplayMeow.Placements;
using KE.Utils.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.CustomStats
{
    public abstract class CustomStatBase
    {
        public abstract float CurValue { get; set; }

        public abstract float MinValue { get; }

        public abstract float MaxValue { get; set; }

        public float NormalizedValue
        {
            get
            {
                if (MinValue != MaxValue)
                {
                    return (CurValue - MinValue) / (MaxValue - MinValue);
                }

                return 0f;
            }
        }

        public ReferenceHub Hub { get; set; }

        public void AddAmount(float amount)
        {
            CurValue = Mathf.Clamp(CurValue + amount, MinValue, MaxValue);
        }

        public void AddAmount(float amount, float percentageCap)
        {
            float max = MaxValue * Mathf.Clamp01(percentageCap);
            float value = CurValue + amount;
            CurValue = Mathf.Clamp(value, MinValue, max);
        }

        
        public virtual void Init(ReferenceHub ply)
        {
            Hub = ply;
        }

        public virtual void Update()
        {
        }

        public virtual void ClassChanged()
        {
        }

    }
}
