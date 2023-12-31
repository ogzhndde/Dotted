using System.Collections;
using ParticleFactoryStatic;
using UnityEngine;

/// <summary>   
/// The class that holds the methods of timebomb dots.
/// </summary>
public class DotTimeBomb : DotAbstract, ITimeBomb
{
    private const float BombCooldown = 4f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private bool _isBombTriggered = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        DrawLine();
        CheckAudioSource();
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance._gameFail) return;
        if (!DotController.Instance._isSwiping) return;

        EventManager.Broadcast(GameEvent.OnConnectDot, gameObject);
        BombTriggered();
    }

    //Start timer when dot triggers
    public void BombTriggered()
    {
        if (_isBombTriggered) return;

        _isBombTriggered = true;
        StartCoroutine(BombTimerCooldown());
    }

    void CheckAudioSource()
    {
        audioSource.enabled = _isBombTriggered;
    }

    //Timer Coroutine
    IEnumerator BombTimerCooldown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < BombCooldown)
        {
            elapsedTime += Time.deltaTime;

            spriteRenderer.color = colorGradient.Evaluate(elapsedTime / BombCooldown);
            yield return null;
        }

        //Dot explode and game fail
        EventManager.Broadcast(GameEvent.OnBombExplode, transform.position);
        EventManager.Broadcast(GameEvent.OnFinish);
        ParticleFactory.SpawnParticle(ParticleType.Explode, transform.position);
        _isBombTriggered = false;
    }


    //########################################    EVENTS    ###################################################################
    private void OnEnable()
    {
        //Appear Process
        anim.SetTrigger("DotAppear");
        spriteRenderer.color = colorGradient.Evaluate(0);
        _isBombTriggered = false;
        TargetMovePoint = transform.position;

        //Events
        EventManager.AddHandler(GameEvent.OnClosedConnection, OnClosedConnection);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnClosedConnection, OnClosedConnection);
    }

    private void OnClosedConnection(object dot)
    {
        GameObject targetDot = (GameObject)dot;

        if (ConnectedDots.Contains(targetDot.transform))
            ConnectedDots.Remove(targetDot.transform);

        if (targetDot != gameObject) return;
        StopCoroutine(BombTimerCooldown());
        ExplodeDot();
    }
}
