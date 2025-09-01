using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000021 RID: 33
	[DebuggerTypeProxy(typeof(V64DebugView))]
	[StructLayout(LayoutKind.Explicit)]
	public struct v64
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000075AC File Offset: 0x000057AC
		public v64(byte b)
		{
			this = default(v64);
			this.Byte7 = b;
			this.Byte6 = b;
			this.Byte5 = b;
			this.Byte4 = b;
			this.Byte3 = b;
			this.Byte2 = b;
			this.Byte1 = b;
			this.Byte0 = b;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007608 File Offset: 0x00005808
		public v64(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h)
		{
			this = default(v64);
			this.Byte0 = a;
			this.Byte1 = b;
			this.Byte2 = c;
			this.Byte3 = d;
			this.Byte4 = e;
			this.Byte5 = f;
			this.Byte6 = g;
			this.Byte7 = h;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000765C File Offset: 0x0000585C
		public v64(sbyte b)
		{
			this = default(v64);
			this.SByte7 = b;
			this.SByte6 = b;
			this.SByte5 = b;
			this.SByte4 = b;
			this.SByte3 = b;
			this.SByte2 = b;
			this.SByte1 = b;
			this.SByte0 = b;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000076B8 File Offset: 0x000058B8
		public v64(sbyte a, sbyte b, sbyte c, sbyte d, sbyte e, sbyte f, sbyte g, sbyte h)
		{
			this = default(v64);
			this.SByte0 = a;
			this.SByte1 = b;
			this.SByte2 = c;
			this.SByte3 = d;
			this.SByte4 = e;
			this.SByte5 = f;
			this.SByte6 = g;
			this.SByte7 = h;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000770C File Offset: 0x0000590C
		public v64(short v)
		{
			this = default(v64);
			this.SShort3 = v;
			this.SShort2 = v;
			this.SShort1 = v;
			this.SShort0 = v;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007742 File Offset: 0x00005942
		public v64(short a, short b, short c, short d)
		{
			this = default(v64);
			this.SShort0 = a;
			this.SShort1 = b;
			this.SShort2 = c;
			this.SShort3 = d;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007768 File Offset: 0x00005968
		public v64(ushort v)
		{
			this = default(v64);
			this.UShort3 = v;
			this.UShort2 = v;
			this.UShort1 = v;
			this.UShort0 = v;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000779E File Offset: 0x0000599E
		public v64(ushort a, ushort b, ushort c, ushort d)
		{
			this = default(v64);
			this.UShort0 = a;
			this.UShort1 = b;
			this.UShort2 = c;
			this.UShort3 = d;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000077C4 File Offset: 0x000059C4
		public v64(int v)
		{
			this = default(v64);
			this.SInt1 = v;
			this.SInt0 = v;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000077E8 File Offset: 0x000059E8
		public v64(int a, int b)
		{
			this = default(v64);
			this.SInt0 = a;
			this.SInt1 = b;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007800 File Offset: 0x00005A00
		public v64(uint v)
		{
			this = default(v64);
			this.UInt1 = v;
			this.UInt0 = v;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007824 File Offset: 0x00005A24
		public v64(uint a, uint b)
		{
			this = default(v64);
			this.UInt0 = a;
			this.UInt1 = b;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000783C File Offset: 0x00005A3C
		public v64(float f)
		{
			this = default(v64);
			this.Float1 = f;
			this.Float0 = f;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007860 File Offset: 0x00005A60
		public v64(float a, float b)
		{
			this = default(v64);
			this.Float0 = a;
			this.Float1 = b;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007877 File Offset: 0x00005A77
		public v64(double a)
		{
			this = default(v64);
			this.Double0 = a;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007887 File Offset: 0x00005A87
		public v64(long a)
		{
			this = default(v64);
			this.SLong0 = a;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007897 File Offset: 0x00005A97
		public v64(ulong a)
		{
			this = default(v64);
			this.ULong0 = a;
		}

		// Token: 0x04000214 RID: 532
		[FieldOffset(0)]
		public byte Byte0;

		// Token: 0x04000215 RID: 533
		[FieldOffset(1)]
		public byte Byte1;

		// Token: 0x04000216 RID: 534
		[FieldOffset(2)]
		public byte Byte2;

		// Token: 0x04000217 RID: 535
		[FieldOffset(3)]
		public byte Byte3;

		// Token: 0x04000218 RID: 536
		[FieldOffset(4)]
		public byte Byte4;

		// Token: 0x04000219 RID: 537
		[FieldOffset(5)]
		public byte Byte5;

		// Token: 0x0400021A RID: 538
		[FieldOffset(6)]
		public byte Byte6;

		// Token: 0x0400021B RID: 539
		[FieldOffset(7)]
		public byte Byte7;

		// Token: 0x0400021C RID: 540
		[FieldOffset(0)]
		public sbyte SByte0;

		// Token: 0x0400021D RID: 541
		[FieldOffset(1)]
		public sbyte SByte1;

		// Token: 0x0400021E RID: 542
		[FieldOffset(2)]
		public sbyte SByte2;

		// Token: 0x0400021F RID: 543
		[FieldOffset(3)]
		public sbyte SByte3;

		// Token: 0x04000220 RID: 544
		[FieldOffset(4)]
		public sbyte SByte4;

		// Token: 0x04000221 RID: 545
		[FieldOffset(5)]
		public sbyte SByte5;

		// Token: 0x04000222 RID: 546
		[FieldOffset(6)]
		public sbyte SByte6;

		// Token: 0x04000223 RID: 547
		[FieldOffset(7)]
		public sbyte SByte7;

		// Token: 0x04000224 RID: 548
		[FieldOffset(0)]
		public ushort UShort0;

		// Token: 0x04000225 RID: 549
		[FieldOffset(2)]
		public ushort UShort1;

		// Token: 0x04000226 RID: 550
		[FieldOffset(4)]
		public ushort UShort2;

		// Token: 0x04000227 RID: 551
		[FieldOffset(6)]
		public ushort UShort3;

		// Token: 0x04000228 RID: 552
		[FieldOffset(0)]
		public short SShort0;

		// Token: 0x04000229 RID: 553
		[FieldOffset(2)]
		public short SShort1;

		// Token: 0x0400022A RID: 554
		[FieldOffset(4)]
		public short SShort2;

		// Token: 0x0400022B RID: 555
		[FieldOffset(6)]
		public short SShort3;

		// Token: 0x0400022C RID: 556
		[FieldOffset(0)]
		public uint UInt0;

		// Token: 0x0400022D RID: 557
		[FieldOffset(4)]
		public uint UInt1;

		// Token: 0x0400022E RID: 558
		[FieldOffset(0)]
		public int SInt0;

		// Token: 0x0400022F RID: 559
		[FieldOffset(4)]
		public int SInt1;

		// Token: 0x04000230 RID: 560
		[FieldOffset(0)]
		public ulong ULong0;

		// Token: 0x04000231 RID: 561
		[FieldOffset(0)]
		public long SLong0;

		// Token: 0x04000232 RID: 562
		[FieldOffset(0)]
		public float Float0;

		// Token: 0x04000233 RID: 563
		[FieldOffset(4)]
		public float Float1;

		// Token: 0x04000234 RID: 564
		[FieldOffset(0)]
		public double Double0;
	}
}
