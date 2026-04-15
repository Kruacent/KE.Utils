using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.CustomEffects
{
    public abstract class CustomEffectBase
    {
        private byte _intensity =0;
        public byte Intensity
        {
            get
            {
                return _intensity;
            }
            set
            {

            }
        }


        public bool IsEnabled
        {
            get
            {
                return Intensity > 0;
            }
            set
            {
                if (value != IsEnabled)
                {
                    Intensity = (byte)(value ? 1 : 0);
                }
            }
        }
        private float _duration;

        
        public float Duration
        {
            get
            {
                return _duration;
            }
            private set
            {
                _duration = Mathf.Max(0f, value);
            }
        }
        private float _timeLeft;
        public float TimeLeft
        {
            get
            {
                return _timeLeft;
            }
            set
            {
                _timeLeft = Mathf.Max(0f, value);
                if (_timeLeft == 0f && Duration != 0f)
                {
                    DisableEffect();
                }
            }
        }

        protected virtual void DisableEffect()
        {
            if (NetworkServer.active)
            {
                Intensity = 0;
            }
        }

        private void RefreshTime()
        {
            if (Duration != 0f)
            {
                TimeLeft -= Time.deltaTime;
            }
        }


        protected virtual void Update()
        {
            if (IsEnabled)
            {
                RefreshTime();
            }
        }
        private ReferenceHub _hub;

        public void Init(ReferenceHub hub)
        {
            _hub = hub; 
        }
    }
}
