using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EA RID: 234
	internal class VisualElementFocusChangeTarget : FocusChangeDirection
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0001AE9C File Offset: 0x0001909C
		public static VisualElementFocusChangeTarget GetPooled(Focusable target)
		{
			VisualElementFocusChangeTarget visualElementFocusChangeTarget = VisualElementFocusChangeTarget.Pool.Get();
			visualElementFocusChangeTarget.target = target;
			return visualElementFocusChangeTarget;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001AEC2 File Offset: 0x000190C2
		protected override void Dispose()
		{
			this.target = null;
			VisualElementFocusChangeTarget.Pool.Release(this);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001AED9 File Offset: 0x000190D9
		internal override void ApplyTo(FocusController focusController, Focusable f)
		{
			f.Focus();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001AEE3 File Offset: 0x000190E3
		public VisualElementFocusChangeTarget() : base(FocusChangeDirection.unspecified)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001AEF7 File Offset: 0x000190F7
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001AEFF File Offset: 0x000190FF
		public Focusable target
		{
			[CompilerGenerated]
			get
			{
				return this.<target>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<target>k__BackingField = value;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001AF08 File Offset: 0x00019108
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElementFocusChangeTarget()
		{
		}

		// Token: 0x040002FA RID: 762
		private static readonly ObjectPool<VisualElementFocusChangeTarget> Pool = new ObjectPool<VisualElementFocusChangeTarget>(100);

		// Token: 0x040002FB RID: 763
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Focusable <target>k__BackingField;
	}
}
