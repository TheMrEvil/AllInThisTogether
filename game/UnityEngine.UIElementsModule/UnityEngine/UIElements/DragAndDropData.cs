using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AD RID: 429
	internal abstract class DragAndDropData : IDragAndDropData
	{
		// Token: 0x06000E1E RID: 3614
		public abstract object GetGenericData(string key);

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003A537 File Offset: 0x00038737
		object IDragAndDropData.userData
		{
			get
			{
				return this.GetGenericData("__unity-drag-and-drop__source-view");
			}
		}

		// Token: 0x06000E20 RID: 3616
		public abstract void SetGenericData(string key, object data);

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E21 RID: 3617
		public abstract object source { get; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E22 RID: 3618
		public abstract DragVisualMode visualMode { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E23 RID: 3619
		public abstract IEnumerable<Object> unityObjectReferences { get; }

		// Token: 0x06000E24 RID: 3620 RVA: 0x000020C2 File Offset: 0x000002C2
		protected DragAndDropData()
		{
		}

		// Token: 0x0400068D RID: 1677
		internal const string dragSourceKey = "__unity-drag-and-drop__source-view";
	}
}
