using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000285 RID: 645
	internal class AttributeAccessor : Accessor
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0008E7F1 File Offset: 0x0008C9F1
		internal bool IsSpecialXmlNamespace
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0008E7F9 File Offset: 0x0008C9F9
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0008E801 File Offset: 0x0008CA01
		internal bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0008E80C File Offset: 0x0008CA0C
		internal void CheckSpecial()
		{
			if (this.Name.LastIndexOf(':') >= 0)
			{
				if (!this.Name.StartsWith("xml:", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("Invalid name character in '{0}'.", new object[]
					{
						this.Name
					}));
				}
				this.Name = this.Name.Substring("xml:".Length);
				base.Namespace = "http://www.w3.org/XML/1998/namespace";
				this.isSpecial = true;
			}
			else if (base.Namespace == "http://www.w3.org/XML/1998/namespace")
			{
				this.isSpecial = true;
			}
			else
			{
				this.isSpecial = false;
			}
			if (this.isSpecial)
			{
				base.Form = XmlSchemaForm.Qualified;
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0008E7B6 File Offset: 0x0008C9B6
		public AttributeAccessor()
		{
		}

		// Token: 0x040018BF RID: 6335
		private bool isSpecial;

		// Token: 0x040018C0 RID: 6336
		private bool isList;
	}
}
