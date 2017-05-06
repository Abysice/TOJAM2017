using UnityEngine;
using System.Collections;

public class ShelfController : MonoBehaviour {

	private LibraryManager m_manager;


	// Use this for initialization
	void Start () {
		m_manager = Managers.GetInstance ().GetLibraryManager ();

	}


}
