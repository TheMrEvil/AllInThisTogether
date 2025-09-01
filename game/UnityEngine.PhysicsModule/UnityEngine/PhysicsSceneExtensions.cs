using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	public static class PhysicsSceneExtensions
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00006098 File Offset: 0x00004298
		public static PhysicsScene GetPhysicsScene(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene physicsScene_Internal = PhysicsSceneExtensions.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000060E8 File Offset: 0x000042E8
		[NativeMethod("GetPhysicsSceneFromUnityScene")]
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		private static PhysicsScene GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene result;
			PhysicsSceneExtensions.GetPhysicsScene_Internal_Injected(ref scene, out result);
			return result;
		}

		// Token: 0x06000433 RID: 1075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene ret);
	}
}
