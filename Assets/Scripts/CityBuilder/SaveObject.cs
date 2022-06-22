using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class SaveObject
{
    public static SaveObject Instance { get; private set; } = new SaveObject();

    public int materials;
    public float powerR;
    public string date;
    public List<SavedBuilding> buildingsInBoard;
    public bool activityRunning;

    public bool dailyActivityCompleted;
    public int dailyCompletedSteps;
    public int actualSessionSteps;

    private SaveObject()
    {
        materials = 0;
        powerR = 0;
        date = "";
        buildingsInBoard = new List<SavedBuilding>();
        activityRunning = false;
        dailyActivityCompleted = false;
        dailyCompletedSteps = 0;
        actualSessionSteps = 0;
    }
}
