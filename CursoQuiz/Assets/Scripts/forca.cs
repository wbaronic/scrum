using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using Random = UnityEngine.Random;
using GoogleMobileAds.Common;
using System;
using System.IO;
using System.Text;
using System.Globalization;

public class forca : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    Text text;
    public Text PalavraNoJogo;
    public Text PerguntaText;
    public GameObject[] paineis;

    string palavraCompleta;
    string palvraNoJogoString;

    public Sprite ImageCorreto;
    public Sprite ImameErrado;
    public Sprite ImamePergunta;
    private float  percTempo, tempTeime;
    public float tempoResponder;


    [Header("Configuração dos Textos")]
    public Text NomeTemaText;
    public Text DenariosText;
    public Text DenariosNaPartidaText;
    private int qtdRevelacao;
    
    public Text InfoRespostaTxt;

    




    [Header("Configuração dos Barras")]
    public GameObject barraProgresso;
    public GameObject barraTempo;

    public Button[] botoes;


    private soundController soundController;
    private bool pediurevelacao;


    private BannerView bannerView;
    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;
    private float progresso;

    public int denarios;
    private int idTema;

    void Start()
    {
        tempTeime = 0;

        tempoResponder = 50;
        int idResponder = GameManager.Instance.idResponder;
        float val2 = (float)idResponder;

        progresso = val2 / GameManager.Instance.alternativasA.Length;

        DenariosNaPartidaText.text = GameManager.Instance.qtdCorretasnafase.ToString();


        if (GameManager.Instance.showAds)
        {

            MobileAds.Initialize(initStatus => { });
            this.RequestBanner();
            CreateAndLoadRewardedAd();
            RequestInterstitial();
        }
           


            palavraCompleta = RemoveSpecialCharacters(  GameManager.Instance.correta[idResponder].ToUpper());
        

        

        Debug.Log("Satrt");

        criarPAlavra(idResponder);

        soundController = soundController.FindObjectOfType(typeof(soundController)) as soundController;
        denarios = PlayerPrefs.GetInt("denarios");
        soundController.retornarSom();


        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();
        idTema = PlayerPrefs.GetInt("idTema");

        NomeTemaText.text = PlayerPrefs.GetString("nomeTema");



        double percentual = 20.0 / 100.0; // 15%
        double valor_final = (palavraCompleta.Length * percentual);
        int i = (int)Math.Ceiling(valor_final);

        qtdRevelacao = i;
        botoes[0].GetComponentInChildren<Text>().text = "Mostrar letra (" + qtdRevelacao + ")";


        progressaoBarra();





    }

    public static string RemoveSpecialCharacters(string text, bool allowSpace = false)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString();
    }



    private void criarPAlavra(int idResponder)
    {
        PerguntaText.text = GameManager.Instance.bdPerguntas[idResponder];


        palvraNoJogoString = "";

        // Use StringBuilder for concatenation in tight loops.
        var sb = new System.Text.StringBuilder();



        for (int i = 0; i < palavraCompleta.Length; i++)
        {

            sb.Append("-");

            Debug.Log(palvraNoJogoString);
        }




        palvraNoJogoString = sb.ToString();
        PalavraNoJogo.text = palvraNoJogoString;
    }





    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-5058605023723222/7749367669";


        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }


    private void MostrarInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-5058605023723222/2881312694";

       // string adUnitIdTetste = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void dica()
    {
        tempoResponder += 9000;

        if (GameManager.Instance.showAds)
        {

            MostrarInterstitial();

        }

        int idResponder = GameManager.Instance.idResponder;

        InfoRespostaTxt.GetComponentInChildren<Text>().text = GameManager.Instance.alternativasD[idResponder];


        paineis[0].SetActive(false);
        paineis[1].SetActive(false);
        paineis[2].SetActive(true);


    }

    public void responder()
    {


        soundController.Instance.retornarSom();
        paineis[0].SetActive(true);
        paineis[1].SetActive(true);
        paineis[2].SetActive(false);


    }


    public void revelacao()
    {

        tempoResponder +=50;

        if (GameManager.Instance.showAds)
        {
            ShowAd();

        }

        int i = palvraNoJogoString.IndexOf("-");
        char p = palavraCompleta.ToUpper()[i];

        
        selecionarLetra(p.ToString());

        qtdRevelacao -= 1;

        botoes[0].GetComponentInChildren<Text>().text = "Mostrar letra (" + qtdRevelacao + ")";

    }


    private void ShowAd()
    {
        UserChoseToWatchAd();
    }

    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    public void CreateAndLoadRewardedAd()
    {
     //   string rwardTeste = "ca-app-pub-3940256099942544/5224354917";

        string rewardValendo = "ca-app-pub-5058605023723222/3466167446";

        this.rewardedAd = new RewardedAd(rewardValendo);

        //    this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        //   this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }


    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }

    // Update is called once per frame
    void Update()
    {
        tempTeime += Time.deltaTime;

        barraTempoControle();

    }


    private void progressaoBarra()
    {



        

            barraProgresso.transform.localScale = new Vector3(progresso, 1, 1);
        

    }


    void barraTempoControle()
    {
    

        percTempo = ((tempTeime - tempoResponder) / tempoResponder) * -1;

        if (percTempo < 0)
        {
            percTempo = 0;
        }

        barraTempo.transform.localScale = new Vector3(percTempo, 1, 1);

    }



    public void temas()
    {
        GameManager.Instance.idResponder = 0;
        SceneManager.LoadScene("temas");
    }



    public void voltar()
    {






        if (GameManager.Instance.idResponder == 0)
        {
            GameManager.Instance.idResponder = 0;

            SceneManager.LoadScene("temas");

        }
        else
        {
            GameManager.Instance.idResponder -= 1;
            SceneManager.LoadScene("1");
        }
     
    }


    public void ajuda()
    {
        registrarEscolhadeConteudoNofirebase("AjudaForca");
            StartCoroutine(TakeScreenshotAndShare());
    }



    public void pause()
    {
        soundController.Instance.pararSom();
        dica();
    }
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Desafio Bíblico").SetText("Preciso de ajuda com essa pergunta").SetUrl("https://play.google.com/store/apps/details?id=br.com.baroni.aventurasbiblicasjp")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public void registrarEscolhadeConteudoNofirebase(string nome)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
Firebase.Analytics.FirebaseAnalytics.EventSelectContent,
new Firebase.Analytics.Parameter(
  Firebase.Analytics.FirebaseAnalytics.ParameterItemName, nome)
);
    }


    public void selecionarLetra(Button button)
    {






        Debug.Log("Selecionar Letra");

        //  btn = FindObjectOfType<Button>();
        var text = button.GetComponentInChildren<Text>().text;

       if(selecionarLetra(text))
        {
            button.GetComponent<Image>().sprite = ImageCorreto;


        }
        else
        {
            button.GetComponent<Image>().sprite = ImameErrado;

        }
    }

    private bool selecionarLetra(string text)
    {
        bool achou = false;

        Debug.Log("Procurando " + text);

        tempoResponder += 10;


        if (palavraCompleta.Contains(text))
        {




         //   denarios = denarios + 1;
           
            DenariosText.text = denarios.ToString();

            PlayerPrefs.SetInt("denarios", denarios);

            soundController.playAcertoForca();

            achou = true;

            string s = palavraCompleta;

            // Loop through all instances of the letter a.
            int i = 0;
            var sb = new System.Text.StringBuilder();
            sb.Append(palvraNoJogoString);
            while ((i = s.IndexOf(text, i)) != -1)
            {

                Debug.Log("Achou " + i);
                Debug.Log(sb.ToString());
                sb.Remove(i, 1);
                Debug.Log(sb.ToString());
                sb.Insert(i, text.Trim());
                Debug.Log(sb.ToString());
                // Increment the index.
                i++;

                Debug.Log(sb.ToString());

            }


            palvraNoJogoString = sb.ToString();

            Debug.Log("Atribuind " + palvraNoJogoString);

            PalavraNoJogo.text = palvraNoJogoString;
        }
        else
        {


            denarios = denarios - 1;
            PlayerPrefs.SetInt("denarios", denarios);

            soundController.playErro();
            DenariosText.text = denarios.ToString();

            achou = false;

        }




        if (!palvraNoJogoString.Contains("-"))
        {

            soundController.playAcerto();

            if (GameManager.Instance.showAds)
            {
                interstitial.Destroy();

            }
            denarios = denarios + 1;

            GameManager.Instance.qtdCorretasnafase = GameManager.Instance.qtdCorretasnafase + 1;

            GameManager.Instance.idResponder += 1;


            if (GameManager.Instance.idResponder < GameManager.Instance.correta.Length)
            {
                SceneManager.LoadScene("1");

            }
            else
            {
                SceneManager.LoadScene("resultado");
                GameManager.Instance.tempo = Time.timeSinceLevelLoad;
                Debug.Log("GameManager.Instance.tempo");



            }





        }


        return achou;
    }



}
