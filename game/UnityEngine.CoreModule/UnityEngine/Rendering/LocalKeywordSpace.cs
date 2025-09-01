using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000421 RID: 1057
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct LocalKeywordSpace : IEquatable<LocalKeywordSpace>
	{
		// Token: 0x060024D6 RID: 9430 RVA: 0x0003E726 File Offset: 0x0003C926
		[FreeFunction("keywords::GetKeywords", HasExplicitThis = true)]
		private LocalKeyword[] GetKeywords()
		{
			return LocalKeywordSpace.GetKeywords_Injected(ref this);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0003E72E File Offset: 0x0003C92E
		[FreeFunction("keywords::GetKeywordNames", HasExplicitThis = true)]
		private string[] GetKeywordNames()
		{
			return LocalKeywordSpace.GetKeywordNames_Injected(ref this);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0003E736 File Offset: 0x0003C936
		[FreeFunction("keywords::GetKeywordCount", HasExplicitThis = true)]
		private uint GetKeywordCount()
		{
			return LocalKeywordSpace.GetKeywordCount_Injected(ref this);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0003E740 File Offset: 0x0003C940
		[FreeFunction("keywords::GetKeyword", HasExplicitThis = true)]
		private LocalKeyword GetKeyword(string name)
		{
			LocalKeyword result;
			LocalKeywordSpace.GetKeyword_Injected(ref this, name, out result);
			return result;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0003E758 File Offset: 0x0003C958
		public LocalKeyword[] keywords
		{
			get
			{
				return this.GetKeywords();
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0003E770 File Offset: 0x0003C970
		public string[] keywordNames
		{
			get
			{
				return this.GetKeywordNames();
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0003E788 File Offset: 0x0003C988
		public uint keywordCount
		{
			get
			{
				return this.GetKeywordCount();
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0003E7A0 File Offset: 0x0003C9A0
		public LocalKeyword FindKeyword(string name)
		{
			return this.GetKeyword(name);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x0003E7BC File Offset: 0x0003C9BC
		public override bool Equals(object o)
		{
			bool result;
			if (o is LocalKeywordSpace)
			{
				LocalKeywordSpace rhs = (LocalKeywordSpace)o;
				result = this.Equals(rhs);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0003E7E8 File Offset: 0x0003C9E8
		public bool Equals(LocalKeywordSpace rhs)
		{
			return this.m_KeywordSpace == rhs.m_KeywordSpace;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0003E80C File Offset: 0x0003CA0C
		public static bool operator ==(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0003E828 File Offset: 0x0003CA28
		public static bool operator !=(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0003E844 File Offset: 0x0003CA44
		public override int GetHashCode()
		{
			return this.m_KeywordSpace.GetHashCode();
		}

		// Token: 0x060024E3 RID: 9443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LocalKeyword[] GetKeywords_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024E4 RID: 9444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetKeywordNames_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024E5 RID: 9445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetKeywordCount_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024E6 RID: 9446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKeyword_Injected(ref LocalKeywordSpace _unity_self, string name, out LocalKeyword ret);

		// Token: 0x04000DA9 RID: 3497
		private readonly IntPtr m_KeywordSpace;
	}
}
