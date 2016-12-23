using UnityEngine;
using System.Collections;

public class UIDisplayController : MonoBehaviour {

    public GameObject playerInv, equippedInv, itemDisplayInv, lootInvDisplay;

    bool isInventoryVis = false, isEquippedVis = false, isLootInvVis = false;
    
    void Start()
    {
        Invoke("SetDisplaysOff", 0.2f);
    }

    void SetDisplaysOff()
    {
        playerInv.SetActive(false);
        equippedInv.SetActive(false);
        itemDisplayInv.SetActive(false);
        lootInvDisplay.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown("i"))
        {
            isInventoryVis = !isInventoryVis;
            playerInv.SetActive(isInventoryVis);
            if (isInventoryVis)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (!isEquippedVis && !isInventoryVis && !isLootInvVis)
            {
                Cursor.lockState = CursorLockMode.Locked;
                itemDisplayInv.SetActive(false);
                Cursor.visible = false;
            }

        }
        if (Input.GetKeyDown("o"))
        {
            ShowEqInv(false);
        }
	}

    public void ShowEqInv(bool mustBeVisible) //Gets called on SendMessage by itemStatDisplay to show when equiping.
    {
        if (isEquippedVis && mustBeVisible)
            isEquippedVis = false;
        isEquippedVis = !isEquippedVis;
        equippedInv.SetActive(isEquippedVis);
        if (isEquippedVis)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isInventoryVis && !isEquippedVis && !isLootInvVis)
        {
            Cursor.lockState = CursorLockMode.Locked;
            itemDisplayInv.SetActive(false);
            Cursor.visible = false;
        }
    }

    public void ShowItmDispInv(bool show)
    {
        itemDisplayInv.SetActive(show);
    }

    public void ShowLootInv()
    {
        isLootInvVis = !isLootInvVis;
        lootInvDisplay.SetActive(isLootInvVis);

        if (isLootInvVis)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isInventoryVis && !isEquippedVis && !isLootInvVis)
        {
            Cursor.lockState = CursorLockMode.Locked;
            itemDisplayInv.SetActive(false);
            Cursor.visible = false;
        }
    }
}