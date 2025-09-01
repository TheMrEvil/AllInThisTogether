using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class ZoneManager : MonoBehaviour
{
	// Token: 0x06000A7F RID: 2687 RVA: 0x0004428B File Offset: 0x0004248B
	[RuntimeInitializeOnLoadMethod]
	private static void Init()
	{
		ZoneManager.instance = null;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00044294 File Offset: 0x00042494
	private void Awake()
	{
		ZoneManager.instance = this;
		ZoneManager.Zones = new List<ZoneArea>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.OnSceneChanged));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.StateChanged));
		this.OnSceneChanged();
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000442F8 File Offset: 0x000424F8
	private void StateChanged(GameState from, GameState to)
	{
		if (ZoneManager.Zones.Count == 0 || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (to == GameState.Ended)
		{
			foreach (ZoneArea zoneArea in ZoneManager.Zones)
			{
				zoneArea.TryCapture(true);
			}
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00044364 File Offset: 0x00042564
	private void OnSceneChanged()
	{
		this.SpawnPoints.Clear();
		ZoneSpawns zoneSpawns = UnityEngine.Object.FindObjectOfType<ZoneSpawns>();
		if (zoneSpawns != null)
		{
			this.SpawnPoints = zoneSpawns.Spawns;
		}
		ZoneManager.Zones.Clear();
		GreyscaleAreas.ClearZones();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x000443A8 File Offset: 0x000425A8
	public static void CreateNewZone(ZoneProperties props)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		List<Transform> validSpawns = ZoneManager.GetValidSpawns();
		if (validSpawns.Count > 0)
		{
			ZoneManager.instance.CreateZone(validSpawns[UnityEngine.Random.Range(0, validSpawns.Count)].position, props);
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x000443EE File Offset: 0x000425EE
	private void CreateZone(Vector3 location, ZoneProperties props)
	{
		PhotonNetwork.InstantiateRoomObject("GoalZone", location, Quaternion.identity, 0, null).GetComponent<ZoneArea>().Setup(props);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00044410 File Offset: 0x00042610
	private static List<Transform> GetValidSpawns()
	{
		List<Transform> list = new List<Transform>();
		foreach (Transform transform in ZoneManager.instance.SpawnPoints)
		{
			bool flag = false;
			using (List<ZoneArea>.Enumerator enumerator2 = ZoneManager.Zones.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (Vector3.Distance(enumerator2.Current.transform.position, transform.position) <= 3f)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				break;
			}
			list.Add(transform);
		}
		return list;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x000444D0 File Offset: 0x000426D0
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.StateChanged));
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.OnSceneChanged));
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0004451D File Offset: 0x0004271D
	public ZoneManager()
	{
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00044530 File Offset: 0x00042730
	// Note: this type is marked as 'beforefieldinit'.
	static ZoneManager()
	{
	}

	// Token: 0x040008DC RID: 2268
	public static ZoneManager instance;

	// Token: 0x040008DD RID: 2269
	private List<Transform> SpawnPoints = new List<Transform>();

	// Token: 0x040008DE RID: 2270
	public static List<ZoneArea> Zones = new List<ZoneArea>();
}
