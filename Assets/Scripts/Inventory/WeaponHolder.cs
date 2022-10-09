using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public static WeaponHolder Instance;
    public Animator animator;
    public int WeaponÝndex;

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
            if (i == WeaponÝndex) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
        
    }
    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) WeaponÝndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)&&transform.childCount>=1) WeaponÝndex = 1;
        SelectWeapon();
    }
    public void MouseScrollInput()
    {
        int previousSelectedWeapon = WeaponÝndex;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (WeaponÝndex >= transform.childCount - 1)
                WeaponÝndex = 0;
            else
                WeaponÝndex++;
            time = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (WeaponÝndex <= 1)
                WeaponÝndex = transform.childCount - 1;
            else
                WeaponÝndex--;
            time = 0;
        }
        if (previousSelectedWeapon != WeaponÝndex)
            SelectWeapon();
    }
    public void UpdateWeaponAnim()
    {
        if (animator.GetBool("Pistol")) animator.CrossFade("PistolÝdle", .15f);
        if (animator.GetBool("Rifle")) animator.CrossFade("RifleIdle", .15f);
        if (Inventory.Instance.InventoryItems.Count>0)
        {
            var data = transform.GetChild(WeaponÝndex).GetComponent<ItemHolder>().Item;//altýmýzdaki objelerden weapon index olanýný bulup içindeki item scriptable objesine eriþtik
            switch (data.Type)
            {
                case ItemType.Pistol:// pistol ise pistol animasyonunu true yaptým diðerini false
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
