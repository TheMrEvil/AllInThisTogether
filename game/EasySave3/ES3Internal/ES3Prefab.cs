using System;
using System.Collections.Generic;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D6 RID: 214
	public class ES3Prefab : MonoBehaviour
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0001B4A0 File Offset: 0x000196A0
		public void Awake()
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return;
			}
			foreach (KeyValuePair<UnityEngine.Object, long> keyValuePair in this.localRefs)
			{
				if (keyValuePair.Key != null)
				{
					es3ReferenceMgrBase.Add(keyValuePair.Key);
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001B51C File Offset: 0x0001971C
		public long Get(UnityEngine.Object obj)
		{
			long result;
			if (this.localRefs.TryGetValue(obj, out result))
			{
				return result;
			}
			return -1L;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001B540 File Offset: 0x00019740
		public long Add(UnityEngine.Object obj)
		{
			long newRefID;
			if (this.localRefs.TryGetValue(obj, out newRefID))
			{
				return newRefID;
			}
			if (!ES3ReferenceMgrBase.CanBeSaved(obj))
			{
				return -1L;
			}
			newRefID = ES3Prefab.GetNewRefID();
			this.localRefs.Add(obj, newRefID);
			return newRefID;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001B580 File Offset: 0x00019780
		public Dictionary<string, string> GetReferences()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return dictionary;
			}
			foreach (KeyValuePair<UnityEngine.Object, long> keyValuePair in this.localRefs)
			{
				long num = es3ReferenceMgrBase.Add(keyValuePair.Key);
				dictionary[keyValuePair.Value.ToString()] = num.ToString();
			}
			return dictionary;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001B610 File Offset: 0x00019810
		public void ApplyReferences(Dictionary<long, long> localToGlobal)
		{
			if (ES3ReferenceMgrBase.Current == null)
			{
				return;
			}
			foreach (KeyValuePair<UnityEngine.Object, long> keyValuePair in this.localRefs)
			{
				long id;
				if (localToGlobal.TryGetValue(keyValuePair.Value, out id))
				{
					ES3ReferenceMgrBase.Current.Add(keyValuePair.Key, id);
				}
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001B690 File Offset: 0x00019890
		public static long GetNewRefID()
		{
			return ES3ReferenceMgrBase.GetNewRefID();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B697 File Offset: 0x00019897
		public ES3Prefab()
		{
		}

		// Token: 0x04000135 RID: 309
		public long prefabId = ES3Prefab.GetNewRefID();

		// Token: 0x04000136 RID: 310
		public ES3RefIdDictionary localRefs = new ES3RefIdDictionary();
	}
}
