using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200065D RID: 1629
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x06003CD7 RID: 15575 RVA: 0x000D2835 File Offset: 0x000D0A35
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000D2852 File Offset: 0x000D0A52
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000D2862 File Offset: 0x000D0A62
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x04002737 RID: 10039
		private ISerializationSurrogate innerSurrogate;
	}
}
