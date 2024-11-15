using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace greg
{
public class Fish : MonoBehaviour
{
    private string letter = "";
    
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private Sprite fishPic;
    [SerializeField] private SpriteRenderer sr = new SpriteRenderer();
    
    [SerializeField] private FishStats thisFishStats;
    
    private Vector3 moveDirection;

    private MeshCollider collider;
    private Vector3 target;
    
    private bool gettingnewdirection = false;
    private Vector3 newtarget;
    
    public void setStats(FishStats f)
    {
        thisFishStats = f;
    }

    
    private void Start()
    {
        letter =  thisFishStats.Letters[(int)(Random.value * thisFishStats.Letters.Length)].ToString();

        fishPic = thisFishStats.Sprites[(int)(Random.value * thisFishStats.Sprites.Length)]; 

        sr.sprite = fishPic;
        
        setMoveAndFaceDirection();
        
        _textMeshPro.text = letter;
        
        collider = GetComponent<MeshCollider>();
        collider.sharedMesh = SpriteToMesh(fishPic);
        
        target = new Vector3(moveDirection.x * 30 , transform.position.y, transform.position.z); // other side of screen
        newtarget = target;
        
        StartCoroutine(setNewTarget());
    }

    private void OnEnable()
    {
        letter =  thisFishStats.Letters[(int)(Random.value * thisFishStats.Letters.Length)].ToString(); 

        fishPic = thisFishStats.Sprites[(int)(Random.value * thisFishStats.Sprites.Length)]; 

        sr.sprite = fishPic;
        collider.sharedMesh = SpriteToMesh(fishPic);
        
        setMoveAndFaceDirection();
        
        _textMeshPro.text = letter;
        
        if (GetComponent<Collider>()) // only difference between this and start
            GetComponent<Collider>().enabled = true;

        target = new Vector3(moveDirection.x * 30 , transform.position.y, transform.position.z);
        newtarget = target;
        
        StartCoroutine(setNewTarget());
    }

    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy == false)
            return;
        
        if (gettingnewdirection == false)
            StartCoroutine(setNewTarget());

        if (transform.position.x > 20.5 || transform.position.x < -20.5)
        {
            killFish();
            return;
        }
           

        float speedUp = 1.75f; // change this per level
        float step =  GlobalSettings.FishSpeedsReversed[thisFishStats.rarity] * Time.fixedDeltaTime * speedUp; // calculate distance to move
        
        transform.position = Vector3.MoveTowards(transform.position, newtarget, step);

    }

    private void setMoveAndFaceDirection()
    {
        if (transform.position.x > 0)
        {
            moveDirection = Vector3.left;
            sr.flipX = true;
        }
        else
        {
            moveDirection = Vector3.right;
            sr.flipX = false;
        }

    }
    
    private Mesh SpriteToMesh(Sprite sprite) // https://stackoverflow.com/questions/45983064/unity-mesh-from-sprite-vertices
    {
        Mesh mesh = new Mesh();
        mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        mesh.SetUVs(0,sprite.uv.ToList());
        mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i),0);

        return mesh;
    }

    public string GetLetter()
    {
        return letter;
    }
    public void Catch()
    {
        collider.enabled = false; // cant re-catch while animating
        
        //Fishstatus = Caught;

        // Animate the fish falling towards the camera, then destroy when out of range
        
        killFish();

    }
    

    private IEnumerator setNewTarget()
    {
        gettingnewdirection = true;
        yield return new WaitForSeconds(1.0f + (Random.value * 2)); // lower this to change more frequently
        float yvalue = transform.position.y + Random.Range(-10, 10); // the larger the range is the more chaotic it is
        
        yvalue = Mathf.Clamp(yvalue, -3, 7);

        newtarget = new Vector3(moveDirection.x * 21 , yvalue, transform.position.z);
        
        // set new rotation also maybe

        gettingnewdirection = false;

    }

    public void killFish()
    {
        gameObject.SetActive(false);
        transform.position = Vector3.zero; // so it wont get destroyed out of bounds
    }

}
    
}
