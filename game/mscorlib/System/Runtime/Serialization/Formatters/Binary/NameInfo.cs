using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006BF RID: 1727
	internal sealed class NameInfo
	{
		// Token: 0x06003FC8 RID: 16328 RVA: 0x0000259F File Offset: 0x0000079F
		internal NameInfo()
		{
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x000DFA4C File Offset: 0x000DDC4C
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000DFAB6 File Offset: 0x000DDCB6
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000DFADE File Offset: 0x000DDCDE
		// (set) Token: 0x06003FCC RID: 16332 RVA: 0x000DFAFF File Offset: 0x000DDCFF
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x040029BF RID: 10687
		internal string NIFullName;

		// Token: 0x040029C0 RID: 10688
		internal long NIobjectId;

		// Token: 0x040029C1 RID: 10689
		internal long NIassemId;

		// Token: 0x040029C2 RID: 10690
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x040029C3 RID: 10691
		internal Type NItype;

		// Token: 0x040029C4 RID: 10692
		internal bool NIisSealed;

		// Token: 0x040029C5 RID: 10693
		internal bool NIisArray;

		// Token: 0x040029C6 RID: 10694
		internal bool NIisArrayItem;

		// Token: 0x040029C7 RID: 10695
		internal bool NItransmitTypeOnObject;

		// Token: 0x040029C8 RID: 10696
		internal bool NItransmitTypeOnMember;

		// Token: 0x040029C9 RID: 10697
		internal bool NIisParentTypeOnObject;

		// Token: 0x040029CA RID: 10698
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x040029CB RID: 10699
		private bool NIsealedStatusChecked;
	}
}
