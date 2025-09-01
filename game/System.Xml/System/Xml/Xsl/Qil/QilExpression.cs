using System;
using System.Collections.Generic;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BA RID: 1210
	internal class QilExpression : QilNode
	{
		// Token: 0x06003021 RID: 12321 RVA: 0x0012003C File Offset: 0x0011E23C
		public QilExpression(QilNodeType nodeType, QilNode root, QilFactory factory) : base(nodeType)
		{
			this._factory = factory;
			this._isDebug = factory.False();
			this._defWSet = factory.LiteralObject(new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Auto
			});
			this._wsRules = factory.LiteralObject(new List<WhitespaceRule>());
			this._gloVars = factory.GlobalVariableList();
			this._gloParams = factory.GlobalParameterList();
			this._earlBnd = factory.LiteralObject(new List<EarlyBoundInfo>());
			this._funList = factory.FunctionList();
			this._rootNod = root;
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000678D5 File Offset: 0x00065AD5
		public override int Count
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170008B9 RID: 2233
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._isDebug;
				case 1:
					return this._defWSet;
				case 2:
					return this._wsRules;
				case 3:
					return this._gloParams;
				case 4:
					return this._gloVars;
				case 5:
					return this._earlBnd;
				case 6:
					return this._funList;
				case 7:
					return this._rootNod;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this._isDebug = value;
					return;
				case 1:
					this._defWSet = value;
					return;
				case 2:
					this._wsRules = value;
					return;
				case 3:
					this._gloParams = value;
					return;
				case 4:
					this._gloVars = value;
					return;
				case 5:
					this._earlBnd = value;
					return;
				case 6:
					this._funList = value;
					return;
				case 7:
					this._rootNod = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x001201BA File Offset: 0x0011E3BA
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x001201C2 File Offset: 0x0011E3C2
		public QilFactory Factory
		{
			get
			{
				return this._factory;
			}
			set
			{
				this._factory = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x001201CB File Offset: 0x0011E3CB
		// (set) Token: 0x06003028 RID: 12328 RVA: 0x001201DC File Offset: 0x0011E3DC
		public bool IsDebug
		{
			get
			{
				return this._isDebug.NodeType == QilNodeType.True;
			}
			set
			{
				this._isDebug = (value ? this._factory.True() : this._factory.False());
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x001201FF File Offset: 0x0011E3FF
		// (set) Token: 0x0600302A RID: 12330 RVA: 0x00120216 File Offset: 0x0011E416
		public XmlWriterSettings DefaultWriterSettings
		{
			get
			{
				return (XmlWriterSettings)((QilLiteral)this._defWSet).Value;
			}
			set
			{
				value.ReadOnly = true;
				((QilLiteral)this._defWSet).Value = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x00120230 File Offset: 0x0011E430
		// (set) Token: 0x0600302C RID: 12332 RVA: 0x00120247 File Offset: 0x0011E447
		public IList<WhitespaceRule> WhitespaceRules
		{
			get
			{
				return (IList<WhitespaceRule>)((QilLiteral)this._wsRules).Value;
			}
			set
			{
				((QilLiteral)this._wsRules).Value = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x0012025A File Offset: 0x0011E45A
		// (set) Token: 0x0600302E RID: 12334 RVA: 0x00120267 File Offset: 0x0011E467
		public QilList GlobalParameterList
		{
			get
			{
				return (QilList)this._gloParams;
			}
			set
			{
				this._gloParams = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x00120270 File Offset: 0x0011E470
		// (set) Token: 0x06003030 RID: 12336 RVA: 0x0012027D File Offset: 0x0011E47D
		public QilList GlobalVariableList
		{
			get
			{
				return (QilList)this._gloVars;
			}
			set
			{
				this._gloVars = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x00120286 File Offset: 0x0011E486
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x0012029D File Offset: 0x0011E49D
		public IList<EarlyBoundInfo> EarlyBoundTypes
		{
			get
			{
				return (IList<EarlyBoundInfo>)((QilLiteral)this._earlBnd).Value;
			}
			set
			{
				((QilLiteral)this._earlBnd).Value = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x001202B0 File Offset: 0x0011E4B0
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x001202BD File Offset: 0x0011E4BD
		public QilList FunctionList
		{
			get
			{
				return (QilList)this._funList;
			}
			set
			{
				this._funList = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x001202C6 File Offset: 0x0011E4C6
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x001202CE File Offset: 0x0011E4CE
		public QilNode Root
		{
			get
			{
				return this._rootNod;
			}
			set
			{
				this._rootNod = value;
			}
		}

		// Token: 0x040025BD RID: 9661
		private QilFactory _factory;

		// Token: 0x040025BE RID: 9662
		private QilNode _isDebug;

		// Token: 0x040025BF RID: 9663
		private QilNode _defWSet;

		// Token: 0x040025C0 RID: 9664
		private QilNode _wsRules;

		// Token: 0x040025C1 RID: 9665
		private QilNode _gloVars;

		// Token: 0x040025C2 RID: 9666
		private QilNode _gloParams;

		// Token: 0x040025C3 RID: 9667
		private QilNode _earlBnd;

		// Token: 0x040025C4 RID: 9668
		private QilNode _funList;

		// Token: 0x040025C5 RID: 9669
		private QilNode _rootNod;
	}
}
