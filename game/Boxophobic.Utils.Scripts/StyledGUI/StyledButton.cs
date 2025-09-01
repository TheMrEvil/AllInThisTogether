using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000003 RID: 3
	public class StyledButton : PropertyAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002141 File Offset: 0x00000341
		public StyledButton(string Text)
		{
			this.Text = Text;
			this.Top = 0f;
			this.Down = 0f;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002171 File Offset: 0x00000371
		public StyledButton(string Text, float Top, float Down)
		{
			this.Text = Text;
			this.Top = Top;
			this.Down = Down;
		}

		// Token: 0x04000006 RID: 6
		public string Text = "";

		// Token: 0x04000007 RID: 7
		public float Top;

		// Token: 0x04000008 RID: 8
		public float Down;
	}
}
