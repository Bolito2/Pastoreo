using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		const string projectId = "2eef4917-e3da-4fbb-bbfb-3acb8cda8827";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}
