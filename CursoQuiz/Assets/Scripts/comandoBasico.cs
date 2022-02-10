using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class comandoBasico : MonoBehaviour
{
    // Start is called before the first frame update
 

    public void irPAraCena()
    {

       SceneManager.LoadScene("titulo");

    }
}
