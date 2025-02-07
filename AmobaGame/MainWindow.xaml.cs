using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmobaGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] gameBoardButtons = new Button[3, 3];
        private string currentPlayer = "X";
        private int playerXScore = 0;
        private int playerOScore = 0;

        public MainWindow()
        {
            InitializeComponent();
            GenerateGameBoard();
        }

        private void GenerateGameBoard()
        {
            GameBoard.Children.Clear();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button button = new Button();
                    button.Click += GameButton_Click;
                    gameBoardButtons[row, col] = button;
                    GameBoard.Children.Add(button);
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                }
            }
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && string.IsNullOrEmpty(button.Content as string))
            {
                button.Content = currentPlayer;
                button.IsEnabled = false;

                if (CheckWin())
                {
                    MessageBox.Show($"{currentPlayer} győzött!");
                    UpdateScore();
                    RestartGame();
                }
                else if (IsBoardFull())
                {
                    RestartGame();
                }
                else
                {
                    currentPlayer = currentPlayer == "X" ? "O" : "X";
                }
            }
        }

        private bool CheckWin()
        {
            // Sorok ellenőrzése
            for (int row = 0; row < 3; row++)
            {
                if (gameBoardButtons[row, 0].Content != null &&
                    gameBoardButtons[row, 0].Content == gameBoardButtons[row, 1].Content &&
                    gameBoardButtons[row, 1].Content == gameBoardButtons[row, 2].Content)
                {
                    return true;
                }
            }

            // Oszlopok ellenőrzése
            for (int col = 0; col < 3; col++)
            {
                if (gameBoardButtons[0, col].Content != null &&
                    gameBoardButtons[0, col].Content == gameBoardButtons[1, col].Content &&
                    gameBoardButtons[1, col].Content == gameBoardButtons[2, col].Content)
                {
                    return true;
                }
            }

            // Átlók ellenőrzése
            if (gameBoardButtons[0, 0].Content != null &&
                gameBoardButtons[0, 0].Content == gameBoardButtons[1, 1].Content &&
                gameBoardButtons[1, 1].Content == gameBoardButtons[2, 2].Content)
            {
                return true;
            }

            if (gameBoardButtons[0, 2].Content != null &&
                gameBoardButtons[0, 2].Content == gameBoardButtons[1, 1].Content &&
                gameBoardButtons[1, 1].Content == gameBoardButtons[2, 0].Content)
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(gameBoardButtons[row, col].Content as string))
                    {
                        return false;
                    }
                }
            }
            MessageBox.Show("Döntetlen!");
            return true;
        }

        private void UpdateScore()
        {
            if (currentPlayer == "X")
            {
                playerXScore++;
                Player1Score.Text = "X: " + playerXScore.ToString();
            }
            else
            {
                playerOScore++;
                Player2Score.Text = "O: " + playerOScore.ToString();
            }
        }

        private void RestartGame()
        {
            currentPlayer = "X";
            GenerateGameBoard();
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            playerOScore = 0;
            playerXScore = 0;

            Player1Score.Text = "X: " + playerXScore.ToString();
            Player2Score.Text = "O: " + playerOScore.ToString();
            RestartGame();
        }
    }
}