using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000292 RID: 658
	internal class MembersMapping : TypeMapping
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0008F6BE File Offset: 0x0008D8BE
		// (set) Token: 0x060018E2 RID: 6370 RVA: 0x0008F6C6 File Offset: 0x0008D8C6
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0008F6CF File Offset: 0x0008D8CF
		// (set) Token: 0x060018E4 RID: 6372 RVA: 0x0008F6D7 File Offset: 0x0008D8D7
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0008F6E0 File Offset: 0x0008D8E0
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x0008F6E8 File Offset: 0x0008D8E8
		internal bool HasWrapperElement
		{
			get
			{
				return this.hasWrapperElement;
			}
			set
			{
				this.hasWrapperElement = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0008F6F1 File Offset: 0x0008D8F1
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0008F6F9 File Offset: 0x0008D8F9
		internal bool ValidateRpcWrapperElement
		{
			get
			{
				return this.validateRpcWrapperElement;
			}
			set
			{
				this.validateRpcWrapperElement = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0008F702 File Offset: 0x0008D902
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0008F70A File Offset: 0x0008D90A
		internal bool WriteAccessors
		{
			get
			{
				return this.writeAccessors;
			}
			set
			{
				this.writeAccessors = value;
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0008F713 File Offset: 0x0008D913
		public MembersMapping()
		{
		}

		// Token: 0x040018F0 RID: 6384
		private MemberMapping[] members;

		// Token: 0x040018F1 RID: 6385
		private bool hasWrapperElement = true;

		// Token: 0x040018F2 RID: 6386
		private bool validateRpcWrapperElement;

		// Token: 0x040018F3 RID: 6387
		private bool writeAccessors = true;

		// Token: 0x040018F4 RID: 6388
		private MemberMapping xmlnsMember;
	}
}
