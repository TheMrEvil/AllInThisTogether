using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AA RID: 170
	internal abstract class Table
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001E6B1 File Offset: 0x0001C8B1
		internal bool IsBig
		{
			get
			{
				return this.RowCount > 65535;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008C7 RID: 2247
		// (set) Token: 0x060008C8 RID: 2248
		internal abstract int RowCount { get; set; }

		// Token: 0x060008C9 RID: 2249
		internal abstract void Write(MetadataWriter mw);

		// Token: 0x060008CA RID: 2250
		internal abstract void Read(MetadataReader mr);

		// Token: 0x060008CB RID: 2251 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		internal int GetLength(MetadataWriter md)
		{
			return this.RowCount * this.GetRowSize(new Table.RowSizeCalc(md));
		}

		// Token: 0x060008CC RID: 2252
		protected abstract int GetRowSize(Table.RowSizeCalc rsc);

		// Token: 0x060008CD RID: 2253 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Table()
		{
		}

		// Token: 0x040003C8 RID: 968
		internal bool Sorted;

		// Token: 0x02000340 RID: 832
		protected sealed class RowSizeCalc
		{
			// Token: 0x060025F9 RID: 9721 RVA: 0x000B4FDF File Offset: 0x000B31DF
			internal RowSizeCalc(MetadataWriter mw)
			{
				this.mw = mw;
			}

			// Token: 0x060025FA RID: 9722 RVA: 0x000B4FEE File Offset: 0x000B31EE
			internal Table.RowSizeCalc AddFixed(int size)
			{
				this.size += size;
				return this;
			}

			// Token: 0x060025FB RID: 9723 RVA: 0x000B4FFF File Offset: 0x000B31FF
			internal Table.RowSizeCalc WriteStringIndex()
			{
				if (this.mw.bigStrings)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x060025FC RID: 9724 RVA: 0x000B502D File Offset: 0x000B322D
			internal Table.RowSizeCalc WriteGuidIndex()
			{
				if (this.mw.bigGuids)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x060025FD RID: 9725 RVA: 0x000B505B File Offset: 0x000B325B
			internal Table.RowSizeCalc WriteBlobIndex()
			{
				if (this.mw.bigBlobs)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x060025FE RID: 9726 RVA: 0x000B5089 File Offset: 0x000B3289
			internal Table.RowSizeCalc WriteTypeDefOrRef()
			{
				if (this.mw.bigTypeDefOrRef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x060025FF RID: 9727 RVA: 0x000B50B7 File Offset: 0x000B32B7
			internal Table.RowSizeCalc WriteField()
			{
				if (this.mw.bigField)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002600 RID: 9728 RVA: 0x000B50E5 File Offset: 0x000B32E5
			internal Table.RowSizeCalc WriteMethodDef()
			{
				if (this.mw.bigMethodDef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002601 RID: 9729 RVA: 0x000B5113 File Offset: 0x000B3313
			internal Table.RowSizeCalc WriteParam()
			{
				if (this.mw.bigParam)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002602 RID: 9730 RVA: 0x000B5141 File Offset: 0x000B3341
			internal Table.RowSizeCalc WriteResolutionScope()
			{
				if (this.mw.bigResolutionScope)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002603 RID: 9731 RVA: 0x000B516F File Offset: 0x000B336F
			internal Table.RowSizeCalc WriteMemberRefParent()
			{
				if (this.mw.bigMemberRefParent)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002604 RID: 9732 RVA: 0x000B519D File Offset: 0x000B339D
			internal Table.RowSizeCalc WriteHasCustomAttribute()
			{
				if (this.mw.bigHasCustomAttribute)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x000B51CB File Offset: 0x000B33CB
			internal Table.RowSizeCalc WriteCustomAttributeType()
			{
				if (this.mw.bigCustomAttributeType)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x000B51F9 File Offset: 0x000B33F9
			internal Table.RowSizeCalc WriteHasConstant()
			{
				if (this.mw.bigHasConstant)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002607 RID: 9735 RVA: 0x000B5227 File Offset: 0x000B3427
			internal Table.RowSizeCalc WriteTypeDef()
			{
				if (this.mw.bigTypeDef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002608 RID: 9736 RVA: 0x000B5255 File Offset: 0x000B3455
			internal Table.RowSizeCalc WriteMethodDefOrRef()
			{
				if (this.mw.bigMethodDefOrRef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002609 RID: 9737 RVA: 0x000B5283 File Offset: 0x000B3483
			internal Table.RowSizeCalc WriteEvent()
			{
				if (this.mw.bigEvent)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260A RID: 9738 RVA: 0x000B52B1 File Offset: 0x000B34B1
			internal Table.RowSizeCalc WriteProperty()
			{
				if (this.mw.bigProperty)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260B RID: 9739 RVA: 0x000B52DF File Offset: 0x000B34DF
			internal Table.RowSizeCalc WriteHasSemantics()
			{
				if (this.mw.bigHasSemantics)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260C RID: 9740 RVA: 0x000B530D File Offset: 0x000B350D
			internal Table.RowSizeCalc WriteImplementation()
			{
				if (this.mw.bigImplementation)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260D RID: 9741 RVA: 0x000B533B File Offset: 0x000B353B
			internal Table.RowSizeCalc WriteTypeOrMethodDef()
			{
				if (this.mw.bigTypeOrMethodDef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260E RID: 9742 RVA: 0x000B5369 File Offset: 0x000B3569
			internal Table.RowSizeCalc WriteGenericParam()
			{
				if (this.mw.bigGenericParam)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x0600260F RID: 9743 RVA: 0x000B5397 File Offset: 0x000B3597
			internal Table.RowSizeCalc WriteHasDeclSecurity()
			{
				if (this.mw.bigHasDeclSecurity)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002610 RID: 9744 RVA: 0x000B53C5 File Offset: 0x000B35C5
			internal Table.RowSizeCalc WriteMemberForwarded()
			{
				if (this.mw.bigMemberForwarded)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002611 RID: 9745 RVA: 0x000B53F3 File Offset: 0x000B35F3
			internal Table.RowSizeCalc WriteModuleRef()
			{
				if (this.mw.bigModuleRef)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x06002612 RID: 9746 RVA: 0x000B5421 File Offset: 0x000B3621
			internal Table.RowSizeCalc WriteHasFieldMarshal()
			{
				if (this.mw.bigHasFieldMarshal)
				{
					this.size += 4;
				}
				else
				{
					this.size += 2;
				}
				return this;
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x06002613 RID: 9747 RVA: 0x000B544F File Offset: 0x000B364F
			internal int Value
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x04000E8D RID: 3725
			private readonly MetadataWriter mw;

			// Token: 0x04000E8E RID: 3726
			private int size;
		}
	}
}
