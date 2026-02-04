using Exiled.API.Features;
using KE.Utils.API.CustomStats;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API
{
    public abstract class CustomPlayerStuff<T> : MonoBehaviour
    {


        private static Dictionary<Type, List<Type>> _types = new();
        private bool _events = false;
        private static bool _awaken = false;
        private readonly Dictionary<Type, T> _dictionarizedTypes = new();
        private ReferenceHub _hub;


        private void Start()
        {
            SubscribeEvents();
            _events = true;
        }


        private void OnDestroy()
        {
            if (_events)
            {
                UnsubscribeEvents();
            }
        }

        protected abstract void SubscribeEvents();
        protected abstract void UnsubscribeEvents();




        //private void Awake()
        //{

        //    _awaken = true;
        //    _hub = ReferenceHub.GetHub(base.gameObject);
        //    Log.Info("adding CPS to " + _hub.Network_playerId.Value);
        //    CustomStatBase[] statModules = StatModules;
        //    foreach (CustomStatBase statBase in statModules)
        //    {
        //        Log.Info("adding " + statBase.GetType().Name);
        //        _dictionarizedTypes.Add(statBase.GetType(), statBase);
        //    }

        //    statModules = StatModules;
        //    for (int i = 0; i < statModules.Length; i++)
        //    {
        //        statModules[i].Init(_hub);
        //    }

        //}


        #region modules


        public static bool AddModule<L>() where L : T
        {
            if (_awaken)
            {
                throw new InvalidOperationException("added module after awaken; do not do that");
            }

            Type typeModule = typeof(L);
            Type typeStuff = typeof(T);

            if (!_types.ContainsKey(typeStuff))
            {
                _types.Add(typeStuff, new());
            }

            List<Type> listStuff = _types[typeStuff];
            

            if (listStuff.Contains(typeModule))
            {
                return false;
            }
            listStuff.Add(typeModule);

            return true;
        }

        public T GetModule<T>() where T : CustomStatBase
        {
            return _dictionarizedTypes[typeof(T)] as T;
        }

        public bool TryGetModule<T>(out T module) where T : CustomStatBase
        {
            if (_dictionarizedTypes.TryGetValue(typeof(T), out var value) && value is T val)
            {
                module = val;
                return true;
            }
            module = null;
            return false;
        }


        #endregion


    }
}
