using System;
using Entitas;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public ScriptableObject GameConfig;

    private Contexts _contexts;
    private Systems _systems;

    private void Awake()
    {
        _contexts = Contexts.sharedInstance;

        _contexts.config.SetGameConfig((IGameConfig)GameConfig);
    }

    private void Start()
    {
        _systems = new DemoFeatures(_contexts);
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    private void OnDestroy()
    {
        _systems.TearDown();
    }
}