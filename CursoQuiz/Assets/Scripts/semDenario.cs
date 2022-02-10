using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class semDenario : MonoBehaviour
{
   // public Text id;
   // public Text nome;
    //public Text linkAula;
    public Text mensagemInicial;
    //public string mensagemDerrota;
    //public string mensagemVitoria;
    public Text denariosNecessario;
    public Text denarios;

    // Start is called before the first frame update
    void Start()
    {

        int v = GameManager.Instance.ultimafase;
        Tema tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == v.ToString());
     //   id.text = tema.id;
     //   nome.text = tema.nome;
        mensagemInicial.text = tema.mensagemInicial;
        denariosNecessario.text = tema.denarios;
        denarios.text = GameManager.Instance.getDenarios();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void IrPAraTemas()
    {

        SceneManager.LoadScene("temas");

    }
}
