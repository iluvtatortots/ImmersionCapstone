using UnityEngine;
using System.Collections;

public class MusicSwitcher : MonoBehaviour {

	public AudioSource HappySong;
	static private AudioSource happySong;

	public AudioSource EvilSong;
	static private AudioSource evilSong;

	private void Start() {
		happySong = HappySong;
		evilSong = EvilSong;
		SongSwitch(false);
	}



	static public void SongSwitch(bool isEvil) {
		if(isEvil) {
			happySong.volume = 0f;
			evilSong.volume = 1f;
		} else {
			happySong.volume = 1f;
			evilSong.volume = 0f;
		}
	}


}
