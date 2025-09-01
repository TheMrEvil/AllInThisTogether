using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ES3Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000006 RID: 6
public class ES3AutoSaveMgr : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000010 RID: 16 RVA: 0x0000224C File Offset: 0x0000044C
	public static ES3AutoSaveMgr Current
	{
		get
		{
			if (ES3AutoSaveMgr._current == null)
			{
				GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
				foreach (GameObject gameObject in rootGameObjects)
				{
					if (gameObject.name == "Easy Save 3 Manager")
					{
						return ES3AutoSaveMgr._current = gameObject.GetComponent<ES3AutoSaveMgr>();
					}
				}
				GameObject[] array = rootGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					if ((ES3AutoSaveMgr._current = array[i].GetComponentInChildren<ES3AutoSaveMgr>()) != null)
					{
						return ES3AutoSaveMgr._current;
					}
				}
			}
			return ES3AutoSaveMgr._current;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000011 RID: 17 RVA: 0x000022DB File Offset: 0x000004DB
	public static ES3AutoSaveMgr Instance
	{
		get
		{
			return ES3AutoSaveMgr.Current;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022E4 File Offset: 0x000004E4
	public void Save()
	{
		if (this.autoSaves == null || this.autoSaves.Count == 0)
		{
			return;
		}
		this.ManageSlots();
		if (this.settings.location == ES3.Location.Cache && !ES3.FileExists(this.settings))
		{
			ES3.CacheFile(this.settings);
		}
		if (this.autoSaves == null || this.autoSaves.Count == 0)
		{
			ES3.DeleteKey(this.key, this.settings);
		}
		else
		{
			List<GameObject> list = new List<GameObject>();
			foreach (ES3AutoSave es3AutoSave in this.autoSaves)
			{
				if (es3AutoSave != null && es3AutoSave.enabled)
				{
					list.Add(es3AutoSave.gameObject);
				}
			}
			ES3.Save<GameObject[]>(this.key, (from x in list
			orderby ES3AutoSaveMgr.GetDepth(x.transform)
			select x).ToArray<GameObject>(), this.settings);
			if (this.destroyedIds != null && this.destroyedIds.Count > 0)
			{
				ES3.Save<List<long>>(this.key + "_destroyed", this.destroyedIds, this.settings);
			}
		}
		if (this.immediatelyCommitToFile && this.settings.location == ES3.Location.Cache && ES3.FileExists(this.settings))
		{
			ES3.StoreCachedFile(this.settings);
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002460 File Offset: 0x00000660
	public void Load()
	{
		this.ManageSlots();
		try
		{
			if (this.settings.location == ES3.Location.Cache && !ES3.FileExists(this.settings))
			{
				ES3.CacheFile(this.settings);
			}
		}
		catch
		{
		}
		ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(base.gameObject.scene, false);
		managerFromScene.Awake();
		ES3.Load<GameObject[]>(this.key, new GameObject[0], this.settings);
		foreach (long id in ES3.Load<List<long>>(this.key + "_destroyed", new List<long>(), this.settings))
		{
			UnityEngine.Object @object = managerFromScene.Get(id, true);
			if (@object != null)
			{
				ES3AutoSave component = ((GameObject)@object).GetComponent<ES3AutoSave>();
				if (component != null)
				{
					ES3AutoSaveMgr.DestroyAutoSave(component);
				}
				UnityEngine.Object.Destroy(@object);
			}
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000256C File Offset: 0x0000076C
	private void Start()
	{
		if (this.loadEvent == ES3AutoSaveMgr.LoadEvent.Start)
		{
			this.Load();
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000257D File Offset: 0x0000077D
	public void Awake()
	{
		ES3AutoSaveMgr.managers[base.gameObject.scene] = this;
		this.GetAutoSaves();
		if (this.loadEvent == ES3AutoSaveMgr.LoadEvent.Awake)
		{
			this.Load();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000025AA File Offset: 0x000007AA
	private void OnApplicationQuit()
	{
		if (this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationQuit)
		{
			this.Save();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000025BB File Offset: 0x000007BB
	private void OnApplicationPause(bool paused)
	{
		if ((this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationPause || (Application.isMobilePlatform && this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationQuit)) && paused)
		{
			this.Save();
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000025E8 File Offset: 0x000007E8
	public static void AddAutoSave(ES3AutoSave autoSave)
	{
		if (autoSave == null)
		{
			return;
		}
		ES3AutoSaveMgr es3AutoSaveMgr;
		if (ES3AutoSaveMgr.managers.TryGetValue(autoSave.gameObject.scene, out es3AutoSaveMgr))
		{
			es3AutoSaveMgr.autoSaves.Add(autoSave);
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002628 File Offset: 0x00000828
	public static void DestroyAutoSave(ES3AutoSave autoSave)
	{
		if (autoSave == null)
		{
			return;
		}
		ES3AutoSaveMgr es3AutoSaveMgr;
		if (ES3AutoSaveMgr.managers.TryGetValue(autoSave.gameObject.scene, out es3AutoSaveMgr))
		{
			es3AutoSaveMgr.autoSaves.Remove(autoSave);
			if (autoSave.saveDestroyed)
			{
				ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(autoSave.gameObject.scene, true);
				if (managerFromScene != null)
				{
					long num = managerFromScene.Add(autoSave.gameObject);
					if (num != -1L)
					{
						es3AutoSaveMgr.destroyedIds.Add(num);
					}
				}
			}
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000026A8 File Offset: 0x000008A8
	public void GetAutoSaves()
	{
		this.autoSaves = new HashSet<ES3AutoSave>();
		foreach (GameObject gameObject in base.gameObject.scene.GetRootGameObjects())
		{
			this.autoSaves.UnionWith(gameObject.GetComponentsInChildren<ES3AutoSave>(true));
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000026F8 File Offset: 0x000008F8
	private static int GetDepth(Transform t)
	{
		int num = 0;
		while (t.parent != null)
		{
			t = t.parent;
			num++;
		}
		return num;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002724 File Offset: 0x00000924
	private void ManageSlots()
	{
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002728 File Offset: 0x00000928
	public ES3AutoSaveMgr()
	{
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002790 File Offset: 0x00000990
	// Note: this type is marked as 'beforefieldinit'.
	static ES3AutoSaveMgr()
	{
	}

	// Token: 0x0400000C RID: 12
	public static ES3AutoSaveMgr _current = null;

	// Token: 0x0400000D RID: 13
	public static Dictionary<Scene, ES3AutoSaveMgr> managers = new Dictionary<Scene, ES3AutoSaveMgr>();

	// Token: 0x0400000E RID: 14
	public string key = Guid.NewGuid().ToString();

	// Token: 0x0400000F RID: 15
	public bool immediatelyCommitToFile = true;

	// Token: 0x04000010 RID: 16
	public ES3AutoSaveMgr.SaveEvent saveEvent = ES3AutoSaveMgr.SaveEvent.OnApplicationQuit;

	// Token: 0x04000011 RID: 17
	public ES3AutoSaveMgr.LoadEvent loadEvent = ES3AutoSaveMgr.LoadEvent.Start;

	// Token: 0x04000012 RID: 18
	public ES3SerializableSettings settings = new ES3SerializableSettings("SaveFile.es3", ES3.Location.Cache);

	// Token: 0x04000013 RID: 19
	public HashSet<ES3AutoSave> autoSaves = new HashSet<ES3AutoSave>();

	// Token: 0x04000014 RID: 20
	private List<long> destroyedIds = new List<long>();

	// Token: 0x020000ED RID: 237
	public enum LoadEvent
	{
		// Token: 0x0400018B RID: 395
		None,
		// Token: 0x0400018C RID: 396
		Awake,
		// Token: 0x0400018D RID: 397
		Start
	}

	// Token: 0x020000EE RID: 238
	public enum SaveEvent
	{
		// Token: 0x0400018F RID: 399
		None,
		// Token: 0x04000190 RID: 400
		OnApplicationQuit,
		// Token: 0x04000191 RID: 401
		OnApplicationPause
	}

	// Token: 0x020000EF RID: 239
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0001F4D6 File Offset: 0x0001D6D6
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001F4E2 File Offset: 0x0001D6E2
		public <>c()
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001F4EA File Offset: 0x0001D6EA
		internal int <Save>b__15_0(GameObject x)
		{
			return ES3AutoSaveMgr.GetDepth(x.transform);
		}

		// Token: 0x04000192 RID: 402
		public static readonly ES3AutoSaveMgr.<>c <>9 = new ES3AutoSaveMgr.<>c();

		// Token: 0x04000193 RID: 403
		public static Func<GameObject, int> <>9__15_0;
	}
}
