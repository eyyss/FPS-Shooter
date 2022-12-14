using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public static WeaponHolder Instance;
    public Animator animator;
    public int Weaponİndex;

    float time;

    public void Awake()
    {
        Instance = this;
        SelectWeapon();
        
    }
    public void Update()
    {
        time += Time.deltaTime;
        KeyboardInput();
        if(time>.6f)
        MouseScrollInput();
    }


    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform) 
        {
            UpdateWeaponAnim();
            if (i == Weaponİndex) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
        
    }
    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Weaponİndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)&&transform.childCount>=1) Weaponİndex = 1;
        SelectWeapon();
    }
    public void MouseScrollInput()
    {
        int previousSelectedWeapon = Weaponİndex;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Weaponİndex >= transform.childCount - 1)
                Weaponİndex = 0;
            else
                Weaponİndex++;
            time = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Weaponİndex <= 1)
                Weaponİndex = transform.childCount - 1;
            else
                Weaponİndex--;
            time = 0;
        }
        if (previousSelectedWeapon != Weaponİndex)
            SelectWeapon();
    }
    public void UpdateWeaponAnim()
    {
        if (animator.GetBool("Pistol")) animator.CrossFade("Pistolİdle", .15f);
        if (animator.GetBool("Rifle")) animator.CrossFade("RifleIdle", .15f);
        if (Inventory.Instance.InventoryItems.Count>0)
        {
            var data = transform.GetChild(Weaponİndex).GetComponent<ItemHolder>().Item;//altımızdaki objelerden weapon index olanını bulup içindeki item scriptable objesine eriştik
            switch (data.Type)
            {
                case ItemType.Pistol:// pistol ise pistol animasyonunu true yaptım diğerini false
                    animator.SetBool("Pistol", true);
                    animator.SetBool("Rifle", false);
                    break;
                case ItemType.Rifle:
                    animator.SetBool("Pistol", false);
                    animator.SetBool("Rifle", true);
                    break;
                default:
                    break;
            }
        }

    }

}
