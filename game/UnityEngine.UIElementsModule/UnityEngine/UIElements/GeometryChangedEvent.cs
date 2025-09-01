using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F1 RID: 497
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003FD70 File Offset: 0x0003DF70
		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent pooled = EventBase<GeometryChangedEvent>.GetPooled();
			pooled.oldRect = oldRect;
			pooled.newRect = newRect;
			return pooled;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003FD99 File Offset: 0x0003DF99
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003FDAA File Offset: 0x0003DFAA
		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003FDCD File Offset: 0x0003DFCD
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0003FDD5 File Offset: 0x0003DFD5
		public Rect oldRect
		{
			[CompilerGenerated]
			get
			{
				return this.<oldRect>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<oldRect>k__BackingField = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0003FDDE File Offset: 0x0003DFDE
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0003FDE6 File Offset: 0x0003DFE6
		public Rect newRect
		{
			[CompilerGenerated]
			get
			{
				return this.<newRect>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<newRect>k__BackingField = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0003FDEF File Offset: 0x0003DFEF
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0003FDF7 File Offset: 0x0003DFF7
		internal int layoutPass
		{
			[CompilerGenerated]
			get
			{
				return this.<layoutPass>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<layoutPass>k__BackingField = value;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003FE00 File Offset: 0x0003E000
		public GeometryChangedEvent()
		{
			this.LocalInit();
		}

		// Token: 0x04000719 RID: 1817
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Rect <oldRect>k__BackingField;

		// Token: 0x0400071A RID: 1818
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Rect <newRect>k__BackingField;

		// Token: 0x0400071B RID: 1819
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <layoutPass>k__BackingField;
	}
}
