using SwordEngine;

/// <summary>
/// The main entry point of the game.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point of the game.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        MainGame game = new MainGame(600, 600);

        game.Start();
    }
}