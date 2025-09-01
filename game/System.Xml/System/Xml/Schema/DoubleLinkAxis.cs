using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020004E3 RID: 1251
	internal class DoubleLinkAxis : Axis
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x00125230 File Offset: 0x00123430
		// (set) Token: 0x06003363 RID: 13155 RVA: 0x00125238 File Offset: 0x00123438
		internal Axis Next
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

		// Token: 0x06003364 RID: 13156 RVA: 0x00125244 File Offset: 0x00123444
		internal DoubleLinkAxis(Axis axis, DoubleLinkAxis inputaxis) : base(axis.TypeOfAxis, inputaxis, axis.Prefix, axis.Name, axis.NodeType)
		{
			this.next = null;
			base.Urn = axis.Urn;
			this.abbrAxis = axis.AbbrAxis;
			if (inputaxis != null)
			{
				inputaxis.Next = this;
			}
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x00125299 File Offset: 0x00123499
		internal static DoubleLinkAxis ConvertTree(Axis axis)
		{
			if (axis == null)
			{
				return null;
			}
			return new DoubleLinkAxis(axis, DoubleLinkAxis.ConvertTree((Axis)axis.Input));
		}

		// Token: 0x0400267B RID: 9851
		internal Axis next;
	}
}
