using Exiled.API.Features;
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
        private readonly Dictionary<Type, CustomStatBase> _dictionarizedTypes = new();
        private static bool _awaken = false;
        private static bool _events = false;
        private ReferenceHub _hub;
        private CustomStatBase[] _statModules;
        public CustomStatBase[] StatModules
        {
            get
            {

                if (_statModules is null || _statModules.Length != Types.Count)
                {
                    AssignStatModules();
                }
                return _statModules;
            }
        }

        /// <summary>
        /// add your modules BEFORE giving the stats to player
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool AddModule<T>()
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
            Log.Info("adding cps to " + _hub.Network_playerId.Value);
            CustomStatBase[] statModules = StatModules;
            foreach (CustomStatBase statBase in statModules)
            {
                Log.Info("adding " + statBase.GetType().Name);
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
