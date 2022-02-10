using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System.IO;

public class modoJogo1 : MonoBehaviour
{



    [Header("Configuração dos Textos")]
    public Text NomeTemaText;
    public Text DenariosText;
    public Text DenariosNaPartidaText;
    public Text msg2Txt;
    public Text msg3Txt;
    public Text msg1Txt;
    private int qtdRevelacao;



    public Text  Perguntaext;
    public Image PerguntaIMG;

    public Text InfoRespostaTxt;
   
    [Header("Configuração dos Alternativas")]
    public Text alternativaA;
    public Text alternativaB;
    public Text alternativaC;
    public Text dica;




    [Header("Configuração dos Barras")] 
    public GameObject barraProgresso;
    public GameObject barraTempo;

    [Header("Configuração dos Botões")]
    
    public Button[] botoes;
    public Color corAcerto;
    public Color corErro;


    [Header("Configuração dos Modo Jogo")]
    // public bool perguntasAleatorias;
    public bool perguntaComImg;
    public bool jogarComTempo;
    public bool perguntaAleatoria;
    public bool utilizarAlternativa;
    public int qtdPiscar;   
    public bool mostrarCorreta;
    public float tempoResponder;
    private int nPropaganda;


    [Header("Configuração dos Perguntas")]
    public string[] perguntas;
    public Sprite[] perguntasImg;
    public string[] correta;
    private List<int> listaPerguntas;
    private int qtdPerguntas;

    [Header("Configuração das alternativas")]
    public string[] alternativasA;
    public string[] alternativasB;
    public string[] alternativasC;
    public string[] alternativasD;

    private int idResponder,qtdAcertos,idTema,idBtnCorreto;
    private float qtdRespondida, percProgresso, percTempo, tempTeime;
    private int idpergunta;
    private bool exibindoCorreta;

    [Header("Configuração dos Paineis")]
    public GameObject[] paineis;



   


    private soundController soundController;
    private bool pediurevelacao;


    private BannerView bannerView;
    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;


    public int denarios { get; private set; }

