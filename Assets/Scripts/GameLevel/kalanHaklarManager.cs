using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kalanHaklarManager : MonoBehaviour
{
    // --- Oyuncunun Haklarının Kontrol Edilme İşlemleri --- // 
    [SerializeField]
    private GameObject hak1,hak2,hak3;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void kalanHakKontrol(int kalanHak){
        switch(kalanHak){
            case 3:
                hak1.SetActive(true);
                hak2.SetActive(true);
                hak3.SetActive(true);
                break;
            case 2:
                hak1.SetActive(true);
                hak2.SetActive(true);
                hak3.SetActive(false);
                break;
            case 1:
                hak1.SetActive(true);
                hak2.SetActive(false);
                hak3.SetActive(false);
                break;
            case 0:
                hak1.SetActive(false);
                hak2.SetActive(false);
                hak3.SetActive(false);
                break;
        }
    }
}
