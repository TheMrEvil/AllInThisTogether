using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000209 RID: 521
	public class NavigationMoveEvent : NavigationEventBase<NavigationMoveEvent>
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x00041728 File Offset: 0x0003F928
		internal static NavigationMoveEvent.Direction DetermineMoveDirection(float x, float y, float deadZone = 0.6f)
		{
			bool flag = new Vector2(x, y).sqrMagnitude < deadZone * deadZone;
			NavigationMoveEvent.Direction result;
			if (flag)
			{
				result = NavigationMoveEvent.Direction.None;
			}
			else
			{
				bool flag2 = Mathf.Abs(x) > Mathf.Abs(y);
				if (flag2)
				{
					bool flag3 = x > 0f;
					if (flag3)
					{
						result = NavigationMoveEvent.Direction.Right;
					}
					else
					{
						result = NavigationMoveEvent.Direction.Left;
					}
				}
				else
				{
					bool flag4 = y > 0f;
					if (flag4)
					{
						result = NavigationMoveEvent.Direction.Up;
					}
					else
					{
						result = NavigationMoveEvent.Direction.Down;
					}
				}
			}
			return result;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00041793 File Offset: 0x0003F993
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0004179B File Offset: 0x0003F99B
		public NavigationMoveEvent.Direction direction
		{
			[CompilerGenerated]
			get
			{
				return this.<direction>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<direction>k__BackingField = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000417A4 File Offset: 0x0003F9A4
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x000417AC File Offset: 0x0003F9AC
		public Vector2 move
		{
			[CompilerGenerated]
			get
			{
				return this.<move>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<move>k__BackingField = value;
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000417B8 File Offset: 0x0003F9B8
		public static NavigationMoveEvent GetPooled(Vector2 moveVector)
		{
			NavigationMoveEvent pooled = EventBase<NavigationMoveEvent>.GetPooled();
			pooled.direction = NavigationMoveEvent.DetermineMoveDirection(moveVector.x, moveVector.y, 0.6f);
			pooled.move = moveVector;
			return pooled;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000417F8 File Offset: 0x0003F9F8
		internal static NavigationMoveEvent GetPooled(NavigationMoveEvent.Direction direction)
		{
			NavigationMoveEvent pooled = EventBase<NavigationMoveEvent>.GetPooled();
			pooled.direction = direction;
			pooled.move = Vector2.zero;
			return pooled;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00041825 File Offset: 0x0003FA25
		protected override void Init()
		{
			base.Init();
			this.direction = NavigationMoveEvent.Direction.None;
			this.move = Vector2.zero;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00041843 File Offset: 0x0003FA43
		public NavigationMoveEvent()
		{
			this.Init();
		}

		// Token: 0x0400072E RID: 1838
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private NavigationMoveEvent.Direction <direction>k__BackingField;

		// Token: 0x0400072F RID: 1839
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <move>k__BackingField;

		// Token: 0x0200020A RID: 522
		public enum Direction
		{
			// Token: 0x04000731 RID: 1841
			None,
			// Token: 0x04000732 RID: 1842
			Left,
			// Token: 0x04000733 RID: 1843
			Up,
			// Token: 0x04000734 RID: 1844
			Right,
			// Token: 0x04000735 RID: 1845
			Down
		}
	}
}
