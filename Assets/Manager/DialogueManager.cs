using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance = null;

    // Don't destroy
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DialogueManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    // Teddy part variable
    public Text m_Text;                                 // context text
    public Text m_NameText;                             // name text
    public SpriteRenderer m_SpriteRenderer;             // portrait sprite
    public SpriteRenderer m_DialogueWindowRenderer;     // dialogue windows
    public GameObject m_DiagMgrObj;

    private Player m_Player;                            // player
    private List<string> m_ListSentence;                // context list
    private List<string> m_ListNames;                   // name list
    private List<Sprite> m_ListSprites;                 // portrait list

    private int m_Count;                                // cound character (alphabet)

    private bool m_IsTalking = false;                   // is talking? check bool

    // Teddy part function
    void Start()
    {
        // init
        m_Count = 0;
        m_Text.text = "";
        m_NameText.text = "";
        m_ListSentence = new List<string>();
        m_ListNames = new List<string>();
        m_ListSprites = new List<Sprite>();
        m_DiagMgrObj.SetActive(false);
        m_Player = FindObjectOfType<Player>();
    }

    // dialogue open
    public void ShowDialogue(Dialogue diag)
    {
        m_DiagMgrObj.SetActive(true);
        // check talking on
        m_IsTalking = true;

        // player controll off
        m_Player.CanNotControl();

        // add dialogue list 
        for (int i = 0; i < diag.sentences.Length; i++)
        {
            m_ListSentence.Add(diag.sentences[i]);
            m_ListSprites.Add(diag.sprites[i]);
            m_ListNames.Add(diag.names[i]);
        }
        // coroutine activate
        StartCoroutine(StartDiagoueCoroutine());
    }

    // end of dialogue
    public void ExitDialogue()
    {
        m_Count = 0;
        m_Text.text = "";
        m_NameText.text = "";
        m_ListSentence.Clear();
        m_ListSprites.Clear();
        m_ListNames.Clear();

        m_IsTalking = false;
        m_DiagMgrObj.SetActive(false);
        // player controll on
        m_Player.CanControl();
    }

    // typing alphabet
    IEnumerator StartDiagoueCoroutine()
    {
        // during typing
        if (m_Count > 0)
        {
            m_NameText.text = m_ListNames[m_Count]; // name

            // end check
           if (m_ListSprites[m_Count] != m_ListSprites[m_Count - 1])
           {
               yield return new WaitForSeconds(0.1f);
               m_SpriteRenderer.GetComponent<SpriteRenderer>().sprite = m_ListSprites[m_Count]; // portrait
           }
           else
           {
               yield return new WaitForSeconds(0.05f);
           }
        }
        // begining of typing
        else
        {
            m_NameText.text = m_ListNames[m_Count]; // name
            m_SpriteRenderer.GetComponent<SpriteRenderer>().sprite = m_ListSprites[m_Count]; // portrait
        }
        for (int i = 0; i < m_ListSentence[m_Count].Length; i++)
        {
            m_Text.text += m_ListSentence[m_Count][i];
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsTalking)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) // next sentence
            {
                m_Count++;
                m_Text.text = ""; // init context

                if (m_Count == m_ListSentence.Count) // end of dialogue
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDiagoueCoroutine());
                }
            }
        }
    }
}
