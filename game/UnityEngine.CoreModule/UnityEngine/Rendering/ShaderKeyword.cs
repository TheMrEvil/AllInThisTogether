using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000425 RID: 1061
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public struct ShaderKeyword
	{
		// Token: 0x060024F2 RID: 9458
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlobalKeywordCount();

		// Token: 0x060024F3 RID: 9459
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlobalKeywordIndex(string keyword);

		// Token: 0x060024F4 RID: 9460
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetKeywordCount(Shader shader);

		// Token: 0x060024F5 RID: 9461
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetKeywordIndex(Shader shader, string keyword);

		// Token: 0x060024F6 RID: 9462
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetComputeShaderKeywordCount(ComputeShader shader);

		// Token: 0x060024F7 RID: 9463
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword);

		// Token: 0x060024F8 RID: 9464
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateGlobalKeyword(string keyword);

		// Token: 0x060024F9 RID: 9465
		[FreeFunction("ShaderScripting::GetKeywordType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ShaderKeywordType GetGlobalShaderKeywordType(uint keyword);

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x0003E9CC File Offset: 0x0003CBCC
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		public static ShaderKeywordType GetGlobalKeywordType(ShaderKeyword index)
		{
			bool flag = index.IsValid();
			ShaderKeywordType result;
			if (flag)
			{
				result = ShaderKeyword.GetGlobalShaderKeywordType(index.m_Index);
			}
			else
			{
				result = ShaderKeywordType.UserDefined;
			}
			return result;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0003EA14 File Offset: 0x0003CC14
		public ShaderKeyword(string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			bool flag = this.m_Index >= ShaderKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				ShaderKeyword.CreateGlobalKeyword(keywordName);
				this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			}
			this.m_IsValid = true;
			this.m_IsLocal = false;
			this.m_IsCompute = false;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0003EA73 File Offset: 0x0003CC73
		public ShaderKeyword(Shader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetKeywordIndex(shader, keywordName);
			this.m_IsValid = (this.m_Index < ShaderKeyword.GetKeywordCount(shader));
			this.m_IsLocal = true;
			this.m_IsCompute = false;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0003EAAC File Offset: 0x0003CCAC
		public ShaderKeyword(ComputeShader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetComputeShaderKeywordIndex(shader, keywordName);
			this.m_IsValid = (this.m_Index < ShaderKeyword.GetComputeShaderKeywordCount(shader));
			this.m_IsLocal = true;
			this.m_IsCompute = true;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
		public static bool IsKeywordLocal(ShaderKeyword keyword)
		{
			return keyword.m_IsLocal;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0003EB00 File Offset: 0x0003CD00
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0003EB18 File Offset: 0x0003CD18
		public bool IsValid(ComputeShader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0003EB30 File Offset: 0x0003CD30
		public bool IsValid(Shader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0003EB48 File Offset: 0x0003CD48
		public int index
		{
			get
			{
				return (int)this.m_Index;
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0003EB60 File Offset: 0x0003CD60
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0003EB78 File Offset: 0x0003CD78
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(Shader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		[Obsolete("GetGlobalKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetGlobalKeywordName(ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(Shader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0003EBD0 File Offset: 0x0003CDD0
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(ComputeShader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0003EBE8 File Offset: 0x0003CDE8
		[Obsolete("GetKeywordType is deprecated. Use ShaderKeyword.name instead.")]
		public ShaderKeywordType GetKeywordType()
		{
			return ShaderKeyword.GetGlobalKeywordType(this);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0003EC08 File Offset: 0x0003CE08
		[Obsolete("GetKeywordName is deprecated. Use ShaderKeyword.name instead.")]
		public string GetKeywordName()
		{
			return ShaderKeyword.GetGlobalKeywordName(this);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0003EC28 File Offset: 0x0003CE28
		[Obsolete("GetName() has been deprecated. Use ShaderKeyword.name instead.")]
		public string GetName()
		{
			return this.GetKeywordName();
		}

		// Token: 0x04000DB5 RID: 3509
		internal string m_Name;

		// Token: 0x04000DB6 RID: 3510
		internal uint m_Index;

		// Token: 0x04000DB7 RID: 3511
		internal bool m_IsLocal;

		// Token: 0x04000DB8 RID: 3512
		internal bool m_IsCompute;

		// Token: 0x04000DB9 RID: 3513
		internal bool m_IsValid;
	}
}
