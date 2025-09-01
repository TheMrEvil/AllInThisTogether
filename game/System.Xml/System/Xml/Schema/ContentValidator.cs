using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000503 RID: 1283
	internal class ContentValidator
	{
		// Token: 0x06003440 RID: 13376 RVA: 0x00127CF1 File Offset: 0x00125EF1
		public ContentValidator(XmlSchemaContentType contentType)
		{
			this.contentType = contentType;
			this.isEmptiable = true;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00127D07 File Offset: 0x00125F07
		protected ContentValidator(XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
		{
			this.contentType = contentType;
			this.isOpen = isOpen;
			this.isEmptiable = isEmptiable;
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x00127D24 File Offset: 0x00125F24
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x00127D2C File Offset: 0x00125F2C
		public bool PreserveWhitespace
		{
			get
			{
				return this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Mixed;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x00127D41 File Offset: 0x00125F41
		public virtual bool IsEmptiable
		{
			get
			{
				return this.isEmptiable;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x00127D49 File Offset: 0x00125F49
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x00127D64 File Offset: 0x00125F64
		public bool IsOpen
		{
			get
			{
				return this.contentType != XmlSchemaContentType.TextOnly && this.contentType != XmlSchemaContentType.Empty && this.isOpen;
			}
			set
			{
				this.isOpen = value;
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void InitValidation(ValidationState context)
		{
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00127D6D File Offset: 0x00125F6D
		public virtual object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			if (this.contentType == XmlSchemaContentType.TextOnly || this.contentType == XmlSchemaContentType.Empty)
			{
				context.NeedValidateChildren = false;
			}
			errorCode = -1;
			return null;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x0001222F File Offset: 0x0001042F
		public virtual bool CompleteValidation(ValidationState context)
		{
			return true;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			return null;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			return null;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00127D8B File Offset: 0x00125F8B
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles)
		{
			ContentValidator.AddParticleToExpected(p, schemaSet, particles, false);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x00127D98 File Offset: 0x00125F98
		public static void AddParticleToExpected(XmlSchemaParticle p, XmlSchemaSet schemaSet, ArrayList particles, bool global)
		{
			if (!particles.Contains(p))
			{
				particles.Add(p);
			}
			XmlSchemaElement xmlSchemaElement = p as XmlSchemaElement;
			if (xmlSchemaElement != null && (global || !xmlSchemaElement.RefName.IsEmpty))
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)schemaSet.SubstitutionGroups[xmlSchemaElement.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i];
						if (!xmlSchemaElement.QualifiedName.Equals(xmlSchemaElement2.QualifiedName) && !particles.Contains(xmlSchemaElement2))
						{
							particles.Add(xmlSchemaElement2);
						}
					}
				}
			}
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x00127E36 File Offset: 0x00126036
		// Note: this type is marked as 'beforefieldinit'.
		static ContentValidator()
		{
		}

		// Token: 0x040026E1 RID: 9953
		private XmlSchemaContentType contentType;

		// Token: 0x040026E2 RID: 9954
		private bool isOpen;

		// Token: 0x040026E3 RID: 9955
		private bool isEmptiable;

		// Token: 0x040026E4 RID: 9956
		public static readonly ContentValidator Empty = new ContentValidator(XmlSchemaContentType.Empty);

		// Token: 0x040026E5 RID: 9957
		public static readonly ContentValidator TextOnly = new ContentValidator(XmlSchemaContentType.TextOnly, false, false);

		// Token: 0x040026E6 RID: 9958
		public static readonly ContentValidator Mixed = new ContentValidator(XmlSchemaContentType.Mixed);

		// Token: 0x040026E7 RID: 9959
		public static readonly ContentValidator Any = new ContentValidator(XmlSchemaContentType.Mixed, true, true);
	}
}
