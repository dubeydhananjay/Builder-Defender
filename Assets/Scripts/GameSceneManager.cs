

public static class GameSceneManager
{
   public enum Scenes
   {
       GameScene,
       MainMenuScene
   }

   public static void LoadScene(Scenes scene)
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
   }
}
