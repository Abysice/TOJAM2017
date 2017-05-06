using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour {
	private GameObject NPC;
	private int NPCCount;

	// Use this for initialization
	void Start () {
		NPCCount = 0;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnNPC() {
		NPC = Managers.GetInstance().GetGameProperties().NPCPrefab;
		GameObject.Instantiate (NPC);
		NPCCount++;
	}

	public void RemoveNPC() {
		NPCCount--;
	}

	public int GetNPCCount() {
		return NPCCount;
	}
}
