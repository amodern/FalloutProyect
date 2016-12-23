using UnityEngine;
using System.Collections;

public class BaseCamp
{
        int sizeType = 0; //0-small, 1-medium, 2-big
        int buildingsPerRow; // 2 * width / 3; -- 
        int shopRows;
        int houseRows;
}

public class CreateCamp : MonoBehaviour 
{
    int size;
    int shopNum; //width / 3; --twice that in the end
    int shopRows;
    int houseRows;

    public GameObject fencePref, shopPref, housePref;

    Vector3 buildingPos, buildingPos2;

    void Start()
    {
        buildingPos = new Vector3(278, 11.7f, 200);
        CreateSquareFence(6);


        BuildShopsForSquare();
        BuildHousesForSquare();
    }

    void CreateSquareFence(int width)
    {
        if (width < 9)
            size = 0;
        else if (width < 12)
            size = 1;
        else
            size = 2;

        shopNum = width / 3;
        shopRows = Mathf.FloorToInt((width / 3) / 2);
        if (shopRows < 1)
            shopRows = 1;
        if (size == 0)
            houseRows = width - 1;
        else if (size == 1)
            houseRows = width - 1;
        else if (size == 2)
            houseRows = width - 3 -( 3 );

        //max width = 10 (from door side to corner) -- (walls with no door are double + 2)
        int doorWallWidth = width;
        int noDoorWallWidth = (width * 2) + 2;

        for (int i = 0; i < doorWallWidth; i++)
        {
            Instantiate(fencePref, buildingPos, Quaternion.identity);
            buildingPos += new Vector3(8, 0, 0);
        }
        buildingPos -= new Vector3(4, 0, 1);
        for (int i = 0; i < noDoorWallWidth; i++)
        {
            Instantiate(fencePref, buildingPos, Quaternion.Euler(new Vector3(0, -90, 0)));
            buildingPos += new Vector3(0, 0, 8);
        }
        buildingPos += new Vector3(1, 0, -4);
        for (int i = 0; i < noDoorWallWidth; i++)
        {
            Instantiate(fencePref, buildingPos, Quaternion.Euler(new Vector3(0, -180, 0)));
            buildingPos += new Vector3(-8, 0, 0);
        }
        buildingPos -= new Vector3(-4, 0, -1);
        for (int i = 0; i < noDoorWallWidth; i++)
        {
            Instantiate(fencePref, buildingPos, Quaternion.Euler(new Vector3(0, -270, 0)));
            buildingPos += new Vector3(0, 0, -8);
        }
        buildingPos += new Vector3(-1, 0, 4);
        for (int i = 0; i < doorWallWidth; i++)
        {
            Instantiate(fencePref, buildingPos, Quaternion.identity);
            buildingPos += new Vector3(8, 0, 0);
        }
    }

    void BuildShopsForSquare()
    {        
     //SetBuildingPosToFirstShop - needs to execute after CreateSquareFence because it uses last buildPosition
       buildingPos2 = buildingPos - new Vector3(-18, 1.7f, -5);
        buildingPos -= new Vector3(18, 1.7f, -5);
        Vector3 curBP, curBP2;
        for (int f = 0; f < shopRows; f++)
        {
            curBP = buildingPos;
            curBP2 = buildingPos2;
            for (int i = 0; i < shopNum; i++)
            {
                Instantiate(shopPref, curBP, Quaternion.identity);
                curBP += new Vector3(-24, 0, 0);

                Instantiate(shopPref, curBP2, Quaternion.identity);
                curBP2 += new Vector3(24, 0, 0);
            }
            buildingPos += new Vector3(0,0,27);
            buildingPos2 += new Vector3(0, 0, 27);
        }

    }

    void BuildHousesForSquare()
    {
        buildingPos = new Vector3(buildingPos.x + 206, 15, buildingPos.z-128);
        buildingPos2 = new Vector3(buildingPos2.x -197, 15, buildingPos2.z + 131);

        //needs execute after build shops
        Vector3 curBP, curBP2;
        for (int f = 0; f < houseRows; f++)
        {
            curBP = buildingPos;
            curBP2 = buildingPos2;
            for (int i = 0; i < shopNum; i++)
            {
                Instantiate(housePref, curBP, Quaternion.Euler(new Vector3(0,-90,0)));
                curBP += new Vector3(-24, 0, 0);

                Instantiate(housePref, curBP2, Quaternion.Euler(new Vector3(0, 90, 0)));
                curBP2 += new Vector3(24, 0, 0);
            }
            buildingPos += new Vector3(0, 0, 16);
            buildingPos2 += new Vector3(0, 0, 16);
        }

    }
}
