using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000420 RID: 1056
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public readonly struct LocalKeyword : IEquatable<LocalKeyword>
	{
		// Token: 0x060024C0 RID: 9408 RVA: 0x0003E4C0 File Offset: 0x0003C6C0
		[FreeFunction("keywords::IsKeywordOverridable")]
		private static bool IsOverridable(LocalKeyword kw)
		{
			return LocalKeyword.IsOverridable_Injected(ref kw);
		}

		// Token: 0x060024C1 RID: 9409
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordCount(Shader shader);

		// Token: 0x060024C2 RID: 9410
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordIndex(Shader shader, string keyword);

		// Token: 0x060024C3 RID: 9411
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordCount(ComputeShader shader);

		// Token: 0x060024C4 RID: 9412
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword);

		// Token: 0x060024C5 RID: 9413 RVA: 0x0003E4C9 File Offset: 0x0003C6C9
		[FreeFunction("keywords::GetKeywordType")]
		private static ShaderKeywordType GetKeywordType(LocalKeywordSpace spaceInfo, uint keyword)
		{
			return LocalKeyword.GetKeywordType_Injected(ref spaceInfo, keyword);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0003E4D3 File Offset: 0x0003C6D3
		[FreeFunction("keywords::IsKeywordValid")]
		private static bool IsValid(LocalKeywordSpace spaceInfo, uint keyword)
		{
			return LocalKeyword.IsValid_Injected(ref spaceInfo, keyword);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x0003E4F8 File Offset: 0x0003C6F8
		public bool isOverridable
		{
			get
			{
				return LocalKeyword.IsOverridable(this);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x0003E518 File Offset: 0x0003C718
		public bool isValid
		{
			get
			{
				return LocalKeyword.IsValid(this.m_SpaceInfo, this.m_Index);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0003E53C File Offset: 0x0003C73C
		public ShaderKeywordType type
		{
			get
			{
				return LocalKeyword.GetKeywordType(this.m_SpaceInfo, this.m_Index);
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0003E560 File Offset: 0x0003C760
		public LocalKeyword(Shader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null Shader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the shader.", new object[]
				{
					name
				});
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
		public LocalKeyword(ComputeShader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null ComputeShader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetComputeShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetComputeShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the compute shader.", new object[]
				{
					name
				});
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0003E640 File Offset: 0x0003C840
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0003E658 File Offset: 0x0003C858
		public override bool Equals(object o)
		{
			bool result;
			if (o is LocalKeyword)
			{
				LocalKeyword rhs = (LocalKeyword)o;
				result = this.Equals(rhs);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0003E684 File Offset: 0x0003C884
		public bool Equals(LocalKeyword rhs)
		{
			return this.m_SpaceInfo == rhs.m_SpaceInfo && this.m_Index == rhs.m_Index;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		public static bool operator ==(LocalKeyword lhs, LocalKeyword rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		public static bool operator !=(LocalKeyword lhs, LocalKeyword rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0003E6F4 File Offset: 0x0003C8F4
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode() ^ this.m_SpaceInfo.GetHashCode();
		}

		// Token: 0x060024D3 RID: 9427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsOverridable_Injected(ref LocalKeyword kw);

		// Token: 0x060024D4 RID: 9428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeywordType GetKeywordType_Injected(ref LocalKeywordSpace spaceInfo, uint keyword);

		// Token: 0x060024D5 RID: 9429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref LocalKeywordSpace spaceInfo, uint keyword);

		// Token: 0x04000DA6 RID: 3494
		internal readonly LocalKeywordSpace m_SpaceInfo;

		// Token: 0x04000DA7 RID: 3495
		internal readonly string m_Name;

		// Token: 0x04000DA8 RID: 3496
		internal readonly uint m_Index;
	}
}
