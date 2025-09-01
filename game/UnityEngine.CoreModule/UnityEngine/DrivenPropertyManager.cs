using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001D4 RID: 468
	[NativeHeader("Editor/Src/Properties/DrivenPropertyManager.h")]
	internal class DrivenPropertyManager
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00023149 File Offset: 0x00021349
		[Conditional("UNITY_EDITOR")]
		public static void RegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.RegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00023155 File Offset: 0x00021355
		[Conditional("UNITY_EDITOR")]
		public static void TryRegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.TryRegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00023161 File Offset: 0x00021361
		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.UnregisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x060015D3 RID: 5587
		[Conditional("UNITY_EDITOR")]
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterProperties([NotNull("ArgumentNullException")] Object driver);

		// Token: 0x060015D4 RID: 5588
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);

		// Token: 0x060015D5 RID: 5589
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryRegisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);

		// Token: 0x060015D6 RID: 5590
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);

		// Token: 0x060015D7 RID: 5591 RVA: 0x00002072 File Offset: 0x00000272
		public DrivenPropertyManager()
		{
		}
	}
}
