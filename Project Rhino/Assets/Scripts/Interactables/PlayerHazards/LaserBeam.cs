using System.Collections;
using UnityEngine;

public class LaserBeam : PlayerHazard
{
    public delegate void OnLaserStart();
    public OnLaserStart onLaserStart;

    public delegate void OnLaserEnd();
    public OnLaserEnd onLaserEnd;

    [Header("Members")]
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private ParticleSystem chargeUpFX, laserImpactFX;
    [SerializeField] private AudioSource laserChargingAudio, laserFiringAudio;
    [Space]

    [Header("Attributes")]
    [SerializeField] private LayerMask whatToHit;
    [SerializeField, Range(-180f, 180f)] private float startAngle = -60f, endAngle = -30f;
    [SerializeField, Range(0.1f, 100f)] private float rotSpeed = 5f;
    [SerializeField, Range(0.1f, 5f)] private float fireDelay = 1.5f;
    [SerializeField] private float maxDistance = 100f;
    [Space]

    [Header("Debugging")]
    [SerializeField] private bool startLaser;

    private Quaternion startRot, endRot, currentRot;
    private Vector2 laserHitPoint;
    private float rotationTimer = 0;
    private bool isFiring;

    private void Awake()
    {
        if (!m_lineRenderer)
            m_lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        SetLimitDirections();
        laserImpactFX.Stop();
        chargeUpFX.Stop();
        laserFiringAudio.Stop();
    }

    void Update()
    {
        if (startLaser)
        {
            StartLaserBeam();
            startLaser = false;
        }
    }

    private void FixedUpdate()
    {
        if(isFiring)
            LaserFiring();
    }

    private void OnDisable()
    {
        onLaserStart = null;
        onLaserEnd = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        SetLimitDirections();
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(startRot, Vector2.right, transform.parent.localScale) * maxDistance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(endRot, Vector2.right, transform.parent.localScale) * maxDistance);
    }

    private Vector2 GetDirectionFromRotation(Quaternion _rotation, Vector2 _from, Vector2 _parentScale)
    {
        Vector2 newScale = new Vector2(Mathf.Sign(_parentScale.x), Mathf.Sign(_parentScale.y));
        return _rotation * _from * newScale;
    }

    private void SetLimitDirections()
    {
        startRot = Quaternion.Euler(0, 0, startAngle);
        endRot = Quaternion.Euler(0, 0, endAngle);
    }

    private void RaycastLaser(Vector2 _dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir, maxDistance, whatToHit); 
        if (hit.collider)
        {
            laserHitPoint = hit.point;
            laserImpactFX.transform.position = laserHitPoint;
            KillPlayerButNotReally(hit.collider);
        }
        else
            laserHitPoint = Vector2.zero;
    }

    private void RenderLaser()
    {
        Vector2 hitLocalPos = laserHitPoint - (Vector2)transform.position;
        hitLocalPos = new Vector2(hitLocalPos.x, hitLocalPos.y) / transform.parent.localScale; //Scaling the line renderer to the parent transform's scale
        m_lineRenderer.SetPosition(1, hitLocalPos);
        laserImpactFX.Play();
    }

    private void GetLaserDirection()
    {
        rotationTimer += Time.deltaTime;
        currentRot = Quaternion.RotateTowards(startRot, endRot, rotationTimer * rotSpeed);
    }

    public void StartLaserBeam()
    {
        StopAllCoroutines();
        onLaserStart?.Invoke();
        chargeUpFX.Play();
        StartCoroutine(FiringSequence());
    }

    private IEnumerator FiringSequence()
    {
        float delayTimer = 0;
        laserChargingAudio.Play();
        while(delayTimer < fireDelay)
        {
            delayTimer += Time.deltaTime;
            yield return null;
        }
        laserChargingAudio.Stop();
        laserFiringAudio.Play();
        isFiring = true;
    }

    private void LaserFiring()
    {
        chargeUpFX.Stop();
        GetLaserDirection();
        if (currentRot != endRot)
        {
            RaycastLaser(GetDirectionFromRotation(currentRot, Vector2.right, transform.parent.localScale));
            if (laserHitPoint != Vector2.zero)
                RenderLaser();
        }
        else
            StopLaserBeam();
    }   

    private void StopLaserBeam()
    {
        laserImpactFX.Stop();
        laserFiringAudio.Stop();
        onLaserEnd?.Invoke();
        m_lineRenderer.SetPosition(1, Vector2.zero);
        rotationTimer = 0;
        currentRot = startRot;
        isFiring = false;
    }
}
