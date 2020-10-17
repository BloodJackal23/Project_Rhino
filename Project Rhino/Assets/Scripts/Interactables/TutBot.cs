using UnityEngine;
using TMPro;
using System.Collections;

public class TutBot : MonoBehaviour
{
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshPro textMesh;

    [SerializeField] [TextArea(3, 10)] string message = "Hello there! I am Tutbot";
    [SerializeField] float typeSpeed = 15f;
    [SerializeField] bool displayMessageOnce = false;
    bool displayedOnce = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        speechBubble.SetActive(false);
        textMesh.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(displayMessageOnce)
        {
            if(!displayedOnce)
            {
                if (collision.gameObject.tag == "Player")
                {
                    speechBubble.SetActive(true);
                    timer = 0;
                    StartCoroutine(TypeMessage(message));
                    displayedOnce = true;
                }
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                speechBubble.SetActive(true);
                timer = 0;
                StartCoroutine(TypeMessage(message));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textMesh.text = "";
            timer = 0;
            StopAllCoroutines();
            //StopCoroutine(TypeMessage(message));
            speechBubble.SetActive(false);
        }
    }

    IEnumerator TypeMessage( string _message)
    {
        while(!TypeEffect.IsFinishedTyping(textMesh.text, _message))
        {
            timer += Time.deltaTime;
            textMesh.text = TypeEffect.TypeText(_message, typeSpeed, timer);
            Debug.Log("Typing...");
            yield return null;
        }
    }
}
