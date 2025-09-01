using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000062 RID: 98
	[NativeHeader("Modules/Animation/Animator.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorJobExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[StaticAccessor("AnimatorJobExtensionsBindings", StaticAccessorType.DoubleColon)]
	public static class AnimatorJobExtensions
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x000079F0 File Offset: 0x00005BF0
		public static void AddJobDependency(this Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency(animator, jobHandle);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000079FC File Offset: 0x00005BFC
		public static TransformStreamHandle BindStreamTransform(this Animator animator, Transform transform)
		{
			TransformStreamHandle result = default(TransformStreamHandle);
			AnimatorJobExtensions.InternalBindStreamTransform(animator, transform, out result);
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00007A24 File Offset: 0x00005C24
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindStreamProperty(transform, type, property, false);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00007A40 File Offset: 0x00005C40
		public static PropertyStreamHandle BindCustomStreamProperty(this Animator animator, string property, CustomStreamPropertyType type)
		{
			PropertyStreamHandle result = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindCustomStreamProperty(animator, property, type, out result);
			return result;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00007A68 File Offset: 0x00005C68
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertyStreamHandle result = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindStreamProperty(animator, transform, type, property, isObjectReference, out result);
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00007A94 File Offset: 0x00005C94
		public static TransformSceneHandle BindSceneTransform(this Animator animator, Transform transform)
		{
			TransformSceneHandle result = default(TransformSceneHandle);
			AnimatorJobExtensions.InternalBindSceneTransform(animator, transform, out result);
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00007ABC File Offset: 0x00005CBC
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindSceneProperty(transform, type, property, false);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertySceneHandle result = default(PropertySceneHandle);
			AnimatorJobExtensions.InternalBindSceneProperty(animator, transform, type, property, isObjectReference, out result);
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00007B04 File Offset: 0x00005D04
		public static bool OpenAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			return AnimatorJobExtensions.InternalOpenAnimationStream(animator, ref stream);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00007B1D File Offset: 0x00005D1D
		public static void CloseAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			AnimatorJobExtensions.InternalCloseAnimationStream(animator, ref stream);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00007B28 File Offset: 0x00005D28
		public static void ResolveAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllStreamHandles(animator);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00007B32 File Offset: 0x00005D32
		public static void ResolveAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllSceneHandles(animator);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00007B3C File Offset: 0x00005D3C
		internal static void UnbindAllHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllStreamHandles(animator);
			AnimatorJobExtensions.InternalUnbindAllSceneHandles(animator);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00007B4D File Offset: 0x00005D4D
		public static void UnbindAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllStreamHandles(animator);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00007B57 File Offset: 0x00005D57
		public static void UnbindAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllSceneHandles(animator);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00007B61 File Offset: 0x00005D61
		private static void InternalAddJobDependency([NotNull("ArgumentNullException")] Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency_Injected(animator, ref jobHandle);
		}

		// Token: 0x0600056B RID: 1387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformStreamHandle transformStreamHandle);

		// Token: 0x0600056C RID: 1388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x0600056D RID: 1389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindCustomStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] string property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x0600056E RID: 1390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformSceneHandle transformSceneHandle);

		// Token: 0x0600056F RID: 1391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertySceneHandle propertySceneHandle);

		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalOpenAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCloseAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllStreamHandles([NotNull("ArgumentNullException")] Animator animator);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllSceneHandles([NotNull("ArgumentNullException")] Animator animator);

		// Token: 0x06000574 RID: 1396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllStreamHandles([NotNull("ArgumentNullException")] Animator animator);

		// Token: 0x06000575 RID: 1397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllSceneHandles([NotNull("ArgumentNullException")] Animator animator);

		// Token: 0x06000576 RID: 1398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalAddJobDependency_Injected(Animator animator, ref JobHandle jobHandle);
	}
}
