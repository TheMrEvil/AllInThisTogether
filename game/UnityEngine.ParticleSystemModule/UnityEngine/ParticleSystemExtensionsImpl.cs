using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200005A RID: 90
	internal class ParticleSystemExtensionsImpl
	{
		// Token: 0x060006F2 RID: 1778
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSafeCollisionEventSize([NotNull("ArgumentNullException")] ParticleSystem ps);

		// Token: 0x060006F3 RID: 1779
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCollisionEventsDeprecated([NotNull("ArgumentNullException")] ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents);

		// Token: 0x060006F4 RID: 1780
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSafeTriggerParticlesSize([NotNull("ArgumentNullException")] ParticleSystem ps, int type);

		// Token: 0x060006F5 RID: 1781
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCollisionEvents([NotNull("ArgumentNullException")] ParticleSystem ps, [NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] List<ParticleCollisionEvent> collisionEvents);

		// Token: 0x060006F6 RID: 1782
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles);

		// Token: 0x060006F7 RID: 1783
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticlesWithData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTriggerParticlesWithData([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, ref ParticleSystem.ColliderData colliderData);

		// Token: 0x060006F8 RID: 1784
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, int offset, int count);

		// Token: 0x060006F9 RID: 1785 RVA: 0x00006435 File Offset: 0x00004635
		public ParticleSystemExtensionsImpl()
		{
		}
	}
}
