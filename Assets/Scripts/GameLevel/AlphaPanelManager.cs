using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // doTween plug-in için

public class AlphaPanelManager : MonoBehaviour
{
    public GameObject alphaPanel;


    // Start is called before the first frame update
    void Start()
    {
        alphaPanel.GetComponent<CanvasGroup>().DOFade(0,2f); // geçişte gamelevel sahnesinin daha yavaş açılmasını sağlıyor.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
