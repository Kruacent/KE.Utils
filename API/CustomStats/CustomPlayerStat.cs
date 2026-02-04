using Exiled.API.Features;
using Exiled.API.Features.Pools;
using KE.Utils.API.CustomStats.GUI;
using PlayerRoles;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KE.Utils.API.CustomStats
{
    public class CustomPlayerStat : MonoBehaviour
    {

        private static List<Type> _types { get; } = new();
        public static IReadOnlyCollection<Type> Types => _types;
        private static bool _awaken = false;
        private bool _events = false;
        private ReferenceHub _hub;
        private CustomStatBase[] _statModules = null;
        private readonly Dictionary<Type, CustomStatBase> _dictionarizedTypes = new();
        public CustomStatBase[] StatModules
        {
            get
            {

                if (_statModules is null)
                {
                    AssignStatModules();
                }
                return _statModules;
            }
        }


        private CustomStatBar[] _statBars = null;
        public CustomStatBar[] StatBars
        {
            get
            {
                if(_statBars is null)
                {
                    CustomStatBase[] stats = StatModules;

                    List<CustomStatBar> list = ListPool<CustomStatBar>.Pool.Get();

                    for (int i = 0; i < stats.Length; i++)
                    {
                        if (stats[i] is CustomStatBar bar)
                        {
                            list.Add(bar);
                        }
                    }

                    _statBars = ListPool<CustomStatBar>.Pool.ToArrayReturn(list);

                }


                return _statBars;
            }
        }



        /// <summary>
        /// add your modules BEFORE giving the stats to player
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool AddModule<T>() where T : CustomStatBase
        {
            if (_awaken)
            {
                throw new InvalidOperationException("added module after awaken; do not do that");
            }
            Type type = typeof(T);
            if (_types.Contains(type))
            {
                return false;
            }
            _types.Add(type);

            return true;
        }

        

        private void AssignStatModules()
        {
            _statModules = new CustomStatBase[Types.Count];
            for (int i = 0; i < Types.Count; i++)
            {
                object obj = Activator.CreateInstance(_types[i]);
                _statModules[i] = obj as CustomStatBase;
            }
        }

        private void Awake()
        {

            _awaken = true;
            _hub = ReferenceHub.GetHub(base.gameObject);
            _hub.gameObject.AddComponent<CustomStatBarManager>();
            
            Log.Debug("adding cps to " + _hub.Network_playerId.Value);
            CustomStatBase[] statModules = StatModules;
            foreach (CustomStatBase statBase in statModules)
            {
                Log.Debug("adding " + statBase.GetType().Name);
                _dictionarizedTypes.Add(statBase.GetType(), statBase);
            }
            
            statModules = StatModules;
            for (int i = 0; i < statModules.Length; i++)
            {
                statModules[i].Init(_hub);
            }
        }

        private void Start()
        {
            PlayerRoleManager.OnRoleChanged += OnClassChanged;
            _events = true;
        }

        private void OnDestroy()
        {
            if (_events)
            {
                PlayerRoleManager.OnRoleChanged -= OnClassChanged;
            }
        }

        private void OnClassChanged(ReferenceHub userHub, PlayerRoleBase prevRole, PlayerRoleBase newRole)
        {
            CustomStatBase[] statModules = StatModules;
            for (int i = 0; i < statModules.Length; i++)
            {
                statModules[i].ClassChanged();
            }
            
        }

        private void Update()
        {
            CustomStatBase[] statModules = StatModules;
            for (int i = 0; i < statModules.Length; i++)
            {
                statModules[i].Update();
            }

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

    }
}
