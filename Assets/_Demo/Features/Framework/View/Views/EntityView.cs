using Entitas.Unity;
using UnityEngine;

public class EntityView : MonoBehaviour, IDestroyedListener
{
    protected Contexts _contexts;
    protected GameEntity _entity;

    public virtual void Link(Contexts contexts, GameEntity entity)
    {
        _contexts = contexts;
        _entity = entity;

        gameObject.Link(entity);
        _entity.AddView(this);
            
        _entity.AddDestroyedListener(this);
    }

    public void OnDestroyed(GameEntity entity)
    {
        gameObject.Unlink();
        Destroy(gameObject);
    }
}