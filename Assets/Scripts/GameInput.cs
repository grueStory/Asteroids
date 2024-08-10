using UnityEngine;

public class GameInput
{
    public bool IsAccelerating => Input.GetKey(KeyCode.UpArrow);
    public bool IsTurningLeft => Input.GetKey(KeyCode.LeftArrow);
    public bool IsTurningRight => Input.GetKey(KeyCode.RightArrow);
    public bool ShootBullet => Input.GetKeyDown(KeyCode.Space);
    public bool ShootLaser => Input.GetKeyDown(KeyCode.LeftShift);
}