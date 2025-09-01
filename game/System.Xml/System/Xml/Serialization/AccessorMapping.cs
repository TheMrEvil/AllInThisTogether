using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200028E RID: 654
	internal abstract class AccessorMapping : Mapping
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x0008EAFC File Offset: 0x0008CCFC
		internal AccessorMapping()
		{
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0008F020 File Offset: 0x0008D220
		protected AccessorMapping(AccessorMapping mapping) : base(mapping)
		{
			this.typeDesc = mapping.typeDesc;
			this.attribute = mapping.attribute;
			this.elements = mapping.elements;
			this.sortedElements = mapping.sortedElements;
			this.text = mapping.text;
			this.choiceIdentifier = mapping.choiceIdentifier;
			this.xmlns = mapping.xmlns;
			this.ignore = mapping.ignore;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0008F094 File Offset: 0x0008D294
		internal bool IsAttribute
		{
			get
			{
				return this.attribute != null;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0008F09F File Offset: 0x0008D29F
		internal bool IsText
		{
			get
			{
				return this.text != null && (this.elements == null || this.elements.Length == 0);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0008F0BF File Offset: 0x0008D2BF
		internal bool IsParticle
		{
			get
			{
				return this.elements != null && this.elements.Length != 0;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0008F0D5 File Offset: 0x0008D2D5
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0008F0DD File Offset: 0x0008D2DD
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0008F0E6 File Offset: 0x0008D2E6
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0008F0EE File Offset: 0x0008D2EE
		internal AttributeAccessor Attribute
		{
			get
			{
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0008F0F7 File Offset: 0x0008D2F7
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x0008F0FF File Offset: 0x0008D2FF
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0008F10F File Offset: 0x0008D30F
		internal static void SortMostToLeastDerived(ElementAccessor[] elements)
		{
			Array.Sort(elements, new AccessorMapping.AccessorComparer());
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0008F11C File Offset: 0x0008D31C
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0008F181 File Offset: 0x0008D381
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0008F189 File Offset: 0x0008D389
		internal TextAccessor Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0008F192 File Offset: 0x0008D392
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0008F19A File Offset: 0x0008D39A
		internal ChoiceIdentifierAccessor ChoiceIdentifier
		{
			get
			{
				return this.choiceIdentifier;
			}
			set
			{
				this.choiceIdentifier = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0008F1A3 File Offset: 0x0008D3A3
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0008F1AB File Offset: 0x0008D3AB
		internal XmlnsAccessor Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0008F1B4 File Offset: 0x0008D3B4
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0008F1BC File Offset: 0x0008D3BC
		internal bool Ignore
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0008F1C5 File Offset: 0x0008D3C5
		internal Accessor Accessor
		{
			get
			{
				if (this.xmlns != null)
				{
					return this.xmlns;
				}
				if (this.attribute != null)
				{
					return this.attribute;
				}
				if (this.elements != null && this.elements.Length != 0)
				{
					return this.elements[0];
				}
				return this.text;
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008F208 File Offset: 0x0008D408
		private static bool IsNeedNullableMember(ElementAccessor element)
		{
			if (element.Mapping is ArrayMapping)
			{
				ArrayMapping arrayMapping = (ArrayMapping)element.Mapping;
				return arrayMapping.Elements != null && arrayMapping.Elements.Length == 1 && AccessorMapping.IsNeedNullableMember(arrayMapping.Elements[0]);
			}
			return element.IsNullable && element.Mapping.TypeDesc.IsValueType;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0008F26B File Offset: 0x0008D46B
		internal bool IsNeedNullable
		{
			get
			{
				return this.xmlns == null && this.attribute == null && (this.elements != null && this.elements.Length == 1) && AccessorMapping.IsNeedNullableMember(this.elements[0]);
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008F2A4 File Offset: 0x0008D4A4
		internal static bool ElementsMatch(ElementAccessor[] a, ElementAccessor[] b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i].Name != b[i].Name || a[i].Namespace != b[i].Namespace || a[i].Form != b[i].Form || a[i].IsNullable != b[i].IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008F330 File Offset: 0x0008D530
		internal bool Match(AccessorMapping mapping)
		{
			if (this.Elements != null && this.Elements.Length != 0)
			{
				if (!AccessorMapping.ElementsMatch(this.Elements, mapping.Elements))
				{
					return false;
				}
				if (this.Text == null)
				{
					return mapping.Text == null;
				}
			}
			if (this.Attribute != null)
			{
				return mapping.Attribute != null && (this.Attribute.Name == mapping.Attribute.Name && this.Attribute.Namespace == mapping.Attribute.Namespace) && this.Attribute.Form == mapping.Attribute.Form;
			}
			if (this.Text != null)
			{
				return mapping.Text != null;
			}
			return mapping.Accessor == null;
		}

		// Token: 0x040018DF RID: 6367
		private TypeDesc typeDesc;

		// Token: 0x040018E0 RID: 6368
		private AttributeAccessor attribute;

		// Token: 0x040018E1 RID: 6369
		private ElementAccessor[] elements;

		// Token: 0x040018E2 RID: 6370
		private ElementAccessor[] sortedElements;

		// Token: 0x040018E3 RID: 6371
		private TextAccessor text;

		// Token: 0x040018E4 RID: 6372
		private ChoiceIdentifierAccessor choiceIdentifier;

		// Token: 0x040018E5 RID: 6373
		private XmlnsAccessor xmlns;

		// Token: 0x040018E6 RID: 6374
		private bool ignore;

		// Token: 0x0200028F RID: 655
		internal class AccessorComparer : IComparer
		{
			// Token: 0x060018C5 RID: 6341 RVA: 0x0008F3F8 File Offset: 0x0008D5F8
			public int Compare(object o1, object o2)
			{
				if (o1 == o2)
				{
					return 0;
				}
				Accessor accessor = (Accessor)o1;
				Accessor accessor2 = (Accessor)o2;
				int weight = accessor.Mapping.TypeDesc.Weight;
				int weight2 = accessor2.Mapping.TypeDesc.Weight;
				if (weight == weight2)
				{
					return 0;
				}
				if (weight < weight2)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x060018C6 RID: 6342 RVA: 0x0000216B File Offset: 0x0000036B
			public AccessorComparer()
			{
			}
		}
	}
}
