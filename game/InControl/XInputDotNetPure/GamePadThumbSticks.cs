using System;
using UnityEngine;

namespace XInputDotNetPure
{
	// Token: 0x02000006 RID: 6
	public struct GamePadThumbSticks
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002175 File Offset: 0x00000375
		internal GamePadThumbSticks(GamePadThumbSticks.StickValue left, GamePadThumbSticks.StickValue right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002185 File Offset: 0x00000385
		public GamePadThumbSticks.StickValue Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000218D File Offset: 0x0000038D
		public GamePadThumbSticks.StickValue Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000012 RID: 18
		private GamePadThumbSticks.StickValue left;

		// Token: 0x04000013 RID: 19
		private GamePadThumbSticks.StickValue right;

		// Token: 0x0200020B RID: 523
		public struct StickValue
		{
			// Token: 0x06000906 RID: 2310 RVA: 0x00052BC3 File Offset: 0x00050DC3
			internal StickValue(float x, float y)
			{
				this.vector = new Vector2(x, y);
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000907 RID: 2311 RVA: 0x00052BD2 File Offset: 0x00050DD2
			public float X
			{
				get
				{
					return this.vector.x;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000908 RID: 2312 RVA: 0x00052BDF File Offset: 0x00050DDF
			public float Y
			{
				get
				{
					return this.vector.y;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x00052BEC File Offset: 0x00050DEC
			public Vector2 Vector
			{
				get
				{
					return this.vector;
				}
			}

			// Token: 0x04000437 RID: 1079
			private Vector2 vector;
		}
	}
}
