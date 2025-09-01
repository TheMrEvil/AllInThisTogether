using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000622 RID: 1570
	internal interface ISerializationRootObject
	{
		// Token: 0x06003B14 RID: 15124
		void RootSetObjectData(SerializationInfo info, StreamingContext context);
	}
}
