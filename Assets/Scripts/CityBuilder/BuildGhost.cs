using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildGhost : MonoBehaviour
{
    [SerializeField] Material ghostMaterial;
    [SerializeField] Color colorClear = Color.cyan;
    [SerializeField] Color colorOccupied = Color.red;
    
    Mesh buildingMesh;
    [SerializeField] Vector2Int buildingSize = new Vector2Int(2,2);

    [SerializeField] MeshFilter buildingMeshFilter;
    [SerializeField] MeshFilter cellsMeshFilter;
    [SerializeField] bool occupied = false;
    [SerializeField] float cellThickness = .1f;
    [SerializeField] const float cellGroundOffset = -.01f;

    [SerializeField] bool refreshSize = false;

    // Start is called before the first frame update
    private void OnEnable()
    {
        ConstructCell(buildingSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied)
        {
            ghostMaterial.SetColor("_Color", colorOccupied);
        }
        else 
        {
            ghostMaterial.SetColor("_Color", colorClear);
        }
        if (refreshSize) 
        {
            ConstructCell(buildingSize);
            refreshSize = !refreshSize; 
        }
    }

    public void SetBuilding(Building building) 
    {
        //buildingMesh = building.
        buildingSize = new Vector2Int(building.col, building.row);

    }

    private int CalculateDisplacement(int d) 
    {
        return (2 * d) + (2 * (d - 1));
    }

    private void ConstructCell(Vector2Int size)
    {
        var mesh = new Mesh();
        mesh.name = "BuildingCells";

        mesh.vertices = new Vector3[] {
            new Vector3(-2, cellGroundOffset, -CalculateDisplacement(size.y)),
            new Vector3(CalculateDisplacement(size.x), cellGroundOffset, -CalculateDisplacement(size.y)),
            new Vector3(CalculateDisplacement(size.x), cellThickness, -CalculateDisplacement(size.y)),
            new Vector3(-2, cellThickness, -CalculateDisplacement(size.y)),
            new Vector3(-2, cellThickness, 2),
            new Vector3(CalculateDisplacement(size.x), cellThickness, 2),
            new Vector3(CalculateDisplacement(size.x), cellGroundOffset, 2),
            new Vector3(-2, cellGroundOffset, 2),
        };

        mesh.triangles = new int[] {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        mesh.Optimize();
        mesh.RecalculateNormals();
        //cellsMeshFilter.sharedMesh.Clear();
        cellsMeshFilter.mesh = mesh;
    }
}
