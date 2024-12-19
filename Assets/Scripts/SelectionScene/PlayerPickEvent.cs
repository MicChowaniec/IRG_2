using JetBrains.Annotations;
using UnityEngine;

public class PlayerPickEvent: Unity.Services.Analytics.Event
{
	public PlayerPickEvent() : base("PlayerPickEvent")
	{
	
	}
	public string Name { set { SetParameter("Name", value); } }
}