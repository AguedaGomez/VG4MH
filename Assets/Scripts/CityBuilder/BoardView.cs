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
        GameObject lodsGroup = FindGameObjectInChildWithTag(building, "LOD_group");
        GameObject lod0 = FindGameObjectInChildWithTag(lodsGroup, "LOD0");

        MeshFilter currentMesh = lod0.GetComponent<MeshFilter>();
        //MeshFilter currentMesh = building.transform.Find("SM_Base_Building_4_LODS_Group").Find("SM_Base_Building_4_LOD0").GetComponent<MeshFilter>(); //change to shared mesh
        buildGhost.ChangeMesh(currentMesh);

        Building buildingScript = building.GetComponent<Building>();
        buildGhost.SetBuilding(buildingScript);

        //Transform buildingTransform = building.GetComponent<Transform>();
        Transform buildGhostTransform = buildGhost.GetComponent<Transform>();
        Quaternion buildingRotation = buildGhostTransform.rotation;

        //buildGhost.SetColor(spaceAvailable);
        ColorDependingAvailability(spaceAvailable);

        return Instantiate(editBuilding, position, buildingRotation);

    }

    public void ColorDependingAvailability(bool spaceAvailable)
    {
        buildGhost.SetColor(spaceAvailable);
    }

    private GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }
}
