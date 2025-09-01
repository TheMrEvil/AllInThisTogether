using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004C RID: 76
	public class AxisEventData : BaseEventData
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00017D27 File Offset: 0x00015F27
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00017D2F File Offset: 0x00015F2F
		public Vector2 moveVector
		{
			[CompilerGenerated]
			get
			{
				return this.<moveVector>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<moveVector>k__BackingField = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00017D38 File Offset: 0x00015F38
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00017D40 File Offset: 0x00015F40
		public MoveDirection moveDir
		{
			[CompilerGenerated]
			get
			{
				return this.<moveDir>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<moveDir>k__BackingField = value;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017D49 File Offset: 0x00015F49
		public AxisEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}

		// Token: 0x040001AB RID: 427
		[CompilerGenerated]
		private Vector2 <moveVector>k__BackingField;

		// Token: 0x040001AC RID: 428
		[CompilerGenerated]
		private MoveDirection <moveDir>k__BackingField;
	}
}
