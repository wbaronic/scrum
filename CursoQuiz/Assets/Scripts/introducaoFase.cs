
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;



using System;
public class introducaoFase : MonoBehaviour
{

    public Text id;
    public Text nome;
    //public Text linkAula;
    public Text mensagemInicial;
    //public string mensagemDerrota;
    //public string mensagemVitoria;
    public Text denariosNecessario;
    public Text denarios;
    private soundController soundController;
    public Button buttonPlay;
    public Button buttonAula;
    private int nPeruntas;
    public Sprite Image1;
    public Sprite ImagePlayAguardando;
    private RewardedAd rewardedAd;
    private int denariosV;
    string rewardValendo = "ca-app-pub-5058605023723222/3466167446";
    int denariosNecessarioV;
    Tema tema;
    public GameObject fotoLucas;
    public GameObject outrosTemas;

    // Start is called before the first frame update
    void Start()
    {


        this.rewardedAd = new RewardedAd(rewardValendo);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);




        soundController = soundController.FindObjectOfType(typeof(soundController)) as soundController;
        int v = GameManager.Instance.ultimafase;

        PlayerPrefs.SetInt("ultimafase", v);

        tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == v.ToString());

        if (tema == null)
        {
            int idSalvo = PlayerPrefs.GetInt("idTema", 1);
            GameManager.Instance.ultimafase = idSalvo;
            tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idSalvo.ToString());
        }



        id.text = tema.id;




        PlayerPrefs.SetString("estudoEscolhido", GameManager.Instance.estudoEscolhido);

        PlayerPrefs.SetString("nomeTema", tema.nome);


        denariosNecessarioV = System.Convert.ToInt32(tema.denarios);

        Debug.Log("tema escolhido" + tema.id);





      







        if (GameManager.Instance.estudoEscolhido.Equals("Mateus")){
            fotoLucas.SetActive(true);
            outrosTemas.SetActive(false);
        }
        else
        {
            fotoLucas.SetActive(false);
            outrosTemas.SetActive(true);
            mensagemInicial.gameObject.SetActive(true);
        }
        
        soundController.playFase(GameManager.Instance.estudoEscolhido,tema.id);


            

        

        Debug.Log("Link Aula " + tema.linkAula);

        if (tema.linkAula == null || tema.linkAula.Equals(""))
        {
            Debug.Log("Link Aula " + tema.linkAula);
            buttonAula.interactable = false;

        }

        denariosV = PlayerPrefs.GetInt("denarios", 0);


        if (denariosNecessarioV <= denariosV)
        {
            nome.text = tema.nome;
            mensagemInicial.text = tema.mensagemInicial;
            denariosNecessario.text = tema.denarios;
            denarios.text = GameManager.Instance.getDenarios();
            buttonPlay.GetComponent<Image>().sprite = ImagePlayAguardando;
            buttonPlay.enabled = false;
        }
        else
        {
            id.text = "OPS";

            nome.text = tema.nome;
            mensagemInicial.gameObject.active = true;
            mensagemInicial.text = "Você não tem denários Suficientes. \n Aperte no mais(+) para ganhar 10 Denários !! \n Ou compre denários infinitos :) é bem baratinho";
            denariosNecessario.text = tema.denarios;
            denarios.text = GameManager.Instance.getDenarios();


        }


    }

    // Update is called once per frame
    void Update()
    {



     
            if (GameManager.Instance.perguntasGerais.Count != 0 && (nPeruntas != GameManager.Instance.temas.Count) && denariosNecessarioV <= denariosV)
            {
                nPeruntas = GameManager.Instance.temas.Count;
                buttonPlay.GetComponent<Image>().sprite = Image1;

                buttonPlay.enabled = true;


                if (!soundController.Instance.isPlayng() && denariosNecessarioV <= denariosV )
                {
                    Jogar();
                }

            

        }







       


       

        }


    public void Aula()
    {

        int v = GameManager.Instance.ultimafase;
        Tema tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == v.ToString());
        Application.OpenURL(tema.linkAula);

    }

    public void irPAraCena(string nomeCena)
    {


        SceneManager.LoadScene(nomeCena);

    }

    //private void HandleShowResult(ShowResult result)
    //{

    //   // PlayGames.Instance.maisMoedas();

    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            Debug.Log("The ad was successfully shown.");

    //            int denarios = PlayerPrefs.GetInt("denarios");
    //            denarios = denarios + 10;
    //            PlayerPrefs.SetInt("denarios", denarios);
    //            GameManager.Instance.jogarFase(0);
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }


    //}

    //public void showRegawardAd()
    //{

    //    if (Advertisement.IsReady("Denarios"))
    //    {


    //        var options = new ShowOptions { resultCallback = HandleShowResult };

    //        Advertisement.Show("Denarios", options);


    //        //


    //    }

    //}


    public void CreateAndLoadRewardedAd()
    {

        string rewardValendo = "ca-app-pub-5058605023723222/3466167446";

        this.rewardedAd = new RewardedAd(rewardValendo);

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
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

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        denariosV = denariosV + 10;
        PlayerPrefs.SetInt("denarios", denariosV);
        GameManager.Instance.jogarFase(0);


    }


    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    public void Jogar()
    {

     
        int denariosV = PlayerPrefs.GetInt("denarios", 0);

        Debug.Log("Jogar");

        if (denariosNecessarioV > denariosV && GameManager.Instance.showAds)
        {

           

            UserChoseToWatchAd();


            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;


            Debug.Log("semDenarioVerPropaganda");

            GameManager.Instance.pegarPerguntasDaFaseNofirebase(GameManager.Instance.ultimafase.ToString());


        }
        else
        {
            Debug.Log("else");

            GameManager.Instance.jogarFase(0);



        }


    }


}