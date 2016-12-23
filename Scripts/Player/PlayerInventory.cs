using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class PlayerInventory : MonoBehaviour 
{
    //DISPLAY
    public GameObject slotPref;

    public static Sprite[] itemIcons;
    public GameObject[] wpPrefabs;
    public ItemStatDisplay itmDisp;

    List<GameObject> go_slots = new List<GameObject>();
    GameObject slotHolder;
    int curSlots = 0, initialX, initialY, curX, curY, slotHeight = 20;
    public BaseItem.ItemType displayingType;

    //DATA
    public static PlayerInventory pInv;

    //List<SuperItem> items = new List<SuperItem>();
    public List<BaseWeapon> weapons = new List<BaseWeapon>();
    List<BaseArmor> armors = new List<BaseArmor>();
    List<BaseConsumable> consumables = new List<BaseConsumable>();
    public List<BaseAmmo> ammunition = new List<BaseAmmo>();
    List<BaseUpgrade> upgrades = new List<BaseUpgrade>();
    string savePath = @"D:\Unity\NEW FALLOUT\Assets\Scripts\InventorySaveDocs\items.txt";
    string sentinel = "#";

    void Start()
    {
        pInv = this;
        
        slotHolder = new GameObject("Slot Holder");
        slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
        slotHolder.transform.position = new Vector3(840, 298, 0);

        curX = initialX = (int)(transform.position.x) - 3;
        curY = initialY = (int)(transform.position.y) + 100;

        LoadInvFromTxt();
//Add new items to saved test inv here play once, then delete their code

        SaveInvToTxt();
    }

    void InitializeAmmo()
    {
        BaseAmmo am1 = new BaseAmmo();
        am1.name = "Sniper Rounds";
        am1.description = "Bullets for a specific weapon type";
        am1.iconIndex = 1;
        am1.weigth = 0.1f;
        am1.value = 2;
        am1.ammoType = BaseWeapon.WeaponType.Sniper;
        ammunition.Add(am1);
        BaseAmmo am2 = new BaseAmmo();
        am2.name = "Rifle Rounds";
        am2.description = "Assault rounds for automatic rifles";
        am2.iconIndex = 1;
        am2.weigth = 0.1f;
        am2.value = 2;
        am2.ammoType = BaseWeapon.WeaponType.Rifle;
        ammunition.Add(am2);
        BaseAmmo am3 = new BaseAmmo();
        am3.name = "Shotgun Shells";
        am3.description = "10 pellet shotgun shells";
        am3.iconIndex = 1;
        am3.weigth = 0.1f;
        am3.value = 2;
        am3.ammoType = BaseWeapon.WeaponType.Shotgun;
        ammunition.Add(am3);
        BaseAmmo am4 = new BaseAmmo();
        am4.name = "Bazooka Rockets";
        am4.description = "Explosive rockets to deal seroius ammounts of damage";
        am4.iconIndex = 1;
        am4.weigth = 0.1f;
        am4.value = 2;
        am4.ammoType = BaseWeapon.WeaponType.Bazooka;
        ammunition.Add(am4);
    }

    public void DisplayType(BaseItem.ItemType type)
    {
        displayingType = type;
        switch(type)
        {
            case BaseItem.ItemType.Weapon:
                if(weapons.Count > curSlots)
                {
                    int missingSlots = weapons.Count - curSlots;
                    for (int i = 0; i < missingSlots; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                else if(weapons.Count != curSlots)
                {
                    Destroy(slotHolder);
                    go_slots.Clear();
                    slotHolder = new GameObject("Slot Holder");
                    slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
                    slotHolder.transform.position = new Vector3(840, 298, 0);
                    curY = initialY;
                    curSlots = 0;

                    for (int i = 0; i < weapons.Count; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                for (int i = 0; i < weapons.Count; i++)
                {
                    Image[] comps = go_slots[i].GetComponentsInChildren<Image>();
                    //comps[0].sprite = itemIcons[weapons[i].iconIndex];
                    comps[1].GetComponentInChildren<Text>().text = weapons[i].name;
                    comps[1].gameObject.GetComponent<Button>().onClick.AddListener(() => ClickSlot());
                    comps[2].GetComponentInChildren<Text>().text = weapons[i].weigth.ToString();
                    comps[3].GetComponentInChildren<Text>().text = weapons[i].value.ToString();
                    comps[4].GetComponentInChildren<Text>().text = weapons[i].damage.ToString();
                }
                break;
            case BaseItem.ItemType.Armor:
                 if(armors.Count > curSlots)
                {
                    int missingSlots = armors.Count - curSlots;
                    for (int i = 0; i < missingSlots; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                 else if (armors.Count != curSlots)
                {
                    Destroy(slotHolder);
                    go_slots.Clear();
                    slotHolder = new GameObject("Slot Holder");
                    slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
                    slotHolder.transform.position = new Vector3(840, 298, 0);
                    curY = initialY;
                    curSlots = 0;

                    for (int i = 0; i < armors.Count; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                 for (int i = 0; i < armors.Count; i++)
                {
                    Image[] comps = go_slots[i].GetComponentsInChildren<Image>();
                    //comps[0].sprite = itemIcons[armors[i].iconIndex];
                    comps[1].GetComponentInChildren<Text>().text = armors[i].name;
                    comps[1].GetComponent<Button>().onClick.AddListener(() => ClickSlot());
                    comps[2].GetComponentInChildren<Text>().text = armors[i].weigth.ToString();
                    comps[3].GetComponentInChildren<Text>().text = armors[i].value.ToString();
                    comps[4].GetComponentInChildren<Text>().text = armors[i].armor.ToString();
                }
                break;
            case BaseItem.ItemType.Consumable:
                if (consumables.Count > curSlots)
                {
                    int missingSlots = consumables.Count - curSlots;
                    for (int i = 0; i < missingSlots; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                else if (consumables.Count != curSlots)
                {
                    Destroy(slotHolder);
                    go_slots.Clear();
                    slotHolder = new GameObject("Slot Holder");
                    slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
                    slotHolder.transform.position = new Vector3(840, 298, 0);
                    curY = initialY;
                    curSlots = 0;

                    for (int i = 0; i < consumables.Count; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                for (int i = 0; i < consumables.Count; i++)
                {
                    Image[] comps = go_slots[i].GetComponentsInChildren<Image>();
                    //comps[0].sprite = itemIcons[consumables[i].iconIndex];
                    comps[1].GetComponentInChildren<Text>().text = consumables[i].name;
                    Debug.Log(i);
                    comps[1].GetComponent<Button>().onClick.AddListener(() => ClickSlot());
                    comps[2].GetComponentInChildren<Text>().text = consumables[i].weigth.ToString();
                    comps[3].GetComponentInChildren<Text>().text = consumables[i].value.ToString();
                    comps[4].GetComponentInChildren<Text>().text = consumables[i].effectAmount.ToString();
                }
                break;
            case BaseItem.ItemType.Ammo:
                if (ammunition.Count > curSlots)
                {
                    int missingSlots = ammunition.Count - curSlots;
                    for (int i = 0; i < missingSlots; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                else if (ammunition.Count != curSlots)
                {
                    Destroy(slotHolder);
                    go_slots.Clear();
                    slotHolder = new GameObject("Slot Holder");
                    slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
                    slotHolder.transform.position = new Vector3(840, 298, 0);
                    curY = initialY;
                    curSlots = 0;

                    for (int i = 0; i < ammunition.Count; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                for (int i = 0; i < ammunition.Count; i++)
                {
                    Image[] comps = go_slots[i].GetComponentsInChildren<Image>();
                    //comps[0].sprite = itemIcons[ammunition[i].iconIndex];
                    comps[1].GetComponentInChildren<Text>().text = ammunition[i].name;
                    comps[1].GetComponent<Button>().onClick.AddListener(() => ClickSlot());
                    comps[2].GetComponentInChildren<Text>().text = ammunition[i].weigth.ToString();
                    comps[3].GetComponentInChildren<Text>().text = ammunition[i].value.ToString();
                    comps[4].GetComponentInChildren<Text>().text = ammunition[i].cantidad.ToString();
                }
                break;
            case BaseItem.ItemType.Upgrade:
                if (upgrades.Count > curSlots)
                {
                    int missingSlots = upgrades.Count - curSlots;
                    for (int i = 0; i < missingSlots; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                else if (upgrades.Count != curSlots)
                {
                    Destroy(slotHolder);
                    go_slots.Clear();
                    slotHolder = new GameObject("Slot Holder");
                    slotHolder.transform.parent = GetComponentInChildren<Image>().transform;
                    slotHolder.transform.position = new Vector3(840, 298, 0);
                    curY = initialY;
                    curSlots = 0;

                    for (int i = 0; i < upgrades.Count; i++)
                    {
                        GameObject temp = Instantiate(slotPref, new Vector3(initialX, curY, 0), Quaternion.identity) as GameObject;
                        temp.name = "Slot";
                        temp.transform.parent = slotHolder.transform;
                        go_slots.Add(temp);
                        curY -= slotHeight;
                        curSlots++;
                    }
                }
                for (int i = 0; i < upgrades.Count; i++)
                {
                    Image[] comps = go_slots[i].GetComponentsInChildren<Image>();
                    //comps[0].sprite = itemIcons[ammunition[i].iconIndex];
                    comps[1].GetComponentInChildren<Text>().text = upgrades[i].name;
                    comps[1].GetComponent<Button>().onClick.AddListener(() => ClickSlot());
                    comps[2].GetComponentInChildren<Text>().text = upgrades[i].weigth.ToString();
                    comps[3].GetComponentInChildren<Text>().text = upgrades[i].value.ToString();
                    for (int j = 0; j < upgrades[i].buffs.Length; j++)
                    {
                        comps[4].GetComponentInChildren<Text>().text += upgrades[i].buffs[j].ToString() + "/n";
                    }
                }
                break;
        }
    }

    public void ClickTypeButton(string type)
    {
        DisplayType(BaseItem.StringToType(type));
        itmDisp.gameObject.SetActive(false);
    }

    public void ClickSlot()
    {
        int index = 0;
        float tDist = Vector2.Distance(Input.mousePosition, go_slots[0].transform.position);
        for (int i = 0; i < curSlots; i++)
        {
            if (Vector2.Distance(Input.mousePosition, go_slots[i].transform.position) < tDist)
            {
                index = i;
                tDist = Vector2.Distance(Input.mousePosition, go_slots[i].transform.position);
            }
        }

        GameObject.FindObjectOfType<UIDisplayController>().SendMessage("ShowItmDispInv", true);

        switch(displayingType)
        {
            case BaseItem.ItemType.Weapon:
                itmDisp.DisplayingItem = weapons[index].ConvertToSuperItem();
                break;
            case BaseItem.ItemType.Armor:
                itmDisp.DisplayingItem = armors[index].ConvertToSuperItem();
                break;
            case BaseItem.ItemType.Consumable:
                itmDisp.DisplayingItem = consumables[index].ConvertToSuperItem();
                break;
            case BaseItem.ItemType.Ammo:
                itmDisp.DisplayingItem = ammunition[index].ConvertToSuperItem();
                break;
        }
    }


    void SaveInvToTxt()
    {//Figure max number that needs to loop to.
        int greatestLength = 0;
        if (weapons.Count > armors.Count)
        {
            if (weapons.Count > consumables.Count)
            {
                if (weapons.Count > ammunition.Count)
                {
                    greatestLength = weapons.Count;
                }
                else
                {
                    greatestLength = ammunition.Count;
                }
            }
            else if (consumables.Count > armors.Count)
            {
                if (consumables.Count > ammunition.Count)
                {
                    greatestLength = consumables.Count;
                }
                else
                {
                    greatestLength = ammunition.Count;
                }
            }
        }
        else if (armors.Count > consumables.Count)
        {
            if (armors.Count > ammunition.Count)
            {
                greatestLength = armors.Count;
            }
            else
            {
                greatestLength = ammunition.Count;
            }
        }
        //Loop and write an item of each type at a time till they are over
        StreamWriter sw = new StreamWriter(savePath);
        for (int i = 0; i < greatestLength; i++)
        {
            SuperItem sit;
            if (i < weapons.Count)
            {
                sit = weapons[i].ConvertToSuperItem();

                sw.WriteLine(sit.type.ToString());
                for (int f = 0; f < sit.parametros.Count; f++)
                {
                    sw.WriteLine(sit.parametros[f]);
                }
                sw.WriteLine(sentinel);
            }
            if (i < armors.Count)
            {
                sit = armors[i].ConvertToSuperItem();
                sw.WriteLine(sit.type.ToString());
                for (int f = 0; f < sit.parametros.Count; f++)
                {
                    sw.WriteLine(sit.parametros[f]);
                }
                sw.WriteLine(sentinel);
            }
            if (i < consumables.Count)
            {
                sit = consumables[i].ConvertToSuperItem();
                sw.WriteLine(sit.type.ToString());
                for (int f = 0; f < sit.parametros.Count; f++)
                {
                    sw.WriteLine(sit.parametros[f]);
                }
                sw.WriteLine(sentinel);
            }
            if (i < ammunition.Count)
            {
                sit = ammunition[i].ConvertToSuperItem();
                sw.WriteLine(sit.type.ToString());
                for (int f = 0; f < sit.parametros.Count; f++)
                {
                    sw.WriteLine(sit.parametros[f]);
                }
                sw.WriteLine(sentinel);
            }
        }
        sw.Close();
    }

    void LoadInvFromTxt()
    {
        StreamReader sr = new StreamReader(savePath);
        SuperItem s = null;
        string cLine;
        for (int i = 0; i < File.ReadAllText(savePath).Length; i++)
        {
            cLine = sr.ReadLine();
            if (s == null)
                s = new SuperItem(BaseItem.StringToType(cLine));
            else if (cLine == "#")
            {
                switch (s.type)
                {
                    case BaseItem.ItemType.Weapon:
                        weapons.Add(s.ConvertToWeapon());
                        s = null;
                        break;
                    case BaseItem.ItemType.Armor:
                        armors.Add(s.ConvertToArmor());
                        s = null;
                        break;
                    case BaseItem.ItemType.Consumable:
                        consumables.Add(s.ConvertToConsumable());
                        s = null;
                        break;
                    case BaseItem.ItemType.Ammo:
                        ammunition.Add(s.ConvertToAmmo());
                        s = null;
                        break;
                }
            }
            else
            {
                s.parametros.Add(cLine);
            }
        }
        sr.Close();
    }
}
