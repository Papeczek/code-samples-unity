using MoreMountains.Tools;
using UnityEngine;
public class AiDecisionOnCollision : AIDecision
{
    [SerializeField] private AICollisionDetector collisionDetector;
    private bool hit = false;
    public override bool Decide()
    {
        return hit;
    }

    private void CollisionDetector_OnCollide()
    {
        hit = true;
    }
    public override void OnExitState()
    {
        base.OnExitState();
        hit = false;
    }
    public override void Initialization()
    {
        base.Initialization();
        collisionDetector.OnCollide += CollisionDetector_OnCollide;
    }
    private void OnDestroy()
    {
        collisionDetector.OnCollide -= CollisionDetector_OnCollide;
    }
}
