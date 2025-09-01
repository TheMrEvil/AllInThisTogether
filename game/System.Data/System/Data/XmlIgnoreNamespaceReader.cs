using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000140 RID: 320
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x0004C9CE File Offset: 0x0004ABCE
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore) : base(xdoc)
		{
			this._namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004C9E4 File Offset: 0x0004ABE4
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this._namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0004CA3C File Offset: 0x0004AC3C
		public override bool MoveToNextAttribute()
		{
			bool result;
			bool flag;
			do
			{
				result = false;
				flag = false;
				if (base.MoveToNextAttribute())
				{
					result = true;
					if (this._namespacesToIgnore.Contains(this.NamespaceURI) || (this.NamespaceURI == "http://www.w3.org/XML/1998/namespace" && this.LocalName != "lang"))
					{
						flag = true;
					}
				}
			}
			while (flag);
			return result;
		}

		// Token: 0x04000A79 RID: 2681
		private List<string> _namespacesToIgnore;
	}
}
