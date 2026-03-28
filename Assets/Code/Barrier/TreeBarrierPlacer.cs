using UnityEngine;
using UnityEditor;

public class TreeBarrierPlacer : MonoBehaviour
{
    [Header("Настройки барьера")]
    public GameObject treePrefab;        // Префаб дерева
    public Vector2 startPoint;           // Начальная точка
    public Vector2 endPoint;             // Конечная точка
    public float spacing = 2f;           // Расстояние между деревьями
    public float offsetY = 0f;           // Смещение по Y
    
    [Header("Случайные вариации")]
    public bool randomRotation = true;
    public float randomScaleRange = 0.2f; // Разброс масштаба
    
    [ContextMenu("Создать барьер")]
    public void CreateBarrier()
    {
        
        // Рассчитываем количество деревьев
        float distance = Vector2.Distance(startPoint, endPoint);
        int treeCount = Mathf.FloorToInt(distance / spacing) + 1;
        
        Vector2 direction = (endPoint - startPoint).normalized;
        
        for (int i = 0; i < treeCount; i++)
        {
            // Позиция текущего дерева
            Vector2 pos = startPoint + direction * (i * spacing);
            Vector3 worldPos = new Vector3(pos.x, offsetY, pos.y);
            
            // Создаем экземпляр
            GameObject tree = Instantiate(treePrefab, worldPos, Quaternion.identity);
            tree.transform.parent = this.transform;
            
            // Добавляем случайные вариации
            if (randomRotation)
            {
                float randomYRot = Random.Range(0f, 360f);
                tree.transform.Rotate(0, randomYRot, 0);
            }
            
            if (randomScaleRange > 0)
            {
                float scaleVariation = 1f + Random.Range(-randomScaleRange, randomScaleRange);
                tree.transform.localScale *= scaleVariation;
            }
        }
        
        Debug.Log($"Создано {treeCount} деревьев");
    }
    
    [ContextMenu("Создать барьер с очищением")]
    public void CreateBarrierNoTrees()
    {
        ClearTrees();
        CreateBarrier();
    }

    [ContextMenu("Очистить деревья")]
    public void ClearTrees()
    {
        // Удаляем все дочерние объекты
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    
    // Визуализация в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 start = new Vector3(startPoint.x, offsetY, startPoint.y);
        Vector3 end = new Vector3(endPoint.x, offsetY, endPoint.y);
        Gizmos.DrawLine(start, end);
        
        // Рисуем точки для каждой позиции
        float distance = Vector2.Distance(startPoint, endPoint);
        int treeCount = Mathf.FloorToInt(distance / spacing) + 1;
        Vector2 direction = (endPoint - startPoint).normalized;
        
        Gizmos.color = Color.yellow;
        for (int i = 0; i < treeCount; i++)
        {
            Vector2 pos = startPoint + direction * (i * spacing);
            Vector3 worldPos = new Vector3(pos.x, offsetY, pos.y);
            Gizmos.DrawWireSphere(worldPos, 0.3f);
        }
    }
}