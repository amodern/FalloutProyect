using UnityEngine;
using System.Collections.Generic;

public class SuperItem
{
    public BaseItem.ItemType type;
    public List<string> parametros = new List<string>();

    public SuperItem(BaseItem.ItemType t)
    {
        type = t;
    }

    public BaseWeapon ConvertToWeapon()
    {
        BaseWeapon wp = new BaseWeapon();
        wp.iconIndex = int.Parse(parametros[0]);
        wp.name = parametros[1];
        wp.description = parametros[3];
        wp.value = int.Parse(parametros[4]);
        wp.weigth = float.Parse(parametros[5]);
        wp.prefabIndex = int.Parse(parametros[6]);

        wp.damage = int.Parse(parametros[7]);
        wp.wpType = BaseWeapon.StringToWpType(parametros[8]);
        wp.fireRate = float.Parse(parametros[9]);
        wp.magSize = int.Parse(parametros[10]);

        return wp;
    }

    public BaseArmor ConvertToArmor()
    {
        BaseArmor ar = new BaseArmor();
        ar.iconIndex = int.Parse(parametros[0]);
        ar.name = parametros[1];
        ar.description = parametros[3];
        ar.value = int.Parse(parametros[4]);
        ar.weigth = float.Parse(parametros[5]);
        ar.prefabIndex = int.Parse(parametros[6]);

        ar.armor = int.Parse(parametros[7]);
        ar.arType = BaseArmor.StringToArType(parametros[8]);

        return ar;
    }

    public BaseConsumable ConvertToConsumable()
    {
        BaseConsumable cm = new BaseConsumable();
        cm.iconIndex = int.Parse(parametros[0]);
        cm.name = parametros[1];
        cm.quality = BaseItem.StringToQuality(parametros[2]);
        cm.description = parametros[3];
        cm.value = int.Parse(parametros[4]);
        cm.weigth = float.Parse(parametros[5]);
        cm.prefabIndex = int.Parse(parametros[6]);

        cm.effectAmount = int.Parse(parametros[7]);
        cm.effect = parametros[8];
        
        return cm;
    }

    public BaseAmmo ConvertToAmmo()
    {
        BaseAmmo am = new BaseAmmo();
        am.iconIndex = int.Parse(parametros[0]);
        am.name = parametros[1];
        am.description = parametros[3];
        am.value = int.Parse(parametros[4]);
        am.weigth = float.Parse(parametros[5]);
        am.prefabIndex = int.Parse(parametros[6]);

        am.cantidad = int.Parse(parametros[7]);
        am.ammoType = BaseWeapon.StringToWpType(parametros[8]);

        return am;
    }
}

public class BaseItem
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Ammo,
        Upgrade
    }

    public enum ItemQuality
    {
        Common,
        UnCommon,
        Rare,
        UltraRare,
        Epic,
        SuperEpic,
        Legendary,
        Unique  //Through the random creation item make an item giving it the best choice on each random moment or something similar
    }

    public ItemType type;
    public ItemQuality quality;
    public string name, description;
    public int value;
    public int iconIndex, prefabIndex;
    public float weigth;
    public static ItemType StringToType(string type)
    {
        ItemType t = ItemType.Ammo;
        switch (type)
        {
            case "Weapon":
                t = ItemType.Weapon;
                break;
            case "Armor":
                t = ItemType.Armor;
                break;
            case "Consumable":
                t = ItemType.Consumable;
                break;
            case "Ammo":
                t = ItemType.Ammo;
                break;
            case "Upgrade":
                t = ItemType.Upgrade;
                break;
        }
        return t;
    }
    public static ItemQuality StringToQuality(string quality)
    {
        ItemQuality t = ItemQuality.Common;
        switch (quality)
        {
            case "Common":
                t = ItemQuality.Common;
                break;
            case "UnCommon":
                t = ItemQuality.UnCommon;
                break;
            case "Rare":
                t = ItemQuality.Rare;
                break;
            case "UltraRare":
                t = ItemQuality.UltraRare;
                break;
            case "Epic":
                t = ItemQuality.Epic;
                break;
            case "SuperEpic":
                t = ItemQuality.SuperEpic;
                break;
            case "Legendary":
                t = ItemQuality.Legendary;
                break;
            case "Unique":
                t = ItemQuality.Unique;
                break;
        }
        return t;
    }
    
    public virtual SuperItem ConvertToSuperItem()
    {
        SuperItem i = new SuperItem(type);

        i.parametros.Add(iconIndex.ToString());
        i.parametros.Add(name);
        i.parametros.Add(quality.ToString());
        i.parametros.Add(description);
        i.parametros.Add(value.ToString());
        i.parametros.Add(weigth.ToString());
        i.parametros.Add(prefabIndex.ToString());

        return i;
    }
}

public class BaseUpgrade : BaseItem
{
    public string[] buffs;
    public float[] amounts;

