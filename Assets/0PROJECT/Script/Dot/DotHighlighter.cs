using UnityEngine;

/// <summary>   
/// Object indicating the currently selected dot.
/// </summary>
public class DotHighlighter : MonoBehaviour
{
    DotController dotController;

    void Start()
    {
        dotController = DotController.Instance;
    }

    void Update()
    {
        ActivationCheck();
        MoveToCurrentDot();
    }

    //Move the highlighter to current dot
    private void MoveToCurrentDot()
    {
        if (dotController.CurrentDot)
            transform.position = Vector3.MoveTowards(transform.position, dotController.CurrentDot.transform.position, Time.deltaTime * 50f);
    }

    //If there is no current dot, make it unvisible
    void ActivationCheck()
    {
        GetComponent<Renderer>().enabled = dotController.CurrentDot ? true : false;
    }
}
