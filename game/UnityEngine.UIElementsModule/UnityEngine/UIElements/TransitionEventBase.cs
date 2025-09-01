using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022E RID: 558
	public abstract class TransitionEventBase<T> : EventBase<T>, ITransitionEvent where T : TransitionEventBase<T>, new()
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004355F File Offset: 0x0004175F
		public StylePropertyNameCollection stylePropertyNames
		{
			[CompilerGenerated]
			get
			{
				return this.<stylePropertyNames>k__BackingField;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00043567 File Offset: 0x00041767
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0004356F File Offset: 0x0004176F
		public double elapsedTime
		{
			[CompilerGenerated]
			get
			{
				return this.<elapsedTime>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<elapsedTime>k__BackingField = value;
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00043578 File Offset: 0x00041778
		protected TransitionEventBase()
		{
			this.stylePropertyNames = new StylePropertyNameCollection(new List<StylePropertyName>());
			this.LocalInit();
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00043599 File Offset: 0x00041799
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000435AA File Offset: 0x000417AA
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
			base.propagateToIMGUI = false;
			this.stylePropertyNames.propertiesList.Clear();
			this.elapsedTime = 0.0;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000435E0 File Offset: 0x000417E0
		public static T GetPooled(StylePropertyName stylePropertyName, double elapsedTime)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.stylePropertyNames.propertiesList.Add(stylePropertyName);
			pooled.elapsedTime = elapsedTime;
			return pooled;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00043620 File Offset: 0x00041820
		public bool AffectsProperty(StylePropertyName stylePropertyName)
		{
			return this.stylePropertyNames.Contains(stylePropertyName);
		}

		// Token: 0x04000775 RID: 1909
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly StylePropertyNameCollection <stylePropertyNames>k__BackingField;

		// Token: 0x04000776 RID: 1910
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private double <elapsedTime>k__BackingField;
	}
}
