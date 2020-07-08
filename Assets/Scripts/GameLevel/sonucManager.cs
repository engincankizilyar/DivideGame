using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// sahne geçiş işlemleri için
public class sonucManager : MonoBehaviour
{
    // --- Butonlara Tıklandığında Sahne Geçiş İşlemleri ---//
    public void yenidenBasla(){
        SceneManager.LoadScene("GameLevel");
    }
    public void menuyeDon(){
        SceneManager.LoadScene("MenuLevel");
    }
}
