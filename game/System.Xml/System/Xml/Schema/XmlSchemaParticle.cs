using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for that is the base class for all particle types (e.g. <see cref="T:System.Xml.Schema.XmlSchemaAny" />).</summary>
	// Token: 0x020005D4 RID: 1492
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the number as a string value. The minimum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. <see langword="String.Empty" /> indicates that <see langword="MinOccurs" /> is equal to the default value. The default is a null reference.</returns>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x0014EC72 File Offset: 0x0014CE72
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x0014EC8C File Offset: 0x0014CE8C
		[XmlAttribute("minOccurs")]
		public string MinOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Min) != XmlSchemaParticle.Occurs.None)
				{
					return XmlConvert.ToString(this.minOccurs);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.minOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Min;
					return;
				}
				this.minOccurs = XmlConvert.ToInteger(value);
				if (this.minOccurs < 0m)
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the number as a string value. Maximum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. <see langword="String.Empty" /> indicates that <see langword="MaxOccurs" /> is equal to the default value. The default is a null reference.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x0014ECF3 File Offset: 0x0014CEF3
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x0014ED2C File Offset: 0x0014CF2C
		[XmlAttribute("maxOccurs")]
		public string MaxOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Max) == XmlSchemaParticle.Occurs.None)
				{
					return null;
				}
				if (!(this.maxOccurs == 79228162514264337593543950335m))
				{
					return XmlConvert.ToString(this.maxOccurs);
				}
				return "unbounded";
			}
			set
			{
				if (value == null)
				{
					this.maxOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Max;
					return;
				}
				if (value == "unbounded")
				{
					this.maxOccurs = decimal.MaxValue;
				}
				else
				{
					this.maxOccurs = XmlConvert.ToInteger(value);
					if (this.maxOccurs < 0m)
					{
						throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
					}
					if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
					{
						this.minOccurs = 0m;
					}
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		/// <summary>Gets or sets the minimum number of times the particle can occur.</summary>
		/// <returns>The minimum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x0014EDDA File Offset: 0x0014CFDA
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x0014EDE4 File Offset: 0x0014CFE4
		[XmlIgnore]
		public decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.minOccurs = value;
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the maximum number of times the particle can occur.</summary>
		/// <returns>The maximum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x0014EE31 File Offset: 0x0014D031
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x0014EE3C File Offset: 0x0014D03C
		[XmlIgnore]
		public decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
				}
				this.maxOccurs = value;
				if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
				{
					this.minOccurs = 0m;
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x0014EEB1 File Offset: 0x0014D0B1
		internal virtual bool IsEmpty
		{
			get
			{
				return this.maxOccurs == 0m;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x0014EEC3 File Offset: 0x0014D0C3
		internal bool IsMultipleOccurrence
		{
			get
			{
				return this.maxOccurs > 1m;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x0001E51E File Offset: 0x0001C71E
		internal virtual string NameString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x0014EED8 File Offset: 0x0014D0D8
		internal XmlQualifiedName GetQualifiedName()
		{
			XmlSchemaElement xmlSchemaElement = this as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return xmlSchemaElement.QualifiedName;
			}
			XmlSchemaAny xmlSchemaAny = this as XmlSchemaAny;
			if (xmlSchemaAny != null)
			{
				string text = xmlSchemaAny.Namespace;
				if (text != null)
				{
					text = text.Trim();
				}
				else
				{
					text = string.Empty;
				}
				return new XmlQualifiedName("*", (text.Length == 0) ? "##any" : text);
			}
			return XmlQualifiedName.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaParticle" /> class.</summary>
		// Token: 0x06003BD6 RID: 15318 RVA: 0x0014EF39 File Offset: 0x0014D139
		protected XmlSchemaParticle()
		{
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x0014EF57 File Offset: 0x0014D157
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSchemaParticle()
		{
		}

		// Token: 0x04002B9B RID: 11163
		private decimal minOccurs = 1m;

		// Token: 0x04002B9C RID: 11164
		private decimal maxOccurs = 1m;

		// Token: 0x04002B9D RID: 11165
		private XmlSchemaParticle.Occurs flags;

		// Token: 0x04002B9E RID: 11166
		internal static readonly XmlSchemaParticle Empty = new XmlSchemaParticle.EmptyParticle();

		// Token: 0x020005D5 RID: 1493
		[Flags]
		private enum Occurs
		{
			// Token: 0x04002BA0 RID: 11168
			None = 0,
			// Token: 0x04002BA1 RID: 11169
			Min = 1,
			// Token: 0x04002BA2 RID: 11170
			Max = 2
		}

		// Token: 0x020005D6 RID: 1494
		private class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x17000BA2 RID: 2978
			// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x0001222F File Offset: 0x0001042F
			internal override bool IsEmpty
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003BD9 RID: 15321 RVA: 0x0014BD7B File Offset: 0x00149F7B
			public EmptyParticle()
			{
			}
		}
	}
}
