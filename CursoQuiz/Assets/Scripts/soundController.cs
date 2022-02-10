using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour
{
    // Start is called before the first frame update


    private AudioSource AudioS;
    public AudioClip somAcerto,somErro,somAcertoForca;
    public List<AudioClip> fases;
    public List<AudioClip> fasesLucas;
    public  List<AudioClip> fasesMAteus;

    public static soundController Instance { get; set; }

    // fase1,fase2,fase3,fase4,fase5,fase6

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


         

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        AudioS = GetComponent<AudioSource>();
    }



    public void playAcertoForca()
    {

        if (GameManager.Instance.comSOm)
        {
            AudioS.PlayOneShot(somAcertoForca);

        }
    }
    public void playAcerto()
    {
        if (GameManager.Instance.comSOm)
        {
            AudioS.PlayOneShot(somAcerto);
        }
    }


    public void playFase(string Estudo,string fase)
    {
        AudioS.Stop();

        if (GameManager.Instance.comSOm)
        {

            if (Estudo.Equals("Jesus"))
            {

                    AudioS.PlayOneShot(fasesLucas[int.Parse(fase) - 1]);

                

            }
            else if (Estudo.Equals("Mateus"))
            {
                AudioS.PlayOneShot(fasesMAteus[int.Parse(fase) - 1]);

            }
            else
            {
                AudioS.PlayOneShot(fases[1]);
            }

        }
    }


    public bool isPlayng()
    {
        return AudioS.isPlaying;

    }

    public void pararSom()
    {
        AudioS.Stop();

    }

    public void retornarSom()
    {
        AudioS.Stop();

        if (GameManager.Instance.comSOm)
        {
            AudioS.Play(0);
        }
    }
    public void playErro()
    {
        if (GameManager.Instance.comSOm)
        {
            AudioS.PlayOneShot(somErro);
        }
    }

   
}
