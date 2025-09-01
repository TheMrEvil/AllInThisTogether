using System;

namespace System.EnterpriseServices
{
	// Token: 0x0200002C RID: 44
	internal interface ISharedPropertyGroup
	{
		// Token: 0x0600008B RID: 139
		ISharedProperty CreateProperty(string name, out bool fExists);

		// Token: 0x0600008C RID: 140
		ISharedProperty CreatePropertyByPosition(int position, out bool fExists);

		// Token: 0x0600008D RID: 141
		ISharedProperty Property(string name);

		// Token: 0x0600008E RID: 142
		ISharedProperty PropertyByPosition(int position);
	}
}
