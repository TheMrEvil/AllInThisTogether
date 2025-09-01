using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000422 RID: 1058
	[NativeHeader("Runtime/Shaders/PassIdentifier.h")]
	[UsedByNativeCode]
	public readonly struct PassIdentifier : IEquatable<PassIdentifier>
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0003E864 File Offset: 0x0003CA64
		public uint SubshaderIndex
		{
			get
			{
				return this.m_SubShaderIndex;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x0003E87C File Offset: 0x0003CA7C
		public uint PassIndex
		{
			get
			{
				return this.m_PassIndex;
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0003E894 File Offset: 0x0003CA94
		public override bool Equals(object o)
		{
			bool result;
			if (o is PassIdentifier)
			{
				PassIdentifier rhs = (PassIdentifier)o;
				result = this.Equals(rhs);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0003E8C0 File Offset: 0x0003CAC0
		public bool Equals(PassIdentifier rhs)
		{
			return this.m_SubShaderIndex == rhs.m_SubShaderIndex && this.m_PassIndex == rhs.m_PassIndex;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0003E8F4 File Offset: 0x0003CAF4
		public static bool operator ==(PassIdentifier lhs, PassIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0003E910 File Offset: 0x0003CB10
		public static bool operator !=(PassIdentifier lhs, PassIdentifier rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public override int GetHashCode()
		{
			return this.m_SubShaderIndex.GetHashCode() ^ this.m_PassIndex.GetHashCode();
		}

		// Token: 0x04000DAA RID: 3498
		internal readonly uint m_SubShaderIndex;

		// Token: 0x04000DAB RID: 3499
		internal readonly uint m_PassIndex;
	}
}
