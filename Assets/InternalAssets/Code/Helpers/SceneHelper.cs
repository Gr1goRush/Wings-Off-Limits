using UnityEngine.SceneManagement;

public static class SceneHelper
{
    public static void LoadScene(ProjectScene scene)
    {
        switch (scene)
        {
            case ProjectScene.Boot:
                SceneManager.LoadScene("Boot");
                break;

            case ProjectScene.Menu:
                SceneManager.LoadScene("Menu");
                break;

            case ProjectScene.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;

            case ProjectScene.Tutorial:
                SceneManager.LoadScene("HowToPlay");
                break;
        }
    }

    public static void LoadMenuWithTab(int TabID)
    {
        SceneManager.LoadScene("Menu");
        MenuSystem.SetPreloadTab(TabID);
    }
}

public enum ProjectScene
{
    Boot,
    Menu,
    Gameplay,
    Tutorial
}
