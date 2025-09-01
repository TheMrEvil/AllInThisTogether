using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E3 RID: 227
	public struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00023AAC File Offset: 0x00021CAC
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0 || tryLength < 0 || filterOffset < 0 || handlerOffset < 0 || handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.tryOffset = tryOffset;
			this.tryLength = tryLength;
			this.filterOffset = filterOffset;
			this.handlerOffset = handlerOffset;
			this.handlerLength = handlerLength;
			this.kind = kind;
			this.exceptionTypeToken = exceptionTypeToken;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00023B0A File Offset: 0x00021D0A
		public int TryOffset
		{
			get
			{
				return this.tryOffset;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00023B12 File Offset: 0x00021D12
		public int TryLength
		{
			get
			{
				return this.tryLength;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00023B1A File Offset: 0x00021D1A
		public int FilterOffset
		{
			get
			{
				return this.filterOffset;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00023B22 File Offset: 0x00021D22
		public int HandlerOffset
		{
			get
			{
				return this.handlerOffset;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00023B2A File Offset: 0x00021D2A
		public int HandlerLength
		{
			get
			{
				return this.handlerLength;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00023B32 File Offset: 0x00021D32
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00023B3A File Offset: 0x00021D3A
		public int ExceptionTypeToken
		{
			get
			{
				return this.exceptionTypeToken;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00023B44 File Offset: 0x00021D44
		public bool Equals(ExceptionHandler other)
		{
			return this.tryOffset == other.tryOffset && this.tryLength == other.tryLength && this.filterOffset == other.filterOffset && this.handlerOffset == other.handlerOffset && this.handlerLength == other.handlerLength && this.kind == other.kind && this.exceptionTypeToken == other.exceptionTypeToken;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public override bool Equals(object obj)
		{
			ExceptionHandler? exceptionHandler = obj as ExceptionHandler?;
			return exceptionHandler != null && this.Equals(exceptionHandler.Value);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00023BE9 File Offset: 0x00021DE9
		public override int GetHashCode()
		{
			return this.tryOffset ^ this.tryLength * 33 ^ this.filterOffset * 333 ^ this.handlerOffset * 3333 ^ this.handlerLength * 33333;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00023C22 File Offset: 0x00021E22
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00023C2C File Offset: 0x00021E2C
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400048C RID: 1164
		private readonly int tryOffset;

		// Token: 0x0400048D RID: 1165
		private readonly int tryLength;

		// Token: 0x0400048E RID: 1166
		private readonly int filterOffset;

		// Token: 0x0400048F RID: 1167
		private readonly int handlerOffset;

		// Token: 0x04000490 RID: 1168
		private readonly int handlerLength;

		// Token: 0x04000491 RID: 1169
		private readonly ExceptionHandlingClauseOptions kind;

		// Token: 0x04000492 RID: 1170
		private readonly int exceptionTypeToken;
	}
}
