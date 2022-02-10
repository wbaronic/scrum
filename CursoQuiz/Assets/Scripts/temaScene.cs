using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class temaScene : MonoBehaviour
{
    public Text nomeTemaTxt;
    public Text introducao;

    public Button btnJogar;


    public GameObject[] btnPaginacao;
    public GameObject[] painelTemas;
    public GameObject[] painelControle;

    private bool ativarPaginacao;
    private int idpagina;
    public Text DenariosText;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("iniciando start das fases");
      //  denarios = PlayerPrefs.GetInt("denarios");
        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();

        OnOffButtons();
        telaInicial();
    }


    public void irPAraEstudos()
    {

        GameManager.Instance.idResponder = 0;
        SceneManager.LoadScene("titulo");

    }

    void OnOffButtons()
    {

      //  btnJogar.interactable = false;


        foreach (GameObject p in painelTemas)
        {
            p.SetActive(false);
        }

        painelTemas[0].SetActive(true);





        if (painelTemas.Length > 1)
        {
            ativarPaginacao = true;
        }
        else
        {
            ativarPaginacao = false;
        }


        foreach (GameObject b in btnPaginacao)
        {
            b.SetActive(ativarPaginacao);
        }

    }

    public void Aula()
    {

        int v = GameManager.Instance.ultimafase;
        Tema tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == v.ToString());
        Application.OpenURL(tema.linkAula);

    }


    public void jogar()
    {

        // int idCena = GameManager.Instance.ultimafase;
        // int denarios = PlayerPrefs.GetInt("denarios",0);
        //Tema tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idCena.ToString());


        GameManager.Instance.idResponder = 0;
        SceneManager.LoadScene("introducaoFase");
           // painelControle[0].SetActive(false);

        //    painelControle[1].SetActive(true);
        


    }

    public void telaInicial()
    {



        foreach (GameObject p in painelTemas)
        {
            p.SetActive(false);
        }

        painelTemas[0].SetActive(true);
        painelControle[0].SetActive(true);

        painelControle[1].SetActive(false);
        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();

    }

    public void btPagina(int i)
    {
         idpagina  += i;
        if (idpagina < 0)
        {
            idpagina = painelTemas.Length - 1;
        } else if (idpagina >= painelTemas.Length)
        {
            idpagina = 0;
        }


        btnJogar.interactable = false;
        nomeTemaTxt.text = "Selecione um tema";
        nomeTemaTxt.color = Color.white;
        foreach (GameObject p in painelTemas)
        {
            p.SetActive(false);
        }

        painelTemas[idpagina].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
