using DG.Tweening;
using UnityEngine;

/// <summary>   
/// Class that controls camera movements
/// </summary>
public class CameraMovement : MonoBehaviour
{
    void SetNewPos(Vector3 camPos)
    {
        float x = camPos.x;
        float y = camPos.y;
        float z = transform.position.z;

        Vector3 newPos = new Vector3(x, y, z);
        transform.DOMove(newPos, 0.5f).SetEase(Ease.OutExpo);
        DOTween.To(x => GetComponent<Camera>().orthographicSize = x, GetComponent<Camera>().orthographicSize, 4, 0.5f);
    }
    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnLineIntersection, OnLineIntersection);
        EventManager.AddHandler(GameEvent.OnBombExplode, OnBombExplode);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnLineIntersection, OnLineIntersection);
        EventManager.RemoveHandler(GameEvent.OnBombExplode, OnBombExplode);
    }

    private void OnLineIntersection(object value)
    {
        Vector3 camPos = (Vector3)value;
        SetNewPos(camPos);
    }
    private void OnBombExplode(object value)
    {
        Vector3 camPos = (Vector3)value;
        SetNewPos(camPos);
    }
}
