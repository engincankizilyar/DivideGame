using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui elementlerinin kullanımı için

public class puanManager : MonoBehaviour
{
    // --- Sorunun Zorluğuna Göre Puan İşlemleri --- //
    private int toplamPuan;
    private int puanArtisi;

    [SerializeField]
    private Text puanText;

    // Start is called before the first frame update
    void Start()
    {
        puanText.text = toplamPuan.ToString();
    }
    public void puanArtir(string zorluk){
        switch(zorluk){
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
                
        }
        toplamPuan += puanArtisi;
        puanText.text = toplamPuan.ToString();
    }

}