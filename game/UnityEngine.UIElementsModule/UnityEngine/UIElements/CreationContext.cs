using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002FF RID: 767
	public struct CreationContext : IEquatable<CreationContext>
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00067AED File Offset: 0x00065CED
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x00067AF5 File Offset: 0x00065CF5
		public VisualElement target
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<target>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<target>k__BackingField = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00067AFE File Offset: 0x00065CFE
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x00067B06 File Offset: 0x00065D06
		public VisualTreeAsset visualTreeAsset
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<visualTreeAsset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<visualTreeAsset>k__BackingField = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00067B0F File Offset: 0x00065D0F
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x00067B17 File Offset: 0x00065D17
		public Dictionary<string, VisualElement> slotInsertionPoints
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<slotInsertionPoints>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<slotInsertionPoints>k__BackingField = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x00067B20 File Offset: 0x00065D20
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x00067B28 File Offset: 0x00065D28
		internal List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<attributeOverrides>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<attributeOverrides>k__BackingField = value;
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00067B31 File Offset: 0x00065D31
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, null, vta, target);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00067B3F File Offset: 0x00065D3F
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides, VisualTreeAsset vta, VisualElement target)
		{
			this.target = target;
			this.slotInsertionPoints = slotInsertionPoints;
			this.attributeOverrides = attributeOverrides;
			this.visualTreeAsset = vta;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00067B64 File Offset: 0x00065D64
		public override bool Equals(object obj)
		{
			return obj is CreationContext && this.Equals((CreationContext)obj);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00067B90 File Offset: 0x00065D90
		public bool Equals(CreationContext other)
		{
			return EqualityComparer<VisualElement>.Default.Equals(this.target, other.target) && EqualityComparer<VisualTreeAsset>.Default.Equals(this.visualTreeAsset, other.visualTreeAsset) && EqualityComparer<Dictionary<string, VisualElement>>.Default.Equals(this.slotInsertionPoints, other.slotInsertionPoints);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00067BF0 File Offset: 0x00065DF0
		public override int GetHashCode()
		{
			int num = -2123482148;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.target);
			num = num * -1521134295 + EqualityComparer<VisualTreeAsset>.Default.GetHashCode(this.visualTreeAsset);
			return num * -1521134295 + EqualityComparer<Dictionary<string, VisualElement>>.Default.GetHashCode(this.slotInsertionPoints);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00067C54 File Offset: 0x00065E54
		public static bool operator ==(CreationContext context1, CreationContext context2)
		{
			return context1.Equals(context2);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00067C70 File Offset: 0x00065E70
		public static bool operator !=(CreationContext context1, CreationContext context2)
		{
			return !(context1 == context2);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00067C8C File Offset: 0x00065E8C
		// Note: this type is marked as 'beforefieldinit'.
		static CreationContext()
		{
		}

		// Token: 0x04000AF0 RID: 2800
		public static readonly CreationContext Default = default(CreationContext);

		// Token: 0x04000AF1 RID: 2801
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <target>k__BackingField;

		// Token: 0x04000AF2 RID: 2802
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualTreeAsset <visualTreeAsset>k__BackingField;

		// Token: 0x04000AF3 RID: 2803
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, VisualElement> <slotInsertionPoints>k__BackingField;

		// Token: 0x04000AF4 RID: 2804
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<TemplateAsset.AttributeOverride> <attributeOverrides>k__BackingField;
	}
}
