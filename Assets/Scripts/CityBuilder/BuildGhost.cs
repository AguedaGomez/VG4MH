using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildGhost : MonoBehaviour
{
    [SerializeField] Material ghostMaterial;
    [SerializeField] Color colorClear = Color.cyan;
    [SerializeField] Color colorOccupied = Color.red;
    
    
    [SerializeField] public Vector2Int buildingSize = new Vector2Int(2,2);
    [SerializeField] BoxCollider buildingCellBoxCollider;

    [SerializeField] MeshFilter buildingMeshFilter; //meshfilter?

   // [SerializeField] MeshFilter cellsMeshFilter;
   // [SerializeField] bool occupied = false;
   // [SerializeField] float cellThickness = .1f;
    //[SerializeField] const float cellGroundOffset = -.01f;

  //  [SerializeField] bool refreshSize = false;

    //NUEVO
    [SerializeField] GameObject cellObject;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //ConstructCell(buildingSize);
        RescaleCell();
       
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //if (occupied)
    //    //{
    //    //    ghostMaterial.SetColor("_Color", colorOccupied);
    //    //}
    //    //else 
    //    //{
    //    //    ghostMaterial.SetColor("_Color", colorClear);
    //    //}
    //    //if (refreshSize) 
    //    //{
    //    //    ConstructCell(buildingSize);
    //    //    refreshSize = !refreshSize; 
    //    //}
    //}

    public void SetColor(bool available)
    {
        ghostMaterial.SetColor("_Color", available ? colorClear : colorOccupied); 
    }

    public void SetBuilding(Building building) 
    {
        buildingSize = new Vector2Int(building.cellsInX, building.cellsInZ);

    }

    public void ChangeMeshAndScale(MeshFilter currentMesh, Vector3 newScale, Quaternion newRotation)
    {
        buildingMeshFilter.sharedMesh = currentMesh.sharedMesh;
        buildingMeshFilter.gameObject.transform.localScale = newScale;
        buildingMeshFilter.gameObject.transform.rotation = newRotation;
        //ScaleCollider();
        //buildingMeshFilter.transform.position = new Vector3(buildingMeshFilter.transform.position.x + cellsMeshFilter.transform.position.x,
          //0, buildingMeshFilter.transform.position.z+ cellsMeshFilter.transform.position.z);
        //Bounds bounds = buildingMeshFilter.GetComponent<Renderer>().bounds;
        //Debug.Log("bound in y: " + bounds.size.y);
        //buildingCellBoxCollider.size = new Vector3(buildingCellBoxCollider.size.x, bounds.size.y, buildingCellBoxCollider.size.z);
        
    }

    private void RescaleCell()
    {
        Vector3 scaleChange = cellObject.transform.localScale;
        scaleChange.x = 4 * buildingSize.x;
        scaleChange.z = 4 * buildingSize.y;
        cellObject.transform.localScale = scaleChange;
    }
    private int CalculateDisplacement(int d) 
    {
        return (2 * d) + (2 * (d - 1));
        //return (2 * d) + (2 * d);
    }

   // private void ConstructCell(Vector2Int size)
   // {
   //     float mitadx = (4 * size.x);
   //     float mitadz = (4 * size.y) ;
   //     var mesh = new Mesh();
   //     mesh.name = "BuildingCells";

   //     mesh.vertices = new Vector3[] {
   //         new Vector3(-2, cellGroundOffset, -CalculateDisplacement(size.y)), //aquí -2 en mitadx
   //         new Vector3(CalculateDisplacement(size.x), cellGroundOffset, -CalculateDisplacement(size.y)),
   //         new Vector3(CalculateDisplacement(size.x), cellThickness, -CalculateDisplacement(size.y)),
   //         new Vector3(-2, cellThickness, -CalculateDisplacement(size.y)),
   //         new Vector3(-2, cellThickness, 2),
   //         new Vector3(CalculateDisplacement(size.x), cellThickness, 2),
   //         new Vector3(CalculateDisplacement(size.x), cellGroundOffset, 2),
   //         new Vector3(-2, cellGroundOffset, 2),
   //     };

   //     mesh.triangles = new int[] {
   //         0, 2, 1, //face front
			//0, 3, 2,
   //         2, 3, 4, //face top
			//2, 4, 5,
   //         1, 2, 5, //face right
			//1, 5, 6,
   //         0, 7, 4, //face left
			//0, 4, 3,
   //         5, 4, 7, //face back
			//5, 7, 6,
   //         0, 6, 7, //face bottom
			//0, 1, 6
   //     };

   //     mesh.Optimize();
   //     mesh.RecalculateNormals();
   //     //cellsMeshFilter.sharedMesh.Clear();
   //     cellsMeshFilter.mesh = mesh;
   //     ScaleCollider();
   //     //buildingCellBoxCollider.center = new Vector3(buildingCellBoxCollider.center.x, buildingCellBoxCollider.center.y + buildingMeshFilter.sharedMesh.bounds.size.y / 2,
   //        // buildingCellBoxCollider.center.z);
        
        
   // }

    private void ScaleCollider()
    {
        buildingCellBoxCollider.size = new Vector3(buildingCellBoxCollider.size.x, buildingMeshFilter.sharedMesh.bounds.size.y, buildingCellBoxCollider.size.z);
    }

}
