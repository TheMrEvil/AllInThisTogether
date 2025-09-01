using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200011F RID: 287
	internal class XsDurationDataContract : TimeSpanDataContract
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00037755 File Offset: 0x00035955
		internal XsDurationDataContract() : base(DictionaryGlobals.TimeSpanLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}
	}
}
