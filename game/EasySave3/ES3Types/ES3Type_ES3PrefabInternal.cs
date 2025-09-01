using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000019 RID: 25
	public class ES3Type_ES3PrefabInternal : ES3Type
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00006FF8 File Offset: 0x000051F8
		public ES3Type_ES3PrefabInternal() : base(typeof(ES3Type_ES3PrefabInternal))
		{
			ES3Type_ES3PrefabInternal.Instance = this;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007010 File Offset: 0x00005210
		public override void Write(object obj, ES3Writer writer)
		{
			ES3Prefab es3Prefab = (ES3Prefab)obj;
			writer.WriteProperty("prefabId", es3Prefab.prefabId.ToString(), ES3Type_string.Instance);
			writer.WriteProperty("refs", es3Prefab.GetReferences());
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007050 File Offset: 0x00005250
		public override object Read<T>(ES3Reader reader)
		{
			long id = reader.ReadRefProperty();
			if (ES3ReferenceMgrBase.Current == null)
			{
				return null;
			}
			ES3Prefab prefab = ES3ReferenceMgrBase.Current.GetPrefab(id, false);
			if (prefab == null)
			{
				throw new MissingReferenceException("Prefab with ID " + id.ToString() + " could not be found.\nPress the 'Refresh References' button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in the scene to refresh prefabs.");
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab.gameObject);
			ES3Prefab component = gameObject.GetComponent<ES3Prefab>();
			if (component == null)
			{
				throw new MissingReferenceException("Prefab with ID " + id.ToString() + " was found, but it does not have an ES3Prefab component attached.");
			}
			this.ReadInto<T>(reader, gameObject);
			return component.gameObject;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000070E8 File Offset: 0x000052E8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Dictionary<ES3Ref, ES3Ref> dictionary = reader.ReadProperty<Dictionary<ES3Ref, ES3Ref>>(ES3Type_ES3RefDictionary.Instance);
			Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
			foreach (KeyValuePair<ES3Ref, ES3Ref> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key.id, keyValuePair.Value.id);
			}
			if (ES3ReferenceMgrBase.Current == null)
			{
				return;
			}
			((GameObject)obj).GetComponent<ES3Prefab>().ApplyReferences(dictionary2);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000717C File Offset: 0x0000537C
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Type_ES3PrefabInternal()
		{
		}

		// Token: 0x0400005B RID: 91
		public static ES3Type Instance = new ES3Type_ES3PrefabInternal();
	}
}
