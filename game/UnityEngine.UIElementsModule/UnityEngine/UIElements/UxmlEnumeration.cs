using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F3 RID: 755
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00065E38 File Offset: 0x00064038
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x00065E50 File Offset: 0x00064050
		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = value.ToList<string>();
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00065E60 File Offset: 0x00064060
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration uxmlEnumeration = other as UxmlEnumeration;
			bool flag = uxmlEnumeration == null;
			return !flag && this.values.All(new Func<string, bool>(uxmlEnumeration.values.Contains<string>)) && this.values.Count<string>() == uxmlEnumeration.values.Count<string>();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00065EBF File Offset: 0x000640BF
		public UxmlEnumeration()
		{
		}

		// Token: 0x04000AB8 RID: 2744
		private List<string> m_Values = new List<string>();
	}
}
