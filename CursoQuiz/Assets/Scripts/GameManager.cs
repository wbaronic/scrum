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

    //Fases:		Questões Objetivas, Múltipla escolha, Verdadeiro ou falso e Complete:		Respostas:		Alternativa A		Alternativa B		Alternativa C		Versículos:
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

        Pergunta pergunta1 = new Pergunta(1, "	1- Livro da geração de Jesus Cristo, filho de Davi, filho de : ", "	Abraão	", "		", "		", "		", "	1- Livro da geração de Jesus Cristo, filho de Davi, filho de Abraão. Mateus 1:1	");
        Pergunta pergunta2 = new Pergunta(1, "	2- Quem foi o pai do rei Davi?	", "	C	", "	Salomão	", "	Salmom	", "	Jessé	", "	E Jessé gerou ao rei Davi; e o rei Davi gerou a Salomão da que foi mulher de Urias.CustoMateus 1:6	");
        Pergunta pergunta3 = new Pergunta(1, "	3- Jeconias gerou a Salatiel, Salatiel gerou a Zorobabel e Zorobabel gerou a ______________ .	", "	Abiúde	", "		", "		", "		", "	E Zorobabel gerou a Abiúde; e Abiúde gerou a Eliaquim; e Eliaquim gerou a Azor;CustoCustoMateus 1:13	");
        Pergunta pergunta4 = new Pergunta(1, "	4- Qual o nome do sogro de Maria?	", "	Jacó	", "		", "		", "		", "	E Jacó gerou a José, marido de Maria, da qual nasceu JESUS, que se chama o Cristo.CustoCustoMateus 1:16	");
        Pergunta pergunta5 = new Pergunta(1, "	5- Quantas gerações são de Abraão até Davi?	", "	Quatorze	", "		", "		", "		", "	De sorte que todas as gerações, desde Abraão até Davi, são catorze gerações; e desde Davi até a deportação para a babilônia, catorze gerações; e desde a deportação para a babilônia até Cristo, catorze gerações.CustoCustoMateus 1:17	");
        Pergunta pergunta6 = new Pergunta(1, "	6- Quem foi a mãe de Jesus Cristo?	", "	Maria	", "		", "		", "		", "	Ora, o nascimento de Jesus Cristo foi assim: Que estando Maria, sua mãe, desposada com José, antes de se ajuntarem, achou-se ter concebido do Espírito Santo.CustoCustoMateus 1:18	");
        Pergunta pergunta7 = new Pergunta(1, "	7- Qual é a tradução de EMANUEL?	", "	B	", "	Deus com você	", "	Deus conosco	", "	Força	", "	23 Eis que a virgem conceberá, e dará à luz um filho, e chamarão seu nome Emanuel, que traduzido é: Deus conosco. Custo|fn:  Ref. - Isaías 7:14	");
        Pergunta pergunta8 = new Pergunta(1, "	8- Jesus nasceu em:	", "	C	", "	Jordânia	", "	Galileia	", "	Belém da Judeia	", "	1 E sendo Jesus já nascido em Belém da Judeia, nos dias do rei Herodes, eis que vieram uns magos do oriente a Jerusalém, Custo	");
        Pergunta pergunta9 = new Pergunta(1, "	9- Quem foi o rei do oriente no nascimento de Jesus Cristo?	", "	Herodes	", "		", "		", "		", "	1 E sendo Jesus já nascido em Belém da Judeia, nos dias do rei Herodes, eis que vieram uns magos do oriente a Jerusalém, Custo	");
        Pergunta pergunta10 = new Pergunta(1, "	10- Quais foram as especiarias que os Reis Magos levaram para Jesus Cristo?	", "	A	", "	Ouro, incenso e mirra	", "	Alumínio, canela e cobre	", "	Prata, cevada e aloé	", "	Mt 2 11 E entrando na casa, acharam o menino com sua mãe Maria, e prostrando-se o adoraram. E abrindo seus tesouros, ofereceram-lhe presentes: ouro, incenso, e mirra. Custo	");
        Pergunta pergunta11 = new Pergunta(1, "	11- José e sua família foram para ______________ depois da fuga para o Egito e morte de Herodes.	", "	Nazaré	", "		", "		", "		", "	Mt 2-23 E veio a habitar na cidade chamada Nazaré, para que se cumprisse o que foi dito pelos profetas, que: Ele será chamado de Nazareno.Custo	");
        Pergunta pergunta12 = new Pergunta(1, "	12- O que disse João Batista no deserto Judeia?	", "	A	", "	'Arrependei-vos, porque é chegado o reino dos céus.''	", "	 Quem tem ouvidos para ouvir ouça.	", "	Porque o meu jugo é suave, e o meu fardo é leve."	, "	Mt 3-2 E dizendo: Arrependei-vos, porque perto está o Reino dos céus. Custo	");
        Pergunta pergunta13 = new Pergunta(1, "	13- Em que rio Jesus Cristo foi batizado?	", "	Jordão	", "		", "		", "		", "	Mt 3 13 Então Jesus veio da Galileia ao Jordão até João para ser por ele batizado. Custo	");
        Pergunta pergunta14 = new Pergunta(1, "	14- Jesus foi batizado por:	", "	A	", "	João Batista	", "	Tiago	", "	Samuel	", "	Mt 3-13 Então Jesus veio da Galileia ao Jordão até João para ser por ele batizado. Custo	");
        Pergunta pergunta15 = new Pergunta(1, "	15- E disse-lhe : Se tu és o _________ de Deus, lança-te daqui abaixo;[...]	", "	Filho	", "		", "		", "		", "	Mt 4-6 E disse-lhe: Se tu és o Filho de Deus, lança-te abaixo, porque está escrito que: Mandará a seus anjos acerca de ti, e te tomarão pelas mãos, para que nunca com teu pé tropeces em pedra alguma. Custo	");
        Pergunta pergunta16 = new Pergunta(1, "	16- Por quem Jesus voltou para a Galileia?	", "	João	", "		", "		", "		", "	Mt 4-12 Mas quando Jesus ouviu que João estava preso, voltou para a Galileia. Custo	");
        Pergunta pergunta17 = new Pergunta(1, "	17- O que faziam os primeiros discípulos de Jesus quando Ele os chamou?	", "	B	", "	Dormindo	", "	Pescando	", "	Pastoreando	", "	Mt 4-18 Enquanto Jesus andava junto ao mar da Galileia, viu dois irmãos: Simão, chamado Pedro, e seu irmão André, lançarem a rede ao mar, porque eram pescadores. Custo	");
        Pergunta pergunta18 = new Pergunta(1, "	18- Tiago era filho de _________ .	", "	Zebedeu	", "		", "		", "		", "	Mt 4-21 E passando dali, viu outros dois irmãos: Tiago, [filho] de Zebedeu, e seu irmão João, em um barco, com seu pai Zebedeu, que estavam consertando suas redes; e ele os chamou. Custo	");
        Pergunta pergunta19 = new Pergunta(1, "	19- Qual era o nome do irmão de Tiago, filho de Zebedeu?	", "	B	", "	Marcos	", "	João	", "	Samuel	", "	Mt 4-21 E passando dali, viu outros dois irmãos: Tiago, [filho] de Zebedeu, e seu irmão João, em um barco, com seu pai Zebedeu, que estavam consertando suas redes; e ele os chamou. Custo	");
        Pergunta pergunta20 = new Pergunta(1, "	20- Jesus percorria Galileia ensinando nas _______________ .	", "	Sinagogas	", "		", "		", "		", "	Mt 4-23 E Jesus rodeava toda a Galileia, ensinando em suas sinagogas, pregando o Evangelho do Reino, e curando toda enfermidade e toda doença no povo. Custo	");
        Pergunta pergunta22 = new Pergunta(2, "	1- Bem-aventurados os pobres de espírito, porque eles herdarão a terra.	", "	F 	", "		", "		", "		", "	Mt 5 3 Benditos são os humildes de espírito, porque deles é o Reino dos céus. Custo	");
        Pergunta pergunta23 = new Pergunta(2, "	2- Bem-aventurados os que têm fome e sede de justiça, porque eles serão fartos.	", "	V	", "		", "		", "		", "	Mt 5-6 Benditos são os que têm fome e sede de justiça, porque eles serão saciados. Custo	");
        Pergunta pergunta24 = new Pergunta(2, "	3- Bem-aventurados os que sofrem perseguição  por causa da justiça, porque deles é o Reino dos céus.	", "	V	", "		", "		", "		", "	Mt 5 10 Benditos são os que sofrem perseguição por causa da justiça, porque deles é o Reino dos céus. Custo	");
        Pergunta pergunta25 = new Pergunta(2, "	4- Vós sois o _________ da terra [...]	", "	sal	", "		", "		", "		", "	Mt 5-13 Vós sois o sal da terra; mas se o sal perder seu sabor, com que se salgará? Para nada mais presta, a não ser para se lançar fora, e ser pisado pelas pessoas. Custo	");
        Pergunta pergunta26 = new Pergunta(2, "	5- Vós sois a _______ do mundo [...] 	", "	luz	", "		", "		", "		", "	Mt 5-14 Vós sois a luz do mundo; não se pode esconder uma cidade fundada sobre o monte; Custo	");
        Pergunta pergunta27 = new Pergunta(2, "	6- Não se pode esconder uma cidade edificada sobre um:	", "	C	", "	Muro	", "	Morro	", "	Monte	", "	Mt 5-14 Vós sois a luz do mundo; não se pode esconder uma cidade fundada sobre o monte; Custo	");
        Pergunta pergunta28 = new Pergunta(2, "	7- Mas buscai primeiro o Reino de Deus, e a sua ___________ , e todas essas coisas vos serão acrescentadas.	", "	justiça	", "		", "		", "		", "	Mt 6-33 Mas buscai primeiramente o Reino de Deus e a sua justiça, e todas estas coisas vos serão acrescentadas. Custo	");
        Pergunta pergunta29 = new Pergunta(2, "	8- Não ____________ , para que não sejais julgados.	", "	julgueis	", "		", "		", "		", "	Mt 7-1 Não julgueis, para que não sejais julgados. Custo	");
        Pergunta pergunta30 = new Pergunta(2, "	9- Entrai pela porta ___________ , porque ___________ é a porta, e ____________ , o caminho que conduz à perdição [...]	", "	A	", "	estreita / larga / espaçoso	", "	pequena / grande / largo	", "	fina / ampla / vasto	", "	Mt 7-13 Entrai pela porta estreita; porque larga é a porta, e espaçoso o caminho que leva à perdição; e muitos são os que por ela entram. Custo	");
        Pergunta pergunta31 = new Pergunta(2, "	10- Toda árvore que não dá bom fruto _________ e lança-se no fogo.	", "	corta	", "		", "		", "		", "	Mt 7-19 Toda árvore que não dá bom fruto é cortada e lançada ao fogo. Custo	");
        Pergunta pergunta32 = new Pergunta(2, "	11- Em que cidade Jesus estava com o apelo do centurião?	", "	Cafarnaum	", "		", "		", "		", "	Mt 8-5 Quando Jesus entrou em Cafarnaum, veio a ele um centurião, rogando-lhe, Custo	");
        Pergunta pergunta33 = new Pergunta(2, "	12-  Onde Jesus tocou na sogra de Pedro para curá-la?	", "	Mão	", "		", "		", "		", "	Mt 8-15 Ele tocou a mão dela, e a febre a deixou. Então ela se levantou e começou a servi-los. Custo	");
        Pergunta pergunta34 = new Pergunta(2, "	13- Ele tomou sobre si as nossas ______________ e levou as nossas doenças.	", "	enfermidades	", "		", "		", "		", "	Mt 8-17 Para que se cumprisse o que havia sido dito pelo profeta Isaías, que disse: Ele tomou sobre si as nossas enfermidades, e levou as nossas doenças. Custo	");
        Pergunta pergunta35 = new Pergunta(2, "	14- Segue-me e deixa aos mortos sepultar os seus ___________ .	", "	C	", "	orgulhos	", "	problemas	", "	mortos	", "	Mt 8-22 Porém Jesus lhe disse: Segue-me, e deixa aos mortos enterrarem seus mortos. Custo	");
        Pergunta pergunta36 = new Pergunta(2, "	15- O que Jesus fazia no barco durante a grande tempestade no mar?	", "	Dormia	", "		", "		", "		", "	Mt 8-24 E eis que se levantou no mar uma tormenta tão grande que o barco era coberto pelas ondas; porém ele dormia. Custo	");
        Pergunta pergunta37 = new Pergunta(2, "	16- Quem os ventos e o mar, durante a tempestade no barco, obedeceram?	", "	Jesus	", "		", "		", "		", "	Mt 8-26 E ele lhes respondeu: Por que temeis, [homens] de pouca fé? Então ele se levantou e repreendeu os ventos e o mar. E houve grande calmaria. Custo	");
        Pergunta pergunta38 = new Pergunta(2, "	17-  Quantos endemoniados Jesus encontrou em Gadareno?	", "	Dois 	", "		", "		", "		", "	Mt 8-28 E quando chegou à outra margem, à terra dos gergesenos, vieram-lhe ao encontro dois endemoninhados que tinham saído dos sepulcros. Eles eram tão ferozes que ninguém podia passar por aquele caminho. Custo	");
        Pergunta pergunta39 = new Pergunta(2, "	18- Para onde os demônios expulsados por Jesus em Gadareno sugeriram ir?	", "	B	", "	Povo gadareno	", "	Manada de porcos	", "	Rebanho de ovelhas	", "	Mt 8-31 E os demônios rogaram-lhe, dizendo: Se nos expulsares, permite-nos entrar naquela manada de porcos. Custo	");
        Pergunta pergunta40 = new Pergunta(2, "	19- O que aconteceu com a manada de porcos em que os demônios entraram em Gadareno?	", "	Suicidaram	", "		", "		", "		", "	Mt 8-32 E ele lhes disse: Ide. Então eles saíram, e entraram na manada de porcos; e eis que toda aquela manada de porcos se lançou de um precipício ao mar, e morreram nas águas. Custo	");
        Pergunta pergunta41 = new Pergunta(2, "	20- E eis que toda aquela cidade saiu ao encontro de Jesus, e, vendo-o, rogaram-lhe que se ___________ do seu território.	", "	retirasse	", "		", "		", "		", "	Mt 8-34 E eis que toda aquela cidade saiu ao encontro de Jesus; e quando o viram, rogaram-lhe que se retirasse do território deles.Custo	");
        Pergunta pergunta43 = new Pergunta(3, "	1- E eis que lhe trouxeram um paralítico deitado ________________ .	", "	A	", "	numa cama	", "	num sofá	", "	numa poltrona	", "	Mt 9-1 Então ele entrou no barco, passou para a outra margem, e veio à sua própria cidade. Custo	");
        Pergunta pergunta44 = new Pergunta(3, "	2- O que Jesus disse ao paralítico de Cafarnaum após curá-lo?	", "	B	", "	Levanta-te e segue-me	", "	Levanta-te, toma a tua cama e vai para tua casa	", "	Segue teu caminho	", "	Mt 9-6 Ora, para que saibais que o Filho do homem tem autoridade na terra para perdoar pecados, (Ele, então, disse ao paralítico): Levanta-te, toma o teu leito, e vai para tua casa. Custo	");
        Pergunta pergunta45 = new Pergunta(3, "	3- Porque eu não vim para chamar __________ , mas _____________ , ao arrependimento.	", "	A	", "	os justos / os pecadores	", "	os limpos / os sujos	", "	os bons / os maus	", "	Mt 9-13 Mas ide aprender o que significa: “Quero misericórdia, e não sacrifício”. Porque eu não vim chamar os justos, mas sim os pecadores, ao arrependimento. Custo	");
        Pergunta pergunta46 = new Pergunta(3, "	4- [...]Dias, porém, virão em que lhes será tirado o esposo, e então descansarão.	", "	F	", "		", "		", "		", "	Mt 9-15 E Jesus lhes respondeu: Podem, por acaso, os convidados do casamento andar tristes enquanto o noivo está com eles? Mas dias virão, quando o noivo lhes for tirado, e então jejuarão. Custo	");
        Pergunta pergunta47 = new Pergunta(3, "	5- Quantos anos a mulher do fluxo de sangue ficou doente?	", "	C	", "	20 anos	", "	17 anos	", "	12 anos	", "	Mt 9-20 (Eis, porém, que uma mulher enferma de um fluxo de sangue havia doze anos veio por detrás [dele] , e tocou a borda de sua roupa; Custo	");
        Pergunta pergunta48 = new Pergunta(3, "	6- O que fez a mulher do fluxo de sangue para ser curada?	", "	B	", "	Chorou	", "	Tocou a orla do seu vestido 	", "	Clamou pelo seu Nome	", "	Mt 9-20 (Eis, porém, que uma mulher enferma de um fluxo de sangue havia doze anos veio por detrás [dele] , e tocou a borda de sua roupa; Custo	");
        Pergunta pergunta49 = new Pergunta(3, "	7- Disse-lhes: Retirai-vos, que a menina não está morta, mas dorme.	", "	V	", "		", "		", "		", "	Mt 9-24 E disse-lhes: Retirai-vos, porque a menina não está morta, mas sim dormindo. E riram dele. Custo|fn:  N4 omite lhes	");
        Pergunta pergunta50 = new Pergunta(3, "	8- Mas os cananeus diziam: Ele expulsa os demônios pelo príncipe dos demônios.	", "	F	", "		", "		", "		", "	Mt 9-34 Mas os fariseus diziam: É pelo chefe dos demônios que ele expulsa os demônios. Custo	");
        Pergunta pergunta51 = new Pergunta(3, "	9- Então, disse aos seus discípulos: A seara é realmente grande, mas poucos são os  ________.	", "	ceifeiros	", "		", "		", "		", "	Mt 9-37 Então disse aos seus discípulos: Em verdade a colheita é grande, porém os trabalhadores são poucos. Custo	");
        Pergunta pergunta52 = new Pergunta(3, "	10- Qual dos discipulos de Jesus era apelidade de Tadeu?	", "	Lebeu	", "		", "		", "		", "	Mt 10-3 Filipe e Bartolomeu; Tomé, e Mateus o coletor de impostos; Tiago, [filho] de Alfeu; e Lebeu, por sobrenome Tadeu; Custo	");
        Pergunta pergunta53 = new Pergunta(3, "	11- Quais foram as cidades destruídas por suas desobediências?	", "	C	", "	Galileia e Nínive	", "	Jerusalém e Belém	", "	Sodoma e Gomorra	", "	Mt 10-15 Em verdade vos digo que no dia do julgamento mais tolerável será para a região de Sodoma e Gomorra do que para aquela cidade. Custo	");
        Pergunta pergunta54 = new Pergunta(3, "	12- E odiados de todos seres por causa do meu nome; mas aquele que perseverar até ao fim será morto.	", "	F	", "		", "		", "		", "	Mt 10-22 E sereis odiados por todos por causa de meu nome; mas aquele que perseverar até o fim, esse será salvo. Custo	");
        Pergunta pergunta55 = new Pergunta(3, "	13- E até mesmo os cabelos da vossa cabeça estão todos contados.	", "	V	", "		", "		", "		", "	Mt 10-30 E até os cabelos de vossas cabeças estão todos contados. Custo	");
        Pergunta pergunta56 = new Pergunta(3, "	14- E assim, os inimigos do homem serão os seus vizinhos.	", "	F	", "		", "		", "		", "	Mt 10-36 E os inimigos do homem serão os de sua própria casa”. Custo	");
        Pergunta pergunta57 = new Pergunta(3, "	15- Quem ama ________ ou __________ mais do que a mim não é digno de mim [...]	", "	A	", "	o pai / a mãe	", "	o tia / a tia	", "	o sobrinho / a sobrinha	", "	Mt 10-37 Quem ama pai ou mãe mais que a mim não é digno de mim; e quem ama filho ou filha mais que a mim não é digno de mim; Custo	");
        Pergunta pergunta58 = new Pergunta(3, "	16- E quem não toma a sua _________ e não segue após mim não é digno de mim.	", "	cruz	", "		", "		", "		", "	Mt 10-38 E quem não toma sua cruz e segue após mim não é digno de mim. Custo	");
        Pergunta pergunta59 = new Pergunta(3, "	17- Quem achar a sua vida ___________ e quem perder a sua vida por amor a mim ______________ .	", "	B	", "	trocá-la-á / ganhá-la-á	", "	perdê-la-á  / achá-la-á	", "	afundar-se-á / arrecadar-se-á	", "	Mt 10-39 Quem achar sua vida a perderá; e quem, por causa de mim, perder sua vida, a achará. Custo	");
        Pergunta pergunta60 = new Pergunta(3, "	18 - [...] Em verdade vos digo que de modo algum perderá seu galardão.	", "	V	", "		", "		", "		", "	Mt 10-42 E qualquer um que der ainda que somente um copo de [água] fria a um destes pequenos por reconhecê-lo como discípulo, em verdade vos digo que de maneira nenhuma perderá sua recompensa.Custo	");
        Pergunta pergunta61 = new Pergunta(3, "	19- E bem-aventurado é aquele que se não _________________ em mim.	", "	escandalizar	", "		", "		", "		", "	Mt 11-6 E bendito é aquele que não deixar de crer em mim. Custo	");
        Pergunta pergunta62 = new Pergunta(3, "	20- Em verdade vos digo que, entre os que de mulher tem nascido, não apareceu alguém maior do que...	", "	C	", "	Davi	", "	Jesus Cristo	", "	João Batista	", "	Mt 11-11 Em verdade vos digo que, dentre os nascidos de mulheres, não se levantou [outro] maior que João Batista; porém o menor no Reino dos céus é maior que ele. Custo	");
        Pergunta pergunta63 = new Pergunta(3, "	21- Porque todos os _____________ e a lei profetizaram até João.	", "	profetas	", "		", "		", "		", "	Mt 11-13 Porque todos os profetas e a Lei profetizaram até João. Custo	");
        Pergunta pergunta64 = new Pergunta(3, "	22- E, se quereis dar crédito, é este o ____________ que havia de vir.	", "	Elias	", "		", "		", "		", "	Mt 11-14 E se estais dispostos a aceitar, este é o Elias que havia de vir. Custo	");
        Pergunta pergunta65 = new Pergunta(3, "	23- Quem tem ouvidos para ouvir ____________ .	", "	ouça	", "		", "		", "		", "	Mt 11-15 Quem tem ouvidos para ouvir, ouça. Custo	");
        Pergunta pergunta66 = new Pergunta(3, "	24- Quais são as três cidades impenitentes?	", "	A	", "	Corazim, Betsaida, Cafarnaum	", "	Belém, Jerusalém, Damasco	", "	Gaza, Atenas, Grécia 	", "	Mt 11 21 Ai de ti Corazim! Ai de ti Betsaida! Porque se em Tiro e em Sídon tivessem sido feitos os milagres que em vós foram feitos, há muito tempo teriam se arrependido com saco e com cinza! Custo22 Porém eu vos digo que mais tolerável será para Tiro e Sídon, no dia do juízo, que para vós. Custo23 E tu, Cafarnaum, que estás exaltada até o céu, ao mundo dos mortos serás derrubada! Pois se em Sodoma tivessem sido feitos os milagres que foram feitos em ti, ela teria permanecido até hoje. 	");
        Pergunta pergunta67 = new Pergunta(3, "	25- Vinde a mim, todos os que estais cansados e oprimidos, e eu vos aliviarei.	", "	V	", "		", "		", "		", "	Mt 11-28 Vinde a mim todos vós que estais cansados e sobrecarregados, e eu vos farei descansar. Custo	");
        Pergunta pergunta68 = new Pergunta(3, "	26- Porque o meu jugo é suave, e o meu fardo é pesado.	", "	F	", "		", "		", "		", "	Mt 11-30 Pois o meu jugo é suave, e minha carga é leve.Custo	");
        Pergunta pergunta69 = new Pergunta(3, "	27- Naquele tempo, passou Jesus pelas searas, em um ____________ [...]	", "	C	", "	instante	", "	domingo	", "	sábado	", "	Mt 12-1 Naquele tempo Jesus estava indo pelas plantações de cereais no sábado. Seus discípulos tinham fome, e começaram a arrancar espigas e a comer. Custo	");
        Pergunta pergunta70 = new Pergunta(3, "	28- Porque o Filho do Homem até do _____________ é Senhor.	", "	sábado	", "		", "		", "		", "	Mt 12-8 Porque o Filho do homem é Senhor até do sábado. Custo	");
        Pergunta pergunta71 = new Pergunta(3, "	29- Quem foi curado no sábado na sinagoga?	", "	B	", "	Um surdo	", "	Um homem com uma mão mirrada	", "	Um paralítico	", "	Mt 12-10 E eis que havia ali um homem que tinha uma mão definhada; e eles, a fim de o acusarem, perguntaram-lhe: É lícito curar nos sábados? Custo	");
        Pergunta pergunta72 = new Pergunta(3, "	30- Quem não é comigo é contra mim; e quem comigo não ajunta espalha.	", "	V	", "		", "		", "		", "	Mt 12-30 Quem não é comigo é contra mim; e quem não ajunta comigo, espalha. Custo	");
        Pergunta pergunta73 = new Pergunta(3, "	31- Contra quem da Santíssima Trindade não lhe é perdoado a blasfêmia?	", "	C	", "	Pai	", "	Filho	", "	Espírito Santo	", "	Mt 12 31 Por isso eu vos digo: todo pecado e blasfêmia serão perdoados aos seres humanos; mas a blasfêmia contra o Espírito não será perdoada aos seres humanos. Custo|fn:  N4 omite aos seres humanos Custo32 E qualquer um que falar palavra contra o Filho do homem lhe será perdoado; mas qualquer um que falar contra o Espírito Santo, não lhe será perdoado, nem na era presente, nem na futura. 	");
        Pergunta pergunta74 = new Pergunta(3, "	32- Porque por tuas palavras serás justificado e por tuas palavras serás condenado.	", "	V	", "		", "		", "		", "	Mt 12-37 Porque por tuas palavras serás justificado, e por tuas palavras serás condenado. Custo|fn:  justificado – ou absolvido	");
        Pergunta pergunta75 = new Pergunta(3, "	33- Uma geração _______ e _________ pede um sinal [...]	", "	A	", "	má / adúltera	", "	perversa / promíscua	", "	desumana / cruel	", "	Mt 12-39 Mas ele lhes deu a seguinte resposta: Uma geração má e adúltera pede sinal; mas não lhe será dado, exceto o sinal do profeta Jonas. Custo	");
        Pergunta pergunta76 = new Pergunta(3, "	34- Quanto tempo Jonas esteve no ventre da baleia?	", "	A	", "	Três dias e três noites	", "	Vinte dias e vinte noites	", "	Quarenta dias e quarenta noites	", "	Mt 12-40 Porque assim como Jonas esteve três dias e três noites no ventre da baleia, assim também o Filho do homem estará três dias e três noites no coração da terra. Custo	");
        Pergunta pergunta77 = new Pergunta(3, "	35- Qual a cidade Jonas levava a palavra do Senhor?	", "	Nínive	", "		", "		", "		", "	Mt 12-41 Os de Nínive se levantarão no Juízo com esta geração, e a condenarão; porque se arrependeram com a pregação de Jonas. E eis aqui quem é maior que Jonas. Custo	");
        Pergunta pergunta79 = new Pergunta(4, "	1- Quantas sementes tinham a parábola do semeador?	", "	B	", "	Cinco	", "	Quatro	", "	Três	", "	Mt 13-3 E ele lhes falou muitas coisas por parábolas. Ele disse: Eis que o semeador saiu a semear. Custo4 E enquanto semeava, caiu parte [das sementes] junto ao caminho, e vieram as aves e a comeram. Custo5 E outra [parte] caiu entre pedras, onde não havia muita terra, e logo nasceu, porque não tinha terra funda. Custo6 Mas quando o sol surgiu, queimou-se; e por não ter raiz, secou-se. Custo7 E outra [parte] caiu entre espinhos, e os espinhos cresceram e a sufocaram. Custo8 E outra [parte] caiu em boa terra, e rendeu fruto: um a cem, outro a sessenta, e outro a trinta. 	");
        Pergunta pergunta80 = new Pergunta(4, "	2- Porque a vós é dado conhecer os mistérios do Reino dos céus, mas a eles não lhes é dado; [...]	", "	V	", "		", "		", "		", "	Ele, respondendo, disse-lhes: Porque a vós é dado conhecer os mistérios do reino dos céus, mas a eles não lhes é dado;CustoCustoMateus 13:11	");
        Pergunta pergunta81 = new Pergunta(4, "	3- Porque o coração deste povo está amolecido, e ouviu de bom grado com seus ouvidos e fechou os olhos [...]	", "	F	", "		", "		", "		", "	Porque o coração deste povo está endurecido, E ouviram de mau grado com seus ouvidos, E fecharam seus olhos; Para que não vejam com os olhos, E ouçam com os ouvidos, e compreendam com o coração, e se convertam, e eu os cure.CustoCustoMateus 13:15	");
        Pergunta pergunta82 = new Pergunta(4, "	4- Escutai vós, pois, a _______________ do semeador.	", "	parábola	", "		", "		", "		", "	Escutai vós, pois, a parábola do semeador.CustoCustoMateus 13:18	");
        Pergunta pergunta83 = new Pergunta(4, "	5- A parábola do trigo e do joio diz para fazermos o que quando crescem juntos?	", "	A	", "	Deixai crescerem juntos até à ceifa	", "	Deixai que se separem sozinhos	", "	Se livrar dos dois, pois o joio contaminou o trigo	", "	Deixai crescer ambos juntos até à ceifa; e, por ocasião da ceifa, direi aos ceifeiros: Colhei primeiro o joio, e atai-o em molhos para o queimar; mas, o trigo, ajuntai-o no meu celeiro.CustoCustoMateus 13:30	");
        Pergunta pergunta84 = new Pergunta(4, "	6- Por qual meio/maneira que Jesus falava e ensinava a multidão?	", "	Parábolas	", "		", "		", "		", "	Tudo isto disse Jesus, por parábolas à multidão, e nada lhes falava sem parábolas;CustoCustoMateus 13:34	");
        Pergunta pergunta85 = new Pergunta(4, "	7- De acordo com a genealogia terrena de Jesus, como se chamam seus irmãos?	", "	B	", "	Lucas, Davi, Pedro e Habacuque	", "	Tiago, José, Simão e Judas	", "	José, Tiago, Simão e João	", "	Não é este o filho do carpinteiro? e não se chama sua mãe Maria, e seus irmãos Tiago, e José, e Simão, e Judas?CustoCustoMateus 13:55	");
        Pergunta pergunta86 = new Pergunta(4, "	8- Quem foi instruído a pedir pela morte de João Batista?	", "	A	", "	A filha de Herodias	", "	Herodias	", "	Felipe	", "	E ela, instruída previamente por sua mãe, disse: Dá-me aqui, num prato, a cabeça de João o Batista.CustoCustoMateus 14:8	");
        Pergunta pergunta87 = new Pergunta(4, "	9- Como João Batista morreu?	", "	Degolado	", "		", "		", "		", "	E mandou degolar João no cárcere.CustoCustoMateus 14:10	");
        Pergunta pergunta88 = new Pergunta(4, "	10- E mandou degolar João no saguão, [...]	", "	F	", "		", "		", "		", "	E mandou degolar João no cárcere.CustoCustoMateus 14:10	");
        Pergunta pergunta89 = new Pergunta(4, "	11- Com quantos pães e peixes Jesus fez a primeira multiplicação dos mesmos?	", "	C	", "	Três pães e dois peixes	", "	Sete pães e três peixes	", "	Cinco pães e dois peixes	", "	E, tendo mandado que a multidão se assentasse sobre a erva, tomou os cinco pães e os dois peixes, e, erguendo os olhos ao céu, os abençoou, e, partindo os pães, deu-os aos discípulos, e os discípulos à multidão.CustoCustoMateus 14:19	");
        Pergunta pergunta90 = new Pergunta(4, "	12- E comeram rodos e saciaram-se, e levantaram dos pedaços que sobejaram doze cestos cheios.	", "	V	", "		", "		", "		", "	E comeram todos, e saciaramse; e levantaram dos pedaços, que sobejaram, doze alcofas cheias.CustoCustoMateus 14:20	");
        Pergunta pergunta91 = new Pergunta(4, "	13- Que discípulo andou sob as águas com Jesus?	", "	Pedro	", "		", "		", "		", "	E respondeu-lhe Pedro, e disse: Senhor, se és tu, manda-me ir ter contigo por cima das águas.CustoCustoMateus 14:28	");
        Pergunta pergunta92 = new Pergunta(4, "	14- Para onde Jesus e seus discípulos pretendiam ir quando o mesmo andou por cima do mar?	", "	B	", "	Gadara	", "	Genesaré	", "	Galileia	", "	E, tendo passado para o outro lado, chegaram à terra de Genesaré.CustoCustoMateus 14:34	");
        Pergunta pergunta93 = new Pergunta(4, "	15- Este povo honra-me com os seus lábios, mas o seu coração está longe de mim.	", "	V	", "		", "		", "		", "	Este povo se aproxima de mim com a sua boca e me honra com os seus lábios, mas o seu coração está longe de mim.CustoCustoMateus 15:8	");
        Pergunta pergunta94 = new Pergunta(4, "	16- Mas me adoram, ensinando doutrinas que são preceitos dos céus.	", "	F	", "		", "		", "		", "	Mas, em vão me adoram, ensinando doutrinas que são preceitos dos homens.CustoCustoMateus 15:9	");
        Pergunta pergunta95 = new Pergunta(4, "	17- O que contamina o homem não é o que entra na boa, mas o que sai da boca, isso é o que contamina o homem.	", "	V	", "		", "		", "		", "	O que contamina o homem não é o que entra na boca, mas o que sai da boca, isso é o que contamina o homem.CustoCustoMateus 15:11	");
        Pergunta pergunta96 = new Pergunta(4, "	18- [...] Toda planta que meu Pai celestial não plantou será arrancada.	", "	V	", "		", "		", "		", "	Ele, porém, respondendo, disse: Toda a planta, que meu Pai celestial não plantou, será arrancada.CustoCustoMateus 15:13	");
        Pergunta pergunta97 = new Pergunta(4, "	19- Onde foi que Jesus encontrou a mulher cananeia com a filha endemoninhada?	", "	A	", "	Nas partes de Tiro e de Sidom	", "	Genesaré	", "	Ebom	", "	E, partindo Jesus dali, foi para as partes de Tiro e de Sidom.CustoCustoMateus 15:21	");
        Pergunta pergunta98 = new Pergunta(4, "	20- Onde foi a segunda multiplicação dos pães e peixes?	", "	C 	", "	Na borda do rio Jordão	", "	Na margem do rio Nilo	", "	Ao pé do mar da Galileia	", "	Partindo Jesus dali, chegou ao pé do mar da Galiléia, e, subindo a um monte, assentou-se lá.CustoCustoMateus 15:29	");
        Pergunta pergunta99 = new Pergunta(4, "	21- Quantos pães e peixes Jesus multiplicou na sua segunda multiplicação dos mesmos?	", "	B	", "	Dez pães e muitos peixes	", "	Sete pães e uns poucos peixinhos	", "	Nove pães e poucos peixes	", "	E Jesus disse-lhes: Quantos pães tendes? E eles disseram: Sete, e uns poucos de peixinhos.CustoCustoMateus 15:34	");
        Pergunta pergunta100 = new Pergunta(4, "	22- Quanto de pães e peixes sobraram na segunda multiplicação?	", "	B	", "	Três cestos cheios de pedaços	", "	Sete cestos cheios de pedaços	", "	Cinco cestos cheios de pedaços	", "	E todos comeram e se saciaram; e levantaram, do que sobejou, sete cestos cheios de pedaços.CustoCustoMateus 15:37	");
        Pergunta pergunta101 = new Pergunta(4, "	23- Quantas pessoas a segunda multiplicação dos pães e peixes de Jesus Cristo alimentou?	", "	B	", "	Cinco mil homens, além de mulheres e crianças	", "	Quatro mil homens, além de mulheres e crianças	", "	Sete mil homens, além de mulheres e crianças	", "	Mt 15 38 E foram os que comeram quatro mil homens, sem contar as mulheres e as crianças. Custo	");
        Pergunta pergunta102 = new Pergunta(4, "	24- E, tendo despedido a multidão, entrou no barco e dirigiu-se ao território de ________________ .	", "	Magdala	", "		", "		", "		", "	Mt 15-39 Depois de despedir as multidões, [Jesus] entrou em um barco, e veio à região de Magdala.Custo	");
        Pergunta pergunta103 = new Pergunta(4, "	25- E, chegando-se os filisteus e os saduceus para o tentarem, pediram-lhe que lhes mostrasse algum sinal do céu.	", "	F	", "		", "		", "		", "	Mt 16 1 Então os fariseus e os saduceus se aproximaram dele e, a fim de tentá-lo, pediram-lhe que lhes mostrasse algum sinal do céu. Custo	");
        Pergunta pergunta104 = new Pergunta(4, "	26- Uma geração má e adúltera pede um sinal, e nenhum sinal lhe será dado, senão o sinal do profeta ______________ .	", "	Jonas	", "		", "		", "		", "	Mt 16-4 Uma geração má e adúltera pede um sinal; mas nenhum sinal lhe será dado, a não ser o sinal do profeta Jonas. Então os deixou, e foi embora Custo	");
        Pergunta pergunta105 = new Pergunta(4, "	27- Então, compreenderam que não dissera que se guardassem do _______________ do pão, mas da _______________ dos fariseus.	", "	C	", "	trigo / ideologia	", "	farinha / disciplina	", "	fermento / doutrina	", "	Mt 16-12 Então entenderam que ele não havia dito que tomassem cuidado com o fermento de pão, mas sim com a doutrina dos fariseus e saduceus. Custo	");
        Pergunta pergunta106 = new Pergunta(4, "	28- E, chegando Jesus às partes de _______________ de Felipe, interrogou os seus discípulos, dizendo: Quem dizem os homens ser o Filho do Homem?	", "	Cesareia	", "		", "		", "		", "	Mt 16-13 E tendo Jesus vindo às partes da Cesareia de Filipe, perguntou aos seus discípulos: Quem as pessoas dizem que eu, o Filho do homem, sou? Custo	");
        Pergunta pergunta107 = new Pergunta(4, "	29- Qual dos discípulos de Jesus respondeu quem era Ele, de acordo com o Pai, que está nos céus?	", "	A	", "	Simão Pedro	", "	Pedro	", "	João 	", "	Mt 16- 16 E Simão Pedro respondeu: Tu és o Cristo, o Filho do Deus vivo! Custo	");
        Pergunta pergunta108 = new Pergunta(4, "	30- Pois que aproveita ao homem ganhar o mundo inteiro, se perder a sua __________________?[...]	", "	alma	", "		", "		", "		", "	Mt 16-26 Pois que proveito há para alguém, se ganhar o mundo todo, mas perder a sua alma? Ou que dará alguém em resgate da sua alma? Custo	");
        Pergunta pergunta110 = new Pergunta(5, "	1- ___________ dias depois, tomou Jesus consigo alguns de seus discípulos, e os conduziu em particular a um alto monte.	", "	C	", "	Quatro	", "	Sete	", "	Seis	", "	Mt 17-1 Seis dias depois, Jesus tomou consigo Pedro, Tiago, e seu irmão João, e os levou a sós a um monte alto. Custo	");
        Pergunta pergunta111 = new Pergunta(5, "	2- Para quem Jesus transfigurou-se em um alto monte nas partes de Cesareia de Filipe?	", "	B	", "	José, Judas e Pedro	", "	Pedro, Tiago e João	", "	Jonas, André e Simão	", "	Mt 17-2 Então transfigurou-se diante deles; seu rosto brilhou como o sol, e suas roupas se tornaram brancas como a luz. Custo	");
        Pergunta pergunta112 = new Pergunta(5, "	3- Qual dos discípulos de Jesus disse no alto monte para fazer tabernáculos?	", "	Pedro	", "		", "		", "		", "	Mt 17-4 Pedro, então, disse a Jesus: Senhor, bom é para nós estarmos aqui. Se queres, façamos aqui três tendas: uma para ti, uma para Moisés, e uma para Elias. Custo	");
        Pergunta pergunta113 = new Pergunta(5, "	4- [...] Se queres, façamos aqui ____________ tabernáculos, um para ti, um para Moisés e um para Elias.	", "	Três	", "		", "		", "		", "	Mt 17-4 Pedro, então, disse a Jesus: Senhor, bom é para nós estarmos aqui. Se queres, façamos aqui três tendas: uma para ti, uma para Moisés, e uma para Elias. Custo	");
        Pergunta pergunta114 = new Pergunta(5, "	5- Quem era Elias?	", "	A	", "	João Batista	", "	Jesus Cristo	", "	José 	", "	Mt 17-13 Então os discípulos entenderam que ele lhes falara a respeito de João Batista. Custo	");
        Pergunta pergunta115 = new Pergunta(5, "	6- [...] Ó geração incrédula e perversa! Até quando estarei eu convosco e até quando vos sofrerei? Trazei-mo aqui.	", "	V	", "		", "		", "		", "	Mt 17 17 Jesus respondeu: Ó geração incrédula e perversa! Até quando estarei convosco? Até quando vos suportarei? Trazei-o a mim aqui. Custo	");
        Pergunta pergunta116 = new Pergunta(5, "	7- Em verdade vos digo que, se tiverdes fé como um _________________________ [...]	", "	B	", "	grão de ervilha	", "	grão de mostarda	", "	grão de arroz	", "	Mt 17-20 E Jesus lhes respondeu: Por causa da vossa incredulidade; pois em verdade vos digo, que se tivésseis fé como um grão de mostarda, diríeis a este monte: “Passa-te daqui para lá”, E ele passaria. E nada vos seria impossível. Custo	");
        Pergunta pergunta117 = new Pergunta(5, "	8- Mas esta casta de demônios não se expulsa senão pela fé.	", "	F	", "		", "		", "		", "	Mt 17 21 Mas este tipo [de demônio] não sai, a não ser por oração e jejum. Custo	");
        Pergunta pergunta118 = new Pergunta(5, "	9- Segundo Jesus, quem é o maior no Reino dos céus ?	", "	C	", "	Os discípulos	", "	Os com mais conquistas	", "	As crianças	", "	Mt 18- 3 e disse: Em verdade vos digo, que se vós não converterdes, e fordes como crianças, de maneira nenhuma entrareis no Reino dos céus. Custo	");
        Pergunta pergunta119 = new Pergunta(5, "	10- Porque o Filho do Homem veio salvar o que se tinha desobedecido.	", "	F	", "		", "		", "		", "	Mt 18- 11 Pois o Filho do homem veio para salvar o que havia se perdido. Custo	");
        Pergunta pergunta120 = new Pergunta(5, "	11- Porque onde estiverem dois ou três reunidos em meu nome, aí estou eu no meio deles.	", "	V	", "		", "		", "		", "	Mt 18-20 Pois onde dois ou três estiverem reunidos em meu nome, ali eu estou no meio deles. 	");
        Pergunta pergunta121 = new Pergunta(5, "	12- Quantas vezes Jesus disse a Pedro que precisaríamos perdoar o irmão?	", "	B	", "	Três vezes	", "	Até setenta vezes sete	", "	Sempre	", "	Mt 18- Jesus lhe respondeu: Eu não te digo até sete, mas sim até setenta vezes sete. 	");
        Pergunta pergunta122 = new Pergunta(5, "	13- Portanto, o que Deus ajuntou não separe o homem.	", "	V	", "		", "		", "		", "	Mt 19-6 Assim eles já não são mais dois, mas sim uma única carne; portanto, o que Deus juntou, o ser humano não separe. Custo	");
        Pergunta pergunta123 = new Pergunta(5, "	14- Em verdade vos digo que é difícil entrar um rico no Reino dos céus.	", "	V	", "		", "		", "		", "	Mt 19-23 Jesus, então, disse aos seus discípulos: Em verdade vos digo que dificilmente o rico entrará no reino dos céus. Custo	");
        Pergunta pergunta124 = new Pergunta(5, "	15- E outra vez vos digo que é mais fácil passar um _______________ pelo fundo de uma ______________ do que entrar um rico no Reino de Deus.	", "	A 	", "	camelo / agulha	", "	cavalo / cerca	", "	burro / agulha	", "	Mt 19-24 Aliás, eu vos digo que é mais fácil um camelo passar pela abertura de uma agulha do que o rico entrar no reino de Deus. Custo	");
        Pergunta pergunta125 = new Pergunta(5, "	16- Porém muitos primeiros serão derradeiros, e muitos derradeiros não serão primeiros.	", "	F	", "		", "		", "		", "	Mt 19-30 Porém muitos primeiros serão últimos; e últimos, primeiros.Custo	");
        Pergunta pergunta126 = new Pergunta(5, "	17- Quantos discípulos Jesus tinha?	", "	Doze	", "		", "		", "		", "	Mt 20-17 E enquanto Jesus subia a Jerusalém, tomou consigo os doze discípulos à parte no caminho, e lhes disse: Custo	");
        Pergunta pergunta127 = new Pergunta(5, "	18- Onde o Filho do Homem foi entregue aos príncipes dos sacerdotes e aos escribas para à morte?	", "	Jerusalém	", "		", "		", "		", "	Mt 20-18 Eis que estamos subindo a Jerusalém, e o Filho do homem será entregue aos chefes dos sacerdotes e aos escribas, e o condenarão à morte. Custo	");
        Pergunta pergunta128 = new Pergunta(5, "	19- O que a mãe dos filhos de Zebedeu pediu a Jesus?	", "	C	", "	Para curá-los	", "	Para salvá-los, independente de tudo	", "	Um lugar para os dois à sua direita e esquerda nos céus	", "	Mt 20-21 E ele lhe perguntou: O que queres? Ela lhe disse: Dá ordem para que estes meus dois filhos se sentem, um à tua direita e outro à tua esquerda, no teu Reino. Custo	");
        Pergunta pergunta129 = new Pergunta(5, "	20- O Filho do Homem não veio para ser servido, mas para servir e para dar a sua vida em resgate dos puros.	", "	F	", "		", "		", "		", "	Mt 20-28 assim como o Filho do homem não veio para ser servido, mas sim para servir, e para dar a sua vida em resgate por muitos. Custo	");
        Pergunta pergunta131 = new Pergunta(6, "	1- Como foi a entrada triunfal de Jesus em Jerusalém?	", "	B	", "	Cercado por uma multidão	", "	Assentado sobre uma jumenta e sobre um jumentinho	", "	Assentado em um cavalo	", "	Mt 21-7 Então trouxeram a jumenta e o jumentinho, puseram as suas capas sobre eles, e fizeram [-no] montar [] sobre elas. Custo	");
        Pergunta pergunta132 = new Pergunta(6, "	2- O que Jesus fez quando encontrou o povo vendendo coisas no templo em Jerusalém?	", "	A 	", "	Expulsou todos	", "	Bateu neles	", "	Ordenou que se retirassem	", "	12 Jesus entrou no Templo de Deus; então expulsou todos os que estavam vendendo e comprando no Templo, e virou as mesas dos cambistas e as cadeiras dos que vendiam pombas. Custo	");
        Pergunta pergunta133 = new Pergunta(6, "	3- O que Jesus Cristo disse que seria chamada sua casa?	", "	C	", "	Sinagoga	", "	Templo 	", "	Casa de oração	", "	Mt 21 13 E disse-lhes: Está escrito: “Minha casa será chamada casa de oração”; mas vós a tornastes em covil de ladrões! Custo|fn:  TR, RP: tornastes - N4: tornais |fn:  Ref. Isaías 56:7; Jeremias 7:11	");
        Pergunta pergunta134 = new Pergunta(6, "	4- Mas vós a tendes convertido em _____________________ .	", "	A 	", "	covil de ladrões	", "	perdição	", "	um lugar de atos ilícitos	", "	Mt 21-13 E disse-lhes: Está escrito: “Minha casa será chamada casa de oração”; mas vós a tornastes em covil de ladrões! Custo|fn:  TR, RP: tornastes - N4: tornais |fn:  Ref. Isaías 56:7; Jeremias 7:11	");
        Pergunta pergunta135 = new Pergunta(6, "	5- Quem foi ter com Jesus no templo durante a purificação?	", "	B	", "	Leprosos	", "	Cegos e coxos	", "	Cegos	", "	Mt 21- 14 E cegos e mancos vieram a ele no Templo, e ele os curou. Custo	");
        Pergunta pergunta136 = new Pergunta(6, "	6- Para onde Jesus foi após a purificação do templo?	", "	Betânia	", "		", "		", "		", "	Mt 21-17 Então ele os deixou, e saiu da cidade para Betânia, e ali passou a noite. Custo	");
        Pergunta pergunta137 = new Pergunta(6, "	7- Qual árvore Jesus Cristo viu voltando para Jerusalém e mandou-a secar?	", "	Figueira	", "		", "		", "		", "	Mt 21-19 Quando ele viu uma figueira perto do caminho, veio a ela, mas nada nela achou, a não ser somente folhas. E disse-lhe: Nunca de ti nasça fruto, jamais! E imediatamente a figueira se secou. Custo	");
        Pergunta pergunta138 = new Pergunta(6, "	8- Porque muitos são chamados, mas poucos, _______________ .	", "	escolhidos	", "		", "		", "		", "	Mt 22 14 Pois muitos são chamados, porém poucos escolhidos. Custo	");
        Pergunta pergunta139 = new Pergunta(6, "	9- Amarás o Senhor, teu Deus, de todo o teu coração.	", "	F 	", "		", "		", "		", "	Mt 22 37 E Jesus lhe respondeu: Amarás ao Senhor teu Deus com todo o teu coração, com toda a tua alma, e com todo o teu entendimento: Custo	");
        Pergunta pergunta140 = new Pergunta(6, "	10- Qual é o segundo grande mandamento da lei?	", "	B 	", "	Honra teu pai e tua mãe	", "	Amarás o teu próximo como a ti mesmo	", "	Não matarás	", "	Mt 22-39 E o segundo, semelhante a este, [é] : Amarás o teu próximo como a ti mesmo. Custo	");
        Pergunta pergunta141 = new Pergunta(6, "	11- E o que a si mesmo se exaltar será humilhado; e o que a si mesmo se humilhar será exaltado.	", "	V	", "		", "		", "		", "	Mt 23-12 E o que a si mesmo se exaltar será humilhado; e o que a si mesmo se humilhar será exaltado. Custo	");
        Pergunta pergunta142 = new Pergunta(6, "	12- Onde estava o Senhor durante o sermão profético do princípio das dores?	", "	B	", "	Betânia	", "	Monte das Oliveiras	", "	Jordânia	", "	Mt 24- 3 E, depois de se assentar no monte das Oliveiras, os discípulos se aproximaram dele reservadamente, perguntando: Dize-nos, quando serão estas coisas, e que sinal haverá da tua vinda, e do fim da era? Custo	");
        Pergunta pergunta143 = new Pergunta(6, "	13- Porque muitos virão em meu nome, dizendo: Eu sou o Cristo; e enganarão a muitos.	", "	V	", "		", "		", "		", "	Mt 24 5 Porque muitos virão em meu nome, dizendo: “Eu sou o Cristo”, e enganarão a muitos. Custo	");
        Pergunta pergunta144 = new Pergunta(6, "	14- Mas todas essas coisas são o fim das dores.	", "	F	", "		", "		", "		", "	Mt 24 8 Mas todas estas coisas são o começo das dores. Custo	");
        Pergunta pergunta145 = new Pergunta(6, "	15- O céu e a terra passarão, mas as minhas _________________ não hão de passar.	", "	palavras	", "		", "		", "		", "	Mt 24 35 O céu e a terra passarão, mas minhas palavras de maneira nenhuma passarão. Custo	");
        Pergunta pergunta146 = new Pergunta(6, "	16- Quem há de saber o dia e a hora da vinda do Filho do Homem?	", "	Deus	", "		", "		", "		", "	Mt 24 36 Porém daquele dia e hora, ninguém sabe, nem os anjos do céu, a não ser meu Pai somente. Custo	");
        Pergunta pergunta147 = new Pergunta(6, "	17- Vigiai, pois, porque não sabeis a que hora há de vir o vosso Senhor.	", "	V	", "		", "		", "		", "	Mt 24 42 Vigiai, pois, porque não sabeis em que hora o vosso Senhor virá. Custo	");
        Pergunta pergunta148 = new Pergunta(6, "	18- Por isso, estai vós apercebidos também, porque o Filho do Homem há de vir à hora determinada.	", "	F	", "		", "		", "		", "	Mt 24 44 Portanto também vós estai prontos, porque o Filho do homem virá na hora que não esperais. Custo	");
        Pergunta pergunta149 = new Pergunta(6, "	19- Bem-aventurado aquele servo que o Senhor, quando vier, achar servindo assim.	", "	V	", "		", "		", "		", "	Mt 24 46 Feliz será aquele servo a quem, quando o seu senhor vier, achar fazendo assim. Custo	");
        Pergunta pergunta150 = new Pergunta(6, "	20- Em verdade vos digo que o porá sobre todos os seus bens.	", "	V	", "		", "		", "		", "	Mt 24-47 Em verdade vos digo que ele o porá sobre todos os seus bens. Custo	");
        Pergunta pergunta152 = new Pergunta(7, "	1- Quantas virgens estavam à espera do esposo?	", "	A 	", "	Dez	", "	Quinze	", "	Doze	", "	Mt 25 1 Então o Reino dos céus será semelhante a dez virgens, que tomaram suas lâmpadas, e saíram ao encontro do noivo. Custo	");
        Pergunta pergunta153 = new Pergunta(7, "	2- Como eram chamadas as dez virgens?	", "	C	", "	Cinco nervosas e cinco tristes	", "	Cinco temerosas e cinco apressadas	", "	Cinco prudentes e cinco loucas	", "	Mt 25 2 E cinco delas eram prudentes, e cinco tolas. Custo	");
        Pergunta pergunta154 = new Pergunta(7, "	3- O que as virgens deveriam ter em suas lâmpadas?	", "	B	", "	Vinagre	", "	Azeite	", "	Óleo	", "	Mt 25 3 As tolas, quando tomaram as suas lâmpadas, não tomaram azeite consigo. Custo	");
        Pergunta pergunta155 = new Pergunta(7, "	4- Quando foi que Jesus Cristo foi crucificado?	", "	A 	", "	Na páscoa	", "	Num sábado	", "	Em uma comemoração de reis	", "	Mt 26 2 Vós bem sabeis que daqui a dois dias é a Páscoa, e o Filho do homem será entregue para ser crucificado. Custo	");
        Pergunta pergunta156 = new Pergunta(7, "	5- Qual o nome do sumo sacerdote envolvido na crucificação de Cristo?	", "	Caifás	", "		", "		", "		", "	Mt 26 3 Então os chefes dos sacerdotes, os escribas, e os anciãos do povo se reuniram na casa do sumo sacerdote, que se chamava Caifás. Custo	");
        Pergunta pergunta157 = new Pergunta(7, "	6- E, estando Jesus em Betânia, em casa de _______________ , o leproso, [....]	", "	Simão	", "		", "		", "		", "	Mt 26 6 Enquanto Jesus estava em Betânia, na casa de Simão o leproso, Custo	");
        Pergunta pergunta158 = new Pergunta(7, "	7- No jantar em Betânia, que perfume foi lavado os pés de Jesus?	", "	Unguento	", "		", "		", "		", "	Mt 26 7 veio a ele uma mulher com um vaso de alabastro, de óleo perfumado de grande valor, e derramou sobre a cabeça dele, enquanto estava sentado à mesa. Custo	");
        Pergunta pergunta159 = new Pergunta(7, "	8- Qual foi o preço da traição de Judas?	", "	B	", "	20 moedas de prata	", "	30 moedas de prata	", "	50 moedas de prata	", "	Mt 26 15 e disse: O que quereis me dar, para que eu o entregue a vós? E eles lhe determinaram trinta [moedas] de prata. Custo	");
        Pergunta pergunta160 = new Pergunta(7, "	9- Quem traiu Jesus Cristo?	", "	C 	", "	Bartolomeu	", "	Habacuque	", "	Judas Iscariotes	", "	Mt 26 25 E Judas, o que o traía, perguntou: Por acaso sou eu, Rabi? [Jesus] lhe disse: Tu o disseste. Custo	");
        Pergunta pergunta161 = new Pergunta(7, "	10- Quantas vezes Pedro negou Jesus?	", "	Três	", "		", "		", "		", "	Mt 26 34 Jesus lhe disse: Em verdade te digo que, nesta mesma noite, antes do galo cantar, tu me negarás três vezes. Custo	");
        Pergunta pergunta162 = new Pergunta(7, "	11- Aonde Jesus foi orar com seus discípulos antes da crucificação?	", "	A 	", "	No Getsêmani	", "	No templo	", "	Na sinagoga	", "	Mt 26 36 Então Jesus veio com eles a um lugar chamado Getsêmani, e disse aos discípulos: Ficai sentados aqui, enquanto eu vou ali orar. Custo	");
        Pergunta pergunta163 = new Pergunta(7, "	12- Meu Pai, se é possível, passa de mim este cálice; todavia, não seja como eu quero, mas como tu queres.	", "	V	", "		", "		", "		", "	Mt 26 39 E indo um pouco mais adiante, prostrou-se sobre o seu rosto, orando, e dizendo: Meu Pai, se é possível, passe de mim este cálice; porém, não [seja] como eu quero, mas sim como tu [queres] . Custo	");
        Pergunta pergunta164 = new Pergunta(7, "	13- Vigiai e orai, para que não entreis em tentação; na verdade, o espírito está pronto, mas a ____________ é fraca.	", "	carne	", "		", "		", "		", "	Mt 26 41 Vigiai e orai, para que não entreis em tentação. De fato, o espírito [está] pronto, mas a carne [é] fraca. Custo	");
        Pergunta pergunta165 = new Pergunta(7, "	14- Qual sinal o traidor deu para entregar Jesus?	", "	B	", "	Um aperto de mão	", "	Um beijo no rosto	", "	Um abraço	", "	Mt 26 48 O seu traidor havia lhes dado sinal, dizendo: Aquele a quem eu beijar, é esse. Prendei-o. Custo	");
        Pergunta pergunta166 = new Pergunta(7, "	15- Como Jesus se referia a Judas enquanto o mesmo lhe traía?	", "	Amigo	", "		", "		", "		", "	Mt 26 50 Jesus, porém, lhe perguntou: Amigo, para que vieste? Então chegaram, agarraram Jesus, e o prenderam. Custo	");
        Pergunta pergunta167 = new Pergunta(7, "	16- Qual animal cantou antes de Pedro negar Jesus?	", "	Galo	", "		", "		", "		", "	Mt 26 75 Então Pedro se lembrou da palavra de Jesus, que lhe dissera: Antes do galo cantar, tu me negarás três vezes. Assim ele saiu, e chorou amargamente.Custo	");
        Pergunta pergunta168 = new Pergunta(7, "	17- Quem era o governador no julgamento de Jesus?	", "	A	", "	Pôncio Pilatos	", "	José	", "	Simão Pedro	", "	Mt 27 2 E o levaram amarrado, e o entregaram a Pôncio Pilatos, o governador. Custo	");
        Pergunta pergunta169 = new Pergunta(7, "	18- Qual atitude de Judas consigo mesmo quando percebeu sua traição?	", "	Enforcou-se	", "		", "		", "		", "	Mt 27 5 Então ele lançou as [moedas] de prata no templo, saiu, e foi enforcar-se. Custo	");
        Pergunta pergunta170 = new Pergunta(7, "	19- Não é lícito metê-las no cofre das ofertas, porque são preço de morte.	", "	F	", "		", "		", "		", "	Mt 27 6 Os chefes dos sacerdotes tomaram as [moedas] de prata, e disseram: Não é lícito pô-las no tesouro das ofertas, pois isto é preço de sangue. Custo	");
        Pergunta pergunta171 = new Pergunta(7, "	20- O que fizeram com as moedas de prata da traição à Jesus?	", "	B	", "	Jogaram no mar	", "	Compraram um campo de um oleiro	", "	Guardaram	", "	Mt 27 7 Então juntamente se aconselharam, e compraram com elas o campo do oleiro, para ser cemitério dos estrangeiros. Custo	");
        Pergunta pergunta172 = new Pergunta(7, "	21- Como se chamou o lugar que compraram com as moedas da traição?	", "	C	", "	Pântano	", "	Vale de Ossos	", "	Campo de Sangue	", "	Mt 27 8 Por isso aquele campo tem sido chamado campo de sangue até hoje. Custo	");
        Pergunta pergunta173 = new Pergunta(7, "	22- Qual o nome do preso que o povo escolheu soltar em vez de Jesus?	", "	Barrabás	", "		", "		", "		", "	Mt 27 21 O governador lhes perguntou: Qual destes dois quereis que vos solte? E responderam: Barrabás! Custo	");
        Pergunta pergunta174 = new Pergunta(7, "	23- Quem lavou as mãos no julgamento de Jesus? 	", "	Pilatos	", "		", "		", "		", "	Mt 27 24 Quando, pois, Pilatos viu que nada adiantava, em vez disso se fazia mais tumulto, ele pegou água, lavou as mãos diante da multidão, e disse: Estou inocente do sangue deste justo. A responsabilidade é vossa. Custo	");
        Pergunta pergunta175 = new Pergunta(7, "	24- Qual a cor da capa que cobriu Jesus na audiência?	", "	A	", "	Escarlate	", "	Vinho	", "	Cinza	", "	Mt 27 28 Eles o despiram e o cobriram com um manto vermelho. Custo	");
        Pergunta pergunta176 = new Pergunta(7, "	25- E, tecendo uma coroa de _______________ , puseram-lha na cabeça, [...]	", "	espinhos	", "		", "		", "		", "	Mt 27 29 E, depois de tecerem uma coroa de espinhos, puseram-na sobre a sua cabeça, e uma cana em sua mão direita. Em seguida, puseram-se de joelhos diante dele, zombando-o, e diziam: Felicitações, Rei dos Judeus! Custo	");
        Pergunta pergunta177 = new Pergunta(7, "	26- E, quando saíam, encontraram um homem cireneu, chamado ______________ , a quem constrangeram a levar a sua cruz.	", "	Simão	", "		", "		", "		", "	Mt 27 32 Ao saírem, encontraram um homem de Cirene, por nome Simão; e obrigaram-no a levar sua cruz. Custo	");
        Pergunta pergunta178 = new Pergunta(7, "	27- O que significa o lugar chamado Gólgota?	", "	A	", "	Lugar da Caveira	", "	Vale de Ossos	", "	Caverna	", "	Mt 27 33 E quando chegaram ao lugar chamado Gólgota, que significa “o lugar da caveira”, Custo	");
        Pergunta pergunta179 = new Pergunta(7, "	28- O que foi dado a Jesus para beber mas não o quis?	", "	C	", "	Vinho com vinagre	", "	Vinho com água	", "	Vinho com fel	", "	Mt 27 34 deram-lhe de beber vinagre misturado com fel. E, depois de provar, não quis beber. Custo	");
        Pergunta pergunta180 = new Pergunta(7, "	29- O que puseram escrito por cima da cabeça de Cristo?	", "	B	", "	ESTE É JESUS, O REI DOS OPRIMIDOS	", "	ESTE É JESUS, O REI DOS JUDEUS	", "	ESTE É JESUS, O REI DOS PECADORES	", "	Mt 27 37 E puseram, por cima de sua cabeça, sua acusação escrita: ESTE É JESUS, O REI DOS JUDEUS. Custo	");
        Pergunta pergunta181 = new Pergunta(7, "	30- E foram crucificados com ele __________ salteadores, [...]	", "	dois	", "		", "		", "		", "	Mt 27 38 Então foram crucificados com ele dois criminosos, um à direita, e outro à esquerda. Custo	");
        Pergunta pergunta182 = new Pergunta(7, "	31- Quem pediu o corpo de Jesus?	", "	A	", "	José, um discípulo rico de Arimateia	", "	Pedro, da Galileia	", "	João, do Egito	", "	Mt 27 58 Ele chegou a Pilatos, e pediu o corpo de Jesus. Então Pilatos mandou que o corpo [lhe] fosse entregue. Custo	");
        Pergunta pergunta183 = new Pergunta(7, "	32- Quem estava assentado defronte do sepulcro?	", "	B	", "	Marta e José	", "	Maria Madalena e a outra Maria	", "	Marta e Maria	", "	Mt 27 61 E ali estavam Maria Madalena e a outra Maria, sentadas de frente ao sepulcro. Custo	");
        Pergunta pergunta184 = new Pergunta(7, "	33- Aonde Jesus ressuscitado aparece para seus discípulos?	", "	Galileia	", "		", "		", "		", "	Mt 28 18 Jesus se aproximou deles, e lhes falou: Todo o poder me é dado no céu e na terra. Custo	");
        Pergunta pergunta185 = new Pergunta(7, "	34 - Jesus afirma que estará conosco em quais momentos ?	", "	C	", "	Quando ele voltar	", "	Quando orarmos	", "	Todos os dias	", "	Mt 28 20 ensinando-lhes a guardar todas as coisas que eu vos tenho mandado. E eis que eu estou convosco todos os dias, até o fim dos tempos. Amém.Custo	");

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
        pergunta1.pergunta = "Quem era o imperador que mandou uma ordem para todos os povos do Império.";
        pergunta1.correta = "A";
        pergunta1.a = "Augusto";
        pergunta1.b = "Alexandre";
        pergunta1.c = "Davi";
        pergunta1.dica = "Lucas 2.1";

        //Este padrão abaixo é para perguntas de verdadeiro ou falso:

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Todas as pessoas deviam se registrar a fim de ser feita uma contagem da população.";
        pergunta2.correta = "A";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = " Lucas 2.1";

        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Onde as pessoas iam se registrar?";
        pergunta3.correta = "C";
        pergunta3.a = "Roma";
        pergunta3.b = "Grécia";
        pergunta3.c = "Onde nasceram";
        pergunta3.dica = " Lucas 2.2";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "José foi para qual região?";
        pergunta4.correta = "C";
        pergunta4.a = "Roma";
        pergunta4.b = "Grécia";
        pergunta4.c = "Judeia";
        pergunta4.dica = " Lucas 2.4";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "José foi registrar-se lá porque era descendente de Davi";
        pergunta5.correta = "A";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = " Lucas 2.4";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "José levou Maria, com quem era casado";
        pergunta6.correta = "B";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = " Lucas 2.5";


        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Maria estava Grávida";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = " Lucas 2.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Então Jesus nasceu em Nazaré";
        pergunta8.correta = "B";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = " Lucas 2.6";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Jesus nasceu em uma pensão";
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

        Tema tema1 = new Tema("1", "Criação", "", "", "", "0");
        tema1.mensagemInicial = "No princípio, Deus projetou os céus a terra e a humanidade para viver nesse mundo. Deus criou a terra, antes de  dizer: 'haja luz'. Você está afiado sobre a obra da Criação e o propósito de Deus?  ";
        temas.Add(tema1);

        Tema tema2 = new Tema("2", "A vida após o jardim", "", "", "", "15");
        tema2.mensagemInicial = "Tudo ia bem no Éden até os primeiros humanos criados desrespeitarem o Criador.  O ser humano ficou incompatível com Deus, que é santo. Ele foi, então, tirado do paraíso. E a Criação não foi mais a mesma.";
        temas.Add(tema2);

        Tema tema3 = new Tema("3", "Caim e Abel", "", "", "", "30");
        tema3.mensagemInicial = "Vamos conhecer os filhos de Adão e Eva";
        temas.Add(tema3);


        Tema tema4 = new Tema("4", "O Dilúvio ", "", "", "", "31");
        tema4.mensagemInicial = "Essa é uma história triste e bonita. Nela vemos uma geração centenária de pessoas se rebelar contra a justiça de Deus. Mas vemos a justiça de Deus em reconpensar o justo por praticar a Sua justiça.";
        temas.Add(tema4);


        Tema tema5 = new Tema("5", "Depois do Dilúvio ", "", "", "", "41");
        tema5.mensagemInicial = "Essa historia é bem curriosa porque mostra um recomeço da humanidade e revela que o ser humano continua  com inclinações para o mal, mas Deus sempre tem seus fiéis ou pessoa (s) que atende ao seu chamado e não o deixa desistir da humanidade.";
        temas.Add(tema5);

        Tema tema6 = new Tema("6", "O chamado de Abraão ", "", "", "", "52");
        tema6.mensagemInicial = "A beleza de uma amizade entre Deus e um ser humano está bem preenchida nessa história mais do que nos relatos  anteriores em que o leitor da Bíblica  precisa  usar  inferência para saber disso, como é o caso de Enoque, que andou com Deus";
        temas.Add(tema6);

        Tema tema7 = new Tema("7", "A fidelidade de Abraão ", "", "", "", "53");
        tema7.mensagemInicial = "A segunda parte da história desse homem de fé mostra mais da relacão dele com Deus de modo a nos emocionar  e ensinar mais sobre nosso Pai celestial.";
        temas.Add(tema7);

        Tema tema8 = new Tema("8", "A resiliência de Isaque", "", "", "", "70");
        tema8.mensagemInicial = "Um homem que soube viver entre amigos e inimigos com uma paciência exemplar foi Isaque,  o protagonista desta vez.";
        temas.Add(tema8);

        Tema tema9 = new Tema("9", "Jacó ", "", "", "", "80");
        tema9.mensagemInicial = "Jacó e a origem dos nomes dos principais territórios de Canaã Embora os judeus(israelitas) estimem Abfaão como seu pai, Jacó é o genitor mais próximo dos descendentes que deram nomes tribos e, na posse  de Canaã, a territorios Essa é uma história bastante  tensa na Bíblia. ";
        temas.Add(tema9);


        Tema tema10 = new Tema("10", "José ", "", "", "", "92");
        tema10.mensagemInicial = "A história de um homem brilhantemente  honesto José teve uma característica  que agrada a Deus mais destacada que os demais personagens de Gênesis, uma qualidade que deve causar admiração no cristão, a honestidade, que é um amor à justiça.";
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
        pergunta2.pergunta = "As áreas mais naturais do planeta são selvagens, difíceis de viver por causa do pecado.";
        pergunta2.correta = "V";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 3.18";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Contudo, a vida de trabalho do homem passou a ser mais fácil.";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 3.19";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "A mulher não nasceu para dar à luz com dores.";
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
        pergunta6.pergunta = "Após o pecado o ser humano ainda pôde viver próximo de 1000 anos";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 5.3 a 27";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Adão viveu até 930 anos de idade.";
        pergunta7.correta = "V";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn 5.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Quem foi o homem mais velho da Bíblia?";
        pergunta8.correta = "B";
        pergunta8.a = "Adão";
        pergunta8.b = "Matusalém";
        pergunta8.c = "Noé";
        pergunta8.dica = "Gn 5.27";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Mesmo não tentado  por satanás, o homem, com raras excecões, se mostrou rebelde. ";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 4.5";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Nos tempos antes do Dilúvio a dieta do homem era baseada em legumes e  frutas. ";
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
        pergunta2.pergunta = "Nenhum Homem se  destacou na prática da justiça de Deus até o tempo de Noé.";
        pergunta2.correta = "F";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 4.4; Gn 5.24; Hb 11.5";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Adão não teve filha";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 5.4";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Deus quis desistir do ser humano porque, com o tempo, não restava senão uma unica família que o temia.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn 6.5 a 8";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Deus decidiu renovar o mundo através de um Dilúvio.";
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
        pergunta6.dica = "GEN 4:2 E depois deu à luz a seu irmão Abel. E foi Abel pastor de ovelhas, e Caim foi lavrador da terra.";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Caim  era Lavrador";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "GEN 4:2 E depois deu à luz a seu irmão Abel. E foi Abel pastor de ovelhas, e Caim foi lavrador da terra.";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "o SENHOR olhou  com agrado a Caim";
        pergunta8.correta = "F";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "GEN 4:4 E Abel trouxe também dos primogênitos de suas ovelhas, e de sua gordura. E olhou o SENHOR com agrado a Abel e à sua oferta;";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "o SENHOR olhou com agrado a Abel";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "GEN 4:4 E Abel trouxe também dos primogênitos de suas ovelhas, e de sua gordura. E olhou o SENHOR com agrado a Abel e à sua oferta;";




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
        pergunta1.pergunta = "Noé viveu no meio de um povo bonzinho.";
        pergunta1.correta = "F";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 6.5";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "O Dilúvio foi um acúmulo de águas que saíram das nuvens e da terra. ";
        pergunta2.correta = "V";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 7.11";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Quantos filhos teve Noe?";
        pergunta3.correta = "A";
        pergunta3.a = "Três";
        pergunta3.b = "Dois";
        pergunta3.c = "Um";
        pergunta3.dica = "Gn 6.10";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "O que Deus ordenou que Noé construísse?";
        pergunta4.correta = "B";
        pergunta4.a = "Uma grande casa";
        pergunta4.b = "Um grande navio";
        pergunta4.c = "Um grande carro";
        pergunta4.dica = "Gn 6.14";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Quantos pares de cada peixe do mar Noé incluiu na Arca?";
        pergunta5.correta = "C";
        pergunta5.a = "Um par";
        pergunta5.b = "Dois pares";
        pergunta5.c = "Nenhum par";
        pergunta5.dica = "Gn 7.2,3";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Noé teve três esposas.";
        pergunta6.correta = "F";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 7.13";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Noé teve um filho de nome Jafé.";
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
        pergunta9.pergunta = "Noé não precisou buscar os animais que deveriam entrar na Arca porque Deus os fez ir até ela.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 7.9";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Nenhuma das pessoas daquela geração centenária entrou na Arca por causa da sua impiedade.";
        pergunta10.correta = "V";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "Gn 7.1";


        Pergunta pergunta11 = new Pergunta();
        pergunta11.pergunta = "O Dilúvio durou trinta dias e trinta noites.";
        pergunta11.correta = "F";
        pergunta11.a = "";
        pergunta11.b = "";
        pergunta11.c = "";
        pergunta11.dica = "Gn 7.12";


        Pergunta pergunta12 = new Pergunta();
        pergunta12.pergunta = "Noé já tinha seissentos anos no dia do Dilúvio.";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "Gn 7.6";


        Pergunta pergunta13 = new Pergunta();
        pergunta13.pergunta = "Depois do Dilúvio as águas demoraram 150 dias para baixarem.";
        pergunta13.correta = "V";
        pergunta13.a = "";
        pergunta13.b = "";
        pergunta13.c = "";
        pergunta13.dica = "Gn 8.3";


        Pergunta pergunta14 = new Pergunta();
        pergunta14.pergunta = "Deus disse que não castigaria mais o ser humano com :";
        pergunta14.correta = "C";
        pergunta14.a = "Fogo";
        pergunta14.b = "Terremoto ";
        pergunta14.c = "Dilúvio ";
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
        pergunta.pergunta = "Sarai era estéril; não tinha filhos.";
        pergunta.correta = "V";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";
        pergunta.dica = "Gênesis 11:30";


        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "Qual é o nome a cidade natal de Abraão?";
        pergunta1.correta = "A";
        pergunta1.a = "Ur";
        pergunta1.b = "Harã";
        pergunta1.c = " Canaã";
        pergunta1.dica = "Gn 11.31";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Por que Abraão entrou em Canaã, percorrendo-a de ponta a ponta, e chegou no Egito depois que Deus mostrou-lhe que Canaã era a terra da promessa?";
        pergunta2.correta = "A";
        pergunta2.a = "Porque havia fome em  Canaã.";
        pergunta2.b = "Porque Deus lhe ordenou que visitasse o Egito.";
        pergunta2.c = "Porque não era tempo de morar em Canaã.";
        pergunta2.dica = "Gn 12.10";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Abraão não estava sozinho nessa peregrinação, com ele estavam sua esposa, seus servos e seu sobrinho Ló.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 13.1";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Ao sair do Egito, Abraão voltou para o lugar onde tinha armado sua tenda quando conheceu Canaã.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "Gn  13.3";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Por que houve contenda entre os pastores do gado de Abrão e os pastores do gado de Ló?";
        pergunta5.correta = "B";
        pergunta5.a = "Porque eram de linhagem diferente.";
        pergunta5.b = "Ambos tinham muitos animaiss e bagagem";
        pergunta5.c = "Os pastores de Ló não queriam viver em Canaã.";
        pergunta5.dica = "Gn 13.6";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Por que Ló escolheu morar em Sodoma?";
        pergunta6.correta = "A";
        pergunta6.a = "Porque suas canpinas eram bonitas.";
        pergunta6.b = "Abraão sugeriu-lhe.";
        pergunta6.c = "Porque ele gostou de seus habitantes.";
        pergunta6.dica = "Gn 13.10,11";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Abraão  aceitava presente de reis porque timha um pacto com Deus sobre depemder sempre da Sua provisão.";
        pergunta7.correta = "F";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";
        pergunta7.dica = "Gn 14.22,23";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Deus disse a Abraão que sua descendência seria como:";
        pergunta8.correta = "C";
        pergunta8.a = "A areia do mar";
        pergunta8.b = "As folhas de uma árvore";
        pergunta8.c = "As estrelas do céu";
        pergunta8.dica = "Gn 15.5";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Abraão foi informado por Deus que sua descendência formaria um povo e herdaria Canaã após cinco gerações. ";
        pergunta9.correta = "F";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 15.15,16";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Qual é o significado do nome Abraão ?";
        pergunta10.correta = "B";
        pergunta10.a = "Pai de ima terra que mama leite e mel.";
        pergunta10.b = "Pai de muitas nações ";
        pergunta10.c = "Pai abençoado ";
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
        pergunta1.pergunta = "Abraão estava preocupado porque era velho e não tinha filhos, mas não comentou isso com seu amigo, Deus...";
        pergunta1.correta = "F";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Por que Deus acabou se agradando de Agar ter o filho de Abraão?";
        pergunta2.correta = "B";
        pergunta2.a = "Porque Sara não teve fé";
        pergunta2.b = "Porque Sara a afligiu.";
        pergunta2.c = "Porque Abraão não teve fé.";
        pergunta2.dica = "Gn 16.6, 8,11";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "A circuncisão foi um sinal de aliança para a formação da nação gerada em Abraão, que entendeu isso mais que Moisés.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 17. 10, 23; Êx 4.24,25";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "A atitude de Abraão com os anjos  que foram anunciar o filho herdeiro mostrou:";
        pergunta4.correta = "B";
        pergunta4.a = "Cordialidade recíproca ";
        pergunta4.b = "Admiração a Deus";
        pergunta4.c = "Etiqueta social";
        pergunta4.dica = "Gn 18.3,17, 19";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "A intercessão de Abraão por Sodoma no diálogo com Deus mostra:";
        pergunta5.correta = "B";
        pergunta5.a = "Descontração ";
        pergunta5.b = "Reverência ";
        pergunta5.c = "Euforia";
        pergunta5.dica = "Gn 18.27";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Deus era tão amigo de Abraão que a sua esposa de homens poderosos. Mas Abraão não mentia diante deles quando dizia, por precaução, que Sara era sua irmã.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 20.2, 3, 12";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Quantos anos tinha Abraão quando Isaac, filho da promessa, nasceu?";
        pergunta7.correta = "A";
        pergunta7.a = "100 anos";
        pergunta7.b = "150 anos";
        pergunta7.c = "90 anos";
        pergunta7.dica = "Gn 21.5";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Depois de tanto tempo tendo Deus como amigo, Abraão confiou  que Deus não deixaria ele sacrificar seu filho.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 22.5,8";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Como Abraão quis que seu servo lhe garantisse que buscaria uma esposa para Isaque conforme ele orientou?";
        pergunta9.correta = "B";
        pergunta9.a = "Assimando um documento";
        pergunta9.b = "Recuperando dados. Aguarde alguns segundos e tente cortar ou copiar novamente.";
        pergunta9.c = "Dando um diamante ao servo ";
        pergunta9.dica = "Gn 24.2,3";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Abraão morreu já em ditosa velhice com:";
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
        pergunta1.pergunta = "O nome Jacó (Yacov), que  quer dizer segurava o calcanhar tem a ver com  o modo como  ele veio à luz.";
        pergunta1.correta = "V";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 25.26";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Jacó teve seu nome mudado, por Deus para Ismael, por isso a confederação  que as tribos que filhos e netos dele formaram  foi chamada Ismael.";
        pergunta2.correta = "F";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = "Gn 35.10";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Raquel era a preferida de Jacó. Dela nasceu a maioria dos filhos que formaram as tribos de Israel. ";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 35.24";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Por que Jacó fez uma reforma espiritual dentro da família depois que deixou a casa do seu sogro?";
        pergunta4.correta = "A";
        pergunta4.a = "Para nenhum tipo de idolatria tirar Deus do centro da vida deles.";
        pergunta4.b = "Porque Raquel  lhe pediu isso.";
        pergunta4.c = "Todas as anteriores";
        pergunta4.dica = "Gn 35.4";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Depois de toda a aflição que Labão causou a Jacó, ele se humilhou e fez um pacto de paz com Jacó.";
        pergunta5.correta = "V";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 31.44";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Dentre os descendentes de Esaú estiveram os amalequitas e os edonitas.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 36.8, 12";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O filho preferido de Jacó era: ";
        pergunta7.correta = "B";
        pergunta7.a = "Judá";
        pergunta7.b = "José";
        pergunta7.c = "Benjamim";
        pergunta7.dica = "Gn 37.3";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Os irmão de José o invejaram pelo sonho de revelação que teve. mas Jacó, mesmo achando estranho o sonho, apenas guardou a dúvida  no coração .";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 37.11";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Jacó pensou que um animal havia despedaçado José, e ficou  muitos anos sem ele.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "Gn 37.33";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Passados muitos anos, quando reencontrou-se com josé, Jacó respondei ao rei do Egito:";
        pergunta10.correta = "B";
        pergunta10.a = "Obrigado por perguntar a minha idade. ";
        pergunta10.b = "...foram poucos e maus os dias dos anos da minha peregrinação";
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
        pergunta12.pergunta = "        Quando José tinha dezessete anos, pastoreava os rebanhos com os seus irmãos";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "Gênesis 37:2";



        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "José nasceu de:";
        pergunta1.correta = "C";
        pergunta1.a = "Zilpa";
        pergunta1.b = "Bila";
        pergunta1.c = "Raquel";
        pergunta1.dica = "Gen. 46.19";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "O zelo de José pela justiça fez com que ele fosse visto como o que vulgarmente chamam, hoje, em nossa cultura, de:";
        pergunta2.correta = "C";
        pergunta2.a = "Descolado";
        pergunta2.b = "De boa";
        pergunta2.c = "Dedo duro";
        pergunta2.dica = "Gn 37.2";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Os sonhos de José eram: ";
        pergunta3.correta = "B";
        pergunta3.a = "Desejos de vencer na vida ";
        pergunta3.b = "Revelações de Deus";
        pergunta3.c = "Preocupações ";
        pergunta3.dica = "Gn 37.9; 42.9";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Para quem os irmãos de José o venderam?";
        pergunta4.correta = "A";
        pergunta4.a = "Para caravaneiros comerciantes que compraram José e o venderam no Egito.";
        pergunta4.b = "Para o rei do Egito";
        pergunta4.c = "Para os filisteus";
        pergunta4.dica = "Gn 37.36";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Por que o patrão de José prosperou enquanto José trabalhou em sua casa?";
        pergunta5.correta = "A";
        pergunta5.a = "Porque Deus fez isso por amor a José.";
        pergunta5.b = "Porque José tinha capacidade de administrar. ";
        pergunta5.c = "Porque José interpretou seus sonhos.";
        pergunta5.dica = "Gn 39.5";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O patrão de José  gostava muito dele, mas fiscalizava  tudo o que ele fazia.";
        pergunta6.correta = "F";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 39.6";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Por que José rejeitou os encantos da esposa do seu patrão? ";
        pergunta7.correta = "C";
        pergunta7.a = "Por tenor a Deus";
        pergunta7.b = "Porque pensou no seu futuro";
        pergunta7.c = "Por que lhe doeu trair seu senhor terreno e seu Senhor  supremo, Deus.";
        pergunta7.dica = "Gn 39.9";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "José era tão amado por Deus que mesmo na prisão Deus o fez administrar.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "Gn 39.22";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "quantos sonhos José interpretou na prisão ?";
        pergunta9.correta = "B";
        pergunta9.a = "Um";
        pergunta9.b = "Dois";
        pergunta9.c = "Três";
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
        pergunta2.b = "Reivindicaram para eles os poços que seus pastores  cavaram.";
        pergunta2.c = "Jogaram pedras nele";
        pergunta2.dica = "Gn 26.14-23";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Deus visitou Isaque e o confortou da aflição que os homens lhe causaram.";
        pergunta3.correta = "V";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Havia naquele tempo pessoas   poderosas  sensatas ao reconhecerem a benção de Deus sobre Isaque.";
        pergunta4.correta = "V";
        pergunta4.a = "";
        pergunta4.b = "";
        pergunta4.c = "";
        pergunta4.dica = "";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Isaque e sua esposa, Rebeca, não gostaram das mulheres que qual filho escolheu?";
        pergunta5.correta = "A";
        pergunta5.a = "Esaú";
        pergunta5.b = "Jacó";
        pergunta5.c = "Noé";
        pergunta5.dica = "Gn 26.35";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "Naquela época não existia óculos. Grande parte das pessoas muito idosas, como Isaque quando envelheceu, já não enxergava mais.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Para preparar-se para dar a benção da primogenitura a Esaú, Isaque pediu que ele lhe servisse: ";
        pergunta7.correta = "A";
        pergunta7.a = "Carne cozida ";
        pergunta7.b = " Um churrasco";
        pergunta7.c = "Uma sopa";
        pergunta7.dica = "Gn 27.3,.4";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Isaque esqueceu-se de orientar Jacó a não casar-se com mulher Cananéia.";
        pergunta8.correta = "F";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "Passados muitos anos Isaque viu seus filhos novamente já com seus netos.";
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
        pergunta.pergunta = "Depois que saiu da Arca, Noé criou animais.";
        pergunta.correta = "F";
        pergunta.a = "";
        pergunta.b = "";
        pergunta.c = "";
        pergunta.dica = "Gn 9.20";


        Pergunta pergunta1 = new Pergunta();
        pergunta1.pergunta = "A Bíblia diz que Noé plantou uma:";
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
        pergunta3.pergunta = "Quantos anos Noé viveu?";
        pergunta3.correta = "A";
        pergunta3.a = "650 anos ";
        pergunta3.b = "120 anos";
        pergunta3.c = "80 anos";
        pergunta3.dica = "Gn 9.29";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Noé amaldiçoou Cam ou Canaã?";
        pergunta4.correta = "B";
        pergunta4.a = "Cam";
        pergunta4.b = "Canaã";
        pergunta4.c = "Os dois";
        pergunta4.dica = "Gn 9.25";


        Pergunta pergunta5 = new Pergunta();
        pergunta5.pergunta = "Antes do Dilúvio os animais não viviam pacificamente com o ser humano.";
        pergunta5.correta = "F";
        pergunta5.a = "";
        pergunta5.b = "";
        pergunta5.c = "";
        pergunta5.dica = "Gn 9.2";


        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O povo africano não descende de Canaã, mas sim os cananeus.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 10.15-19";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O lugar onde a geração da Torre de Babel recebeu o nome de Babel porque:";
        pergunta7.correta = "C";
        pergunta7.a = "Eram pecadores";
        pergunta7.b = "Eram nômades";
        pergunta7.c = "Ali Deus Dividiu as línguas";
        pergunta7.dica = "Gn 11.9";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Os judeus (israelitas) são descendentes de Sem.";
        pergunta8.correta = "V";
        pergunta8.a = "";
        pergunta8.b = "";
        pergunta8.c = "";
        pergunta8.dica = "";

        Pergunta pergunta9 = new Pergunta();
        pergunta9.pergunta = "A cidade de Nínive foi edificada por um descendente de Cam.";
        pergunta9.correta = "V";
        pergunta9.a = "";
        pergunta9.b = "";
        pergunta9.c = "";
        pergunta9.dica = "";




        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Qual dessas três coisas Deus viu necessárilo criar regra de proibição para ela logo após o Dilúvio:";
        pergunta10.correta = "C";
        pergunta10.a = "O furto ";
        pergunta10.b = "A idolatria ";
        pergunta10.c = "Qualquer tipo de homicídio.";
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
        pergunta1.pergunta = "Os dias da criação foram divididos por dois períodos: tarde e manhã.";
        pergunta1.correta = "A";
        pergunta1.a = "";
        pergunta1.b = "";
        pergunta1.c = "";
        pergunta1.dica = "Gn 1.5; 1.8; 1.13; 1.19;  1.23; 1.31.";


        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Deus fez o sol para marcar o período diúrno.";
        pergunta2.correta = "A";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";
        pergunta2.dica = " Gn 1.16";


        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "A lua é chamada de luminar maior.";
        pergunta3.correta = "F";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";
        pergunta3.dica = "Gn 1.16";


        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Antes de ser criada a fauna e a flora a terra estava submersa pelas águas.";
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
        pergunta6.pergunta = "Deus criou o homem  com mais alguém.";
        pergunta6.correta = "V";
        pergunta6.a = "";
        pergunta6.b = "";
        pergunta6.c = "";
        pergunta6.dica = "Gn 1.26; Jo 1.3.";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "O trabalho do homem era cultivar a terra do jardim e protegê-lo";
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
        pergunta12.pergunta = "Adão deu à sua mulher o nome de Eva, pois ela seria mãe de toda a humanidade.";
        pergunta12.correta = "V";
        pergunta12.a = "";
        pergunta12.b = "";
        pergunta12.c = "";
        pergunta12.dica = "Gênesis 3:20";

        Pergunta pergunta10 = new Pergunta();
        pergunta10.pergunta = "Deus colocou dois guardiões na entrada do caminho que levava até a árvore da vida.";
        pergunta10.correta = "V";
        pergunta10.a = "";
        pergunta10.b = "";
        pergunta10.c = "";
        pergunta10.dica = "Gn 3.22 a 24";


        Pergunta pergunta11 = new Pergunta();
        pergunta11.pergunta = "para que o homem não comesse mais do seu fruto e conseguisse viver para sempre.";
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
        pergunta1.pergunta = "Mesmo mudo,Zacarias enfrentou a multidão para registrar seu filho ( v 62,63)";
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
        pergunta4.pergunta = " Zacarias tinha fé que a Paz ia chegar (v 78)";
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
        pergunta2.pergunta = "Maria se aprontou e foi depressa para uma cidade que ficava na região montanhosa da Judeia. ";
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
        pergunta5.pergunta = "Quando Isabel ouviu a saudação de Maria, o que aconteceu? ";
        pergunta5.correta = "A";
        pergunta5.a = "João Batista se mexeu ";
        pergunta5.b = "Jesus se mexeu ";
        pergunta5.c = "ela ficou com inveja ";

        Pergunta pergunta6 = new Pergunta();
        pergunta6.pergunta = "O que Isabel fez quando ficou cheio do ES";
        pergunta6.correta = "B";
        pergunta6.a = "Falou em Linguas";
        pergunta6.b = "Falou belas palavras";
        pergunta6.c = "Profetizou";

        Pergunta pergunta7 = new Pergunta();
        pergunta7.pergunta = "Então disse Maria: Minha alma engrandece ao Senhor";
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
        pergunta1.b = "Belém";
        pergunta1.c = "Nazaré";

        Pergunta pergunta2 = new Pergunta();
        pergunta2.pergunta = "Maria era muito velha quando Deus a escolheu ";
        pergunta2.correta = "B";
        pergunta2.a = "";
        pergunta2.b = "";
        pergunta2.c = "";

        Pergunta pergunta3 = new Pergunta();
        pergunta3.pergunta = "Maria tinha uma profissão muito importante";
        pergunta3.correta = "B";
        pergunta3.a = "";
        pergunta3.b = "";
        pergunta3.c = "";

        Pergunta pergunta4 = new Pergunta();
        pergunta4.pergunta = "Maria  estava noiva de José. ";
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
        pergunta7.pergunta = "Jesus significa Deus é a Salvação";
        pergunta7.correta = "A";
        pergunta7.a = "";
        pergunta7.b = "";
        pergunta7.c = "";

        Pergunta pergunta8 = new Pergunta();
        pergunta8.pergunta = "Maria acreditou que ia ficar Grávida";
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

        Pergunta perguntaModelo = new Pergunta("João Batista foi o maior ser humano dentre os nascidos de mulher ? ", "A", "", null, null);
        perguntaModelo.dica = "(Lucas 7:28) 28 Porque eu vos digo, que dentre os nascidos de mulheres, não há maior profeta que João o Batista; mas o menor no Reino dos céus é maior que ele. ";

        Pergunta pergunta2 = new Pergunta("Zacarias era o nome do Pai de João Batista", "A", "", "", null,"2 Sendo Anás e Caifás os sumos sacerdotes, foi feita a palavra de Deus a João, filho de Zacarias, no deserto.  ");
        Pergunta pergunta3 = new Pergunta("O nome da mãe de João Batista era Maria", "B", "", null, null,"5 Houve nos dias de Herodes, rei da Judeia, um sacerdote, por nome Zacarias, da ordem de Abias; e sua mulher das filhas de Arão, e [era] seu nome Isabel. ");
        Pergunta pergunta4 = new Pergunta("Como era a vida do casal ?", "A", "Correta", "bagunça", "Errada", "6 E eram ambos justos diante de Deus, andando em todos os mandamentos e preceitos do Senhor sem repreensão. ");
        Pergunta pergunta5 = new Pergunta("Zacarias e Isabel eram Jovens ", "B", "", "", "", "7 E não tinham filhos, porque Isabel era estéril, e ambos tinham muitos anos de vida. ");
        Pergunta pergunta6 = new Pergunta("Zacarias sentiu medo ao ver o Anjo", "A", "", "", "", "12 E Zacarias vendo [-o] , ficou perturbado, e caiu medo sobre ele. ");
        Pergunta pergunta7 = new Pergunta("O que joão não podia Beber", "B", "Refrigerante", "Vinho", "Água", "15 Porque [ele] será grande diante do Senhor, e não beberá vinho, nem bebida alcoólica, e será cheio do Espírito Santo, até desde o ventre de sua mãe. ");

        Pergunta pergunta8 = new Pergunta("Qual foi a função de João Batista", "B", "Levita", "Precursor do Messias", "Pastor");
        Pergunta pergunta9 = new Pergunta("Gabriel era o nome do Anjo que apareceu para Zacarias", "A", "", "", "", "19 E respondendo o anjo, disse-lhe: Eu sou Gabriel, que fico presente diante de Deus, e fui mandado para falar a ti, e para te dar estas boas notícias. ");
        Pergunta pergunta10 = new Pergunta("Zacarias ficou ____ porque não acreditou no anjo", "mudo", "", "", "", "20 E eis que tu ficarás mudo, e não poderás falar, até o dia em que estas coisas aconteçam, porque não creste nas minhas palavras, que se cumprirão a seu tempo. ");
        Pergunta pergunta11 = new Pergunta("O que aconteceu com Isabel pouco tempo depois ?", "C", "Muda", "Triste", "Grávida");



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




        Pergunta pergunta1 = new Pergunta("Evangelho significa Boas Notícias?", "A", "", "", "Sim, você pode fazer nosso curso de grego");

        Pergunta pergunta2 = new Pergunta("Para que serve um evangelho   ?", "B", "Para salvar todas as pessoas", "para salvar todos os que creem", "Para contar uma estória");
        pergunta2.dica = "Porque não me envergonho do Evangelho de Cristo, pois é o poder de Deus para a salvação de todo aquele que crê... (Romanos 1. 16)";

        Pergunta pergunta3 = new Pergunta("Os  livros de Lucas e Atos foram escritos entre os anos 59 e 63 d.C.", "A", "", null, null,"SIM");


        Pergunta pergunta4 = new Pergunta("O Evangelho de Lucas foi escrito em grego.", "A", "", null, null,"Simn no nosso curso de grego voCê vai ler direto do original");


        Pergunta pergunta5 = new Pergunta("Os evangelhos de Mateus, Marcos, Tiago e João são os evangelhos que estão na Bíblia ?", "B", "", "", "");
        pergunta5.dica = "Os evangelhos são os 4 primeiros livro do Novo testamento. Pode ver na sua Bíblia :)";


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
        pergunta1.dica = "(Atos 1.1)  Eu fiz o primeiro livro, ó Teófilo, sobre todas as coisas que Jesus começou, tanto a fazer como a ensinar; )";
        Pergunta pergunta2 = new Pergunta("Qual foi a principal profissão de Lucas  ?", "A", "Medico", "Veterinário", "Pintor");
        pergunta2.dica = "(Colossenses 4:14 14 Lucas, o médico amado, ele vos saúda;)";
        Pergunta pergunta3 = new Pergunta("Seu nome aparece três vezes  no N.T. ", "A", "", null, null);
        pergunta3.dica = "(Cl 4.14 - 2 Tm 4.11 - Fm 24)";
        Pergunta pergunta4 = new Pergunta("Lucas participou de Missões com o apóstolo Paulo para pregar o evangelho  ?", "A", "", null, null);
        pergunta4.dica = " (2 Timóteo 4:11) 11 para o qual eu fui posto como pregador e apóstolo, e instrutor dos gentios. ";
        Pergunta pergunta5 = new Pergunta("O que Lucas fez para escrever o Evangelho  ?", "B", "Inventou", "Investigou", "Copiou", "3 Pareceu-me bom que também eu, que tenho me informado com exatidão desde o princípio, escrevesse [estas coisas] em ordem para ti, excelentíssimo Teófilo, ");
        Pergunta pergunta6 = new Pergunta("Para quem Lucas escreveu ?", "C", "Alexandre", "Herodes", "Teófilo", "3 Pareceu-me bom que também eu, que tenho me informado com exatidão desde o princípio, escrevesse [estas coisas] em ordem para ti, excelentíssimo Teófilo, ");
       
        Pergunta pergunta7 = new Pergunta("O que Lucas Contou ?", "B", "Mentiras sobre Jesus", "Verdade sobre Jesus", "Lendas sobre Jesus", "4 Para que conheças a certeza das coisas de que foste ensinado. ");


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

        tema1.mensagemInicial = "Olá meu nome é Mateus, o filho de Alfeu, eu era coletor de impostos, e me chamava Levi, mas  minha vida mudou para sempre quando atendi ao chamado de Jesus Cristo.  Agora Sou um dos Doze Apóstolos, por isso vou te contar tudo que vivi com Jesus. Mas antes vou te mostrar Como Jesus cumpre a profecia para ser o Messias.";


        Tema tema2 = new Tema("2", "Ensinamentos", "", "Agora que sabemos por que Jesus é o Messias, Vou contar alguns de seus ensinamentos que foram revolucionários e vão durar para sempre", "", "20");

        Tema tema3 = new Tema("3", "Missões", "", " Jesus além de ensinar com palavras, ensinava com ações Veja alguma de suas missões", "", "50");

        Tema tema4 = new Tema("4", "Parábolas", "", "As parábolas de jesus são marcantes, consegue nos ensinar de forma fácil assuntos complexos", "", "80");



        Tema tema5 = new Tema("5", "Seguindo Jesus", "", "Seguir Jesus não é fácil, alguns se perdem , outros  se desviam mas voltam, vou te contar algumas histórias de seus seguidores", "", "120");


        Tema tema6 = new Tema("6", "condenação", "", "Jesus cumpriu toda a escritura , inclusive o sofrimento que havia na profecia", "", "170");


        Tema tema7 = new Tema("7", "ressurreição ", "", "Mas o melhor  é que ele ressuscitou , agora se prepare para a maior acontecimento de todos os tempos a ressureição de Jesus", "", "200");


        //Tema tema4 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema5 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema6 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

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
        Tema tema1 = new Tema("1", "Quem é Lucas ?", "https://www.youtube.com/watch?v=7SeGSVHUm-M&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=1", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        tema1.mensagemInicial = "Olá, meu nome é Lucas, sou o médico que estava com o apóstolo paulo." +
"Preciso entregar uma carta para Teófilo." + "Nessa carta vou escrever as histórias que estão falando sobre Jesus." +
"Vou contar o evangelho Mas para isso preciso fazer uma grande investigação," +
"falar com as pessoas que viram Jesus, saber exatamente a verdade. (Lucas 1 :1 - 4) Você pode me ajudar nessa grande missão ?";


        Tema tema2 = new Tema("2", "O que é um evangelho ?", "https://www.youtube.com/watch?v=7SeGSVHUm-M&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=1", "Muito bom, agora que nos conhecemos, precisamos entender o que vamos escrever, não será uma carta comum, será um evangelho, !!!  Vamos entender o que é isso  ", "", "7");

        Tema tema3 = new Tema("3", "João Batista", "https://www.youtube.com/watch?v=HTO4wokqrtw&list=PLUfKJXiDnXqpTmi8PkLn4Ve2uiMTZQ00o&index=2", " Fiquei sabendo que antes de Jesus, nasceu um homem,João Batista, que teve a missão de preparar o caminho para o messias, vamos conhecê-lo!!!", "", "13");

        Tema tema4 = new Tema("4", "Maria mãe de Jesus", "https://www.youtube.com/watch?v=lzTjkitye8w&t=1s", "Muito bem, conhecemos João o precursor de Jesus, agora vamos conhecer uma mulher que foi agraciada por Deus, A mãe de Jesus", "", "200");



        Tema tema5 = new Tema("5", "Maria visita Isabel", "https://www.youtube.com/watch?v=60gPW7VqE9U&t=1s", "Fiquei sabendo que Maria e ISABEL SE CONHECIAM,  SERÁ QUE ERAM AMIGAS? Vamos investigar isso", "", "50");


        Tema tema6 = new Tema("6", "O cântico de Zacarias (1 : 57-80)", "https://www.youtube.com/watch?v=u2aPzf8opsg", "Que legal essa amizade de Maria e Isabel, e Zacarias será que estava feliz com o nascimento de seu filho", "", "50");


        Tema tema7 = new Tema("7", "Nascimento de Jesus", "https://www.youtube.com/watch?v=u2aPzf8opsg", "(Lucas 2 : 1-7)", "", "80");


        //Tema tema4 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema5 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

        //Tema tema6 = new Tema("1", "Quem é Lucas ?", "", "Antes de iniciar nossa jornada vamos nos conhecer ? Eu sou Lucas e quero te contar um pouco sobre mim", "", "0");

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





