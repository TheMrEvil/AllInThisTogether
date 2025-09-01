using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x02000212 RID: 530
	internal struct UnSafeCharBuffer
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x0005B17E File Offset: 0x0005937E
		[SecurityCritical]
		public unsafe UnSafeCharBuffer(char* buffer, int bufferSize)
		{
			this.m_buffer = buffer;
			this.m_totalSize = bufferSize;
			this.m_length = 0;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0005B198 File Offset: 0x00059398
		[SecuritySafeCritical]
		public unsafe void AppendString(string stringToAppend)
		{
			if (string.IsNullOrEmpty(stringToAppend))
			{
				return;
			}
			if (this.m_totalSize - this.m_length < stringToAppend.Length)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (string text = stringToAppend)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.Memcpy((byte*)(this.m_buffer + this.m_length), (byte*)ptr, stringToAppend.Length * 2, true);
			}
			this.m_length += stringToAppend.Length;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0005B20D File Offset: 0x0005940D
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x04001647 RID: 5703
		[SecurityCritical]
		private unsafe char* m_buffer;

		// Token: 0x04001648 RID: 5704
		private int m_totalSize;

		// Token: 0x04001649 RID: 5705
		private int m_length;
	}
}
