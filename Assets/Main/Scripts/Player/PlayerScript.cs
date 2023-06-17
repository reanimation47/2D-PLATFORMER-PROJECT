using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PlayerClass
{


    //Public values
    public float PlayerJumpHeight = 9f;
    public float PlayerMovementSpeed = 9f;
    public int PlayerJumpCount = 1; //How many times player could jump mid air
    //public string ObstacleTag = "Obstacle";


    private bool playerKilled = false;

    //Unity's method

    private void Awake()
    {
        if (playerKilled) { return; }
        InitPlayerObject();
    }

    private void Start()
    {
        
        
    }

    private void Update()
    {
        if (playerKilled) {return;}
        Player.GetHorizontalInput(); //getting user's horizontal inputs
        Player.GetJumpInput(); //getting user's jump input AND also do jump action
    }

    private void FixedUpdate()
    {
        if (playerKilled) { return; }
        Player.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerKilled) { return; }
        int collisionState = Player.CheckOnCollision(collision);
        if (collisionState == 0)
        {
            ICommon.KillPlayer();
        }
    }


    //Custom methods

    public void KillPlayer()
    {
        playerKilled = true;
        Player.ToggleAnimTrigger("killed");
        Invoke("DestroyPlayer", 0.5f);
    }

    public void InjectHorizontalInput(float _dir)
    {
        Player.InjectHorizontalInput(_dir);
    }
    public void InjectJumpInput()
    {
        Player.Jump();
    }


    private void InitPlayerObject()
    {
        if (playerKilled) { return; }
        IPlayer.LoadPlayerObject(this.gameObject);
        Player.GetPlayerObject();
    }


    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }



}
