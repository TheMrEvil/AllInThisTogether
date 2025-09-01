using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002A0 RID: 672
	internal interface INameScope
	{
		// Token: 0x170004DB RID: 1243
		object this[string name, string ns]
		{
			get;
			set;
		}
	}
}
