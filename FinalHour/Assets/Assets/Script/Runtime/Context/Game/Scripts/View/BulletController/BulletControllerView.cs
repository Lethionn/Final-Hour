  using Assets.Script.Runtime.Context.Game.Scripts.Enum;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Game.Scripts.View.BulletController
{
  public class BulletControllerView : EventView
  {
    public Rigidbody2D bulletRigidBody;

    private bool _raycast = true;

    protected override void OnEnable()
    {
      bulletRigidBody.linearVelocity = new Vector2(GameMechanicSettings.BulletSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("FireBarrier") || (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && _raycast))
      {
        gameObject.SetActive(false);
      }
      
      if (other.gameObject.layer == LayerMask.NameToLayer("FireRaycast"))
      {
        _raycast = false;
      }
    }
  }
}
