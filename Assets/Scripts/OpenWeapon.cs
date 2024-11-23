using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWeapon : MonoBehaviour
{

    public GameObject Weapon;
    public void WeaponStart()
    {
        Weapon.SetActive(true);
    }
    public void WeaponEnd()
    {
        Weapon.SetActive(false);
    }
}
