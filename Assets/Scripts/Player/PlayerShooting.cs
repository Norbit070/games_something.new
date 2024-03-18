
using UnityEngine;

public class PlayerShooting : CharacterShooting
{
    [SerializeField] protected bool _autoReloading = true;

    protected override void OnInit()
    {
        base.OnInit();
    }
    protected override void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
            AutoReloading();
        }
    }
    protected override void Reloading()
    {
        if ((!CheckHasBulletsInRow() && Input.GetMouseButton(0)) || Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void AutoReloading()
    {
        if (!_autoReloading)
        {
            return;
        }
        if (!CheckHasBulletsInRow())
        {
            Reload();
        }
    }
}