using System;
using System.Diagnostics;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001C RID: 28
	internal class V64DebugView
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005714 File Offset: 0x00003914
		public V64DebugView(v64 value)
		{
			this.m_Value = value;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005724 File Offset: 0x00003924
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
					this.m_Value.Byte7
				};
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000057A8 File Offset: 0x000039A8
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
					this.m_Value.SByte7
				};
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000582B File Offset: 0x00003A2B
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
					this.m_Value.UShort3
				};
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000586B File Offset: 0x00003A6B
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
					this.m_Value.SShort3
				};
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000058AB File Offset: 0x00003AAB
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public uint[] UInt
		{
			get
			{
				return new uint[]
				{
					this.m_Value.UInt0,
					this.m_Value.UInt1
				};
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000058CF File Offset: 0x00003ACF
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public int[] SInt
		{
			get
			{
				return new int[]
				{
					this.m_Value.SInt0,
					this.m_Value.SInt1
				};
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000058F3 File Offset: 0x00003AF3
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public float[] Float
		{
			get
			{
				return new float[]
				{
					this.m_Value.Float0,
					this.m_Value.Float1
				};
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005917 File Offset: 0x00003B17
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public long[] SLong
		{
			get
			{
				return new long[]
				{
					this.m_Value.SLong0
				};
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000592D File Offset: 0x00003B2D
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ulong[] ULong
		{
			get
			{
				return new ulong[]
				{
					this.m_Value.ULong0
				};
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005943 File Offset: 0x00003B43
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public double[] Double
		{
			get
			{
				return new double[]
				{
					this.m_Value.Double0
				};
			}
		}

		// Token: 0x04000147 RID: 327
		private v64 m_Value;
	}
}
