using System;

namespace Mono
{
	// Token: 0x0200004F RID: 79
	internal static class RuntimeStructs
	{
		// Token: 0x02000050 RID: 80
		internal struct RemoteClass
		{
			// Token: 0x04000DE5 RID: 3557
			internal IntPtr default_vtable;

			// Token: 0x04000DE6 RID: 3558
			internal IntPtr xdomain_vtable;

			// Token: 0x04000DE7 RID: 3559
			internal unsafe RuntimeStructs.MonoClass* proxy_class;

			// Token: 0x04000DE8 RID: 3560
			internal IntPtr proxy_class_name;

			// Token: 0x04000DE9 RID: 3561
			internal uint interface_count;
		}

		// Token: 0x02000051 RID: 81
		internal struct MonoClass
		{
		}

		// Token: 0x02000052 RID: 82
		internal struct GenericParamInfo
		{
			// Token: 0x04000DEA RID: 3562
			internal unsafe RuntimeStructs.MonoClass* pklass;

			// Token: 0x04000DEB RID: 3563
			internal IntPtr name;

			// Token: 0x04000DEC RID: 3564
			internal ushort flags;

			// Token: 0x04000DED RID: 3565
			internal uint token;

			// Token: 0x04000DEE RID: 3566
			internal unsafe RuntimeStructs.MonoClass** constraints;
		}

		// Token: 0x02000053 RID: 83
		internal struct GPtrArray
		{
			// Token: 0x04000DEF RID: 3567
			internal unsafe IntPtr* data;

			// Token: 0x04000DF0 RID: 3568
			internal int len;
		}
	}
}
