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
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        DisplayForm windowForm = new DisplayForm();
        Application.Run(windowForm);
    }
}