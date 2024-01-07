using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] public ProjectPoolManager projectilePoolManager;

    [Header("Fire")]
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private Transform fireTargets;

    [SerializeField] private GameObject deffBlockCannon;
    [SerializeField] private GameObject surgeCannon;
    [SerializeField] private float projectileForce = 2f;
    [SerializeField] private float delayBetweenShots = 0.3f;
    public Color cannonColor;

    private Renderer renderers;
    private Color projectileColor;
    private int remainingProjectiles;
    private bool canShoot = false;

    private void Awake()
    {
        renderers = GetComponent<Renderer>();
    }

    private void Start()
    {
        projectileColor = cannonColor;
        SetCannonColor(cannonColor);
    }

    private void Update()
    {
        projectileColor = cannonColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Renderer otherRenderer = other.GetComponent<Renderer>();
        if (canShoot == false && otherRenderer.material.color != projectileColor)
        {
            ChangeCannonColor(otherRenderer.material.color);
            Destroy(other.gameObject);
        }
    }

    public void Shoot(int count, int index)
    {
        remainingProjectiles = count;
        if (remainingProjectiles > 0)
        {
            switch (index)
            {
                case 0:
                    StartCoroutine(ShootCoroutine(0));
                    break;
                case 1:
                    ActiveSurge(remainingProjectiles);
                    break;
                case 2:
                    remainingProjectiles /= 64;
                    StartCoroutine(ShootCoroutine(1));
                    break;
                case 3:
                    remainingProjectiles /= 64;
                    StartCoroutine(ShootCoroutine(2));
                    break;
                default:
                    StartCoroutine(ShootCoroutine(0));
                    break;
            }
            canShoot = true;
            ActiveBlock(canShoot);
        }   
    }

    private IEnumerator ShootCoroutine(int projectilePrefab)
    {
        for (int i = 0; i < remainingProjectiles; i++)
        {
            int firePointIndex = i % firePoints.Length;
            Transform firePoint = firePoints[firePointIndex];

            GameObject projectile = projectilePoolManager.GetProjectile(projectilePrefab);
            if (projectile != null)
            {
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;

                projectile.GetComponent<Renderer>().material.color = projectileColor;

                AddForceToProjectile(projectile);

                yield return new WaitForSeconds(delayBetweenShots);
            }
        }
        remainingProjectiles = 0;

        canShoot = false;
        ActiveBlock(canShoot);
    }

    private void AddForceToProjectile(GameObject projectile)
    {
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.gravityScale = 0f;
        Vector2 direction = (fireTargets.position - projectile.transform.position).normalized;
        projectileRigidbody.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }

    private void ActiveSurge(int currentCount)
    {
        PointEffector2D pointEffector = GetComponentInChildren<PointEffector2D>();

        if (pointEffector != null)
        {
            float value = currentCount / 64 ;
            float forceMagnitude = pointEffector.forceMagnitude * value / 2;
            pointEffector.forceMagnitude = forceMagnitude;
            StartCoroutine(TimerEnableSurge(pointEffector));
        }
    }

    private IEnumerator TimerEnableSurge(PointEffector2D pointEffector)
    {
        pointEffector.enabled = true;
        surgeCannon.SetActive(true);
        yield return new WaitForSeconds(1);
        pointEffector.forceMagnitude = 10f;
        surgeCannon.SetActive(false);
        pointEffector.enabled = false;

        canShoot = false;
        ActiveBlock(canShoot);
    }

    private void SetCannonColor(Color color)
    {
        renderers.material.color = color;
    }

    private void ChangeCannonColor(Color color)
    {
        cannonColor = color;
        SetCannonColor(color);
    }

    private void ActiveBlock(bool canShoot)
    {
        deffBlockCannon.SetActive(canShoot);
    }
}
