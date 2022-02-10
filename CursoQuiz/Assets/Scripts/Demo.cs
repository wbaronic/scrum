﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Globalization;
using UnityEngine.SceneManagement;

public static class ButtonExtension
{
	public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener(delegate () {
			OnClick(param);
		});
	}
}


public enum Temas
{
	Lucas,
	Genesis,
	Jose,
	Moises
}

public class Demo : MonoBehaviour
{





	[Serializable]
	public struct Game
	{

		public int id;
		public string Name;
		public string Description;
		public Sprite Icon;
		int[] allGames;
	}

	[SerializeField] Game[] allGames;

	void Start()
	{


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
			g.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].Icon;
			g.transform.GetChild(1).GetComponent<Text>().text = allGames[i].Name;
			g.transform.GetChild(2).GetComponent<Text>().text = allGames[i].Description;

            /*g.GetComponent <Button> ().onClick.AddListener (delegate() {
				ItemClicked (i);
			});*/

            if (i+1 != N && i!=0)
            {
				g.GetComponent<Button>().AddEventListener(i, ItemClicked);

			}else if (i == 0)
            {

				g.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("estudoEscolhido", "Jogar") + "\n"  + PlayerPrefs.GetString("nomeTema", "");
				;

				g.GetComponent<Button>().AddEventListener(i, jogarFase);

			}


			else
            {
				g.GetComponent<Button>().AddEventListener(i, abrirZap);

			}







		}







		Destroy(buttonTemplate);
	}

	void jogarFase(int item)
	{



		SceneManager.LoadScene("introducaoFase");
	}

	void abrirZap(int item)
    {
		Application.OpenURL("https://wa.me/5512991200575?text=Eu%20tenho%20interesse%20em%20cadastrar%20perguntas");
		
}
	void ItemClicked(int itemIndex)
	{

		Debug.Log("------------item " + itemIndex + " clicked---------------");
		Debug.Log("name " + allGames[itemIndex].Name);
		Debug.Log("desc " + allGames[itemIndex].Description);



		string nome = RemoveSpecialCharacters(allGames[itemIndex].Name, false);

		Debug.Log("O nome é " + nome);


		registrarEscolhadeConteudoNofirebase(nome);

		selecinarEstudo(nome);

	}



	public void selecinarEstudo(string id)
	{



		//   PlayGames.platform.Events.IncrementEvent(GPGSIds.event_voc_ganhou_pontos_para_se_tornar_um__dos_maiores_mestres_da_bblia, 1);

		// PlayerPrefs.SetInt("idTema", idTema);
		GameManager.Instance.estudoEscolhido = id;



		GameManager.Instance.carregarTemaEscolhido();




	}

	public void registrarEscolhadeConteudoNofirebase(string nome)
	{
		Firebase.Analytics.FirebaseAnalytics.LogEvent(
Firebase.Analytics.FirebaseAnalytics.EventSelectContent,
new Firebase.Analytics.Parameter(
  Firebase.Analytics.FirebaseAnalytics.ParameterItemName, nome)
);
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


}