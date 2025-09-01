using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00003D88 File Offset: 0x00001F88
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarInternal(go, humanDescription);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00003DB2 File Offset: 0x00001FB2
		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return AvatarBuilder.BuildHumanAvatarInternal_Injected(go, ref humanDescription);
		}

		// Token: 0x06000242 RID: 578
		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Avatar BuildGenericAvatar([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] string rootMotionTransformName);

		// Token: 0x06000243 RID: 579 RVA: 0x000037B9 File Offset: 0x000019B9
		public AvatarBuilder()
		{
		}

		// Token: 0x06000244 RID: 580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Avatar BuildHumanAvatarInternal_Injected(GameObject go, ref HumanDescription humanDescription);
	}
}
