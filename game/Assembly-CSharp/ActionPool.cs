using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ActionPool : MonoBehaviour
{
	// Token: 0x06000070 RID: 112 RVA: 0x000063DB File Offset: 0x000045DB
	private void Awake()
	{
		ActionPool.instance = this;
		this.root = base.transform;
		UnityEngine.Object.DontDestroyOnLoad(this);
		this.prefabLookup = new Dictionary<GameObject, ActionPool.ObjectPool<GameObject>>();
		this.instanceLookup = new Dictionary<GameObject, ActionPool.ObjectPool<GameObject>>();
		this.toReleaseDelayed = new Dictionary<GameObject, ActionPool.Timer>();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006418 File Offset: 0x00004618
	private void Update()
	{
		this.keysToRemove.Clear();
		float deltaTime = GameplayManager.deltaTime;
		foreach (KeyValuePair<GameObject, ActionPool.Timer> keyValuePair in this.toReleaseDelayed)
		{
			keyValuePair.Value.TimeRemaining -= deltaTime;
			if (keyValuePair.Value.TimeRemaining <= 0f)
			{
				this.keysToRemove.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < this.keysToRemove.Count; i++)
		{
			this.toReleaseDelayed.Remove(this.keysToRemove[i]);
			ActionPool.ReleaseObject(this.keysToRemove[i]);
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000064F0 File Offset: 0x000046F0
	public static void WarmGlobalAugments()
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in AIManager.GlobalEnemyMods.trees)
		{
			keyValuePair.Key.WarmPoolObjects();
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair2 in InkManager.PurchasedMods.trees)
		{
			keyValuePair2.Key.WarmPoolObjects();
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair3 in GameplayManager.instance.GenreBindings.trees)
		{
			keyValuePair3.Key.WarmPoolObjects();
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000065E8 File Offset: 0x000047E8
	public static void Warm(GameObject prefab, int size)
	{
		if (ActionPool.instance.PoolDisabled)
		{
			return;
		}
		ActionPool.instance.warmPool(prefab, size);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00006603 File Offset: 0x00004803
	public static void ClearAll()
	{
		ActionPool.instance.clear();
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000660F File Offset: 0x0000480F
	public static GameObject SpawnObject(GameObject prefab)
	{
		return ActionPool.instance.spawnObject(prefab);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000661C File Offset: 0x0000481C
	public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		if (ActionPool.instance.PoolDisabled)
		{
			return UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation);
		}
		return ActionPool.instance.spawnObject(prefab, position, rotation);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00006640 File Offset: 0x00004840
	public static void ReleaseObject(GameObject clone)
	{
		if (clone == null)
		{
			return;
		}
		if (!ActionPool.instance.PoolDisabled)
		{
			ActionPool actionPool = ActionPool.instance;
			if (actionPool != null && actionPool.releaseObject(clone))
			{
				return;
			}
		}
		UnityEngine.Object.Destroy(clone);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00006674 File Offset: 0x00004874
	public static void ReleaseDelayed(GameObject clone, float delay)
	{
		if (ActionPool.instance.toReleaseDelayed.ContainsKey(clone))
		{
			ActionPool.instance.toReleaseDelayed[clone].TimeRemaining = Mathf.Min(delay, ActionPool.instance.toReleaseDelayed[clone].TimeRemaining);
			return;
		}
		ActionPool.instance.toReleaseDelayed.Add(clone, new ActionPool.Timer(delay));
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000066DA File Offset: 0x000048DA
	public static void CancelRelease(GameObject clone)
	{
		ActionPool.instance.toReleaseDelayed.Remove(clone);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000066ED File Offset: 0x000048ED
	public static bool IsPooled(GameObject clone)
	{
		ActionPool actionPool = ActionPool.instance;
		return actionPool != null && actionPool.instanceLookup.ContainsKey(clone);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006708 File Offset: 0x00004908
	private void clear()
	{
		for (int i = base.transform.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(i).gameObject);
		}
		this.instanceLookup.Clear();
		this.prefabLookup.Clear();
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000675C File Offset: 0x0000495C
	private void warmPool(GameObject prefab, int size)
	{
		if (this.prefabLookup.ContainsKey(prefab))
		{
			return;
		}
		ActionPool.ObjectPool<GameObject> value = new ActionPool.ObjectPool<GameObject>(() => this.InstantiatePrefab(prefab), size);
		this.prefabLookup[prefab] = value;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000067B6 File Offset: 0x000049B6
	private GameObject spawnObject(GameObject prefab)
	{
		return this.spawnObject(prefab, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000067CC File Offset: 0x000049CC
	private GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		if (!this.prefabLookup.ContainsKey(prefab))
		{
			if (prefab.GetComponent<NoPool>() != null)
			{
				return UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation);
			}
			this.warmPool(prefab, 1);
		}
		ActionPool.ObjectPool<GameObject> objectPool = this.prefabLookup[prefab];
		GameObject item = objectPool.GetItem();
		if (item == null)
		{
			Debug.LogError("Pooled object is null -> " + prefab.name);
			return null;
		}
		item.transform.SetPositionAndRotation(position, rotation);
		item.transform.localScale = prefab.transform.localScale;
		item.SetActive(true);
		this.instanceLookup.Add(item, objectPool);
		return item;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006874 File Offset: 0x00004A74
	private bool releaseObject(GameObject clone)
	{
		if (clone == null)
		{
			return false;
		}
		clone.SetActive(false);
		if (!this.instanceLookup.ContainsKey(clone))
		{
			return false;
		}
		this.toReleaseDelayed.Remove(clone);
		this.instanceLookup[clone].ReleaseItem(clone);
		this.instanceLookup.Remove(clone);
		return true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000068D0 File Offset: 0x00004AD0
	private GameObject InstantiatePrefab(GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, this.root);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000068E5 File Offset: 0x00004AE5
	public ActionPool()
	{
	}

	// Token: 0x04000074 RID: 116
	public Transform root;

	// Token: 0x04000075 RID: 117
	public bool PoolDisabled;

	// Token: 0x04000076 RID: 118
	private static ActionPool instance;

	// Token: 0x04000077 RID: 119
	private Dictionary<GameObject, ActionPool.ObjectPool<GameObject>> prefabLookup;

	// Token: 0x04000078 RID: 120
	private Dictionary<GameObject, ActionPool.ObjectPool<GameObject>> instanceLookup;

	// Token: 0x04000079 RID: 121
	private Dictionary<GameObject, ActionPool.Timer> toReleaseDelayed;

	// Token: 0x0400007A RID: 122
	private List<GameObject> keysToRemove = new List<GameObject>();

	// Token: 0x020003E4 RID: 996
	private class ObjectPool<T>
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x000BFDDB File Offset: 0x000BDFDB
		public ObjectPool(Func<T> factoryFunc, int initialSize = 1)
		{
			this.factoryFunc = factoryFunc;
			this.list = new List<ActionPool.ObjectPoolContainer<T>>(initialSize);
			this.lookup = new Dictionary<T, ActionPool.ObjectPoolContainer<T>>(initialSize);
			this.Warm(initialSize);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000BFE0C File Offset: 0x000BE00C
		private void Warm(int capacity)
		{
			for (int i = 0; i < capacity; i++)
			{
				this.CreateContainer();
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000BFE2C File Offset: 0x000BE02C
		private ActionPool.ObjectPoolContainer<T> CreateContainer()
		{
			ActionPool.ObjectPoolContainer<T> objectPoolContainer = new ActionPool.ObjectPoolContainer<T>();
			objectPoolContainer.Item = this.factoryFunc();
			this.list.Add(objectPoolContainer);
			return objectPoolContainer;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000BFE60 File Offset: 0x000BE060
		public T GetItem()
		{
			ActionPool.ObjectPoolContainer<T> objectPoolContainer = null;
			for (int i = 0; i < this.list.Count; i++)
			{
				this.lastIndex++;
				if (this.lastIndex > this.list.Count - 1)
				{
					this.lastIndex = 0;
				}
				if (!this.list[this.lastIndex].Used)
				{
					objectPoolContainer = this.list[this.lastIndex];
					break;
				}
			}
			if (objectPoolContainer == null)
			{
				objectPoolContainer = this.CreateContainer();
			}
			objectPoolContainer.Consume();
			this.lookup.Add(objectPoolContainer.Item, objectPoolContainer);
			return objectPoolContainer.Item;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000BFF03 File Offset: 0x000BE103
		public void ReleaseItem(object item)
		{
			this.ReleaseItem((T)((object)item));
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000BFF14 File Offset: 0x000BE114
		public void ReleaseItem(T item)
		{
			if (this.lookup.ContainsKey(item))
			{
				this.lookup[item].Release();
				this.lookup.Remove(item);
				return;
			}
			string str = "This object pool does not contain the item provided: ";
			T t = item;
			Debug.LogWarning(str + ((t != null) ? t.ToString() : null));
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000BFF77 File Offset: 0x000BE177
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x000BFF84 File Offset: 0x000BE184
		public int CountUsedItems
		{
			get
			{
				return this.lookup.Count;
			}
		}

		// Token: 0x040020C3 RID: 8387
		private List<ActionPool.ObjectPoolContainer<T>> list;

		// Token: 0x040020C4 RID: 8388
		private Dictionary<T, ActionPool.ObjectPoolContainer<T>> lookup;

		// Token: 0x040020C5 RID: 8389
		private Func<T> factoryFunc;

		// Token: 0x040020C6 RID: 8390
		private int lastIndex;
	}

	// Token: 0x020003E5 RID: 997
	private class ObjectPoolContainer<T>
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x000BFF91 File Offset: 0x000BE191
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x000BFF99 File Offset: 0x000BE199
		public bool Used
		{
			[CompilerGenerated]
			get
			{
				return this.<Used>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Used>k__BackingField = value;
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000BFFA2 File Offset: 0x000BE1A2
		public void Consume()
		{
			this.Used = true;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x000BFFAB File Offset: 0x000BE1AB
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x000BFFB3 File Offset: 0x000BE1B3
		public T Item
		{
			[CompilerGenerated]
			get
			{
				return this.<Item>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Item>k__BackingField = value;
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000BFFBC File Offset: 0x000BE1BC
		public void Release()
		{
			this.Used = false;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000BFFC5 File Offset: 0x000BE1C5
		public ObjectPoolContainer()
		{
		}

		// Token: 0x040020C7 RID: 8391
		[CompilerGenerated]
		private bool <Used>k__BackingField;

		// Token: 0x040020C8 RID: 8392
		[CompilerGenerated]
		private T <Item>k__BackingField;
	}

	// Token: 0x020003E6 RID: 998
	private class Timer
	{
		// Token: 0x06002055 RID: 8277 RVA: 0x000BFFCD File Offset: 0x000BE1CD
		public Timer(float time)
		{
			this.TimeRemaining = time;
		}

		// Token: 0x040020C9 RID: 8393
		public float TimeRemaining;
	}

	// Token: 0x020003E7 RID: 999
	[CompilerGenerated]
	private sealed class <>c__DisplayClass19_0
	{
		// Token: 0x06002056 RID: 8278 RVA: 0x000BFFDC File Offset: 0x000BE1DC
		public <>c__DisplayClass19_0()
		{
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000BFFE4 File Offset: 0x000BE1E4
		internal GameObject <warmPool>b__0()
		{
			return this.<>4__this.InstantiatePrefab(this.prefab);
		}

		// Token: 0x040020CA RID: 8394
		public ActionPool <>4__this;

		// Token: 0x040020CB RID: 8395
		public GameObject prefab;
	}
}
