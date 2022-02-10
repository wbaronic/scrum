using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class btnComandos : MonoBehaviour


{
    // Start is called before the first frame update



    public GameObject rowPrefab;
    public Transform rowsParent;
    private Button btnTema;
    public Button btnCompras;

    public Text nomeCategoria;
    private int indiceAtual;
    private string[] temas;
    private string[] nomeBotoes;

    public void irPAraCena(string nomeCena){

        btnTema = GetComponent<Button>();

        SceneManager.LoadScene(nomeCena);

}

    public void anteriorTema()
    {
        indiceAtual -= 1;

      //  configurartemas();
    }

    public void proximoTema()
    {
        indiceAtual += 1;

     //   configurartemas();

    }

   //private void configurartemas()
   // {



   //     if (indiceAtual > temas.Length-1)
   //     {
   //         indiceAtual = 0;
   //     }else if (indiceAtual < 0)
   //     {
   //         indiceAtual = temas.Length-1;
   //     }

   //     foreach (Button item in botoes)
   //     {
   //         item.gameObject.SetActive(false);
   //     }

   //     switch (indiceAtual)
   //     {
   //         case 0:
   //             botoes[0].gameObject.SetActive(true);
   //           //  botoes[0].GetComponentInChildren<Text>().text = nomeBotoes[0];

   //             botoes[1].gameObject.SetActive(true);
   //         //    botoes[1].GetComponentInChildren< Text >().text = nomeBotoes[1];


   //             nomeCategoria.text = temas[0];
   //             break;

   //         default :

   //             Debug.Log("indece Atual=" + indiceAtual);
   //             botoes[indiceAtual*2].gameObject.SetActive(true);
   //         //    botoes[indiceAtual * 2].GetComponentInChildren<Text>().text = nomeBotoes[indiceAtual * 2];

   //             botoes[indiceAtual*2+1].gameObject.SetActive(true);
   //          //   botoes[indiceAtual * 2+1].GetComponentInChildren<Text>().text = nomeBotoes[indiceAtual * 2 + 1];

   //             nomeCategoria.text = temas[indiceAtual];

   //             break;
    

   //     }
   // }

    public void irPAraLeaderboardCompleto()
    {


        SceneManager.LoadScene("ResultadoCompleto");

    }

    void Start()
    {

        if (!GameManager.Instance.showAds)
        {
            btnCompras.gameObject.SetActive(false);
        }
       // configurartemas();
        login();
    }

        public void sair(){

    Application.Quit();

}

    void login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }

        };

        PlayFabClientAPI.LoginWithCustomID(request, onLoginSucces, Onerror);

    }

    void onLoginSucces(LoginResult result)
    {
        getLeaderBoard();

    }


    public void jogarNovamente()
    {
        int idCena = GameManager.Instance.ultimafase;
        if (idCena != 0)
        {
            GameManager.Instance.jogarFase(idCena);
        }
    }


    public void jogarPRoximo()
    {

        print("JogarProxima");
        int idCena = GameManager.Instance.ultimafase;
        idCena = idCena + 1;
       GameManager.Instance.ultimafase=idCena;

        SceneManager.LoadScene("introducaoFase");




    }

    public void opcoes(bool onOff)
    {
        //painel1.SetActive(onOff);
        //painel2.SetActive(!onOff);


    }

    public void zerarProgresso()
    {
        PlayerPrefs.DeleteAll();
    }

    


 

    public void getLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "teste",
            StartPosition = 0,
            MaxResultsCount = 5

        };
        PlayFabClientAPI.GetLeaderboard(request, onLeaderboardGet, Onerror);
    }


    public void onLeaderboardGet(GetLeaderboardResult result)
    {


        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);

        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            if (item.DisplayName != null)
            {
                texts[1].text = item.DisplayName.ToString();

            }
            else
            {
                texts[1].text = item.PlayFabId.ToString();

            }
            texts[2].text = item.StatValue.ToString();



            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }

    }

    void Onerror(PlayFabError playFabError)
    {
        Debug.Log("deu erro leadeboard");

        Debug.Log(playFabError.GenerateErrorReport());

    }




}
