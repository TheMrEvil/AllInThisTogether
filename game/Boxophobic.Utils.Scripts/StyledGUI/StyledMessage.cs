using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x0200000A RID: 10
	public class StyledMessage : PropertyAttribute
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023DD File Offset: 0x000005DD
		public StyledMessage(string Type, string Message)
		{
			this.Type = Type;
			this.Message = Message;
			this.Top = 0f;
			this.Down = 0f;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002409 File Offset: 0x00000609
		public StyledMessage(string Type, string Message, float Top, float Down)
		{
			this.Type = Type;
			this.Message = Message;
			this.Top = Top;
			this.Down = Down;
		}

		// Token: 0x04000019 RID: 25
		public string Type;

		// Token: 0x0400001A RID: 26
		public string Message;

		// Token: 0x0400001B RID: 27
		public float Top;

		// Token: 0x0400001C RID: 28
		public float Down;
	}
}
