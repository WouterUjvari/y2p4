using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipList", menuName = "ClipLists/ClipList", order = 1)]
public class ClipList : ScriptableObject {
	public List<AudioClip> clips = new List<AudioClip>();
}
