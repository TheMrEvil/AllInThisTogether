using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000127 RID: 295
	[NativeType("Runtime/Graphics/RefreshRate.h")]
	public struct RefreshRate : IEquatable<RefreshRate>
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public double value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.numerator / this.denominator;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RefreshRate other)
		{
			return this.numerator == other.numerator && this.denominator == other.denominator;
		}

		// Token: 0x040003B9 RID: 953
		[RequiredMember]
		public uint numerator;

		// Token: 0x040003BA RID: 954
		[RequiredMember]
		public uint denominator;
	}
}
