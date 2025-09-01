using System;

namespace System.Xml.Schema
{
	// Token: 0x020004EB RID: 1259
	internal class CompiledIdentityConstraint
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x001267C5 File Offset: 0x001249C5
		public CompiledIdentityConstraint.ConstraintRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x001267CD File Offset: 0x001249CD
		public Asttree Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x001267D5 File Offset: 0x001249D5
		public Asttree[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x001267DD File Offset: 0x001249DD
		private CompiledIdentityConstraint()
		{
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x001267FC File Offset: 0x001249FC
		public CompiledIdentityConstraint(XmlSchemaIdentityConstraint constraint, XmlNamespaceManager nsmgr)
		{
			this.name = constraint.QualifiedName;
			try
			{
				this.selector = new Asttree(constraint.Selector.XPath, false, nsmgr);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(constraint.Selector);
				throw ex;
			}
			XmlSchemaObjectCollection xmlSchemaObjectCollection = constraint.Fields;
			this.fields = new Asttree[xmlSchemaObjectCollection.Count];
			for (int i = 0; i < xmlSchemaObjectCollection.Count; i++)
			{
				try
				{
					this.fields[i] = new Asttree(((XmlSchemaXPath)xmlSchemaObjectCollection[i]).XPath, true, nsmgr);
				}
				catch (XmlSchemaException ex2)
				{
					ex2.SetSource(constraint.Fields[i]);
					throw ex2;
				}
			}
			if (constraint is XmlSchemaUnique)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Unique;
				return;
			}
			if (constraint is XmlSchemaKey)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Key;
				return;
			}
			this.role = CompiledIdentityConstraint.ConstraintRole.Keyref;
			this.refer = ((XmlSchemaKeyref)constraint).Refer;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0012690C File Offset: 0x00124B0C
		// Note: this type is marked as 'beforefieldinit'.
		static CompiledIdentityConstraint()
		{
		}

		// Token: 0x040026A3 RID: 9891
		internal XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x040026A4 RID: 9892
		private CompiledIdentityConstraint.ConstraintRole role;

		// Token: 0x040026A5 RID: 9893
		private Asttree selector;

		// Token: 0x040026A6 RID: 9894
		private Asttree[] fields;

		// Token: 0x040026A7 RID: 9895
		internal XmlQualifiedName refer = XmlQualifiedName.Empty;

		// Token: 0x040026A8 RID: 9896
		public static readonly CompiledIdentityConstraint Empty = new CompiledIdentityConstraint();

		// Token: 0x020004EC RID: 1260
		public enum ConstraintRole
		{
			// Token: 0x040026AA RID: 9898
			Unique,
			// Token: 0x040026AB RID: 9899
			Key,
			// Token: 0x040026AC RID: 9900
			Keyref
		}
	}
}
