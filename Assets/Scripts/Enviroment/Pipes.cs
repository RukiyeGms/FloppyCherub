using UnityEngine;
using UnityEngine.UIElements;

public class Pipes : MonoBehaviour
{
    [SerializeField] GameObject topPipe, bottomPipe;
    [SerializeField]  float gapSize; //gap for pipe centers
    [SerializeField] float distance; // gap for center
    [SerializeField] float scoreMult = 0.04f;

    float MAXGAPSIZE = 26;
    float MINGAPSIZE = 22;

    float MAXDISTANCE = 6;
    float MINDISTANCE = 2;

    //oynanabilen alan y pos

    float gapCenterMaxY = 4;
    float gapCenterMinY = -4;

    [SerializeField] GameObject spawnPosX;

    [SerializeField] GameObject gapCenter;

    [SerializeField] float moveSpeed = 2f;


    private void Start()
    {
        ChangePos(0);
    }

    private void Update()
    {
        MoveLeft();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            ChangePos(GameManager.CheckScore());
            Debug.Log("a");
        }
        Debug.Log("a");
    }

    private void SetDistance(int score)
    {
        gapSize = MAXGAPSIZE - (score * scoreMult);
        distance = MAXDISTANCE - (score * scoreMult);

        if (gapSize < MINGAPSIZE || distance < MINDISTANCE)
        {
            gapSize = MINGAPSIZE;
            distance = MINDISTANCE;
        }
    }
    

    void ChangePos(int score)
    {
        SetDistance(score); //getscore from gamemanager later
        float minCenterY = gapCenterMinY + (distance / 2);
        float maxCenterY = gapCenterMaxY  - (distance/ 2);

        transform.position = new Vector2(spawnPosX.transform.position.x, transform.position.y);


        gapCenter.transform.position = new Vector2(gapCenter.transform.position.x ,Random.Range(minCenterY , maxCenterY ));

        topPipe.transform.position = new Vector2(topPipe.transform.position.x , gapCenter.transform.position.y + (gapSize /2));
        bottomPipe.transform.position = new Vector2(bottomPipe.transform.position.x, gapCenter.transform.position.y - (gapSize / 2));
    }

    void MoveLeft()
    {
        transform.position = new Vector2( transform.position.x - moveSpeed * Time.deltaTime , transform.position.y);
    }


}
