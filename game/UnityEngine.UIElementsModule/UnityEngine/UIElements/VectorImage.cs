using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class VectorImage : ScriptableObject
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0001AA8E File Offset: 0x00018C8E
		public VectorImage()
		{
		}

		// Token: 0x040002D8 RID: 728
		[SerializeField]
		internal Texture2D atlas = null;

		// Token: 0x040002D9 RID: 729
		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		// Token: 0x040002DA RID: 730
		[SerializeField]
		internal ushort[] indices = null;

		// Token: 0x040002DB RID: 731
		[SerializeField]
		internal GradientSettings[] settings = null;

		// Token: 0x040002DC RID: 732
		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
