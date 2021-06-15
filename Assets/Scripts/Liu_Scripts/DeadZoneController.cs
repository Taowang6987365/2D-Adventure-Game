using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [SerializeField]private bool canMove;
    public bool isHorizontal;
    public bool isVertical;
    [SerializeField] private float moveDistance;
    private float maxMoveDistance;
    // Start is called before the first frame update
    void Start()
    {
        moveDistance = 1f;
        
            
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == true)
        {
            if(isHorizontal == true)
            {
                maxMoveDistance = this.gameObject.transform.position.y + moveDistance;
                StartCoroutine(MoveHorizontal());
            }
            else if(isVertical == true)
            {
                maxMoveDistance = this.gameObject.transform.position.x + moveDistance;
                MoveVertical();
            }
        }
        
    }

    public void MoveUp()
    {
        this.gameObject.transform.position = 
            new Vector2(this.gameObject.transform.position.x, 
            this.gameObject.transform.position.y + moveDistance); 
    }

    public void MoveDown()
    {
        this.gameObject.transform.position =
            new Vector2(this.gameObject.transform.position.x,
            this.gameObject.transform.position.y - moveDistance);
    }

    public void MoveVertical()
    {

    }
    IEnumerator MoveHorizontal()
    {
        yield return new WaitForSeconds(1f);
        MoveUp();
        yield return new WaitForSeconds(1f);
        MoveDown();
    }
}
