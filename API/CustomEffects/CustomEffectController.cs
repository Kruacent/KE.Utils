using Exiled.API.Features;
using KE.Utils.API.CustomStats;
using PlayerRoles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KE.Utils.API.CustomEffects
{
    public class CustomEffectController : MonoBehaviour
    {

        private static List<Type> _types { get; } = new();
        public static IReadOnlyCollection<Type> Types => _types;
        private static bool _awaken = false;
        private bool _events = false;
        private ReferenceHub _hub;
        private CustomEffectBase[] _modules;
        private readonly Dictionary<Type, CustomEffectBase> _dictionarizedTypes = new();
        public CustomEffectBase[] Modules
        {
            get
            {

                if (_modules is null || _modules.Length != Types.Count)
                {
                    AssignModules();
                }
                return _modules;
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



        private void AssignModules()
        {
            _modules = new CustomEffectBase[Types.Count];
            for (int i = 0; i < Types.Count; i++)
            {
                object obj = Activator.CreateInstance(_types[i]);
                _modules[i] = obj as CustomEffectBase;
            }
        }

        private void Awake()
        {
            _awaken = true;
            _hub = ReferenceHub.GetHub(base.gameObject);

            Log.Info("adding cps to " + _hub.Network_playerId.Value);
            CustomEffectBase[] modules = Modules;
            foreach (CustomEffectBase module in modules)
            {
                Log.Info("adding " + module.GetType().Name);
                _dictionarizedTypes.Add(module.GetType(), module);
            }

            modules = Modules;
            for (int i = 0; i < modules.Length; i++)
            {
                modules[i].Init(_hub);
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
            //CustomStatBase[] statModules = StatModules;
            //for (int i = 0; i < statModules.Length; i++)
            //{
            //    statModules[i].ClassChanged();
            //}

        }

        private void Update()
        {


        }
        public T GetModule<T>() where T : CustomEffectBase
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

