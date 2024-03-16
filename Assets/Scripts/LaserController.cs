using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LaserMode
{
    CompleteCircle,
    SemiCircle,
    CustomAngle
}

public class LaserController : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform firePoint;

    public float arcRadius = 100f; // Rayon de l'arc de cercle
    public float arcSpeed = 10f; // Vitesse de déplacement le long de l'arc
    private float angle = 0f; // Angle pour calculer la position le long de l'arc

    public float angleOffset = 0f;

    public float customAngle;
    private bool goingForward = true;

    public float recoilForce;

    public LaserMode laserMode;

    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
                // Calcul de la position le long de l'arc de cercle
            float x = Mathf.Cos(angle) * arcRadius;
            float y = Mathf.Sin(angle) * arcRadius;

            
            lineRenderer.SetPosition(0, firePoint.position);

            if (laserMode == LaserMode.CompleteCircle)
            {
                CompleteCircle();
            }
            else if (laserMode == LaserMode.SemiCircle)
            {
                SemiCircle();
            } else if (laserMode == LaserMode.CustomAngle) {
                CustomAngle();
            }

            Vector2 laserEndPosition = new Vector2(x, y) + (Vector2)firePoint.position;       
            lineRenderer.SetPosition(1, laserEndPosition);

            Vector2 direction = laserEndPosition - (Vector2)firePoint.position;
            // Lancer un raycast le long du laser
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)firePoint.position, direction.normalized, direction.magnitude);

            // Le raycast ne fonctionne pas comme prévu quand le personnage est entre le laser et un mur


            if (hits.Length > 0)
            {
                bool collided = false;
                int i=0;
                while (!collided && i < hits.Length) {


                    if (hits[i].collider.CompareTag("Player")) {
                        var healthComponent = hits[i].collider.GetComponent<Health>();
                        if (healthComponent != null) {
                            Debug.Log(healthComponent);
                            healthComponent.TakeDamage(1);

                            Vector2 recoilDirection = (Vector2)hits[i].collider.transform.position - hits[i].point;
                            healthComponent.Recoil(recoilDirection, recoilForce);
                        }
                    } else {
                        lineRenderer.SetPosition(1, hits[i].point);
                        collided=true;
                    }

                    i++;
                } 

            }
        }
    }

    public void EnableLaser() {
        lineRenderer.enabled = true;
        isActive = true;
    }

    public void DisableLaser() {
        lineRenderer.enabled = false;
        isActive = false;
    }


    public void CompleteCircle() {
        // Mise à jour de l'angle pour effectuer un mouvement circulaire
        angle += arcSpeed * Time.deltaTime;

        // Si l'angle dépasse 2π (360 degrés), le réinitialiser pour continuer le mouvement circulaire
        if (angle > Mathf.PI * 2f)
        {
            angle -= Mathf.PI * 2f;
        }
    }

    public void SemiCircle() {
        // Mise à jour de l'angle pour effectuer un mouvement le long de l'arc
        if (goingForward)
        {
            angle += arcSpeed * Time.deltaTime;
            if (angle >= Mathf.PI + angleOffset) // Si l'angle atteint ou dépasse 180 degrés (π radians)
            {
                angle = Mathf.PI + angleOffset;
                goingForward = false; // Inverser la direction
            }
        }
        else
        {
            angle -= arcSpeed * Time.deltaTime;
            if (angle <= angleOffset) // Si l'angle atteint ou descend en dessous de 0
            {
                angle = angleOffset;
                goingForward = true; // Revenir à la direction initiale
            }
        }
    }

    public void CustomAngle() {

        float customRadiantAngle = customAngle / (180 / Mathf.PI);

        // Mise à jour de l'angle pour effectuer un mouvement le long de l'arc
        if (goingForward)
        {
            angle += arcSpeed * Time.deltaTime;
            if (angle >= customRadiantAngle + angleOffset) 
            {
                angle = customRadiantAngle + angleOffset;
                goingForward = false; // Inverser la direction
            }
        }
        else
        {
            angle -= arcSpeed * Time.deltaTime;
            if (angle <= angleOffset)
            {
                angle = angleOffset;
                goingForward = true; // Revenir à la direction initiale
            }
        }
    }

}
