using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public enum LandStatus
    {
        PROPERTY,
        TAX,
        RAILROAD,
        UTILITY,
        CHANCE,
        CHEST,
        START,
        GOTOJAIL,
        JAIL,
        FREEPARKING
    };


    string[]
        landNameArr =
        {
            "",
            "San Diego Drive",
            "",
            "San Marina Harbor",
            "",
            "Atlantic Ocean",
            "Point Reyes",
            "",
            "Monterey Bay",
            "La Jolla Cove",
            "",
            "Hudson River",
            "Water Current",
            "Blackney Point",
            "Moss Landing",
            "Pacific Ocean",
            "Ross Sea",
            "",
            "Bering Sea",
            "Baikal Lake",
            "",
            "South Georgia",
            "",
            "Falkand Islands",
            "King George Island",
            "North Sea",
            "Wellington New Zealand",
            "Galapagos Islands",
            "Schools of Fish",
            "Campbell Island",
            "",
            "South Orkney",
            "Cape Crozier",
            "",
            "Ballestas Peru",
            "Arctic Circle",
            "",
            "Tasmania",
            "",
            "South Africa"
        };

    int[]
        landPriceArr =
        {
            0,
            60,
            0,
            90,
            0,
            200,
            120,
            0,
            130,
            150,
            0,
            140,
            150,
            160,
            140,
            200,
            180,
            0,
            200,
            200,
            0,
            200,
            0,
            200,
            260,
            200,
            260,
            260,
            130,
            230,
            0,
            300,
            300,
            0,
            250,
            200,
            0,
            200,
            0,
            350
        };

    LandStatus[] landStatus = new LandStatus[40];

    Land[] lands = new Land[40];

    public int[] funds;

    public string whatUserCanDo = "";

    public int playernum = 0;

    public int curIndex = 0;
    private int[] num_houses;
    private int[] ownership;

    public static int[] build_costs = new int[40] {
        0, 50, 0, 50, 0, 0, 50, 0, 50, 50,
        0, 100, 0, 100, 100, 0, 100, 0, 100, 100,
        0, 150, 0, 150, 150, 0, 150, 150, 0, 150,
        0, 200, 200, 0, 200, 0, 0, 200, 0, 200
    };

    public static int[,] rent = new int[40, 4] {
        { 0, 0, 0, 0 },
        { 2, 10, 30, 90 },
        { 0, 0, 0, 0 },
        { 4, 20, 60, 180 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 6, 30, 90, 270 },
        { 0, 0, 0, 0 },
        { 6, 30, 90, 270 },
        { 8, 40, 100, 300 },
        { 0, 0, 0, 0 },
        { 10, 50, 150, 450 },
        { 0, 0, 0, 0 },
        { 10, 50, 150, 450 },
        { 12, 60, 180, 500 },
        { 0, 0, 0, 0 },
        { 14, 70, 200, 550 },
        { 0, 0, 0, 0 },
        { 14, 70, 200, 550 },
        { 16, 80, 220, 600 },
        { 0, 0, 0, 0 },
        { 18, 90, 250, 700 },
        { 0, 0, 0, 0 },
        { 18, 90, 250, 700 },
        { 20, 100, 300, 750 },
        { 0, 0, 0, 0 },
        { 22, 110, 330, 800 },
        { 22, 110, 330, 800 },
        { 0, 0, 0, 0 },
        { 22, 120, 360, 850 },
        { 0, 0, 0, 0 },
        { 26, 130, 390, 900 },
        { 26, 130, 390, 900 },
        { 0, 0, 0, 0 },
        { 28, 150, 450, 1000 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 35, 175, 500, 1100 },
        { 0, 0, 0, 0 },
        { 50, 200, 600, 1400 }
    };

    void Awake()
    {
        funds = new int[3];
        funds[1] = funds[2] = 1500;

        ownership = new int[40];
        for (int i = 0; i < lands.Length; i++)
        {
            lands[i] = new Land(landNameArr[i], landPriceArr[i], 0, build_costs[i]);
            landStatus[i] = LandStatus.PROPERTY;
            ownership[i] = 0;
        }

        landStatus[4] = landStatus[38] = LandStatus.TAX;
        landStatus[5] = landStatus[15] = landStatus[25] = landStatus[35] = LandStatus.RAILROAD;
        landStatus[12] = landStatus[28] = LandStatus.UTILITY;
        landStatus[7] = landStatus[22] = landStatus[36] = LandStatus.CHANCE;
        landStatus[2] = landStatus[17] = landStatus[33] = LandStatus.CHEST;
        landStatus[0] = LandStatus.START;
        landStatus[10] = LandStatus.JAIL;
        landStatus[20] = LandStatus.FREEPARKING;
        landStatus[30] = LandStatus.GOTOJAIL;

        num_houses = new int[40];
        for (int i = 0; i < 40; ++i)
        {
            num_houses[i] = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnHouse()
    {
        if (curIndex % 10 == 0 || curIndex < 0 || curIndex >= 40) throw new System.ArgumentException();

        Spawner spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        if (curIndex > 0 && curIndex < 10)
        {
            float offset = (curIndex - 1) * 0.8f + num_houses[curIndex] * 0.25f;
            Vector3 pos = new Vector3(0.161f, 0.25f, 0.55f + offset);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
            spawner.SpawnHouse(playernum - 1, pos, rot);
        }
        else if (curIndex > 10 && curIndex < 20)
        {
            float offset = (curIndex - 11) * 0.8f + num_houses[curIndex] * 0.25f;
            Vector3 pos = new Vector3(0.55f + offset, 0.25f, 7.8f);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 90, 0));
            spawner.SpawnHouse(playernum - 1, pos, rot);
        }
        else if (curIndex > 20 && curIndex < 30)
        {
            float offset = (curIndex - 21) * 0.8f + num_houses[curIndex] * 0.25f;
            Vector3 pos = new Vector3(7.8f, 0.25f, 7.4f - offset);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 180, 0));
            spawner.SpawnHouse(playernum - 1, pos, rot);
        }
        else if (curIndex > 30 && curIndex < 40)
        {
            float offset = (curIndex - 31) * 0.8f + num_houses[curIndex] * 0.25f;
            Vector3 pos = new Vector3(7.4f - offset, 0.25f, 0.18f);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 270, 0));
            spawner.SpawnHouse(playernum - 1, pos, rot);
        } 
        num_houses[curIndex] += 1;
    }

    public void purchase()
    {
        if (ownership[curIndex] == playernum)
            return;

        TextMeshProUGUI txt;
        if (playernum == 1)
        {
            funds[1] -= lands[curIndex].price;
            ownership[curIndex] = 1;
            txt =
                GameObject
                    .Find("Player1LandsList")
                    .GetComponent<TextMeshProUGUI>();
            txt.text += (landNameArr[curIndex] + "\n");
        }
        else if (playernum == 2)
        {
            funds[2] -= lands[curIndex].price;
            ownership[curIndex] = 2;
            txt =
                GameObject
                    .Find("Player2LandsList")
                    .GetComponent<TextMeshProUGUI>();
            txt.text += (landNameArr[curIndex] + "\n");
        }

        SpawnHouse();
    }

    

    public void build()
    {
        if (playernum == 1)
        {
            funds[1] -= 2 * landPriceArr[curIndex];
        }
        else
        {
            funds[2] -= 2 * landPriceArr[curIndex];
        }
        lands[curIndex].numOfBuildings++;
        lands[curIndex].price += 2 * landPriceArr[curIndex];

        TextMesh txt;
        string landName = lands[curIndex].landName;
        txt = GameObject.Find(landName + "_price").GetComponent<TextMesh>();
        txt.text = (lands[curIndex].price).ToString();

        print(lands[curIndex].numOfBuildings);
        print(lands[curIndex].price);
    }

    public void arrivedOnCity(int playerNum, int curIndex)
    {
        TextMesh txt;
        this.playernum = playerNum;
        this.curIndex = curIndex;

        //whatusercando: = based on landstatus,
        if (landStatus[curIndex] == LandStatus.PROPERTY ||
            landStatus[curIndex] == LandStatus.RAILROAD ||
            landStatus[curIndex] == LandStatus.UTILITY
        )
        {
            if (ownership[curIndex] == 0)
            {
                print(funds[playerNum]);
                // Purchase Property
                if (funds[playerNum] >= lands[curIndex].price)
                    whatUserCanDo = "purchase";
                else
                    whatUserCanDo = "";
            }
            else
            {
                if (ownership[curIndex] == playerNum)
                {
                    if (landStatus[curIndex] == LandStatus.PROPERTY)
                    {
                        // Build House
                        if (funds[playerNum] >= lands[curIndex].build && lands[curIndex].numOfBuildings < 3)
                            whatUserCanDo = "build";
                        else
                            whatUserCanDo = "";
                    }
                }
                else
                {
                    // Pay Rent
                    // "sell"
                }
            }

            UpdateFunds();
        }
        else if (landStatus[curIndex] == LandStatus.TAX)
        {
            if (curIndex == 4)
                funds[playerNum] -= 150;
            else if (curIndex == 38)
                funds[playerNum] -= 200;

            UpdateFunds();
        }
        else if (landStatus[curIndex] == LandStatus.CHANCE)
        {
            
        }
        else if (landStatus[curIndex] == LandStatus.CHEST)
        {
            
        }
        else if (landStatus[curIndex] == LandStatus.START)
        {
            //funds[playerNum] += 200;

            UpdateFunds();
        }
        else if (landStatus[curIndex] == LandStatus.GOTOJAIL)
        {
            if (playernum == 1)
                FindObjectOfType<Player1Script>().goToJail();
            else
                FindObjectOfType<Player2Script>().goToJail();
        }

        //button on, purchase,build,sell
        if (
            whatUserCanDo == "purchase" ||
            whatUserCanDo == "build" ||
            whatUserCanDo == "sell"
        )
        {
            //turn the button on
        }
    }

    //button on, purchase,build,sell
    public void onyesclick()
    {
        print("yes clicekd");
        if (whatUserCanDo == "purchase")
        {
            print("purchasing");
            purchase();
        }
        else if (whatUserCanDo == "build")
        {
            build();
        }
        else if (whatUserCanDo == "sell")
        {
            //sell();
        }

        UpdateFunds();
    }

    public void onnoclick()
    {
        //close the button tab
    }

    private void UpdateFunds()
    {
        TextMeshProUGUI txt;
        txt = GameObject.Find("Player1Money").GetComponent<TextMeshProUGUI>();
        txt.text = ("$" + funds[1]);
        txt = GameObject.Find("Player2Money").GetComponent<TextMeshProUGUI>();
        txt.text = ("$" + funds[2]);
    }
}
