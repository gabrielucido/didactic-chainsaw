using UnityEngine;

public class PlayerInputController : PlayerBase
{
    void Update()
    {
        GatherHorizontalInput();
        GatherJumpInput();
        // GatherOtherInputs();
    }

    private void GatherHorizontalInput()
    {
        Player.data.move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!Player.data.snapInput) return;
        Player.data.move.x = Mathf.Abs(Player.data.move.x) < Player.data.horizontalDeadZoneThreshold
            ? 0
            : Mathf.Sign(Player.data.move.x);
        Player.data.move.y = Mathf.Abs(Player.data.move.y) < Player.data.verticalDeadZoneThreshold
            ? 0
            : Mathf.Sign(Player.data.move.y);
    }

    private void GatherOtherInputs()
    {
        // if (_frameInput.JumpDown)
        // {
        //     _jumpToConsume = true;
        //     _timeJumpWasPressed = _time;
        // }

        // TODO: move flip to a HandleFlip method
        // if (_frameInput.Move.x > 0 && !_facingRight) Flip();
        // else if (_frameInput.Move.x < 0 && _facingRight) Flip();

        // if (_frameInput.Attack)
        // {
        //     _attacking = true;
        // }

        // TODO: Move to set to handleSOPosition
        // playerData.playerPosition = transform.position;   
    }


    private void GatherJumpInput()
    {
        // PlayerController.playerData._frameInput = new FrameInput
        // {
        //     // JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
        //     // JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
        //     Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
        //     // Attack = Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Mouse0)
        // };
    }
}