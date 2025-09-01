using System;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000426 RID: 1062
	[UsedByNativeCode]
	[NativeHeader("Editor/Src/Graphics/ShaderCompilerData.h")]
	public struct ShaderKeywordSet
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x0003EC40 File Offset: 0x0003CE40
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsGlobalKeywordEnabled(ShaderKeywordSet state, uint index)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled_Injected(ref state, index);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0003EC4A File Offset: 0x0003CE4A
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsKeywordEnabled(ShaderKeywordSet state, LocalKeywordSpace keywordSpace, uint index)
		{
			return ShaderKeywordSet.IsKeywordEnabled_Injected(ref state, ref keywordSpace, index);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0003EC56 File Offset: 0x0003CE56
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsKeywordNameEnabled(ShaderKeywordSet state, string name)
		{
			return ShaderKeywordSet.IsKeywordNameEnabled_Injected(ref state, name);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0003EC60 File Offset: 0x0003CE60
		[FreeFunction("keywords::EnableKeyword")]
		private static void EnableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.EnableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0003EC6A File Offset: 0x0003CE6A
		[FreeFunction("keywords::EnableKeyword")]
		private static void EnableKeywordName(ShaderKeywordSet state, string name)
		{
			ShaderKeywordSet.EnableKeywordName_Injected(ref state, name);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0003EC74 File Offset: 0x0003CE74
		[FreeFunction("keywords::DisableKeyword")]
		private static void DisableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.DisableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0003EC7E File Offset: 0x0003CE7E
		[FreeFunction("keywords::DisableKeyword")]
		private static void DisableKeywordName(ShaderKeywordSet state, string name)
		{
			ShaderKeywordSet.DisableKeywordName_Injected(ref state, name);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0003EC88 File Offset: 0x0003CE88
		[FreeFunction("keywords::GetEnabledKeywords")]
		private static ShaderKeyword[] GetEnabledKeywords(ShaderKeywordSet state)
		{
			return ShaderKeywordSet.GetEnabledKeywords_Injected(ref state);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0003EC94 File Offset: 0x0003CE94
		private void CheckKeywordCompatible(ShaderKeyword keyword)
		{
			bool isLocal = keyword.m_IsLocal;
			if (isLocal)
			{
				bool flag = this.m_Shader != IntPtr.Zero;
				if (flag)
				{
					Assert.IsTrue(!keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
				else
				{
					Assert.IsTrue(keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0003ECEC File Offset: 0x0003CEEC
		public bool IsEnabled(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			return ShaderKeywordSet.IsKeywordNameEnabled(this, keyword.m_Name);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0003ED18 File Offset: 0x0003CF18
		public bool IsEnabled(GlobalKeyword keyword)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled(this, keyword.m_Index);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		public bool IsEnabled(LocalKeyword keyword)
		{
			return ShaderKeywordSet.IsKeywordEnabled(this, keyword.m_SpaceInfo, keyword.m_Index);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0003ED68 File Offset: 0x0003CF68
		public void Enable(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.EnableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.EnableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0003EDC0 File Offset: 0x0003CFC0
		public void Disable(ShaderKeyword keyword)
		{
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.DisableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.DisableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0003EE10 File Offset: 0x0003D010
		public ShaderKeyword[] GetShaderKeywords()
		{
			return ShaderKeywordSet.GetEnabledKeywords(this);
		}

		// Token: 0x0600251C RID: 9500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsGlobalKeywordEnabled_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x0600251D RID: 9501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(ref ShaderKeywordSet state, ref LocalKeywordSpace keywordSpace, uint index);

		// Token: 0x0600251E RID: 9502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordNameEnabled_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x0600251F RID: 9503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGlobalKeyword_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x06002520 RID: 9504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeywordName_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x06002521 RID: 9505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGlobalKeyword_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x06002522 RID: 9506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeywordName_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x06002523 RID: 9507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeyword[] GetEnabledKeywords_Injected(ref ShaderKeywordSet state);

		// Token: 0x04000DBA RID: 3514
		private IntPtr m_KeywordState;

		// Token: 0x04000DBB RID: 3515
		private IntPtr m_Shader;

		// Token: 0x04000DBC RID: 3516
		private IntPtr m_ComputeShader;

		// Token: 0x04000DBD RID: 3517
		private ulong m_StateIndex;
	}
}
