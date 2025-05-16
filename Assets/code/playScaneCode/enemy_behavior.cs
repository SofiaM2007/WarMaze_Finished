using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behavior : MonoBehaviour
{
    struct Point
    {
        public int i, j;
        public Point(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }

    int[,] aktuell_maze;
    int rows, columns;
    int enemy_x, enemy_y; // Текущая позиция врага
    int f_x, f_y; // Цель (игрок)
    float step = 2.3f; // Размер шага
    public float speed; // Скорость движения

    private gameController g;
    Queue<Point> pathQueue;
    bool isStepInProgress = false; // Идёт ли текущий шаг



    void Start()
    {
        StartCoroutine(WaitSomeSecond());
    }

    IEnumerator WaitSomeSecond()
    {
        yield return new WaitForSeconds(0.1f);

        g = FindObjectOfType<gameController>();
        aktuell_maze = g.maze;
        speed = g.enemy_speed;

        rows = g.maze_h;
        columns = g.maze_b;

        GameObject mazeObjekt = g.getmazeArrayForObjektGeneration(g.level_now);

        if (mazeObjekt != null)
        {
            Renderer mazeRenderer = mazeObjekt.GetComponent<Renderer>();
            if (mazeRenderer != null)
            {
                Vector3 worldSize = mazeRenderer.bounds.size;
                Vector3 worldMin = mazeRenderer.bounds.min;

                Vector3 thisWorldPos = transform.position;

                float normalizedX = (thisWorldPos.x - worldMin.x) / worldSize.x;
                float normalizedY = (thisWorldPos.y - worldMin.y) / worldSize.y;

                enemy_x = Mathf.Clamp(Mathf.FloorToInt(normalizedX * 12), 0, 11);
                enemy_y = Mathf.Clamp(Mathf.FloorToInt(normalizedY * 12), 0, 11);

            }
            else
            {
                Debug.LogError("Ошибка: у mazeObject нет Renderer!");
            }

        }
        else
        {
            Debug.LogWarning("мэйз или enemy не найдены!");
        }


        f_x = g.player_x; f_y = g.player_y;
        FindTheWay();
    }

    void Update()
    {
        if(g!=null){
            if (!isStepInProgress && (f_x != g.player_x || f_y != g.player_y))
            {
                f_x = g.player_x;
                f_y = g.player_y;
                FindTheWay();
            }
        }

    }

    private void FindTheWay()
    {
        int[,] distanse = new int[rows, columns];
        int[] di = { -1, 0, 1, 0 };
        int[] dj = { 0, 1, 0, -1 };
        Queue<Point> queue = new Queue<Point>();

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                distanse[i, j] = -1;

        Point start = new Point(enemy_x, enemy_y);
        distanse[start.i, start.j] = 0;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
            int cx = current.i, cy = current.j;

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = cx + di[dir];
                int ny = cy + dj[dir];

                if (nx >= 0 && nx < rows && ny >= 0 && ny < columns)
                {
                    if (aktuell_maze[nx, ny] != 1 && distanse[nx, ny] == -1)
                    {
                        distanse[nx, ny] = distanse[cx, cy] + 1;
                        queue.Enqueue(new Point(nx, ny));
                    }
                }
            }
        }


        List<Point> path = GetShortestPath(distanse);
        if (path.Count > 0)
        {
            StartMoving(path);
        }
    }

    private List<Point> GetShortestPath(int[,] distanse)
    {
        List<Point> path = new List<Point>();
        int cx = f_x, cy = f_y;

        if (distanse[cx, cy] == -1) return path;

        while (cx != enemy_x || cy != enemy_y)
        {
            path.Add(new Point(cx, cy));

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = cx + (dir == 0 ? -1 : dir == 2 ? 1 : 0);
                int ny = cy + (dir == 1 ? 1 : dir == 3 ? -1 : 0);

                if (nx >= 0 && nx < rows && ny >= 0 && ny < columns)
                {
                    if (distanse[nx, ny] == distanse[cx, cy] - 1)
                    {
                        cx = nx;
                        cy = ny;
                        break;
                    }
                }
            }
        }

        path.Reverse();
        return path;
    }

    private void StartMoving(List<Point> path)
    {
        pathQueue = new Queue<Point>(path);
        if (!isStepInProgress)
        {
            StartCoroutine(MoveStepByStep());
        }
    }

    private IEnumerator MoveStepByStep()
    {
        while (pathQueue.Count > 0)
        {
            Point nextPos = pathQueue.Dequeue();
            int dy = nextPos.j - enemy_y;
            int dx = nextPos.i - enemy_x;

            Vector2 direction = new Vector2(dx, dy);

            if (direction != Vector2.zero)
            {
                if (direction == Vector2.up)
                    transform.rotation = Quaternion.Euler(0, 0, 0);// Вверх
                else if (direction == Vector2.down)
                    transform.rotation = Quaternion.Euler(0, 0, 180);// Вниз
                else if (direction == Vector2.left)
                    transform.rotation = Quaternion.Euler(0, 0, 90);// Влево
                else if (direction == Vector2.right)
                    transform.rotation = Quaternion.Euler(0, 0, -90);// Вправо
            }


            Vector3 finishPosition = transform.position + new Vector3(dx * step, dy * step, 0);

            isStepInProgress = true; 

            while (Vector3.Distance(transform.position, finishPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, finishPosition, speed * Time.deltaTime);


                yield return null;

            }
            isStepInProgress = false;
            enemy_x = nextPos.i;
            enemy_y = nextPos.j;

            if ((f_x != g.player_x || f_y != g.player_y))
            {
                f_x = g.player_x;
                f_y = g.player_y;

                FindTheWay();
                break;
            }
        }
    }
}
