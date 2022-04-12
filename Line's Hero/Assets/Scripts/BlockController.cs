using UnityEngine;

public class BlockController : MonoBehaviour

{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private Effector effector;
    [SerializeField] private Spawner2 spawner;

    [SerializeField] private float fallTime = 0.8f;
    [SerializeField] private float horizontalMoveTime = 0.8f;

    private Transform activeBlockTransform;
    private Transform rotationPoint;

    private float previousTime;
    private float previousSideMoveTime;

    private static int height = 20;
    private static int width = 10;

    private static Transform[,] grid = new Transform[width, height];

    private float swipeDeadZone = 60;
    private Vector2 startSwipe;

    public enum SwipeDirection {Left, Right, Up, Down}
    public SwipeDirection swipeDirection = new SwipeDirection();

    private void Update()
    {
        if (!GameManager.gamePaused && !GameManager.gameEnded && activeBlockTransform != null)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                TurnLeft();

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                TurnRight();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                spawner.Hold();

            if (Input.GetKeyDown(KeyCode.Space))
                Rotate();

            if (Time.time - previousTime > ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? fallTime / 20 : fallTime))
                FallingDown();

            if (Input.GetKeyDown(KeyCode.Escape))
                gameManager.Pause();

            if (Input.touchCount > 0)
                Swipe();
        } 
    }

    private void Swipe()
    {
        Touch activeTouch = Input.GetTouch(0);

        if (activeTouch.phase == TouchPhase.Began)
            startSwipe = activeTouch.position;

        if (activeTouch.phase == TouchPhase.Moved)
        {
            switch (GetSwipeDirection(activeTouch))
            {
                case SwipeDirection.Left:
                    {
                        if (activeTouch.position.x - startSwipe.x < -swipeDeadZone)
                        {
                            TurnLeftSwipe();
                            startSwipe = activeTouch.position;
                        }

                        break;
                    }
                case SwipeDirection.Right:
                    {
                        if ((activeTouch.position.x - startSwipe.x) > swipeDeadZone)
                        {
                            TurnRightSwipe();
                            startSwipe = activeTouch.position;
                        }

                        break;
                    }
                case SwipeDirection.Up:
                    break;

                case SwipeDirection.Down:
                    {
                        if (activeTouch.position.y - startSwipe.y < -swipeDeadZone)
                            FallingDown();
                        break;
                    }
                    
                default:
                    break;
            }
        }

        if(activeTouch.phase == TouchPhase.Ended || activeTouch.phase == TouchPhase.Canceled)
        {
            if (activeTouch.position.x - startSwipe.x == 0 && activeTouch.position.y - startSwipe.y == 0)
                Rotate();
        }
    }

    private SwipeDirection GetSwipeDirection(Touch touch)
    {
        if (Mathf.Abs(touch.position.x - startSwipe.x) > Mathf.Abs(touch.position.y - startSwipe.y))
            if (touch.position.x - startSwipe.x < 0)
                swipeDirection = SwipeDirection.Left;
            else
                swipeDirection = SwipeDirection.Right;
        else if (Mathf.Abs(touch.position.x - startSwipe.x) < Mathf.Abs(touch.position.y - startSwipe.y))
            if (touch.position.y - startSwipe.y < 0)
                swipeDirection = SwipeDirection.Down;
            else
                swipeDirection = SwipeDirection.Up;
 
        return swipeDirection;
    }

    private void TurnLeft()
    {
        if (Time.time - previousSideMoveTime > horizontalMoveTime / 10)
        {
            activeBlockTransform.position += Vector3.left;
                     
            if (ValidMove())
                activeBlockTransform.position += Vector3.right;

            previousSideMoveTime = Time.time;
        }
    }

    private void TurnLeftSwipe()
    {
        activeBlockTransform.position += Vector3.left;

        if (ValidMove())
            activeBlockTransform.position += Vector3.right;
    }

    private void TurnRight()
    {
        if (Time.time - previousSideMoveTime > horizontalMoveTime / 10)
        {
            activeBlockTransform.position += Vector3.right;

            if (ValidMove())
                activeBlockTransform.position += Vector3.left;

            previousSideMoveTime = Time.time;
        }
    }

    private void TurnRightSwipe()
    {
        activeBlockTransform.position += Vector3.right;

        if (ValidMove())
            activeBlockTransform.position += Vector3.left;
    }

    private void Rotate()
    {
        activeBlockTransform.RotateAround(rotationPoint.position, new Vector3(0, 0, 1), 90);
        if (ValidMove())
            activeBlockTransform.RotateAround(rotationPoint.position, new Vector3(0, 0, 1), -90);
    }

    private void FallingDown()
    {
        if (!GameManager.gameEnded)
        {

            activeBlockTransform.position += new Vector3(0, -1, 0);

            if (ValidMove())
            {
                activeBlockTransform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                if(!GameManager.gameEnded)
                    spawner.SpawnNewBlock();
                else
                    gameManager.GameOver();
            }
        }
        else
            gameManager.GameOver();

        previousTime = Time.time;

    }

    private bool ValidMove()
    {
        foreach (Transform children in activeBlockTransform)
        {
            if (children.tag == "Cube")
            {
                int roundedX = Mathf.RoundToInt(children.position.x);
                int roundedY = Mathf.RoundToInt(children.position.y);

                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                    return true;

                if (grid[roundedX, roundedY] != null)
                    return true;
            }
        }

        return false;
    }

    private void AddToGrid()
    {
        foreach (Transform children in activeBlockTransform)
        {
            if (children.tag == "Cube")
            {
                int roundedX = Mathf.RoundToInt(children.position.x);
                int roundedY = Mathf.RoundToInt(children.position.y);

                if (roundedY < grid.GetLength(1)-1)
                    grid[roundedX, roundedY] = children;
                else
                    GameManager.gameEnded = true;
            }
        }
    }

    private  void CheckForLines()
    {
        int combo = 0;

        for (int row = height-1; row >= 0; row--)
        {
            if (HasLine(row))
            {
                scoreSystem.AddLines();
                effector.PlayLineClearedParticles(row);
                DeleteLine(row);
                RowDown(row);
                combo++;
            }
        }

        if (combo > 0)
            scoreSystem.CheckCombo(combo);
    }

    private bool HasLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            if (grid[column, row] == null)
                return false;
        }
        return true;
    }

    private void DeleteLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            Destroy(grid[column, row].gameObject);
            grid[column, row] = null;
        }
    }

    private void RowDown(int row)
    {
        for (int activeRow = row; activeRow < height; activeRow++)
        {
            for (int column = 0; column < width; column++)
            {
                if (grid[column,activeRow] != null)
                {
                    grid[column, activeRow - 1] = grid[column, activeRow];
                    grid[column, activeRow] = null;
                    grid[column, activeRow - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void SetActiveBlock(Transform blockTransform)
    {
        activeBlockTransform = blockTransform;
        rotationPoint = activeBlockTransform.GetChild(4);
    }

    public Transform GetaActiveBlock()
    {
        return activeBlockTransform;
    }

    public static void SpeedUp()
    {

    }
}
