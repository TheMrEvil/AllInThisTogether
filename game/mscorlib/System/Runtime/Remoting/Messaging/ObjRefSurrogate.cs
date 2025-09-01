using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000633 RID: 1587
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06003BE6 RID: 15334 RVA: 0x000D097E File Offset: 0x000CEB7E
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000D09A5 File Offset: 0x000CEBA5
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x0000259F File Offset: 0x0000079F
		public ObjRefSurrogate()
		{
		}
	}
}
