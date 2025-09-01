using System;
using System.Reflection;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BE RID: 1214
	internal class QilInvokeEarlyBound : QilTernary
	{
		// Token: 0x060030BB RID: 12475 RVA: 0x00121623 File Offset: 0x0011F823
		public QilInvokeEarlyBound(QilNodeType nodeType, QilNode name, QilNode method, QilNode arguments, XmlQueryType resultType) : base(nodeType, name, method, arguments)
		{
			this.xmlType = resultType;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x00121638 File Offset: 0x0011F838
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x00121645 File Offset: 0x0011F845
		public QilName Name
		{
			get
			{
				return (QilName)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0012164E File Offset: 0x0011F84E
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x00121665 File Offset: 0x0011F865
		public MethodInfo ClrMethod
		{
			get
			{
				return (MethodInfo)((QilLiteral)base.Center).Value;
			}
			set
			{
				((QilLiteral)base.Center).Value = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x00121678 File Offset: 0x0011F878
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x00121685 File Offset: 0x0011F885
		public QilList Arguments
		{
			get
			{
				return (QilList)base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