    public BaseUpgrade()
    {
        type = ItemType.Upgrade;
    }
}

public class BaseWeapon : BaseItem
{
    public enum WeaponType
    {
        Sniper,
        Rifle,
        Shotgun,
        Bazooka
    }
    BaseUpgrade[] upgradeSlots;

    public WeaponType wpType;
    public int damage;
    public float fireRate;
    public int magSize;

    public BaseWeapon()
    {
        type = ItemType.Weapon;
    }

    public static WeaponType StringToWpType(string type)
    {
        WeaponType t = WeaponType.Rifle;
        switch(type)
        {
            case "Sniper":
                t = WeaponType.Sniper;
                break;
            case "Rifle":
                t = WeaponType.Rifle;
                break;
            case "Shotgun":
                t = WeaponType.Shotgun;
                break;
            case "Bazooka":
                t = WeaponType.Bazooka;
                break;
        }
        return t;
    }

    public override SuperItem ConvertToSuperItem()
    {
        SuperItem i = new SuperItem(type);

        i.parametros.Add(iconIndex.ToString());
        i.parametros.Add(name);
        i.parametros.Add(quality.ToString());
        i.parametros.Add(description);
        i.parametros.Add(value.ToString());
        i.parametros.Add(weigth.ToString());
        i.parametros.Add(prefabIndex.ToString());

        i.parametros.Add(damage.ToString());
        i.parametros.Add(wpType.ToString());
        i.parametros.Add(fireRate.ToString());
        i.parametros.Add(magSize.ToString());

        return i;
    }


}

public class BaseArmor : BaseItem
{
    public enum ArmorType
    {
        Helmet,
        Chest,
        RightLeg,
        LeftLeg,
        RightArm,
        LeftArm,
        LeftGlove,
        RightGlove,
        LeftBoot,
        RightBoot
    }
    BaseUpgrade[] upgradeSlots;

    public ArmorType arType;
    public int armor;
    //Radiation resistance, (...)

    public BaseArmor()
    {
        type = ItemType.Armor;
    }

    public static ArmorType StringToArType(string type)
    {
        ArmorType t = ArmorType.Helmet;
        switch (type)
        {
            case "Helmet":
                t = ArmorType.Helmet;
                break;
            case "Chest":
                t = ArmorType.Chest;
                break;
            case "LeftBoot":
                t = ArmorType.LeftBoot;
                break;
            case "RightBoot":
                t = ArmorType.RightBoot;
                break;
            case "RightGlove":
                t = ArmorType.RightGlove;
                break;
            case "LeftGlove":
                t = ArmorType.LeftGlove;
                break;
            case "LeftLeg":
                t = ArmorType.LeftLeg;
                break;
            case "RightLeg":
                t = ArmorType.RightLeg;
                break;
            case "LeftArm":
                t = ArmorType.LeftArm;
                break;
            case "RightArm":
                t = ArmorType.RightArm;
                break;
        }
        return t;
    }

    public override SuperItem ConvertToSuperItem()
    {
        SuperItem i = new SuperItem(type);

        i.parametros.Add(iconIndex.ToString());
        i.parametros.Add(name);
        i.parametros.Add(quality.ToString());
        i.parametros.Add(description);
        i.parametros.Add(value.ToString());
        i.parametros.Add(weigth.ToString());
        i.parametros.Add(prefabIndex.ToString());

        i.parametros.Add(armor.ToString());
        i.parametros.Add(arType.ToString());

        return i;
    }
}

public class BaseConsumable : BaseItem
{
    public string effect;
    public int effectAmount;

    public BaseConsumable()
    {
        type = ItemType.Consumable;
    }

    public override SuperItem ConvertToSuperItem()
    {
        SuperItem i = new SuperItem(type);

        i.parametros.Add(iconIndex.ToString());
        i.parametros.Add(name);
        i.parametros.Add(quality.ToString());
        i.parametros.Add(description);
        i.parametros.Add(value.ToString());
        i.parametros.Add(weigth.ToString());
        i.parametros.Add(prefabIndex.ToString());

        i.parametros.Add(effectAmount.ToString());
        i.parametros.Add(effect);

        return i;
    }
}

public class BaseAmmo : BaseItem
{
    public int cantidad;
    public BaseWeapon.WeaponType ammoType;

    public BaseAmmo()
    {
        type = ItemType.Ammo;
    }

    public override SuperItem ConvertToSuperItem()
    {
        SuperItem i = new SuperItem(type);

        i.parametros.Add(iconIndex.ToString());
        i.parametros.Add(name);
        i.parametros.Add(quality.ToString());
        i.parametros.Add(description);
        i.parametros.Add(value.ToString());
        i.parametros.Add(weigth.ToString());
        i.parametros.Add(prefabIndex.ToString());

        i.parametros.Add(cantidad.ToString());
        i.parametros.Add(ammoType.ToString());
        return i;
    }
}