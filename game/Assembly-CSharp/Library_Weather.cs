using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class Library_Weather : MonoBehaviour
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x00056D2F File Offset: 0x00054F2F
	private void Awake()
	{
		Library_Weather.instance = this;
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Combine(NetworkManager.OnRoomPlayersChanged, new Action(this.SyncWeather));
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00056D57 File Offset: 0x00054F57
	private void Start()
	{
		LibraryManager libraryManager = LibraryManager.instance;
		libraryManager.OnLibraryEntered = (Action)Delegate.Combine(libraryManager.OnLibraryEntered, new Action(this.OnEnter));
		if (PlayerControl.myInstance != null)
		{
			this.OnEnter();
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x00056D92 File Offset: 0x00054F92
	private void OnEnter()
	{
		Debug.Log("Entering Library");
		if (!PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient)
		{
			this.SetWeather(UnityEngine.Random.Range(0, 4), true);
		}
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x00056DBC File Offset: 0x00054FBC
	public void SetWeather(int index, bool updateNet = false)
	{
		if (updateNet)
		{
			MapManager.instance.SetLibWeather(index);
		}
		this.currentSeason = (Library_Weather.Season)index;
		Debug.Log("Setting Weather to " + index.ToString());
		this.SummerWeather.SetActive(this.currentSeason == Library_Weather.Season.Summer);
		this.FallWeather.SetActive(this.currentSeason == Library_Weather.Season.Fall);
		this.WinterWeather.SetActive(this.currentSeason == Library_Weather.Season.Winter);
		this.SpringWeather.SetActive(this.currentSeason == Library_Weather.Season.Spring);
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x00056E44 File Offset: 0x00055044
	private void SyncWeather()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.SetLibWeather((int)this.currentSeason);
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00056E5D File Offset: 0x0005505D
	private void OnDestroy()
	{
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Remove(NetworkManager.OnRoomPlayersChanged, new Action(this.SyncWeather));
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00056E7F File Offset: 0x0005507F
	public Library_Weather()
	{
	}

	// Token: 0x04000B2B RID: 2859
	public static Library_Weather instance;

	// Token: 0x04000B2C RID: 2860
	public GameObject SummerWeather;

	// Token: 0x04000B2D RID: 2861
	public GameObject FallWeather;

	// Token: 0x04000B2E RID: 2862
	public GameObject WinterWeather;

	// Token: 0x04000B2F RID: 2863
	public GameObject SpringWeather;

	// Token: 0x04000B30 RID: 2864
	private Library_Weather.Season currentSeason;

	// Token: 0x0200052D RID: 1325
	public enum Season
	{
		// Token: 0x04002631 RID: 9777
		Summer,
		// Token: 0x04002632 RID: 9778
		Fall,
		// Token: 0x04002633 RID: 9779
		Winter,
		// Token: 0x04002634 RID: 9780
		Spring
	}
}
