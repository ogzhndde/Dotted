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

    private void MoveToCurrentDot()
    {
        if (dotController.CurrentDot)
            // transform.position = Vector3.Lerp(transform.position, dotController.CurrentDot.transform.position, Time.deltaTime * 25f);
            transform.position = Vector3.MoveTowards(transform.position, dotController.CurrentDot.transform.position, Time.deltaTime * 50f);
    }

    void ActivationCheck()
    {
        GetComponent<Renderer>().enabled = dotController.CurrentDot ? true : false;
    }
}
