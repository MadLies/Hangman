using System.ComponentModel;

namespace Hangman;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    #region  UI Properties

    public string Message
    {
        get
        {
            return message;
        }

        set
        {
            message = value;
            OnPropertyChanged();
        }
    }
    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
            OnPropertyChanged();
        }
    }

    public string GameStatus
    {
        get => gameStatus; set
        {
            gameStatus = value;
            OnPropertyChanged();
        }
    }
    public List<char> Characters
    {
        get => characters; set
        {
            characters = value;
            OnPropertyChanged();
        }
    }

    public string CurrentImage
    {
        get => currentImage; set
        {   
            currentImage = value;
            OnPropertyChanged();
        }
    }


    #endregion
    #region Fields
    List<string> words = new List<string>(){
      "shatter",
    "default",
    "warm",
    "sword",
    "throne",
    "leak",
    "amber",
    "hotdog",
    "auction",
    "resident",
    "immune",
    "hope",
    "decisive",
    "pardon",
    "drama",
    "ceiling",
    "firefighter",
    "first",
    "export",
    "barrel",
    "affect",
    "spite",
    "name",
    "cotton",
    "silence",
    "condition",
    "lead",
    "conspiracy",
    "intervention",
    "investment",
    "dentist",
    "scramble",
    "sculpture",
    "allowance",
    "environment",
    "margin",
    "start",
    "spin",
    "broccoli",
    "friendly",
    "science",
    "relate",
    "finish",
    "drop",
    "slump",
    "care",
    "fork",
    "mass",
    "stool",
    "basic"
};


    string answer = "";
    private string spotlight;
    List<char> guessed = new List<char>();
    private List<char> characters = new List<char>();
    private string message = "You Can Is To Easy" ;
    int mistakes = 0;
    private string gameStatus = "Erros: 0 of 6";
    int maxWrong = 6;
    private string currentImage = "img0.jpg" ;

    #endregion


    public MainPage()
    {
        InitializeComponent();
        characters.AddRange("abcdefghijklmnopqrstuvwxyz");
        BindingContext = this;
        PickWord();
        CalculateWord(answer, guessed);
    }

    #region Game  Engine
    private void PickWord()
    {
        answer = words[new Random().Next(0, words.Count)];

    }

    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
        Spotlight = string.Join(" ", temp);
    }
    #endregion

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var letter = btn.Text;
            btn.IsEnabled = false;
            HandleGuess(letter[0]);
        }
    }

    private void HandleGuess(char letter)
    {
        if (guessed.IndexOf(letter) == -1)
        {
            guessed.Add(letter);
        }
        if (answer.IndexOf(letter) >= 0)
        {
            CalculateWord(answer, guessed);
            CheckIfWon();
        }
        else if (answer.IndexOf(letter) == -1)
        {
            mistakes++;
            CurrentImage = $"img{mistakes}.jpg";
            UpdateStatus();
            CheckIfGameLost();
        }

    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {
            Message = "You Lost!!!";
            DisableLetters();
        }
    }

    private void DisableLetters()
    {
       foreach (Button children in lettersContainer.Children)
        {
            if(children!= null)
            {
                children.IsEnabled=false;
            }
        }
    }
    private void EnableLetters()
    {
        foreach (Button children in lettersContainer.Children)
        {
            if (children != null)
            {
                children.IsEnabled = true;
            }
        }
    }
    private void UpdateStatus()
    {
        GameStatus = $"Erros: {mistakes} of {maxWrong}";
    }
    private void CheckIfWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "You Win !!!";
            DisableLetters();
        }
    }

    private void ResetGame(object sender, EventArgs e)
    {
        mistakes= 0;
        guessed= new List<char>();
        Message = "You Can Is To Easy";
        CurrentImage = "img0.jpg";
        PickWord();
        CalculateWord(answer, guessed);
        UpdateStatus();
        EnableLetters();
    }
}

