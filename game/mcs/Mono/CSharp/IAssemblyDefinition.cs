using System;

namespace Mono.CSharp
{
	// Token: 0x020000FF RID: 255
	public interface IAssemblyDefinition
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000CCB RID: 3275
		string FullName { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000CCC RID: 3276
		bool IsCLSCompliant { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000CCD RID: 3277
		bool IsMissing { get; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000CCE RID: 3278
		string Name { get; }

		// Token: 0x06000CCF RID: 3279
		byte[] GetPublicKeyToken();

		// Token: 0x06000CD0 RID: 3280
		bool IsFriendAssemblyTo(IAssemblyDefinition assembly);
	}
}
