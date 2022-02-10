using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class leadeaboardCompleto : MonoBehaviour
{


    public GameObject rowPrefab;
    public Transform rowsParent;
    string LoggedId;

    // Start is called before the first frame update
    void Start()
    {
        login();
    }

    // Update is called once per frame
    public void irPAraEstudos()
    {


        SceneManager.LoadScene("titulo");

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

        getLeaderBoard();







    }

    void Onerror(PlayFabError playFabError)
    {
        Debug.Log("deu erro leadeboard");

        Debug.Log(playFabError.GenerateErrorReport());

    }


    public void getLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "teste",
            StartPosition = 0,
            MaxResultsCount = 18

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

            if (item.PlayFabId == LoggedId)
            {
                texts[0].color = Color.green;
                texts[1].color = Color.green;
                texts[2].color = Color.green;

            }



            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }

    }


}
