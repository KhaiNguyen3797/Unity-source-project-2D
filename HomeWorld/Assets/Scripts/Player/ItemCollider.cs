using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollider : MonoBehaviour
{
    [SerializeField] public GameObject itemCreat;
    private int scoreGem = 0;
    private int scoreCherry = 0;
    [SerializeField] public Text textGem;
    [SerializeField] public Text textCherry;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gem"))
        {
            Destroy(col.gameObject);
            scoreGem += 1;
            textGem.text = scoreGem.ToString();
            Instantiate(itemCreat, col.transform.position, col.transform.rotation);
        }

        if (col.CompareTag("Cherry"))
        {
            Destroy(col.gameObject);
            scoreCherry += 1;
            textCherry.text = scoreCherry.ToString();
            Instantiate(itemCreat, col.transform.position, col.transform.rotation);
        }
    }
}
