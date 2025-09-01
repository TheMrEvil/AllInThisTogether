using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200028A RID: 650
	internal class ArrayMapping : TypeMapping
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0008E9E2 File Offset: 0x0008CBE2
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0008E9EA File Offset: 0x0008CBEA
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0008E9FC File Offset: 0x0008CBFC
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0008EA61 File Offset: 0x0008CC61
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0008EA69 File Offset: 0x0008CC69
		internal ArrayMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0008EA72 File Offset: 0x0008CC72
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0008EA7A File Offset: 0x0008CC7A
		internal StructMapping TopLevelMapping
		{
			get
			{
				return this.topLevelMapping;
			}
			set
			{
				this.topLevelMapping = value;
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0008E9BC File Offset: 0x0008CBBC
		public ArrayMapping()
		{
		}

		// Token: 0x040018CB RID: 6347
		private ElementAccessor[] elements;

		// Token: 0x040018CC RID: 6348
		private ElementAccessor[] sortedElements;

		// Token: 0x040018CD RID: 6349
		private ArrayMapping next;

		// Token: 0x040018CE RID: 6350
		private StructMapping topLevelMapping;
	}
}
