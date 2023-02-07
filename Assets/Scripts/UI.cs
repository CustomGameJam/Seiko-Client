using UnityEngine;

public class UI : MonoBehaviour
{
   public GameObject startButton;

   private Client _client;

   private void Awake()
   {
      _client = FindObjectOfType<Client>();
   }

   public void StartGame()
   {
      _client.Join();
      gameObject.SetActive(false);
   }

}
