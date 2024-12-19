using UnityEngine;

public class ParametersEvent: Unity.Services.Analytics.Event
{
    public ParametersEvent() : base("ParametersEvent")
    {

    }
    public int MapSize { set { SetParameter("MapSize", value); } }
    public int CyclesPerDay { set { SetParameter("Cycles Per Day", value); } }
    public int Difficulty { set { SetParameter("Difficulty", value); } }
    public bool RootRestrictions { set { SetParameter("Root Restrictions", value); } }
}

