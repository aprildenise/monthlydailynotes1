using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    // References
    //public GameObject gameMenu;
    //public GameObject startMenu;
    public Image firstheart;
    public Image secondheart;
    public Image thirdheart;
    public TextMeshProUGUI secondstime;
    public TextMeshProUGUI dialogueText;
    public CanvasGroup dialogueCanvas;
    public CanvasGroup startCanvas;
    public CanvasGroup gameCanvas;
    public Animator gameAnimator;
    public GameObject board;


    // variables
    public bool timestarts;
    private bool dialoguePlaying;
    private bool dialogueFinished;
    private bool finishedEntry;
    private int entryCount;

    public List<string> beginningDialogue;




    // Start is called before the first frame update
    void Awake()
    {
        dialoguePlaying = false;
        timestarts = false;
        entryCount = 0;

        dialogueCanvas.alpha = 0;
        board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the intro animation is still playing

        // Check if the dialogue is finished
        if (dialogueFinished)
        {
            // No need to run the rest of the function
            return;
        }

        // If the dialogue is still playing
        if (dialoguePlaying)
        {
            if (finishedEntry)
            {
                // See if we play the next entry
                if (entryCount == beginningDialogue.Count - 1)
                {
                    // Finished playing the entire dialogue
                    dialogueFinished = true;
                    dialoguePlaying = false;
                    StartCoroutine(FadeOutDialogue());
                    return;
                }

                else
                {
                    // play the next dialogue entry
                    entryCount++;
                    StartCoroutine(TypeSentence(beginningDialogue[entryCount]));
                }
            }
        }



        // check the time
        if (timestarts)
        {
            int seconds = (int)(Time.time % 60f);
            secondstime.text = seconds.ToString("00");
        }

    }


    public void TriggerDialogue()
    {
        // Setup and play the first dialogue
        dialoguePlaying = true;
        entryCount = 0;
        finishedEntry = false;
        dialogueCanvas.alpha = 1;

        StartCoroutine(TypeSentence(beginningDialogue[entryCount]));

    }



    public IEnumerator TypeSentence(string entry)
    {
        finishedEntry = false;
        dialogueText.text = "";

        // Type out the sentence by looping through the entry
        foreach (char letter in entry.ToCharArray())
        {

            //type out the letters
            dialogueText.text += letter;
            yield return new WaitForSeconds(.1f);

        }
        dialogueText.text = entry;

        // wait a little after done
        yield return new WaitForSeconds(1f);

        finishedEntry = true;
    }


    public void TurnOnGame()
    {
        gameCanvas.alpha = 1;
        gameCanvas.interactable = true;

        // Start animations
        Animator gameAnimator = gameCanvas.gameObject.GetComponent<Animator>();

        // Spawn board
        board.SetActive(true);
    }


    public void TurnOffGame()
    {
        gameCanvas.alpha = 0;
        gameCanvas.interactable = false;

        // Hide board
        board.SetActive(false) ;
    }


    public void TurnOnStart()
    {
        startCanvas.alpha = 1;
        startCanvas.interactable = true;
    }


    public void TurnOffStart()
    {
        startCanvas.alpha = 0;
        startCanvas.interactable = false;
    }


    private IEnumerator FadeOutDialogue()
    {
        // Get canvas renderer
        CanvasGroup group = dialogueCanvas;
        float temp = group.alpha;

        while (temp > .01)
        {
            temp -= .1f;
            group.alpha = temp;
            yield return new WaitForSeconds(0.1f);
        }

        // turn it off completely
        group.interactable = false;
        yield return new WaitForSeconds(1f);

        // make the game appear
        gameAnimator.SetBool("startgame", true);
        TurnOnGame();


    }


    public IEnumerator FadeOutStartMenu()
    {
        // Get canvas renderer
        CanvasGroup group = startCanvas;
        float temp = group.alpha;

        while (temp > .01)
        {
            temp -= .1f;
            group.alpha = temp;
            yield return new WaitForSeconds(0.1f);
        }

        // turn it off completely
        group.interactable = false;
        yield return new WaitForSeconds(1f);

        // Play dialogue
        TriggerDialogue();
        
    }

    public void RemoveHeart(int lives)
    {
        if (lives == 2)
        {
            thirdheart.enabled = false;
        }
        else if (lives == 1)
        {
            secondheart.enabled = false;
        }
        else if (lives == 0)
        {
            firstheart.enabled = false;
        }
        else
        {
            Debug.LogWarning("you have a strange number of lives....");
        }
    }
}
