using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Transform Player;
    public float radius = 2.5f;
    public int GoToScene;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(Player.transform.position, transform.position);
        if(distance <= radius)
        {
            if (sprite != null)
            {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        GetComponent<SpriteRenderer>().sprite = sprite;
                        StartCoroutine(ChangeScene());
                    }
                        
            }
            else
            {
                    StartCoroutine(ChangeScene());
            }
             
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(GoToScene);
    }
}
