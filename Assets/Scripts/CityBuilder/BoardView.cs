using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    public GameObject editBuilding; //prefab building in edit mode

    private BuildGhost buildGhost;
    // Start is called before the first frame update
    void Start()
    {
        buildGhost = editBuilding.GetComponent<BuildGhost>();
    }

    public GameObject SetUpBuildingEditMode(GameObject building, bool spaceAvailable, Vector3 position)
    {
        MeshFilter currentMesh = building.transform.Find("SM_Base_Building_LODS_Group").Find("SM_Base_Building_LOD0").GetComponent<MeshFilter>();
        buildGhost.buildingMeshFilter = currentMesh;

        Building buildingScript = building.GetComponent<Building>();
        buildGhost.SetBuilding(buildingScript);

        Transform buildingTransform = building.GetComponent<Transform>();
        Quaternion buildingRotation = buildingTransform.rotation;

        //buildGhost.SetColor(spaceAvailable);
        ColorDependingAvailability(spaceAvailable);

        return Instantiate(editBuilding, position, buildingRotation);

    }

    public void ColorDependingAvailability(bool spaceAvailable)
    {
        buildGhost.SetColor(spaceAvailable);
    }
}
