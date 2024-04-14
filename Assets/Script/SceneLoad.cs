using Assets.Script.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScèneLoad : MonoBehaviour
{
    public string MyScene;
    public enum EnumLevel
    {
        Level1, Level2, Level3,
        MainMenu, MenuClasse
    }
    private EnumLevel level;
    public static TypeClasse typeClasse { get; private set; }

    public void LoadScene()
    {
        level = MyScene switch
        {
            "Play" => EnumLevel.Level1,
            "Play 1" => EnumLevel.Level2,
            "Play 2" => EnumLevel.Level3,
            "MenuClasse" => EnumLevel.MenuClasse,
            _ => EnumLevel.MainMenu,
        };
        typeClasse = MyScene switch
        {
            "Play" => TypeClasse.Cromagnon,
            "Play 1" => TypeClasse.Samourai,
            "Play 2" => TypeClasse.Futuriste,
            _ => TypeClasse.Cromagnon,
        };
        // MainMenu <-> MenuClasse
        AudioManager.SaveMusicTime();
        SceneManager.LoadScene(MyScene);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoToMenuPrincipale();
        }
    }

    public static void GoToMenuPrincipale()
    {
        AudioManager.SaveMusicTime();
        SceneManager.LoadScene("MenuPrincipale");
    }
}
