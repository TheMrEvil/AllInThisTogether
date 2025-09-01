using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000022 RID: 34
	public struct HighlightState
	{
		// Token: 0x0600012F RID: 303 RVA: 0x000175D8 File Offset: 0x000157D8
		public HighlightState(Color32 color, TMP_Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000175E8 File Offset: 0x000157E8
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.Compare(rhs.color) && lhs.padding == rhs.padding;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00017610 File Offset: 0x00015810
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0001761C File Offset: 0x0001581C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0001762E File Offset: 0x0001582E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00017641 File Offset: 0x00015841
		public bool Equals(HighlightState other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000116 RID: 278
		public Color32 color;

		// Token: 0x04000117 RID: 279
		public TMP_Offset padding;
	}
}
