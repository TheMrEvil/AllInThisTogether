using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000693 RID: 1683
	internal interface IStreamable
	{
		// Token: 0x06003E10 RID: 15888
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x06003E11 RID: 15889
		void Write(__BinaryWriter sout);
	}
}
