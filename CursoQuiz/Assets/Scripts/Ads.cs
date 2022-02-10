using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class Ads : MonoBehaviour
{

    // Start is called before the first frame update
    //void Start()
    //{
    //    Advertisement.Initialize("4246434");

    //}

    //// Update is called once per frame
    //private void HandleShowResult(ShowResult result)
    //{


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


    //public void showInterstitial_Android()
    //{


        
    //     Advertisement.Load("Interstitial_Android");

    //    Advertisement.Show("Interstitial_Android");


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



    //private void DobrarHandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            Debug.Log("The ad was successfully shown.");
    //             ;

    //            int denarios = PlayerPrefs.GetInt("denarios");
    //            denarios = denarios + (GameManager.Instance.qtdCorretasnafase * 2);
    //            PlayerPrefs.SetInt("denarios", denarios);
    //            print("Denarios na ultima rodada" + GameManager.Instance.qtdCorretasnafase);
    //            GameManager.Instance.qtdCorretasnafase = 0;
    //            int idCena = GameManager.Instance.ultimafase;
    //            idCena = idCena + 1;
    //            GameManager.Instance.jogarFase(idCena);
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}
    //public void DobrarshowRegawardAd()
    //{

    //    if (Advertisement.IsReady("Denarios"))
    //    {


    //        var options = new ShowOptions { resultCallback = DobrarHandleShowResult };

    //        Advertisement.Show("Denarios", options);


    //        //


    //    }

    //}

}
