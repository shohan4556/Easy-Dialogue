using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyDialogue
{	
	
	[CreateAssetMenu(fileName = "new-narrative", menuName = "Create New Narrative", order = 1)]
	public class NarrativeSO : ScriptableObject
	{

		[System.Serializable]
		public class Node
		{
			[TextArea(1, 4)] public string head;
			public Node[] child;
		}
		
		[System.Serializable]
		public class Narrative
		{
			public Sprite characterIcon;
			public string characterName;
			[TextArea(2, 10)] public string[] text;
			//public Node[] nodes;
		}
		
		public Narrative narrative;

	}

}