using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x02000272 RID: 626
	internal class CaseInsensitiveKeyComparer : CaseInsensitiveComparer, IEqualityComparer
	{
		// Token: 0x060017C9 RID: 6089 RVA: 0x0008B9DB File Offset: 0x00089BDB
		public CaseInsensitiveKeyComparer() : base(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0008B9E8 File Offset: 0x00089BE8
		bool IEqualityComparer.Equals(object x, object y)
		{
			return base.Compare(x, y) == 0;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0008B9F5 File Offset: 0x00089BF5
		int IEqualityComparer.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				throw new ArgumentException(null, "obj");
			}
			return text.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
		}
	}
}
