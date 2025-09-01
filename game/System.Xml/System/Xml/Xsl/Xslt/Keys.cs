using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000408 RID: 1032
	internal class Keys : KeyedCollection<QilName, List<Key>>
	{
		// Token: 0x060028C5 RID: 10437 RVA: 0x000F51A0 File Offset: 0x000F33A0
		protected override QilName GetKeyForItem(List<Key> list)
		{
			return list[0].Name;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000F51AE File Offset: 0x000F33AE
		public Keys()
		{
		}
	}
}
