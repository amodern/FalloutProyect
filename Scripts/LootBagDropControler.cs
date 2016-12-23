using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LootBagDropControler : MonoBehaviour {
    public GameObject slotPref;

    SuperItem[] lootItems;
    GameObject lootInv, slotHolder;
    Vector3 firstSlotPos;
    UIDisplayController uiDisp;

    public SuperItem[] LootItems
    {
        get { return lootItems; }
        set { 
            lootItems = value;
            haveItemsChanged = true;
        }
    }

    int slotHeigth = 22;
    bool haveItemsChanged = false;
	// Use this for initialization
	void Start () {
        uiDisp = GameObject.FindObjectOfType<UIDisplayController>();
        uiDisp.ShowLootInv();
        slotHolder = GameObject.Find("LootInvSlotHolder");

        firstSlotPos = new Vector3(slotHolder.transform.position.x + 52, slotHolder.transform.position.y + 90, 0); //347 - 295 = 52

        lootInv = slotHolder.transform.parent.gameObject;
        lootInv.GetComponent<CanvasRenderer>().SetAlpha(0);
        Debug.Log(lootInv.name);

        //int numItems = Random.Range(1, 5);//1 - ammo, 2 - ammo+item*2, 3 - ammo+item*3 ... 
        int numItems = 2;
        LootItems = new SuperItem[numItems];


        AddAmmoToInventory(LootItems);
        if (numItems > 1)
        {
            for (int i = 1; i < numItems; i++)
            {
                //int rnd = Random.Range(0, 1);//0-weapon, 1-armor, 2-upgrade?
                int rnd = 0;
                switch (rnd)
                {
                    case 0:
                        LootItems[i] = CreateBaseWeapon.CreateNewBaseWeapon(BaseItem.ItemQuality.Common).ConvertToSuperItem();
                        break;
                    case 1:
                        //LootItems[i] = CreateBaseWeapon.CreateNewBaseWeapon(BaseItem.ItemQuality.Legendary).ConvertToSuperItem();
                        break;
                }
            }
        }
        transform.parent = null;
        //add rest of items
        lootInv.GetComponent<CanvasRenderer>().SetAlpha(1);
        uiDisp.ShowLootInv();
	}

    void DisplayItems(SuperItem[] items)
    {
        while(slotHolder.transform.childCount > 0)
            Destroy(slotHolder.GetComponentInChildren<Transform>().gameObject);
        Debug.Log(items.Length);

        for (int i = 0; i < items.Length; i++)
        {
            GameObject r = Instantiate(slotPref, firstSlotPos - new Vector3(0,slotHeigth*i,0), Quaternion.identity) as GameObject;
            r.transform.parent = slotHolder.transform;
            r.GetComponentInChildren<Button>().onClick.AddListener(() => ClickLootSlot("" + i));
            Image[] imgs = r.GetComponentsInChildren<Image>();
            //imgs[0] = PlayerInventory.pInv.itemIcons (...)
            if (items[i] == null)
                Debug.Log("NULL");
            imgs[1].GetComponentInChildren<Text>().text = items[i].parametros[1];
            imgs[2].GetComponentInChildren<Text>().text = items[i].parametros[5];
            imgs[3].GetComponentInChildren<Text>().text = items[i].parametros[4];
            imgs[4].GetComponentInChildren<Text>().text = items[i].parametros[7];
        }
        haveItemsChanged = false;
    }

    void AddAmmoToInventory(SuperItem[] items)
    {
        string wpType = ((BaseWeapon.WeaponType)Random.Range(0, 4)).ToString();
        BaseAmmo am = new BaseAmmo();
        am.name = wpType + " ammunition";
        am.iconIndex = 1;
        am.prefabIndex = 0;
        am.quality = BaseItem.ItemQuality.Common;
        am.type = BaseItem.ItemType.Ammo;
        am.value = 2;
        am.weigth = 0.05f;
        am.description = "bullets for your " + wpType;
        am.cantidad = Random.Range(1, 8) * 5;
        LootItems[0] = am.ConvertToSuperItem();
        Debug.Log(am.name);
    }
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
	    if(Input.GetKeyDown("e") &&  dist <= 7)
        {
            //Make the menu APPEAR!
            uiDisp.ShowLootInv();
            if (haveItemsChanged)
                DisplayItems(LootItems);
        }
	}

    void ClickLootSlot(string index)
    {
        int ind = int.Parse(index) - 1;
        switch(lootItems[ind].type)
        { 
            case BaseItem.ItemType.Weapon:
                PlayerInventory.pInv.weapons.Add(lootItems[ind].ConvertToWeapon());
                lootItems[ind] = null;
                break;
            case BaseItem.ItemType.Ammo:
                PlayerInventory.pInv.ammunition.Add(lootItems[ind].ConvertToAmmo()); //Create a function to add ammo that finds out if you have any of that type and if so, increases the  quantity
                lootItems[ind] = null;
                break;
        }
        PlayerInventory.pInv.DisplayType(PlayerInventory.pInv.displayingType);
    }
}
