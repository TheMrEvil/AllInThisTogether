using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E3 RID: 227
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ObjectParameter<T> : VolumeParameter<T>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001E072 File Offset: 0x0001C272
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001E07A File Offset: 0x0001C27A
		internal ReadOnlyCollection<VolumeParameter> parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<parameters>k__BackingField = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001E083 File Offset: 0x0001C283
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001E086 File Offset: 0x0001C286
		public sealed override bool overrideState
		{
			get
			{
				return true;
			}
			set
			{
				this.m_OverrideState = true;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001E08F File Offset: 0x0001C28F
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001E098 File Offset: 0x0001C298
		public sealed override T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
				if (this.m_Value == null)
				{
					this.parameters = null;
					return;
				}
				this.parameters = (from t in this.m_Value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
				where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
				orderby t.MetadataToken
				select (VolumeParameter)t.GetValue(this.m_Value)).ToList<VolumeParameter>().AsReadOnly();
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E142 File Offset: 0x0001C342
		public ObjectParameter(T value)
		{
			this.m_OverrideState = true;
			this.value = value;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001E158 File Offset: 0x0001C358
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			if (this.m_Value == null)
			{
				return;
			}
			ReadOnlyCollection<VolumeParameter> parameters = this.parameters;
			ReadOnlyCollection<VolumeParameter> parameters2 = ((ObjectParameter<T>)from).parameters;
			ReadOnlyCollection<VolumeParameter> parameters3 = ((ObjectParameter<T>)to).parameters;
			for (int i = 0; i < parameters2.Count; i++)
			{
				parameters[i].overrideState = parameters3[i].overrideState;
				if (parameters3[i].overrideState)
				{
					parameters[i].Interp(parameters2[i], parameters3[i], t);
				}
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		[CompilerGenerated]
		private VolumeParameter <set_value>b__9_2(FieldInfo t)
		{
			return (VolumeParameter)t.GetValue(this.m_Value);
		}

		// Token: 0x040003C6 RID: 966
		[CompilerGenerated]
		private ReadOnlyCollection<VolumeParameter> <parameters>k__BackingField;

		// Token: 0x02000181 RID: 385
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000920 RID: 2336 RVA: 0x00024A55 File Offset: 0x00022C55
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x00024A61 File Offset: 0x00022C61
			public <>c()
			{
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x00024A69 File Offset: 0x00022C69
			internal bool <set_value>b__9_0(FieldInfo t)
			{
				return t.FieldType.IsSubclassOf(typeof(VolumeParameter));
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x00024A80 File Offset: 0x00022C80
			internal int <set_value>b__9_1(FieldInfo t)
			{
				return t.MetadataToken;
			}

			// Token: 0x040005C7 RID: 1479
			public static readonly ObjectParameter<T>.<>c <>9 = new ObjectParameter<T>.<>c();

			// Token: 0x040005C8 RID: 1480
			public static Func<FieldInfo, bool> <>9__9_0;

			// Token: 0x040005C9 RID: 1481
			public static Func<FieldInfo, int> <>9__9_1;
		}
	}
}
