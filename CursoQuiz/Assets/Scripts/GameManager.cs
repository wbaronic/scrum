using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using System;


public class Estudo
{
    public string id;
    public string nome;

}


public class Pergunta
{
    public string pergunta;
    public string correta;
    public string a;
    public string b;
    public string c;
    public string dica;
    public int tema;

    public Pergunta()
    {
    }


    public Pergunta(string pergunta, string correta, string a, string b, string c)
    {
        this.pergunta = pergunta;
        this.correta = correta;
        this.a = a;
        this.b = b;
        this.c = c;

    }

    public Pergunta(string pergunta, string correta, string a, string b, string c, string dica)
    {
        this.pergunta = pergunta;
        this.correta = correta;
        this.a = a;
        this.b = b;
        this.c = c;
        this.dica = dica;

    }

    //Fases:		Quest�es Objetivas, M�ltipla escolha, Verdadeiro ou falso e Complete:		Respostas:		Alternativa A		Alternativa B		Alternativa C		Vers�culos:
    public Pergunta(int fases, string pergunta ,string correta ,string a, string b, string c, string dica)
    {
        this.tema = fases;
        this.pergunta = pergunta;
        this.correta = correta;
        this.a = a;
        this.b = b;
        this.c = c;
        this.dica = dica;

    }
}



public class Tema
{
    public string id;
    public string nome;
    public string linkAula;
    public string mensagemInicial;
    public string achiviments;
    public string denarios;

    public Tema(string id, string nome, string mensagemInicial, string linkAula, string UnlockAchievement, string denariosNecessario)
    {

        this.id = id;
        this.nome = nome;
        this.linkAula = linkAula;
        this.mensagemInicial = mensagemInicial;
        this.achiviments = UnlockAchievement;
        this.denarios = denariosNecessario;
    }


}







