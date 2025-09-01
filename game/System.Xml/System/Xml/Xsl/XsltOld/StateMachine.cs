using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003AB RID: 939
	internal class StateMachine
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x000E705A File Offset: 0x000E525A
		internal StateMachine()
		{
			this._State = 0;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000E7069 File Offset: 0x000E5269
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x000E7071 File Offset: 0x000E5271
		internal int State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000E707A File Offset: 0x000E527A
		internal void Reset()
		{
			this._State = 0;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000E7083 File Offset: 0x000E5283
		internal static int StateOnly(int state)
		{
			return state & 15;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000E7089 File Offset: 0x000E5289
		internal int BeginOutlook(XPathNodeType nodeType)
		{
			return StateMachine.s_BeginTransitions[(int)nodeType][this._State];
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000E709C File Offset: 0x000E529C
		internal int Begin(XPathNodeType nodeType)
		{
			int num = StateMachine.s_BeginTransitions[(int)nodeType][this._State];
			if (num != 16 && num != 32)
			{
				this._State = (num & 15);
			}
			return num;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000E70CD File Offset: 0x000E52CD
		internal int EndOutlook(XPathNodeType nodeType)
		{
			return StateMachine.s_EndTransitions[(int)nodeType][this._State];
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000E70E0 File Offset: 0x000E52E0
		internal int End(XPathNodeType nodeType)
		{
			int num = StateMachine.s_EndTransitions[(int)nodeType][this._State];
			if (num != 16 && num != 32)
			{
				this._State = (num & 15);
			}
			return num;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000E7114 File Offset: 0x000E5314
		// Note: this type is marked as 'beforefieldinit'.
		static StateMachine()
		{
		}

		// Token: 0x04001E25 RID: 7717
		private const int Init = 0;

		// Token: 0x04001E26 RID: 7718
		private const int Elem = 1;

		// Token: 0x04001E27 RID: 7719
		private const int NsN = 2;

		// Token: 0x04001E28 RID: 7720
		private const int NsV = 3;

		// Token: 0x04001E29 RID: 7721
		private const int Ns = 4;

		// Token: 0x04001E2A RID: 7722
		private const int AttrN = 5;

		// Token: 0x04001E2B RID: 7723
		private const int AttrV = 6;

		// Token: 0x04001E2C RID: 7724
		private const int Attr = 7;

		// Token: 0x04001E2D RID: 7725
		private const int InElm = 8;

		// Token: 0x04001E2E RID: 7726
		private const int EndEm = 9;

		// Token: 0x04001E2F RID: 7727
		private const int InCmt = 10;

		// Token: 0x04001E30 RID: 7728
		private const int InPI = 11;

		// Token: 0x04001E31 RID: 7729
		private const int StateMask = 15;

		// Token: 0x04001E32 RID: 7730
		internal const int Error = 16;

		// Token: 0x04001E33 RID: 7731
		private const int Ignor = 32;

		// Token: 0x04001E34 RID: 7732
		private const int Assrt = 48;

		// Token: 0x04001E35 RID: 7733
		private const int U = 256;

		// Token: 0x04001E36 RID: 7734
		private const int D = 512;

		// Token: 0x04001E37 RID: 7735
		internal const int DepthMask = 768;

		// Token: 0x04001E38 RID: 7736
		internal const int DepthUp = 256;

		// Token: 0x04001E39 RID: 7737
		internal const int DepthDown = 512;

		// Token: 0x04001E3A RID: 7738
		private const int C = 1024;

		// Token: 0x04001E3B RID: 7739
		private const int H = 2048;

		// Token: 0x04001E3C RID: 7740
		private const int M = 4096;

		// Token: 0x04001E3D RID: 7741
		internal const int BeginChild = 1024;

		// Token: 0x04001E3E RID: 7742
		internal const int HadChild = 2048;

		// Token: 0x04001E3F RID: 7743
		internal const int EmptyTag = 4096;

		// Token: 0x04001E40 RID: 7744
		private const int B = 8192;

		// Token: 0x04001E41 RID: 7745
		private const int E = 16384;

		// Token: 0x04001E42 RID: 7746
		internal const int BeginRecord = 8192;

		// Token: 0x04001E43 RID: 7747
		internal const int EndRecord = 16384;

		// Token: 0x04001E44 RID: 7748
		private const int S = 32768;

		// Token: 0x04001E45 RID: 7749
		private const int P = 65536;

		// Token: 0x04001E46 RID: 7750
		internal const int PushScope = 32768;

		// Token: 0x04001E47 RID: 7751
		internal const int PopScope = 65536;

		// Token: 0x04001E48 RID: 7752
		private int _State;

		// Token: 0x04001E49 RID: 7753
		private static readonly int[][] s_BeginTransitions = new int[][]
		{
			new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			},
			new int[]
			{
				40961,
				42241,
				16,
				16,
				41985,
				16,
				16,
				41985,
				40961,
				106497,
				16,
				16
			},
			new int[]
			{
				16,
				261,
				16,
				16,
				5,
				16,
				16,
				5,
				16,
				16,
				16,
				16
			},
			new int[]
			{
				16,
				258,
				16,
				16,
				2,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			},
			new int[]
			{
				8200,
				9480,
				259,
				3,
				9224,
				262,
				6,
				9224,
				8,
				73736,
				10,
				11
			},
			new int[]
			{
				8200,
				9480,
				259,
				3,
				9224,
				262,
				6,
				9224,
				8,
				73736,
				10,
				11
			},
			new int[]
			{
				8200,
				9480,
				259,
				3,
				9224,
				262,
				6,
				9224,
				8,
				73736,
				10,
				11
			},
			new int[]
			{
				8203,
				9483,
				16,
				16,
				9227,
				16,
				16,
				9227,
				8203,
				73739,
				16,
				16
			},
			new int[]
			{
				8202,
				9482,
				16,
				16,
				9226,
				16,
				16,
				9226,
				8202,
				73738,
				16,
				16
			},
			new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			}
		};

		// Token: 0x04001E4A RID: 7754
		private static readonly int[][] s_EndTransitions = new int[][]
		{
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				94217,
				48,
				48,
				94729,
				48,
				48,
				94729,
				92681,
				92681,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				7,
				519,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				4,
				516,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				16393
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				16393,
				48
			},
			new int[]
			{
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48,
				48
			}
		};
	}
}
