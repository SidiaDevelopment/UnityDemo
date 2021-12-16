﻿using UnityEngine;

public partial class GameContext
{
    public GameEntity Instantiate(string resource, Transform parent = null)
    {
        var entity = CreateEntity();
        var gameObject = Object.Instantiate(Resources.Load<GameObject>(resource), parent);
        if (gameObject == null) return null;

        gameObject.name = resource;

        var view = gameObject.GetComponent<EntityView>();
        view.Link(Contexts.sharedInstance, entity);

        return entity;
    }
}