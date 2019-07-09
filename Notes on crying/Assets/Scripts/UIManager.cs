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
    public CanvasGroup quitCanvas;
    public CanvasGroup congratsCanvas;
    public Animator gameAnimator;
    public Animator creatureAnimator;
    public Animator textPrompt;
    public Animator phonePrompt;
    public GameObject board;
    public AudioManager audio;


    // variables
    public bool timestarts;
    private bool dialoguePlaying;
    private bool dialogueFinished;
    private bool finishedEntry;
    private int entryCount;

    //private int seconds;

    public List<string> beginningDialogue;




    // Start is called before the first frame update
    void Awake()
    {
        dialoguePlaying = false;
        timestarts = false;
        entryCount = 0;

        dialogueCanvas.alpha = 0;
        //seconds = 0;

        board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check the time
        //if (timestarts)
        //{
        //    seconds += (int)Time.deltaTime % 60;
        //    Debug.Log("seconds:" + seconds);
        //    secondstime.text = seconds.ToString("00");
        //}

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


    }


    public void TriggerDialogue()
    {
        // Setup and play the first dialogue
        dialoguePlaying = true;
        entryCount = 0;
        finishedEntry = false;
        dialogueCanvas.alpha = 1;

        // test
        StartCoroutine(TypeSentence(beginningDialogue[entryCount]));
        //StartCoroutine(FadeOutDialogue());

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


    public IEnumerator PlayQuitScreen()
    {

        CanvasGroup group = quitCanvas;
        // fade in the canvas
        group.interactable = true;
        float temp = group.alpha;

        while (temp < 1)
        {
            temp += .01f;
            group.alpha = temp;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);

        // exit 
        Application.Quit();
        Debug.Log("quit");

    }


    public IEnumerator PlayCongratsScreen(bool gameOver)
    {
        if (gameOver)
        {
            // play ending scene
            creatureAnimator.SetBool("currentlyCrying", true);
            yield return new WaitForSeconds(5f);
            creatureAnimator.SetBool("currentlyCrying", false);
            creatureAnimator.SetBool("doneCrying", true);

            // play dialogue
            dialogueCanvas.alpha = 1;
            StartCoroutine(TypeSentence("but then everything feels alright again"));
            yield return new WaitForSeconds(4);
        }

        else
        {
            // play dialogue
            dialogueCanvas.alpha = 1;
            StartCoroutine(TypeSentence("but i think im alright for now"));
            yield return new WaitForSeconds(4);
        }



        CanvasGroup group = congratsCanvas;
        // fade in the canvas
        group.interactable = true;
        float temp = group.alpha;

        while (temp < 1)
        {
            temp += .01f;
            group.alpha = temp;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);

        // exit 
        Application.Quit();
        //Debug.Log("done");
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
            StartCoroutine(PlayAboutToCry());
            thirdheart.enabled = false;
        }
        else if (lives == 1)
        {
            StartCoroutine(PlayAboutToCry());
            secondheart.enabled = false;
        }
        else if (lives == 0)
        {
            StartCoroutine(PlayAboutToCry());
            creatureAnimator.SetBool("currentlyCrying", true);
            firstheart.enabled = false;
        }
        else
        {
            Debug.LogWarning("you have a strange number of lives....");
        }
    }


    public IEnumerator PlayAboutToCry()
    {
        creatureAnimator.SetBool("aboutToCry", true);
        yield return new WaitForSeconds(1f);
        creatureAnimator.SetBool("aboutToCry", false);
    }


    public void PlayTextPrompt()
    {
        textPrompt.SetBool("showTextPrompt", true);
        audio.Play("phone");
    }

    public void HideTextPrompt()
    {
        textPrompt.SetBool("showTextPrompt", false);
        audio.Stop("phone");
    }

    public void DisplayPhone()
    {
        phonePrompt.SetBool("displayPhone", true);
        HideTextPrompt();
    }

    public void HidePhone()
    {
        phonePrompt.SetBool("displayPhone", false);
        PlayTextPrompt();
    }
}
