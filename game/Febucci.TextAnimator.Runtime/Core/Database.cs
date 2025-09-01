using System;
using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class Database<T> : ScriptableObject where T : ScriptableObject, ITagProvider
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000693D File Offset: 0x00004B3D
		private void OnEnable()
		{
			this.built = false;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006946 File Offset: 0x00004B46
		public List<T> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006950 File Offset: 0x00004B50
		public void Add(T element)
		{
			if (this.data == null)
			{
				this.data = new List<T>();
			}
			this.data.Add(element);
			if (!this.built || !Application.isPlaying)
			{
				this.built = false;
				return;
			}
			string tagID = element.TagID;
			if (this.dictionary.ContainsKey(tagID))
			{
				Debug.LogError("Text Animator: Tag " + tagID + " is already present in the database. Skipping...");
				return;
			}
			this.dictionary.Add(tagID, element);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000069D0 File Offset: 0x00004BD0
		public void ForceBuildRefresh()
		{
			this.built = false;
			this.BuildOnce();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000069E0 File Offset: 0x00004BE0
		public void BuildOnce()
		{
			if (this.built)
			{
				return;
			}
			this.built = true;
			if (this.dictionary == null)
			{
				this.dictionary = new Dictionary<string, T>();
			}
			else
			{
				this.dictionary.Clear();
			}
			foreach (T t in this.data)
			{
				if (t)
				{
					string tagID = t.TagID;
					if (string.IsNullOrEmpty(tagID))
					{
						Debug.LogError("Text Animator: Tag is null or empty. Skipping...");
					}
					else if (this.dictionary.ContainsKey(tagID))
					{
						Debug.LogError("Text Animator: Tag " + tagID + " is already present in the database. Skipping...");
					}
					else
					{
						this.dictionary.Add(tagID, t);
					}
				}
			}
			this.OnBuildOnce();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006AC4 File Offset: 0x00004CC4
		protected virtual void OnBuildOnce()
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006AC6 File Offset: 0x00004CC6
		public bool ContainsKey(string key)
		{
			this.BuildOnce();
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x17000030 RID: 48
		public T this[string key]
		{
			get
			{
				this.BuildOnce();
				return this.dictionary[key];
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public void DestroyImmediate(bool databaseOnly = false)
		{
			if (!databaseOnly)
			{
				foreach (T t in this.data)
				{
					UnityEngine.Object.DestroyImmediate(t);
				}
			}
			UnityEngine.Object.DestroyImmediate(this);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006B50 File Offset: 0x00004D50
		public Database()
		{
		}

		// Token: 0x040000EB RID: 235
		private bool built;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		private List<T> data = new List<T>();

		// Token: 0x040000ED RID: 237
		private Dictionary<string, T> dictionary;
	}
}
