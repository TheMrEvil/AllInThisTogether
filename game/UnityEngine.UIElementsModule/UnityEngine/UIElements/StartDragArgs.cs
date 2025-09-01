using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B0 RID: 432
	internal struct StartDragArgs
	{
		// Token: 0x06000E2B RID: 3627 RVA: 0x0003ADA3 File Offset: 0x00038FA3
		public StartDragArgs(string title, DragVisualMode visualMode)
		{
			this.title = title;
			this.visualMode = visualMode;
			this.genericData = null;
			this.unityObjectReferences = null;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003ADC4 File Offset: 0x00038FC4
		internal StartDragArgs(string title, object target)
		{
			this.title = title;
			this.visualMode = 2;
			this.genericData = null;
			this.unityObjectReferences = null;
			this.SetGenericData("__unity-drag-and-drop__source-view", target);
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0003ADF2 File Offset: 0x00038FF2
		public readonly string title
		{
			[CompilerGenerated]
			get
			{
				return this.<title>k__BackingField;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0003ADFA File Offset: 0x00038FFA
		public readonly DragVisualMode visualMode
		{
			[CompilerGenerated]
			get
			{
				return this.<visualMode>k__BackingField;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0003AE02 File Offset: 0x00039002
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0003AE0A File Offset: 0x0003900A
		internal Hashtable genericData
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<genericData>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<genericData>k__BackingField = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003AE13 File Offset: 0x00039013
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0003AE1B File Offset: 0x0003901B
		internal IEnumerable<Object> unityObjectReferences
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<unityObjectReferences>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<unityObjectReferences>k__BackingField = value;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003AE24 File Offset: 0x00039024
		public void SetGenericData(string key, object data)
		{
			if (this.genericData == null)
			{
				this.genericData = new Hashtable();
			}
			this.genericData[key] = data;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003AE56 File Offset: 0x00039056
		public void SetUnityObjectReferences(IEnumerable<Object> references)
		{
			this.unityObjectReferences = references;
		}

		// Token: 0x04000693 RID: 1683
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <title>k__BackingField;

		// Token: 0x04000694 RID: 1684
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly DragVisualMode <visualMode>k__BackingField;

		// Token: 0x04000695 RID: 1685
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Hashtable <genericData>k__BackingField;

		// Token: 0x04000696 RID: 1686
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<Object> <unityObjectReferences>k__BackingField;
	}
}
