using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ObjectColor
{
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();

    [SerializeField] private LayerMask groundPlayer;
    [SerializeField] private LayerMask stairLayer;

    [SerializeField] protected Transform skin;
    [SerializeField] private PlayerBrick playerBrickPrefabs;
    [SerializeField] private Transform brickHolder;
    
    [HideInInspector] public Stage stage;

    public int BrickCount => playerBricks.Count;

    // Start is called before the first frame update
    //TODO: Test
    protected virtual void Start()
    {
        ChangeColor(colorType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Check next place is Ground or not
    //if the next place is Ground:
    //+ Get that position
    //- Return the position before it

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundPlayer))
        {
            Debug.DrawRay(transform.position, nextPoint, Color.green);
            return hit.point + Vector3.up * 1.1f;
        }
        return transform.position;
    }

    /*
     *Going to the Bridge
     *Check color of Stair
     *IF Stair's color isn't same as Player and Player's Brickholder have brick
     * => fill the Stair's color
     *IF Player's Brickholder haven't Brick and Player is trying to go upstair,
     * => Player can't move upstair
     * 
     * Moving back to playground always enanble
     */
    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanMove = true;
        RaycastHit raycastHit;
        if (Physics.Raycast(nextPoint, Vector3.down, out raycastHit, 2f, stairLayer))
        {
            Stair stair = raycastHit.collider.GetComponent<Stair>();
            Debug.Log("playerbricks preb: " + playerBricks.Count);

            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
                Debug.Log("Goin");
                Debug.Log("playerbricks preb: " + playerBricks.Count);
            }
            if (stair.colorType != colorType && playerBricks.Count == 0 && skin.forward.z > 0)
            {
                Debug.Log("Go out");
                return isCanMove = false;
            }
        }
        return isCanMove;
    }

    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(playerBrickPrefabs, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = Vector3.up * 0.25f * playerBricks.Count;
        playerBricks.Add(playerBrick);
    }
    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }

    }
    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i]);
        }
        playerBricks.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();

            if (brick.colorType == colorType)
            {
                brick.OnDespawm();
                AddBrick();
                Destroy(brick.gameObject);
            }
        }
    }
}
