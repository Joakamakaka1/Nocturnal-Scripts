using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProyectile : MonoBehaviour
{
    [SerializeField] private int damage = 1; 
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadow;

    private GameObject grapeShadow;

    private void Start()
    {
        grapeShadow = Instantiate(grapeProjectileShadow, transform.position - Vector3.up * 0.3f, Quaternion.identity);
        StartCoroutine(ProjectileCurveRoutine(transform.position, PlayerController.Instance.transform.position));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadow.transform.position, PlayerController.Instance.transform.position));
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / duration;
            float height = Mathf.Lerp(0f, heightY, animCurve.Evaluate(t));
            transform.position = Vector3.Lerp(startPosition, endPosition, t) + Vector3.up * height;
            yield return null;
        }
        DestroyProjectile();
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / duration;
            grapeShadow.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        Destroy(grapeShadow);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
        Destroy(grapeShadow);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerVida player = other.GetComponent<PlayerVida>();
            if (player != null)
            {
                player.TakeDamage(damage, transform); 
            }
            DestroyProjectile();
        }
    }
}

