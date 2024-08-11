using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Ship _ship;
    private GameInput _gameInput;

    public void Construct(Ship ship, GameInput gameInput)
    {
        _ship = ship;
        _gameInput = gameInput;
    }

    public void Update()
    {
        if (_ship.isAlive)
        {
            HandleShipAcceleration();
            HandleShipRotation();
            HandleBulletShooting();
            HandleLaserShooting();
        }
    }

    private void HandleShipAcceleration()
    {
        _ship.isAccelerating = _gameInput.IsAccelerating;
    }
    
    private void HandleShipRotation()
    {
        if (_gameInput.IsTurningLeft)
        {
            _ship.transform.Rotate(_ship.shipRotationSpeed * Time.deltaTime * Vector3.forward);
        }
        else if (_gameInput.IsTurningRight)
        {
            _ship.transform.Rotate(-_ship.shipRotationSpeed * Time.deltaTime * Vector3.forward);
        }
    }

    private void HandleBulletShooting()
    {
        if (_gameInput.ShootBullet)
        {
            _ship.ShootBullet();
        }
    }

    private void HandleLaserShooting()
    {
        if (_gameInput.ShootLaser && _ship.laserChargeCurent > 0)
        {
            _ship.ShootLaser();
        }
    }
}