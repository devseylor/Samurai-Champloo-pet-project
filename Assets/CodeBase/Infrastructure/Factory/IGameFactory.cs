using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        List<ISavedProgessReader> ProgessReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        event Action HeroCreated;
        GameObject HeroGameObject { get; }
        void Cleanup();
    }
}