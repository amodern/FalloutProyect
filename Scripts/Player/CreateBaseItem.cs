using UnityEngine;
using System.Collections;

public class CreateBaseItem : BaseItem 
{
    public BaseItem CreateNewBaseItem()
    {
        BaseItem i = new BaseItem();
        return i;
    }
}
public class CreateBaseWeapon : BaseWeapon
{
    static float[] fireRates = new float[]{
        0.7f,//sniper
        0.2f,//rifle
        1f,//shotgun
        2f//rpg
    };
    static int[] magSizes = new int[]{
        12,//sniper
        35,//rifle
        2,//shotgun
        1//rpg
    };
    static int[] prefabIndexes = new int[]{
        3,//bazooka
        1,//rifle
        5,//shotgun
        4,//sniper
    };
    public static BaseWeapon CreateNewBaseWeapon(ItemQuality quality)
    {
        WeaponType wpType = (WeaponType)Random.Range(0, 4);
        //set quality
        int numBuffs = Random.Range((int)quality, (int)quality + 2);//0-2 for common
        Debug.Log("Number of buffs: " + numBuffs);
        BaseWeapon i = new BaseWeapon();
        i.name = RandomName(quality, numBuffs, wpType);
        i.description = ItemType.Weapon.ToString();
        i.value = Random.Range(0, 100) + (int)quality * 200;
        i.weigth = 20;//THINK ABOUT THIS!!!
        Debug.Log(i.name);
        int index = (int)wpType;
        i.fireRate = fireRates[index];//fireRates array and wpType enum are ordered equally
        i.magSize = magSizes[index];
        i.prefabIndex = prefabIndexes[index];
        i.iconIndex = 1;
        i.wpType = wpType;
        i.quality = quality;
        i.damage = Random.Range(((int)quality+4)*25, ((int)quality+4)*25*2);
        //continue with them all
        return i;
    }

    public static string RandomName(ItemQuality quality, int numBuffs, BaseWeapon.WeaponType wpType)
    {
        string[] commonWeaponNames = new string[]{
            "Mid Range",
            "Mercenary",
            "Military"
        };
        string name = quality.ToString();
        switch(wpType)
        {
            case BaseWeapon.WeaponType.Sniper:
                switch(quality)
                {
                    case ItemQuality.Common:
                        int rnd = Random.Range(0, commonWeaponNames.Length);
                        name += " " + commonWeaponNames[rnd];
                        break;
                }
                break;
        }
        name += " " + wpType.ToString();//Up to now example: Common Mercenary Sniper


        return name;
    }

}
public class CreateBaseArmor : CreateBaseItem
{

}