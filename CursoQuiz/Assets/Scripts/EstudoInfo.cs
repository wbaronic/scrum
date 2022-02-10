using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstudoInfo : MonoBehaviour
{


    public int idEstudo;
    public string nomeEstudo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void selecinarEstudo()
    {



        //   PlayGames.platform.Events.IncrementEvent(GPGSIds.event_voc_ganhou_pontos_para_se_tornar_um__dos_maiores_mestres_da_bblia, 1);

        // PlayerPrefs.SetInt("idTema", idTema);
       // GameManager.Instance.estudoEscolhido = idEstudo;
        GameManager.Instance.carregarTemaEscolhido();
        SceneManager.LoadScene("temas");




    }


}
