using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMove : MonoBehaviour
{
    protected Vector3 velocity;
    protected Vector2 moveDirection;

    protected SpriteRenderer rend;
    protected Animator anim;
    protected ActorController controller;
    protected EnemyPatrol patrol;
    protected EnemyChase chase;

    protected virtual void Start ()
    {
		
	}
	
    protected abstract void Update();
    protected abstract void Move();
    protected abstract void AnimatePlayer();
}
