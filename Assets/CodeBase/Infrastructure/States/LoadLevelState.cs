using CodeBase.CameraLogic;
using UnityEngine;
using CodeBase.Logic;
using CodeBase.Infrastructure.Factory;
using System;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI;
using CodeBase.Hero;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgessReader progessReader in _gameFactory.ProgessReaders)
                progessReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            GameObject hero = InitHero();

            InitHud(hero);

            CameraFollow(hero);
        }

        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>() 
                .Consctruct(hero.GetComponent<HeroHealth>());
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(hero);
        }
    }
}