using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public event Action HeroCreated;

        public List<ISavedProgessReader> ProgessReaders { get; } = new List<ISavedProgessReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        public void CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath);

        public void Cleanup()
        {
            ProgessReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgessReader progessReader in gameObject.GetComponentsInChildren<ISavedProgessReader>())
                Register(progessReader);
        }

        private void Register(ISavedProgessReader progessReader)
        {
            if(progessReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgessReaders.Add(progessReader);
        }
    }
}
