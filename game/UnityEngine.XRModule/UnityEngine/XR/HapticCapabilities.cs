using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000009 RID: 9
	[NativeConditional("ENABLE_VR")]
	public struct HapticCapabilities : IEquatable<HapticCapabilities>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000255C File Offset: 0x0000075C
		public uint numChannels
		{
			get
			{
				return this.m_NumChannels;
			}
			internal set
			{
				this.m_NumChannels = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002580 File Offset: 0x00000780
		public bool supportsImpulse
		{
			get
			{
				return this.m_SupportsImpulse;
			}
			internal set
			{
				this.m_SupportsImpulse = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000258C File Offset: 0x0000078C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool supportsBuffer
		{
			get
			{
				return this.m_SupportsBuffer;
			}
			internal set
			{
				this.m_SupportsBuffer = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025B0 File Offset: 0x000007B0
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000025C8 File Offset: 0x000007C8
		public uint bufferFrequencyHz
		{
			get
			{
				return this.m_BufferFrequencyHz;
			}
			internal set
			{
				this.m_BufferFrequencyHz = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000025EC File Offset: 0x000007EC
		public uint bufferMaxSize
		{
			get
			{
				return this.m_BufferMaxSize;
			}
			internal set
			{
				this.m_BufferMaxSize = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025F8 File Offset: 0x000007F8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002610 File Offset: 0x00000810
		public uint bufferOptimalSize
		{
			get
			{
				return this.m_BufferOptimalSize;
			}
			internal set
			{
				this.m_BufferOptimalSize = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000261C File Offset: 0x0000081C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is HapticCapabilities);
			return !flag && this.Equals((HapticCapabilities)obj);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002650 File Offset: 0x00000850
		public bool Equals(HapticCapabilities other)
		{
			return this.numChannels == other.numChannels && this.supportsImpulse == other.supportsImpulse && this.supportsBuffer == other.supportsBuffer && this.bufferFrequencyHz == other.bufferFrequencyHz && this.bufferMaxSize == other.bufferMaxSize && this.bufferOptimalSize == other.bufferOptimalSize;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026C0 File Offset: 0x000008C0
		public override int GetHashCode()
		{
			return this.numChannels.GetHashCode() ^ this.supportsImpulse.GetHashCode() << 1 ^ this.supportsBuffer.GetHashCode() >> 1 ^ this.bufferFrequencyHz.GetHashCode() << 2 ^ this.bufferMaxSize.GetHashCode() >> 2 ^ this.bufferOptimalSize.GetHashCode() << 3;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002738 File Offset: 0x00000938
		public static bool operator ==(HapticCapabilities a, HapticCapabilities b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002754 File Offset: 0x00000954
		public static bool operator !=(HapticCapabilities a, HapticCapabilities b)
		{
			return !(a == b);
		}

		// Token: 0x04000026 RID: 38
		private uint m_NumChannels;

		// Token: 0x04000027 RID: 39
		private bool m_SupportsImpulse;

		// Token: 0x04000028 RID: 40
		private bool m_SupportsBuffer;

		// Token: 0x04000029 RID: 41
		private uint m_BufferFrequencyHz;

		// Token: 0x0400002A RID: 42
		private uint m_BufferMaxSize;

		// Token: 0x0400002B RID: 43
		private uint m_BufferOptimalSize;
	}
}
