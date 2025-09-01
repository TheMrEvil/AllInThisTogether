using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041F RID: 1055
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public readonly struct GlobalKeyword
	{
		// Token: 0x060024B9 RID: 9401
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordCount();

		// Token: 0x060024BA RID: 9402
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordIndex(string keyword);

		// Token: 0x060024BB RID: 9403
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGlobalKeyword(string keyword);

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0003E424 File Offset: 0x0003C624
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0003E43C File Offset: 0x0003C63C
		public static GlobalKeyword Create(string name)
		{
			GlobalKeyword.CreateGlobalKeyword(name);
			return new GlobalKeyword(name);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0003E45C File Offset: 0x0003C65C
		public GlobalKeyword(string name)
		{
			this.m_Name = name;
			this.m_Index = GlobalKeyword.GetGlobalKeywordIndex(name);
			bool flag = this.m_Index >= GlobalKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				Debug.LogErrorFormat("Global keyword {0} doesn't exist.", new object[]
				{
					name
				});
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x04000DA4 RID: 3492
		internal readonly string m_Name;

		// Token: 0x04000DA5 RID: 3493
		internal readonly uint m_Index;
	}
}
