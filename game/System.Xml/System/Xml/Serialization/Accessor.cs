using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000280 RID: 640
	internal abstract class Accessor
	{
		// Token: 0x0600182D RID: 6189 RVA: 0x0000216B File Offset: 0x0000036B
		internal Accessor()
		{
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0008E528 File Offset: 0x0008C728
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x0008E530 File Offset: 0x0008C730
		internal TypeMapping Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0008E539 File Offset: 0x0008C739
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x0008E541 File Offset: 0x0008C741
		internal object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0008E54A File Offset: 0x0008C74A
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue != DBNull.Value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0008E566 File Offset: 0x0008C766
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0008E57C File Offset: 0x0008C77C
		internal virtual string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0008E585 File Offset: 0x0008C785
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x0008E58D File Offset: 0x0008C78D
		internal bool Any
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0008E596 File Offset: 0x0008C796
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x0008E59E File Offset: 0x0008C79E
		internal string AnyNamespaces
		{
			get
			{
				return this.anyNs;
			}
			set
			{
				this.anyNs = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0008E5A7 File Offset: 0x0008C7A7
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0008E5AF File Offset: 0x0008C7AF
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0008E5B8 File Offset: 0x0008C7B8
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x0008E5C0 File Offset: 0x0008C7C0
		internal XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0008E5C9 File Offset: 0x0008C7C9
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x0008E5D1 File Offset: 0x0008C7D1
		internal bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0008E5DA File Offset: 0x0008C7DA
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x0008E5E2 File Offset: 0x0008C7E2
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
			set
			{
				this.isOptional = value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0008E5EB File Offset: 0x0008C7EB
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0008E5F3 File Offset: 0x0008C7F3
		internal bool IsTopLevelInSchema
		{
			get
			{
				return this.topLevelInSchema;
			}
			set
			{
				this.topLevelInSchema = value;
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0008E5FC File Offset: 0x0008C7FC
		internal static string EscapeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0008E614 File Offset: 0x0008C814
		internal static string EscapeQName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			int num = name.LastIndexOf(':');
			if (num < 0)
			{
				return XmlConvert.EncodeLocalName(name);
			}
			if (num == 0 || num == name.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[]
				{
					name
				}), "name");
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(name.Substring(num + 1)), XmlConvert.EncodeLocalName(name.Substring(0, num))).ToString();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008E694 File Offset: 0x0008C894
		internal static string UnescapeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008E69C File Offset: 0x0008C89C
		internal string ToString(string defaultNs)
		{
			if (this.Any)
			{
				return ((this.Namespace == null) ? "##any" : this.Namespace) + ":" + this.Name;
			}
			if (!(this.Namespace == defaultNs))
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x040018AF RID: 6319
		private string name;

		// Token: 0x040018B0 RID: 6320
		private object defaultValue;

		// Token: 0x040018B1 RID: 6321
		private string ns;

		// Token: 0x040018B2 RID: 6322
		private TypeMapping mapping;

		// Token: 0x040018B3 RID: 6323
		private bool any;

		// Token: 0x040018B4 RID: 6324
		private string anyNs;

		// Token: 0x040018B5 RID: 6325
		private bool topLevelInSchema;

		// Token: 0x040018B6 RID: 6326
		private bool isFixed;

		// Token: 0x040018B7 RID: 6327
		private bool isOptional;

		// Token: 0x040018B8 RID: 6328
		private XmlSchemaForm form;
	}
}
