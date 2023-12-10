//Block to initialize base data.
var board = new char[3,3];// Creation of 3x3 board
var pieces = new [] { 'X', 'O' };// Bank of pieces in player1, player2 order
var score = new [] { 0, 0 };

while (true) // Loop to keep starting new game forever.
{
    // Start a new game, passing our created board, pieces to use, and starting score. Waits for confirmation before starting a new game.
    PlayGame(board, pieces, score);
    Console.WriteLine($"Current score: \n Player 1: {score[0]} Player 2: {score[1]}");
    Console.WriteLine("Press any key to play again.");
    Console.ReadKey();
}

void PlayGame(char[,] board, char[] pieces, int[] score)
{
    // Initialize loop case and starting player.
    bool play = true;
    int player = 1;

    // Initialize values on board matrix, clear the console screen, and print the first board.
    SetBoard(board);
    Console.Clear();
    PrintBoard(board);

    // Game loop
    while (play)
    {
        Console.WriteLine($"Player {player}'s turn. Choose a space: ");

        // Attempt to parse input string to int. If unable, set input to 0 (which will reach default case).
        string? rawInput = Console.ReadLine();
        if (!int.TryParse(rawInput, out int input))
        {
            input = 0;
        }

        // Input loop
        switch (input)
        {
            // If the input int is between 0 and 10
            case > 0 and < 10:
                // Acquire [i,j] matrix coordinates from input
                int row = (input - 1) / 3;
                int col = (input - 1) % 3;

                if (board[row,col] != ' ') //Check if chosen space is available
                {
                    Console.WriteLine("Space is taken. Choose another!");
                    break;
                }
                // Input is now vetted

                board[row, col] = pieces[player - 1]; // Place piece

                // Check if placing the piece has put the board into a win condition.
                if (CheckWin(board))
                {
                    Console.Clear();
                    PrintBoard(board);
                    Console.WriteLine($"Player {player} wins!");
                    score[player - 1]++;
                    play = false;
                    break;
                }

                // Check if placing the piece has put the board into a tie condition.
                else if (CheckTie(board))
                {
                    Console.Clear();
                    PrintBoard(board);
                    Console.WriteLine("Scratch! Neither player wins!");
                    play = false;
                    break;
                }

                // In absence of win or tie, switch player, print new board, and keep playing.
                else
                {
                    player = SwitchPlayer(player);
                    Console.Clear();
                    PrintBoard(board);
                    break;
                }

            // If the input int doesn't match a space (1-9), notify them of restriction and re-prompt input.
            default:
            {
                Console.WriteLine("Invalid input. Please select a space 0-9. Press enter to continue.");
                break;
            }
        }
    }
}

// Switch from current player to next player. In hind sight, unnecessary to separate this. But maybe future changes will justify this.
static int SwitchPlayer(int player)
{
    return player == 1 ? 2 : 1;
}

// Initialize each space on board to ' '.
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

// Check for tie condition on given board
static bool CheckTie(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (board[i, j] != 'X' && board[i, j] != 'O') // If any space is blank, there is no tie.
            {
                return false;
            }
        }
    }

    return true;
}

// Check if given board is in a win condition.
static bool CheckWin(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        if (board[i,0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) // For each row, if at least one space is not empty AND all spaces are equal, then win condition exists.
        {
            return true;
        }

        if (board[0,i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i]) // For each column, if at least one space is not empty AND all spaces are equal, then win condition exists.
        {
            return true;
        }
    }

    if (board[0,0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) // If top-left => bot-right diagonal is equal AND at least one is not blank, then win condition exists.
    {
        return true;
    }

    return board[0,2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]; // If top-right => bot-left diagonal is equal AND at least one is not blank, then win condition exists.
}

// Responsible for printing out the board. I would like to add capability to print winning line in color in the future.
 void PrintBoard(char[,] board) //Function to create 
{
    if (score[0] != 0)
    {
        Console.WriteLine($"Score: Player 1 - {score[0]} ; Player 2 - {score[1]}");
    }
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