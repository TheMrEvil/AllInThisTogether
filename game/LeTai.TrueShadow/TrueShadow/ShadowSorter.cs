using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x02000010 RID: 16
	[ExecuteAlways]
	public class ShadowSorter : MonoBehaviour
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public static ShadowSorter Instance
		{
			get
			{
				if (!ShadowSorter.instance)
				{
					ShadowSorter[] array = Shims.FindObjectsOfType<ShadowSorter>(false);
					for (int i = array.Length - 1; i > 0; i--)
					{
						UnityEngine.Object.Destroy(array[i]);
					}
					ShadowSorter.instance = ((array.Length != 0) ? array[0] : null);
					if (!ShadowSorter.instance)
					{
						ShadowSorter.instance = new GameObject("ShadowSorter")
						{
							hideFlags = HideFlags.HideAndDontSave
						}.AddComponent<ShadowSorter>();
					}
				}
				return ShadowSorter.instance;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004B24 File Offset: 0x00002D24
		public void Register(TrueShadow shadow)
		{
			this.shadows.AddUnique(shadow);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B33 File Offset: 0x00002D33
		public void UnRegister(TrueShadow shadow)
		{
			this.shadows.Remove(shadow);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B44 File Offset: 0x00002D44
		private void LateUpdate()
		{
			if (!this)
			{
				return;
			}
			for (int i = 0; i < this.shadows.Count; i++)
			{
				TrueShadow trueShadow = this.shadows[i];
				if (trueShadow && trueShadow.isActiveAndEnabled)
				{
					trueShadow.CheckHierarchyDirtied();
					if (trueShadow.HierachyDirty)
					{
						this.AddSortEntry(trueShadow);
					}
				}
			}
			this.Sort();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004BA8 File Offset: 0x00002DA8
		private void AddSortEntry(TrueShadow shadow)
		{
			ShadowSorter.SortEntry sortEntry = new ShadowSorter.SortEntry(shadow);
			ShadowSorter.SortGroup item = new ShadowSorter.SortGroup(sortEntry);
			int num = this.sortGroups.IndexOf(item);
			if (num > -1)
			{
				this.sortGroups[num].Add(sortEntry);
				return;
			}
			this.sortGroups.Add(item);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public void Sort()
		{
			for (int i = 0; i < this.sortGroups.Count; i++)
			{
				ShadowSorter.SortGroup sortGroup = this.sortGroups[i];
				if (sortGroup.parentTransform)
				{
					foreach (ShadowSorter.SortEntry sortEntry in sortGroup.sortEntries)
					{
						sortEntry.rendererTransform.SetParent(sortGroup.parentTransform, false);
						int siblingIndex = sortEntry.rendererTransform.GetSiblingIndex();
						int siblingIndex2 = sortEntry.shadowTransform.GetSiblingIndex();
						if (siblingIndex > siblingIndex2)
						{
							sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2);
						}
						else
						{
							sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2 - 1);
						}
						sortEntry.shadow.UnSetHierachyDirty();
					}
					foreach (ShadowSorter.SortEntry sortEntry2 in sortGroup.sortEntries)
					{
						sortEntry2.shadow.ForgetSiblingIndexChanges();
					}
				}
			}
			this.sortGroups.Clear();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004D28 File Offset: 0x00002F28
		private void OnApplicationQuit()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004D35 File Offset: 0x00002F35
		public ShadowSorter()
		{
		}

		// Token: 0x04000066 RID: 102
		private static ShadowSorter instance;

		// Token: 0x04000067 RID: 103
		private readonly IndexedSet<TrueShadow> shadows = new IndexedSet<TrueShadow>();

		// Token: 0x04000068 RID: 104
		private readonly IndexedSet<ShadowSorter.SortGroup> sortGroups = new IndexedSet<ShadowSorter.SortGroup>();

		// Token: 0x0200002E RID: 46
		private readonly struct SortEntry : IComparable<ShadowSorter.SortEntry>
		{
			// Token: 0x0600014A RID: 330 RVA: 0x00006D29 File Offset: 0x00004F29
			public SortEntry(TrueShadow shadow)
			{
				this.shadow = shadow;
				this.shadowTransform = shadow.transform;
				this.rendererTransform = shadow.shadowRenderer.transform;
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00006D50 File Offset: 0x00004F50
			public int CompareTo(ShadowSorter.SortEntry other)
			{
				return other.shadowTransform.GetSiblingIndex().CompareTo(this.shadowTransform.GetSiblingIndex());
			}

			// Token: 0x040000CC RID: 204
			public readonly TrueShadow shadow;

			// Token: 0x040000CD RID: 205
			public readonly Transform shadowTransform;

			// Token: 0x040000CE RID: 206
			public readonly Transform rendererTransform;
		}

		// Token: 0x0200002F RID: 47
		private readonly struct SortGroup
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00006D7B File Offset: 0x00004F7B
			public SortGroup(ShadowSorter.SortEntry firstEntry)
			{
				this.sortEntries = new List<ShadowSorter.SortEntry>
				{
					firstEntry
				};
				this.parentTransform = firstEntry.shadowTransform.parent;
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00006DA0 File Offset: 0x00004FA0
			public void Add(ShadowSorter.SortEntry pair)
			{
				if (pair.shadowTransform.parent != this.parentTransform)
				{
					return;
				}
				int num = this.sortEntries.BinarySearch(pair);
				if (num < 0)
				{
					this.sortEntries.Insert(~num, pair);
				}
			}

			// Token: 0x0600014E RID: 334 RVA: 0x00006DE5 File Offset: 0x00004FE5
			public override int GetHashCode()
			{
				return this.parentTransform.GetHashCode();
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00006DF4 File Offset: 0x00004FF4
			public override bool Equals(object obj)
			{
				if (obj is ShadowSorter.SortGroup)
				{
					ShadowSorter.SortGroup sortGroup = (ShadowSorter.SortGroup)obj;
					return sortGroup.parentTransform == this.parentTransform;
				}
				return false;
			}

			// Token: 0x040000CF RID: 207
			public readonly Transform parentTransform;

			// Token: 0x040000D0 RID: 208
			public readonly List<ShadowSorter.SortEntry> sortEntries;
		}
	}
}
