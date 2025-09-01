using System;

namespace XInputDotNetPure
{
	// Token: 0x02000007 RID: 7
	public struct GamePadTriggers
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002195 File Offset: 0x00000395
		internal GamePadTriggers(float left, float right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021A5 File Offset: 0x000003A5
		public float Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021AD File Offset: 0x000003AD
		public float Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000014 RID: 20
		private float left;

		// Token: 0x04000015 RID: 21
		private float right;
	}
}
