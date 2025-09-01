using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x0200084E RID: 2126
	internal class SecurityBuffer
	{
		// Token: 0x0600439A RID: 17306 RVA: 0x000EBE28 File Offset: 0x000EA028
		public SecurityBuffer(byte[] data, int offset, int size, SecurityBufferType tokentype)
		{
			if (offset < 0 || offset > ((data == null) ? 0 : data.Length))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[]
				{
					offset
				}), ".ctor");
			}
			if (size < 0 || size > ((data == null) ? 0 : (data.Length - offset)))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[]
				{
					size
				}), ".ctor");
			}
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000EBEF3 File Offset: 0x000EA0F3
		public SecurityBuffer(byte[] data, SecurityBufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000EBF24 File Offset: 0x000EA124
		public SecurityBuffer(int size, SecurityBufferType tokentype)
		{
			if (size < 0)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[]
				{
					size
				}), ".ctor");
			}
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000EBF7F File Offset: 0x000EA17F
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = SecurityBufferType.SECBUFFER_CHANNEL_BINDINGS;
			this.unmanagedToken = binding;
		}

		// Token: 0x040028D3 RID: 10451
		public int size;

		// Token: 0x040028D4 RID: 10452
		public SecurityBufferType type;

		// Token: 0x040028D5 RID: 10453
		public byte[] token;

		// Token: 0x040028D6 RID: 10454
		public SafeHandle unmanagedToken;

		// Token: 0x040028D7 RID: 10455
		public int offset;
	}
}
