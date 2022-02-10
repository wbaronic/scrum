
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System.Text;
using System.Globalization;

public class PlayFabManager : MonoBehaviour
{





    [Header("ConfiguraГЦo dos Textos")]
    public Text NomeTemaText;
    public Text DenariosText;
    public Text DenariosNaPartidaText;


    [Header("nota")]

    public Text notaFinalTxt;
    private float notaFinal;
    private int idTema;
    private int qtdAcertos, notaMin1Estela, notaMin2Estrelas, nEstelas;

    [Header("ConfiguraГЦo dos mensagens")]
    public string[] mensagens1;
    public string[] mensagens2;
    public Color[] coresMensagem;
    public Text msg1Txt;
    public Text msg2Txt;
    public GameObject[] estrela;


    [Header("PlayFab")]

    public GameObject nameWindow;
    public GameObject LeaderboardWindow;

    public InputField nameInput;
    public GameObject nameError;


    public GameObject rowPrefab;
    public Transform rowsParent;


    string LoggedId;

    public static PlayFabManager Instance { get; set; }


    // Start is called before the first frame update
    void Start()
    {

        int ultimafase = GameManager.Instance.ultimafase;
        ultimafase += 1;
        print("pegar perguntas no tema");
        GameManager.Instance.pegarPerguntasDaFaseNofirebase(ultimafase.ToString());
        calcularNotaFinal();

        login();

        NomeTemaText.text = PlayerPrefs.GetString("nomeTema");
        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();
        DenariosNaPartidaText.text = GameManager.Instance.qtdCorretasnafase.ToString();

        idTema = PlayerPrefs.GetInt("idTema");
        notaMin1Estela = PlayerPrefs.GetInt("notaMin1Estela");
        notaMin2Estrelas = PlayerPrefs.GetInt("notaMin2Estrelas");





    }

    //ControleJogo

    public void registrareVENTOFirebase(string tema, int secretID)
    {


        Firebase.Analytics.FirebaseAnalytics.LogEvent(
  Firebase.Analytics.FirebaseAnalytics.EventLevelEnd,
  new Firebase.Analytics.Parameter(
    Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, tema.ToLower() + secretID)
); ;



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



    public void jogarPRoximo()
    {

        print("JogarProxima");
        int idCena = GameManager.Instance.ultimafase;
        idCena = idCena + 1;
        GameManager.Instance.ultimafase = idCena;

        SceneManager.LoadScene("introducaoFase");




    }

    public void jogarNovamente()
    {
        int idCena = GameManager.Instance.ultimafase;
        if (idCena != 0)
        {
            GameManager.Instance.jogarFase(idCena);
        }
    }




    public void irPAraTemas()
    {

        SceneManager.LoadScene("temas");

    }

    public void AddScoreToLeaderboard(int score)
    {
        Debug.Log("score " + score);
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate
            {
                StatisticName="teste",
                Value =score
            } }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSuccess, Onerror);
    }
    // Update is called once per frame

    void OnSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Postou ponto");
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


        LoggedId = result.PlayFabId;
        Debug.Log("deu certo login");
        name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {


            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        }
        else
        {

        }
        AddScoreToLeaderboard((int)notaFinal);



        if (name == null || name.Equals(""))
        {
            Debug.Log("name null, mostrar nameWindow");
            Debug.Log(LoggedId);

            nameWindow.SetActive(true);
            LeaderboardWindow.SetActive(false);

        }
        else
        {
            Debug.Log("name completp, mostrar leadrarboar");
            Debug.Log(name);

            nameWindow.SetActive(false);
            LeaderboardWindow.SetActive(true);
            getLeaderBoardAround();

        }


    }

    public void SubimitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {

            DisplayName = nameInput.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OndisplayNameUpdate, Onerror);
    }

    void OndisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {

        nameWindow.SetActive(false);
        LeaderboardWindow.SetActive(true);
        getLeaderBoardAround();

    }

    void Onerror(PlayFabError playFabError)
    {
        Debug.Log("deu erro leadeboard");

        Debug.Log(playFabError.GenerateErrorReport());

    }


    public void getLeaderBoardAround()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "teste",
            MaxResultsCount = 5

        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, onLeaderboardAroundGet, Onerror);
    }




    public void onLeaderboardAroundGet(GetLeaderboardAroundPlayerResult result)
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

            if (item.PlayFabId == LoggedId)
            {
                texts[0].color = Color.green;
                texts[1].color = Color.green;
                texts[2].color = Color.green;

            }


            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }

    }


    public void getLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "teste",
            StartPosition = 0,
            MaxResultsCount = 20

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
            texts[1].text = item.DisplayName.ToString();
            texts[2].text = item.StatValue.ToString();



            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }

    }


    void calcularNotaFinal()
    {

        GameManager.Instance.idResponder = 0;

        notaFinal = Mathf.RoundToInt(GameManager.Instance.valorQuestao * GameManager.Instance.qtdCorretasnafase);

        if (notaFinal > PlayerPrefs.GetInt("notaFinal" + idTema))
        {
            PlayerPrefs.SetInt("notaFinal" + idTema.ToString(), (int)notaFinal);

        }



        //int nEstelas = 0;

        if (notaFinal == 10)
        {
            nEstelas = 3;
            //   PlayGames.Instance.UnlockAchievement(GPGSIds.achievement_trs_estrelas);

        }
        else if (notaFinal >= notaMin2Estrelas)
        {
            nEstelas = 2;
        }
        else if (notaFinal >= notaMin1Estela)
        {
            nEstelas = 1;
        }

        int idCena = GameManager.Instance.ultimafase;

        Tema query = GameManager.Instance.temas.FindLast(fruit => fruit.id.Equals(idCena.ToString()));


        string novoNome = RemoveSpecialCharacters(query.nome, false);
        registrareVENTOFirebase(novoNome, idCena);

        // if (!query.achiviments.Equals("") && notaFinal > 7)
        //{
        //  PlayGames.Instance.UnlockAchievement(query.achiviments);


        // }


        //   PlayGames.Instance.registrarEventodoTema(query.eventos);



        notaFinalTxt.text = notaFinal.ToString();
        //notaFinalTxt.color = coresMensagem[nEstelas];


        msg1Txt.text = mensagens1[nEstelas];
        msg2Txt.text = mensagens2[nEstelas];
        msg1Txt.color = coresMensagem[nEstelas];


        //foreach (GameObject e in estrela)
        //{
        //    e.SetActive(false);
        //}


        //for (int i = 0; i < nEstelas; i++)
        //{
        //    estrela[i].SetActive(true);
        //}




        PlayerPrefs.SetFloat(GameManager.Instance.estudoEscolhido + GameManager.Instance.ultimafase, notaFinal);



        //paineis[0].SetActive(false);
        //paineis[1].SetActive(false);
        //paineis[2].SetActive(true);
        //paineis[3].SetActive(false);
    }


}