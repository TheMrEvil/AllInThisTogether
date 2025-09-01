using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000571 RID: 1393
	internal abstract class SchemaDeclBase
	{
		// Token: 0x060037B5 RID: 14261 RVA: 0x0013FC48 File Offset: 0x0013DE48
		protected SchemaDeclBase(XmlQualifiedName name, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			this.maxLength = -1L;
			this.minLength = -1L;
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x0013FC79 File Offset: 0x0013DE79
		protected SchemaDeclBase()
		{
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x0013FC8C File Offset: 0x0013DE8C
		// (set) Token: 0x060037B8 RID: 14264 RVA: 0x0013FC94 File Offset: 0x0013DE94
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x0013FC9D File Offset: 0x0013DE9D
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x0013FCB3 File Offset: 0x0013DEB3
		internal string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x0013FCBC File Offset: 0x0013DEBC
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x0013FCC4 File Offset: 0x0013DEC4
		internal bool IsDeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x0013FCCD File Offset: 0x0013DECD
		// (set) Token: 0x060037BE RID: 14270 RVA: 0x0013FCD5 File Offset: 0x0013DED5
		internal SchemaDeclBase.Use Presence
		{
			get
			{
				return this.presence;
			}
			set
			{
				this.presence = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x0013FCDE File Offset: 0x0013DEDE
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x0013FCE6 File Offset: 0x0013DEE6
		internal long MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x0013FCEF File Offset: 0x0013DEEF
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x0013FCF7 File Offset: 0x0013DEF7
		internal long MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x0013FD00 File Offset: 0x0013DF00
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x0013FD08 File Offset: 0x0013DF08
		internal XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x0013FD11 File Offset: 0x0013DF11
		// (set) Token: 0x060037C6 RID: 14278 RVA: 0x0013FD19 File Offset: 0x0013DF19
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0013FD22 File Offset: 0x0013DF22
		internal void AddValue(string value)
		{
			if (this.values == null)
			{
				this.values = new List<string>();
			}
			this.values.Add(value);
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x0013FD43 File Offset: 0x0013DF43
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x0013FD4B File Offset: 0x0013DF4B
		internal List<string> Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x0013FD54 File Offset: 0x0013DF54
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x0013FD6A File Offset: 0x0013DF6A
		internal string DefaultValueRaw
		{
			get
			{
				if (this.defaultValueRaw == null)
				{
					return string.Empty;
				}
				return this.defaultValueRaw;
			}
			set
			{
				this.defaultValueRaw = value;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x0013FD73 File Offset: 0x0013DF73
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x0013FD7B File Offset: 0x0013DF7B
		internal object DefaultValueTyped
		{
			get
			{
				return this.defaultValueTyped;
			}
			set
			{
				this.defaultValueTyped = value;
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0013FD84 File Offset: 0x0013DF84
		internal bool CheckEnumeration(object pVal)
		{
			return (this.datatype.TokenizedType != XmlTokenizedType.NOTATION && this.datatype.TokenizedType != XmlTokenizedType.ENUMERATION) || this.values.Contains(pVal.ToString());
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0013FDB6 File Offset: 0x0013DFB6
		internal bool CheckValue(object pVal)
		{
			return (this.presence != SchemaDeclBase.Use.Fixed && this.presence != SchemaDeclBase.Use.RequiredFixed) || (this.defaultValueTyped != null && this.datatype.IsEqual(pVal, this.defaultValueTyped));
		}

		// Token: 0x0400289B RID: 10395
		protected XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x0400289C RID: 10396
		protected string prefix;

		// Token: 0x0400289D RID: 10397
		protected bool isDeclaredInExternal;

		// Token: 0x0400289E RID: 10398
		protected SchemaDeclBase.Use presence;

		// Token: 0x0400289F RID: 10399
		protected XmlSchemaType schemaType;

		// Token: 0x040028A0 RID: 10400
		protected XmlSchemaDatatype datatype;

		// Token: 0x040028A1 RID: 10401
		protected string defaultValueRaw;

		// Token: 0x040028A2 RID: 10402
		protected object defaultValueTyped;

		// Token: 0x040028A3 RID: 10403
		protected long maxLength;

		// Token: 0x040028A4 RID: 10404
		protected long minLength;

		// Token: 0x040028A5 RID: 10405
		protected List<string> values;

		// Token: 0x02000572 RID: 1394
		internal enum Use
		{
			// Token: 0x040028A7 RID: 10407
			Default,
			// Token: 0x040028A8 RID: 10408
			Required,
			// Token: 0x040028A9 RID: 10409
			Implied,
			// Token: 0x040028AA RID: 10410
			Fixed,
			// Token: 0x040028AB RID: 10411
			RequiredFixed
		}
	}
}
