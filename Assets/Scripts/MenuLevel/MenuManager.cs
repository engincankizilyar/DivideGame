using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // doTween plug-in için
using UnityEngine.SceneManagement; // sahneler arası geçiş için

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject btnStart,btnExit;
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeOut(){
        btnStart.GetComponent<CanvasGroup>().DOFade(1,0.8f);
        btnExit.GetComponent<CanvasGroup>().DOFade(1,0.8f).SetDelay(0.5f);
    }
    public void exitGame(){
        Application.Quit();
    }
    public void startGameLevel(){
        SceneManager.LoadScene("GameLevel");
    }
}
