
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class temaInfo : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Configuracao das estrelas")]

    private temaScene temaScene;
    public int idTema;
    public string nomeTema;
    public Color corTema;
    public bool requerNotaMinima;
    public int notaMinNecessaria;

    [Header("Configuracao das estrelas")] 
    public int notaMinima1Estrela;
    public int notaMinima2Estrelas;

    public Text idTemaTxt;
    public GameObject[] estrelas;
    private float notaFinal;
    private Button btnTema;
    private Tema tema;
    public Text DenariosText;
    private int nFases;
    private int nFasestemp;

    void Start()
    {
        Debug.Log("Entrou no start do temas ");


        DenariosText.text = PlayerPrefs.GetInt("denarios").ToString();

        if (!GameManager.Instance.estudos.Contains(GameManager.Instance.estudoEscolhido.ToLower()))
        {
            idTemaTxt.text = "Desculpe, esse tema ainda não está pronto. Em breve estará cheio de desafios. Agora você pode escolher outro tema";

        }
        else
        {
            idTemaTxt.text = "";

        }




        //temaScene =temaScene.FindObjectOfType(typeof(temaScene)) as temaScene;
        // idTemaTxt.text = idTema.ToString();

        // CalcularEstrelas();

        //  btnTema = GetComponent<Button>();
        // verificarNotaMinima();
        // verificarConteudd();
    }






    [Serializable]
    public struct Game
    {

        public string id;
        public string Name;
        public string Description;
    }


    [SerializeField] Game[] allGames;

    void StartFases()
    {

        List<Tema> temas = GameManager.Instance.temas;
        Debug.Log("O tamanho do tema é " + GameManager.Instance.temas.Count);

        allGames = new Game[GameManager.Instance.temas.Count];


        int x = 0;
        foreach (Tema tema in temas)
        {
            Game game = new Game();
            game.id = tema.id;
            game.Name = tema.id + " - " + tema.nome;



            notaFinal = PlayerPrefs.GetFloat(GameManager.Instance.estudoEscolhido + tema.id, 0);





            game.Description = notaFinal.ToString();


            allGames.SetValue(game, System.Convert.ToInt32(tema.id)-1);
            Debug.Log("Carregando pelo firebase " + allGames[x].Name);
            x++;

        }

        //Game game = new Game();
        //game.Name = "Gênesis";
        //game.id = Temas.Genesis;
        //game.Icon 


        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        int N = allGames.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttonTemplate, transform);
        //    g.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].Icon;
            g.transform.GetChild(1).GetComponent<Text>().text = allGames[i].Name;
            g.transform.GetChild(2).GetComponent<Text>().text = allGames[i].Description;



            if (System.Convert.ToInt32(allGames[i].Description) == 10)
            {
                g.transform.GetChild(2).GetComponent<Text>().color = Color.blue;
                //   PlayGames.Instance.UnlockAchievement(GPGSIds.achievement_trs_estrelas);

            }
            else if (System.Convert.ToInt32(allGames[i].Description) >= 5)
            {
                g.transform.GetChild(2).GetComponent<Text>().color = Color.green;
            }
            else if (System.Convert.ToInt32(allGames[i].Description) >= 0)
            {
                g.transform.GetChild(2).GetComponent<Text>().color = Color.red;
            }




            /*g.GetComponent <Button> ().onClick.AddListener (delegate() {
				ItemClicked (i);
			});*/
            g.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }

        Destroy(buttonTemplate);
    }



    private void Update()
    {
       if( GameManager.Instance.temas.Count !=0 && (nFases != GameManager.Instance.temas.Count))
        {
            StartFases();
            nFases = GameManager.Instance.temas.Count;
        }

        


    }

    void ItemClicked(int itemIndex)
    {

        Debug.Log("------------item " + itemIndex + " clicked---------------");
        Debug.Log("name " + allGames[itemIndex].Name);
        Debug.Log("desc " + allGames[itemIndex].Description);

        selecinarTema(itemIndex+1);



    }



    public void irPAraTemas()
    {


        SceneManager.LoadScene("titulo");

    }

    void verificarConteudd()
    {
        tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idTema.ToString());

        if (tema == null)
        {
            btnTema.interactable = false;

        }

        //   temaScene.bt


    }
    void verificarNotaMinima()
    {
        btnTema.interactable = false;
        if (requerNotaMinima == true)
        {
          int   notaAnterior = PlayerPrefs.GetInt("notaFinal" + (idTema-1).ToString());
            if (notaAnterior >= notaMinNecessaria)
            {
                btnTema.interactable = true;
            }

        }
        else
        {
            btnTema.interactable = true;

        }
    }

    // Update is called once per frame

    public void selecinarTema(int idTema)
    {



     //   PlayGames.platform.Events.IncrementEvent(GPGSIds.event_voc_ganhou_pontos_para_se_tornar_um__dos_maiores_mestres_da_bblia, 1);

       // PlayerPrefs.SetInt("idTema", idTema);
        GameManager.Instance.ultimafase = idTema;
        print("pegar perguntas no tema");

        tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idTema.ToString());

        
      int  denariosNecessarioV = System.Convert.ToInt32(tema.denarios);



       int denariosV = PlayerPrefs.GetInt("denarios", 0);


        if (denariosNecessarioV <= denariosV)
        {

            GameManager.Instance.pegarPerguntasDaFaseNofirebase(idTema.ToString());


        }
        //     GameManager.Instance.carregarTema();


        //  tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idTema.ToString());
        // print("Tema selecionado " + idTema);
        // print("Montando tema" + tema.id);
        //  temaScene.nomeTemaTxt.text = tema.nome;
        //temaScene.introducao.text = tema.mensagemInicial;

        // temaScene.nomeTemaTxt.color = corTema;
        // temaScene.introducao.color = corTema;

        // GameManager.Instance.carregarTema();

        //  PlayerPrefs.SetString("nomeTema", tema.nome);
        //  PlayerPrefs.SetInt("notaMinima1Estrela", notaMinima1Estrela);
        // PlayerPrefs.SetInt("notaMinima2Estrelas", notaMinima2Estrelas);

        //  temaScene.btnJogar.interactable = true;

        GameManager.Instance.idResponder = 0;
        SceneManager.LoadScene("introducaoFase");

    }

   

    public void CalcularEstrelas()
    {
        
       
        
        foreach ( GameObject e in estrelas)
        {
            e.SetActive(false);
        }


        int nEstelas = 0;

        if (notaFinal == 10)
        {
            nEstelas = 3;
        }
        else if (notaFinal >= notaMinima2Estrelas)
        {
            nEstelas = 2;
        }
        else if (notaFinal >= notaMinima1Estrela)
        {
            nEstelas = 1;
        }
        
        
        for (int i = 0; i < nEstelas; i++)
        {
            estrelas[i].SetActive(true);
        }


    }
    
    
}
