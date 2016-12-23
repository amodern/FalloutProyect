using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemStatDisplay : MonoBehaviour 
{
    public GameObject armorEquipedSlots;

    SuperItem displayingItem;
    WeaponManager wpManager;
    PlayerInventory pInv;

    void Start()
    {
        wpManager = GameObject.FindObjectOfType<WeaponManager>();
        pInv = PlayerInventory.pInv;
    }

    public SuperItem DisplayingItem
    {
        get { return displayingItem; }
        set { 
            displayingItem = value;
            DisplayItem();
        }
    }

    void DisplayItem()
    {
        Text[] txt = GetComponentInChildren<Image>().GetComponentsInChildren<Text>();

        txt[0].text = displayingItem.parametros[1];//name
        txt[1].text = displayingItem.parametros[2];//quality
        txt[2].text = "Value: " + displayingItem.parametros[4];
        txt[3].text = "Weight: " + displayingItem.parametros[5];
        string itemTypeStat = "";
        switch(displayingItem.type)
        {
            case BaseItem.ItemType.Weapon:
                itemTypeStat = "Damage: ";
                break;
            case BaseItem.ItemType.Armor:
                itemTypeStat = "Armor: ";
                break;
            case BaseItem.ItemType.Consumable:
                itemTypeStat = "Effect Amount: ";
                break;
            case BaseItem.ItemType.Ammo:
                itemTypeStat = "Quantity: ";
                break;
        }
        txt[4].text = itemTypeStat + displayingItem.parametros[7];//damage/armor/effectAmount/...
        txt[5].text = displayingItem.parametros[3];//description
    }

    public void ClickUnEquip(int index)
    {
        armorEquipedSlots.GetComponentsInChildren<Button>()[index].GetComponent<Image>().color = Color.white;
        armorEquipedSlots.GetComponentsInChildren<Button>()[index].GetComponentInChildren<Text>().text = "--";
    }
    public void ClickEquip(int equipSlotIndex)
    {
        switch(displayingItem.type)
        {
            case BaseItem.ItemType.Weapon://If its already equipped, remove it from the weaponsInUse before adding it.
                wpManager.StartCoroutine(wpManager.DeselectWeapon());
                for (int i = 0; i < wpManager.weaponsInUse.Length; i++)
                {
                    if(wpManager.weaponsInUse[i] == wpManager.weaponsInGame[displayingItem.ConvertToWeapon().prefabIndex])
                    {
                        wpManager.weaponsInUse[i] = wpManager.weaponsInGame[0];
                        break;
                    }
                }
                wpManager.weaponsInUse[equipSlotIndex] = wpManager.weaponsInGame[displayingItem.ConvertToWeapon().prefabIndex];
                SetPrefDamageToBaseWeaponValue(wpManager.weaponsInUse[equipSlotIndex].GetComponent<WeaponScriptNEW>());
                break;
            case BaseItem.ItemType.Armor:
                Transform[] slots = armorEquipedSlots.GetComponentsInChildren<Transform>();
                slots[(int)displayingItem.ConvertToArmor().arType].GetComponentInChildren<Image>().color = Color.blue;
                slots[(int)displayingItem.ConvertToArmor().arType].GetComponentInChildren<Image>().GetComponentInChildren<Text>().text = displayingItem.ConvertToArmor().armor.ToString();
                GameObject.FindObjectOfType<UIDisplayController>().SendMessage("ShowEqInv", true);
                break;
        }
    }

    void SetPrefDamageToBaseWeaponValue(WeaponScriptNEW wps)
    {
        BaseWeapon w = displayingItem.ConvertToWeapon();
        wps.damage = w.damage;
        wps.fireRateFirstMode = w.fireRate;
        wps.bulletsPerMag = w.magSize;
    }
}
