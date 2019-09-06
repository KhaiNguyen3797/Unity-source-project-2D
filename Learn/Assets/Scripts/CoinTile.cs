using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTile : MonoBehaviour
{
    //Du an that bai can tim hieu them

    public enum coinType
    {
        arrow,
        rectangle,
        parapol
    }

    public coinType coinTypeOf;
    public GameObject Coin;
    [Range(1, 7)] public int weithCoin;
    [Range(1, 7)] public int heighCoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coinTypeOf == coinType.arrow)
        {

        }

        if(coinTypeOf == coinType.rectangle)
        {
            
        }

        if(coinTypeOf == coinType.parapol)
        {

        }
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(weithCoin, heighCoin, 0);
    }
}
