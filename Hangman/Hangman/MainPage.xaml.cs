using System.ComponentModel;

namespace Hangman;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    #region  UI Properties

    public string Message
    {
        get => message; 
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

    public List<char> Characters
    {
        get => characters; set
        {
            characters = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Fields
    List<string> words = new List<string>(){
       "hola",
       "adios"
};
    string answer = "";
    private string spotlight;
    List<char> guessed = new List<char>();
    private List<char> characters = new List<char>();
    private string message;
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

    }

    private void CheckIfWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "You Win !!!";
        }
    }
}

