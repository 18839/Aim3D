using UnityEngine;

public interface IDamageable// a interface, every script that implements this interface is forced to have this method(TakeHit) including it.
{
    void TakeHit(float damage, RaycastHit hit);//how much damage and a raycast to profide info like where it was hit
    void PlayerDamg(float damage);//how much damage
}