public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] bdPerguntas;
    public string[] correta;
    public string[] alternativasA;
    public string[] alternativasB;
    public string[] alternativasC;
    public string[] alternativasD;
    public bool viuMensagemDica;
    public bool viumensagemRevelacao;
    public int qtdCorretasnafase { get; set; }
    public int ultimafase { get; set; }
    public string estudoEscolhido { get; set; }

    public bool comSOm;

    public List<Pergunta> perguntasGerais = new List<Pergunta>();
    public List<Tema> temas = new List<Tema>();

    DatabaseReference dataBaseRefence;
    public float tempo;
    internal int idResponder;
    public  List<String> estudos;
    public bool showAds;

    public static GameManager Instance { get; set; }
    public float valorQuestao { get; internal set; }

    void Start()
    {


        comSOm = true;

        // NewMethod();
        //  mostrarTodosTemas();
        estudoEscolhido = PlayerPrefs.GetString("estudoEscolhido", "Jose");
        ultimafase = PlayerPrefs.GetInt("ultimafase", 1);


        

        pegarEstudosNoFirebase("pt");

        showAds = (PlayerPrefs.GetInt("ShowAds", 1) == 1);        // mostrarTodosRegistro();


        if (estudoEscolhido.Equals("Jesus"))
        {
            carregarTemasOffLine();
            tema1();
            tema2();
            tema3();
            tema4();
            tema5();
            tema6();
            tema7_Lucas2_1_7();

        }
        else if (estudoEscolhido.Equals("Mateus"))
        {
            carregarTemasMateus();

            Mateus();
        }
        else
        {

            pegarTemasNoFirebase("pt", estudoEscolhido);
            pegarPerguntasDaFaseNofirebase(ultimafase.ToString());
        }




       
        
        //  PlayGames.platform.Instance.Events.IncrementEvent("YOUR_EVENT_ID", 1);

    }


    public void alterarSom()
    {
        comSOm = !comSOm;

        if (comSOm)
        {



            soundController.Instance.retornarSom();

        }
        else
        {

            soundController.Instance.pararSom();
        }
    }

    public void DisableAds()
    {
         showAds = false;

        // Used to store that we shouldn't show ads
        PlayerPrefs.SetInt("ShowAds", 0);
        PlayerPrefs.SetInt("denarios", 500000);

    }


    public void tratarPerguntas()
    {
        List<Pergunta> perguntaTratada = new List<Pergunta>();






        foreach (Pergunta perguntaDotema in perguntasGerais)
        {

            perguntaDotema.pergunta = perguntaDotema.pergunta.Trim();
            perguntaDotema.correta = perguntaDotema.correta.Trim();
            perguntaDotema.a = perguntaDotema.a.Trim();
            perguntaDotema.b = perguntaDotema.b.Trim();
            perguntaDotema.c = perguntaDotema.c.Trim();
            perguntaDotema.dica = perguntaDotema.dica.Trim();


            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.ToUpper().Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.ToUpper().Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntaTratada.Add(perguntaDotema);

            }
        }


        perguntasGerais.Clear();
        perguntasGerais.AddRange(perguntaTratada);
    }

    public void Mateus()
    {

        Pergunta pergunta1 = new Pergunta(1, "	1- Livro da gera��o de Jesus Cristo, filho de Davi, filho de : ", "	Abra�o	", "		", "		", "		", "	1- Livro da gera��o de Jesus Cristo, filho de Davi, filho de Abra�o. Mateus 1:1	");
        Pergunta pergunta2 = new Pergunta(1, "	2- Quem foi o pai do rei Davi?	", "	C	", "	Salom�o	", "	Salmom	", "	Jess�	", "	E Jess� gerou ao rei Davi; e o rei Davi gerou a Salom�o da que foi mulher de Urias.CustoMateus 1:6	");
        Pergunta pergunta3 = new Pergunta(1, "	3- Jeconias gerou a Salatiel, Salatiel gerou a Zorobabel e Zorobabel gerou a ______________ .	", "	Abi�de	", "		", "		", "		", "	E Zorobabel gerou a Abi�de; e Abi�de gerou a Eliaquim; e Eliaquim gerou a Azor;CustoCustoMateus 1:13	");
        Pergunta pergunta4 = new Pergunta(1, "	4- Qual o nome do sogro de Maria?	", "	Jac�	", "		", "		", "		", "	E Jac� gerou a Jos�, marido de Maria, da qual nasceu JESUS, que se chama o Cristo.CustoCustoMateus 1:16	");
        Pergunta pergunta5 = new Pergunta(1, "	5- Quantas gera��es s�o de Abra�o at� Davi?	", "	Quatorze	", "		", "		", "		", "	De sorte que todas as gera��es, desde Abra�o at� Davi, s�o catorze gera��es; e desde Davi at� a deporta��o para a babil�nia, catorze gera��es; e desde a deporta��o para a babil�nia at� Cristo, catorze gera��es.CustoCustoMateus 1:17	");
        Pergunta pergunta6 = new Pergunta(1, "	6- Quem foi a m�e de Jesus Cristo?	", "	Maria	", "		", "		", "		", "	Ora, o nascimento de Jesus Cristo foi assim: Que estando Maria, sua m�e, desposada com Jos�, antes de se ajuntarem, achou-se ter concebido do Esp�rito Santo.CustoCustoMateus 1:18	");
        Pergunta pergunta7 = new Pergunta(1, "	7- Qual � a tradu��o de EMANUEL?	", "	B	", "	Deus com voc�	", "	Deus conosco	", "	For�a	", "	23 Eis que a virgem conceber�, e dar� � luz um filho, e chamar�o seu nome Emanuel, que traduzido �: Deus conosco. Custo|fn:  Ref. - Isa�as 7:14	");
        Pergunta pergunta8 = new Pergunta(1, "	8- Jesus nasceu em:	", "	C	", "	Jord�nia	", "	Galileia	", "	Bel�m da Judeia	", "	1 E sendo Jesus j� nascido em Bel�m da Judeia, nos dias do rei Herodes, eis que vieram uns magos do oriente a Jerusal�m, Custo	");
        Pergunta pergunta9 = new Pergunta(1, "	9- Quem foi o rei do oriente no nascimento de Jesus Cristo?	", "	Herodes	", "		", "		", "		", "	1 E sendo Jesus j� nascido em Bel�m da Judeia, nos dias do rei Herodes, eis que vieram uns magos do oriente a Jerusal�m, Custo	");
        Pergunta pergunta10 = new Pergunta(1, "	10- Quais foram as especiarias que os Reis Magos levaram para Jesus Cristo?	", "	A	", "	Ouro, incenso e mirra	", "	Alum�nio, canela e cobre	", "	Prata, cevada e alo�	", "	Mt 2 11 E entrando na casa, acharam o menino com sua m�e Maria, e prostrando-se o adoraram. E abrindo seus tesouros, ofereceram-lhe presentes: ouro, incenso, e mirra. Custo	");
        Pergunta pergunta11 = new Pergunta(1, "	11- Jos� e sua fam�lia foram para ______________ depois da fuga para o Egito e morte de Herodes.	", "	Nazar�	", "		", "		", "		", "	Mt 2-23 E veio a habitar na cidade chamada Nazar�, para que se cumprisse o que foi dito pelos profetas, que: Ele ser� chamado de Nazareno.Custo	");
        Pergunta pergunta12 = new Pergunta(1, "	12- O que disse Jo�o Batista no deserto Judeia?	", "	A	", "	'Arrependei-vos, porque � chegado o reino dos c�us.''	", "	 Quem tem ouvidos para ouvir ou�a.	", "	Porque o meu jugo � suave, e o meu fardo � leve."	, "	Mt 3-2 E dizendo: Arrependei-vos, porque perto est� o Reino dos c�us. Custo	");
        Pergunta pergunta13 = new Pergunta(1, "	13- Em que rio Jesus Cristo foi batizado?	", "	Jord�o	", "		", "		", "		", "	Mt 3 13 Ent�o Jesus veio da Galileia ao Jord�o at� Jo�o para ser por ele batizado. Custo	");
        Pergunta pergunta14 = new Pergunta(1, "	14- Jesus foi batizado por:	", "	A	", "	Jo�o Batista	", "	Tiago	", "	Samuel	", "	Mt 3-13 Ent�o Jesus veio da Galileia ao Jord�o at� Jo�o para ser por ele batizado. Custo	");
        Pergunta pergunta15 = new Pergunta(1, "	15- E disse-lhe : Se tu �s o _________ de Deus, lan�a-te daqui abaixo;[...]	", "	Filho	", "		", "		", "		", "	Mt 4-6 E disse-lhe: Se tu �s o Filho de Deus, lan�a-te abaixo, porque est� escrito que: Mandar� a seus anjos acerca de ti, e te tomar�o pelas m�os, para que nunca com teu p� tropeces em pedra alguma. Custo	");
        Pergunta pergunta16 = new Pergunta(1, "	16- Por quem Jesus voltou para a Galileia?	", "	Jo�o	", "		", "		", "		", "	Mt 4-12 Mas quando Jesus ouviu que Jo�o estava preso, voltou para a Galileia. Custo	");
        Pergunta pergunta17 = new Pergunta(1, "	17- O que faziam os primeiros disc�pulos de Jesus quando Ele os chamou?	", "	B	", "	Dormindo	", "	Pescando	", "	Pastoreando	", "	Mt 4-18 Enquanto Jesus andava junto ao mar da Galileia, viu dois irm�os: Sim�o, chamado Pedro, e seu irm�o Andr�, lan�arem a rede ao mar, porque eram pescadores. Custo	");
        Pergunta pergunta18 = new Pergunta(1, "	18- Tiago era filho de _________ .	", "	Zebedeu	", "		", "		", "		", "	Mt 4-21 E passando dali, viu outros dois irm�os: Tiago, [filho] de Zebedeu, e seu irm�o Jo�o, em um barco, com seu pai Zebedeu, que estavam consertando suas redes; e ele os chamou. Custo	");
        Pergunta pergunta19 = new Pergunta(1, "	19- Qual era o nome do irm�o de Tiago, filho de Zebedeu?	", "	B	", "	Marcos	", "	Jo�o	", "	Samuel	", "	Mt 4-21 E passando dali, viu outros dois irm�os: Tiago, [filho] de Zebedeu, e seu irm�o Jo�o, em um barco, com seu pai Zebedeu, que estavam consertando suas redes; e ele os chamou. Custo	");
        Pergunta pergunta20 = new Pergunta(1, "	20- Jesus percorria Galileia ensinando nas _______________ .	", "	Sinagogas	", "		", "		", "		", "	Mt 4-23 E Jesus rodeava toda a Galileia, ensinando em suas sinagogas, pregando o Evangelho do Reino, e curando toda enfermidade e toda doen�a no povo. Custo	");
        Pergunta pergunta22 = new Pergunta(2, "	1- Bem-aventurados os pobres de esp�rito, porque eles herdar�o a terra.	", "	F 	", "		", "		", "		", "	Mt 5 3 Benditos s�o os humildes de esp�rito, porque deles � o Reino dos c�us. Custo	");
        Pergunta pergunta23 = new Pergunta(2, "	2- Bem-aventurados os que t�m fome e sede de justi�a, porque eles ser�o fartos.	", "	V	", "		", "		", "		", "	Mt 5-6 Benditos s�o os que t�m fome e sede de justi�a, porque eles ser�o saciados. Custo	");
        Pergunta pergunta24 = new Pergunta(2, "	3- Bem-aventurados os que sofrem persegui��o  por causa da justi�a, porque deles � o Reino dos c�us.	", "	V	", "		", "		", "		", "	Mt 5 10 Benditos s�o os que sofrem persegui��o por causa da justi�a, porque deles � o Reino dos c�us. Custo	");
        Pergunta pergunta25 = new Pergunta(2, "	4- V�s sois o _________ da terra [...]	", "	sal	", "		", "		", "		", "	Mt 5-13 V�s sois o sal da terra; mas se o sal perder seu sabor, com que se salgar�? Para nada mais presta, a n�o ser para se lan�ar fora, e ser pisado pelas pessoas. Custo	");
        Pergunta pergunta26 = new Pergunta(2, "	5- V�s sois a _______ do mundo [...] 	", "	luz	", "		", "		", "		", "	Mt 5-14 V�s sois a luz do mundo; n�o se pode esconder uma cidade fundada sobre o monte; Custo	");
        Pergunta pergunta27 = new Pergunta(2, "	6- N�o se pode esconder uma cidade edificada sobre um:	", "	C	", "	Muro	", "	Morro	", "	Monte	", "	Mt 5-14 V�s sois a luz do mundo; n�o se pode esconder uma cidade fundada sobre o monte; Custo	");
        Pergunta pergunta28 = new Pergunta(2, "	7- Mas buscai primeiro o Reino de Deus, e a sua ___________ , e todas essas coisas vos ser�o acrescentadas.	", "	justi�a	", "		", "		", "		", "	Mt 6-33 Mas buscai primeiramente o Reino de Deus e a sua justi�a, e todas estas coisas vos ser�o acrescentadas. Custo	");
        Pergunta pergunta29 = new Pergunta(2, "	8- N�o ____________ , para que n�o sejais julgados.	", "	julgueis	", "		", "		", "		", "	Mt 7-1 N�o julgueis, para que n�o sejais julgados. Custo	");
        Pergunta pergunta30 = new Pergunta(2, "	9- Entrai pela porta ___________ , porque ___________ � a porta, e ____________ , o caminho que conduz � perdi��o [...]	", "	A	", "	estreita / larga / espa�oso	", "	pequena / grande / largo	", "	fina / ampla / vasto	", "	Mt 7-13 Entrai pela porta estreita; porque larga � a porta, e espa�oso o caminho que leva � perdi��o; e muitos s�o os que por ela entram. Custo	");
        Pergunta pergunta31 = new Pergunta(2, "	10- Toda �rvore que n�o d� bom fruto _________ e lan�a-se no fogo.	", "	corta	", "		", "		", "		", "	Mt 7-19 Toda �rvore que n�o d� bom fruto � cortada e lan�ada ao fogo. Custo	");
        Pergunta pergunta32 = new Pergunta(2, "	11- Em que cidade Jesus estava com o apelo do centuri�o?	", "	Cafarnaum	", "		", "		", "		", "	Mt 8-5 Quando Jesus entrou em Cafarnaum, veio a ele um centuri�o, rogando-lhe, Custo	");
        Pergunta pergunta33 = new Pergunta(2, "	12-  Onde Jesus tocou na sogra de Pedro para cur�-la?	", "	M�o	", "		", "		", "		", "	Mt 8-15 Ele tocou a m�o dela, e a febre a deixou. Ent�o ela se levantou e come�ou a servi-los. Custo	");
        Pergunta pergunta34 = new Pergunta(2, "	13- Ele tomou sobre si as nossas ______________ e levou as nossas doen�as.	", "	enfermidades	", "		", "		", "		", "	Mt 8-17 Para que se cumprisse o que havia sido dito pelo profeta Isa�as, que disse: Ele tomou sobre si as nossas enfermidades, e levou as nossas doen�as. Custo	");
        Pergunta pergunta35 = new Pergunta(2, "	14- Segue-me e deixa aos mortos sepultar os seus ___________ .	", "	C	", "	orgulhos	", "	problemas	", "	mortos	", "	Mt 8-22 Por�m Jesus lhe disse: Segue-me, e deixa aos mortos enterrarem seus mortos. Custo	");
        Pergunta pergunta36 = new Pergunta(2, "	15- O que Jesus fazia no barco durante a grande tempestade no mar?	", "	Dormia	", "		", "		", "		", "	Mt 8-24 E eis que se levantou no mar uma tormenta t�o grande que o barco era coberto pelas ondas; por�m ele dormia. Custo	");
        Pergunta pergunta37 = new Pergunta(2, "	16- Quem os ventos e o mar, durante a tempestade no barco, obedeceram?	", "	Jesus	", "		", "		", "		", "	Mt 8-26 E ele lhes respondeu: Por que temeis, [homens] de pouca f�? Ent�o ele se levantou e repreendeu os ventos e o mar. E houve grande calmaria. Custo	");
        Pergunta pergunta38 = new Pergunta(2, "	17-  Quantos endemoniados Jesus encontrou em Gadareno?	", "	Dois 	", "		", "		", "		", "	Mt 8-28 E quando chegou � outra margem, � terra dos gergesenos, vieram-lhe ao encontro dois endemoninhados que tinham sa�do dos sepulcros. Eles eram t�o ferozes que ningu�m podia passar por aquele caminho. Custo	");
        Pergunta pergunta39 = new Pergunta(2, "	18- Para onde os dem�nios expulsados por Jesus em Gadareno sugeriram ir?	", "	B	", "	Povo gadareno	", "	Manada de porcos	", "	Rebanho de ovelhas	", "	Mt 8-31 E os dem�nios rogaram-lhe, dizendo: Se nos expulsares, permite-nos entrar naquela manada de porcos. Custo	");
        Pergunta pergunta40 = new Pergunta(2, "	19- O que aconteceu com a manada de porcos em que os dem�nios entraram em Gadareno?	", "	Suicidaram	", "		", "		", "		", "	Mt 8-32 E ele lhes disse: Ide. Ent�o eles sa�ram, e entraram na manada de porcos; e eis que toda aquela manada de porcos se lan�ou de um precip�cio ao mar, e morreram nas �guas. Custo	");
        Pergunta pergunta41 = new Pergunta(2, "	20- E eis que toda aquela cidade saiu ao encontro de Jesus, e, vendo-o, rogaram-lhe que se ___________ do seu territ�rio.	", "	retirasse	", "		", "		", "		", "	Mt 8-34 E eis que toda aquela cidade saiu ao encontro de Jesus; e quando o viram, rogaram-lhe que se retirasse do territ�rio deles.Custo	");
        Pergunta pergunta43 = new Pergunta(3, "	1- E eis que lhe trouxeram um paral�tico deitado ________________ .	", "	A	", "	numa cama	", "	num sof�	", "	numa poltrona	", "	Mt 9-1 Ent�o ele entrou no barco, passou para a outra margem, e veio � sua pr�pria cidade. Custo	");
        Pergunta pergunta44 = new Pergunta(3, "	2- O que Jesus disse ao paral�tico de Cafarnaum ap�s cur�-lo?	", "	B	", "	Levanta-te e segue-me	", "	Levanta-te, toma a tua cama e vai para tua casa	", "	Segue teu caminho	", "	Mt 9-6 Ora, para que saibais que o Filho do homem tem autoridade na terra para perdoar pecados, (Ele, ent�o, disse ao paral�tico): Levanta-te, toma o teu leito, e vai para tua casa. Custo	");
        Pergunta pergunta45 = new Pergunta(3, "	3- Porque eu n�o vim para chamar __________ , mas _____________ , ao arrependimento.	", "	A	", "	os justos / os pecadores	", "	os limpos / os sujos	", "	os bons / os maus	", "	Mt 9-13 Mas ide aprender o que significa: �Quero miseric�rdia, e n�o sacrif�cio�. Porque eu n�o vim chamar os justos, mas sim os pecadores, ao arrependimento. Custo	");
        Pergunta pergunta46 = new Pergunta(3, "	4- [...]Dias, por�m, vir�o em que lhes ser� tirado o esposo, e ent�o descansar�o.	", "	F	", "		", "		", "		", "	Mt 9-15 E Jesus lhes respondeu: Podem, por acaso, os convidados do casamento andar tristes enquanto o noivo est� com eles? Mas dias vir�o, quando o noivo lhes for tirado, e ent�o jejuar�o. Custo	");
        Pergunta pergunta47 = new Pergunta(3, "	5- Quantos anos a mulher do fluxo de sangue ficou doente?	", "	C	", "	20 anos	", "	17 anos	", "	12 anos	", "	Mt 9-20 (Eis, por�m, que uma mulher enferma de um fluxo de sangue havia doze anos veio por detr�s [dele] , e tocou a borda de sua roupa; Custo	");
        Pergunta pergunta48 = new Pergunta(3, "	6- O que fez a mulher do fluxo de sangue para ser curada?	", "	B	", "	Chorou	", "	Tocou a orla do seu vestido 	", "	Clamou pelo seu Nome	", "	Mt 9-20 (Eis, por�m, que uma mulher enferma de um fluxo de sangue havia doze anos veio por detr�s [dele] , e tocou a borda de sua roupa; Custo	");
        Pergunta pergunta49 = new Pergunta(3, "	7- Disse-lhes: Retirai-vos, que a menina n�o est� morta, mas dorme.	", "	V	", "		", "		", "		", "	Mt 9-24 E disse-lhes: Retirai-vos, porque a menina n�o est� morta, mas sim dormindo. E riram dele. Custo|fn:  N4 omite lhes	");
        Pergunta pergunta50 = new Pergunta(3, "	8- Mas os cananeus diziam: Ele expulsa os dem�nios pelo pr�ncipe dos dem�nios.	", "	F	", "		", "		", "		", "	Mt 9-34 Mas os fariseus diziam: � pelo chefe dos dem�nios que ele expulsa os dem�nios. Custo	");
        Pergunta pergunta51 = new Pergunta(3, "	9- Ent�o, disse aos seus disc�pulos: A seara � realmente grande, mas poucos s�o os  ________.	", "	ceifeiros	", "		", "		", "		", "	Mt 9-37 Ent�o disse aos seus disc�pulos: Em verdade a colheita � grande, por�m os trabalhadores s�o poucos. Custo	");
        Pergunta pergunta52 = new Pergunta(3, "	10- Qual dos discipulos de Jesus era apelidade de Tadeu?	", "	Lebeu	", "		", "		", "		", "	Mt 10-3 Filipe e Bartolomeu; Tom�, e Mateus o coletor de impostos; Tiago, [filho] de Alfeu; e Lebeu, por sobrenome Tadeu; Custo	");
        Pergunta pergunta53 = new Pergunta(3, "	11- Quais foram as cidades destru�das por suas desobedi�ncias?	", "	C	", "	Galileia e N�nive	", "	Jerusal�m e Bel�m	", "	Sodoma e Gomorra	", "	Mt 10-15 Em verdade vos digo que no dia do julgamento mais toler�vel ser� para a regi�o de Sodoma e Gomorra do que para aquela cidade. Custo	");
        Pergunta pergunta54 = new Pergunta(3, "	12- E odiados de todos seres por causa do meu nome; mas aquele que perseverar at� ao fim ser� morto.	", "	F	", "		", "		", "		", "	Mt 10-22 E sereis odiados por todos por causa de meu nome; mas aquele que perseverar at� o fim, esse ser� salvo. Custo	");
        Pergunta pergunta55 = new Pergunta(3, "	13- E at� mesmo os cabelos da vossa cabe�a est�o todos contados.	", "	V	", "		", "		", "		", "	Mt 10-30 E at� os cabelos de vossas cabe�as est�o todos contados. Custo	");
        Pergunta pergunta56 = new Pergunta(3, "	14- E assim, os inimigos do homem ser�o os seus vizinhos.	", "	F	", "		", "		", "		", "	Mt 10-36 E os inimigos do homem ser�o os de sua pr�pria casa�. Custo	");
        Pergunta pergunta57 = new Pergunta(3, "	15- Quem ama ________ ou __________ mais do que a mim n�o � digno de mim [...]	", "	A	", "	o pai / a m�e	", "	o tia / a tia	", "	o sobrinho / a sobrinha	", "	Mt 10-37 Quem ama pai ou m�e mais que a mim n�o � digno de mim; e quem ama filho ou filha mais que a mim n�o � digno de mim; Custo	");
        Pergunta pergunta58 = new Pergunta(3, "	16- E quem n�o toma a sua _________ e n�o segue ap�s mim n�o � digno de mim.	", "	cruz	", "		", "		", "		", "	Mt 10-38 E quem n�o toma sua cruz e segue ap�s mim n�o � digno de mim. Custo	");
        Pergunta pergunta59 = new Pergunta(3, "	17- Quem achar a sua vida ___________ e quem perder a sua vida por amor a mim ______________ .	", "	B	", "	troc�-la-� / ganh�-la-�	", "	perd�-la-�  / ach�-la-�	", "	afundar-se-� / arrecadar-se-�	", "	Mt 10-39 Quem achar sua vida a perder�; e quem, por causa de mim, perder sua vida, a achar�. Custo	");
        Pergunta pergunta60 = new Pergunta(3, "	18 - [...] Em verdade vos digo que de modo algum perder� seu galard�o.	", "	V	", "		", "		", "		", "	Mt 10-42 E qualquer um que der ainda que somente um copo de [�gua] fria a um destes pequenos por reconhec�-lo como disc�pulo, em verdade vos digo que de maneira nenhuma perder� sua recompensa.Custo	");
        Pergunta pergunta61 = new Pergunta(3, "	19- E bem-aventurado � aquele que se n�o _________________ em mim.	", "	escandalizar	", "		", "		", "		", "	Mt 11-6 E bendito � aquele que n�o deixar de crer em mim. Custo	");
        Pergunta pergunta62 = new Pergunta(3, "	20- Em verdade vos digo que, entre os que de mulher tem nascido, n�o apareceu algu�m maior do que...	", "	C	", "	Davi	", "	Jesus Cristo	", "	Jo�o Batista	", "	Mt 11-11 Em verdade vos digo que, dentre os nascidos de mulheres, n�o se levantou [outro] maior que Jo�o Batista; por�m o menor no Reino dos c�us � maior que ele. Custo	");
        Pergunta pergunta63 = new Pergunta(3, "	21- Porque todos os _____________ e a lei profetizaram at� Jo�o.	", "	profetas	", "		", "		", "		", "	Mt 11-13 Porque todos os profetas e a Lei profetizaram at� Jo�o. Custo	");
        Pergunta pergunta64 = new Pergunta(3, "	22- E, se quereis dar cr�dito, � este o ____________ que havia de vir.	", "	Elias	", "		", "		", "		", "	Mt 11-14 E se estais dispostos a aceitar, este � o Elias que havia de vir. Custo	");
        Pergunta pergunta65 = new Pergunta(3, "	23- Quem tem ouvidos para ouvir ____________ .	", "	ou�a	", "		", "		", "		", "	Mt 11-15 Quem tem ouvidos para ouvir, ou�a. Custo	");
        Pergunta pergunta66 = new Pergunta(3, "	24- Quais s�o as tr�s cidades impenitentes?	", "	A	", "	Corazim, Betsaida, Cafarnaum	", "	Bel�m, Jerusal�m, Damasco	", "	Gaza, Atenas, Gr�cia 	", "	Mt 11 21 Ai de ti Corazim! Ai de ti Betsaida! Porque se em Tiro e em S�don tivessem sido feitos os milagres que em v�s foram feitos, h� muito tempo teriam se arrependido com saco e com cinza! Custo22 Por�m eu vos digo que mais toler�vel ser� para Tiro e S�don, no dia do ju�zo, que para v�s. Custo23 E tu, Cafarnaum, que est�s exaltada at� o c�u, ao mundo dos mortos ser�s derrubada! Pois se em Sodoma tivessem sido feitos os milagres que foram feitos em ti, ela teria permanecido at� hoje. 	");
        Pergunta pergunta67 = new Pergunta(3, "	25- Vinde a mim, todos os que estais cansados e oprimidos, e eu vos aliviarei.	", "	V	", "		", "		", "		", "	Mt 11-28 Vinde a mim todos v�s que estais cansados e sobrecarregados, e eu vos farei descansar. Custo	");
        Pergunta pergunta68 = new Pergunta(3, "	26- Porque o meu jugo � suave, e o meu fardo � pesado.	", "	F	", "		", "		", "		", "	Mt 11-30 Pois o meu jugo � suave, e minha carga � leve.Custo	");
        Pergunta pergunta69 = new Pergunta(3, "	27- Naquele tempo, passou Jesus pelas searas, em um ____________ [...]	", "	C	", "	instante	", "	domingo	", "	s�bado	", "	Mt 12-1 Naquele tempo Jesus estava indo pelas planta��es de cereais no s�bado. Seus disc�pulos tinham fome, e come�aram a arrancar espigas e a comer. Custo	");
        Pergunta pergunta70 = new Pergunta(3, "	28- Porque o Filho do Homem at� do _____________ � Senhor.	", "	s�bado	", "		", "		", "		", "	Mt 12-8 Porque o Filho do homem � Senhor at� do s�bado. Custo	");
        Pergunta pergunta71 = new Pergunta(3, "	29- Quem foi curado no s�bado na sinagoga?	", "	B	", "	Um surdo	", "	Um homem com uma m�o mirrada	", "	Um paral�tico	", "	Mt 12-10 E eis que havia ali um homem que tinha uma m�o definhada; e eles, a fim de o acusarem, perguntaram-lhe: � l�cito curar nos s�bados? Custo	");
        Pergunta pergunta72 = new Pergunta(3, "	30- Quem n�o � comigo � contra mim; e quem comigo n�o ajunta espalha.	", "	V	", "		", "		", "		", "	Mt 12-30 Quem n�o � comigo � contra mim; e quem n�o ajunta comigo, espalha. Custo	");
        Pergunta pergunta73 = new Pergunta(3, "	31- Contra quem da Sant�ssima Trindade n�o lhe � perdoado a blasf�mia?	", "	C	", "	Pai	", "	Filho	", "	Esp�rito Santo	", "	Mt 12 31 Por isso eu vos digo: todo pecado e blasf�mia ser�o perdoados aos seres humanos; mas a blasf�mia contra o Esp�rito n�o ser� perdoada aos seres humanos. Custo|fn:  N4 omite aos seres humanos Custo32 E qualquer um que falar palavra contra o Filho do homem lhe ser� perdoado; mas qualquer um que falar contra o Esp�rito Santo, n�o lhe ser� perdoado, nem na era presente, nem na futura. 	");
        Pergunta pergunta74 = new Pergunta(3, "	32- Porque por tuas palavras ser�s justificado e por tuas palavras ser�s condenado.	", "	V	", "		", "		", "		", "	Mt 12-37 Porque por tuas palavras ser�s justificado, e por tuas palavras ser�s condenado. Custo|fn:  justificado � ou absolvido	");
        Pergunta pergunta75 = new Pergunta(3, "	33- Uma gera��o _______ e _________ pede um sinal [...]	", "	A	", "	m� / ad�ltera	", "	perversa / prom�scua	", "	desumana / cruel	", "	Mt 12-39 Mas ele lhes deu a seguinte resposta: Uma gera��o m� e ad�ltera pede sinal; mas n�o lhe ser� dado, exceto o sinal do profeta Jonas. Custo	");
        Pergunta pergunta76 = new Pergunta(3, "	34- Quanto tempo Jonas esteve no ventre da baleia?	", "	A	", "	Tr�s dias e tr�s noites	", "	Vinte dias e vinte noites	", "	Quarenta dias e quarenta noites	", "	Mt 12-40 Porque assim como Jonas esteve tr�s dias e tr�s noites no ventre da baleia, assim tamb�m o Filho do homem estar� tr�s dias e tr�s noites no cora��o da terra. Custo	");
        Pergunta pergunta77 = new Pergunta(3, "	35- Qual a cidade Jonas levava a palavra do Senhor?	", "	N�nive	", "		", "		", "		", "	Mt 12-41 Os de N�nive se levantar�o no Ju�zo com esta gera��o, e a condenar�o; porque se arrependeram com a prega��o de Jonas. E eis aqui quem � maior que Jonas. Custo	");
        Pergunta pergunta79 = new Pergunta(4, "	1- Quantas sementes tinham a par�bola do semeador?	", "	B	", "	Cinco	", "	Quatro	", "	Tr�s	", "	Mt 13-3 E ele lhes falou muitas coisas por par�bolas. Ele disse: Eis que o semeador saiu a semear. Custo4 E enquanto semeava, caiu parte [das sementes] junto ao caminho, e vieram as aves e a comeram. Custo5 E outra [parte] caiu entre pedras, onde n�o havia muita terra, e logo nasceu, porque n�o tinha terra funda. Custo6 Mas quando o sol surgiu, queimou-se; e por n�o ter raiz, secou-se. Custo7 E outra [parte] caiu entre espinhos, e os espinhos cresceram e a sufocaram. Custo8 E outra [parte] caiu em boa terra, e rendeu fruto: um a cem, outro a sessenta, e outro a trinta. 	");
        Pergunta pergunta80 = new Pergunta(4, "	2- Porque a v�s � dado conhecer os mist�rios do Reino dos c�us, mas a eles n�o lhes � dado; [...]	", "	V	", "		", "		", "		", "	Ele, respondendo, disse-lhes: Porque a v�s � dado conhecer os mist�rios do reino dos c�us, mas a eles n�o lhes � dado;CustoCustoMateus 13:11	");
        Pergunta pergunta81 = new Pergunta(4, "	3- Porque o cora��o deste povo est� amolecido, e ouviu de bom grado com seus ouvidos e fechou os olhos [...]	", "	F	", "		", "		", "		", "	Porque o cora��o deste povo est� endurecido, E ouviram de mau grado com seus ouvidos, E fecharam seus olhos; Para que n�o vejam com os olhos, E ou�am com os ouvidos, e compreendam com o cora��o, e se convertam, e eu os cure.CustoCustoMateus 13:15	");
        Pergunta pergunta82 = new Pergunta(4, "	4- Escutai v�s, pois, a _______________ do semeador.	", "	par�bola	", "		", "		", "		", "	Escutai v�s, pois, a par�bola do semeador.CustoCustoMateus 13:18	");
        Pergunta pergunta83 = new Pergunta(4, "	5- A par�bola do trigo e do joio diz para fazermos o que quando crescem juntos?	", "	A	", "	Deixai crescerem juntos at� � ceifa	", "	Deixai que se separem sozinhos	", "	Se livrar dos dois, pois o joio contaminou o trigo	", "	Deixai crescer ambos juntos at� � ceifa; e, por ocasi�o da ceifa, direi aos ceifeiros: Colhei primeiro o joio, e atai-o em molhos para o queimar; mas, o trigo, ajuntai-o no meu celeiro.CustoCustoMateus 13:30	");
        Pergunta pergunta84 = new Pergunta(4, "	6- Por qual meio/maneira que Jesus falava e ensinava a multid�o?	", "	Par�bolas	", "		", "		", "		", "	Tudo isto disse Jesus, por par�bolas � multid�o, e nada lhes falava sem par�bolas;CustoCustoMateus 13:34	");
        Pergunta pergunta85 = new Pergunta(4, "	7- De acordo com a genealogia terrena de Jesus, como se chamam seus irm�os?	", "	B	", "	Lucas, Davi, Pedro e Habacuque	", "	Tiago, Jos�, Sim�o e Judas	", "	Jos�, Tiago, Sim�o e Jo�o	", "	N�o � este o filho do carpinteiro? e n�o se chama sua m�e Maria, e seus irm�os Tiago, e Jos�, e Sim�o, e Judas?CustoCustoMateus 13:55	");
        Pergunta pergunta86 = new Pergunta(4, "	8- Quem foi instru�do a pedir pela morte de Jo�o Batista?	", "	A	", "	A filha de Herodias	", "	Herodias	", "	Felipe	", "	E ela, instru�da previamente por sua m�e, disse: D�-me aqui, num prato, a cabe�a de Jo�o o Batista.CustoCustoMateus 14:8	");
        Pergunta pergunta87 = new Pergunta(4, "	9- Como Jo�o Batista morreu?	", "	Degolado	", "		", "		", "		", "	E mandou degolar Jo�o no c�rcere.CustoCustoMateus 14:10	");
        Pergunta pergunta88 = new Pergunta(4, "	10- E mandou degolar Jo�o no sagu�o, [...]	", "	F	", "		", "		", "		", "	E mandou degolar Jo�o no c�rcere.CustoCustoMateus 14:10	");
        Pergunta pergunta89 = new Pergunta(4, "	11- Com quantos p�es e peixes Jesus fez a primeira multiplica��o dos mesmos?	", "	C	", "	Tr�s p�es e dois peixes	", "	Sete p�es e tr�s peixes	", "	Cinco p�es e dois peixes	", "	E, tendo mandado que a multid�o se assentasse sobre a erva, tomou os cinco p�es e os dois peixes, e, erguendo os olhos ao c�u, os aben�oou, e, partindo os p�es, deu-os aos disc�pulos, e os disc�pulos � multid�o.CustoCustoMateus 14:19	");
        Pergunta pergunta90 = new Pergunta(4, "	12- E comeram rodos e saciaram-se, e levantaram dos peda�os que sobejaram doze cestos cheios.	", "	V	", "		", "		", "		", "	E comeram todos, e saciaramse; e levantaram dos peda�os, que sobejaram, doze alcofas cheias.CustoCustoMateus 14:20	");
        Pergunta pergunta91 = new Pergunta(4, "	13- Que disc�pulo andou sob as �guas com Jesus?	", "	Pedro	", "		", "		", "		", "	E respondeu-lhe Pedro, e disse: Senhor, se �s tu, manda-me ir ter contigo por cima das �guas.CustoCustoMateus 14:28	");
        Pergunta pergunta92 = new Pergunta(4, "	14- Para onde Jesus e seus disc�pulos pretendiam ir quando o mesmo andou por cima do mar?	", "	B	", "	Gadara	", "	Genesar�	", "	Galileia	", "	E, tendo passado para o outro lado, chegaram � terra de Genesar�.CustoCustoMateus 14:34	");
        Pergunta pergunta93 = new Pergunta(4, "	15- Este povo honra-me com os seus l�bios, mas o seu cora��o est� longe de mim.	", "	V	", "		", "		", "		", "	Este povo se aproxima de mim com a sua boca e me honra com os seus l�bios, mas o seu cora��o est� longe de mim.CustoCustoMateus 15:8	");
        Pergunta pergunta94 = new Pergunta(4, "	16- Mas me adoram, ensinando doutrinas que s�o preceitos dos c�us.	", "	F	", "		", "		", "		", "	Mas, em v�o me adoram, ensinando doutrinas que s�o preceitos dos homens.CustoCustoMateus 15:9	");
        Pergunta pergunta95 = new Pergunta(4, "	17- O que contamina o homem n�o � o que entra na boa, mas o que sai da boca, isso � o que contamina o homem.	", "	V	", "		", "		", "		", "	O que contamina o homem n�o � o que entra na boca, mas o que sai da boca, isso � o que contamina o homem.CustoCustoMateus 15:11	");
        Pergunta pergunta96 = new Pergunta(4, "	18- [...] Toda planta que meu Pai celestial n�o plantou ser� arrancada.	", "	V	", "		", "		", "		", "	Ele, por�m, respondendo, disse: Toda a planta, que meu Pai celestial n�o plantou, ser� arrancada.CustoCustoMateus 15:13	");
        Pergunta pergunta97 = new Pergunta(4, "	19- Onde foi que Jesus encontrou a mulher cananeia com a filha endemoninhada?	", "	A	", "	Nas partes de Tiro e de Sidom	", "	Genesar�	", "	Ebom	", "	E, partindo Jesus dali, foi para as partes de Tiro e de Sidom.CustoCustoMateus 15:21	");
        Pergunta pergunta98 = new Pergunta(4, "	20- Onde foi a segunda multiplica��o dos p�es e peixes?	", "	C 	", "	Na borda do rio Jord�o	", "	Na margem do rio Nilo	", "	Ao p� do mar da Galileia	", "	Partindo Jesus dali, chegou ao p� do mar da Galil�ia, e, subindo a um monte, assentou-se l�.CustoCustoMateus 15:29	");
        Pergunta pergunta99 = new Pergunta(4, "	21- Quantos p�es e peixes Jesus multiplicou na sua segunda multiplica��o dos mesmos?	", "	B	", "	Dez p�es e muitos peixes	", "	Sete p�es e uns poucos peixinhos	", "	Nove p�es e poucos peixes	", "	E Jesus disse-lhes: Quantos p�es tendes? E eles disseram: Sete, e uns poucos de peixinhos.CustoCustoMateus 15:34	");
        Pergunta pergunta100 = new Pergunta(4, "	22- Quanto de p�es e peixes sobraram na segunda multiplica��o?	", "	B	", "	Tr�s cestos cheios de peda�os	", "	Sete cestos cheios de peda�os	", "	Cinco cestos cheios de peda�os	", "	E todos comeram e se saciaram; e levantaram, do que sobejou, sete cestos cheios de peda�os.CustoCustoMateus 15:37	");
        Pergunta pergunta101 = new Pergunta(4, "	23- Quantas pessoas a segunda multiplica��o dos p�es e peixes de Jesus Cristo alimentou?	", "	B	", "	Cinco mil homens, al�m de mulheres e crian�as	", "	Quatro mil homens, al�m de mulheres e crian�as	", "	Sete mil homens, al�m de mulheres e crian�as	", "	Mt 15 38 E foram os que comeram quatro mil homens, sem contar as mulheres e as crian�as. Custo	");
        Pergunta pergunta102 = new Pergunta(4, "	24- E, tendo despedido a multid�o, entrou no barco e dirigiu-se ao territ�rio de ________________ .	", "	Magdala	", "		", "		", "		", "	Mt 15-39 Depois de despedir as multid�es, [Jesus] entrou em um barco, e veio � regi�o de Magdala.Custo	");
        Pergunta pergunta103 = new Pergunta(4, "	25- E, chegando-se os filisteus e os saduceus para o tentarem, pediram-lhe que lhes mostrasse algum sinal do c�u.	", "	F	", "		", "		", "		", "	Mt 16 1 Ent�o os fariseus e os saduceus se aproximaram dele e, a fim de tent�-lo, pediram-lhe que lhes mostrasse algum sinal do c�u. Custo	");
        Pergunta pergunta104 = new Pergunta(4, "	26- Uma gera��o m� e ad�ltera pede um sinal, e nenhum sinal lhe ser� dado, sen�o o sinal do profeta ______________ .	", "	Jonas	", "		", "		", "		", "	Mt 16-4 Uma gera��o m� e ad�ltera pede um sinal; mas nenhum sinal lhe ser� dado, a n�o ser o sinal do profeta Jonas. Ent�o os deixou, e foi embora Custo	");
        Pergunta pergunta105 = new Pergunta(4, "	27- Ent�o, compreenderam que n�o dissera que se guardassem do _______________ do p�o, mas da _______________ dos fariseus.	", "	C	", "	trigo / ideologia	", "	farinha / disciplina	", "	fermento / doutrina	", "	Mt 16-12 Ent�o entenderam que ele n�o havia dito que tomassem cuidado com o fermento de p�o, mas sim com a doutrina dos fariseus e saduceus. Custo	");
        Pergunta pergunta106 = new Pergunta(4, "	28- E, chegando Jesus �s partes de _______________ de Felipe, interrogou os seus disc�pulos, dizendo: Quem dizem os homens ser o Filho do Homem?	", "	Cesareia	", "		", "		", "		", "	Mt 16-13 E tendo Jesus vindo �s partes da Cesareia de Filipe, perguntou aos seus disc�pulos: Quem as pessoas dizem que eu, o Filho do homem, sou? Custo	");
        Pergunta pergunta107 = new Pergunta(4, "	29- Qual dos disc�pulos de Jesus respondeu quem era Ele, de acordo com o Pai, que est� nos c�us?	", "	A	", "	Sim�o Pedro	", "	Pedro	", "	Jo�o 	", "	Mt 16- 16 E Sim�o Pedro respondeu: Tu �s o Cristo, o Filho do Deus vivo! Custo	");
        Pergunta pergunta108 = new Pergunta(4, "	30- Pois que aproveita ao homem ganhar o mundo inteiro, se perder a sua __________________?[...]	", "	alma	", "		", "		", "		", "	Mt 16-26 Pois que proveito h� para algu�m, se ganhar o mundo todo, mas perder a sua alma? Ou que dar� algu�m em resgate da sua alma? Custo	");
        Pergunta pergunta110 = new Pergunta(5, "	1- ___________ dias depois, tomou Jesus consigo alguns de seus disc�pulos, e os conduziu em particular a um alto monte.	", "	C	", "	Quatro	", "	Sete	", "	Seis	", "	Mt 17-1 Seis dias depois, Jesus tomou consigo Pedro, Tiago, e seu irm�o Jo�o, e os levou a s�s a um monte alto. Custo	");
        Pergunta pergunta111 = new Pergunta(5, "	2- Para quem Jesus transfigurou-se em um alto monte nas partes de Cesareia de Filipe?	", "	B	", "	Jos�, Judas e Pedro	", "	Pedro, Tiago e Jo�o	", "	Jonas, Andr� e Sim�o	", "	Mt 17-2 Ent�o transfigurou-se diante deles; seu rosto brilhou como o sol, e suas roupas se tornaram brancas como a luz. Custo	");
        Pergunta pergunta112 = new Pergunta(5, "	3- Qual dos disc�pulos de Jesus disse no alto monte para fazer tabern�culos?	", "	Pedro	", "		", "		", "		", "	Mt 17-4 Pedro, ent�o, disse a Jesus: Senhor, bom � para n�s estarmos aqui. Se queres, fa�amos aqui tr�s tendas: uma para ti, uma para Mois�s, e uma para Elias. Custo	");
        Pergunta pergunta113 = new Pergunta(5, "	4- [...] Se queres, fa�amos aqui ____________ tabern�culos, um para ti, um para Mois�s e um para Elias.	", "	Tr�s	", "		", "		", "		", "	Mt 17-4 Pedro, ent�o, disse a Jesus: Senhor, bom � para n�s estarmos aqui. Se queres, fa�amos aqui tr�s tendas: uma para ti, uma para Mois�s, e uma para Elias. Custo	");
        Pergunta pergunta114 = new Pergunta(5, "	5- Quem era Elias?	", "	A	", "	Jo�o Batista	", "	Jesus Cristo	", "	Jos� 	", "	Mt 17-13 Ent�o os disc�pulos entenderam que ele lhes falara a respeito de Jo�o Batista. Custo	");
        Pergunta pergunta115 = new Pergunta(5, "	6- [...] � gera��o incr�dula e perversa! At� quando estarei eu convosco e at� quando vos sofrerei? Trazei-mo aqui.	", "	V	", "		", "		", "		", "	Mt 17 17 Jesus respondeu: � gera��o incr�dula e perversa! At� quando estarei convosco? At� quando vos suportarei? Trazei-o a mim aqui. Custo	");
        Pergunta pergunta116 = new Pergunta(5, "	7- Em verdade vos digo que, se tiverdes f� como um _________________________ [...]	", "	B	", "	gr�o de ervilha	", "	gr�o de mostarda	", "	gr�o de arroz	", "	Mt 17-20 E Jesus lhes respondeu: Por causa da vossa incredulidade; pois em verdade vos digo, que se tiv�sseis f� como um gr�o de mostarda, dir�eis a este monte: �Passa-te daqui para l�, E ele passaria. E nada vos seria imposs�vel. Custo	");
        Pergunta pergunta117 = new Pergunta(5, "	8- Mas esta casta de dem�nios n�o se expulsa sen�o pela f�.	", "	F	", "		", "		", "		", "	Mt 17 21 Mas este tipo [de dem�nio] n�o sai, a n�o ser por ora��o e jejum. Custo	");
        Pergunta pergunta118 = new Pergunta(5, "	9- Segundo Jesus, quem � o maior no Reino dos c�us ?	", "	C	", "	Os disc�pulos	", "	Os com mais conquistas	", "	As crian�as	", "	Mt 18- 3 e disse: Em verdade vos digo, que se v�s n�o converterdes, e fordes como crian�as, de maneira nenhuma entrareis no Reino dos c�us. Custo	");
        Pergunta pergunta119 = new Pergunta(5, "	10- Porque o Filho do Homem veio salvar o que se tinha desobedecido.	", "	F	", "		", "		", "		", "	Mt 18- 11 Pois o Filho do homem veio para salvar o que havia se perdido. Custo	");
        Pergunta pergunta120 = new Pergunta(5, "	11- Porque onde estiverem dois ou tr�s reunidos em meu nome, a� estou eu no meio deles.	", "	V	", "		", "		", "		", "	Mt 18-20 Pois onde dois ou tr�s estiverem reunidos em meu nome, ali eu estou no meio deles. 	");
        Pergunta pergunta121 = new Pergunta(5, "	12- Quantas vezes Jesus disse a Pedro que precisar�amos perdoar o irm�o?	", "	B	", "	Tr�s vezes	", "	At� setenta vezes sete	", "	Sempre	", "	Mt 18- Jesus lhe respondeu: Eu n�o te digo at� sete, mas sim at� setenta vezes sete. 	");
        Pergunta pergunta122 = new Pergunta(5, "	13- Portanto, o que Deus ajuntou n�o separe o homem.	", "	V	", "		", "		", "		", "	Mt 19-6 Assim eles j� n�o s�o mais dois, mas sim uma �nica carne; portanto, o que Deus juntou, o ser humano n�o separe. Custo	");
        Pergunta pergunta123 = new Pergunta(5, "	14- Em verdade vos digo que � dif�cil entrar um rico no Reino dos c�us.	", "	V	", "		", "		", "		", "	Mt 19-23 Jesus, ent�o, disse aos seus disc�pulos: Em verdade vos digo que dificilmente o rico entrar� no reino dos c�us. Custo	");
        Pergunta pergunta124 = new Pergunta(5, "	15- E outra vez vos digo que � mais f�cil passar um _______________ pelo fundo de uma ______________ do que entrar um rico no Reino de Deus.	", "	A 	", "	camelo / agulha	", "	cavalo / cerca	", "	burro / agulha	", "	Mt 19-24 Ali�s, eu vos digo que � mais f�cil um camelo passar pela abertura de uma agulha do que o rico entrar no reino de Deus. Custo	");
        Pergunta pergunta125 = new Pergunta(5, "	16- Por�m muitos primeiros ser�o derradeiros, e muitos derradeiros n�o ser�o primeiros.	", "	F	", "		", "		", "		", "	Mt 19-30 Por�m muitos primeiros ser�o �ltimos; e �ltimos, primeiros.Custo	");
        Pergunta pergunta126 = new Pergunta(5, "	17- Quantos disc�pulos Jesus tinha?	", "	Doze	", "		", "		", "		", "	Mt 20-17 E enquanto Jesus subia a Jerusal�m, tomou consigo os doze disc�pulos � parte no caminho, e lhes disse: Custo	");
        Pergunta pergunta127 = new Pergunta(5, "	18- Onde o Filho do Homem foi entregue aos pr�ncipes dos sacerdotes e aos escribas para � morte?	", "	Jerusal�m	", "		", "		", "		", "	Mt 20-18 Eis que estamos subindo a Jerusal�m, e o Filho do homem ser� entregue aos chefes dos sacerdotes e aos escribas, e o condenar�o � morte. Custo	");
        Pergunta pergunta128 = new Pergunta(5, "	19- O que a m�e dos filhos de Zebedeu pediu a Jesus?	", "	C	", "	Para cur�-los	", "	Para salv�-los, independente de tudo	", "	Um lugar para os dois � sua direita e esquerda nos c�us	", "	Mt 20-21 E ele lhe perguntou: O que queres? Ela lhe disse: D� ordem para que estes meus dois filhos se sentem, um � tua direita e outro � tua esquerda, no teu Reino. Custo	");
        Pergunta pergunta129 = new Pergunta(5, "	20- O Filho do Homem n�o veio para ser servido, mas para servir e para dar a sua vida em resgate dos puros.	", "	F	", "		", "		", "		", "	Mt 20-28 assim como o Filho do homem n�o veio para ser servido, mas sim para servir, e para dar a sua vida em resgate por muitos. Custo	");
        Pergunta pergunta131 = new Pergunta(6, "	1- Como foi a entrada triunfal de Jesus em Jerusal�m?	", "	B	", "	Cercado por uma multid�o	", "	Assentado sobre uma jumenta e sobre um jumentinho	", "	Assentado em um cavalo	", "	Mt 21-7 Ent�o trouxeram a jumenta e o jumentinho, puseram as suas capas sobre eles, e fizeram [-no] montar [] sobre elas. Custo	");
        Pergunta pergunta132 = new Pergunta(6, "	2- O que Jesus fez quando encontrou o povo vendendo coisas no templo em Jerusal�m?	", "	A 	", "	Expulsou todos	", "	Bateu neles	", "	Ordenou que se retirassem	", "	12 Jesus entrou no Templo de Deus; ent�o expulsou todos os que estavam vendendo e comprando no Templo, e virou as mesas dos cambistas e as cadeiras dos que vendiam pombas. Custo	");
        Pergunta pergunta133 = new Pergunta(6, "	3- O que Jesus Cristo disse que seria chamada sua casa?	", "	C	", "	Sinagoga	", "	Templo 	", "	Casa de ora��o	", "	Mt 21 13 E disse-lhes: Est� escrito: �Minha casa ser� chamada casa de ora��o�; mas v�s a tornastes em covil de ladr�es! Custo|fn:  TR, RP: tornastes - N4: tornais |fn:  Ref. Isa�as 56:7; Jeremias 7:11	");
        Pergunta pergunta134 = new Pergunta(6, "	4- Mas v�s a tendes convertido em _____________________ .	", "	A 	", "	covil de ladr�es	", "	perdi��o	", "	um lugar de atos il�citos	", "	Mt 21-13 E disse-lhes: Est� escrito: �Minha casa ser� chamada casa de ora��o�; mas v�s a tornastes em covil de ladr�es! Custo|fn:  TR, RP: tornastes - N4: tornais |fn:  Ref. Isa�as 56:7; Jeremias 7:11	");
        Pergunta pergunta135 = new Pergunta(6, "	5- Quem foi ter com Jesus no templo durante a purifica��o?	", "	B	", "	Leprosos	", "	Cegos e coxos	", "	Cegos	", "	Mt 21- 14 E cegos e mancos vieram a ele no Templo, e ele os curou. Custo	");
        Pergunta pergunta136 = new Pergunta(6, "	6- Para onde Jesus foi ap�s a purifica��o do templo?	", "	Bet�nia	", "		", "		", "		", "	Mt 21-17 Ent�o ele os deixou, e saiu da cidade para Bet�nia, e ali passou a noite. Custo	");
        Pergunta pergunta137 = new Pergunta(6, "	7- Qual �rvore Jesus Cristo viu voltando para Jerusal�m e mandou-a secar?	", "	Figueira	", "		", "		", "		", "	Mt 21-19 Quando ele viu uma figueira perto do caminho, veio a ela, mas nada nela achou, a n�o ser somente folhas. E disse-lhe: Nunca de ti nas�a fruto, jamais! E imediatamente a figueira se secou. Custo	");
        Pergunta pergunta138 = new Pergunta(6, "	8- Porque muitos s�o chamados, mas poucos, _______________ .	", "	escolhidos	", "		", "		", "		", "	Mt 22 14 Pois muitos s�o chamados, por�m poucos escolhidos. Custo	");
        Pergunta pergunta139 = new Pergunta(6, "	9- Amar�s o Senhor, teu Deus, de todo o teu cora��o.	", "	F 	", "		", "		", "		", "	Mt 22 37 E Jesus lhe respondeu: Amar�s ao Senhor teu Deus com todo o teu cora��o, com toda a tua alma, e com todo o teu entendimento: Custo	");
        Pergunta pergunta140 = new Pergunta(6, "	10- Qual � o segundo grande mandamento da lei?	", "	B 	", "	Honra teu pai e tua m�e	", "	Amar�s o teu pr�ximo como a ti mesmo	", "	N�o matar�s	", "	Mt 22-39 E o segundo, semelhante a este, [�] : Amar�s o teu pr�ximo como a ti mesmo. Custo	");
        Pergunta pergunta141 = new Pergunta(6, "	11- E o que a si mesmo se exaltar ser� humilhado; e o que a si mesmo se humilhar ser� exaltado.	", "	V	", "		", "		", "		", "	Mt 23-12 E o que a si mesmo se exaltar ser� humilhado; e o que a si mesmo se humilhar ser� exaltado. Custo	");
        Pergunta pergunta142 = new Pergunta(6, "	12- Onde estava o Senhor durante o serm�o prof�tico do princ�pio das dores?	", "	B	", "	Bet�nia	", "	Monte das Oliveiras	", "	Jord�nia	", "	Mt 24- 3 E, depois de se assentar no monte das Oliveiras, os disc�pulos se aproximaram dele reservadamente, perguntando: Dize-nos, quando ser�o estas coisas, e que sinal haver� da tua vinda, e do fim da era? Custo	");
        Pergunta pergunta143 = new Pergunta(6, "	13- Porque muitos vir�o em meu nome, dizendo: Eu sou o Cristo; e enganar�o a muitos.	", "	V	", "		", "		", "		", "	Mt 24 5 Porque muitos vir�o em meu nome, dizendo: �Eu sou o Cristo�, e enganar�o a muitos. Custo	");
        Pergunta pergunta144 = new Pergunta(6, "	14- Mas todas essas coisas s�o o fim das dores.	", "	F	", "		", "		", "		", "	Mt 24 8 Mas todas estas coisas s�o o come�o das dores. Custo	");
        Pergunta pergunta145 = new Pergunta(6, "	15- O c�u e a terra passar�o, mas as minhas _________________ n�o h�o de passar.	", "	palavras	", "		", "		", "		", "	Mt 24 35 O c�u e a terra passar�o, mas minhas palavras de maneira nenhuma passar�o. Custo	");
        Pergunta pergunta146 = new Pergunta(6, "	16- Quem h� de saber o dia e a hora da vinda do Filho do Homem?	", "	Deus	", "		", "		", "		", "	Mt 24 36 Por�m daquele dia e hora, ningu�m sabe, nem os anjos do c�u, a n�o ser meu Pai somente. Custo	");
        Pergunta pergunta147 = new Pergunta(6, "	17- Vigiai, pois, porque n�o sabeis a que hora h� de vir o vosso Senhor.	", "	V	", "		", "		", "		", "	Mt 24 42 Vigiai, pois, porque n�o sabeis em que hora o vosso Senhor vir�. Custo	");
        Pergunta pergunta148 = new Pergunta(6, "	18- Por isso, estai v�s apercebidos tamb�m, porque o Filho do Homem h� de vir � hora determinada.	", "	F	", "		", "		", "		", "	Mt 24 44 Portanto tamb�m v�s estai prontos, porque o Filho do homem vir� na hora que n�o esperais. Custo	");
        Pergunta pergunta149 = new Pergunta(6, "	19- Bem-aventurado aquele servo que o Senhor, quando vier, achar servindo assim.	", "	V	", "		", "		", "		", "	Mt 24 46 Feliz ser� aquele servo a quem, quando o seu senhor vier, achar fazendo assim. Custo	");
        Pergunta pergunta150 = new Pergunta(6, "	20- Em verdade vos digo que o por� sobre todos os seus bens.	", "	V	", "		", "		", "		", "	Mt 24-47 Em verdade vos digo que ele o por� sobre todos os seus bens. Custo	");
        Pergunta pergunta152 = new Pergunta(7, "	1- Quantas virgens estavam � espera do esposo?	", "	A 	", "	Dez	", "	Quinze	", "	Doze	", "	Mt 25 1 Ent�o o Reino dos c�us ser� semelhante a dez virgens, que tomaram suas l�mpadas, e sa�ram ao encontro do noivo. Custo	");
        Pergunta pergunta153 = new Pergunta(7, "	2- Como eram chamadas as dez virgens?	", "	C	", "	Cinco nervosas e cinco tristes	", "	Cinco temerosas e cinco apressadas	", "	Cinco prudentes e cinco loucas	", "	Mt 25 2 E cinco delas eram prudentes, e cinco tolas. Custo	");
        Pergunta pergunta154 = new Pergunta(7, "	3- O que as virgens deveriam ter em suas l�mpadas?	", "	B	", "	Vinagre	", "	Azeite	", "	�leo	", "	Mt 25 3 As tolas, quando tomaram as suas l�mpadas, n�o tomaram azeite consigo. Custo	");
        Pergunta pergunta155 = new Pergunta(7, "	4- Quando foi que Jesus Cristo foi crucificado?	", "	A 	", "	Na p�scoa	", "	Num s�bado	", "	Em uma comemora��o de reis	", "	Mt 26 2 V�s bem sabeis que daqui a dois dias � a P�scoa, e o Filho do homem ser� entregue para ser crucificado. Custo	");
        Pergunta pergunta156 = new Pergunta(7, "	5- Qual o nome do sumo sacerdote envolvido na crucifica��o de Cristo?	", "	Caif�s	", "		", "		", "		", "	Mt 26 3 Ent�o os chefes dos sacerdotes, os escribas, e os anci�os do povo se reuniram na casa do sumo sacerdote, que se chamava Caif�s. Custo	");
        Pergunta pergunta157 = new Pergunta(7, "	6- E, estando Jesus em Bet�nia, em casa de _______________ , o leproso, [....]	", "	Sim�o	", "		", "		", "		", "	Mt 26 6 Enquanto Jesus estava em Bet�nia, na casa de Sim�o o leproso, Custo	");
        Pergunta pergunta158 = new Pergunta(7, "	7- No jantar em Bet�nia, que perfume foi lavado os p�s de Jesus?	", "	Unguento	", "		", "		", "		", "	Mt 26 7 veio a ele uma mulher com um vaso de alabastro, de �leo perfumado de grande valor, e derramou sobre a cabe�a dele, enquanto estava sentado � mesa. Custo	");
        Pergunta pergunta159 = new Pergunta(7, "	8- Qual foi o pre�o da trai��o de Judas?	", "	B	", "	20 moedas de prata	", "	30 moedas de prata	", "	50 moedas de prata	", "	Mt 26 15 e disse: O que quereis me dar, para que eu o entregue a v�s? E eles lhe determinaram trinta [moedas] de prata. Custo	");
        Pergunta pergunta160 = new Pergunta(7, "	9- Quem traiu Jesus Cristo?	", "	C 	", "	Bartolomeu	", "	Habacuque	", "	Judas Iscariotes	", "	Mt 26 25 E Judas, o que o tra�a, perguntou: Por acaso sou eu, Rabi? [Jesus] lhe disse: Tu o disseste. Custo	");
        Pergunta pergunta161 = new Pergunta(7, "	10- Quantas vezes Pedro negou Jesus?	", "	Tr�s	", "		", "		", "		", "	Mt 26 34 Jesus lhe disse: Em verdade te digo que, nesta mesma noite, antes do galo cantar, tu me negar�s tr�s vezes. Custo	");
        Pergunta pergunta162 = new Pergunta(7, "	11- Aonde Jesus foi orar com seus disc�pulos antes da crucifica��o?	", "	A 	", "	No Gets�mani	", "	No templo	", "	Na sinagoga	", "	Mt 26 36 Ent�o Jesus veio com eles a um lugar chamado Gets�mani, e disse aos disc�pulos: Ficai sentados aqui, enquanto eu vou ali orar. Custo	");
        Pergunta pergunta163 = new Pergunta(7, "	12- Meu Pai, se � poss�vel, passa de mim este c�lice; todavia, n�o seja como eu quero, mas como tu queres.	", "	V	", "		", "		", "		", "	Mt 26 39 E indo um pouco mais adiante, prostrou-se sobre o seu rosto, orando, e dizendo: Meu Pai, se � poss�vel, passe de mim este c�lice; por�m, n�o [seja] como eu quero, mas sim como tu [queres] . Custo	");
        Pergunta pergunta164 = new Pergunta(7, "	13- Vigiai e orai, para que n�o entreis em tenta��o; na verdade, o esp�rito est� pronto, mas a ____________ � fraca.	", "	carne	", "		", "		", "		", "	Mt 26 41 Vigiai e orai, para que n�o entreis em tenta��o. De fato, o esp�rito [est�] pronto, mas a carne [�] fraca. Custo	");
        Pergunta pergunta165 = new Pergunta(7, "	14- Qual sinal o traidor deu para entregar Jesus?	", "	B	", "	Um aperto de m�o	", "	Um beijo no rosto	", "	Um abra�o	", "	Mt 26 48 O seu traidor havia lhes dado sinal, dizendo: Aquele a quem eu beijar, � esse. Prendei-o. Custo	");
        Pergunta pergunta166 = new Pergunta(7, "	15- Como Jesus se referia a Judas enquanto o mesmo lhe tra�a?	", "	Amigo	", "		", "		", "		", "	Mt 26 50 Jesus, por�m, lhe perguntou: Amigo, para que vieste? Ent�o chegaram, agarraram Jesus, e o prenderam. Custo	");
        Pergunta pergunta167 = new Pergunta(7, "	16- Qual animal cantou antes de Pedro negar Jesus?	", "	Galo	", "		", "		", "		", "	Mt 26 75 Ent�o Pedro se lembrou da palavra de Jesus, que lhe dissera: Antes do galo cantar, tu me negar�s tr�s vezes. Assim ele saiu, e chorou amargamente.Custo	");
        Pergunta pergunta168 = new Pergunta(7, "	17- Quem era o governador no julgamento de Jesus?	", "	A	", "	P�ncio Pilatos	", "	Jos�	", "	Sim�o Pedro	", "	Mt 27 2 E o levaram amarrado, e o entregaram a P�ncio Pilatos, o governador. Custo	");
        Pergunta pergunta169 = new Pergunta(7, "	18- Qual atitude de Judas consigo mesmo quando percebeu sua trai��o?	", "	Enforcou-se	", "		", "		", "		", "	Mt 27 5 Ent�o ele lan�ou as [moedas] de prata no templo, saiu, e foi enforcar-se. Custo	");
        Pergunta pergunta170 = new Pergunta(7, "	19- N�o � l�cito met�-las no cofre das ofertas, porque s�o pre�o de morte.	", "	F	", "		", "		", "		", "	Mt 27 6 Os chefes dos sacerdotes tomaram as [moedas] de prata, e disseram: N�o � l�cito p�-las no tesouro das ofertas, pois isto � pre�o de sangue. Custo	");
        Pergunta pergunta171 = new Pergunta(7, "	20- O que fizeram com as moedas de prata da trai��o � Jesus?	", "	B	", "	Jogaram no mar	", "	Compraram um campo de um oleiro	", "	Guardaram	", "	Mt 27 7 Ent�o juntamente se aconselharam, e compraram com elas o campo do oleiro, para ser cemit�rio dos estrangeiros. Custo	");
        Pergunta pergunta172 = new Pergunta(7, "	21- Como se chamou o lugar que compraram com as moedas da trai��o?	", "	C	", "	P�ntano	", "	Vale de Ossos	", "	Campo de Sangue	", "	Mt 27 8 Por isso aquele campo tem sido chamado campo de sangue at� hoje. Custo	");
        Pergunta pergunta173 = new Pergunta(7, "	22- Qual o nome do preso que o povo escolheu soltar em vez de Jesus?	", "	Barrab�s	", "		", "		", "		", "	Mt 27 21 O governador lhes perguntou: Qual destes dois quereis que vos solte? E responderam: Barrab�s! Custo	");
        Pergunta pergunta174 = new Pergunta(7, "	23- Quem lavou as m�os no julgamento de Jesus? 	", "	Pilatos	", "		", "		", "		", "	Mt 27 24 Quando, pois, Pilatos viu que nada adiantava, em vez disso se fazia mais tumulto, ele pegou �gua, lavou as m�os diante da multid�o, e disse: Estou inocente do sangue deste justo. A responsabilidade � vossa. Custo	");
        Pergunta pergunta175 = new Pergunta(7, "	24- Qual a cor da capa que cobriu Jesus na audi�ncia?	", "	A	", "	Escarlate	", "	Vinho	", "	Cinza	", "	Mt 27 28 Eles o despiram e o cobriram com um manto vermelho. Custo	");
        Pergunta pergunta176 = new Pergunta(7, "	25- E, tecendo uma coroa de _______________ , puseram-lha na cabe�a, [...]	", "	espinhos	", "		", "		", "		", "	Mt 27 29 E, depois de tecerem uma coroa de espinhos, puseram-na sobre a sua cabe�a, e uma cana em sua m�o direita. Em seguida, puseram-se de joelhos diante dele, zombando-o, e diziam: Felicita��es, Rei dos Judeus! Custo	");
        Pergunta pergunta177 = new Pergunta(7, "	26- E, quando sa�am, encontraram um homem cireneu, chamado ______________ , a quem constrangeram a levar a sua cruz.	", "	Sim�o	", "		", "		", "		", "	Mt 27 32 Ao sa�rem, encontraram um homem de Cirene, por nome Sim�o; e obrigaram-no a levar sua cruz. Custo	");
        Pergunta pergunta178 = new Pergunta(7, "	27- O que significa o lugar chamado G�lgota?	", "	A	", "	Lugar da Caveira	", "	Vale de Ossos	", "	Caverna	", "	Mt 27 33 E quando chegaram ao lugar chamado G�lgota, que significa �o lugar da caveira�, Custo	");
        Pergunta pergunta179 = new Pergunta(7, "	28- O que foi dado a Jesus para beber mas n�o o quis?	", "	C	", "	Vinho com vinagre	", "	Vinho com �gua	", "	Vinho com fel	", "	Mt 27 34 deram-lhe de beber vinagre misturado com fel. E, depois de provar, n�o quis beber. Custo	");
        Pergunta pergunta180 = new Pergunta(7, "	29- O que puseram escrito por cima da cabe�a de Cristo?	", "	B	", "	ESTE � JESUS, O REI DOS OPRIMIDOS	", "	ESTE � JESUS, O REI DOS JUDEUS	", "	ESTE � JESUS, O REI DOS PECADORES	", "	Mt 27 37 E puseram, por cima de sua cabe�a, sua acusa��o escrita: ESTE � JESUS, O REI DOS JUDEUS. Custo	");
        Pergunta pergunta181 = new Pergunta(7, "	30- E foram crucificados com ele __________ salteadores, [...]	", "	dois	", "		", "		", "		", "	Mt 27 38 Ent�o foram crucificados com ele dois criminosos, um � direita, e outro � esquerda. Custo	");
        Pergunta pergunta182 = new Pergunta(7, "	31- Quem pediu o corpo de Jesus?	", "	A	", "	Jos�, um disc�pulo rico de Arimateia	", "	Pedro, da Galileia	", "	Jo�o, do Egito	", "	Mt 27 58 Ele chegou a Pilatos, e pediu o corpo de Jesus. Ent�o Pilatos mandou que o corpo [lhe] fosse entregue. Custo	");
        Pergunta pergunta183 = new Pergunta(7, "	32- Quem estava assentado defronte do sepulcro?	", "	B	", "	Marta e Jos�	", "	Maria Madalena e a outra Maria	", "	Marta e Maria	", "	Mt 27 61 E ali estavam Maria Madalena e a outra Maria, sentadas de frente ao sepulcro. Custo	");
        Pergunta pergunta184 = new Pergunta(7, "	33- Aonde Jesus ressuscitado aparece para seus disc�pulos?	", "	Galileia	", "		", "		", "		", "	Mt 28 18 Jesus se aproximou deles, e lhes falou: Todo o poder me � dado no c�u e na terra. Custo	");
        Pergunta pergunta185 = new Pergunta(7, "	34 - Jesus afirma que estar� conosco em quais momentos ?	", "	C	", "	Quando ele voltar	", "	Quando orarmos	", "	Todos os dias	", "	Mt 28 20 ensinando-lhes a guardar todas as coisas que eu vos tenho mandado. E eis que eu estou convosco todos os dias, at� o fim dos tempos. Am�m.Custo	");

        perguntasGerais.Add(pergunta1);
        perguntasGerais.Add(pergunta2);
        perguntasGerais.Add(pergunta3);
        perguntasGerais.Add(pergunta4);
        perguntasGerais.Add(pergunta5);
        perguntasGerais.Add(pergunta6);
        perguntasGerais.Add(pergunta7);
        perguntasGerais.Add(pergunta8);
        perguntasGerais.Add(pergunta9);
        perguntasGerais.Add(pergunta10);
        perguntasGerais.Add(pergunta11);
        perguntasGerais.Add(pergunta12);
        perguntasGerais.Add(pergunta13);
        perguntasGerais.Add(pergunta14);
        perguntasGerais.Add(pergunta15);
        perguntasGerais.Add(pergunta16);
        perguntasGerais.Add(pergunta17);
        perguntasGerais.Add(pergunta18);
        perguntasGerais.Add(pergunta19);
        perguntasGerais.Add(pergunta20);
        perguntasGerais.Add(pergunta22);
        perguntasGerais.Add(pergunta23);
        perguntasGerais.Add(pergunta24);
        perguntasGerais.Add(pergunta25);
        perguntasGerais.Add(pergunta26);
        perguntasGerais.Add(pergunta27);
        perguntasGerais.Add(pergunta28);
        perguntasGerais.Add(pergunta29);
        perguntasGerais.Add(pergunta30);
        perguntasGerais.Add(pergunta31);
        perguntasGerais.Add(pergunta32);
        perguntasGerais.Add(pergunta33);
        perguntasGerais.Add(pergunta34);
        perguntasGerais.Add(pergunta35);
        perguntasGerais.Add(pergunta36);
        perguntasGerais.Add(pergunta37);
        perguntasGerais.Add(pergunta38);
        perguntasGerais.Add(pergunta39);
        perguntasGerais.Add(pergunta40);
        perguntasGerais.Add(pergunta41);
        perguntasGerais.Add(pergunta43);
        perguntasGerais.Add(pergunta44);
        perguntasGerais.Add(pergunta45);
        perguntasGerais.Add(pergunta46);
        perguntasGerais.Add(pergunta47);
        perguntasGerais.Add(pergunta48);
        perguntasGerais.Add(pergunta49);
        perguntasGerais.Add(pergunta50);
        perguntasGerais.Add(pergunta51);
        perguntasGerais.Add(pergunta52);
        perguntasGerais.Add(pergunta53);
        perguntasGerais.Add(pergunta54);
        perguntasGerais.Add(pergunta55);
        perguntasGerais.Add(pergunta56);
        perguntasGerais.Add(pergunta57);
        perguntasGerais.Add(pergunta58);
        perguntasGerais.Add(pergunta59);
        perguntasGerais.Add(pergunta60);
        perguntasGerais.Add(pergunta61);
        perguntasGerais.Add(pergunta62);
        perguntasGerais.Add(pergunta63);
        perguntasGerais.Add(pergunta64);
        perguntasGerais.Add(pergunta65);
        perguntasGerais.Add(pergunta66);
        perguntasGerais.Add(pergunta67);
        perguntasGerais.Add(pergunta68);
        perguntasGerais.Add(pergunta69);
        perguntasGerais.Add(pergunta70);
        perguntasGerais.Add(pergunta71);
        perguntasGerais.Add(pergunta72);
        perguntasGerais.Add(pergunta73);
        perguntasGerais.Add(pergunta74);
        perguntasGerais.Add(pergunta75);
        perguntasGerais.Add(pergunta76);
        perguntasGerais.Add(pergunta77);
        perguntasGerais.Add(pergunta79);
        perguntasGerais.Add(pergunta80);
        perguntasGerais.Add(pergunta81);
        perguntasGerais.Add(pergunta82);
        perguntasGerais.Add(pergunta83);
        perguntasGerais.Add(pergunta84);
        perguntasGerais.Add(pergunta85);
        perguntasGerais.Add(pergunta86);
        perguntasGerais.Add(pergunta87);
        perguntasGerais.Add(pergunta88);
        perguntasGerais.Add(pergunta89);
        perguntasGerais.Add(pergunta90);
        perguntasGerais.Add(pergunta91);
        perguntasGerais.Add(pergunta92);
        perguntasGerais.Add(pergunta93);
        perguntasGerais.Add(pergunta94);
        perguntasGerais.Add(pergunta95);
        perguntasGerais.Add(pergunta96);
        perguntasGerais.Add(pergunta97);
        perguntasGerais.Add(pergunta98);
        perguntasGerais.Add(pergunta99);
        perguntasGerais.Add(pergunta100);
        perguntasGerais.Add(pergunta101);
        perguntasGerais.Add(pergunta102);
        perguntasGerais.Add(pergunta103);
        perguntasGerais.Add(pergunta104);
        perguntasGerais.Add(pergunta105);
        perguntasGerais.Add(pergunta106);
        perguntasGerais.Add(pergunta107);
        perguntasGerais.Add(pergunta108);
        perguntasGerais.Add(pergunta110);
        perguntasGerais.Add(pergunta111);
        perguntasGerais.Add(pergunta112);
        perguntasGerais.Add(pergunta113);
        perguntasGerais.Add(pergunta114);
        perguntasGerais.Add(pergunta115);
        perguntasGerais.Add(pergunta116);
        perguntasGerais.Add(pergunta117);
        perguntasGerais.Add(pergunta118);
        perguntasGerais.Add(pergunta119);
        perguntasGerais.Add(pergunta120);
        perguntasGerais.Add(pergunta121);
        perguntasGerais.Add(pergunta122);
        perguntasGerais.Add(pergunta123);
        perguntasGerais.Add(pergunta124);
        perguntasGerais.Add(pergunta125);
        perguntasGerais.Add(pergunta126);
        perguntasGerais.Add(pergunta127);
        perguntasGerais.Add(pergunta128);
        perguntasGerais.Add(pergunta129);
        perguntasGerais.Add(pergunta131);
        perguntasGerais.Add(pergunta132);
        perguntasGerais.Add(pergunta133);
        perguntasGerais.Add(pergunta134);
        perguntasGerais.Add(pergunta135);
        perguntasGerais.Add(pergunta136);
        perguntasGerais.Add(pergunta137);
        perguntasGerais.Add(pergunta138);
        perguntasGerais.Add(pergunta139);
        perguntasGerais.Add(pergunta140);
        perguntasGerais.Add(pergunta141);
        perguntasGerais.Add(pergunta142);
        perguntasGerais.Add(pergunta143);
        perguntasGerais.Add(pergunta144);
        perguntasGerais.Add(pergunta145);
        perguntasGerais.Add(pergunta146);
        perguntasGerais.Add(pergunta147);
        perguntasGerais.Add(pergunta148);
        perguntasGerais.Add(pergunta149);
        perguntasGerais.Add(pergunta150);
        perguntasGerais.Add(pergunta152);
        perguntasGerais.Add(pergunta153);
        perguntasGerais.Add(pergunta154);
        perguntasGerais.Add(pergunta155);
        perguntasGerais.Add(pergunta156);
        perguntasGerais.Add(pergunta157);
        perguntasGerais.Add(pergunta158);
        perguntasGerais.Add(pergunta159);
        perguntasGerais.Add(pergunta160);
        perguntasGerais.Add(pergunta161);
        perguntasGerais.Add(pergunta162);
        perguntasGerais.Add(pergunta163);
        perguntasGerais.Add(pergunta164);
        perguntasGerais.Add(pergunta165);
        perguntasGerais.Add(pergunta166);
        perguntasGerais.Add(pergunta167);
        perguntasGerais.Add(pergunta168);
        perguntasGerais.Add(pergunta169);
        perguntasGerais.Add(pergunta170);
        perguntasGerais.Add(pergunta171);
        perguntasGerais.Add(pergunta172);
        perguntasGerais.Add(pergunta173);
        perguntasGerais.Add(pergunta174);
        perguntasGerais.Add(pergunta175);
        perguntasGerais.Add(pergunta176);
        perguntasGerais.Add(pergunta177);
        perguntasGerais.Add(pergunta178);
        perguntasGerais.Add(pergunta179);
        perguntasGerais.Add(pergunta180);
        perguntasGerais.Add(pergunta181);
        perguntasGerais.Add(pergunta182);
        perguntasGerais.Add(pergunta183);
        perguntasGerais.Add(pergunta184);
        perguntasGerais.Add(pergunta185);
        //perguntasGerais.Add(pergunta186);
        //perguntasGerais.Add(pergunta187);
        //perguntasGerais.Add(pergunta188);
        //perguntasGerais.Add(pergunta189);
        //perguntasGerais.Add(pergunta190);
        //perguntasGerais.Add(pergunta191);
        //perguntasGerais.Add(pergunta192);
        //perguntasGerais.Add(pergunta193);
        //perguntasGerais.Add(pergunta194);


       tratarPerguntas();
    }

    public void carregarTemaEscolhido()
    {

        perguntasGerais.Clear();
        temas.Clear();



        if (estudoEscolhido.Equals("Jesus"))
        {
            carregarTemasOffLine();
            tema1();
            tema2();
            tema3();
            tema4();
            tema5();
            tema6();
            tema7_Lucas2_1_7();
            
        }else if (estudoEscolhido.Equals("Mateus"))
        {
            carregarTemasMateus();

            Mateus();
        }
        else
        {

            pegarTemasNoFirebase("pt", estudoEscolhido);

        }





        // SceneManager.LoadScene("temas");


        SceneManager.LoadScene("temas");

    }






    private void tema7_Lucas2_1_7()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Quem era o imperador que mandou uma ordem para todos os povos do Imp�rio.";
        pergunta1.correta = "A";
        pergunta1.a = "Augusto";
        pergunta1.b = "Alexandre";
        pergunta1.c = "Davi";
        pergunta1.dica = "Lucas 2.1";

        //Este padr�o abaixo � para perguntas de verdadeiro ou falso:

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Todas as pessoas deviam se registrar a fim de ser feita uma contagem da popula��o.";
        pergunta2.correta = "A";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = " Lucas 2.1";

        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Onde as pessoas iam se registrar?";
        pergunta3.correta = "C";
        pergunta3.a = "Roma";
        pergunta3.b = "Gr�cia";
        pergunta3.c = "Onde nasceram";
        pergunta3.dica = " Lucas 2.2";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Jos� foi para qual regi�o?";
        pergunta4.correta = "C";
        pergunta4.a = "Roma";
        pergunta4.b = "Gr�cia";
        pergunta4.c = "Judeia";
        pergunta4.dica = " Lucas 2.4";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Jos� foi registrar-se l� porque era descendente de Davi";
        pergunta5.correta = "A";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = " Lucas 2.4";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Jos� levou Maria, com quem era casado";
        pergunta6.correta = "B";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = " Lucas 2.5";


        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Maria estava Gr�vida";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = " Lucas 2.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Ent�o Jesus nasceu em Nazar�";
        pergunta8.correta = "B";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = " Lucas 2.6";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Jesus nasceu em uma pens�o";
        pergunta9.correta = "B";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = " Lucas 2.7";



        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 7;
        }

        perguntasGerais.AddRange(perguntas);


    }



    public void carregarTemasOffLineGenesis()
    {

        Tema tema1 = new Tema("1", "Cria��o", "", "", "", "0");
        tema1.mensagemInicial = "No princ�pio, Deus projetou os c�us a terra e a humanidade para viver nesse mundo. Deus criou a terra, antes de  dizer: 'haja luz'. Voc� est� afiado sobre a obra da Cria��o e o prop�sito de Deus?  ";
        temas.Add(tema1);

        Tema tema2 = new Tema("2", "A vida ap�s o jardim", "", "", "", "15");
        tema2.mensagemInicial = "Tudo ia bem no �den at� os primeiros humanos criados desrespeitarem o Criador.  O ser humano ficou incompat�vel com Deus, que � santo. Ele foi, ent�o, tirado do para�so. E a Cria��o n�o foi mais a mesma.";
        temas.Add(tema2);

        Tema tema3 = new Tema("3", "Caim e Abel", "", "", "", "30");
        tema3.mensagemInicial = "Vamos conhecer os filhos de Ad�o e Eva";
        temas.Add(tema3);


        Tema tema4 = new Tema("4", "O Dil�vio ", "", "", "", "31");
        tema4.mensagemInicial = "Essa � uma hist�ria triste e bonita. Nela vemos uma gera��o centen�ria de pessoas se rebelar contra a justi�a de Deus. Mas vemos a justi�a de Deus em reconpensar o justo por praticar a Sua justi�a.";
        temas.Add(tema4);


        Tema tema5 = new Tema("5", "Depois do Dil�vio ", "", "", "", "41");
        tema5.mensagemInicial = "Essa historia � bem curriosa porque mostra um recome�o da humanidade e revela que o ser humano continua  com inclina��es para o mal, mas Deus sempre tem seus fi�is ou pessoa (s) que atende ao seu chamado e n�o o deixa desistir da humanidade.";
        temas.Add(tema5);

        Tema tema6 = new Tema("6", "O chamado de Abra�o ", "", "", "", "52");
        tema6.mensagemInicial = "A beleza de uma amizade entre Deus e um ser humano est� bem preenchida nessa hist�ria mais do que nos relatos  anteriores em que o leitor da B�blica  precisa  usar  infer�ncia para saber disso, como � o caso de Enoque, que andou com Deus";
        temas.Add(tema6);

        Tema tema7 = new Tema("7", "A fidelidade de Abra�o ", "", "", "", "53");
        tema7.mensagemInicial = "A segunda parte da hist�ria desse homem de f� mostra mais da relac�o dele com Deus de modo a nos emocionar  e ensinar mais sobre nosso Pai celestial.";
        temas.Add(tema7);

        Tema tema8 = new Tema("8", "A resili�ncia de Isaque", "", "", "", "70");
        tema8.mensagemInicial = "Um homem que soube viver entre amigos e inimigos com uma paci�ncia exemplar foi Isaque,  o protagonista desta vez.";
        temas.Add(tema8);

        Tema tema9 = new Tema("9", "Jac� ", "", "", "", "80");
        tema9.mensagemInicial = "Jac� e a origem dos nomes dos principais territ�rios de Cana� Embora os judeus(israelitas) estimem Abfa�o como seu pai, Jac� � o genitor mais pr�ximo dos descendentes que deram nomes tribos e, na posse  de Cana�, a territorios Essa � uma hist�ria bastante  tensa na B�blia. ";
        temas.Add(tema9);


        Tema tema10 = new Tema("10", "Jos� ", "", "", "", "92");
        tema10.mensagemInicial = "A hist�ria de um homem brilhantemente  honesto Jos� teve uma caracter�stica  que agrada a Deus mais destacada que os demais personagens de G�nesis, uma qualidade que deve causar admira��o no crist�o, a honestidade, que � um amor � justi�a.";
        temas.Add(tema10);



    }


    private void genesis2()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "";
        pergunta1.correta = "";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "As �reas mais naturais do planeta s�o selvagens, dif�ceis de viver por causa do pecado.";
        pergunta2.correta = "V";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 3.18";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Contudo, a vida de trabalho do homem passou a ser mais f�cil.";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 3.19";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "A mulher n�o nasceu para dar � luz com dores.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn 3.16";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "A  humanidade  perdeu a oportunidade de viver para sempre nesse mundo por causa do pecado.";
        pergunta5.correta = "V";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 3.19; Rm 6.23";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Ap�s o pecado o ser humano ainda p�de viver pr�ximo de 1000 anos";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 5.3 a 27";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Ad�o viveu at� 930 anos de idade.";
        pergunta7.correta = "V";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn 5.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Quem foi o homem mais velho da B�blia?";
        pergunta8.correta = "B";
        pergunta8.a = "Ad�o";
        pergunta8.b = "Matusal�m";
        pergunta8.c = "No�";
        pergunta8.dica = "Gn 5.27";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Mesmo n�o tentado  por satan�s, o homem, com raras excec�es, se mostrou rebelde. ";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 4.5";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Nos tempos antes do Dil�vio a dieta do homem era baseada em legumes e  frutas. ";
        pergunta10.correta = "V";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "Gn 1.29";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 2;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.ToUpper().Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.ToUpper().Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }

    private void genesis3()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "caim foi com sua esposa para a terra de Node.";
        pergunta1.correta = "V";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 4.16";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Nenhum Homem se  destacou na pr�tica da justi�a de Deus at� o tempo de No�.";
        pergunta2.correta = "F";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 4.4; Gn 5.24; Hb 11.5";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Ad�o n�o teve filha";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 5.4";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Deus quis desistir do ser humano porque, com o tempo, n�o restava sen�o uma unica fam�lia que o temia.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn 6.5 a 8";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Deus decidiu renovar o mundo atrav�s de um Dil�vio.";
        pergunta5.correta = "V";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 6.17";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = " Abel  era Lavrador";
        pergunta6.correta = "B";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "GEN 4:2 E depois deu � luz a seu irm�o Abel. E foi Abel pastor de ovelhas, e Caim foi lavrador da terra.";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Caim  era Lavrador";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "GEN 4:2 E depois deu � luz a seu irm�o Abel. E foi Abel pastor de ovelhas, e Caim foi lavrador da terra.";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "o SENHOR olhou  com agrado a Caim";
        pergunta8.correta = "F";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "GEN 4:4 E Abel trouxe tamb�m dos primog�nitos de suas ovelhas, e de sua gordura. E olhou o SENHOR com agrado a Abel e � sua oferta;";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "o SENHOR olhou com agrado a Abel";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "GEN 4:4 E Abel trouxe tamb�m dos primog�nitos de suas ovelhas, e de sua gordura. E olhou o SENHOR com agrado a Abel e � sua oferta;";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "";
        pergunta10.correta = "";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);
        perguntas.Add(pergunta5);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 3;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }

    private void genesis4()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "No� viveu no meio de um povo bonzinho.";
        pergunta1.correta = "F";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 6.5";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "O Dil�vio foi um ac�mulo de �guas que sa�ram das nuvens e da terra. ";
        pergunta2.correta = "V";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 7.11";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Quantos filhos teve Noe?";
        pergunta3.correta = "A";
        pergunta3.a = "Tr�s";
        pergunta3.b = "Dois";
        pergunta3.c = "Um";
        pergunta3.dica = "Gn 6.10";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "O que Deus ordenou que No� constru�sse?";
        pergunta4.correta = "B";
        pergunta4.a = "Uma grande casa";
        pergunta4.b = "Um grande navio";
        pergunta4.c = "Um grande carro";
        pergunta4.dica = "Gn 6.14";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Quantos pares de cada peixe do mar No� incluiu na Arca?";
        pergunta5.correta = "C";
        pergunta5.a = "Um par";
        pergunta5.b = "Dois pares";
        pergunta5.c = "Nenhum par";
        pergunta5.dica = "Gn 7.2,3";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "No� teve tr�s esposas.";
        pergunta6.correta = "F";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 7.13";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "No� teve um filho de nome Jaf�.";
        pergunta7.correta = "V";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn 6.10";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Quantas pessoas entraram na Arca?";
        pergunta8.correta = "B";
        pergunta8.a = "Quatro";
        pergunta8.b = "Oito";
        pergunta8.c = "Um";
        pergunta8.dica = "Gn 7.13";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "No� n�o precisou buscar os animais que deveriam entrar na Arca porque Deus os fez ir at� ela.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 7.9";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Nenhuma das pessoas daquela gera��o centen�ria entrou na Arca por causa da sua impiedade.";
        pergunta10.correta = "V";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "Gn 7.1";


        Pergunta pergunta11 = new Pergunta();
        pergunta11.pergunta = "O Dil�vio durou trinta dias e trinta noites.";
        pergunta11.correta = "F";
        pergunta11.a = "";
        pergunta11.b = "";
        pergunta11.c = "";
        pergunta11.dica = "Gn 7.12";


        Pergunta pergunta12 = new Pergunta();
        pergunta12.pergunta = "No� j� tinha seissentos anos no dia do Dil�vio.";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "Gn 7.6";


        Pergunta pergunta13 = new Pergunta();
        pergunta13.pergunta = "Depois do Dil�vio as �guas demoraram 150 dias para baixarem.";
        pergunta13.correta = "V";
        pergunta13.a = "";
        pergunta13.b = "";
        pergunta13.c = "";
        pergunta13.dica = "Gn 8.3";


        Pergunta pergunta14 = new Pergunta();
        pergunta14.pergunta = "Deus disse que n�o castigaria mais o ser humano com :";
        pergunta14.correta = "C";
        pergunta14.a = "Fogo";
        pergunta14.b = "Terremoto ";
        pergunta14.c = "Dil�vio ";
        pergunta14.dica = "Gn 8.21";


        Pergunta pergunta15 = new Pergunta();
        pergunta15.pergunta = "";
        pergunta15.correta = "";
        pergunta15.a = "";
        pergunta15.b = "";
        pergunta15.c = "";
        pergunta15.dica = "";


        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);

        perguntas.Add(pergunta11);
        perguntas.Add(pergunta12);
        perguntas.Add(pergunta13);
        perguntas.Add(pergunta14);
        perguntas.Add(pergunta15);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 4;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }

    private void genesis6()
    {

        List<Pergunta> perguntas = new List<Pergunta>();


        Pergunta pergunta = new Pergunta();
        pergunta.pergunta = "Sarai era est�ril; n�o tinha filhos.";
        pergunta.correta = "V";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";
        pergunta.dica = "G�nesis 11:30";


        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Qual � o nome a cidade natal de Abra�o?";
        pergunta1.correta = "A";
        pergunta1.a = "Ur";
        pergunta1.b = "Har�";
        pergunta1.c = " Cana�";
        pergunta1.dica = "Gn 11.31";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Por que Abra�o entrou em Cana�, percorrendo-a de ponta a ponta, e chegou no Egito depois que Deus mostrou-lhe que Cana� era a terra da promessa?";
        pergunta2.correta = "A";
        pergunta2.a = "Porque havia fome em  Cana�.";
        pergunta2.b = "Porque Deus lhe ordenou que visitasse o Egito.";
        pergunta2.c = "Porque n�o era tempo de morar em Cana�.";
        pergunta2.dica = "Gn 12.10";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Abra�o n�o estava sozinho nessa peregrina��o, com ele estavam sua esposa, seus servos e seu sobrinho L�.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 13.1";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Ao sair do Egito, Abra�o voltou para o lugar onde tinha armado sua tenda quando conheceu Cana�.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn  13.3";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Por que houve contenda entre os pastores do gado de Abr�o e os pastores do gado de L�?";
        pergunta5.correta = "B";
        pergunta5.a = "Porque eram de linhagem diferente.";
        pergunta5.b = "Ambos tinham muitos animaiss e bagagem";
        pergunta5.c = "Os pastores de L� n�o queriam viver em Cana�.";
        pergunta5.dica = "Gn 13.6";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Por que L� escolheu morar em Sodoma?";
        pergunta6.correta = "A";
        pergunta6.a = "Porque suas canpinas eram bonitas.";
        pergunta6.b = "Abra�o sugeriu-lhe.";
        pergunta6.c = "Porque ele gostou de seus habitantes.";
        pergunta6.dica = "Gn 13.10,11";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Abra�o  aceitava presente de reis porque timha um pacto com Deus sobre depemder sempre da Sua provis�o.";
        pergunta7.correta = "F";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn 14.22,23";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Deus disse a Abra�o que sua descend�ncia seria como:";
        pergunta8.correta = "C";
        pergunta8.a = "A areia do mar";
        pergunta8.b = "As folhas de uma �rvore";
        pergunta8.c = "As estrelas do c�u";
        pergunta8.dica = "Gn 15.5";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Abra�o foi informado por Deus que sua descend�ncia formaria um povo e herdaria Cana� ap�s cinco gera��es. ";
        pergunta9.correta = "F";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 15.15,16";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Qual � o significado do nome Abra�o ?";
        pergunta10.correta = "B";
        pergunta10.a = "Pai de ima terra que mama leite e mel.";
        pergunta10.b = "Pai de muitas na��es ";
        pergunta10.c = "Pai aben�oado ";
        pergunta10.dica = "Gn 17.5";



        perguntas.Add(pergunta);

        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 6;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }

    private void genesis7()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Abra�o estava preocupado porque era velho e n�o tinha filhos, mas n�o comentou isso com seu amigo, Deus...";
        pergunta1.correta = "F";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Por que Deus acabou se agradando de Agar ter o filho de Abra�o?";
        pergunta2.correta = "B";
        pergunta2.a = "Porque Sara n�o teve f�";
        pergunta2.b = "Porque Sara a afligiu.";
        pergunta2.c = "Porque Abra�o n�o teve f�.";
        pergunta2.dica = "Gn 16.6, 8,11";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "A circuncis�o foi um sinal de alian�a para a forma��o da na��o gerada em Abra�o, que entendeu isso mais que Mois�s.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 17. 10, 23; �x 4.24,25";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "A atitude de Abra�o com os anjos  que foram anunciar o filho herdeiro mostrou:";
        pergunta4.correta = "B";
        pergunta4.a = "Cordialidade rec�proca ";
        pergunta4.b = "Admira��o a Deus";
        pergunta4.c = "Etiqueta social";
        pergunta4.dica = "Gn 18.3,17, 19";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "A intercess�o de Abra�o por Sodoma no di�logo com Deus mostra:";
        pergunta5.correta = "B";
        pergunta5.a = "Descontra��o ";
        pergunta5.b = "Rever�ncia ";
        pergunta5.c = "Euforia";
        pergunta5.dica = "Gn 18.27";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Deus era t�o amigo de Abra�o que a sua esposa de homens poderosos. Mas Abra�o n�o mentia diante deles quando dizia, por precau��o, que Sara era sua irm�.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 20.2, 3, 12";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Quantos anos tinha Abra�o quando Isaac, filho da promessa, nasceu?";
        pergunta7.correta = "A";
        pergunta7.a = "100 anos";
        pergunta7.b = "150 anos";
        pergunta7.c = "90 anos";
        pergunta7.dica = "Gn 21.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Depois de tanto tempo tendo Deus como amigo, Abra�o confiou  que Deus n�o deixaria ele sacrificar seu filho.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 22.5,8";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Como Abra�o quis que seu servo lhe garantisse que buscaria uma esposa para Isaque conforme ele orientou?";
        pergunta9.correta = "B";
        pergunta9.a = "Assimando um documento";
        pergunta9.b = "Recuperando dados. Aguarde alguns segundos e tente cortar ou copiar novamente.";
        pergunta9.c = "Dando um diamante ao servo ";
        pergunta9.dica = "Gn 24.2,3";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Abra�o morreu j� em ditosa velhice com:";
        pergunta10.correta = "A";
        pergunta10.a = "175 anos de idade";
        pergunta10.b = "150 anos de idade";
        pergunta10.c = "180 anos de idade";
        pergunta10.dica = "Gn 25.7";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 7;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }


    }


    private void genesis9()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "O nome Jac� (Yacov), que  quer dizer segurava o calcanhar tem a ver com  o modo como  ele veio � luz.";
        pergunta1.correta = "V";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 25.26";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Jac� teve seu nome mudado, por Deus para Ismael, por isso a confedera��o  que as tribos que filhos e netos dele formaram  foi chamada Ismael.";
        pergunta2.correta = "F";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 35.10";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Raquel era a preferida de Jac�. Dela nasceu a maioria dos filhos que formaram as tribos de Israel. ";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 35.24";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Por que Jac� fez uma reforma espiritual dentro da fam�lia depois que deixou a casa do seu sogro?";
        pergunta4.correta = "A";
        pergunta4.a = "Para nenhum tipo de idolatria tirar Deus do centro da vida deles.";
        pergunta4.b = "Porque Raquel  lhe pediu isso.";
        pergunta4.c = "Todas as anteriores";
        pergunta4.dica = "Gn 35.4";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Depois de toda a afli��o que Lab�o causou a Jac�, ele se humilhou e fez um pacto de paz com Jac�.";
        pergunta5.correta = "V";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 31.44";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Dentre os descendentes de Esa� estiveram os amalequitas e os edonitas.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 36.8, 12";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O filho preferido de Jac� era: ";
        pergunta7.correta = "B";
        pergunta7.a = "Jud�";
        pergunta7.b = "Jos�";
        pergunta7.c = "Benjamim";
        pergunta7.dica = "Gn 37.3";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Os irm�o de Jos� o invejaram pelo sonho de revela��o que teve. mas Jac�, mesmo achando estranho o sonho, apenas guardou a d�vida  no cora��o .";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 37.11";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Jac� pensou que um animal havia despeda�ado Jos�, e ficou  muitos anos sem ele.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 37.33";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Passados muitos anos, quando reencontrou-se com jos�, Jac� respondei ao rei do Egito:";
        pergunta10.correta = "B";
        pergunta10.a = "Obrigado por perguntar a minha idade. ";
        pergunta10.b = "...foram poucos e maus os dias dos anos da minha peregrina��o";
        pergunta10.c = "Vivi muito sobre a face da terra.";
        pergunta10.dica = "Gn 47.9";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 9;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }



    private void PERGUNTASMODELO()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "";
        pergunta1.correta = "";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "";
        pergunta2.correta = "";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "";
        pergunta3.correta = "";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "";
        pergunta4.correta = "";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "";
        pergunta5.correta = "";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "";
        pergunta6.correta = "";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "";
        pergunta7.correta = "";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "";
        pergunta8.correta = "";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "";
        pergunta9.correta = "";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "";
        pergunta10.correta = "";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 1;
            if (!perguntaDotema.pergunta.Equals(""))
            {
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }


    private void genesis10()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta12 = new Pergunta();
        pergunta12.pergunta = "        Quando Jos� tinha dezessete anos, pastoreava os rebanhos com os seus irm�os";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "G�nesis 37:2";



        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Jos� nasceu de:";
        pergunta1.correta = "C";
        pergunta1.a = "Zilpa";
        pergunta1.b = "Bila";
        pergunta1.c = "Raquel";
        pergunta1.dica = "Gen. 46.19";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "O zelo de Jos� pela justi�a fez com que ele fosse visto como o que vulgarmente chamam, hoje, em nossa cultura, de:";
        pergunta2.correta = "C";
        pergunta2.a = "Descolado";
        pergunta2.b = "De boa";
        pergunta2.c = "Dedo duro";
        pergunta2.dica = "Gn 37.2";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Os sonhos de Jos� eram: ";
        pergunta3.correta = "B";
        pergunta3.a = "Desejos de vencer na vida ";
        pergunta3.b = "Revela��es de Deus";
        pergunta3.c = "Preocupa��es ";
        pergunta3.dica = "Gn 37.9; 42.9";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Para quem os irm�os de Jos� o venderam?";
        pergunta4.correta = "A";
        pergunta4.a = "Para caravaneiros comerciantes que compraram Jos� e o venderam no Egito.";
        pergunta4.b = "Para o rei do Egito";
        pergunta4.c = "Para os filisteus";
        pergunta4.dica = "Gn 37.36";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Por que o patr�o de Jos� prosperou enquanto Jos� trabalhou em sua casa?";
        pergunta5.correta = "A";
        pergunta5.a = "Porque Deus fez isso por amor a Jos�.";
        pergunta5.b = "Porque Jos� tinha capacidade de administrar. ";
        pergunta5.c = "Porque Jos� interpretou seus sonhos.";
        pergunta5.dica = "Gn 39.5";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O patr�o de Jos�  gostava muito dele, mas fiscalizava  tudo o que ele fazia.";
        pergunta6.correta = "F";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 39.6";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Por que Jos� rejeitou os encantos da esposa do seu patr�o? ";
        pergunta7.correta = "C";
        pergunta7.a = "Por tenor a Deus";
        pergunta7.b = "Porque pensou no seu futuro";
        pergunta7.c = "Por que lhe doeu trair seu senhor terreno e seu Senhor  supremo, Deus.";
        pergunta7.dica = "Gn 39.9";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Jos� era t�o amado por Deus que mesmo na pris�o Deus o fez administrar.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 39.22";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "quantos sonhos Jos� interpretou na pris�o ?";
        pergunta9.correta = "B";
        pergunta9.a = "Um";
        pergunta9.b = "Dois";
        pergunta9.c = "Tr�s";
        pergunta9.dica = "Gn  40.5";







        perguntas.Add(pergunta12);

        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        //   perguntas.Add(pergunta10);


        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 10;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }


    private void genesis8()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "O nome Isaque tem a ver com riso e sorriso.";
        pergunta1.correta = "V";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "";


        Pergunta pergunta12 = new Pergunta();
        pergunta12.pergunta = "Isaque foi mandado embora da terra onde estava, pelo rei Abimeleque?";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "Gn 26.14-23";

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "que outro desgosto lhe sobreveio em seguida por inveja dos homens?";
        pergunta2.correta = "B";
        pergunta2.a = "Quiseram roubar seu gado?";
        pergunta2.b = "Reivindicaram para eles os po�os que seus pastores  cavaram.";
        pergunta2.c = "Jogaram pedras nele";
        pergunta2.dica = "Gn 26.14-23";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Deus visitou Isaque e o confortou da afli��o que os homens lhe causaram.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Havia naquele tempo pessoas   poderosas  sensatas ao reconhecerem a ben��o de Deus sobre Isaque.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Isaque e sua esposa, Rebeca, n�o gostaram das mulheres que qual filho escolheu?";
        pergunta5.correta = "A";
        pergunta5.a = "Esa�";
        pergunta5.b = "Jac�";
        pergunta5.c = "No�";
        pergunta5.dica = "Gn 26.35";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Naquela �poca n�o existia �culos. Grande parte das pessoas muito idosas, como Isaque quando envelheceu, j� n�o enxergava mais.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Para preparar-se para dar a ben��o da primogenitura a Esa�, Isaque pediu que ele lhe servisse: ";
        pergunta7.correta = "A";
        pergunta7.a = "Carne cozida ";
        pergunta7.b = " Um churrasco";
        pergunta7.c = "Uma sopa";
        pergunta7.dica = "Gn 27.3,.4";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Isaque esqueceu-se de orientar Jac� a n�o casar-se com mulher Canan�ia.";
        pergunta8.correta = "F";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Passados muitos anos Isaque viu seus filhos novamente j� com seus netos.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = " Isaque morrei em ditosa velhice aos:";
        pergunta10.correta = "C";
        pergunta10.a = "110 anos ds idade";
        pergunta10.b = "140";
        pergunta10.c = "180 anos de idade";
        pergunta10.dica = "";




        perguntas.Add(pergunta1);
        perguntas.Add(pergunta12);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 8;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }



    private void genesis5()
    {

        List<Pergunta> perguntas = new List<Pergunta>();


        Pergunta pergunta = new Pergunta();
        pergunta.pergunta = "Depois que saiu da Arca, No� criou animais.";
        pergunta.correta = "F";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";
        pergunta.dica = "Gn 9.20";


        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "A B�blia diz que No� plantou uma:";
        pergunta1.correta = "C";
        pergunta1.a = "Figueira ";
        pergunta1.b = "Tamareira";
        pergunta1.c = "Vinha";
        pergunta1.dica = "Gn 9.20";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Deus permitiu  que a carne fosse comida com sangue do animal.";
        pergunta2.correta = "F";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 9.4";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Quantos anos No� viveu?";
        pergunta3.correta = "A";
        pergunta3.a = "650 anos ";
        pergunta3.b = "120 anos";
        pergunta3.c = "80 anos";
        pergunta3.dica = "Gn 9.29";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "No� amaldi�oou Cam ou Cana�?";
        pergunta4.correta = "B";
        pergunta4.a = "Cam";
        pergunta4.b = "Cana�";
        pergunta4.c = "Os dois";
        pergunta4.dica = "Gn 9.25";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Antes do Dil�vio os animais n�o viviam pacificamente com o ser humano.";
        pergunta5.correta = "F";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 9.2";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O povo africano n�o descende de Cana�, mas sim os cananeus.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 10.15-19";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O lugar onde a gera��o da Torre de Babel recebeu o nome de Babel porque:";
        pergunta7.correta = "C";
        pergunta7.a = "Eram pecadores";
        pergunta7.b = "Eram n�mades";
        pergunta7.c = "Ali Deus Dividiu as l�nguas";
        pergunta7.dica = "Gn 11.9";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Os judeus (israelitas) s�o descendentes de Sem.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "A cidade de N�nive foi edificada por um descendente de Cam.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Qual dessas tr�s coisas Deus viu necess�rilo criar regra de proibi��o para ela logo ap�s o Dil�vio:";
        pergunta10.correta = "C";
        pergunta10.a = "O furto ";
        pergunta10.b = "A idolatria ";
        pergunta10.c = "Qualquer tipo de homic�dio.";
        pergunta10.dica = "Gn. 9.6";



        perguntas.Add(pergunta);

        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);
     
        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 5;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }



    }

    private void genesis1()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Os dias da cria��o foram divididos por dois per�odos: tarde e manh�.";
        pergunta1.correta = "A";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 1.5; 1.8; 1.13; 1.19;  1.23; 1.31.";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Deus fez o sol para marcar o per�odo di�rno.";
        pergunta2.correta = "A";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = " Gn 1.16";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "A lua � chamada de luminar maior.";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 1.16";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Antes de ser criada a fauna e a flora a terra estava submersa pelas �guas.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn 1.9";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "O homem foi criado no sexto dia.";
        pergunta5.correta = "F";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 1.26";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Deus criou o homem  com mais algu�m.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 1.26; Jo 1.3.";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O trabalho do homem era cultivar a terra do jardim e proteg�-lo";
        pergunta7.correta = "V";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn  2.15";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Deus fez a mulher para auxiliar o homem como companheira dele.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 2.18 (cf. Almeida Revista e Corrigida)";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "O homem foi seduzido pela serpente.";
        pergunta9.correta = "F";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 3.1";


        Pergunta pergunta12 = new Pergunta();
        pergunta12.pergunta = "Ad�o deu � sua mulher o nome de Eva, pois ela seria m�e de toda a humanidade.";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "G�nesis 3:20";

        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Deus colocou dois guardi�es na entrada do caminho que levava at� a �rvore da vida.";
        pergunta10.correta = "V";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "Gn 3.22 a 24";


        Pergunta pergunta11 = new Pergunta();
        pergunta11.pergunta = "para que o homem n�o comesse mais do seu fruto e conseguisse viver para sempre.";
        pergunta11.correta = "V";
        pergunta11.a = "";
        pergunta11.b = "";
        pergunta11.c = "";
        pergunta11.dica = "Gn 3.22 a 24";


      


        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta12);
        perguntas.Add(pergunta10);
        perguntas.Add(pergunta11);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 1;
            if (!perguntaDotema.pergunta.Equals(""))
            {

                if (perguntaDotema.correta.Equals("V"))
                {
                    perguntaDotema.correta = "A";

                }
                else if (perguntaDotema.correta.Equals("F"))
                {
                    perguntaDotema.correta = "B";

                }
                perguntasGerais.Add(perguntaDotema);

            }
        }


    }


    private void tema6()
    {


        List<Pergunta> perguntas = new List<Pergunta>();


        Pergunta pergunta = new Pergunta();
        pergunta.pergunta = "Zacarias era um Sacerdote";
        pergunta.correta = "A";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Mesmo mudo,Zacarias enfrentou a multid�o para registrar seu filho ( v 62,63)";
        pergunta1.correta = "A";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Ao voltar a falar Zacarias manifestou toda sua Raiva (v 68)";
        pergunta2.correta = "B";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Ao voltar a falar Zacarias manifestou toda sua Alegria (v 68)";
        pergunta3.correta = "A";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = " Zacarias tinha f� que a Paz ia chegar (v 78)";
        pergunta4.correta = "A";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";


        perguntas.Add(pergunta);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);

        //perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 6;
        }

        perguntasGerais.AddRange(perguntas);



    }

    private void tema5()
    {



        List<Pergunta> perguntas = new List<Pergunta>();


        Pergunta pergunta = new Pergunta();
        pergunta.pergunta = "";
        pergunta.correta = "";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";

        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Maria ficou sabendo sobre Isabel 12 meses depois do encontro de Zacarias e Isabel  ";
        pergunta1.correta = "B";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Maria se aprontou e foi depressa para uma cidade que ficava na regi�o montanhosa da Judeia. ";
        pergunta2.correta = "A";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";

        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Maria resolveu seus problemas em casa antes de  visitar Isabel";
        pergunta3.correta = "B";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";

        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Isabel e Maria eram parentes ";
        pergunta4.correta = "A";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";

        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Quando Isabel ouviu a sauda��o de Maria, o que aconteceu? ";
        pergunta5.correta = "A";
        pergunta5.a = "Jo�o Batista se mexeu ";
        pergunta5.b = "Jesus se mexeu ";
        pergunta5.c = "ela ficou com inveja ";

        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O que Isabel fez quando ficou cheio do ES";
        pergunta6.correta = "B";
        pergunta6.a = "Falou em Linguas";
        pergunta6.b = "Falou belas palavras";
        pergunta6.c = "Profetizou";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Ent�o disse Maria: Minha alma engrandece ao Senhor";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Maria continuou :pois o Poderoso fez grandes coisas em meu favor; ";
        pergunta8.correta = "A";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Maria ficou com Isabel cerca de dois meses e depois voltou para casa.";
        pergunta9.correta = "B";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";



        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        //perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 5;
        }

        perguntasGerais.AddRange(perguntas);


    }
    private void tema4()
    {



        List<Pergunta> perguntas = new List<Pergunta>();


        Pergunta pergunta = new Pergunta();
        pergunta.pergunta = "Foi o anjo Miguel que anunciou o nascimento de Jesus";
        pergunta.correta = "B";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";









        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Em qual  cidade  o anjo Gabriel encontrou Maria ?";
        pergunta1.correta = "C";
        pergunta1.a = "Natal";
        pergunta1.b = "Bel�m";
        pergunta1.c = "Nazar�";

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Maria era muito velha quando Deus a escolheu ";
        pergunta2.correta = "B";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";

        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Maria tinha uma profiss�o muito importante";
        pergunta3.correta = "B";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";

        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Maria  estava noiva de Jos�. ";
        pergunta4.correta = "A";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";

        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Foi Maria que escolheu o nome de Jesus  ";
        pergunta5.correta = "B";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";

        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Foi Deus que escolheu o nome de Jesus";
        pergunta6.correta = "A";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Jesus significa Deus � a Salva��o";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Maria acreditou que ia ficar Gr�vida";
        pergunta8.correta = "A";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";

        //Pergunta pergunta9 = new Pergunta();
        //pergunta.pergunta = "Maria era muito velha quando Deus a escolheu ";
        //pergunta.correta = "B";
        //pergunta.a = "";
        //pergunta.b = "";
        //pergunta.c = "";

        //Pergunta pergunta10 = new Pergunta();
        //pergunta.pergunta = "Maria era muito velha quando Deus a escolheu ";
        //pergunta.correta = "B";
        //pergunta.a = "";
        //pergunta.b = "";
        //pergunta.c = "";

        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        //perguntas.Add(pergunta9);
        //perguntas.Add(pergunta10);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 4;
        }

        perguntasGerais.AddRange(perguntas);


    }

    private void tema3()
    {




        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta perguntaModelo = new Pergunta("Jo�o Batista foi o maior ser humano dentre os nascidos de mulher ? ", "A", "", null, null);
        perguntaModelo.dica = "(Lucas 7:28) 28 Porque eu vos digo, que dentre os nascidos de mulheres, n�o h� maior profeta que Jo�o o Batista; mas o menor no Reino dos c�us � maior que ele. ";

        Pergunta pergunta2 = new Pergunta("Zacarias era o nome do Pai de Jo�o Batista", "A", "", "", null,"2 Sendo An�s e Caif�s os sumos sacerdotes, foi feita a palavra de Deus a Jo�o, filho de Zacarias, no deserto.  ");
        Pergunta pergunta3 = new Pergunta("O nome da m�e de Jo�o Batista era Maria", "B", "", null, null,"5 Houve nos dias de Herodes, rei da Judeia, um sacerdote, por nome Zacarias, da ordem de Abias; e sua mulher das filhas de Ar�o, e [era] seu nome Isabel. ");
        Pergunta pergunta4 = new Pergunta("Como era a vida do casal ?", "A", "Correta", "bagun�a", "Errada", "6 E eram ambos justos diante de Deus, andando em todos os mandamentos e preceitos do Senhor sem repreens�o. ");
        Pergunta pergunta5 = new Pergunta("Zacarias e Isabel eram Jovens ", "B", "", "", "", "7 E n�o tinham filhos, porque Isabel era est�ril, e ambos tinham muitos anos de vida. ");
        Pergunta pergunta6 = new Pergunta("Zacarias sentiu medo ao ver o Anjo", "A", "", "", "", "12 E Zacarias vendo [-o] , ficou perturbado, e caiu medo sobre ele. ");
        Pergunta pergunta7 = new Pergunta("O que jo�o n�o podia Beber", "B", "Refrigerante", "Vinho", "�gua", "15 Porque [ele] ser� grande diante do Senhor, e n�o beber� vinho, nem bebida alco�lica, e ser� cheio do Esp�rito Santo, at� desde o ventre de sua m�e. ");

        Pergunta pergunta8 = new Pergunta("Qual foi a fun��o de Jo�o Batista", "B", "Levita", "Precursor do Messias", "Pastor");
        Pergunta pergunta9 = new Pergunta("Gabriel era o nome do Anjo que apareceu para Zacarias", "A", "", "", "", "19 E respondendo o anjo, disse-lhe: Eu sou Gabriel, que fico presente diante de Deus, e fui mandado para falar a ti, e para te dar estas boas not�cias. ");
        Pergunta pergunta10 = new Pergunta("Zacarias ficou ____ porque n�o acreditou no anjo", "mudo", "", "", "", "20 E eis que tu ficar�s mudo, e n�o poder�s falar, at� o dia em que estas coisas aconte�am, porque n�o creste nas minhas palavras, que se cumprir�o a seu tempo. ");
        Pergunta pergunta11 = new Pergunta("O que aconteceu com Isabel pouco tempo depois ?", "C", "Muda", "Triste", "Gr�vida");



        perguntas.Add(perguntaModelo);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);
        perguntas.Add(pergunta8);
        perguntas.Add(pergunta9);
        perguntas.Add(pergunta10);
        perguntas.Add(pergunta11);



        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 3;
        }



        perguntasGerais.AddRange(perguntas);


    }


    private void tema2()
    {

        List<Pergunta> perguntas = new List<Pergunta>();




        Pergunta pergunta1 = new Pergunta("Evangelho significa Boas Not�cias?", "A", "", "", "Sim, voc� pode fazer nosso curso de grego");

        Pergunta pergunta2 = new Pergunta("Para que serve um evangelho   ?", "B", "Para salvar todas as pessoas", "para salvar todos os que creem", "Para contar uma est�ria");
        pergunta2.dica = "Porque n�o me envergonho do Evangelho de Cristo, pois � o poder de Deus para a salva��o de todo aquele que cr�... (Romanos 1. 16)";

        Pergunta pergunta3 = new Pergunta("Os  livros de Lucas e Atos foram escritos entre os anos 59 e 63 d.C.", "A", "", null, null,"SIM");


        Pergunta pergunta4 = new Pergunta("O Evangelho de Lucas foi escrito em grego.", "A", "", null, null,"Simn no nosso curso de grego voC� vai ler direto do original");


        Pergunta pergunta5 = new Pergunta("Os evangelhos de Mateus, Marcos, Tiago e Jo�o s�o os evangelhos que est�o na B�blia ?", "B", "", "", "");
        pergunta5.dica = "Os evangelhos s�o os 4 primeiros livro do Novo testamento. Pode ver na sua B�blia :)";


        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);


        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 2;
        }



        perguntasGerais.AddRange(perguntas);


    }

    private void tema1()
    {

        List<Pergunta> perguntas = new List<Pergunta>();

        Pergunta perguntaModelo = new Pergunta("", "A", "", null, null);

        Pergunta pergunta1 = new Pergunta("Lucas escreveu o evangelho de Lucas e o livro de Atos ", "A", "", null, null);
        pergunta1.dica = "(Atos 1.1)  Eu fiz o primeiro livro, � Te�filo, sobre todas as coisas que Jesus come�ou, tanto a fazer como a ensinar; )";
        Pergunta pergunta2 = new Pergunta("Qual foi a principal profiss�o de Lucas  ?", "A", "Medico", "Veterin�rio", "Pintor");
        pergunta2.dica = "(Colossenses 4:14 14 Lucas, o m�dico amado, ele vos sa�da;)";
        Pergunta pergunta3 = new Pergunta("Seu nome aparece tr�s vezes  no N.T. ", "A", "", null, null);
        pergunta3.dica = "(Cl 4.14 - 2 Tm 4.11 - Fm 24)";
        Pergunta pergunta4 = new Pergunta("Lucas participou de Miss�es com o ap�stolo Paulo para pregar o evangelho  ?", "A", "", null, null);
        pergunta4.dica = " (2 Tim�teo 4:11) 11 para o qual eu fui posto como pregador e ap�stolo, e instrutor dos gentios. ";
        Pergunta pergunta5 = new Pergunta("O que Lucas fez para escrever o Evangelho  ?", "B", "Inventou", "Investigou", "Copiou", "3 Pareceu-me bom que tamb�m eu, que tenho me informado com exatid�o desde o princ�pio, escrevesse [estas coisas] em ordem para ti, excelent�ssimo Te�filo, ");
        Pergunta pergunta6 = new Pergunta("Para quem Lucas escreveu ?", "C", "Alexandre", "Herodes", "Te�filo", "3 Pareceu-me bom que tamb�m eu, que tenho me informado com exatid�o desde o princ�pio, escrevesse [estas coisas] em ordem para ti, excelent�ssimo Te�filo, ");
       
        Pergunta pergunta7 = new Pergunta("O que Lucas Contou ?", "B", "Mentiras sobre Jesus", "Verdade sobre Jesus", "Lendas sobre Jesus", "4 Para que conhe�as a certeza das coisas de que foste ensinado. ");


        perguntas.Add(pergunta1);
        perguntas.Add(pergunta2);
        perguntas.Add(pergunta3);
        perguntas.Add(pergunta4);
        perguntas.Add(pergunta5);
        perguntas.Add(pergunta6);
        perguntas.Add(pergunta7);

        foreach (Pergunta perguntaDotema in perguntas)
        {
            perguntaDotema.tema = 1;
        }



        perguntasGerais.AddRange(perguntas);


    }


    public void jogarFase(int idCena)
    {

        qtdCorretasnafase = 0;
        if (idCena == 0)
        {

            idCena = GameManager.Instance.ultimafase;

            print(" tentando jogar" + idCena);

        }

        int denarios = PlayerPrefs.GetInt("denarios", 0);
        Tema tema = GameManager.Instance.temas.Find(temaAchar => temaAchar.id == idCena.ToString());
        string denariosNecessario = tema.denarios;
        int v = System.Convert.ToInt32(denariosNecessario);
        GameManager.Instance.ultimafase = idCena;


        if (idCena != 0 && v <= denarios)
        {

            print("tem denatio");
            GameManager.Instance.carregarTema();
            PlayerPrefs.SetString("nomeTema", tema.nome);
            PlayerPrefs.SetString("estudoEscolhido", estudoEscolhido);
            PlayerPrefs.SetInt("ultimafase", ultimafase);


            SceneManager.LoadScene("1");
        }
        else
        {

            PlayerPrefs.SetInt("ultimafase", ultimafase);

            print(" nao tem denatio");
            SceneManager.LoadScene("introducaoFase");


        }


    }

    public void carregarTemasMateus()
    {
        Tema tema1 = new Tema("1", "Crescimento", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        tema1.mensagemInicial = "Ol� meu nome � Mateus, o filho de Alfeu, eu era coletor de impostos, e me chamava Levi, mas  minha vida mudou para sempre quando atendi ao chamado de Jesus Cristo.  Agora Sou um dos Doze Ap�stolos, por isso vou te contar tudo que vivi com Jesus. Mas antes vou te mostrar Como Jesus cumpre a profecia para ser o Messias.";


        Tema tema2 = new Tema("2", "Ensinamentos", "", "Agora que sabemos por que Jesus � o Messias, Vou contar alguns de seus ensinamentos que foram revolucion�rios e v�o durar para sempre", "", "20");

        Tema tema3 = new Tema("3", "Miss�es", "", " Jesus al�m de ensinar com palavras, ensinava com a��es Veja alguma de suas miss�es", "", "50");

        Tema tema4 = new Tema("4", "Par�bolas", "", "As par�bolas de jesus s�o marcantes, consegue nos ensinar de forma f�cil assuntos complexos", "", "80");



        Tema tema5 = new Tema("5", "Seguindo Jesus", "", "Seguir Jesus n�o � f�cil, alguns se perdem , outros  se desviam mas voltam, vou te contar algumas hist�rias de seus seguidores", "", "120");


        Tema tema6 = new Tema("6", "condena��o", "", "Jesus cumpriu toda a escritura , inclusive o sofrimento que havia na profecia", "", "170");


        Tema tema7 = new Tema("7", "ressurrei��o ", "", "Mas o melhor  � que ele ressuscitou , agora se prepare para a maior acontecimento de todos os tempos a ressurei��o de Jesus", "", "200");


        //Tema tema4 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema5 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema6 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        temas.Add(tema1);
        temas.Add(tema2);
        temas.Add(tema3);
        temas.Add(tema4);
        temas.Add(tema5);
        temas.Add(tema6);
        temas.Add(tema7);




    }
    public void carregarTemasOffLine()
    {
        Tema tema1 = new Tema("1", "Quem � Lucas ?", "https://www.youtube.com/watch?v=7SeGSVHUm-M&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=1", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        tema1.mensagemInicial = "Ol�, meu nome � Lucas, sou o m�dico que estava com o ap�stolo paulo." +
"Preciso entregar uma carta para Te�filo." + "Nessa carta vou escrever as hist�rias que est�o falando sobre Jesus." +
"Vou contar o evangelho Mas para isso preciso fazer uma grande investiga��o," +
"falar com as pessoas que viram Jesus, saber exatamente a verdade. (Lucas 1 :1 - 4) Voc� pode me ajudar nessa grande miss�o ?";


        Tema tema2 = new Tema("2", "O que � um evangelho ?", "https://www.youtube.com/watch?v=7SeGSVHUm-M&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=1", "Muito bom, agora que nos conhecemos, precisamos entender o que vamos escrever, n�o ser� uma carta comum, ser� um evangelho, !!!  Vamos entender o que � isso  ", "", "7");

        Tema tema3 = new Tema("3", "Jo�o Batista", "https://www.youtube.com/watch?v=HTO4wokqrtw&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=2", " Fiquei sabendo que antes de Jesus, nasceu um homem,Jo�o Batista, que teve a miss�o de preparar o caminho para o messias, vamos conhec�-lo!!!", "", "13");

        Tema tema4 = new Tema("4", "Maria m�e de Jesus", "https://www.youtube.com/watch?v=lzTjkitye8w&t=1s", "Muito bem, conhecemos Jo�o o precursor de Jesus, agora vamos conhecer uma mulher que foi agraciada por Deus, A m�e de Jesus", "", "200");



        Tema tema5 = new Tema("5", "Maria visita Isabel", "https://www.youtube.com/watch?v=60gPW7VqE9U&t=1s", "Fiquei sabendo que Maria e ISABEL SE CONHECIAM,  SER� QUE ERAM AMIGAS? Vamos investigar isso", "", "50");


        Tema tema6 = new Tema("6", "O c�ntico de Zacarias (1 : 57-80)", "https://www.youtube.com/watch?v=u2aPzf8opsg", "Que legal essa amizade de Maria e Isabel, e Zacarias ser� que estava feliz com o nascimento de seu filho", "", "50");


        Tema tema7 = new Tema("7", "Nascimento de Jesus", "https://www.youtube.com/watch?v=u2aPzf8opsg", "(Lucas 2 : 1-7)", "", "80");


        //Tema tema4 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema5 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema6 = new Tema("1", "Quem � Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        temas.Add(tema1);
        temas.Add(tema2);
        temas.Add(tema3);
        temas.Add(tema4);
        temas.Add(tema5);
        temas.Add(tema6);
        temas.Add(tema7);




    }

    public void pegarTemasNoFirebase(string idioma,string estudo)
    {



        print("pegando  tema " + estudo);
 
        FirebaseDatabase.DefaultInstance
  .GetReference(idioma).Child(estudo).Child("fases").GetValueAsync().ContinueWith(task =>
     {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...

          int contador = 1;





          foreach (var filhos in snapshot.Children)
          {


              print("pegsando tema "+ estudo + contador);


              string f = filhos.GetRawJsonValue();






              Tema tema = JsonUtility.FromJson<Tema>(f);

              temas.Add(tema);

              print(tema.nome);





          }
             contador = contador + 1;
             print("pode chmar LoadScene");







         }
     });
    }



    public void pegarEstudosNoFirebase(string idioma)
    {



        print("pegando  estudos " );

        FirebaseDatabase.DefaultInstance
  .GetReference(idioma).Child("estudos").GetValueAsync().ContinueWith(task =>
  {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...

          int contador = 1;







          foreach (var filhos in snapshot.Children)
          {


              print(filhos);



              string f = filhos.GetRawJsonValue();






              Estudo estudo = JsonUtility.FromJson<Estudo>(f);

              estudos.Add(estudo.nome);

              print(estudo);





          }
          contador = contador + 1;
          print("pode chmar estudos");





      }
  });
    }


    public void pegarPerguntasDaFaseNofirebase(string fase)
    {

        print("Buscando perguntas firebase" + "pt/"+estudoEscolhido +" "+ fase);


  //      perguntasGerais.Clear();
        int idCena = PlayerPrefs.GetInt("idTema");

  

        FirebaseDatabase.DefaultInstance.GetReference("pt").Child(estudoEscolhido).Child("perguntas").Child("fase"+fase)
  .GetValueAsync().ContinueWith(task =>
  {
      if (task.IsFaulted)
      {
          // Handle the error...
      }
      else if (task.IsCompleted)
      {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...



          print("antes do for");


         int tema= System.Convert.ToInt32(fase);




          for (int i = 1; i < snapshot.ChildrenCount+1; i++)
          {
              
              
              var temas = snapshot.Child(i.ToString());
              
              print("entrouNoFOrTema " + snapshot.GetRawJsonValue());

             
           


                  print("key" + temas.Key);
                  print("value" + temas.Value);

              string f = temas.GetRawJsonValue();






                  Pergunta pergunta = JsonUtility.FromJson<Pergunta>(f);
                  pergunta.tema = tema;



              if (pergunta.correta.ToUpper().Equals("V"))
              {
                  pergunta.correta = "A";

              }
              else if (pergunta.correta.ToUpper().Equals("F"))
              {
                  pergunta.correta = "B";

              }





              perguntasGerais.Add(pergunta);

                  print(pergunta.pergunta);

                  print(pergunta.correta);
                  print(pergunta.tema);



              
          }



         




      }
  });
    }

    public void carregarTema()
    {




        int idCena = GameManager.Instance.ultimafase;
    //    pegarPerguntasDaFaseNofirebase("pt",estudoEscolhido,idCena.ToString());



        print("CarregandoTema " + idCena);






        List<Pergunta> query = perguntasGerais.FindAll(fruit => fruit.tema == idCena);



        print("query" + query.Count);

        bdPerguntas = new string[query.Count];

        correta = new string[query.Count];

        alternativasA = new string[query.Count];
        alternativasB = new string[query.Count];
        alternativasC = new string[query.Count];
        alternativasD = new string[query.Count];


        int contador = 0;
        foreach (Pergunta filhos in query)
        {

            bdPerguntas[contador] = filhos.pergunta.Trim();
            correta[contador] = filhos.correta.Trim();


            

           alternativasA[contador] = filhos.a == null ? "" : filhos.a.Trim();
            alternativasB[contador] = filhos.b == null ? "" : filhos.b.Trim();
            alternativasC[contador] = filhos.c == null ? "" : filhos.c.Trim();
            alternativasD[contador] = filhos.dica == null ? "" : filhos.dica.Trim();


            print("CarregandoTema " + filhos.pergunta);

            contador = contador + 1;

        }




    }

    void InicializarBD()
    {

        print("inicandoBD");
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith  (task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.

                dataBaseRefence = FirebaseDatabase.DefaultInstance.RootReference;

                print("Logou database");

                //app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {


                print("nao passou db");

                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });


    }




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


            //     FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://aventuras-da-bblia-jp-96278862-default-rtdb.firebaseio.com/");

            // dataBaseRefence = FirebaseDatabase.DefaultInstance.RootReference;

            //      InicializarBD();
            //Reis

        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void sendTOLeaderboard()
    {
        //    PlayFabManager.Instance.SendLeadraboard(30);
    }

    public string getDenarios()
    {
        return PlayerPrefs.GetInt("denarios").ToString();

    }
}





