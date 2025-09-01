using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class LibraryMoteManager : MonoBehaviour
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x000525A5 File Offset: 0x000507A5
	private void Awake()
	{
		LibraryMoteManager.instance = this;
		NetworkManager.LeftRoom = (Action)Delegate.Combine(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
		this.CollectSpawns();
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x000525D4 File Offset: 0x000507D4
	private void CollectSpawns()
	{
		int childCount = base.transform.childCount;
		this.SpawnPoints = new List<Transform>();
		for (int i = 0; i < childCount; i++)
		{
			this.SpawnPoints.Add(base.transform.GetChild(i));
		}
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0005261C File Offset: 0x0005081C
	public static void TickUpdate()
	{
		if (LibraryMoteManager.instance == null)
		{
			return;
		}
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (LibraryMoteManager.instance.MotesOut.Count >= 15)
		{
			return;
		}
		LibraryMoteManager.instance.LocalUpdate();
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0005266C File Offset: 0x0005086C
	private void LocalUpdate()
	{
		if (this.spawnCD >= 0f)
		{
			this.spawnCD -= Time.deltaTime;
			if (this.spawnCD <= 0f)
			{
				List<Transform> availableSpawns = LibraryMoteManager.instance.GetAvailableSpawns();
				if (availableSpawns.Count == 0)
				{
					return;
				}
				int index = UnityEngine.Random.Range(0, availableSpawns.Count);
				Transform transform = availableSpawns[index];
				this.HostSpawnMote(this.GetMoteToSpawn(), transform);
				this.recentSpawns.Add(transform);
				if (this.recentSpawns.Count > 10)
				{
					this.recentSpawns.RemoveAt(0);
				}
				this.spawnCD = this.SpawnCDCurve.Evaluate((float)this.MotesOut.Count / 15f);
			}
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0005272B File Offset: 0x0005092B
	public void HostSpawnMote(LibraryMoteManager.LibraryMoteRef mote, Transform point)
	{
		GoalManager.instance.LibSpawnMote(point.position, this.MoteOptions.IndexOf(mote));
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0005274C File Offset: 0x0005094C
	public static void SpawnMote(Vector3 point, int moteIndex)
	{
		if (LibraryMoteManager.instance == null || moteIndex < 0 || moteIndex >= LibraryMoteManager.instance.MoteOptions.Count)
		{
			return;
		}
		LibraryMoteManager.LibraryMoteRef refObj = LibraryMoteManager.instance.MoteOptions[moteIndex];
		Transform spawnPoint = LibraryMoteManager.instance.GetSpawnPoint(point);
		if (spawnPoint == null)
		{
			return;
		}
		LibraryMoteManager.LibMote item = new LibraryMoteManager.LibMote(refObj, moteIndex, spawnPoint);
		LibraryMoteManager.instance.MotesOut.Add(item);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x000527C0 File Offset: 0x000509C0
	public static void MoteCollectedLocal(LibraryMotePickup pickup)
	{
		if (LibraryMoteManager.instance == null)
		{
			return;
		}
		for (int i = 0; i < LibraryMoteManager.instance.MotesOut.Count; i++)
		{
			if (!(LibraryMoteManager.instance.MotesOut[i].Pickup != pickup))
			{
				LibraryMoteManager.instance.MotesOut[i].Clear();
				LibraryMoteManager.instance.MotesOut.RemoveAt(i);
				break;
			}
		}
		GoalManager.instance.LibMoteCollectedLocal(pickup.transform.position);
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x00052850 File Offset: 0x00050A50
	public static void MoteCollected(Vector3 point)
	{
		if (LibraryMoteManager.instance == null)
		{
			return;
		}
		for (int i = 0; i < LibraryMoteManager.instance.MotesOut.Count; i++)
		{
			if (Vector3.Distance(LibraryMoteManager.instance.MotesOut[i].Location, point) <= 0.25f)
			{
				LibraryMoteManager.instance.MotesOut[i].Clear();
				LibraryMoteManager.instance.MotesOut.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x000528D0 File Offset: 0x00050AD0
	public static void SpawnAllMotes(string motestring)
	{
		if (LibraryMoteManager.instance == null)
		{
			return;
		}
		string[] array = motestring.Split(',', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split('&', StringSplitOptions.None);
			if (array2.Length == 2)
			{
				int num = int.Parse(array2[0]);
				Vector3 point = array2[1].ToVector3();
				if (num >= 0 && num < LibraryMoteManager.instance.MoteOptions.Count)
				{
					LibraryMoteManager.LibraryMoteRef refObj = LibraryMoteManager.instance.MoteOptions[num];
					Transform spawnPoint = LibraryMoteManager.instance.GetSpawnPoint(point);
					if (!(spawnPoint == null))
					{
						LibraryMoteManager.LibMote item = new LibraryMoteManager.LibMote(refObj, num, spawnPoint);
						LibraryMoteManager.instance.MotesOut.Add(item);
					}
				}
			}
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0005298C File Offset: 0x00050B8C
	public static string GetMoteString()
	{
		if (LibraryMoteManager.instance == null)
		{
			return "";
		}
		string text = "";
		foreach (LibraryMoteManager.LibMote libMote in LibraryMoteManager.instance.MotesOut)
		{
			text = string.Concat(new string[]
			{
				text,
				libMote.MoteIndex.ToString(),
				"&",
				libMote.Location.ToDetailedString(),
				","
			});
		}
		if (text.Length > 1)
		{
			string text2 = text;
			int length = text2.Length - 1 - 0;
			text = text2.Substring(0, length);
		}
		return text;
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x00052A50 File Offset: 0x00050C50
	private static void ClearMotes()
	{
		if (LibraryMoteManager.instance == null)
		{
			return;
		}
		foreach (LibraryMoteManager.LibMote libMote in LibraryMoteManager.instance.MotesOut)
		{
			UnityEngine.Object.Destroy(libMote.Pickup.gameObject);
		}
		LibraryMoteManager.instance.MotesOut.Clear();
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00052ACC File Offset: 0x00050CCC
	private LibraryMoteManager.LibraryMoteRef GetMoteToSpawn()
	{
		List<LibraryMoteManager.LibraryMoteRef> list = new List<LibraryMoteManager.LibraryMoteRef>();
		foreach (LibraryMoteManager.LibraryMoteRef libraryMoteRef in LibraryMoteManager.instance.MoteOptions)
		{
			int num = 0;
			while ((float)num < libraryMoteRef.Abundance)
			{
				list.Add(libraryMoteRef);
				num++;
			}
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00052B50 File Offset: 0x00050D50
	private Transform GetSpawnPoint(Vector3 point)
	{
		foreach (Transform transform in this.SpawnPoints)
		{
			if (Vector3.Distance(point, transform.position) < 0.5f)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x00052BB8 File Offset: 0x00050DB8
	private List<Transform> GetAvailableSpawns()
	{
		List<Transform> list = new List<Transform>();
		foreach (Transform item in this.SpawnPoints)
		{
			list.Add(item);
		}
		foreach (Transform item2 in this.recentSpawns)
		{
			list.Remove(item2);
		}
		foreach (LibraryMoteManager.LibMote libMote in this.MotesOut)
		{
			list.Remove(libMote.SpawnPoint);
		}
		return list;
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00052CA0 File Offset: 0x00050EA0
	private void OnLeftRoom()
	{
		LibraryMoteManager.ClearMotes();
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00052CA7 File Offset: 0x00050EA7
	private void OnDestroy()
	{
		NetworkManager.LeftRoom = (Action)Delegate.Remove(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x00052CCC File Offset: 0x00050ECC
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Gizmos.DrawSphere(base.transform.GetChild(i).position, 0.25f);
		}
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00052D16 File Offset: 0x00050F16
	public LibraryMoteManager()
	{
	}

	// Token: 0x04000A55 RID: 2645
	private static LibraryMoteManager instance;

	// Token: 0x04000A56 RID: 2646
	public List<LibraryMoteManager.LibraryMoteRef> MoteOptions;

	// Token: 0x04000A57 RID: 2647
	private List<LibraryMoteManager.LibMote> MotesOut = new List<LibraryMoteManager.LibMote>();

	// Token: 0x04000A58 RID: 2648
	public AnimationCurve SpawnCDCurve;

	// Token: 0x04000A59 RID: 2649
	public const int MAX_MOTES_OUT = 15;

	// Token: 0x04000A5A RID: 2650
	private List<Transform> SpawnPoints;

	// Token: 0x04000A5B RID: 2651
	private List<Transform> recentSpawns = new List<Transform>();

	// Token: 0x04000A5C RID: 2652
	private float spawnCD;

	// Token: 0x02000516 RID: 1302
	[Serializable]
	public class LibraryMoteRef
	{
		// Token: 0x060023C9 RID: 9161 RVA: 0x000CBF02 File Offset: 0x000CA102
		public LibraryMoteRef()
		{
		}

		// Token: 0x040025C5 RID: 9669
		public GameObject SpawnRef;

		// Token: 0x040025C6 RID: 9670
		[Range(0f, 100f)]
		public float Abundance;
	}

	// Token: 0x02000517 RID: 1303
	[Serializable]
	public class LibMote
	{
		// Token: 0x060023CA RID: 9162 RVA: 0x000CBF0C File Offset: 0x000CA10C
		public LibMote(LibraryMoteManager.LibraryMoteRef refObj, int index, Transform spawnPoint)
		{
			this.SpawnPoint = spawnPoint;
			this.Location = this.SpawnPoint.position;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(refObj.SpawnRef, this.Location, spawnPoint.rotation);
			this.Pickup = gameObject.GetComponent<LibraryMotePickup>();
			this.MoteIndex = index;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000CBF62 File Offset: 0x000CA162
		public void Clear()
		{
			if (this.Pickup == null)
			{
				return;
			}
			this.Pickup.Trigger();
		}

		// Token: 0x040025C7 RID: 9671
		public int MoteIndex;

		// Token: 0x040025C8 RID: 9672
		public Vector3 Location;

		// Token: 0x040025C9 RID: 9673
		public LibraryMotePickup Pickup;

		// Token: 0x040025CA RID: 9674
		public Transform SpawnPoint;
	}
}
