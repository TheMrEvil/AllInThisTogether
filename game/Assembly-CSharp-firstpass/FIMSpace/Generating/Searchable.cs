using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.Generating
{
	// Token: 0x02000063 RID: 99
	public static class Searchable
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00019183 File Offset: 0x00017383
		public static void Choose(object value)
		{
			Searchable.IsSetted = true;
			Searchable.choosed = value;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00019194 File Offset: 0x00017394
		public static T Get<T>(bool extensiveTypeMatch = false) where T : UnityEngine.Object
		{
			if (Searchable.choosed == null)
			{
				Searchable.IsSetted = false;
				return default(T);
			}
			bool flag;
			if (extensiveTypeMatch)
			{
				if (Searchable.CheckSubType)
				{
					flag = Searchable.choosed.GetType().IsSubclassOf(typeof(T));
				}
				else
				{
					flag = (Searchable.choosed.GetType() == typeof(T));
				}
			}
			else
			{
				flag = (typeof(T) == Searchable.choosed.GetType());
			}
			if (flag)
			{
				Searchable.IsSetted = false;
				T result = (T)((object)Searchable.choosed);
				Searchable.choosed = null;
				return result;
			}
			return default(T);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00019237 File Offset: 0x00017437
		public static object Get()
		{
			if (Searchable.IsSetted)
			{
				Searchable.IsSetted = false;
				object result = Searchable.choosed;
				Searchable.choosed = null;
				return result;
			}
			return null;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00019253 File Offset: 0x00017453
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0001925A File Offset: 0x0001745A
		public static bool IsSetted
		{
			[CompilerGenerated]
			get
			{
				return Searchable.<IsSetted>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Searchable.<IsSetted>k__BackingField = value;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00019262 File Offset: 0x00017462
		// Note: this type is marked as 'beforefieldinit'.
		static Searchable()
		{
		}

		// Token: 0x04000308 RID: 776
		public static bool CheckSubType = true;

		// Token: 0x04000309 RID: 777
		[CompilerGenerated]
		private static bool <IsSetted>k__BackingField;

		// Token: 0x0400030A RID: 778
		private static object choosed = null;
	}
}
