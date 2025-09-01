using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000396 RID: 918
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class UsesLuminPlatformLevelAttribute : Attribute
	{
		// Token: 0x06001EFD RID: 7933 RVA: 0x000327FB File Offset: 0x000309FB
		public UsesLuminPlatformLevelAttribute(uint platformLevel)
		{
			this.m_PlatformLevel = platformLevel;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0003280C File Offset: 0x00030A0C
		public uint platformLevel
		{
			get
			{
				return this.m_PlatformLevel;
			}
		}

		// Token: 0x04000A34 RID: 2612
		private readonly uint m_PlatformLevel;
	}
}
