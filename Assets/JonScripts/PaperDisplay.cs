using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaperDisplay : MonoBehaviour
{
    public List<Paper> papers;
    public Paper paper;

    public TextMeshProUGUI headline;
    public TextMeshProUGUI story;

    public GameObject Paper;


    private void OnEnable()
    {
        int rand = Random.Range(0, papers.Count);
        paper = papers[rand];

        headline.text = paper.headline;
        story.text = paper.story;
    }

    public void ExtractVariables()
    {

    }

    /*
    public void EnterPaper()
    {
        gameObject.SetActive(true);

        int rand = Random.Range(0, papers.Count);
        paper = papers[rand];

        headline.text = paper.headline;
        story.text = paper.story;

    }
    */

    public void ExitPaper()
    {
        gameObject.SetActive(false);
    }
    

}
