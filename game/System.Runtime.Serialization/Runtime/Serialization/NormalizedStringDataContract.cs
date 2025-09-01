using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000110 RID: 272
	internal class NormalizedStringDataContract : StringDataContract
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00037508 File Offset: 0x00035708
		internal NormalizedStringDataContract() : base(DictionaryGlobals.normalizedStringLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}
	}
}
