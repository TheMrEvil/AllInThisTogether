using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	public static class ParticlePhysicsExtensions
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00005BF0 File Offset: 0x00003DF0
		[Obsolete("GetCollisionEvents function using ParticleCollisionEvent[] is deprecated. Use List<ParticleCollisionEvent> instead.", false)]
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new ArgumentNullException("go");
			}
			bool flag2 = collisionEvents == null;
			if (flag2)
			{
				throw new ArgumentNullException("collisionEvents");
			}
			return ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated(ps, go, collisionEvents);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00005C34 File Offset: 0x00003E34
		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00005C4C File Offset: 0x00003E4C
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00005C68 File Offset: 0x00003E68
		public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
		{
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00005C84 File Offset: 0x00003E84
		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, out ParticleSystem.ColliderData colliderData)
		{
			bool flag = type == ParticleSystemTriggerEventType.Exit;
			if (flag)
			{
				throw new InvalidOperationException("Querying the collider data for the Exit event is not currently supported.");
			}
			bool flag2 = type == ParticleSystemTriggerEventType.Outside;
			if (flag2)
			{
				throw new InvalidOperationException("Querying the collider data for the Outside event is not supported, because when a particle is outside the collision volume, it is always outside every collider.");
			}
			colliderData = default(ParticleSystem.ColliderData);
			return ParticleSystemExtensionsImpl.GetTriggerParticlesWithData(ps, (int)type, particles, ref colliderData);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
		{
			bool flag = particles == null;
			if (flag)
			{
				throw new ArgumentNullException("particles");
			}
			bool flag2 = offset >= particles.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
			}
			bool flag3 = offset + count >= particles.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");
			}
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00005D58 File Offset: 0x00003F58
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
		}
	}
}
