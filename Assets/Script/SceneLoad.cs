using Assets.Script.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScèneLoad : MonoBehaviour
{
    public string MyScene;
    public enum EnumLevel
    {
        Level1, Level2, Level3,
        MainMenu
    }
    private EnumLevel level;
    public static TypeClasse typeClasse { get; private set; }

    public void LoadScene()
    {
        Debug.Log("scene to load" + MyScene);
        SceneManager.LoadScene(MyScene);
        level = MyScene switch
        {
            "Play" => EnumLevel.Level1,
            "Play 1" => EnumLevel.Level2,
            "Play 2" => EnumLevel.Level3,
            _ => EnumLevel.MainMenu,
        };
        typeClasse = MyScene switch
        {
            "Play" => TypeClasse.Cromagnon,
            "Play 1" => TypeClasse.Samourai,
            "Play 2" => TypeClasse.Futuriste,
            _ => TypeClasse.Cromagnon,
        };

        //var audioManager = Object.FindFirstObjectByType<AudioManager>();
        //audioManager.SetCurrentLevel(level);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(MyScene);
        }
    }

    public static void GoToMenuPrincipale()
    {
        SceneManager.LoadScene("MenuPrincipale");
        Debug.Log("GoToMenuPrincipale");
    }
}
