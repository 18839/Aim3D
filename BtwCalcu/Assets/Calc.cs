using UnityEngine;
using System.Collections;

public class Calc : MonoBehaviour {
    [SerializeField]
    //private item[] items;

    public float ammountItem;
    public float exclBTWprice;
    public float inclBTWprice;
    public float finalPrice;
    public int discount;
    public int land;
    //public int item1;
    public float btw;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        inclBTWprice = (exclBTWprice / 100) * (100 + btw);
        Debug.Log((exclBTWprice / 100) * (100 + btw)+"test");
        if (land == 0)
        {
            btw = 0;
        }
        else if (land == 1)
        {
            btw = 21;
        }
        else if (land == 2)
        {
            btw = 20;
        }
        else if (land == 3)
        {
            btw = 19;
        }
        else if (land == 4)
        {
            btw = 27;
        }
        if (inclBTWprice* ammountItem >= 1000)
        {
            if (inclBTWprice* ammountItem >= 5000)
            {
                if (inclBTWprice* ammountItem >= 7000)
                {
                    if (inclBTWprice* ammountItem >= 10000)
                    {
                        if (inclBTWprice* ammountItem >= 50000)
                        {
                            finalPrice = (inclBTWprice / 100 * 85)*ammountItem;
                            discount = 15;
                        }
                        else
                        {
                            finalPrice = (inclBTWprice / 100 * 90)*ammountItem;
                            discount = 10;
                        }
                    }
                    else {
                    finalPrice = (inclBTWprice / 100 * 93)*ammountItem;
                        discount = 7;
                    }
                }
                else
                { 
                finalPrice = (inclBTWprice / 100 * 95)*ammountItem;
                discount = 5;
                }
        }
            else { 
                finalPrice = (inclBTWprice / 100 * 97)*ammountItem;
                discount = 3;
        }
    }
        else
        {
            discount = 0;
            finalPrice = (inclBTWprice)*ammountItem;
        }
    }
            
	}

    /*[System.Serializable]
    public class item
    {
        public int price;
        public string land;

    }*/

