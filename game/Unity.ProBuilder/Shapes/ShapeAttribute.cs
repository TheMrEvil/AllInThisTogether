using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.Class)]
	public class ShapeAttribute : Attribute
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x000272C9 File Offset: 0x000254C9
		public ShapeAttribute(string n)
		{
			this.name = n;
		}

		// Token: 0x0400024C RID: 588
		public string name;
	}
}
