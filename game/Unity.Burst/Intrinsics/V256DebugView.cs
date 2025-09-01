using System;
using System.Diagnostics;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001E RID: 30
	internal class V256DebugView
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00005D93 File Offset: 0x00003F93
		public V256DebugView(v256 value)
		{
			this.m_Value = value;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005DA4 File Offset: 0x00003FA4
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public byte[] Byte
		{
			get
			{
				return new byte[]
				{
					this.m_Value.Byte0,
					this.m_Value.Byte1,
					this.m_Value.Byte2,
					this.m_Value.Byte3,
					this.m_Value.Byte4,
					this.m_Value.Byte5,
					this.m_Value.Byte6,
					this.m_Value.Byte7,
					this.m_Value.Byte8,
					this.m_Value.Byte9,
					this.m_Value.Byte10,
					this.m_Value.Byte11,
					this.m_Value.Byte12,
					this.m_Value.Byte13,
					this.m_Value.Byte14,
					this.m_Value.Byte15,
					this.m_Value.Byte16,
					this.m_Value.Byte17,
					this.m_Value.Byte18,
					this.m_Value.Byte19,
					this.m_Value.Byte20,
					this.m_Value.Byte21,
					this.m_Value.Byte22,
					this.m_Value.Byte23,
					this.m_Value.Byte24,
					this.m_Value.Byte25,
					this.m_Value.Byte26,
					this.m_Value.Byte27,
					this.m_Value.Byte28,
					this.m_Value.Byte29,
					this.m_Value.Byte30,
					this.m_Value.Byte31
				};
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005F90 File Offset: 0x00004190
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public sbyte[] SByte
		{
			get
			{
				return new sbyte[]
				{
					this.m_Value.SByte0,
					this.m_Value.SByte1,
					this.m_Value.SByte2,
					this.m_Value.SByte3,
					this.m_Value.SByte4,
					this.m_Value.SByte5,
					this.m_Value.SByte6,
					this.m_Value.SByte7,
					this.m_Value.SByte8,
					this.m_Value.SByte9,
					this.m_Value.SByte10,
					this.m_Value.SByte11,
					this.m_Value.SByte12,
					this.m_Value.SByte13,
					this.m_Value.SByte14,
					this.m_Value.SByte15,
					this.m_Value.SByte16,
					this.m_Value.SByte17,
					this.m_Value.SByte18,
					this.m_Value.SByte19,
					this.m_Value.SByte20,
					this.m_Value.SByte21,
					this.m_Value.SByte22,
					this.m_Value.SByte23,
					this.m_Value.SByte24,
					this.m_Value.SByte25,
					this.m_Value.SByte26,
					this.m_Value.SByte27,
					this.m_Value.SByte28,
					this.m_Value.SByte29,
					this.m_Value.SByte30,
					this.m_Value.SByte31
				};
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000617C File Offset: 0x0000437C
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ushort[] UShort
		{
			get
			{
				return new ushort[]
				{
					this.m_Value.UShort0,
					this.m_Value.UShort1,
					this.m_Value.UShort2,
					this.m_Value.UShort3,
					this.m_Value.UShort4,
					this.m_Value.UShort5,
					this.m_Value.UShort6,
					this.m_Value.UShort7,
					this.m_Value.UShort8,
					this.m_Value.UShort9,
					this.m_Value.UShort10,
					this.m_Value.UShort11,
					this.m_Value.UShort12,
					this.m_Value.UShort13,
					this.m_Value.UShort14,
					this.m_Value.UShort15
				};
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006278 File Offset: 0x00004478
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public short[] SShort
		{
			get
			{
				return new short[]
				{
					this.m_Value.SShort0,
					this.m_Value.SShort1,
					this.m_Value.SShort2,
					this.m_Value.SShort3,
					this.m_Value.SShort4,
					this.m_Value.SShort5,
					this.m_Value.SShort6,
					this.m_Value.SShort7,
					this.m_Value.SShort8,
					this.m_Value.SShort9,
					this.m_Value.SShort10,
					this.m_Value.SShort11,
					this.m_Value.SShort12,
					this.m_Value.SShort13,
					this.m_Value.SShort14,
					this.m_Value.SShort15
				};
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006374 File Offset: 0x00004574
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public uint[] UInt
		{
			get
			{
				return new uint[]
				{
					this.m_Value.UInt0,
					this.m_Value.UInt1,
					this.m_Value.UInt2,
					this.m_Value.UInt3,
					this.m_Value.UInt4,
					this.m_Value.UInt5,
					this.m_Value.UInt6,
					this.m_Value.UInt7
				};
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000063F8 File Offset: 0x000045F8
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public int[] SInt
		{
			get
			{
				return new int[]
				{
					this.m_Value.SInt0,
					this.m_Value.SInt1,
					this.m_Value.SInt2,
					this.m_Value.SInt3,
					this.m_Value.SInt4,
					this.m_Value.SInt5,
					this.m_Value.SInt6,
					this.m_Value.SInt7
				};
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000647C File Offset: 0x0000467C
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public float[] Float
		{
			get
			{
				return new float[]
				{
					this.m_Value.Float0,
					this.m_Value.Float1,
					this.m_Value.Float2,
					this.m_Value.Float3,
					this.m_Value.Float4,
					this.m_Value.Float5,
					this.m_Value.Float6,
					this.m_Value.Float7
				};
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000064FF File Offset: 0x000046FF
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public long[] SLong
		{
			get
			{
				return new long[]
				{
					this.m_Value.SLong0,
					this.m_Value.SLong1,
					this.m_Value.SLong2,
					this.m_Value.SLong3
				};
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000653F File Offset: 0x0000473F
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ulong[] ULong
		{
			get
			{
				return new ulong[]
				{
					this.m_Value.ULong0,
					this.m_Value.ULong1,
					this.m_Value.ULong2,
					this.m_Value.ULong3
				};
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000657F File Offset: 0x0000477F
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public double[] Double
		{
			get
			{
				return new double[]
				{
					this.m_Value.Double0,
					this.m_Value.Double1,
					this.m_Value.Double2,
					this.m_Value.Double3
				};
			}
		}

		// Token: 0x04000149 RID: 329
		private v256 m_Value;
	}
}
