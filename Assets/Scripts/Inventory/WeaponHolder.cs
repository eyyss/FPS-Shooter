using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public static WeaponHolder Instance;
    public Animator animator;
    public int Weapon�ndex;

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
            if (i == Weapon�ndex) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
        
    }
    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Weapon�ndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)&&transform.childCount>=1) Weapon�ndex = 1;
        SelectWeapon();
    }
    public void MouseScrollInput()
    {
        int previousSelectedWeapon = Weapon�ndex;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Weapon�ndex >= transform.childCount - 1)
                Weapon�ndex = 0;
            else
                Weapon�ndex++;
            time = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Weapon�ndex <= 1)
                Weapon�ndex = transform.childCount - 1;
            else
                Weapon�ndex--;
            time = 0;
        }
        if (previousSelectedWeapon != Weapon�ndex)
            SelectWeapon();
    }
    public void UpdateWeaponAnim()
    {
        if (animator.GetBool("Pistol")) animator.CrossFade("Pistol�dle", .15f);
        if (animator.GetBool("Rifle")) animator.CrossFade("RifleIdle", .15f);
        if (Inventory.Instance.InventoryItems.Count>0)
        {
            var data = transform.GetChild(Weapon�ndex).GetComponent<ItemHolder>().Item;//alt�m�zdaki objelerden weapon index olan�n� bulup i�indeki item scriptable objesine eri�tik
            switch (data.Type)
            {
                case ItemType.Pistol:// pistol ise pistol animasyonunu true yapt�m di�erini false
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
