using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Out");
        Debug.Log(collision.gameObject.tag);
        if (transform.name.Contains("border")) {
            GameManager.lostCause = "Reached Infinity";
        }

        if (transform.name.Contains("Galaxy"))
        {
            GameManager.lostCause = "Block Hole Collision";
        }

        if (collision.gameObject.tag == "Spacecraft")
        {
            if (GameManager.fuel > 0)
            {
                if (GameManager.health > 0)
                {

                    Debug.Log("Collision In");
                    if (transform.name.StartsWith("Planet"))
                    {
                        string[] parts = transform.name.Split(' ');
                        Debug.Log(transform.name);
                        foreach (bool value in GameManager.boolArray)
                        {
                            Debug.Log(value);
                        }
                        if (parts.Length == 2)
                        {
                            // Attempt to parse the second part as an integer
                            if (int.TryParse(parts[1], out int result))
                            {
                                GameManager.currentPlanet = result - 1;

                                if (!GameManager.boolArray[result - 1])
                                {
                                    StartCoroutine(LoadScene(sceneName));
                                    GameManager.boolArray[result - 1] = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(LoadScene(sceneName));
                    }
                }
                else {
                    Debug.Log("Health Over");
                    GameManager.lostCause = "Health Over";
                    StartCoroutine(LoadScene("HealthOver"));
                }

                  
            }
            else {

                Debug.Log("Fuel Over");
                GameManager.lostCause = "Fuel Over";
                StartCoroutine(LoadScene("FuelOver"));
            }
            
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
