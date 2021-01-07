using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp43
{
    public partial class Form1 : Form
    {
        public const int mapSize = 15;
        public int cellSize = 50;
        public const int countLetters = 7;
        public int[,] Map = new int[mapSize, mapSize];

        public ListBox NoticeResults;

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[] Letters = new Button[countLetters];
        public string[] LettersPlayer1 = new string[countLetters];
        public string[] LettersPlayer2 = new string[countLetters];

        public bool isPlaying = false;
        public string alphabet = "ААААААААААБББВВВВВГГГДДДДДЕЕЕЕЕЕЕЕЕЖЖЗЗИИИИИИИИЙЙЙЙККККККЛЛЛЛМММММНННННННННООООООООООППППППРРРРРРССССССТТТТТУУУФХХЦЧЧШЩЪЫЫЬЬЭЮЯЯЯ";

        public Player player1;
        public Player player2;

        public Button buttonPlayer1;
        public Button buttonPlayer2;

        public int Res1;
        public int Res2;

        public bool IsPlayPlayer1;
        public bool IsPlayPlayer2;


        public Button startButton;
        public Button finishButton;

        public string currentLetter;
        public int currentResults=0;

        public string currentWord;
        public bool IfChangeLetter = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Эрудит";
            Init();
        }

        public void Init()
        {
            //isPlaying = false;
            IfChangeLetter = false;
            CreateMaps();
            player1 = new Player("Player1", Res1, LettersPlayer1);
            player2 = new Player("Player2", Res2, LettersPlayer2);

            buttonPlayer1.Enabled = false;
            buttonPlayer2.Enabled = false;
    }

        public void ChangePlayer()
        {
            if (IsPlayPlayer1)
            {
                buttonPlayer1.BackColor = Color.LightPink;
                buttonPlayer2.BackColor = Color.Red;
                IsPlayPlayer1 = false;
                IsPlayPlayer2 = true;
                NoticeResults.Items.Add("Ход Игрока 2");

                for (int i = 0; i < countLetters; i++)
                {
                    
                    Letters[i].Text = LettersPlayer2[i];
                    
                }
            }
            else if (IsPlayPlayer2)
            {
                buttonPlayer1.BackColor = Color.Red;
                buttonPlayer2.BackColor = Color.LightPink;
                IsPlayPlayer1 = true;
                IsPlayPlayer2 = false;
                NoticeResults.Items.Add("Ход Игрока 1");
                for (int i = 0; i < countLetters; i++)
                {

                    Letters[i].Text = LettersPlayer1[i];

                }
            }
        }
        public void Miss(object sender, EventArgs e)
        {
           
            if (IsPlayPlayer1)
            {
                
                NoticeResults.Items.Add("Игрок 1 пропустил ход. Всего очков " + Res1.ToString());
               
            }
             if (IsPlayPlayer2)
             {

                NoticeResults.Items.Add("Игрок 2 пропустил ход. Всего очков " + Res2.ToString());
            }
            ChangePlayer();
        }


        public void CreateMaps()
        {
            this.Width = mapSize *  cellSize + 370;
            this.Height = (mapSize + 3) * cellSize ;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Map[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 7 && j == 7)
                    {
                        button.BackColor = Color.LightGray;
                    }
                    if (i == 0  && j == 0 || i==7&&j==0||i==14&&j==0||i==0&&j==7||i==0&&j==14||i==7&&j==14||i==14&&j==14||i==14&&j==7)
                    {
                        button.BackColor = Color.Coral;
                    }
                    if (i==1&&j==1||i == 2 && j == 2||i==3&&j==3||i==4&&j==4||i==13&&j==13||i==12&&j==12||i==11&&j==11||i==10&&j==10||i==13&&j==1||i==12&&j==2||i==11&&j==3||i==10&&j==4|| j == 13 && i == 1 || j == 12 && i == 2 || j == 11 && i == 3 || j == 10 && i == 4)
                    {
                        button.BackColor = Color.Blue;
                    }
                    if (i==0&&j==3||i==0&&j==11||i==2&&j==6||i==2&&j==8||i==3&&j==0||i==3&&j==7||i==3&&j==14||i==6&&j==2||i==6&&j==6||i==6&&j==8||i==6&&j==12||i==7&&j==3||i==7&&j==3||i==7&&j==11||i==8&&j==2||i==8&&j==6||i==8&&j==8||i==8&&j==12||i==11&&j==0||i==11&&j==7||i==11&&j==14||i==12&&j==6||i==12&&j==6||i==12&&j==8||i==14&&j==3||i==14&&j==11)
                    {
                        button.BackColor = Color.Green;
                    }
                    if (i==1&&j==5||i==1&&j==9||i==5&&j==1||i==5&&j==13||i==9&&j==1||i==9&&j==13||i==13&&j==5||i==13&&j==9)
                    {
                        button.BackColor = Color.Yellow;
                    }

                    myButtons[i, j] = button;
                    button.Click += new EventHandler(ConfigureLetters);
                    this.Controls.Add(button);
                    
                }
            }

            startButton = new Button();
            startButton.Text = "Начать";
            startButton.Click += new EventHandler(Start);
            startButton.Location = new Point(750,0);
            this.Controls.Add(startButton);

             buttonPlayer1 = new Button();
            buttonPlayer1.Text = "Игрок 1";
            buttonPlayer1.Location = new Point(830, 0);
            this.Controls.Add(buttonPlayer1);

            buttonPlayer2 = new Button();
            buttonPlayer2.Text = "Игрок 2";
            buttonPlayer2.Location = new Point(910, 0);
            this.Controls.Add(buttonPlayer2);

             finishButton = new Button();
            finishButton.Text = "Закончить";
            finishButton.Click += new EventHandler(Finish);
            finishButton.Location = new Point(990, 0);
            finishButton.Enabled = false;
            this.Controls.Add(finishButton);

            for (int i = 0; i < countLetters; i++)
            {
                Button button = new Button();
                button.Location = new Point( i * cellSize+750, 40);
                button.Size = new Size(cellSize, cellSize);
                button.BackColor = Color.White;
                button.Click += new EventHandler(SetCurrentLetter);
                Letters[i] = button;
                this.Controls.Add(button);
            }

            Button myTurn = new Button();
            myTurn.Text = "Слово";
            myTurn.Click += new EventHandler(SetLetters);
            myTurn.Location = new Point(750, 90);
            this.Controls.Add(myTurn);

            Button MissTurn = new Button();
            MissTurn.Text = "Пропустить";
            MissTurn.Location = new Point(830, 90);
            MissTurn.Click += new EventHandler(Miss);
            this.Controls.Add(MissTurn);

            Button ChangeLetters = new Button();
            ChangeLetters.Text = "Заменить";
            ChangeLetters.Location = new Point(910, 90);
            ChangeLetters.Click += new EventHandler(ChangeLetter);
            this.Controls.Add(ChangeLetters);

            Button FinishWord = new Button();
            FinishWord.Text = "Завершить";
            FinishWord.Location = new Point(990, 90);
            FinishWord.Click += new EventHandler(CountPoints);
            this.Controls.Add(FinishWord);

            NoticeResults = new ListBox();
            NoticeResults.Width = 350;
            NoticeResults.Height = 630;
            NoticeResults.Location = new Point(750, 120);
            this.Controls.Add(NoticeResults);

            Button coral = new Button();
            coral.BackColor = Color.Coral;
            coral.Location = new Point(0,750);
            this.Controls.Add(coral);

            Label labelCoral = new Label();
            labelCoral.Location = new Point(80, 755);
            labelCoral.Text = "Х 5";
            labelCoral.Width = 40;
            labelCoral.ForeColor = Color.Black;
            this.Controls.Add(labelCoral);

            Button green = new Button();
            green.BackColor = Color.Green;
            green.Location = new Point(120, 750);
            this.Controls.Add(green);

            Label labelGreen = new Label();
            labelGreen.Location = new Point(200, 755);
            labelGreen.Text = "Х 2";
            labelGreen.Width = 40;
            labelGreen.ForeColor = Color.Black;
            this.Controls.Add(labelGreen);

            Button blue = new Button();
            blue.BackColor = Color.Blue;
            blue.Location = new Point(240, 750);
            this.Controls.Add(blue);

            Label labelBlue = new Label();
            labelBlue.Location = new Point(320, 755);
            labelBlue.Text = "Х 4";
            labelBlue.Width = 40;
            labelBlue.ForeColor = Color.Black;
            this.Controls.Add(labelBlue);

            Button yellow = new Button();
            yellow.BackColor = Color.Yellow;
            yellow.Location = new Point(360, 750);
            this.Controls.Add(yellow);

            Label labelYellow = new Label();
            labelYellow.Location = new Point(440, 755);
            labelYellow.Text = "Х 3";
            labelYellow.Width = 40;
            labelYellow.ForeColor = Color.Black;
            this.Controls.Add(labelYellow);

        }

        public void SetLetters(object sender, EventArgs e)
        {
            isPlaying = false;

        }
        public void Finish(object sender, EventArgs e)
        {
            isPlaying = false;
            startButton.Enabled = true;
            finishButton.Enabled = false;
            if (Res1 > Res2)
            {
               MessageBox.Show("Победил Игрок 1.");
               this.Close();
                
            }
            else if(Res1<Res2)
            {
                MessageBox.Show("Победил Игрок 2.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ничья");
                this.Close();
            }
            
        }

        public void ChangeLetter(object sender, EventArgs e)
        {
            IfChangeLetter = true;
        }

        public void CountPoints(object sender, EventArgs e)
        {
            Random rand = new Random();
            
            
            if (IsPlayPlayer1)
            {
                int Position;
                NoticeResults.Items.Add(" Игрок 1 заработал  Всего "+Res1 + " Слово:"+currentWord);

                for (int i = 0; i < countLetters; i++)
                {
                    if (Letters[i].Text == "")
                    {

                        Position = rand.Next(0, alphabet.Length - 1);

                        Letters[i].Text = alphabet[Position].ToString();
                        LettersPlayer1[i] = Letters[i].Text;
                    }

                }
                currentWord = "";
               
                
            }
            if (IsPlayPlayer2)
            {
                int Position;
                NoticeResults.Items.Add(" Игрок 2 заработал  Всего " + Res2 + " Слово:" + currentWord);

                for (int i = 0; i < countLetters; i++)
                {
                    if (Letters[i].Text == "")
                    {

                        Position = rand.Next(0, alphabet.Length - 1);

                        Letters[i].Text = alphabet[Position].ToString();
                        LettersPlayer2[i] = Letters[i].Text;
                    }

                }
                currentWord = "";

            }

            isPlaying = true;
            ChangePlayer();


        }
        public void SetCurrentLetter(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            Random rand = new Random();
            int Position;
            if (!IfChangeLetter)
            {
                
                if (pressedButton.Text != "")
                {
                    currentLetter = pressedButton.Text;
                    pressedButton.Text = "";
                }
                
            }
            else
            {
               
                IfChangeLetter = false;
                if (IsPlayPlayer1)
                {

                    for (int i = 0; i < countLetters; i++)
                    {
                        if (Letters[i] == pressedButton)
                        {
                            Position = rand.Next(0, alphabet.Length - 1);
                            Letters[i].Text = alphabet[Position].ToString();
                            LettersPlayer1[i] = Letters[i].Text;
                        }
                      
                    }
                    NoticeResults.Items.Add("Игрок 1 заменил букву. Всего очков " + Res1);
                }
                if (IsPlayPlayer2)
                {
                    for (int i = 0; i < countLetters; i++)
                    {
                        if (Letters[i] == pressedButton)
                        {
                            Position = rand.Next(0, alphabet.Length - 1);
                            Letters[i].Text = alphabet[Position].ToString();
                            LettersPlayer2[i] = Letters[i].Text;
                        }

                    }
                    NoticeResults.Items.Add("Игрок 2 заменил букву. Всего очков " + Res2);
                }
                ChangePlayer();
            }
           

           

        }
        public void Start(object sender, EventArgs e)
        {
            isPlaying = true;
            buttonPlayer1.BackColor = Color.Red;
            buttonPlayer2.BackColor = Color.LightPink;
            IsPlayPlayer1 = true;
            IsPlayPlayer2 = false;

            Random rand = new Random();
            int Position;
               
            for (int i = 0; i < countLetters; i++)
            {
                Position = rand.Next(0, alphabet.Length - 1);

                Letters[i].Text = alphabet[Position].ToString();
                LettersPlayer1[i] = Letters[i].Text;
                 

            }

            for (int i = 0; i < countLetters; i++)
            {
                Position = rand.Next(0, alphabet.Length - 1);

                LettersPlayer2[i] = alphabet[Position].ToString();
                
            }
            NoticeResults.Items.Add("Ход Игрока 1");

            startButton.Enabled = false;
            finishButton.Enabled = true;
            

        }

        public void ConfigureLetters(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
           

            if (isPlaying)
            {
                  if (Map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
                    {
                        pressedButton.Text = currentLetter.ToString();
                        Map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
                        currentLetter = "";
                       
                    }
                    else
                    {
                        for (int i = 0; i < countLetters; i++)
                        {
                            if (Letters[i].Text == "")
                            {
                                Letters[i].Text = pressedButton.Text;

                                break;
                            }

                        }
                       
                        pressedButton.Text = "";

                        Map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
                    }
                
            }
            else
            {
                if (pressedButton.Text != "")
                {
                    
                    if (pressedButton.BackColor == Color.White || pressedButton.BackColor == Color.LightGray)
                    { 
                        currentWord += pressedButton.Text;
                        if (IsPlayPlayer1)
                        {
                            Res1 += 1;
                        }
                        if(IsPlayPlayer2)
                        {
                            Res2 += 1;
                        }
                    
                    }
                    if(pressedButton.BackColor == Color.Coral)
                    {
                        currentWord += pressedButton.Text;
                        if (IsPlayPlayer1)
                        {
                            Res1 += 5;
                        }
                        if (IsPlayPlayer2)
                        {
                            Res2 += 5;
                        }
                    }
                    if(pressedButton.BackColor == Color.Blue)
                    {
                        currentWord += pressedButton.Text;
                        if (IsPlayPlayer1)
                        {
                            Res1 += 4;
                        }
                        if (IsPlayPlayer2)
                        {
                            Res2 += 4;
                        }
                    }
                    if(pressedButton.BackColor == Color.Green)
                    {
                        currentWord += pressedButton.Text;
                        if (IsPlayPlayer1)
                        {
                            Res1 += 2;
                        }
                        if (IsPlayPlayer2)
                        {
                            Res2 += 2;
                        }
                    }
                    if (pressedButton.BackColor == Color.Yellow)
                    {
                        currentWord += pressedButton.Text;
                        if (IsPlayPlayer1)
                        {
                            Res1 += 3;
                        }
                        if (IsPlayPlayer2)
                        {
                            Res2 += 3;
                        }
                    }
                    pressedButton.FlatStyle=FlatStyle.Flat;
                    pressedButton.FlatAppearance.BorderColor = Color.Red;
                }
                else
                {
                    MessageBox.Show("В этой ячейке нет буквы!");

                }
            } 


            
           
           
        }
    }
}
