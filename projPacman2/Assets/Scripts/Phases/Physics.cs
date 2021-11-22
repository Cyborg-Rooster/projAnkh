using UnityEngine;

class Physics
{
    public Transform Transform;
    public Grid Grid;
    public float Speed;

    private bool Up, Left, Down, Right, CanMove;
    private Vector2 NextPos;

    public void SetNextPos(Vector3 position)
    {
        NextPos = position;
    }
    /// <summary>
    /// <para>Muda a posição que o jogador está olhando relativo a tecla apertada.</para>
    /// </summary>
    /// <param name="code"></param>
    public bool HaveCollision(KeyCode code, bool gonnaMove)
    {
        return ValidatePosition(code, gonnaMove);
    }

    /// <summary>
    /// <para>Move o objeto para a direção definida.</para>
    /// </summary>
    public void Move()
    {
        BackToCenterOfGrid();
        if (!CheckIfExistCollider(NextPos, Color.blue, "Wall") && CanMove)
        {
            Transform.position = Vector3.MoveTowards(Transform.position, NextPos, Speed * Time.deltaTime);
            ChangeNextPosition();
        }
        
    }

    /// <summary>
    /// <para>Atualiza o colisor do objeto.</para>
    /// </summary>
    public void UpdateCollider()
    {
        ChangeNextPosition();
    }

    /// <summary>
    /// <para>Retorna o objeto colidido.</para>
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectByCollision(string layer)
    {
        if (CheckIfExistCollider(Color.magenta, layer).collider == null) return null;
        else return CheckIfExistCollider(Color.magenta, layer).collider.gameObject;
    }

    /// <summary>
    /// <para>Muda a velocidade da física.</para>
    /// </summary>
    /// <param name="code"></param>
    public void ChangeSpeed(float speed)
    {
        Speed = speed;
    }

    /// <summary>
    /// <para>Começa a mover o jogador.</para>
    /// </summary>
    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
    }

    /// <summary>
    /// <para>Calcula a distância para o próximo movimento e o valida.</para>
    /// </summary>
    /// <param name="code"></param>
    private bool ValidatePosition(KeyCode code, bool gonnaMove)
    {
        Vector2 temp = Vector2.zero;
        if (code == KeyCode.W) temp = new Vector2(Transform.position.x, Transform.position.y + 0.16f);
        else if (code == KeyCode.A) temp = new Vector2(Transform.position.x - 0.16f, Transform.position.y);
        else if (code == KeyCode.S) temp = new Vector2(Transform.position.x, Transform.position.y - 0.16f);
        else if (code == KeyCode.D) temp = new Vector2(Transform.position.x + 0.16f, Transform.position.y);

        bool collisor = CheckIfExistCollider(temp, Color.red, "Wall");

        if (!collisor && gonnaMove)
        {
            TurnOffAllDirections();
            NextPos = temp;
            SetCanMove(true);
            if (code == KeyCode.W) Up = true;
            else if (code == KeyCode.A) Left = true;
            else if (code == KeyCode.S) Down = true;
            else if (code == KeyCode.D) Right = true;
        }

        return collisor;
    }
    /// <summary>
    /// <para>Checa se existe um colisor em uma camada específica do editor.</para>
    /// </summary>
    /// <param name="code"></param>
    private bool CheckIfExistCollider(Vector2 finalPos, Color color, string layer)
    {
        Debug.DrawLine(Transform.position, finalPos, color);

        var collider = Physics2D.Linecast(Transform.position, finalPos, 1 << LayerMask.NameToLayer(layer));

        return collider;
    }

    /// <summary>
    /// <para>Checa se existe um colisor em uma camada específica do editor na direção pré definida do objeto.</para>
    /// </summary>
    /// <param name="code"></param>
    private RaycastHit2D CheckIfExistCollider(Color color, string layer)
    {
        Debug.DrawLine(Transform.position, NextPos, color);

        var collider = Physics2D.Linecast(Transform.position, NextPos, 1 << LayerMask.NameToLayer(layer));

        return collider;
    }
    /// <summary>
    /// <para>Desliga todas as direções.</para>
    /// </summary>
    private void TurnOffAllDirections()
    {
        Up = false;
        Left = false;
        Down = false;
        Right = false;
    }
    /// <summary>
    /// <para>Calcula o próximo movimento sem depender de Input.</para>
    /// </summary>
    private void ChangeNextPosition()
    {
        if (Up) NextPos = new Vector2(Transform.position.x, Transform.position.y + 0.09f);
        else if (Left) NextPos = new Vector2(Transform.position.x - 0.09f, Transform.position.y);
        else if (Down) NextPos = new Vector2(Transform.position.x, Transform.position.y - 0.09f);
        else if (Right) NextPos = new Vector2(Transform.position.x + 0.09f, Transform.position.y);
    }

    /// <summary>
    /// <para>Centraliza o objeto no grid.</para>
    /// </summary>
    private void BackToCenterOfGrid()
    {
        Vector3Int cellPosition = Grid.LocalToCell(Transform.localPosition);

        /*if (Up || Down) Transform.localPosition = new Vector3(Grid.GetCellCenterLocal(cellPosition).x, Transform.localPosition.y, Transform.localPosition.z);
        else if (Left || Right) Transform.localPosition = new Vector3(Transform.localPosition.x, Grid.GetCellCenterLocal(cellPosition).y, Transform.localPosition.z);*/

        if (Up || Down) Transform.localPosition = Vector3.Lerp(Transform.localPosition, new Vector3(Grid.GetCellCenterLocal(cellPosition).x, Transform.localPosition.y, Transform.localPosition.z), Speed);
        else if (Left || Right) Transform.localPosition = Vector3.Lerp(Transform.localPosition, new Vector3(Transform.localPosition.x, Grid.GetCellCenterLocal(cellPosition).y, Transform.localPosition.z), Speed);

    }
}
