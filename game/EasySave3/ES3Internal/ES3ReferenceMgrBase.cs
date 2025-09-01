using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ES3Internal
{
	// Token: 0x020000D7 RID: 215
	[DisallowMultipleComponent]
	[Serializable]
	public abstract class ES3ReferenceMgrBase : MonoBehaviour
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001B6B8 File Offset: 0x000198B8
		public static ES3ReferenceMgrBase Current
		{
			get
			{
				if (ES3ReferenceMgrBase._current == null)
				{
					ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(SceneManager.GetActiveScene(), true);
					if (managerFromScene != null)
					{
						ES3ReferenceMgrBase.mgrs.Add(ES3ReferenceMgrBase._current = managerFromScene);
					}
				}
				return ES3ReferenceMgrBase._current;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001B700 File Offset: 0x00019900
		public static ES3ReferenceMgrBase GetManagerFromScene(Scene scene, bool getAnyManagerIfNotInScene = true)
		{
			if (scene.IsValid())
			{
				foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
				{
					if (es3ReferenceMgrBase != null && es3ReferenceMgrBase.gameObject.scene == scene)
					{
						return es3ReferenceMgrBase;
					}
				}
				GameObject[] rootGameObjects;
				try
				{
					rootGameObjects = scene.GetRootGameObjects();
				}
				catch
				{
					return null;
				}
				foreach (GameObject gameObject in rootGameObjects)
				{
					if (gameObject.name == "Easy Save 3 Manager")
					{
						ES3ReferenceMgr component = gameObject.GetComponent<ES3ReferenceMgr>();
						if (component != null)
						{
							return component;
						}
					}
				}
				GameObject[] array = rootGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					ES3ReferenceMgr componentInChildren = array[i].GetComponentInChildren<ES3ReferenceMgr>();
					if (componentInChildren != null)
					{
						return componentInChildren;
					}
				}
			}
			if (getAnyManagerIfNotInScene)
			{
				for (int j = 0; j < SceneManager.sceneCount; j++)
				{
					Scene sceneAt = SceneManager.GetSceneAt(j);
					if (sceneAt != scene && sceneAt.IsValid())
					{
						ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(sceneAt, false);
						if (managerFromScene != null)
						{
							ES3Debug.LogWarning("The reference you're trying to save does not exist in any scene, or the scene it belongs to does not contain an Easy Save 3 Manager. Using the reference manager from scene " + sceneAt.name + " instead. This may cause unexpected behaviour or leak memory in some situations. See <a href=\"https://docs.moodkie.com/easy-save-3/es3-guides/saving-and-loading-references/\">the Saving and Loading References guide</a> for more information.", null, 0);
							return managerFromScene;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001B878 File Offset: 0x00019A78
		public bool IsInitialised
		{
			get
			{
				return this.idRef.Count > 0;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001B888 File Offset: 0x00019A88
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0001B918 File Offset: 0x00019B18
		public ES3RefIdDictionary refId
		{
			get
			{
				if (this._refId == null)
				{
					this._refId = new ES3RefIdDictionary();
					foreach (KeyValuePair<long, UnityEngine.Object> keyValuePair in this.idRef)
					{
						if (keyValuePair.Value != null)
						{
							this._refId[keyValuePair.Value] = keyValuePair.Key;
						}
					}
				}
				return this._refId;
			}
			set
			{
				this._refId = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001B921 File Offset: 0x00019B21
		public ES3GlobalReferences GlobalReferences
		{
			get
			{
				return ES3GlobalReferences.Instance;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001B928 File Offset: 0x00019B28
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			ES3ReferenceMgrBase._current = null;
			ES3ReferenceMgrBase.mgrs = new HashSet<ES3ReferenceMgrBase>();
			ES3ReferenceMgrBase.rng = null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001B940 File Offset: 0x00019B40
		internal void Awake()
		{
			if (ES3ReferenceMgrBase._current != null && ES3ReferenceMgrBase._current != this)
			{
				ES3ReferenceMgrBase current = ES3ReferenceMgrBase._current;
				if (ES3ReferenceMgrBase.Current != null)
				{
					this.RemoveNullValues();
					ES3ReferenceMgrBase._current = current;
				}
			}
			else
			{
				ES3ReferenceMgrBase._current = this;
			}
			ES3ReferenceMgrBase.mgrs.Add(this);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001B99A File Offset: 0x00019B9A
		private void OnDestroy()
		{
			if (ES3ReferenceMgrBase._current == this)
			{
				ES3ReferenceMgrBase._current = null;
			}
			ES3ReferenceMgrBase.mgrs.Remove(this);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public void Merge(ES3ReferenceMgrBase otherMgr)
		{
			foreach (KeyValuePair<long, UnityEngine.Object> keyValuePair in otherMgr.idRef)
			{
				this.Add(keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001BA20 File Offset: 0x00019C20
		public long Get(UnityEngine.Object obj)
		{
			if (!ES3ReferenceMgrBase.mgrs.Contains(this))
			{
				ES3ReferenceMgrBase.mgrs.Add(this);
			}
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					if (obj == null)
					{
						return -1L;
					}
					long result;
					if (es3ReferenceMgrBase.refId.TryGetValue(obj, out result))
					{
						return result;
					}
				}
			}
			return -1L;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		internal UnityEngine.Object Get(long id, Type type, bool suppressWarnings = false)
		{
			if (!ES3ReferenceMgrBase.mgrs.Contains(this))
			{
				ES3ReferenceMgrBase.mgrs.Add(this);
			}
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					if (id == -1L)
					{
						return null;
					}
					UnityEngine.Object @object;
					if (es3ReferenceMgrBase.idRef.TryGetValue(id, out @object))
					{
						if (@object == null)
						{
							return null;
						}
						return @object;
					}
				}
			}
			if (this.GlobalReferences != null)
			{
				UnityEngine.Object object2 = this.GlobalReferences.Get(id);
				if (object2 != null)
				{
					return object2;
				}
			}
			if (!suppressWarnings)
			{
				if (type != null)
				{
					ES3Debug.LogWarning(string.Concat(new string[]
					{
						"Reference for ",
						(type != null) ? type.ToString() : null,
						" with ID ",
						id.ToString(),
						" could not be found in Easy Save's reference manager. See <a href=\"https://docs.moodkie.com/easy-save-3/es3-guides/saving-and-loading-references/#reference-could-not-be-found-warning\">the Saving and Loading References guide</a> for more information."
					}), this, 0);
				}
				else
				{
					ES3Debug.LogWarning("Reference with ID " + id.ToString() + " could not be found in Easy Save's reference manager. See <a href=\"https://docs.moodkie.com/easy-save-3/es3-guides/saving-and-loading-references/#reference-could-not-be-found-warning\">the Saving and Loading References guide</a> for more information.", this, 0);
				}
			}
			return null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public UnityEngine.Object Get(long id, bool suppressWarnings = false)
		{
			return this.Get(id, null, suppressWarnings);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001BBFC File Offset: 0x00019DFC
		public ES3Prefab GetPrefab(long id, bool suppressWarnings = false)
		{
			if (!ES3ReferenceMgrBase.mgrs.Contains(this))
			{
				ES3ReferenceMgrBase.mgrs.Add(this);
			}
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					foreach (ES3Prefab es3Prefab in es3ReferenceMgrBase.prefabs)
					{
						if (es3Prefab != null && es3Prefab.prefabId == id)
						{
							return es3Prefab;
						}
					}
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with ID " + id.ToString() + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene, or exit play mode and right-click the prefab and select Easy Save 3 > Add Reference(s) to Manager.", this, 0);
			}
			return null;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public long GetPrefab(ES3Prefab prefabToFind, bool suppressWarnings = false)
		{
			if (!ES3ReferenceMgrBase.mgrs.Contains(this))
			{
				ES3ReferenceMgrBase.mgrs.Add(this);
			}
			using (HashSet<ES3ReferenceMgrBase>.Enumerator enumerator = ES3ReferenceMgrBase.mgrs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current == null))
					{
						foreach (ES3Prefab es3Prefab in this.prefabs)
						{
							if (es3Prefab == prefabToFind)
							{
								return es3Prefab.prefabId;
							}
						}
					}
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with name " + prefabToFind.name + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene, or exit play mode and right-click the prefab and select Easy Save 3 > Add Reference(s) to Manager.", prefabToFind, 0);
			}
			return -1L;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001BDBC File Offset: 0x00019FBC
		public long Add(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return -1L;
			}
			if (!ES3ReferenceMgrBase.CanBeSaved(obj))
			{
				return -1L;
			}
			long num;
			if (this.refId.TryGetValue(obj, out num))
			{
				return num;
			}
			if (this.GlobalReferences != null)
			{
				num = this.GlobalReferences.GetOrAdd(obj);
				if (num != -1L)
				{
					this.Add(obj, num);
					return num;
				}
			}
			object @lock = this._lock;
			long result;
			lock (@lock)
			{
				num = ES3ReferenceMgrBase.GetNewRefID();
				result = this.Add(obj, num);
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001BE5C File Offset: 0x0001A05C
		public long Add(UnityEngine.Object obj, long id)
		{
			if (obj == null)
			{
				return -1L;
			}
			if (!ES3ReferenceMgrBase.CanBeSaved(obj))
			{
				return -1L;
			}
			if (id == -1L)
			{
				id = ES3ReferenceMgrBase.GetNewRefID();
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.idRef[id] = obj;
				if (obj != null)
				{
					this.refId[obj] = id;
				}
			}
			return id;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		public bool AddPrefab(ES3Prefab prefab)
		{
			if (!this.prefabs.Contains(prefab))
			{
				this.prefabs.Add(prefab);
				return true;
			}
			return false;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BF00 File Offset: 0x0001A100
		public void Remove(UnityEngine.Object obj)
		{
			if (!ES3ReferenceMgrBase.mgrs.Contains(this))
			{
				ES3ReferenceMgrBase.mgrs.Add(this);
			}
			Func<KeyValuePair<long, UnityEngine.Object>, bool> <>9__0;
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null) && (Application.isPlaying || !(es3ReferenceMgrBase != this)))
				{
					object @lock = es3ReferenceMgrBase._lock;
					lock (@lock)
					{
						es3ReferenceMgrBase.refId.Remove(obj);
						IEnumerable<KeyValuePair<long, UnityEngine.Object>> source = es3ReferenceMgrBase.idRef;
						Func<KeyValuePair<long, UnityEngine.Object>, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((KeyValuePair<long, UnityEngine.Object> kvp) => kvp.Value == obj));
						}
						foreach (KeyValuePair<long, UnityEngine.Object> keyValuePair in source.Where(predicate).ToList<KeyValuePair<long, UnityEngine.Object>>())
						{
							es3ReferenceMgrBase.idRef.Remove(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001C04C File Offset: 0x0001A24C
		public void Remove(long referenceID)
		{
			Func<KeyValuePair<UnityEngine.Object, long>, bool> <>9__0;
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					object @lock = es3ReferenceMgrBase._lock;
					lock (@lock)
					{
						es3ReferenceMgrBase.idRef.Remove(referenceID);
						IEnumerable<KeyValuePair<UnityEngine.Object, long>> refId = es3ReferenceMgrBase.refId;
						Func<KeyValuePair<UnityEngine.Object, long>, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((KeyValuePair<UnityEngine.Object, long> kvp) => kvp.Value == referenceID));
						}
						foreach (KeyValuePair<UnityEngine.Object, long> keyValuePair in refId.Where(predicate).ToList<KeyValuePair<UnityEngine.Object, long>>())
						{
							es3ReferenceMgrBase.refId.Remove(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001C16C File Offset: 0x0001A36C
		public void RemoveNullValues()
		{
			foreach (long key in (from pair in this.idRef
			where pair.Value == null
			select pair.Key).ToList<long>())
			{
				this.idRef.Remove(key);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001C214 File Offset: 0x0001A414
		public void RemoveNullOrInvalidValues()
		{
			foreach (long key in (from pair in this.idRef
			where pair.Value == null || !ES3ReferenceMgrBase.CanBeSaved(pair.Value) || this.excludeObjects.Contains(pair.Value)
			select pair.Key).ToList<long>())
			{
				this.idRef.Remove(key);
			}
			if (this.GlobalReferences != null)
			{
				this.GlobalReferences.RemoveInvalidKeys();
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.refId.Clear();
				this.idRef.Clear();
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001C310 File Offset: 0x0001A510
		public bool Contains(UnityEngine.Object obj)
		{
			return this.refId.ContainsKey(obj);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001C31E File Offset: 0x0001A51E
		public bool Contains(long referenceID)
		{
			return this.idRef.ContainsKey(referenceID);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001C32C File Offset: 0x0001A52C
		public void ChangeId(long oldId, long newId)
		{
			this.idRef.ChangeKey(oldId, newId);
			this.refId = null;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001C344 File Offset: 0x0001A544
		internal static long GetNewRefID()
		{
			if (ES3ReferenceMgrBase.rng == null)
			{
				ES3ReferenceMgrBase.rng = new System.Random();
			}
			byte[] array = new byte[8];
			ES3ReferenceMgrBase.rng.NextBytes(array);
			return Math.Abs(BitConverter.ToInt64(array, 0) % long.MaxValue);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001C38A File Offset: 0x0001A58A
		internal static bool CanBeSaved(UnityEngine.Object obj)
		{
			return true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001C38D File Offset: 0x0001A58D
		protected ES3ReferenceMgrBase()
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001C3C1 File Offset: 0x0001A5C1
		// Note: this type is marked as 'beforefieldinit'.
		static ES3ReferenceMgrBase()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001C3D3 File Offset: 0x0001A5D3
		[CompilerGenerated]
		private bool <RemoveNullOrInvalidValues>b__35_0(KeyValuePair<long, UnityEngine.Object> pair)
		{
			return pair.Value == null || !ES3ReferenceMgrBase.CanBeSaved(pair.Value) || this.excludeObjects.Contains(pair.Value);
		}

		// Token: 0x04000137 RID: 311
		internal object _lock = new object();

		// Token: 0x04000138 RID: 312
		public const string referencePropertyName = "_ES3Ref";

		// Token: 0x04000139 RID: 313
		private static ES3ReferenceMgrBase _current = null;

		// Token: 0x0400013A RID: 314
		private static HashSet<ES3ReferenceMgrBase> mgrs = new HashSet<ES3ReferenceMgrBase>();

		// Token: 0x0400013B RID: 315
		[NonSerialized]
		public List<UnityEngine.Object> excludeObjects = new List<UnityEngine.Object>();

		// Token: 0x0400013C RID: 316
		private static System.Random rng;

		// Token: 0x0400013D RID: 317
		[HideInInspector]
		public bool openPrefabs;

		// Token: 0x0400013E RID: 318
		public List<ES3Prefab> prefabs = new List<ES3Prefab>();

		// Token: 0x0400013F RID: 319
		[SerializeField]
		public ES3IdRefDictionary idRef = new ES3IdRefDictionary();

		// Token: 0x04000140 RID: 320
		private ES3RefIdDictionary _refId;

		// Token: 0x02000109 RID: 265
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x060005AA RID: 1450 RVA: 0x000205B7 File Offset: 0x0001E7B7
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x000205BF File Offset: 0x0001E7BF
			internal bool <Remove>b__0(KeyValuePair<long, UnityEngine.Object> kvp)
			{
				return kvp.Value == this.obj;
			}

			// Token: 0x04000202 RID: 514
			public UnityEngine.Object obj;

			// Token: 0x04000203 RID: 515
			public Func<KeyValuePair<long, UnityEngine.Object>, bool> <>9__0;
		}

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x060005AC RID: 1452 RVA: 0x000205D3 File Offset: 0x0001E7D3
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x000205DB File Offset: 0x0001E7DB
			internal bool <Remove>b__0(KeyValuePair<UnityEngine.Object, long> kvp)
			{
				return kvp.Value == this.referenceID;
			}

			// Token: 0x04000204 RID: 516
			public long referenceID;

			// Token: 0x04000205 RID: 517
			public Func<KeyValuePair<UnityEngine.Object, long>, bool> <>9__0;
		}

		// Token: 0x0200010B RID: 267
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005AE RID: 1454 RVA: 0x000205EC File Offset: 0x0001E7EC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x000205F8 File Offset: 0x0001E7F8
			public <>c()
			{
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x00020600 File Offset: 0x0001E800
			internal bool <RemoveNullValues>b__34_0(KeyValuePair<long, UnityEngine.Object> pair)
			{
				return pair.Value == null;
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0002060F File Offset: 0x0001E80F
			internal long <RemoveNullValues>b__34_1(KeyValuePair<long, UnityEngine.Object> pair)
			{
				return pair.Key;
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x00020618 File Offset: 0x0001E818
			internal long <RemoveNullOrInvalidValues>b__35_1(KeyValuePair<long, UnityEngine.Object> pair)
			{
				return pair.Key;
			}

			// Token: 0x04000206 RID: 518
			public static readonly ES3ReferenceMgrBase.<>c <>9 = new ES3ReferenceMgrBase.<>c();

			// Token: 0x04000207 RID: 519
			public static Func<KeyValuePair<long, UnityEngine.Object>, bool> <>9__34_0;

			// Token: 0x04000208 RID: 520
			public static Func<KeyValuePair<long, UnityEngine.Object>, long> <>9__34_1;

			// Token: 0x04000209 RID: 521
			public static Func<KeyValuePair<long, UnityEngine.Object>, long> <>9__35_1;
		}
	}
}
