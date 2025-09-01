using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000281 RID: 641
	internal class ElementAccessor : Accessor
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0008E702 File Offset: 0x0008C902
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x0008E70A File Offset: 0x0008C90A
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0008E713 File Offset: 0x0008C913
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0008E71B File Offset: 0x0008C91B
		internal bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0008E724 File Offset: 0x0008C924
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0008E72C File Offset: 0x0008C92C
		internal bool IsUnbounded
		{
			get
			{
				return this.unbounded;
			}
			set
			{
				this.unbounded = value;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008E738 File Offset: 0x0008C938
		internal ElementAccessor Clone()
		{
			return new ElementAccessor
			{
				nullable = this.nullable,
				IsTopLevelInSchema = base.IsTopLevelInSchema,
				Form = base.Form,
				isSoap = this.isSoap,
				Name = this.Name,
				Default = base.Default,
				Namespace = base.Namespace,
				Mapping = base.Mapping,
				Any = base.Any
			};
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008E7B6 File Offset: 0x0008C9B6
		public ElementAccessor()
		{
		}

		// Token: 0x040018B9 RID: 6329
		private bool nullable;

		// Token: 0x040018BA RID: 6330
		private bool isSoap;

		// Token: 0x040018BB RID: 6331
		private bool unbounded;
	}
}
