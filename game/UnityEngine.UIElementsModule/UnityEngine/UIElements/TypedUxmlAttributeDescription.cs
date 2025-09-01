using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D2 RID: 722
	public abstract class TypedUxmlAttributeDescription<T> : UxmlAttributeDescription
	{
		// Token: 0x06001850 RID: 6224
		public abstract T GetValueFromBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0006486F File Offset: 0x00062A6F
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x00064877 File Offset: 0x00062A77
		public T defaultValue
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<defaultValue>k__BackingField = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00064880 File Offset: 0x00062A80
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = this.defaultValue;
				return defaultValue.ToString();
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000648A6 File Offset: 0x00062AA6
		protected TypedUxmlAttributeDescription()
		{
		}

		// Token: 0x04000A7C RID: 2684
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T <defaultValue>k__BackingField;
	}
}
