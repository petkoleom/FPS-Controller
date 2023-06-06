using System;
using System.Drawing;
using UnityEngine;

public class Wallrunning : PlayerComponent
{

    [SerializeField] private float wallrunForce, maxWallrunTime, wallCheckDistance, minJumpHeight, exitWallTime;

    private float wallrunTimer, exitWallTimer;

    private bool wallRight, wallLeft, exitingWall;
    private RaycastHit rightHit, leftHit;

    public static event Action<bool, bool> OnWallRunning;
    public static event Action OnJumpedFromWall;

    private void OnEnable()
    {
        PlayerInput.OnJumpInput += WallJump;
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpInput -= WallJump;
    }

    private void Update()
    {
        CheckForWall();

        if (exitingWall)
        {
            if (player.State == PlayerState.Wallrunning)
                StopWallrun();

            if (exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;

            if (exitWallTimer <= 0)
                exitingWall = false;
        }
        else if ((wallRight || wallLeft) && AboveGround())
        {                
            if (player.State != PlayerState.Wallrunning)
                StartWallRun();

            if (wallrunTimer > 0)
                wallrunTimer -= Time.deltaTime;

            if (wallrunTimer <= 0 && player.State == PlayerState.Wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }
        }
        else
        {
            if (player.State == PlayerState.Wallrunning)
                StopWallrun();
        }

        
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position + Vector3.up * .1f, player.Orientation.right, out rightHit, wallCheckDistance);
        wallLeft = Physics.Raycast(transform.position + Vector3.up * .1f, -player.Orientation.right, out leftHit, wallCheckDistance);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position - Vector3.down * .01f, Vector3.down, minJumpHeight);
    }

    private void StartWallRun()
    {
        player.TryWallrunning = true;
        wallrunTimer = maxWallrunTime;
        OnWallRunning?.Invoke(true, wallRight);
    }

    private void StopWallrun()
    {
        player.TryWallrunning = false;
        OnWallRunning?.Invoke(false, wallRight);

    }

    private void WallJump(bool _value)
    {
        if (player.State != PlayerState.Wallrunning) return;

        var _side = wallRight ? -player.Orientation.right : player.Orientation.right;
        var _jumpDir = (Vector3.up + 2.3f * _side).normalized;
        player.Rb.AddForce(2 * player.Specs.JumpForce * _jumpDir, ForceMode.Impulse);
        OnJumpedFromWall?.Invoke();
        return;


    }
}
