using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003D4 RID: 980
	internal class RootLevel : StylesheetLevel
	{
		// Token: 0x06002743 RID: 10051 RVA: 0x000EA3AB File Offset: 0x000E85AB
		public RootLevel(Stylesheet principal)
		{
			this.Imports = new Stylesheet[]
			{
				principal
			};
		}
	}
}
