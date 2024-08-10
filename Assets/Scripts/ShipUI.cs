using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    [SerializeField] private Text speedText;
    [SerializeField] private Text positionText;
    [SerializeField] private Text rotationText;
    [SerializeField] private Text laserChargeText;
    [SerializeField] private Text laserCooldownText;

    public ShipUI(float speed, Vector3 position, float rotation, int laserCharge)
    {
        speedText.text = "Speed: " + speed.ToString("F2");
        positionText.text = "Position: (" + position.x.ToString("F2") + ", " + position.y.ToString("F2") + ")";
        rotationText.text = "Rotation: " + rotation.ToString("F2");
        laserChargeText.text = "Laser Charges: " + laserCharge;
    }

    public void UpdateLaserCooldown(float cooldown)
    {
        laserCooldownText.text = "Laser Cooldown: " + Mathf.Round(cooldown) + "с";
    }
}