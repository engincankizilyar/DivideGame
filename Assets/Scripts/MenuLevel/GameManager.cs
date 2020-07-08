using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using DG.Tweening; // doTween plug-ini için
using UnityEngine.UI; // ui elementleri için

public class GameManager : MonoBehaviour
{
    // --- Değişken Tanımlamaları --- //
    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]
    private Transform karePanel;

    [SerializeField]
    private GameObject[] kareler = new GameObject[25];

    [SerializeField]
    private Transform soruPanel;

    [SerializeField]
    private Text txtSoru;
    [SerializeField]
    private Sprite[] kareSprites; 
    
    [SerializeField]
    private GameObject sonucPanel;

    [SerializeField]
    private AudioSource audioSource;

    public AudioClip butonSesi;
    List<int> bolumList = new List<int>();
    int bolunen, bolen;
    int soruIdx, dogruCevap, tiklanan;
    bool cond = false;
    int kalanHak;
    string zorlukDerecesi;
    kalanHaklarManager haklarManager;
    puanManager puanManager;
    GameObject tiklananKare;

    // --- Oyun Genelinde Yapılacak Olan İşlemler --- //
    private void Awake(){
        kalanHak = 3;
        
        audioSource = GetComponent<AudioSource>(); //GameManager'ın bağlı olduğu nesne içerisindeki audioSource'a ulaş diyoruz.

        sonucPanel.gameObject.SetActive(false);

        haklarManager = Object.FindObjectOfType<kalanHaklarManager>();
        puanManager = Object.FindObjectOfType<puanManager>();

        haklarManager.kalanHakKontrol(kalanHak);
    }
    // Start is called before the first frame update
    void Start()
    {
        //soruPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        soruPanel.gameObject.SetActive(false);
        kareleriOlustur();
        bolumDegerleriYazdir();
        Invoke("soruPaneliniAc", 3f); // 3 sn sonra açması için Invoke kullandık.
    }

    // ------------------------------------------------------- Oyun İçin Kendi Oluşturduğum Fonksiyonlar ------------------------------------------------------- //
    public void kareleriOlustur(){
        for(int i = 0; i< 25; i++){
            GameObject kare = Instantiate(karePrefab,karePanel); // Instantiate ile prefab şeklinde kareleri çoğaltma işlemini yapıyoruz.
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0,kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => buttonClicked());
            kareler[i] = kare;
            
            StartCoroutine(doFadeRoutine());
            //kare.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void buttonClicked(){ // buton'a basıldığında dinlenecek Fonksiyon.
        if(cond){
            audioSource.PlayOneShot(butonSesi);
            tiklanan = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            // Debug.Log(tiklanan);
            tiklananKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            sonucKontrol();
        }
    }

    void sonucKontrol(){
        if(tiklanan == dogruCevap){ // soru doğrudur.
            tiklananKare.transform.GetChild(1).GetComponent<Image>().enabled = true; // doğru bilinen kare yeni bir resim olarak ekranda kalıyor.
            tiklananKare.transform.GetChild(0).GetComponent<Text>().text = "";
            tiklananKare.transform.GetComponent<Button>().interactable = false; // doğru olduktan sonra o kareye tekrar tıklanmasını önlüyoruz

            puanManager.puanArtir(zorlukDerecesi); // bilinen sorunun zorluk derecesine göre puanlama yapıyoruz.
            bolumList.RemoveAt(soruIdx); // aynı soru gelmemesi için önce listeden o soruyu çıkarıyoruz.
            // Debug.Log(bolumList.Count);
            if(bolumList.Count > 0)
                soruPaneliniAc(); // doğru bilindikten sonra yeni soruya geçiş yapılacak.
            else
                oyunBitisi();

        }else{
            kalanHak--;
            haklarManager.kalanHakKontrol(kalanHak);
        }

        if(kalanHak <= 0){
            oyunBitisi();
        }
    }

    void oyunBitisi(){
        cond = false; // oyun bitisinde butonlara basılmasını önlüyoruz.
        sonucPanel.gameObject.SetActive(true);
    }

    public IEnumerator doFadeRoutine(){
        foreach(var kare in kareler){
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void bolumDegerleriYazdir(){
        foreach(var kare in kareler){
            int randomValue = Random.Range(1, 13); // 1 - 12 arası (bölüm değerleri)
            bolumList.Add(randomValue);
            kare.transform.GetChild(0).GetComponent<Text>().text = randomValue.ToString();
        }
        // Debug.Log(bolumList[0]);
    }

    void soruPaneliniAc(){
        soruPanel.gameObject.SetActive(true);
        soruSor();
        cond = true; // soru panelini açtıktan sonra butonların tıklanabilirliğini açıyoruz.
    }

    void soruSor(){
        bolen = Random.RandomRange(2,11); // 2 - 10 arası
        soruIdx = Random.RandomRange(0, bolumList.Count);
        dogruCevap = bolumList[soruIdx];
        // Debug.Log(soruIdx);
        bolunen = bolen * dogruCevap;

        if(bolunen <= 40)
            zorlukDerecesi = "kolay";
        else if(bolunen > 40 && bolunen <= 80)
            zorlukDerecesi = "orta";
        else
            zorlukDerecesi = "zor";

        txtSoru.text = bolunen.ToString() + " / " + bolen.ToString();
    }

}
