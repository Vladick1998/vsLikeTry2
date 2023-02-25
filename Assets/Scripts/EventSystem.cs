using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : MonoBehaviour
{
    public static UnityEvent playerChangeStats = new UnityEvent();
    public static void reCalculatePlayerStats()
	{
		playerChangeStats.Invoke();
	}
}
