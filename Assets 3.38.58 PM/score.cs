using System.Security;
using UnityEngine.UI;
using UnityEngine;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = player.position.z.ToString("0");
    }
}
