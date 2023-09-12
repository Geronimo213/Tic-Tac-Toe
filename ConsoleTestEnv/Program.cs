
var board = new char[3,3];
var pieces = new char[2] { 'X', 'O' };
var score = new int[2] { 0, 0 };
bool play = true;
int player = 1;

while (true)
{
    PlayGame(board, pieces, score);
    Console.WriteLine($"Current score: \n Player 1: {score[0]} Player 2: {score[1]}");
    Console.WriteLine("Press any key to play again.");
    Console.ReadKey();
}

return;

static void PlayGame(char[,] board, char[] pieces, int[] score)
{
    bool play = true;
    int player = 1;

    SetBoard(board);

    Console.Clear();
    PrintBoard(board);
    while (play)
    {
        Console.WriteLine($"Player {player}'s turn. Choose a space: ");
        string? rawInput = Console.ReadLine();
        if (!int.TryParse(rawInput, out int input))
        {
            input = 0;
        }

        switch (input)
        {
            case > 0 and < 10:
                int row = (input - 1) / 3;
                int col = (input - 1) % 3;

                if (board[row,col] != ' ')
                {
                    Console.WriteLine("Space is taken. Choose another!");
                    break;
                }

                else
                {
                    board[row, col] = pieces[player - 1];
                    if (CheckWin(board))
                    {
                        Console.Clear();
                        PrintBoard(board);
                        Console.WriteLine($"Player {player} wins!");
                        score[player - 1]++;
                        play = false;
                        break;
                    }

                    else if (CheckTie(board))
                    {
                        Console.Clear();
                        PrintBoard(board);
                        Console.WriteLine("Scratch! Neither player wins!");
                        play = false;
                        break;
                    }
                    else
                    {
                        player = SwitchPlayer(player);
                        Console.Clear();
                        PrintBoard(board);
                        break;
                    }
                }

            case < 1 or > 9:
            {
                Console.WriteLine("Invalid input. Please select a space 0-9. Press enter to continue.");
                break;
            }
        }
    }
}
static int SwitchPlayer(int player)
{
    return player == 1 ? 2 : 1;
}
static void SetBoard(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            //board[i,j] = (char)(('0' + i * board.GetLength(1)) + j + 1);
            board[i, j] = ' ';
        }
    }
}

static bool CheckTie(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (board[i, j] != 'X' && board[i, j] != 'O')
            {
                return false;
            }
        }
    }

    return true;
}
static bool CheckWin(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        if (board[i,0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
        {
            return true;
        }

        if (board[0,i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
        {
            return true;
        }
    }

    if (board[0,0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
    {
        return true;
    }

    if (board[0,2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
    {
        return true;
    }

    return false;
}

static void PrintBoard(char[,] board) //Function to create 
{
    Console.WriteLine("   |   |   ");
    Console.WriteLine(" {0} | {1} | {2} ", board[0,0], board[0,1], board[0,2]);
    Console.WriteLine("___|___|___");

    Console.WriteLine("   |   |   ");
    Console.WriteLine(" {0} | {1} | {2} ", board[1,0], board[1,1], board[1,2]);
    Console.WriteLine("___|___|___");

    Console.WriteLine("   |   |   ");
    Console.WriteLine(" {0} | {1} | {2} ", board[2,0], board[2,1], board[2,2]);
    Console.WriteLine("   |   |   ");

}