    void Start()
    {
        idResponder = GameManager.Instance.idResponder;

        MobileAds.Initialize(initStatus => { });

        if (GameManager.Instance.showAds)
        {
            this.RequestBanner();
            CreateAndLoadRewardedAd();
            RequestInterstitial();
        }
   
        // this.rewardedAd = new RewardedAd(rwardTeste);


        soundController = soundController.FindObjectOfType(typeof(soundController)) as soundController;
        denarios= PlayerPrefs.GetInt("denarios");

        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();
        idTema = PlayerPrefs.GetInt("idTema");
      
        NomeTemaText.text = PlayerPrefs.GetString("nomeTema");

        barraTempo.SetActive(false);

        if (PerguntaIMG == true)
        {
            montarListaPerguntasIMG();

        }
        else
        {
            montarListaPerguntas();

        }

        double percentual = 20.0 / 100.0; // 15%
        double valor_final = (perguntas.Length * percentual);
        int i = (int)Math.Ceiling(valor_final);

        qtdRevelacao = i;
        botoes[7].GetComponentInChildren<Text>().text = "Resposta (" + qtdRevelacao + ")";
    //    botoes[9].GetComponentInChildren<Text>().text = "Revelação (" + qtdRevelacao + ")";


        barraTempoControle();
        progressaoBarra();




        
            idResponder -=1;
       
        

        DenariosNaPartidaText.text = GameManager.Instance.qtdCorretasnafase.ToString();

        soundController.retornarSom();

        proximaPergunta();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-5058605023723222/2881312694";

        //string adUnitIdTetste = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
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


    private void ShowAd()
    {
        UserChoseToWatchAd();
    }

    private void UserChoseToWatchAd()
{
  if (this.rewardedAd.IsLoaded()) {
    this.rewardedAd.Show();
  }
}
    public void CreateAndLoadRewardedAd()
    {
       // string rwardTeste = "ca-app-pub-3940256099942544/5224354917";

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

        if (jogarComTempo == true && exibindoCorreta== false)
        {
            tempTeime += Time.deltaTime;
            
            barraTempoControle();
        }

        if (tempTeime > tempoResponder && idResponder < listaPerguntas.Count)
        {


            paineis[0].SetActive(false);
            paineis[1].SetActive(false);
            paineis[2].SetActive(true);
            paineis[3].SetActive(false);



            tempTeime = 0;
            tempoResponder = 90;

         //   Advertisement.Load("Interstitial_Android");

            //   Advertisement.Show("Interstitial_Android");

            if (nPropaganda < 3)
            {
                tempTeime = 0;
                tempoResponder = tempoResponder - 5.0f;
                nPropaganda += 1;

            }
            else
            {
                tempTeime = 0;

                tempoResponder = 7.0f;

            }


            // proximaPergunta();


        }
    }

    public void montarListaPerguntas()
    {

        int idCena = PlayerPrefs.GetInt("idTema");

    



        perguntas = GameManager.Instance.bdPerguntas;
        correta = GameManager.Instance.correta;
        alternativasA = GameManager.Instance.alternativasA;
        alternativasB = GameManager.Instance.alternativasB;
        alternativasC = GameManager.Instance.alternativasC;
        alternativasD = GameManager.Instance.alternativasD;

        listaPerguntas = new List<int>();
         qtdPerguntas = perguntas.Length;

        print("perguntas.Length "+ perguntas.Length);

        for (int i = 0; i < qtdPerguntas; i++)
        {
            listaPerguntas.Add(i);
        }




        GameManager.Instance.valorQuestao = 10 / (float)perguntas.Length;




        if (perguntaAleatoria == true)
        {


            bool addPergunta = true;


         


            while (listaPerguntas.Count < qtdPerguntas)
            {
                addPergunta = true;

                int rand = 0;

               
                
                    rand = Random.Range(0, perguntas.Length);
                
                

                foreach (int idp in listaPerguntas)
                {

                    if (idp == rand)
                    {
                        addPergunta = false;

                    }
                }
                if (addPergunta == true) { listaPerguntas.Add(rand); }



            }



        }
        else
        {
           
              
            


          
        }
      

           
        
            Perguntaext.text = perguntas[listaPerguntas[idResponder]];

        

        Perguntaext.text = perguntas[listaPerguntas[idResponder]];


        if (utilizarAlternativa == true)
        {
            print("alternativas");
            print("A :  " + alternativasA[listaPerguntas[idResponder]]);
            alternativaA.text = alternativasA[listaPerguntas[idResponder]];
            alternativaB.text = alternativasB[listaPerguntas[idResponder]];
            alternativaC.text = alternativasC[listaPerguntas[idResponder]];
            dica.text = alternativasD[listaPerguntas[idResponder]];
        }

        if (alternativasD[idResponder] != null)
        {

            botoes[6].gameObject.SetActive(true);
       //     botoes[7].gameObject.SetActive(true);
            botoes[6].interactable = true;
       //     botoes[7].interactable = true;

            //dica.text = alternativasD[listaPerguntas[idResponder]];

        }
        else
        {
            botoes[6].gameObject.SetActive(false);
       //     botoes[7].gameObject.SetActive(false);
            botoes[6].interactable = false;
       //     botoes[7].interactable = false;

        }




    }



    public void montarListaPerguntasIMG()
    {

        if (qtdPerguntas > perguntasImg.Length)
        {
            qtdPerguntas = perguntasImg.Length;
        }
        GameManager.Instance.valorQuestao= 10 / (float)qtdPerguntas;

        if (perguntaAleatoria == true)
        {


            bool addPergunta = true;

          
            



            while (listaPerguntas.Count < qtdPerguntas)
            {
                addPergunta = true;

                int rand = 0;

            
                    rand = Random.Range(0, perguntasImg.Length);


               

                foreach (int idp in listaPerguntas)
                {

                    if (idp == rand)
                    {
                        addPergunta = false;

                    }
                }
                if (addPergunta == true) { listaPerguntas.Add(rand); }



            }



        }
        else
        {
            
                for (int i = 0; i < qtdPerguntas; i++)
                {
                    listaPerguntas.Add(i);
                }
           



        }
       
            PerguntaIMG.sprite = perguntasImg[listaPerguntas[idResponder]];

       



        if (utilizarAlternativa == true)
        {
            print("alternativas");
            print("A :  " + alternativasA[listaPerguntas[idResponder]]);
            alternativaA.text = alternativasA[listaPerguntas[idResponder]];
            alternativaB.text = alternativasB[listaPerguntas[idResponder]];
            alternativaC.text = alternativasC[listaPerguntas[idResponder]];
            dica.text= alternativasD[listaPerguntas[idResponder]];

        }
    }

    public void revelacao()
    {



        //pediurevelacao = true;
        //paineis[0].SetActive(false);
        //paineis[1].SetActive(false);
        //paineis[2].SetActive(true);
        //paineis[3].SetActive(false);
        //msg1Txt.text = "Olha como é boa uma revelação";



        //denarios = denarios+1;
        //PlayerPrefs.SetInt("denarios", denarios);


        //msg2Txt.text = "1) Você ganha um denário \n  2) Não perde ponto \n3) Aprende mais da Bíblia \n (Basta ver um vídeo)";
        //msg3Txt.text = "1) Essa pergunta não contará para sua pontuação final.";
        //botoes[5].interactable = true;
        //botoes[5].image.color = Color.white;


        qtdRevelacao -= 1;
        botoes[7].GetComponentInChildren<Text>().text = "Revelação (" + qtdRevelacao + ")";
       // botoes[9].GetComponentInChildren<Text>().text = "Revelação (" + qtdRevelacao + ")";


        Debug.Log("revelacao"  + idBtnCorreto);


        tempTeime = 0;
        tempoResponder = 500;
        verBotaoCorreto();
        botoes[idBtnCorreto].image.color = Color.green;



        if (GameManager.Instance.showAds)
        {
            ShowAd();

        }

        if (qtdRevelacao == 0)
        {
            botoes[7].gameObject.SetActive(false);
           // botoes[9].gameObject.SetActive(false);
        }




        //  botoes[idBtnCorreto].image.color = Color.green;


    }
    public void responder(string alternativa)
    {


        if (exibindoCorreta == true) { return; }


        qtdRespondida += 1;
        // progressaoBarra();


        if (correta[listaPerguntas[idResponder]] == alternativa)
        {
            denarios = denarios + 1;
            GameManager.Instance.qtdCorretasnafase = GameManager.Instance.qtdCorretasnafase + 1;
            DenariosNaPartidaText.text = GameManager.Instance.qtdCorretasnafase.ToString();

            PlayerPrefs.SetInt("denarios", denarios);

            qtdAcertos += 1;

            soundController.playAcerto();
        }
        else
        {
            soundController.playErro();
            denarios = denarios - 1;
            PlayerPrefs.SetInt("denarios", denarios);

        }

        verBotaoCorreto();

        print("idBtnCorreto" + idBtnCorreto);


        if (mostrarCorreta == true)
        {

            foreach (Button i in botoes)
            {
                i.image.color = corErro;
            }

            exibindoCorreta = true;
            botoes[idBtnCorreto].image.color = corAcerto;
            StartCoroutine("mostrarAlternativaCorreta");

        }
        else
        {
            exibindoCorreta = true;
            StartCoroutine("aguardarProxima");

        }

    }

    private void verBotaoCorreto()
    {
        if (utilizarAlternativa == true)
        {

            switch (correta[listaPerguntas[idResponder]])
            {
                case "A":
                    idBtnCorreto = 2;
                    break;
                case "B":
                    idBtnCorreto = 3;
                    break;
                case "C":
                    idBtnCorreto = 4;
                    break;
                case "D":
                    idBtnCorreto = 5;
                    break;

            }
        }
        else
        {
            switch (correta[listaPerguntas[idResponder]])
            {
                case "A":
                    idBtnCorreto = 0;
                    break;
                case "B":
                    idBtnCorreto = 1;
                    break;


            }
        }
    }

    IEnumerator aguardarProxima()
    {
        yield return new WaitForSeconds(0.9f);
        exibindoCorreta = false;

        proximaPergunta();
    }

    IEnumerator mostrarAlternativaCorreta()
    {

        for (int i = 0; i < qtdPiscar; i++)
        {

            print(idBtnCorreto.ToString());
            print(idBtnCorreto.ToString());

            botoes[idBtnCorreto].image.color = corAcerto;
            yield return new  WaitForSeconds(0.2f);
            botoes[idBtnCorreto].image.color = Color.white;
            yield return new  WaitForSeconds(0.2f);
        }

        foreach (Button i in botoes)
        {
            i.image.color = Color.white;
        }
        exibindoCorreta = false;
        proximaPergunta();

    }


    public void verDica()
    {


      

        if (GameManager.Instance.showAds)
        {
            MostrarInterstitial();

        }






        denarios = denarios + 5;
        PlayerPrefs.SetInt("denarios", denarios);
        msg1Txt.text = "Esse versículo vai ajudar a responder";
        msg2Txt.text = "Dica da pergunta";
        msg2Txt.text = alternativasD[listaPerguntas[idResponder]];
      //  idResponder -= 1;

        botoes[5].interactable = false;
        botoes[5].image.color = Color.black;

        


    }


    public void pause()
    {
        soundController.Instance.pararSom();
        verDicaNoJogo();
    }

    public void verDicaNoJogo()
    {

        paineis[0].SetActive(false);
        paineis[1].SetActive(false);
        paineis[2].SetActive(true);
        paineis[3].SetActive(true);
        paineis[4].SetActive(false);
        paineis[5].SetActive(false);
        verDica();

        //msg1Txt.text = "Olha como é bom pedir uma dica";


        //if (GameManager.Instance.viuMensagemDica == false)
        //{
        //    GameManager.Instance.viuMensagemDica = true;

        //}
        //else
        //{
        //    verDica();
        //}




        tempTeime = 0;
        tempoResponder = 900;


    }


    public void ajuda()
    {
        registrarEscolhadeConteudoNofirebase("AjudaPerguntas");
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        print("botaoAjuda");
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


    public void proximaPerguntaEscolha()
    {


        soundController.Instance.retornarSom();


        paineis[4].SetActive(true);
        paineis[5].SetActive(true);

        organizarPAinel();

        //denarios = denarios - 3;
        //PlayerPrefs.SetInt("denarios", denarios);
        //paineis[3].SetActive(true);


        //msg1Txt.text = "Acabou o tempo \n O que você deseja?";
        //msg2Txt.text = "1) Ver Dica na Bíblia \n  2) Ganhar 2 Denários \n3) Responder de novo \n (Basta ver um vídeo)";
        //msg3Txt.text = "1) Perder 3 denários \n  2) Não aprender \n 3) Responder próxima";
        //botoes[5].interactable = true;
        //botoes[5].image.color=Color.white;

        //progressaoBarra();



    }


    public void temas()
    {
        GameManager.Instance.idResponder = 0;
        SceneManager.LoadScene("temas");
    }



    public void voltar()
    {

        if (idResponder == 0)
        {
            GameManager.Instance.idResponder = 0;

            SceneManager.LoadScene("temas");

        }
        idResponder -= 2;
        proximaPergunta();
    }


        public void proximaPergunta()
    {
        paineis[2].SetActive(false);

        idResponder += 1;
        tempTeime = 0;
       tempoResponder = 20;



        //if (idResponder <= 1)
        //{
        //    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        //    Advertisement.Banner.Show("Banner_Android");


        //}


        //if (idResponder >= listaPerguntas.Count - 1)
        //{
        //    Advertisement.Banner.Hide();


        //}





        //if (correta[idResponder].Length > 1)
        //{
        //    utilizarAlternativa = true;
        //    paineis[0].SetActive(false);
        //    paineis[1].SetActive(true);

        //}
        //else
        //{
        //    utilizarAlternativa = false;
        //    paineis[0].SetActive(true);
        //    paineis[1].SetActive(false);
        //}



        if (idResponder < listaPerguntas.Count)
        {


            Debug.Log("A dica é  " + alternativasD[idResponder]);
            if (alternativasD[idResponder] != null)
            {

                botoes[6].gameObject.SetActive(true);
              //  botoes[7].gameObject.SetActive(true);
                botoes[6].interactable = true;
             //   botoes[7].interactable = true;

                // dica.text = alternativasD[listaPerguntas[idResponder]];

            }
            else
            {
                botoes[6].gameObject.SetActive(false);
             //   botoes[7].gameObject.SetActive(false);
                botoes[6].interactable = false;
            //    botoes[7].interactable = false;

            }

            organizarPAinel();

            if (perguntaComImg == true)
            {
                PerguntaIMG.sprite = perguntasImg[listaPerguntas[idResponder]];
                DenariosText.text = PlayerPrefs.GetInt("denarios", 0).ToString();

            }
            else
            {
                Perguntaext.text = perguntas[listaPerguntas[idResponder]];
                DenariosText.text = PlayerPrefs.GetInt("denarios", 0).ToString();


            }

            if (utilizarAlternativa == true)
            {


                alternativaA.text = alternativasA[listaPerguntas[idResponder]];
                alternativaB.text = alternativasB[listaPerguntas[idResponder]];
                alternativaC.text = alternativasC[listaPerguntas[idResponder]];




            }

            progressaoBarra();


        }
        else
        {

            SceneManager.LoadScene("resultado");
            GameManager.Instance.tempo = Time.timeSinceLevelLoad;
            Debug.Log("GameManager.Instance.tempo");

           //   calcularNotaFinal();
        }

    }

    private void organizarPAinel()
    {
        if (!alternativasA[listaPerguntas[idResponder]].Equals(""))
        {

            paineis[0].SetActive(false);
            paineis[1].SetActive(true);
            paineis[2].SetActive(false);

            utilizarAlternativa = true;

        }
        else
        {

            if (correta[listaPerguntas[idResponder]].Length > 1)
            {



                GameManager.Instance.idResponder = idResponder;
                SceneManager.LoadScene("forca");

            }



            paineis[0].SetActive(true);
            paineis[1].SetActive(false);
            paineis[2].SetActive(false);

            utilizarAlternativa = false;


        }
    }

    private void progressaoBarra()
    {


        if(idResponder< listaPerguntas.Count)
        {

            float val2 = (float)idResponder;
            InfoRespostaTxt.text =  (val2 + 1) + " de " + listaPerguntas.Count;
            percProgresso = (val2 + 1) / listaPerguntas.Count;
            barraProgresso.transform.localScale = new Vector3(percProgresso, 1, 1);
        }
        

    }

    void barraTempoControle()
    {
        if (jogarComTempo == true)
        {
            barraTempo.SetActive(true);
        }

        percTempo = ((tempTeime - tempoResponder) / tempoResponder) * -1;
        
        if (percTempo < 0)
        {
            percTempo = 0;
        }
        
        barraTempo.transform.localScale = new Vector3(percTempo, 1, 1);

    }



}






