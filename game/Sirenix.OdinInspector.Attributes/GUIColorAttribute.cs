using System;
using System.Diagnostics;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class GUIColorAttribute : Attribute
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000027F6 File Offset: 0x000009F6
		public GUIColorAttribute(float r, float g, float b, float a = 1f)
		{
			this.Color = new Color(r, g, b, a);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000280E File Offset: 0x00000A0E
		public GUIColorAttribute(string getColor)
		{
			this.GetColor = getColor;
		}

		// Token: 0x0400005F RID: 95
		public Color Color;

		// Token: 0x04000060 RID: 96
		public string GetColor;
	}
}
