using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DF RID: 223
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00005E9C File Offset: 0x0000409C
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00005EB4 File Offset: 0x000040B4
		public string name
		{
			get
			{
				return SortingLayer.IDToName(this.m_Id);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00005ED4 File Offset: 0x000040D4
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00005EF4 File Offset: 0x000040F4
		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		// Token: 0x06000381 RID: 897
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] GetSortingLayerIDsInternal();

		// Token: 0x06000382 RID: 898
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x06000383 RID: 899
		[FreeFunction("GetTagManager().GetSortingLayerValueFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromName(string name);

		// Token: 0x06000384 RID: 900
		[FreeFunction("GetTagManager().GetSortingLayerUniqueIDFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToID(string name);

		// Token: 0x06000385 RID: 901
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string IDToName(int id);

		// Token: 0x06000386 RID: 902
		[FreeFunction("GetTagManager().IsSortingLayerUniqueIDValid")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValid(int id);

		// Token: 0x040002E2 RID: 738
		private int m_Id;
	}
}
