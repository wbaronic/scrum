using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Usuario
{

    public string userName;
    public string email;


    public Usuario(string userName, string email)
    {
        this.userName = userName;
        this.email = email;
    }

}
public class FBcontroller : MonoBehaviour
{



    public InputField txtId;
    public InputField txtNome;
    public InputField txtEmail;

    public InputField respostaA;
    public InputField respostaB;
    public InputField respostaC;



    // Start is called before the first frame update
    DatabaseReference dataBaseRefence;

    public string recebeNOme;
    public string recebeEmail;
    private bool ismostra = false;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://aventuras-da-bblia-jp-96278862-default-rtdb.firebaseio.com/");

        // dataBaseRefence = FirebaseDatabase.DefaultInstance.RootReference;

        InicializarBD();
    }


    void InicializarBD()
    {

        print("inicandoBD");
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.

                dataBaseRefence = FirebaseDatabase.DefaultInstance.RootReference;

                print("conectando ao bd");

                //app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {

                print("nao conectou");
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });


    }


    // Update is called once per frame
    void Update()
    {
        if (ismostra)
        {
            mostrarDados();
            ismostra = false;
        }
    }

    private void mostrarDados()
    {

        txtNome.text = recebeNOme;
        txtEmail.text = recebeEmail;


    }

    public void excluirRegistro()
    {


        dataBaseRefence.Child("usuarios").Child(txtId.text.Trim()).RemoveValueAsync();
        Debug.Log("Registro Exluído : " + txtId.text.Trim());
    }




    public void Consultrar()
    {
        mostrarRegistro(txtId.text.Trim());
    }

    void mostrarRegistro(string userId)
    {

        FirebaseDatabase.DefaultInstance
  .GetReference("usuarios")
  .GetValueAsync().ContinueWith(task => {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...

          string json = snapshot.Child(userId).GetRawJsonValue();
          Usuario usuario = JsonUtility.FromJson<Usuario>(json);


          recebeNOme = usuario.userName;
          recebeEmail = usuario.email;
          ismostra = true;

      }
  });
    }


    public void mostrarTodosRegistro()
    {

        FirebaseDatabase.DefaultInstance
  .GetReference("usuarios")
  .GetValueAsync().ContinueWith(task => {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...

          foreach (var filhos in snapshot.Children)
          {

              string f = filhos.GetRawJsonValue();

              Usuario usuario = JsonUtility.FromJson<Usuario>(f);

              Debug.Log(usuario.userName);
              Debug.Log(usuario.email);


          }





      }
  });
    }

    void  numeroNovoRegistro(string temaId, String nome, String email)
    {

        int novoId = 0;


        FirebaseDatabase.DefaultInstance
  .GetReference("Perguntas").Child(temaId).OrderByKey().LimitToLast(1)
  .GetValueAsync().ContinueWith(task => {
      if (task.IsFaulted)
      {

          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...


          string key = snapshot.Key;
          print("Key" + key);

          Int32.TryParse(key, out novoId);

          // int novoId =  Inte snapshot.Key;
          novoId = novoId + 1;

          print("NovoId" + novoId);


         // gravarDados(temaId,nome,email,novoId);

      }
  });

        print("retornando " + novoId);



    }

    public void gravarTema()
    {

        if (this.txtNome.text.Equals(""))
        {
            Debug.Log("digite o nome");
            return;
        }

        if (this.txtId.text.Equals(""))
        {
            Debug.Log("digite o ID");
            return;
        }


        gravarTema(txtId.text.Trim(), txtNome.text.Trim(), txtEmail.text.Trim(), respostaA.text.Trim(), respostaB.text.Trim(), respostaC.text.Trim());
    }

    public void gravarTema(string idTema, String nome, String linkAula, string a, string b, string c)
    {






        Tema  pergunta = new Tema(idTema, nome,linkAula, a, b, c);
        string json = JsonUtility.ToJson(pergunta);



        dataBaseRefence.Child("temas").Child(idTema).SetRawJsonValueAsync(json);



        //  dataBaseRefence.Child("usuarios").Child(id).Child("").SetRawJsonValueAsync(json);
        Debug.Log("gravando");

        txtId.text = ""; ; txtNome.text = ""; txtEmail.text = "";
        respostaA.text = ""; respostaB.text = ""; respostaC.text = "";

    }


    public void gravar()
    {

        if (this.txtNome.text.Equals(""))
        {
            Debug.Log("digite o nome");
            return;
        }

        if (this.txtId.text.Equals(""))
        {
            Debug.Log("digite o ID");
            return;
        }


        gravarDados(txtId.text.Trim(), txtNome.text.Trim(), txtEmail.text.Trim(), respostaA.text.Trim(), respostaB.text.Trim(), respostaC.text.Trim());
    }

    public void gravarDados(string idTema, String nome, String email,string a,string b, string c)
    {



        


        Pergunta pergunta = new Pergunta(nome, email,a,b,c);
        string json = JsonUtility.ToJson(pergunta);



        dataBaseRefence.Child("Perguntas").Child(idTema).Push().SetRawJsonValueAsync(json);



        //  dataBaseRefence.Child("usuarios").Child(id).Child("").SetRawJsonValueAsync(json);
        Debug.Log("gravando");

        txtId.text = ""; ; txtNome.text=""; txtEmail.text = "";
        respostaA.text = ""; respostaB.text = ""; respostaC.text = "";

    }



    public void gravarUsuario()
    {


        SceneManager.LoadScene("titulo");



    //    if (this.txtNome.text.Equals(""))
    //    {
    //        Debug.Log("digite o nome");
    //        return;
    //    }

    //    gravarDadosUsuario(txtId.text.Trim(), txtNome.text.Trim(), txtEmail.text.Trim());
    }


    //only send bool, string, long, double, IDictionary, and List<Object> to Value.
    public void gravarDadosUsuario(string id, String nome, String email)
    {


        Usuario usuario = new Usuario(nome, email);
        string json = JsonUtility.ToJson(usuario);
 

        dataBaseRefence.Child("usuarios").Push().SetRawJsonValueAsync(json);
        Debug.Log("gravando");

    }
}
