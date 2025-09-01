using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AF RID: 175
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ReloadAttribute : Attribute
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0001C02B File Offset: 0x0001A22B
		public ReloadAttribute(string[] paths, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001C033 File Offset: 0x0001A233
		public ReloadAttribute(string path, ReloadAttribute.Package package = ReloadAttribute.Package.Root) : this(new string[]
		{
			path
		}, package)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001C046 File Offset: 0x0001A246
		public ReloadAttribute(string pathFormat, int rangeMin, int rangeMax, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x0200017D RID: 381
		public enum Package
		{
			// Token: 0x040005BF RID: 1471
			Builtin,
			// Token: 0x040005C0 RID: 1472
			Root
		}
	}
}
