using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public static class PhysicsSceneExtensions2D
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00002EE8 File Offset: 0x000010E8
		public static PhysicsScene2D GetPhysicsScene2D(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene2D physicsScene_Internal = PhysicsSceneExtensions2D.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002F38 File Offset: 0x00001138
		[NativeMethod("GetPhysicsSceneFromUnityScene")]
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		private static PhysicsScene2D GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene2D result;
			PhysicsSceneExtensions2D.GetPhysicsScene_Internal_Injected(ref scene, out result);
			return result;
		}

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene2D ret);
	}
}
