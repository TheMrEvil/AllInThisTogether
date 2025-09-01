using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class ParticleSystem : Component
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5U;
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002104 File Offset: 0x00000304
		[Obsolete("startDelay property is deprecated. Use main.startDelay or main.startDelayMultiplier instead.", false)]
		public float startDelay
		{
			get
			{
				return this.main.startDelayMultiplier;
			}
			set
			{
				this.main.startDelayMultiplier = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		[Obsolete("loop property is deprecated. Use main.loop instead.", false)]
		public bool loop
		{
			get
			{
				return this.main.loop;
			}
			set
			{
				this.main.loop = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002184 File Offset: 0x00000384
		[Obsolete("playOnAwake property is deprecated. Use main.playOnAwake instead.", false)]
		public bool playOnAwake
		{
			get
			{
				return this.main.playOnAwake;
			}
			set
			{
				this.main.playOnAwake = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
		[Obsolete("duration property is deprecated. Use main.duration instead.", false)]
		public float duration
		{
			get
			{
				return this.main.duration;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C4 File Offset: 0x000003C4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021E4 File Offset: 0x000003E4
		[Obsolete("playbackSpeed property is deprecated. Use main.simulationSpeed instead.", false)]
		public float playbackSpeed
		{
			get
			{
				return this.main.simulationSpeed;
			}
			set
			{
				this.main.simulationSpeed = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002224 File Offset: 0x00000424
		[Obsolete("enableEmission property is deprecated. Use emission.enabled instead.", false)]
		public bool enableEmission
		{
			get
			{
				return this.emission.enabled;
			}
			set
			{
				this.emission.enabled = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002264 File Offset: 0x00000464
		[Obsolete("emissionRate property is deprecated. Use emission.rateOverTime, emission.rateOverDistance, emission.rateOverTimeMultiplier or emission.rateOverDistanceMultiplier instead.", false)]
		public float emissionRate
		{
			get
			{
				return this.emission.rateOverTimeMultiplier;
			}
			set
			{
				this.emission.rateOverTime = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002288 File Offset: 0x00000488
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022A8 File Offset: 0x000004A8
		[Obsolete("startSpeed property is deprecated. Use main.startSpeed or main.startSpeedMultiplier instead.", false)]
		public float startSpeed
		{
			get
			{
				return this.main.startSpeedMultiplier;
			}
			set
			{
				this.main.startSpeedMultiplier = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022C8 File Offset: 0x000004C8
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022E8 File Offset: 0x000004E8
		[Obsolete("startSize property is deprecated. Use main.startSize or main.startSizeMultiplier instead.", false)]
		public float startSize
		{
			get
			{
				return this.main.startSizeMultiplier;
			}
			set
			{
				this.main.startSizeMultiplier = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002308 File Offset: 0x00000508
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002330 File Offset: 0x00000530
		[Obsolete("startColor property is deprecated. Use main.startColor instead.", false)]
		public Color startColor
		{
			get
			{
				return this.main.startColor.color;
			}
			set
			{
				this.main.startColor = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002354 File Offset: 0x00000554
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002374 File Offset: 0x00000574
		[Obsolete("startRotation property is deprecated. Use main.startRotation or main.startRotationMultiplier instead.", false)]
		public float startRotation
		{
			get
			{
				return this.main.startRotationMultiplier;
			}
			set
			{
				this.main.startRotationMultiplier = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023D8 File Offset: 0x000005D8
		[Obsolete("startRotation3D property is deprecated. Use main.startRotationX, main.startRotationY and main.startRotationZ instead. (Or main.startRotationXMultiplier, main.startRotationYMultiplier and main.startRotationZMultiplier).", false)]
		public Vector3 startRotation3D
		{
			get
			{
				return new Vector3(this.main.startRotationXMultiplier, this.main.startRotationYMultiplier, this.main.startRotationZMultiplier);
			}
			set
			{
				ParticleSystem.MainModule main = this.main;
				main.startRotationXMultiplier = value.x;
				main.startRotationYMultiplier = value.y;
				main.startRotationZMultiplier = value.z;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002418 File Offset: 0x00000618
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002438 File Offset: 0x00000638
		[Obsolete("startLifetime property is deprecated. Use main.startLifetime or main.startLifetimeMultiplier instead.", false)]
		public float startLifetime
		{
			get
			{
				return this.main.startLifetimeMultiplier;
			}
			set
			{
				this.main.startLifetimeMultiplier = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002478 File Offset: 0x00000678
		[Obsolete("gravityModifier property is deprecated. Use main.gravityModifier or main.gravityModifierMultiplier instead.", false)]
		public float gravityModifier
		{
			get
			{
				return this.main.gravityModifierMultiplier;
			}
			set
			{
				this.main.gravityModifierMultiplier = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024B8 File Offset: 0x000006B8
		[Obsolete("maxParticles property is deprecated. Use main.maxParticles instead.", false)]
		public int maxParticles
		{
			get
			{
				return this.main.maxParticles;
			}
			set
			{
				this.main.maxParticles = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024F8 File Offset: 0x000006F8
		[Obsolete("simulationSpace property is deprecated. Use main.simulationSpace instead.", false)]
		public ParticleSystemSimulationSpace simulationSpace
		{
			get
			{
				return this.main.simulationSpace;
			}
			set
			{
				this.main.simulationSpace = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002518 File Offset: 0x00000718
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002538 File Offset: 0x00000738
		[Obsolete("scalingMode property is deprecated. Use main.scalingMode instead.", false)]
		public ParticleSystemScalingMode scalingMode
		{
			get
			{
				return this.main.scalingMode;
			}
			set
			{
				this.main.scalingMode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002558 File Offset: 0x00000758
		[Obsolete("automaticCullingEnabled property is deprecated. Use proceduralSimulationSupported instead (UnityUpgradable) -> proceduralSimulationSupported", true)]
		public bool automaticCullingEnabled
		{
			get
			{
				return this.proceduralSimulationSupported;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39
		public extern bool isPlaying { [NativeName("SyncJobs(false)->IsPlaying")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40
		public extern bool isEmitting { [NativeName("SyncJobs(false)->IsEmitting")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41
		public extern bool isStopped { [NativeName("SyncJobs(false)->IsStopped")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002A RID: 42
		public extern bool isPaused { [NativeName("SyncJobs(false)->IsPaused")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002B RID: 43
		public extern int particleCount { [NativeName("SyncJobs(false)->GetParticleCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002C RID: 44
		// (set) Token: 0x0600002D RID: 45
		public extern float time { [NativeName("SyncJobs(false)->GetSecPosition")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SyncJobs(false)->SetSecPosition")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002E RID: 46
		// (set) Token: 0x0600002F RID: 47
		public extern uint randomSeed { [NativeName("GetRandomSeed")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SyncJobs(false)->SetRandomSeed")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000030 RID: 48
		// (set) Token: 0x06000031 RID: 49
		public extern bool useAutoRandomSeed { [NativeName("GetAutoRandomSeed")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SyncJobs(false)->SetAutoRandomSeed")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000032 RID: 50
		public extern bool proceduralSimulationSupported { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000033 RID: 51
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetParticleCurrentSize(ref ParticleSystem.Particle particle);

		// Token: 0x06000034 RID: 52 RVA: 0x00002570 File Offset: 0x00000770
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
		internal Vector3 GetParticleCurrentSize3D(ref ParticleSystem.Particle particle)
		{
			Vector3 result;
			this.GetParticleCurrentSize3D_Injected(ref particle, out result);
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002588 File Offset: 0x00000788
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
		internal Color32 GetParticleCurrentColor(ref ParticleSystem.Particle particle)
		{
			Color32 result;
			this.GetParticleCurrentColor_Injected(ref particle, out result);
			return result;
		}

		// Token: 0x06000036 RID: 54
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleMeshIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetParticleMeshIndex(ref ParticleSystem.Particle particle);

		// Token: 0x06000037 RID: 55
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParticles([Out] ParticleSystem.Particle[] particles, int size, int offset);

		// Token: 0x06000038 RID: 56 RVA: 0x0000259F File Offset: 0x0000079F
		public void SetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025AC File Offset: 0x000007AC
		public void SetParticles([Out] ParticleSystem.Particle[] particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x0600003A RID: 58
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x0600003B RID: 59 RVA: 0x000025B8 File Offset: 0x000007B8
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			this.SetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr<ParticleSystem.Particle>(), particles.Length, size, offset);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025D8 File Offset: 0x000007D8
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025E5 File Offset: 0x000007E5
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x0600003E RID: 62
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetParticles([NotNull("ArgumentNullException")] [Out] ParticleSystem.Particle[] particles, int size, int offset);

		// Token: 0x0600003F RID: 63 RVA: 0x000025F4 File Offset: 0x000007F4
		public int GetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002610 File Offset: 0x00000810
		public int GetParticles([Out] ParticleSystem.Particle[] particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x06000041 RID: 65
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x06000042 RID: 66 RVA: 0x0000262C File Offset: 0x0000082C
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			return this.GetParticlesWithNativeArray((IntPtr)particles.GetUnsafePtr<ParticleSystem.Particle>(), particles.Length, size, offset);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000265C File Offset: 0x0000085C
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002678 File Offset: 0x00000878
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x06000045 RID: 69
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		// Token: 0x06000046 RID: 70
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		// Token: 0x06000047 RID: 71 RVA: 0x00002694 File Offset: 0x00000894
		public ParticleSystem.PlaybackState GetPlaybackState()
		{
			ParticleSystem.PlaybackState result;
			this.GetPlaybackState_Injected(out result);
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026AA File Offset: 0x000008AA
		public void SetPlaybackState(ParticleSystem.PlaybackState playbackState)
		{
			this.SetPlaybackState_Injected(ref playbackState);
		}

		// Token: 0x06000049 RID: 73
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTrailData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTrailDataInternal(ref ParticleSystem.Trails trailData);

		// Token: 0x0600004A RID: 74 RVA: 0x000026B4 File Offset: 0x000008B4
		public ParticleSystem.Trails GetTrails()
		{
			ParticleSystem.Trails result = default(ParticleSystem.Trails);
			result.Allocate();
			this.GetTrailDataInternal(ref result);
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000026E0 File Offset: 0x000008E0
		public int GetTrails(ref ParticleSystem.Trails trailData)
		{
			trailData.Allocate();
			this.GetTrailDataInternal(ref trailData);
			return trailData.positions.Count;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000270C File Offset: 0x0000090C
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTrailData", HasExplicitThis = true)]
		public void SetTrails(ParticleSystem.Trails trailData)
		{
			this.SetTrails_Injected(ref trailData);
		}

		// Token: 0x0600004D RID: 77
		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep);

		// Token: 0x0600004E RID: 78 RVA: 0x00002716 File Offset: 0x00000916
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart)
		{
			this.Simulate(t, withChildren, restart, true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002724 File Offset: 0x00000924
		public void Simulate(float t, [DefaultValue("true")] bool withChildren)
		{
			this.Simulate(t, withChildren, true);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002731 File Offset: 0x00000931
		public void Simulate(float t)
		{
			this.Simulate(t, true);
		}

		// Token: 0x06000051 RID: 81
		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play([DefaultValue("true")] bool withChildren);

		// Token: 0x06000052 RID: 82 RVA: 0x0000273D File Offset: 0x0000093D
		public void Play()
		{
			this.Play(true);
		}

		// Token: 0x06000053 RID: 83
		[FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause([DefaultValue("true")] bool withChildren);

		// Token: 0x06000054 RID: 84 RVA: 0x00002748 File Offset: 0x00000948
		public void Pause()
		{
			this.Pause(true);
		}

		// Token: 0x06000055 RID: 85
		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop([DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior);

		// Token: 0x06000056 RID: 86 RVA: 0x00002753 File Offset: 0x00000953
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			this.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000275F File Offset: 0x0000095F
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06000058 RID: 88
		[FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear([DefaultValue("true")] bool withChildren);

		// Token: 0x06000059 RID: 89 RVA: 0x0000276A File Offset: 0x0000096A
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x0600005A RID: 90
		[FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAlive([DefaultValue("true")] bool withChildren);

		// Token: 0x0600005B RID: 91 RVA: 0x00002778 File Offset: 0x00000978
		public bool IsAlive()
		{
			return this.IsAlive(true);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002791 File Offset: 0x00000991
		[RequiredByNativeCode]
		public void Emit(int count)
		{
			this.Emit_Internal(count);
		}

		// Token: 0x0600005D RID: 93
		[NativeName("SyncJobs()->Emit")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit_Internal(int count);

		// Token: 0x0600005E RID: 94 RVA: 0x0000279C File Offset: 0x0000099C
		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			this.Emit_Injected(ref emitParams, count);
		}

		// Token: 0x0600005F RID: 95
		[NativeName("SyncJobs()->EmitParticleExternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EmitOld_Internal(ref ParticleSystem.Particle particle);

		// Token: 0x06000060 RID: 96 RVA: 0x000027A7 File Offset: 0x000009A7
		public void TriggerSubEmitter(int subEmitterIndex)
		{
			this.TriggerSubEmitter(subEmitterIndex, null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000027B3 File Offset: 0x000009B3
		public void TriggerSubEmitter(int subEmitterIndex, ref ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle(subEmitterIndex, particle);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000027C4 File Offset: 0x000009C4
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
		internal void TriggerSubEmitterForParticle(int subEmitterIndex, ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle_Injected(subEmitterIndex, ref particle);
		}

		// Token: 0x06000063 RID: 99
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitter", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TriggerSubEmitter(int subEmitterIndex, List<ParticleSystem.Particle> particles);

		// Token: 0x06000064 RID: 100
		[FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetPreMappedBufferMemory();

		// Token: 0x06000065 RID: 101
		[FreeFunction(Name = "ParticleSystemGeometryJob::SetMaximumPreMappedBufferCounts")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMaximumPreMappedBufferCounts(int vertexBuffersCount, int indexBuffersCount);

		// Token: 0x06000066 RID: 102
		[NativeName("SetUsesAxisOfRotation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AllocateAxisOfRotationAttribute();

		// Token: 0x06000067 RID: 103
		[NativeName("SetUsesMeshIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AllocateMeshIndexAttribute();

		// Token: 0x06000068 RID: 104
		[NativeName("SetUsesCustomData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AllocateCustomDataAttribute(ParticleSystemCustomData stream);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void* GetManagedJobData();

		// Token: 0x0600006A RID: 106 RVA: 0x000027D0 File Offset: 0x000009D0
		internal JobHandle GetManagedJobHandle()
		{
			JobHandle result;
			this.GetManagedJobHandle_Injected(out result);
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000027E6 File Offset: 0x000009E6
		internal void SetManagedJobHandle(JobHandle handle)
		{
			this.SetManagedJobHandle_Injected(ref handle);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000027F0 File Offset: 0x000009F0
		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		internal unsafe static JobHandle ScheduleManagedJob(ref JobsUtility.JobScheduleParameters parameters, void* additionalData)
		{
			JobHandle result;
			ParticleSystem.ScheduleManagedJob_Injected(ref parameters, additionalData, out result);
			return result;
		}

		// Token: 0x0600006D RID: 109
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CopyManagedJobData(void* systemPtr, out NativeParticleData particleData);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002808 File Offset: 0x00000A08
		public ParticleSystem.MainModule main
		{
			get
			{
				return new ParticleSystem.MainModule(this);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002820 File Offset: 0x00000A20
		public ParticleSystem.EmissionModule emission
		{
			get
			{
				return new ParticleSystem.EmissionModule(this);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002838 File Offset: 0x00000A38
		public ParticleSystem.ShapeModule shape
		{
			get
			{
				return new ParticleSystem.ShapeModule(this);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002850 File Offset: 0x00000A50
		public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime
		{
			get
			{
				return new ParticleSystem.VelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002868 File Offset: 0x00000A68
		public ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime
		{
			get
			{
				return new ParticleSystem.LimitVelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002880 File Offset: 0x00000A80
		public ParticleSystem.InheritVelocityModule inheritVelocity
		{
			get
			{
				return new ParticleSystem.InheritVelocityModule(this);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002898 File Offset: 0x00000A98
		public ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed
		{
			get
			{
				return new ParticleSystem.LifetimeByEmitterSpeedModule(this);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000028B0 File Offset: 0x00000AB0
		public ParticleSystem.ForceOverLifetimeModule forceOverLifetime
		{
			get
			{
				return new ParticleSystem.ForceOverLifetimeModule(this);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000028C8 File Offset: 0x00000AC8
		public ParticleSystem.ColorOverLifetimeModule colorOverLifetime
		{
			get
			{
				return new ParticleSystem.ColorOverLifetimeModule(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000028E0 File Offset: 0x00000AE0
		public ParticleSystem.ColorBySpeedModule colorBySpeed
		{
			get
			{
				return new ParticleSystem.ColorBySpeedModule(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000028F8 File Offset: 0x00000AF8
		public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime
		{
			get
			{
				return new ParticleSystem.SizeOverLifetimeModule(this);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002910 File Offset: 0x00000B10
		public ParticleSystem.SizeBySpeedModule sizeBySpeed
		{
			get
			{
				return new ParticleSystem.SizeBySpeedModule(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002928 File Offset: 0x00000B28
		public ParticleSystem.RotationOverLifetimeModule rotationOverLifetime
		{
			get
			{
				return new ParticleSystem.RotationOverLifetimeModule(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002940 File Offset: 0x00000B40
		public ParticleSystem.RotationBySpeedModule rotationBySpeed
		{
			get
			{
				return new ParticleSystem.RotationBySpeedModule(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002958 File Offset: 0x00000B58
		public ParticleSystem.ExternalForcesModule externalForces
		{
			get
			{
				return new ParticleSystem.ExternalForcesModule(this);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002970 File Offset: 0x00000B70
		public ParticleSystem.NoiseModule noise
		{
			get
			{
				return new ParticleSystem.NoiseModule(this);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002988 File Offset: 0x00000B88
		public ParticleSystem.CollisionModule collision
		{
			get
			{
				return new ParticleSystem.CollisionModule(this);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000029A0 File Offset: 0x00000BA0
		public ParticleSystem.TriggerModule trigger
		{
			get
			{
				return new ParticleSystem.TriggerModule(this);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000029B8 File Offset: 0x00000BB8
		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000029D0 File Offset: 0x00000BD0
		public ParticleSystem.TextureSheetAnimationModule textureSheetAnimation
		{
			get
			{
				return new ParticleSystem.TextureSheetAnimationModule(this);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000029E8 File Offset: 0x00000BE8
		public ParticleSystem.LightsModule lights
		{
			get
			{
				return new ParticleSystem.LightsModule(this);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002A00 File Offset: 0x00000C00
		public ParticleSystem.TrailModule trails
		{
			get
			{
				return new ParticleSystem.TrailModule(this);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002A18 File Offset: 0x00000C18
		public ParticleSystem.CustomDataModule customData
		{
			get
			{
				return new ParticleSystem.CustomDataModule(this);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002A30 File Offset: 0x00000C30
		public ParticleSystem()
		{
		}

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentSize3D_Injected(ref ParticleSystem.Particle particle, out Vector3 ret);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentColor_Injected(ref ParticleSystem.Particle particle, out Color32 ret);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPlaybackState_Injected(out ParticleSystem.PlaybackState ret);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlaybackState_Injected(ref ParticleSystem.PlaybackState playbackState);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTrails_Injected(ref ParticleSystem.Trails trailData);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit_Injected(ref ParticleSystem.EmitParams emitParams, int count);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerSubEmitterForParticle_Injected(int subEmitterIndex, ref ParticleSystem.Particle particle);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetManagedJobHandle_Injected(out JobHandle ret);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetManagedJobHandle_Injected(ref JobHandle handle);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleManagedJob_Injected(ref JobsUtility.JobScheduleParameters parameters, void* additionalData, out JobHandle ret);

		// Token: 0x02000006 RID: 6
		public struct MainModule
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00002A3C File Offset: 0x00000C3C
			// (set) Token: 0x06000091 RID: 145 RVA: 0x00002A54 File Offset: 0x00000C54
			[Obsolete("Please use flipRotation instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MainModule.flipRotation", false)]
			public float randomizeRotationDirection
			{
				get
				{
					return this.flipRotation;
				}
				set
				{
					this.flipRotation = value;
				}
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00002A5F File Offset: 0x00000C5F
			internal MainModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00002A6C File Offset: 0x00000C6C
			// (set) Token: 0x06000094 RID: 148 RVA: 0x00002A82 File Offset: 0x00000C82
			public Vector3 emitterVelocity
			{
				get
				{
					Vector3 result;
					ParticleSystem.MainModule.get_emitterVelocity_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_emitterVelocity_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000095 RID: 149 RVA: 0x00002A8C File Offset: 0x00000C8C
			// (set) Token: 0x06000096 RID: 150 RVA: 0x00002A94 File Offset: 0x00000C94
			public float duration
			{
				get
				{
					return ParticleSystem.MainModule.get_duration_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_duration_Injected(ref this, value);
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000097 RID: 151 RVA: 0x00002A9D File Offset: 0x00000C9D
			// (set) Token: 0x06000098 RID: 152 RVA: 0x00002AA5 File Offset: 0x00000CA5
			public bool loop
			{
				get
				{
					return ParticleSystem.MainModule.get_loop_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_loop_Injected(ref this, value);
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000099 RID: 153 RVA: 0x00002AAE File Offset: 0x00000CAE
			// (set) Token: 0x0600009A RID: 154 RVA: 0x00002AB6 File Offset: 0x00000CB6
			public bool prewarm
			{
				get
				{
					return ParticleSystem.MainModule.get_prewarm_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_prewarm_Injected(ref this, value);
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00002AC0 File Offset: 0x00000CC0
			// (set) Token: 0x0600009C RID: 156 RVA: 0x00002AD6 File Offset: 0x00000CD6
			public ParticleSystem.MinMaxCurve startDelay
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startDelay_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelay_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00002AE0 File Offset: 0x00000CE0
			// (set) Token: 0x0600009E RID: 158 RVA: 0x00002AE8 File Offset: 0x00000CE8
			public float startDelayMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startDelayMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelayMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x0600009F RID: 159 RVA: 0x00002AF4 File Offset: 0x00000CF4
			// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002B0A File Offset: 0x00000D0A
			public ParticleSystem.MinMaxCurve startLifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startLifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002B14 File Offset: 0x00000D14
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002B1C File Offset: 0x00000D1C
			public float startLifetimeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startLifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002B28 File Offset: 0x00000D28
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002B3E File Offset: 0x00000D3E
			public ParticleSystem.MinMaxCurve startSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002B48 File Offset: 0x00000D48
			// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002B50 File Offset: 0x00000D50
			public float startSpeedMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002B59 File Offset: 0x00000D59
			// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002B61 File Offset: 0x00000D61
			public bool startSize3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startSize3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize3D_Injected(ref this, value);
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002B6C File Offset: 0x00000D6C
			// (set) Token: 0x060000AA RID: 170 RVA: 0x00002B82 File Offset: 0x00000D82
			[NativeName("StartSizeX")]
			public ParticleSystem.MinMaxCurve startSize
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSize_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00002B8C File Offset: 0x00000D8C
			// (set) Token: 0x060000AC RID: 172 RVA: 0x00002B94 File Offset: 0x00000D94
			[NativeName("StartSizeXMultiplier")]
			public float startSizeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00002BA0 File Offset: 0x00000DA0
			// (set) Token: 0x060000AE RID: 174 RVA: 0x00002BB6 File Offset: 0x00000DB6
			public ParticleSystem.MinMaxCurve startSizeX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeX_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000AF RID: 175 RVA: 0x00002BC0 File Offset: 0x00000DC0
			// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002BC8 File Offset: 0x00000DC8
			public float startSizeXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002BD4 File Offset: 0x00000DD4
			// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002BEA File Offset: 0x00000DEA
			public ParticleSystem.MinMaxCurve startSizeY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002BF4 File Offset: 0x00000DF4
			// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002BFC File Offset: 0x00000DFC
			public float startSizeYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002C08 File Offset: 0x00000E08
			// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002C1E File Offset: 0x00000E1E
			public ParticleSystem.MinMaxCurve startSizeZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C28 File Offset: 0x00000E28
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C30 File Offset: 0x00000E30
			public float startSizeZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C39 File Offset: 0x00000E39
			// (set) Token: 0x060000BA RID: 186 RVA: 0x00002C41 File Offset: 0x00000E41
			public bool startRotation3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotation3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation3D_Injected(ref this, value);
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00002C4C File Offset: 0x00000E4C
			// (set) Token: 0x060000BC RID: 188 RVA: 0x00002C62 File Offset: 0x00000E62
			[NativeName("StartRotationZ")]
			public ParticleSystem.MinMaxCurve startRotation
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotation_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00002C6C File Offset: 0x00000E6C
			// (set) Token: 0x060000BE RID: 190 RVA: 0x00002C74 File Offset: 0x00000E74
			[NativeName("StartRotationZMultiplier")]
			public float startRotationMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00002C80 File Offset: 0x00000E80
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002C96 File Offset: 0x00000E96
			public ParticleSystem.MinMaxCurve startRotationX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002CA0 File Offset: 0x00000EA0
			// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002CA8 File Offset: 0x00000EA8
			public float startRotationXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002CB4 File Offset: 0x00000EB4
			// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002CCA File Offset: 0x00000ECA
			public ParticleSystem.MinMaxCurve startRotationY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002CD4 File Offset: 0x00000ED4
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002CDC File Offset: 0x00000EDC
			public float startRotationYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002CE8 File Offset: 0x00000EE8
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002CFE File Offset: 0x00000EFE
			public ParticleSystem.MinMaxCurve startRotationZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002D08 File Offset: 0x00000F08
			// (set) Token: 0x060000CA RID: 202 RVA: 0x00002D10 File Offset: 0x00000F10
			public float startRotationZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000CB RID: 203 RVA: 0x00002D19 File Offset: 0x00000F19
			// (set) Token: 0x060000CC RID: 204 RVA: 0x00002D21 File Offset: 0x00000F21
			public float flipRotation
			{
				get
				{
					return ParticleSystem.MainModule.get_flipRotation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_flipRotation_Injected(ref this, value);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000CD RID: 205 RVA: 0x00002D2C File Offset: 0x00000F2C
			// (set) Token: 0x060000CE RID: 206 RVA: 0x00002D42 File Offset: 0x00000F42
			public ParticleSystem.MinMaxGradient startColor
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.MainModule.get_startColor_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startColor_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000CF RID: 207 RVA: 0x00002D4C File Offset: 0x00000F4C
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002D62 File Offset: 0x00000F62
			public ParticleSystem.MinMaxCurve gravityModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_gravityModifier_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifier_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002D6C File Offset: 0x00000F6C
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002D74 File Offset: 0x00000F74
			public float gravityModifierMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_gravityModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifierMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002D7D File Offset: 0x00000F7D
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002D85 File Offset: 0x00000F85
			public ParticleSystemSimulationSpace simulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpace_Injected(ref this, value);
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002D8E File Offset: 0x00000F8E
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002D96 File Offset: 0x00000F96
			public Transform customSimulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_customSimulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_customSimulationSpace_Injected(ref this, value);
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002D9F File Offset: 0x00000F9F
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002DA7 File Offset: 0x00000FA7
			public float simulationSpeed
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002DB0 File Offset: 0x00000FB0
			// (set) Token: 0x060000DA RID: 218 RVA: 0x00002DB8 File Offset: 0x00000FB8
			public bool useUnscaledTime
			{
				get
				{
					return ParticleSystem.MainModule.get_useUnscaledTime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_useUnscaledTime_Injected(ref this, value);
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00002DC1 File Offset: 0x00000FC1
			// (set) Token: 0x060000DC RID: 220 RVA: 0x00002DC9 File Offset: 0x00000FC9
			public ParticleSystemScalingMode scalingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_scalingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_scalingMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00002DD2 File Offset: 0x00000FD2
			// (set) Token: 0x060000DE RID: 222 RVA: 0x00002DDA File Offset: 0x00000FDA
			public bool playOnAwake
			{
				get
				{
					return ParticleSystem.MainModule.get_playOnAwake_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_playOnAwake_Injected(ref this, value);
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00002DE3 File Offset: 0x00000FE3
			// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002DEB File Offset: 0x00000FEB
			public int maxParticles
			{
				get
				{
					return ParticleSystem.MainModule.get_maxParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_maxParticles_Injected(ref this, value);
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002DF4 File Offset: 0x00000FF4
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002DFC File Offset: 0x00000FFC
			public ParticleSystemEmitterVelocityMode emitterVelocityMode
			{
				get
				{
					return ParticleSystem.MainModule.get_emitterVelocityMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_emitterVelocityMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002E05 File Offset: 0x00001005
			// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002E0D File Offset: 0x0000100D
			public ParticleSystemStopAction stopAction
			{
				get
				{
					return ParticleSystem.MainModule.get_stopAction_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_stopAction_Injected(ref this, value);
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002E16 File Offset: 0x00001016
			// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002E1E File Offset: 0x0000101E
			public ParticleSystemRingBufferMode ringBufferMode
			{
				get
				{
					return ParticleSystem.MainModule.get_ringBufferMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002E28 File Offset: 0x00001028
			// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002E3E File Offset: 0x0000103E
			public Vector2 ringBufferLoopRange
			{
				get
				{
					Vector2 result;
					ParticleSystem.MainModule.get_ringBufferLoopRange_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferLoopRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002E48 File Offset: 0x00001048
			// (set) Token: 0x060000EA RID: 234 RVA: 0x00002E50 File Offset: 0x00001050
			public ParticleSystemCullingMode cullingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_cullingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_cullingMode_Injected(ref this, value);
				}
			}

			// Token: 0x060000EB RID: 235
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_emitterVelocity_Injected(ref ParticleSystem.MainModule _unity_self, out Vector3 ret);

			// Token: 0x060000EC RID: 236
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_emitterVelocity_Injected(ref ParticleSystem.MainModule _unity_self, ref Vector3 value);

			// Token: 0x060000ED RID: 237
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_duration_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000EE RID: 238
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_duration_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000EF RID: 239
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_loop_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F0 RID: 240
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_loop_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x060000F1 RID: 241
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_prewarm_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F2 RID: 242
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_prewarm_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x060000F3 RID: 243
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000F4 RID: 244
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000F5 RID: 245
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000F6 RID: 246
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000F7 RID: 247
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000F8 RID: 248
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000F9 RID: 249
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000FA RID: 250
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000FB RID: 251
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060000FC RID: 252
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060000FD RID: 253
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x060000FE RID: 254
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x060000FF RID: 255
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000100 RID: 256
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000101 RID: 257
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSize_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000102 RID: 258
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSize_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000103 RID: 259
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000104 RID: 260
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000105 RID: 261
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000106 RID: 262
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000107 RID: 263
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000108 RID: 264
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000109 RID: 265
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600010A RID: 266
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600010B RID: 267
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600010C RID: 268
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600010D RID: 269
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600010E RID: 270
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600010F RID: 271
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000110 RID: 272
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000111 RID: 273
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000112 RID: 274
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000113 RID: 275
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000114 RID: 276
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000115 RID: 277
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000116 RID: 278
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000117 RID: 279
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000118 RID: 280
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000119 RID: 281
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600011A RID: 282
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600011B RID: 283
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600011C RID: 284
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600011D RID: 285
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600011E RID: 286
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600011F RID: 287
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000120 RID: 288
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000121 RID: 289
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000122 RID: 290
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000123 RID: 291
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000124 RID: 292
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000125 RID: 293
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startColor_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x06000126 RID: 294
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startColor_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x06000127 RID: 295
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000128 RID: 296
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000129 RID: 297
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012A RID: 298
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x0600012B RID: 299
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012C RID: 300
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x0600012D RID: 301
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform get_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600012E RID: 302
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, Transform value);

			// Token: 0x0600012F RID: 303
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000130 RID: 304
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			// Token: 0x06000131 RID: 305
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000132 RID: 306
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000133 RID: 307
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemScalingMode get_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000134 RID: 308
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemScalingMode value);

			// Token: 0x06000135 RID: 309
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000136 RID: 310
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			// Token: 0x06000137 RID: 311
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000138 RID: 312
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self, int value);

			// Token: 0x06000139 RID: 313
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemEmitterVelocityMode get_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600013A RID: 314
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemEmitterVelocityMode value);

			// Token: 0x0600013B RID: 315
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemStopAction get_stopAction_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600013C RID: 316
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_stopAction_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemStopAction value);

			// Token: 0x0600013D RID: 317
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemRingBufferMode get_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600013E RID: 318
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemRingBufferMode value);

			// Token: 0x0600013F RID: 319
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, out Vector2 ret);

			// Token: 0x06000140 RID: 320
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, ref Vector2 value);

			// Token: 0x06000141 RID: 321
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCullingMode get_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x06000142 RID: 322
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemCullingMode value);

			// Token: 0x04000004 RID: 4
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000007 RID: 7
		public struct EmissionModule
		{
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00002E5C File Offset: 0x0000105C
			// (set) Token: 0x06000144 RID: 324 RVA: 0x00002E6F File Offset: 0x0000106F
			[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
			public ParticleSystemEmissionType type
			{
				get
				{
					return ParticleSystemEmissionType.Time;
				}
				set
				{
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00002E74 File Offset: 0x00001074
			// (set) Token: 0x06000146 RID: 326 RVA: 0x00002E8C File Offset: 0x0000108C
			[Obsolete("rate property is deprecated. Use rateOverTime or rateOverDistance instead.", false)]
			public ParticleSystem.MinMaxCurve rate
			{
				get
				{
					return this.rateOverTime;
				}
				set
				{
					this.rateOverTime = value;
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00002E98 File Offset: 0x00001098
			// (set) Token: 0x06000148 RID: 328 RVA: 0x00002EB0 File Offset: 0x000010B0
			[Obsolete("rateMultiplier property is deprecated. Use rateOverTimeMultiplier or rateOverDistanceMultiplier instead.", false)]
			public float rateMultiplier
			{
				get
				{
					return this.rateOverTimeMultiplier;
				}
				set
				{
					this.rateOverTimeMultiplier = value;
				}
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00002EBB File Offset: 0x000010BB
			internal EmissionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x0600014A RID: 330 RVA: 0x00002EC5 File Offset: 0x000010C5
			// (set) Token: 0x0600014B RID: 331 RVA: 0x00002ECD File Offset: 0x000010CD
			public bool enabled
			{
				get
				{
					return ParticleSystem.EmissionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x0600014C RID: 332 RVA: 0x00002ED8 File Offset: 0x000010D8
			// (set) Token: 0x0600014D RID: 333 RVA: 0x00002EEE File Offset: 0x000010EE
			public ParticleSystem.MinMaxCurve rateOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.EmissionModule.get_rateOverTime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTime_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x0600014E RID: 334 RVA: 0x00002EF8 File Offset: 0x000010F8
			// (set) Token: 0x0600014F RID: 335 RVA: 0x00002F00 File Offset: 0x00001100
			public float rateOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000150 RID: 336 RVA: 0x00002F0C File Offset: 0x0000110C
			// (set) Token: 0x06000151 RID: 337 RVA: 0x00002F22 File Offset: 0x00001122
			public ParticleSystem.MinMaxCurve rateOverDistance
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.EmissionModule.get_rateOverDistance_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistance_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000152 RID: 338 RVA: 0x00002F2C File Offset: 0x0000112C
			// (set) Token: 0x06000153 RID: 339 RVA: 0x00002F34 File Offset: 0x00001134
			public float rateOverDistanceMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverDistanceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistanceMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00002F3D File Offset: 0x0000113D
			public void SetBursts(ParticleSystem.Burst[] bursts)
			{
				this.SetBursts(bursts, bursts.Length);
			}

			// Token: 0x06000155 RID: 341 RVA: 0x00002F4C File Offset: 0x0000114C
			public void SetBursts(ParticleSystem.Burst[] bursts, int size)
			{
				this.burstCount = size;
				for (int i = 0; i < size; i++)
				{
					this.SetBurst(i, bursts[i]);
				}
			}

			// Token: 0x06000156 RID: 342 RVA: 0x00002F84 File Offset: 0x00001184
			public int GetBursts(ParticleSystem.Burst[] bursts)
			{
				int burstCount = this.burstCount;
				for (int i = 0; i < burstCount; i++)
				{
					bursts[i] = this.GetBurst(i);
				}
				return burstCount;
			}

			// Token: 0x06000157 RID: 343 RVA: 0x00002FBC File Offset: 0x000011BC
			[NativeThrows]
			public void SetBurst(int index, ParticleSystem.Burst burst)
			{
				ParticleSystem.EmissionModule.SetBurst_Injected(ref this, index, ref burst);
			}

			// Token: 0x06000158 RID: 344 RVA: 0x00002FC8 File Offset: 0x000011C8
			[NativeThrows]
			public ParticleSystem.Burst GetBurst(int index)
			{
				ParticleSystem.Burst result;
				ParticleSystem.EmissionModule.GetBurst_Injected(ref this, index, out result);
				return result;
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000159 RID: 345 RVA: 0x00002FDF File Offset: 0x000011DF
			// (set) Token: 0x0600015A RID: 346 RVA: 0x00002FE7 File Offset: 0x000011E7
			public int burstCount
			{
				get
				{
					return ParticleSystem.EmissionModule.get_burstCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_burstCount_Injected(ref this, value);
				}
			}

			// Token: 0x0600015B RID: 347
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x0600015C RID: 348
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self, bool value);

			// Token: 0x0600015D RID: 349
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600015E RID: 350
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600015F RID: 351
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000160 RID: 352
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			// Token: 0x06000161 RID: 353
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000162 RID: 354
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000163 RID: 355
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000164 RID: 356
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			// Token: 0x06000165 RID: 357
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, ref ParticleSystem.Burst burst);

			// Token: 0x06000166 RID: 358
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, out ParticleSystem.Burst ret);

			// Token: 0x06000167 RID: 359
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self);

			// Token: 0x06000168 RID: 360
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self, int value);

			// Token: 0x04000005 RID: 5
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000008 RID: 8
		public struct ShapeModule
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000169 RID: 361 RVA: 0x00002FF0 File Offset: 0x000011F0
			// (set) Token: 0x0600016A RID: 362 RVA: 0x00003008 File Offset: 0x00001208
			[Obsolete("Please use scale instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/ShapeModule.scale", false)]
			public Vector3 box
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.scale = value;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600016B RID: 363 RVA: 0x00003014 File Offset: 0x00001214
			// (set) Token: 0x0600016C RID: 364 RVA: 0x00003031 File Offset: 0x00001231
			[Obsolete("meshScale property is deprecated.Please use scale instead.", false)]
			public float meshScale
			{
				get
				{
					return this.scale.x;
				}
				set
				{
					this.scale = new Vector3(value, value, value);
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00003044 File Offset: 0x00001244
			// (set) Token: 0x0600016E RID: 366 RVA: 0x00003066 File Offset: 0x00001266
			[Obsolete("randomDirection property is deprecated. Use randomDirectionAmount instead.", false)]
			public bool randomDirection
			{
				get
				{
					return this.randomDirectionAmount >= 0.5f;
				}
				set
				{
					this.randomDirectionAmount = (value ? 1f : 0f);
				}
			}

			// Token: 0x0600016F RID: 367 RVA: 0x0000307F File Offset: 0x0000127F
			internal ShapeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000170 RID: 368 RVA: 0x00003089 File Offset: 0x00001289
			// (set) Token: 0x06000171 RID: 369 RVA: 0x00003091 File Offset: 0x00001291
			public bool enabled
			{
				get
				{
					return ParticleSystem.ShapeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000172 RID: 370 RVA: 0x0000309A File Offset: 0x0000129A
			// (set) Token: 0x06000173 RID: 371 RVA: 0x000030A2 File Offset: 0x000012A2
			public ParticleSystemShapeType shapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_shapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_shapeType_Injected(ref this, value);
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000174 RID: 372 RVA: 0x000030AB File Offset: 0x000012AB
			// (set) Token: 0x06000175 RID: 373 RVA: 0x000030B3 File Offset: 0x000012B3
			public float randomDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomDirectionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000176 RID: 374 RVA: 0x000030BC File Offset: 0x000012BC
			// (set) Token: 0x06000177 RID: 375 RVA: 0x000030C4 File Offset: 0x000012C4
			public float sphericalDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sphericalDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sphericalDirectionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000178 RID: 376 RVA: 0x000030CD File Offset: 0x000012CD
			// (set) Token: 0x06000179 RID: 377 RVA: 0x000030D5 File Offset: 0x000012D5
			public float randomPositionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomPositionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomPositionAmount_Injected(ref this, value);
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x0600017A RID: 378 RVA: 0x000030DE File Offset: 0x000012DE
			// (set) Token: 0x0600017B RID: 379 RVA: 0x000030E6 File Offset: 0x000012E6
			public bool alignToDirection
			{
				get
				{
					return ParticleSystem.ShapeModule.get_alignToDirection_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_alignToDirection_Injected(ref this, value);
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x0600017C RID: 380 RVA: 0x000030EF File Offset: 0x000012EF
			// (set) Token: 0x0600017D RID: 381 RVA: 0x000030F7 File Offset: 0x000012F7
			public float radius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radius_Injected(ref this, value);
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600017E RID: 382 RVA: 0x00003100 File Offset: 0x00001300
			// (set) Token: 0x0600017F RID: 383 RVA: 0x00003108 File Offset: 0x00001308
			public ParticleSystemShapeMultiModeValue radiusMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000180 RID: 384 RVA: 0x00003111 File Offset: 0x00001311
			// (set) Token: 0x06000181 RID: 385 RVA: 0x00003119 File Offset: 0x00001319
			public float radiusSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpread_Injected(ref this, value);
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000182 RID: 386 RVA: 0x00003124 File Offset: 0x00001324
			// (set) Token: 0x06000183 RID: 387 RVA: 0x0000313A File Offset: 0x0000133A
			public ParticleSystem.MinMaxCurve radiusSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_radiusSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000184 RID: 388 RVA: 0x00003144 File Offset: 0x00001344
			// (set) Token: 0x06000185 RID: 389 RVA: 0x0000314C File Offset: 0x0000134C
			public float radiusSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000186 RID: 390 RVA: 0x00003155 File Offset: 0x00001355
			// (set) Token: 0x06000187 RID: 391 RVA: 0x0000315D File Offset: 0x0000135D
			public float radiusThickness
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusThickness_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusThickness_Injected(ref this, value);
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000188 RID: 392 RVA: 0x00003166 File Offset: 0x00001366
			// (set) Token: 0x06000189 RID: 393 RVA: 0x0000316E File Offset: 0x0000136E
			public float angle
			{
				get
				{
					return ParticleSystem.ShapeModule.get_angle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_angle_Injected(ref this, value);
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x0600018A RID: 394 RVA: 0x00003177 File Offset: 0x00001377
			// (set) Token: 0x0600018B RID: 395 RVA: 0x0000317F File Offset: 0x0000137F
			public float length
			{
				get
				{
					return ParticleSystem.ShapeModule.get_length_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_length_Injected(ref this, value);
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600018C RID: 396 RVA: 0x00003188 File Offset: 0x00001388
			// (set) Token: 0x0600018D RID: 397 RVA: 0x0000319E File Offset: 0x0000139E
			public Vector3 boxThickness
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_boxThickness_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_boxThickness_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600018E RID: 398 RVA: 0x000031A8 File Offset: 0x000013A8
			// (set) Token: 0x0600018F RID: 399 RVA: 0x000031B0 File Offset: 0x000013B0
			public ParticleSystemMeshShapeType meshShapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshShapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshShapeType_Injected(ref this, value);
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000190 RID: 400 RVA: 0x000031B9 File Offset: 0x000013B9
			// (set) Token: 0x06000191 RID: 401 RVA: 0x000031C1 File Offset: 0x000013C1
			public Mesh mesh
			{
				get
				{
					return ParticleSystem.ShapeModule.get_mesh_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_mesh_Injected(ref this, value);
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000192 RID: 402 RVA: 0x000031CA File Offset: 0x000013CA
			// (set) Token: 0x06000193 RID: 403 RVA: 0x000031D2 File Offset: 0x000013D2
			public MeshRenderer meshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000194 RID: 404 RVA: 0x000031DB File Offset: 0x000013DB
			// (set) Token: 0x06000195 RID: 405 RVA: 0x000031E3 File Offset: 0x000013E3
			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_skinnedMeshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_skinnedMeshRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000196 RID: 406 RVA: 0x000031EC File Offset: 0x000013EC
			// (set) Token: 0x06000197 RID: 407 RVA: 0x000031F4 File Offset: 0x000013F4
			public Sprite sprite
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sprite_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sprite_Injected(ref this, value);
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000198 RID: 408 RVA: 0x000031FD File Offset: 0x000013FD
			// (set) Token: 0x06000199 RID: 409 RVA: 0x00003205 File Offset: 0x00001405
			public SpriteRenderer spriteRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_spriteRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_spriteRenderer_Injected(ref this, value);
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600019A RID: 410 RVA: 0x0000320E File Offset: 0x0000140E
			// (set) Token: 0x0600019B RID: 411 RVA: 0x00003216 File Offset: 0x00001416
			public bool useMeshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshMaterialIndex_Injected(ref this, value);
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600019C RID: 412 RVA: 0x0000321F File Offset: 0x0000141F
			// (set) Token: 0x0600019D RID: 413 RVA: 0x00003227 File Offset: 0x00001427
			public int meshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshMaterialIndex_Injected(ref this, value);
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x0600019E RID: 414 RVA: 0x00003230 File Offset: 0x00001430
			// (set) Token: 0x0600019F RID: 415 RVA: 0x00003238 File Offset: 0x00001438
			public bool useMeshColors
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshColors_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshColors_Injected(ref this, value);
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003241 File Offset: 0x00001441
			// (set) Token: 0x060001A1 RID: 417 RVA: 0x00003249 File Offset: 0x00001449
			public float normalOffset
			{
				get
				{
					return ParticleSystem.ShapeModule.get_normalOffset_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_normalOffset_Injected(ref this, value);
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003252 File Offset: 0x00001452
			// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000325A File Offset: 0x0000145A
			public ParticleSystemShapeMultiModeValue meshSpawnMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnMode_Injected(ref this, value);
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003263 File Offset: 0x00001463
			// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000326B File Offset: 0x0000146B
			public float meshSpawnSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpread_Injected(ref this, value);
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003274 File Offset: 0x00001474
			// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000328A File Offset: 0x0000148A
			public ParticleSystem.MinMaxCurve meshSpawnSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_meshSpawnSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003294 File Offset: 0x00001494
			// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000329C File Offset: 0x0000149C
			public float meshSpawnSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001AA RID: 426 RVA: 0x000032A5 File Offset: 0x000014A5
			// (set) Token: 0x060001AB RID: 427 RVA: 0x000032AD File Offset: 0x000014AD
			public float arc
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arc_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arc_Injected(ref this, value);
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001AC RID: 428 RVA: 0x000032B6 File Offset: 0x000014B6
			// (set) Token: 0x060001AD RID: 429 RVA: 0x000032BE File Offset: 0x000014BE
			public ParticleSystemShapeMultiModeValue arcMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcMode_Injected(ref this, value);
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001AE RID: 430 RVA: 0x000032C7 File Offset: 0x000014C7
			// (set) Token: 0x060001AF RID: 431 RVA: 0x000032CF File Offset: 0x000014CF
			public float arcSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpread_Injected(ref this, value);
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x000032D8 File Offset: 0x000014D8
			// (set) Token: 0x060001B1 RID: 433 RVA: 0x000032EE File Offset: 0x000014EE
			public ParticleSystem.MinMaxCurve arcSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_arcSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x000032F8 File Offset: 0x000014F8
			// (set) Token: 0x060001B3 RID: 435 RVA: 0x00003300 File Offset: 0x00001500
			public float arcSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003309 File Offset: 0x00001509
			// (set) Token: 0x060001B5 RID: 437 RVA: 0x00003311 File Offset: 0x00001511
			public float donutRadius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_donutRadius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_donutRadius_Injected(ref this, value);
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000331C File Offset: 0x0000151C
			// (set) Token: 0x060001B7 RID: 439 RVA: 0x00003332 File Offset: 0x00001532
			public Vector3 position
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_position_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_position_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000333C File Offset: 0x0000153C
			// (set) Token: 0x060001B9 RID: 441 RVA: 0x00003352 File Offset: 0x00001552
			public Vector3 rotation
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_rotation_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_rotation_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060001BA RID: 442 RVA: 0x0000335C File Offset: 0x0000155C
			// (set) Token: 0x060001BB RID: 443 RVA: 0x00003372 File Offset: 0x00001572
			public Vector3 scale
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_scale_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_scale_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060001BC RID: 444 RVA: 0x0000337C File Offset: 0x0000157C
			// (set) Token: 0x060001BD RID: 445 RVA: 0x00003384 File Offset: 0x00001584
			public Texture2D texture
			{
				get
				{
					return ParticleSystem.ShapeModule.get_texture_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_texture_Injected(ref this, value);
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060001BE RID: 446 RVA: 0x0000338D File Offset: 0x0000158D
			// (set) Token: 0x060001BF RID: 447 RVA: 0x00003395 File Offset: 0x00001595
			public ParticleSystemShapeTextureChannel textureClipChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipChannel_Injected(ref this, value);
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000339E File Offset: 0x0000159E
			// (set) Token: 0x060001C1 RID: 449 RVA: 0x000033A6 File Offset: 0x000015A6
			public float textureClipThreshold
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipThreshold_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipThreshold_Injected(ref this, value);
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x000033AF File Offset: 0x000015AF
			// (set) Token: 0x060001C3 RID: 451 RVA: 0x000033B7 File Offset: 0x000015B7
			public bool textureColorAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureColorAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureColorAffectsParticles_Injected(ref this, value);
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x000033C0 File Offset: 0x000015C0
			// (set) Token: 0x060001C5 RID: 453 RVA: 0x000033C8 File Offset: 0x000015C8
			public bool textureAlphaAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureAlphaAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureAlphaAffectsParticles_Injected(ref this, value);
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x000033D1 File Offset: 0x000015D1
			// (set) Token: 0x060001C7 RID: 455 RVA: 0x000033D9 File Offset: 0x000015D9
			public bool textureBilinearFiltering
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureBilinearFiltering_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureBilinearFiltering_Injected(ref this, value);
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x000033E2 File Offset: 0x000015E2
			// (set) Token: 0x060001C9 RID: 457 RVA: 0x000033EA File Offset: 0x000015EA
			public int textureUVChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureUVChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureUVChannel_Injected(ref this, value);
				}
			}

			// Token: 0x060001CA RID: 458
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CB RID: 459
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001CC RID: 460
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeType get_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CD RID: 461
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeType value);

			// Token: 0x060001CE RID: 462
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001CF RID: 463
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D0 RID: 464
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D1 RID: 465
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D2 RID: 466
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D3 RID: 467
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D4 RID: 468
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D5 RID: 469
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001D6 RID: 470
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D7 RID: 471
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001D8 RID: 472
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001D9 RID: 473
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x060001DA RID: 474
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001DB RID: 475
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001DC RID: 476
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060001DD RID: 477
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060001DE RID: 478
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001DF RID: 479
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001E0 RID: 480
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E1 RID: 481
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001E2 RID: 482
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_angle_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E3 RID: 483
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_angle_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001E4 RID: 484
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_length_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E5 RID: 485
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_length_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001E6 RID: 486
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001E7 RID: 487
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x060001E8 RID: 488
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemMeshShapeType get_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E9 RID: 489
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemMeshShapeType value);

			// Token: 0x060001EA RID: 490
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Mesh get_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001EB RID: 491
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self, Mesh value);

			// Token: 0x060001EC RID: 492
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern MeshRenderer get_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001ED RID: 493
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, MeshRenderer value);

			// Token: 0x060001EE RID: 494
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern SkinnedMeshRenderer get_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001EF RID: 495
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SkinnedMeshRenderer value);

			// Token: 0x060001F0 RID: 496
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Sprite get_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F1 RID: 497
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self, Sprite value);

			// Token: 0x060001F2 RID: 498
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern SpriteRenderer get_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F3 RID: 499
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SpriteRenderer value);

			// Token: 0x060001F4 RID: 500
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F5 RID: 501
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001F6 RID: 502
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F7 RID: 503
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);

			// Token: 0x060001F8 RID: 504
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F9 RID: 505
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x060001FA RID: 506
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FB RID: 507
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x060001FC RID: 508
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FD RID: 509
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x060001FE RID: 510
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001FF RID: 511
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000200 RID: 512
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000201 RID: 513
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000202 RID: 514
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000203 RID: 515
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000204 RID: 516
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arc_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000205 RID: 517
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arc_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000206 RID: 518
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000207 RID: 519
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			// Token: 0x06000208 RID: 520
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000209 RID: 521
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x0600020A RID: 522
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600020B RID: 523
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600020C RID: 524
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600020D RID: 525
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x0600020E RID: 526
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600020F RID: 527
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x06000210 RID: 528
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_position_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x06000211 RID: 529
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_position_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x06000212 RID: 530
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x06000213 RID: 531
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x06000214 RID: 532
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x06000215 RID: 533
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			// Token: 0x06000216 RID: 534
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Texture2D get_texture_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000217 RID: 535
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_texture_Injected(ref ParticleSystem.ShapeModule _unity_self, Texture2D value);

			// Token: 0x06000218 RID: 536
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeTextureChannel get_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000219 RID: 537
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeTextureChannel value);

			// Token: 0x0600021A RID: 538
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600021B RID: 539
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			// Token: 0x0600021C RID: 540
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600021D RID: 541
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x0600021E RID: 542
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x0600021F RID: 543
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x06000220 RID: 544
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000221 RID: 545
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			// Token: 0x06000222 RID: 546
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x06000223 RID: 547
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);

			// Token: 0x04000006 RID: 6
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000009 RID: 9
		public struct CollisionModule
		{
			// Token: 0x17000099 RID: 153
			// (get) Token: 0x06000224 RID: 548 RVA: 0x000033F4 File Offset: 0x000015F4
			[Obsolete("The maxPlaneCount restriction has been removed. Please use planeCount instead to find out how many planes there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/CollisionModule.planeCount", false)]
			public int maxPlaneCount
			{
				get
				{
					return this.planeCount;
				}
			}

			// Token: 0x06000225 RID: 549 RVA: 0x0000340C File Offset: 0x0000160C
			internal CollisionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000226 RID: 550 RVA: 0x00003416 File Offset: 0x00001616
			// (set) Token: 0x06000227 RID: 551 RVA: 0x0000341E File Offset: 0x0000161E
			public bool enabled
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000228 RID: 552 RVA: 0x00003427 File Offset: 0x00001627
			// (set) Token: 0x06000229 RID: 553 RVA: 0x0000342F File Offset: 0x0000162F
			public ParticleSystemCollisionType type
			{
				get
				{
					return ParticleSystem.CollisionModule.get_type_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_type_Injected(ref this, value);
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x0600022A RID: 554 RVA: 0x00003438 File Offset: 0x00001638
			// (set) Token: 0x0600022B RID: 555 RVA: 0x00003440 File Offset: 0x00001640
			public ParticleSystemCollisionMode mode
			{
				get
				{
					return ParticleSystem.CollisionModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x0600022C RID: 556 RVA: 0x0000344C File Offset: 0x0000164C
			// (set) Token: 0x0600022D RID: 557 RVA: 0x00003462 File Offset: 0x00001662
			public ParticleSystem.MinMaxCurve dampen
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_dampen_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampen_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0000346C File Offset: 0x0000166C
			// (set) Token: 0x0600022F RID: 559 RVA: 0x00003474 File Offset: 0x00001674
			public float dampenMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_dampenMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampenMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000230 RID: 560 RVA: 0x00003480 File Offset: 0x00001680
			// (set) Token: 0x06000231 RID: 561 RVA: 0x00003496 File Offset: 0x00001696
			public ParticleSystem.MinMaxCurve bounce
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_bounce_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounce_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000232 RID: 562 RVA: 0x000034A0 File Offset: 0x000016A0
			// (set) Token: 0x06000233 RID: 563 RVA: 0x000034A8 File Offset: 0x000016A8
			public float bounceMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_bounceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounceMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000234 RID: 564 RVA: 0x000034B4 File Offset: 0x000016B4
			// (set) Token: 0x06000235 RID: 565 RVA: 0x000034CA File Offset: 0x000016CA
			public ParticleSystem.MinMaxCurve lifetimeLoss
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_lifetimeLoss_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLoss_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000236 RID: 566 RVA: 0x000034D4 File Offset: 0x000016D4
			// (set) Token: 0x06000237 RID: 567 RVA: 0x000034DC File Offset: 0x000016DC
			public float lifetimeLossMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_lifetimeLossMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLossMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000238 RID: 568 RVA: 0x000034E5 File Offset: 0x000016E5
			// (set) Token: 0x06000239 RID: 569 RVA: 0x000034ED File Offset: 0x000016ED
			public float minKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_minKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_minKillSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x0600023A RID: 570 RVA: 0x000034F6 File Offset: 0x000016F6
			// (set) Token: 0x0600023B RID: 571 RVA: 0x000034FE File Offset: 0x000016FE
			public float maxKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxKillSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x0600023C RID: 572 RVA: 0x00003508 File Offset: 0x00001708
			// (set) Token: 0x0600023D RID: 573 RVA: 0x0000351E File Offset: 0x0000171E
			public LayerMask collidesWith
			{
				get
				{
					LayerMask result;
					ParticleSystem.CollisionModule.get_collidesWith_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_collidesWith_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600023E RID: 574 RVA: 0x00003528 File Offset: 0x00001728
			// (set) Token: 0x0600023F RID: 575 RVA: 0x00003530 File Offset: 0x00001730
			public bool enableDynamicColliders
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableDynamicColliders_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableDynamicColliders_Injected(ref this, value);
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x06000240 RID: 576 RVA: 0x00003539 File Offset: 0x00001739
			// (set) Token: 0x06000241 RID: 577 RVA: 0x00003541 File Offset: 0x00001741
			public int maxCollisionShapes
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxCollisionShapes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxCollisionShapes_Injected(ref this, value);
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000242 RID: 578 RVA: 0x0000354A File Offset: 0x0000174A
			// (set) Token: 0x06000243 RID: 579 RVA: 0x00003552 File Offset: 0x00001752
			public ParticleSystemCollisionQuality quality
			{
				get
				{
					return ParticleSystem.CollisionModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_quality_Injected(ref this, value);
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000244 RID: 580 RVA: 0x0000355B File Offset: 0x0000175B
			// (set) Token: 0x06000245 RID: 581 RVA: 0x00003563 File Offset: 0x00001763
			public float voxelSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_voxelSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_voxelSize_Injected(ref this, value);
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000246 RID: 582 RVA: 0x0000356C File Offset: 0x0000176C
			// (set) Token: 0x06000247 RID: 583 RVA: 0x00003574 File Offset: 0x00001774
			public float radiusScale
			{
				get
				{
					return ParticleSystem.CollisionModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_radiusScale_Injected(ref this, value);
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x06000248 RID: 584 RVA: 0x0000357D File Offset: 0x0000177D
			// (set) Token: 0x06000249 RID: 585 RVA: 0x00003585 File Offset: 0x00001785
			public bool sendCollisionMessages
			{
				get
				{
					return ParticleSystem.CollisionModule.get_sendCollisionMessages_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_sendCollisionMessages_Injected(ref this, value);
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x0600024A RID: 586 RVA: 0x0000358E File Offset: 0x0000178E
			// (set) Token: 0x0600024B RID: 587 RVA: 0x00003596 File Offset: 0x00001796
			public float colliderForce
			{
				get
				{
					return ParticleSystem.CollisionModule.get_colliderForce_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_colliderForce_Injected(ref this, value);
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x0600024C RID: 588 RVA: 0x0000359F File Offset: 0x0000179F
			// (set) Token: 0x0600024D RID: 589 RVA: 0x000035A7 File Offset: 0x000017A7
			public bool multiplyColliderForceByCollisionAngle
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByCollisionAngle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByCollisionAngle_Injected(ref this, value);
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x0600024E RID: 590 RVA: 0x000035B0 File Offset: 0x000017B0
			// (set) Token: 0x0600024F RID: 591 RVA: 0x000035B8 File Offset: 0x000017B8
			public bool multiplyColliderForceByParticleSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSpeed_Injected(ref this, value);
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000250 RID: 592 RVA: 0x000035C1 File Offset: 0x000017C1
			// (set) Token: 0x06000251 RID: 593 RVA: 0x000035C9 File Offset: 0x000017C9
			public bool multiplyColliderForceByParticleSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSize_Injected(ref this, value);
				}
			}

			// Token: 0x06000252 RID: 594 RVA: 0x000035D2 File Offset: 0x000017D2
			[NativeThrows]
			public void AddPlane(Transform transform)
			{
				ParticleSystem.CollisionModule.AddPlane_Injected(ref this, transform);
			}

			// Token: 0x06000253 RID: 595 RVA: 0x000035DB File Offset: 0x000017DB
			[NativeThrows]
			public void RemovePlane(int index)
			{
				ParticleSystem.CollisionModule.RemovePlane_Injected(ref this, index);
			}

			// Token: 0x06000254 RID: 596 RVA: 0x000035E4 File Offset: 0x000017E4
			public void RemovePlane(Transform transform)
			{
				this.RemovePlaneObject(transform);
			}

			// Token: 0x06000255 RID: 597 RVA: 0x000035EF File Offset: 0x000017EF
			[NativeThrows]
			private void RemovePlaneObject(Transform transform)
			{
				ParticleSystem.CollisionModule.RemovePlaneObject_Injected(ref this, transform);
			}

			// Token: 0x06000256 RID: 598 RVA: 0x000035F8 File Offset: 0x000017F8
			[NativeThrows]
			public void SetPlane(int index, Transform transform)
			{
				ParticleSystem.CollisionModule.SetPlane_Injected(ref this, index, transform);
			}

			// Token: 0x06000257 RID: 599 RVA: 0x00003602 File Offset: 0x00001802
			[NativeThrows]
			public Transform GetPlane(int index)
			{
				return ParticleSystem.CollisionModule.GetPlane_Injected(ref this, index);
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000258 RID: 600 RVA: 0x0000360B File Offset: 0x0000180B
			[NativeThrows]
			public int planeCount
			{
				get
				{
					return ParticleSystem.CollisionModule.get_planeCount_Injected(ref this);
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000259 RID: 601 RVA: 0x00003613 File Offset: 0x00001813
			// (set) Token: 0x0600025A RID: 602 RVA: 0x0000361B File Offset: 0x0000181B
			[Obsolete("enableInteriorCollisions property is deprecated and is no longer required and has no effect on the particle system.", false)]
			public bool enableInteriorCollisions
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableInteriorCollisions_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableInteriorCollisions_Injected(ref this, value);
				}
			}

			// Token: 0x0600025B RID: 603
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600025C RID: 604
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x0600025D RID: 605
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionType get_type_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600025E RID: 606
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_type_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionType value);

			// Token: 0x0600025F RID: 607
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionMode get_mode_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000260 RID: 608
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionMode value);

			// Token: 0x06000261 RID: 609
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000262 RID: 610
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000263 RID: 611
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000264 RID: 612
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x06000265 RID: 613
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000266 RID: 614
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000267 RID: 615
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000268 RID: 616
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x06000269 RID: 617
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600026A RID: 618
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600026B RID: 619
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600026C RID: 620
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x0600026D RID: 621
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600026E RID: 622
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x0600026F RID: 623
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000270 RID: 624
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x06000271 RID: 625
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, out LayerMask ret);

			// Token: 0x06000272 RID: 626
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, ref LayerMask value);

			// Token: 0x06000273 RID: 627
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000274 RID: 628
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x06000275 RID: 629
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000276 RID: 630
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self, int value);

			// Token: 0x06000277 RID: 631
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionQuality get_quality_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000278 RID: 632
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_quality_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionQuality value);

			// Token: 0x06000279 RID: 633
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600027A RID: 634
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x0600027B RID: 635
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600027C RID: 636
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x0600027D RID: 637
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600027E RID: 638
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x0600027F RID: 639
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000280 RID: 640
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			// Token: 0x06000281 RID: 641
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000282 RID: 642
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x06000283 RID: 643
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000284 RID: 644
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x06000285 RID: 645
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x06000286 RID: 646
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x06000287 RID: 647
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, Transform transform);

			// Token: 0x06000288 RID: 648
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			// Token: 0x06000289 RID: 649
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlaneObject_Injected(ref ParticleSystem.CollisionModule _unity_self, Transform transform);

			// Token: 0x0600028A RID: 650
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index, Transform transform);

			// Token: 0x0600028B RID: 651
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform GetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			// Token: 0x0600028C RID: 652
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_planeCount_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600028D RID: 653
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self);

			// Token: 0x0600028E RID: 654
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			// Token: 0x04000007 RID: 7
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000A RID: 10
		public struct TriggerModule
		{
			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x0600028F RID: 655 RVA: 0x00003624 File Offset: 0x00001824
			[Obsolete("The maxColliderCount restriction has been removed. Please use colliderCount instead to find out how many colliders there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/TriggerModule.colliderCount", false)]
			public int maxColliderCount
			{
				get
				{
					return this.colliderCount;
				}
			}

			// Token: 0x06000290 RID: 656 RVA: 0x0000363C File Offset: 0x0000183C
			internal TriggerModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000291 RID: 657 RVA: 0x00003646 File Offset: 0x00001846
			// (set) Token: 0x06000292 RID: 658 RVA: 0x0000364E File Offset: 0x0000184E
			public bool enabled
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00003657 File Offset: 0x00001857
			// (set) Token: 0x06000294 RID: 660 RVA: 0x0000365F File Offset: 0x0000185F
			public ParticleSystemOverlapAction inside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_inside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_inside_Injected(ref this, value);
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000295 RID: 661 RVA: 0x00003668 File Offset: 0x00001868
			// (set) Token: 0x06000296 RID: 662 RVA: 0x00003670 File Offset: 0x00001870
			public ParticleSystemOverlapAction outside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_outside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_outside_Injected(ref this, value);
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000297 RID: 663 RVA: 0x00003679 File Offset: 0x00001879
			// (set) Token: 0x06000298 RID: 664 RVA: 0x00003681 File Offset: 0x00001881
			public ParticleSystemOverlapAction enter
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enter_Injected(ref this, value);
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000299 RID: 665 RVA: 0x0000368A File Offset: 0x0000188A
			// (set) Token: 0x0600029A RID: 666 RVA: 0x00003692 File Offset: 0x00001892
			public ParticleSystemOverlapAction exit
			{
				get
				{
					return ParticleSystem.TriggerModule.get_exit_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_exit_Injected(ref this, value);
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600029B RID: 667 RVA: 0x0000369B File Offset: 0x0000189B
			// (set) Token: 0x0600029C RID: 668 RVA: 0x000036A3 File Offset: 0x000018A3
			public ParticleSystemColliderQueryMode colliderQueryMode
			{
				get
				{
					return ParticleSystem.TriggerModule.get_colliderQueryMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_colliderQueryMode_Injected(ref this, value);
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600029D RID: 669 RVA: 0x000036AC File Offset: 0x000018AC
			// (set) Token: 0x0600029E RID: 670 RVA: 0x000036B4 File Offset: 0x000018B4
			public float radiusScale
			{
				get
				{
					return ParticleSystem.TriggerModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_radiusScale_Injected(ref this, value);
				}
			}

			// Token: 0x0600029F RID: 671 RVA: 0x000036BD File Offset: 0x000018BD
			[NativeThrows]
			public void AddCollider(Component collider)
			{
				ParticleSystem.TriggerModule.AddCollider_Injected(ref this, collider);
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x000036C6 File Offset: 0x000018C6
			[NativeThrows]
			public void RemoveCollider(int index)
			{
				ParticleSystem.TriggerModule.RemoveCollider_Injected(ref this, index);
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x000036CF File Offset: 0x000018CF
			public void RemoveCollider(Component collider)
			{
				this.RemoveColliderObject(collider);
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x000036DA File Offset: 0x000018DA
			[NativeThrows]
			private void RemoveColliderObject(Component collider)
			{
				ParticleSystem.TriggerModule.RemoveColliderObject_Injected(ref this, collider);
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x000036E3 File Offset: 0x000018E3
			[NativeThrows]
			public void SetCollider(int index, Component collider)
			{
				ParticleSystem.TriggerModule.SetCollider_Injected(ref this, index, collider);
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x000036ED File Offset: 0x000018ED
			[NativeThrows]
			public Component GetCollider(int index)
			{
				return ParticleSystem.TriggerModule.GetCollider_Injected(ref this, index);
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x000036F6 File Offset: 0x000018F6
			[NativeThrows]
			public int colliderCount
			{
				get
				{
					return ParticleSystem.TriggerModule.get_colliderCount_Injected(ref this);
				}
			}

			// Token: 0x060002A6 RID: 678
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002A7 RID: 679
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self, bool value);

			// Token: 0x060002A8 RID: 680
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_inside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002A9 RID: 681
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_inside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060002AA RID: 682
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_outside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002AB RID: 683
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_outside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060002AC RID: 684
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_enter_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002AD RID: 685
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enter_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060002AE RID: 686
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_exit_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002AF RID: 687
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_exit_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			// Token: 0x060002B0 RID: 688
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemColliderQueryMode get_colliderQueryMode_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002B1 RID: 689
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colliderQueryMode_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemColliderQueryMode value);

			// Token: 0x060002B2 RID: 690
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x060002B3 RID: 691
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self, float value);

			// Token: 0x060002B4 RID: 692
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, Component collider);

			// Token: 0x060002B5 RID: 693
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			// Token: 0x060002B6 RID: 694
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveColliderObject_Injected(ref ParticleSystem.TriggerModule _unity_self, Component collider);

			// Token: 0x060002B7 RID: 695
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index, Component collider);

			// Token: 0x060002B8 RID: 696
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Component GetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			// Token: 0x060002B9 RID: 697
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_colliderCount_Injected(ref ParticleSystem.TriggerModule _unity_self);

			// Token: 0x04000008 RID: 8
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000B RID: 11
		public struct SubEmittersModule
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002BA RID: 698 RVA: 0x00003700 File Offset: 0x00001900
			// (set) Token: 0x060002BB RID: 699 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("birth0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002BC RID: 700 RVA: 0x00003724 File Offset: 0x00001924
			// (set) Token: 0x060002BD RID: 701 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("birth1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002BE RID: 702 RVA: 0x00003740 File Offset: 0x00001940
			// (set) Token: 0x060002BF RID: 703 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("collision0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000375C File Offset: 0x0000195C
			// (set) Token: 0x060002C1 RID: 705 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("collision1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002C2 RID: 706 RVA: 0x00003778 File Offset: 0x00001978
			// (set) Token: 0x060002C3 RID: 707 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("death0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003794 File Offset: 0x00001994
			// (set) Token: 0x060002C5 RID: 709 RVA: 0x00003719 File Offset: 0x00001919
			[Obsolete("death1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x000037AD File Offset: 0x000019AD
			private static void ThrowNotImplemented()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x000037B5 File Offset: 0x000019B5
			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060002C8 RID: 712 RVA: 0x000037BF File Offset: 0x000019BF
			// (set) Token: 0x060002C9 RID: 713 RVA: 0x000037C7 File Offset: 0x000019C7
			public bool enabled
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SubEmittersModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002CA RID: 714 RVA: 0x000037D0 File Offset: 0x000019D0
			public int subEmittersCount
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_subEmittersCount_Injected(ref this);
				}
			}

			// Token: 0x060002CB RID: 715 RVA: 0x000037D8 File Offset: 0x000019D8
			[NativeThrows]
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.AddSubEmitter_Injected(ref this, subEmitter, type, properties, emitProbability);
			}

			// Token: 0x060002CC RID: 716 RVA: 0x000037E5 File Offset: 0x000019E5
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties)
			{
				this.AddSubEmitter(subEmitter, type, properties, 1f);
			}

			// Token: 0x060002CD RID: 717 RVA: 0x000037F7 File Offset: 0x000019F7
			[NativeThrows]
			public void RemoveSubEmitter(int index)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitter_Injected(ref this, index);
			}

			// Token: 0x060002CE RID: 718 RVA: 0x00003800 File Offset: 0x00001A00
			public void RemoveSubEmitter(ParticleSystem subEmitter)
			{
				this.RemoveSubEmitterObject(subEmitter);
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000380B File Offset: 0x00001A0B
			[NativeThrows]
			private void RemoveSubEmitterObject(ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitterObject_Injected(ref this, subEmitter);
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x00003814 File Offset: 0x00001A14
			[NativeThrows]
			public void SetSubEmitterSystem(int index, ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterSystem_Injected(ref this, index, subEmitter);
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000381E File Offset: 0x00001A1E
			[NativeThrows]
			public void SetSubEmitterType(int index, ParticleSystemSubEmitterType type)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterType_Injected(ref this, index, type);
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x00003828 File Offset: 0x00001A28
			[NativeThrows]
			public void SetSubEmitterProperties(int index, ParticleSystemSubEmitterProperties properties)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterProperties_Injected(ref this, index, properties);
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x00003832 File Offset: 0x00001A32
			[NativeThrows]
			public void SetSubEmitterEmitProbability(int index, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterEmitProbability_Injected(ref this, index, emitProbability);
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000383C File Offset: 0x00001A3C
			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterSystem_Injected(ref this, index);
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x00003845 File Offset: 0x00001A45
			[NativeThrows]
			public ParticleSystemSubEmitterType GetSubEmitterType(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterType_Injected(ref this, index);
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x0000384E File Offset: 0x00001A4E
			[NativeThrows]
			public ParticleSystemSubEmitterProperties GetSubEmitterProperties(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterProperties_Injected(ref this, index);
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00003857 File Offset: 0x00001A57
			[NativeThrows]
			public float GetSubEmitterEmitProbability(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterEmitProbability_Injected(ref this, index);
			}

			// Token: 0x060002D8 RID: 728
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			// Token: 0x060002D9 RID: 729
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self, bool value);

			// Token: 0x060002DA RID: 730
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_subEmittersCount_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			// Token: 0x060002DB RID: 731
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability);

			// Token: 0x060002DC RID: 732
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x060002DD RID: 733
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitterObject_Injected(ref ParticleSystem.SubEmittersModule _unity_self, ParticleSystem subEmitter);

			// Token: 0x060002DE RID: 734
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystem subEmitter);

			// Token: 0x060002DF RID: 735
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterType type);

			// Token: 0x060002E0 RID: 736
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterProperties properties);

			// Token: 0x060002E1 RID: 737
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, float emitProbability);

			// Token: 0x060002E2 RID: 738
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x060002E3 RID: 739
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterType GetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x060002E4 RID: 740
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterProperties GetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x060002E5 RID: 741
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x04000009 RID: 9
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000C RID: 12
		public struct TextureSheetAnimationModule
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003860 File Offset: 0x00001A60
			// (set) Token: 0x060002E7 RID: 743 RVA: 0x00003888 File Offset: 0x00001A88
			[Obsolete("flipU property is deprecated. Use ParticleSystemRenderer.flip.x instead.", false)]
			public float flipU
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.x;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.x = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x000038BC File Offset: 0x00001ABC
			// (set) Token: 0x060002E9 RID: 745 RVA: 0x000038E4 File Offset: 0x00001AE4
			[Obsolete("flipV property is deprecated. Use ParticleSystemRenderer.flip.y instead.", false)]
			public float flipV
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.y;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.y = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002EB RID: 747 RVA: 0x00003928 File Offset: 0x00001B28
			// (set) Token: 0x060002EA RID: 746 RVA: 0x00003915 File Offset: 0x00001B15
			[Obsolete("useRandomRow property is deprecated. Use rowMode instead.", false)]
			public bool useRandomRow
			{
				get
				{
					return this.rowMode == ParticleSystemAnimationRowMode.Random;
				}
				set
				{
					this.rowMode = (value ? ParticleSystemAnimationRowMode.Random : ParticleSystemAnimationRowMode.Custom);
				}
			}

			// Token: 0x060002EC RID: 748 RVA: 0x00003943 File Offset: 0x00001B43
			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000394D File Offset: 0x00001B4D
			// (set) Token: 0x060002EE RID: 750 RVA: 0x00003955 File Offset: 0x00001B55
			public bool enabled
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000395E File Offset: 0x00001B5E
			// (set) Token: 0x060002F0 RID: 752 RVA: 0x00003966 File Offset: 0x00001B66
			public ParticleSystemAnimationMode mode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000396F File Offset: 0x00001B6F
			// (set) Token: 0x060002F2 RID: 754 RVA: 0x00003977 File Offset: 0x00001B77
			public ParticleSystemAnimationTimeMode timeMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_timeMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_timeMode_Injected(ref this, value);
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x00003980 File Offset: 0x00001B80
			// (set) Token: 0x060002F4 RID: 756 RVA: 0x00003988 File Offset: 0x00001B88
			public float fps
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_fps_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_fps_Injected(ref this, value);
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003991 File Offset: 0x00001B91
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x00003999 File Offset: 0x00001B99
			public int numTilesX
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesX_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesX_Injected(ref this, value);
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x000039A2 File Offset: 0x00001BA2
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x000039AA File Offset: 0x00001BAA
			public int numTilesY
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesY_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesY_Injected(ref this, value);
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x000039B3 File Offset: 0x00001BB3
			// (set) Token: 0x060002FA RID: 762 RVA: 0x000039BB File Offset: 0x00001BBB
			public ParticleSystemAnimationType animation
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_animation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_animation_Injected(ref this, value);
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002FB RID: 763 RVA: 0x000039C4 File Offset: 0x00001BC4
			// (set) Token: 0x060002FC RID: 764 RVA: 0x000039CC File Offset: 0x00001BCC
			public ParticleSystemAnimationRowMode rowMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowMode_Injected(ref this, value);
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002FD RID: 765 RVA: 0x000039D8 File Offset: 0x00001BD8
			// (set) Token: 0x060002FE RID: 766 RVA: 0x000039EE File Offset: 0x00001BEE
			public ParticleSystem.MinMaxCurve frameOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TextureSheetAnimationModule.get_frameOverTime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002FF RID: 767 RVA: 0x000039F8 File Offset: 0x00001BF8
			// (set) Token: 0x06000300 RID: 768 RVA: 0x00003A00 File Offset: 0x00001C00
			public float frameOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_frameOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000301 RID: 769 RVA: 0x00003A0C File Offset: 0x00001C0C
			// (set) Token: 0x06000302 RID: 770 RVA: 0x00003A22 File Offset: 0x00001C22
			public ParticleSystem.MinMaxCurve startFrame
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TextureSheetAnimationModule.get_startFrame_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrame_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000303 RID: 771 RVA: 0x00003A2C File Offset: 0x00001C2C
			// (set) Token: 0x06000304 RID: 772 RVA: 0x00003A34 File Offset: 0x00001C34
			public float startFrameMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_startFrameMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrameMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000305 RID: 773 RVA: 0x00003A3D File Offset: 0x00001C3D
			// (set) Token: 0x06000306 RID: 774 RVA: 0x00003A45 File Offset: 0x00001C45
			public int cycleCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_cycleCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_cycleCount_Injected(ref this, value);
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000307 RID: 775 RVA: 0x00003A4E File Offset: 0x00001C4E
			// (set) Token: 0x06000308 RID: 776 RVA: 0x00003A56 File Offset: 0x00001C56
			public int rowIndex
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowIndex_Injected(ref this, value);
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000309 RID: 777 RVA: 0x00003A5F File Offset: 0x00001C5F
			// (set) Token: 0x0600030A RID: 778 RVA: 0x00003A67 File Offset: 0x00001C67
			public UVChannelFlags uvChannelMask
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_uvChannelMask_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_uvChannelMask_Injected(ref this, value);
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600030B RID: 779 RVA: 0x00003A70 File Offset: 0x00001C70
			public int spriteCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_spriteCount_Injected(ref this);
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600030C RID: 780 RVA: 0x00003A78 File Offset: 0x00001C78
			// (set) Token: 0x0600030D RID: 781 RVA: 0x00003A8E File Offset: 0x00001C8E
			public Vector2 speedRange
			{
				get
				{
					Vector2 result;
					ParticleSystem.TextureSheetAnimationModule.get_speedRange_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_speedRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00003A98 File Offset: 0x00001C98
			[NativeThrows]
			public void AddSprite(Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.AddSprite_Injected(ref this, sprite);
			}

			// Token: 0x0600030F RID: 783 RVA: 0x00003AA1 File Offset: 0x00001CA1
			[NativeThrows]
			public void RemoveSprite(int index)
			{
				ParticleSystem.TextureSheetAnimationModule.RemoveSprite_Injected(ref this, index);
			}

			// Token: 0x06000310 RID: 784 RVA: 0x00003AAA File Offset: 0x00001CAA
			[NativeThrows]
			public void SetSprite(int index, Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.SetSprite_Injected(ref this, index, sprite);
			}

			// Token: 0x06000311 RID: 785 RVA: 0x00003AB4 File Offset: 0x00001CB4
			[NativeThrows]
			public Sprite GetSprite(int index)
			{
				return ParticleSystem.TextureSheetAnimationModule.GetSprite_Injected(ref this, index);
			}

			// Token: 0x06000312 RID: 786
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000313 RID: 787
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, bool value);

			// Token: 0x06000314 RID: 788
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationMode get_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000315 RID: 789
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationMode value);

			// Token: 0x06000316 RID: 790
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationTimeMode get_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000317 RID: 791
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationTimeMode value);

			// Token: 0x06000318 RID: 792
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000319 RID: 793
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x0600031A RID: 794
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600031B RID: 795
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600031C RID: 796
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600031D RID: 797
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600031E RID: 798
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationType get_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600031F RID: 799
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationType value);

			// Token: 0x06000320 RID: 800
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationRowMode get_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000321 RID: 801
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationRowMode value);

			// Token: 0x06000322 RID: 802
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000323 RID: 803
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000324 RID: 804
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000325 RID: 805
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x06000326 RID: 806
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000327 RID: 807
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000328 RID: 808
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000329 RID: 809
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			// Token: 0x0600032A RID: 810
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600032B RID: 811
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600032C RID: 812
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600032D RID: 813
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			// Token: 0x0600032E RID: 814
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern UVChannelFlags get_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x0600032F RID: 815
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, UVChannelFlags value);

			// Token: 0x06000330 RID: 816
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_spriteCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			// Token: 0x06000331 RID: 817
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out Vector2 ret);

			// Token: 0x06000332 RID: 818
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref Vector2 value);

			// Token: 0x06000333 RID: 819
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, Sprite sprite);

			// Token: 0x06000334 RID: 820
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			// Token: 0x06000335 RID: 821
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index, Sprite sprite);

			// Token: 0x06000336 RID: 822
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Sprite GetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			// Token: 0x0400000A RID: 10
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000D RID: 13
		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000337 RID: 823 RVA: 0x00003AC0 File Offset: 0x00001CC0
			// (set) Token: 0x06000338 RID: 824 RVA: 0x00003AD8 File Offset: 0x00001CD8
			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				get
				{
					return this.remainingLifetime;
				}
				set
				{
					this.remainingLifetime = value;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000339 RID: 825 RVA: 0x00003AE4 File Offset: 0x00001CE4
			// (set) Token: 0x0600033A RID: 826 RVA: 0x00003B07 File Offset: 0x00001D07
			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.", false)]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x0600033B RID: 827 RVA: 0x00003B1C File Offset: 0x00001D1C
			// (set) Token: 0x0600033C RID: 828 RVA: 0x00003B34 File Offset: 0x00001D34
			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.", false)]
			public float size
			{
				get
				{
					return this.startSize;
				}
				set
				{
					this.startSize = value;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x0600033D RID: 829 RVA: 0x00003B40 File Offset: 0x00001D40
			// (set) Token: 0x0600033E RID: 830 RVA: 0x00003B58 File Offset: 0x00001D58
			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.", false)]
			public Color32 color
			{
				get
				{
					return this.startColor;
				}
				set
				{
					this.startColor = value;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600033F RID: 831 RVA: 0x00003B64 File Offset: 0x00001D64
			// (set) Token: 0x06000340 RID: 832 RVA: 0x00003B7C File Offset: 0x00001D7C
			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000341 RID: 833 RVA: 0x00003B88 File Offset: 0x00001D88
			// (set) Token: 0x06000342 RID: 834 RVA: 0x00003BA0 File Offset: 0x00001DA0
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000343 RID: 835 RVA: 0x00003BAC File Offset: 0x00001DAC
			public Vector3 animatedVelocity
			{
				get
				{
					return this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000344 RID: 836 RVA: 0x00003BC4 File Offset: 0x00001DC4
			public Vector3 totalVelocity
			{
				get
				{
					return this.m_Velocity + this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000345 RID: 837 RVA: 0x00003BE8 File Offset: 0x00001DE8
			// (set) Token: 0x06000346 RID: 838 RVA: 0x00003C00 File Offset: 0x00001E00
			public float remainingLifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000347 RID: 839 RVA: 0x00003C0C File Offset: 0x00001E0C
			// (set) Token: 0x06000348 RID: 840 RVA: 0x00003C24 File Offset: 0x00001E24
			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000349 RID: 841 RVA: 0x00003C30 File Offset: 0x00001E30
			// (set) Token: 0x0600034A RID: 842 RVA: 0x00003C48 File Offset: 0x00001E48
			public Color32 startColor
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600034B RID: 843 RVA: 0x00003C54 File Offset: 0x00001E54
			// (set) Token: 0x0600034C RID: 844 RVA: 0x00003C6C File Offset: 0x00001E6C
			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600034D RID: 845 RVA: 0x00003C78 File Offset: 0x00001E78
			// (set) Token: 0x0600034E RID: 846 RVA: 0x00003C90 File Offset: 0x00001E90
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600034F RID: 847 RVA: 0x00003C9C File Offset: 0x00001E9C
			// (set) Token: 0x06000350 RID: 848 RVA: 0x00003CB9 File Offset: 0x00001EB9
			public float startSize
			{
				get
				{
					return this.m_StartSize.x;
				}
				set
				{
					this.m_StartSize = new Vector3(value, value, value);
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000351 RID: 849 RVA: 0x00003CCC File Offset: 0x00001ECC
			// (set) Token: 0x06000352 RID: 850 RVA: 0x00003CE4 File Offset: 0x00001EE4
			public Vector3 startSize3D
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
					this.m_Flags |= 1U;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000353 RID: 851 RVA: 0x00003CFC File Offset: 0x00001EFC
			// (set) Token: 0x06000354 RID: 852 RVA: 0x00003D1F File Offset: 0x00001F1F
			public float rotation
			{
				get
				{
					return this.m_Rotation.z * 57.29578f;
				}
				set
				{
					this.m_Rotation = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000355 RID: 853 RVA: 0x00003D40 File Offset: 0x00001F40
			// (set) Token: 0x06000356 RID: 854 RVA: 0x00003D62 File Offset: 0x00001F62
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000357 RID: 855 RVA: 0x00003D84 File Offset: 0x00001F84
			// (set) Token: 0x06000358 RID: 856 RVA: 0x00003DA7 File Offset: 0x00001FA7
			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000359 RID: 857 RVA: 0x00003DC8 File Offset: 0x00001FC8
			// (set) Token: 0x0600035A RID: 858 RVA: 0x00003DEA File Offset: 0x00001FEA
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x0600035B RID: 859 RVA: 0x00003E0C File Offset: 0x0000200C
			public float GetCurrentSize(ParticleSystem system)
			{
				return system.GetParticleCurrentSize(ref this);
			}

			// Token: 0x0600035C RID: 860 RVA: 0x00003E28 File Offset: 0x00002028
			public Vector3 GetCurrentSize3D(ParticleSystem system)
			{
				return system.GetParticleCurrentSize3D(ref this);
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00003E44 File Offset: 0x00002044
			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return system.GetParticleCurrentColor(ref this);
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00003E5D File Offset: 0x0000205D
			public void SetMeshIndex(int index)
			{
				this.m_MeshIndex = index;
				this.m_Flags |= 4U;
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00003E78 File Offset: 0x00002078
			public int GetMeshIndex(ParticleSystem system)
			{
				return system.GetParticleMeshIndex(ref this);
			}

			// Token: 0x0400000B RID: 11
			private Vector3 m_Position;

			// Token: 0x0400000C RID: 12
			private Vector3 m_Velocity;

			// Token: 0x0400000D RID: 13
			private Vector3 m_AnimatedVelocity;

			// Token: 0x0400000E RID: 14
			private Vector3 m_InitialVelocity;

			// Token: 0x0400000F RID: 15
			private Vector3 m_AxisOfRotation;

			// Token: 0x04000010 RID: 16
			private Vector3 m_Rotation;

			// Token: 0x04000011 RID: 17
			private Vector3 m_AngularVelocity;

			// Token: 0x04000012 RID: 18
			private Vector3 m_StartSize;

			// Token: 0x04000013 RID: 19
			private Color32 m_StartColor;

			// Token: 0x04000014 RID: 20
			private uint m_RandomSeed;

			// Token: 0x04000015 RID: 21
			private uint m_ParentRandomSeed;

			// Token: 0x04000016 RID: 22
			private float m_Lifetime;

			// Token: 0x04000017 RID: 23
			private float m_StartLifetime;

			// Token: 0x04000018 RID: 24
			private int m_MeshIndex;

			// Token: 0x04000019 RID: 25
			private float m_EmitAccumulator0;

			// Token: 0x0400001A RID: 26
			private float m_EmitAccumulator1;

			// Token: 0x0400001B RID: 27
			private uint m_Flags;

			// Token: 0x0200000E RID: 14
			[Flags]
			private enum Flags
			{
				// Token: 0x0400001D RID: 29
				Size3D = 1,
				// Token: 0x0400001E RID: 30
				Rotation3D = 2,
				// Token: 0x0400001F RID: 31
				MeshIndex = 4
			}
		}

		// Token: 0x0200000F RID: 15
		[NativeType(CodegenOptions.Custom, "MonoBurst", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct Burst
		{
			// Token: 0x06000360 RID: 864 RVA: 0x00003E91 File Offset: 0x00002091
			public Burst(float _time, short _count)
			{
				this.m_Time = _time;
				this.m_Count = (float)_count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x06000361 RID: 865 RVA: 0x00003EC5 File Offset: 0x000020C5
			public Burst(float _time, short _minCount, short _maxCount)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x06000362 RID: 866 RVA: 0x00003EFB File Offset: 0x000020FB
			public Burst(float _time, short _minCount, short _maxCount, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x06000363 RID: 867 RVA: 0x00003F31 File Offset: 0x00002131
			public Burst(float _time, ParticleSystem.MinMaxCurve _count)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00003F5F File Offset: 0x0000215F
			public Burst(float _time, ParticleSystem.MinMaxCurve _count, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000365 RID: 869 RVA: 0x00003F8C File Offset: 0x0000218C
			// (set) Token: 0x06000366 RID: 870 RVA: 0x00003FA4 File Offset: 0x000021A4
			public float time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000367 RID: 871 RVA: 0x00003FB0 File Offset: 0x000021B0
			// (set) Token: 0x06000368 RID: 872 RVA: 0x00003FC8 File Offset: 0x000021C8
			public ParticleSystem.MinMaxCurve count
			{
				get
				{
					return this.m_Count;
				}
				set
				{
					this.m_Count = value;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000369 RID: 873 RVA: 0x00003FD4 File Offset: 0x000021D4
			// (set) Token: 0x0600036A RID: 874 RVA: 0x00003FF2 File Offset: 0x000021F2
			public short minCount
			{
				get
				{
					return (short)this.m_Count.constantMin;
				}
				set
				{
					this.m_Count.constantMin = (float)value;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600036B RID: 875 RVA: 0x00004004 File Offset: 0x00002204
			// (set) Token: 0x0600036C RID: 876 RVA: 0x00004022 File Offset: 0x00002222
			public short maxCount
			{
				get
				{
					return (short)this.m_Count.constantMax;
				}
				set
				{
					this.m_Count.constantMax = (float)value;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600036D RID: 877 RVA: 0x00004034 File Offset: 0x00002234
			// (set) Token: 0x0600036E RID: 878 RVA: 0x00004050 File Offset: 0x00002250
			public int cycleCount
			{
				get
				{
					return this.m_RepeatCount + 1;
				}
				set
				{
					bool flag = value < 0;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("cycleCount", "cycleCount must be at least 0: " + value.ToString());
					}
					this.m_RepeatCount = value - 1;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000408C File Offset: 0x0000228C
			// (set) Token: 0x06000370 RID: 880 RVA: 0x000040A4 File Offset: 0x000022A4
			public float repeatInterval
			{
				get
				{
					return this.m_RepeatInterval;
				}
				set
				{
					bool flag = value <= 0f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("repeatInterval", "repeatInterval must be greater than 0.0f: " + value.ToString());
					}
					this.m_RepeatInterval = value;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000371 RID: 881 RVA: 0x000040E4 File Offset: 0x000022E4
			// (set) Token: 0x06000372 RID: 882 RVA: 0x00004104 File Offset: 0x00002304
			public float probability
			{
				get
				{
					return 1f - this.m_InvProbability;
				}
				set
				{
					bool flag = value < 0f || value > 1f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("probability", "probability must be between 0.0f and 1.0f: " + value.ToString());
					}
					this.m_InvProbability = 1f - value;
				}
			}

			// Token: 0x04000020 RID: 32
			private float m_Time;

			// Token: 0x04000021 RID: 33
			private ParticleSystem.MinMaxCurve m_Count;

			// Token: 0x04000022 RID: 34
			private int m_RepeatCount;

			// Token: 0x04000023 RID: 35
			private float m_RepeatInterval;

			// Token: 0x04000024 RID: 36
			private float m_InvProbability;
		}

		// Token: 0x02000010 RID: 16
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxCurve
		{
			// Token: 0x06000373 RID: 883 RVA: 0x00004152 File Offset: 0x00002352
			public MinMaxCurve(float constant)
			{
				this.m_Mode = ParticleSystemCurveMode.Constant;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = constant;
			}

			// Token: 0x06000374 RID: 884 RVA: 0x00004187 File Offset: 0x00002387
			public MinMaxCurve(float multiplier, AnimationCurve curve)
			{
				this.m_Mode = ParticleSystemCurveMode.Curve;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = null;
				this.m_CurveMax = curve;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x06000375 RID: 885 RVA: 0x000041BC File Offset: 0x000023BC
			public MinMaxCurve(float multiplier, AnimationCurve min, AnimationCurve max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoCurves;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = min;
				this.m_CurveMax = max;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x06000376 RID: 886 RVA: 0x000041F1 File Offset: 0x000023F1
			public MinMaxCurve(float min, float max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoConstants;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = min;
				this.m_ConstantMax = max;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000377 RID: 887 RVA: 0x00004224 File Offset: 0x00002424
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0000423C File Offset: 0x0000243C
			public ParticleSystemCurveMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000379 RID: 889 RVA: 0x00004248 File Offset: 0x00002448
			// (set) Token: 0x0600037A RID: 890 RVA: 0x00004260 File Offset: 0x00002460
			public float curveMultiplier
			{
				get
				{
					return this.m_CurveMultiplier;
				}
				set
				{
					this.m_CurveMultiplier = value;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000426C File Offset: 0x0000246C
			// (set) Token: 0x0600037C RID: 892 RVA: 0x00004284 File Offset: 0x00002484
			public AnimationCurve curveMax
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600037D RID: 893 RVA: 0x00004290 File Offset: 0x00002490
			// (set) Token: 0x0600037E RID: 894 RVA: 0x000042A8 File Offset: 0x000024A8
			public AnimationCurve curveMin
			{
				get
				{
					return this.m_CurveMin;
				}
				set
				{
					this.m_CurveMin = value;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600037F RID: 895 RVA: 0x000042B4 File Offset: 0x000024B4
			// (set) Token: 0x06000380 RID: 896 RVA: 0x000042CC File Offset: 0x000024CC
			public float constantMax
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000381 RID: 897 RVA: 0x000042D8 File Offset: 0x000024D8
			// (set) Token: 0x06000382 RID: 898 RVA: 0x000042F0 File Offset: 0x000024F0
			public float constantMin
			{
				get
				{
					return this.m_ConstantMin;
				}
				set
				{
					this.m_ConstantMin = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000383 RID: 899 RVA: 0x000042FC File Offset: 0x000024FC
			// (set) Token: 0x06000384 RID: 900 RVA: 0x000042CC File Offset: 0x000024CC
			public float constant
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000385 RID: 901 RVA: 0x00004314 File Offset: 0x00002514
			// (set) Token: 0x06000386 RID: 902 RVA: 0x00004284 File Offset: 0x00002484
			public AnimationCurve curve
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x06000387 RID: 903 RVA: 0x0000432C File Offset: 0x0000252C
			public float Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x06000388 RID: 904 RVA: 0x0000434C File Offset: 0x0000254C
			public float Evaluate(float time, float lerpFactor)
			{
				switch (this.mode)
				{
				case ParticleSystemCurveMode.Constant:
					return this.m_ConstantMax;
				case ParticleSystemCurveMode.TwoCurves:
					return Mathf.Lerp(this.m_CurveMin.Evaluate(time), this.m_CurveMax.Evaluate(time), lerpFactor) * this.m_CurveMultiplier;
				case ParticleSystemCurveMode.TwoConstants:
					return Mathf.Lerp(this.m_ConstantMin, this.m_ConstantMax, lerpFactor);
				}
				return this.m_CurveMax.Evaluate(time) * this.m_CurveMultiplier;
			}

			// Token: 0x06000389 RID: 905 RVA: 0x000043D8 File Offset: 0x000025D8
			public static implicit operator ParticleSystem.MinMaxCurve(float constant)
			{
				return new ParticleSystem.MinMaxCurve(constant);
			}

			// Token: 0x04000025 RID: 37
			[SerializeField]
			private ParticleSystemCurveMode m_Mode;

			// Token: 0x04000026 RID: 38
			[SerializeField]
			private float m_CurveMultiplier;

			// Token: 0x04000027 RID: 39
			[SerializeField]
			private AnimationCurve m_CurveMin;

			// Token: 0x04000028 RID: 40
			[SerializeField]
			private AnimationCurve m_CurveMax;

			// Token: 0x04000029 RID: 41
			[SerializeField]
			private float m_ConstantMin;

			// Token: 0x0400002A RID: 42
			[SerializeField]
			private float m_ConstantMax;
		}

		// Token: 0x02000011 RID: 17
		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxGradient
		{
			// Token: 0x0600038A RID: 906 RVA: 0x000043F0 File Offset: 0x000025F0
			public MinMaxGradient(Color color)
			{
				this.m_Mode = ParticleSystemGradientMode.Color;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = color;
			}

			// Token: 0x0600038B RID: 907 RVA: 0x0000441A File Offset: 0x0000261A
			public MinMaxGradient(Gradient gradient)
			{
				this.m_Mode = ParticleSystemGradientMode.Gradient;
				this.m_GradientMin = null;
				this.m_GradientMax = gradient;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00004448 File Offset: 0x00002648
			public MinMaxGradient(Color min, Color max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoColors;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = min;
				this.m_ColorMax = max;
			}

			// Token: 0x0600038D RID: 909 RVA: 0x0000446E File Offset: 0x0000266E
			public MinMaxGradient(Gradient min, Gradient max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoGradients;
				this.m_GradientMin = min;
				this.m_GradientMax = max;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x0600038E RID: 910 RVA: 0x0000449C File Offset: 0x0000269C
			// (set) Token: 0x0600038F RID: 911 RVA: 0x000044B4 File Offset: 0x000026B4
			public ParticleSystemGradientMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000390 RID: 912 RVA: 0x000044C0 File Offset: 0x000026C0
			// (set) Token: 0x06000391 RID: 913 RVA: 0x000044D8 File Offset: 0x000026D8
			public Gradient gradientMax
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000392 RID: 914 RVA: 0x000044E4 File Offset: 0x000026E4
			// (set) Token: 0x06000393 RID: 915 RVA: 0x000044FC File Offset: 0x000026FC
			public Gradient gradientMin
			{
				get
				{
					return this.m_GradientMin;
				}
				set
				{
					this.m_GradientMin = value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000394 RID: 916 RVA: 0x00004508 File Offset: 0x00002708
			// (set) Token: 0x06000395 RID: 917 RVA: 0x00004520 File Offset: 0x00002720
			public Color colorMax
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000396 RID: 918 RVA: 0x0000452C File Offset: 0x0000272C
			// (set) Token: 0x06000397 RID: 919 RVA: 0x00004544 File Offset: 0x00002744
			public Color colorMin
			{
				get
				{
					return this.m_ColorMin;
				}
				set
				{
					this.m_ColorMin = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000398 RID: 920 RVA: 0x00004550 File Offset: 0x00002750
			// (set) Token: 0x06000399 RID: 921 RVA: 0x00004520 File Offset: 0x00002720
			public Color color
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600039A RID: 922 RVA: 0x00004568 File Offset: 0x00002768
			// (set) Token: 0x0600039B RID: 923 RVA: 0x000044D8 File Offset: 0x000026D8
			public Gradient gradient
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x0600039C RID: 924 RVA: 0x00004580 File Offset: 0x00002780
			public Color Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x0600039D RID: 925 RVA: 0x000045A0 File Offset: 0x000027A0
			public Color Evaluate(float time, float lerpFactor)
			{
				switch (this.m_Mode)
				{
				case ParticleSystemGradientMode.Color:
					return this.m_ColorMax;
				case ParticleSystemGradientMode.TwoColors:
					return Color.Lerp(this.m_ColorMin, this.m_ColorMax, lerpFactor);
				case ParticleSystemGradientMode.TwoGradients:
					return Color.Lerp(this.m_GradientMin.Evaluate(time), this.m_GradientMax.Evaluate(time), lerpFactor);
				case ParticleSystemGradientMode.RandomColor:
					return this.m_GradientMax.Evaluate(lerpFactor);
				}
				return this.m_GradientMax.Evaluate(time);
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00004634 File Offset: 0x00002834
			public static implicit operator ParticleSystem.MinMaxGradient(Color color)
			{
				return new ParticleSystem.MinMaxGradient(color);
			}

			// Token: 0x0600039F RID: 927 RVA: 0x0000464C File Offset: 0x0000284C
			public static implicit operator ParticleSystem.MinMaxGradient(Gradient gradient)
			{
				return new ParticleSystem.MinMaxGradient(gradient);
			}

			// Token: 0x0400002B RID: 43
			[SerializeField]
			private ParticleSystemGradientMode m_Mode;

			// Token: 0x0400002C RID: 44
			[SerializeField]
			private Gradient m_GradientMin;

			// Token: 0x0400002D RID: 45
			[SerializeField]
			private Gradient m_GradientMax;

			// Token: 0x0400002E RID: 46
			[SerializeField]
			private Color m_ColorMin;

			// Token: 0x0400002F RID: 47
			[SerializeField]
			private Color m_ColorMax;
		}

		// Token: 0x02000012 RID: 18
		public struct EmitParams
		{
			// Token: 0x17000100 RID: 256
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x00004664 File Offset: 0x00002864
			// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000467C File Offset: 0x0000287C
			public ParticleSystem.Particle particle
			{
				get
				{
					return this.m_Particle;
				}
				set
				{
					this.m_Particle = value;
					this.m_PositionSet = true;
					this.m_VelocitySet = true;
					this.m_AxisOfRotationSet = true;
					this.m_RotationSet = true;
					this.m_AngularVelocitySet = true;
					this.m_StartSizeSet = true;
					this.m_StartColorSet = true;
					this.m_RandomSeedSet = true;
					this.m_StartLifetimeSet = true;
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x000046D8 File Offset: 0x000028D8
			// (set) Token: 0x060003A3 RID: 931 RVA: 0x000046F5 File Offset: 0x000028F5
			public Vector3 position
			{
				get
				{
					return this.m_Particle.position;
				}
				set
				{
					this.m_Particle.position = value;
					this.m_PositionSet = true;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000470C File Offset: 0x0000290C
			// (set) Token: 0x060003A5 RID: 933 RVA: 0x00004724 File Offset: 0x00002924
			public bool applyShapeToPosition
			{
				get
				{
					return this.m_ApplyShapeToPosition;
				}
				set
				{
					this.m_ApplyShapeToPosition = value;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x00004730 File Offset: 0x00002930
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000474D File Offset: 0x0000294D
			public Vector3 velocity
			{
				get
				{
					return this.m_Particle.velocity;
				}
				set
				{
					this.m_Particle.velocity = value;
					this.m_VelocitySet = true;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x00004764 File Offset: 0x00002964
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x00004781 File Offset: 0x00002981
			public float startLifetime
			{
				get
				{
					return this.m_Particle.startLifetime;
				}
				set
				{
					this.m_Particle.startLifetime = value;
					this.m_StartLifetimeSet = true;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060003AA RID: 938 RVA: 0x00004798 File Offset: 0x00002998
			// (set) Token: 0x060003AB RID: 939 RVA: 0x000047B5 File Offset: 0x000029B5
			public float startSize
			{
				get
				{
					return this.m_Particle.startSize;
				}
				set
				{
					this.m_Particle.startSize = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060003AC RID: 940 RVA: 0x000047CC File Offset: 0x000029CC
			// (set) Token: 0x060003AD RID: 941 RVA: 0x000047E9 File Offset: 0x000029E9
			public Vector3 startSize3D
			{
				get
				{
					return this.m_Particle.startSize3D;
				}
				set
				{
					this.m_Particle.startSize3D = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060003AE RID: 942 RVA: 0x00004800 File Offset: 0x00002A00
			// (set) Token: 0x060003AF RID: 943 RVA: 0x0000481D File Offset: 0x00002A1D
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_Particle.axisOfRotation;
				}
				set
				{
					this.m_Particle.axisOfRotation = value;
					this.m_AxisOfRotationSet = true;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x00004834 File Offset: 0x00002A34
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x00004851 File Offset: 0x00002A51
			public float rotation
			{
				get
				{
					return this.m_Particle.rotation;
				}
				set
				{
					this.m_Particle.rotation = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x00004868 File Offset: 0x00002A68
			// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004885 File Offset: 0x00002A85
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Particle.rotation3D;
				}
				set
				{
					this.m_Particle.rotation3D = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000489C File Offset: 0x00002A9C
			// (set) Token: 0x060003B5 RID: 949 RVA: 0x000048B9 File Offset: 0x00002AB9
			public float angularVelocity
			{
				get
				{
					return this.m_Particle.angularVelocity;
				}
				set
				{
					this.m_Particle.angularVelocity = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x000048D0 File Offset: 0x00002AD0
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x000048ED File Offset: 0x00002AED
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_Particle.angularVelocity3D;
				}
				set
				{
					this.m_Particle.angularVelocity3D = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00004904 File Offset: 0x00002B04
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x00004921 File Offset: 0x00002B21
			public Color32 startColor
			{
				get
				{
					return this.m_Particle.startColor;
				}
				set
				{
					this.m_Particle.startColor = value;
					this.m_StartColorSet = true;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00004938 File Offset: 0x00002B38
			// (set) Token: 0x060003BB RID: 955 RVA: 0x00004955 File Offset: 0x00002B55
			public uint randomSeed
			{
				get
				{
					return this.m_Particle.randomSeed;
				}
				set
				{
					this.m_Particle.randomSeed = value;
					this.m_RandomSeedSet = true;
				}
			}

			// Token: 0x1700010E RID: 270
			// (set) Token: 0x060003BC RID: 956 RVA: 0x0000496C File Offset: 0x00002B6C
			public int meshIndex
			{
				set
				{
					this.m_Particle.SetMeshIndex(value);
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00004983 File Offset: 0x00002B83
			public void ResetPosition()
			{
				this.m_PositionSet = false;
			}

			// Token: 0x060003BE RID: 958 RVA: 0x0000498D File Offset: 0x00002B8D
			public void ResetVelocity()
			{
				this.m_VelocitySet = false;
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00004997 File Offset: 0x00002B97
			public void ResetAxisOfRotation()
			{
				this.m_AxisOfRotationSet = false;
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x000049A1 File Offset: 0x00002BA1
			public void ResetRotation()
			{
				this.m_RotationSet = false;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x000049AB File Offset: 0x00002BAB
			public void ResetAngularVelocity()
			{
				this.m_AngularVelocitySet = false;
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x000049B5 File Offset: 0x00002BB5
			public void ResetStartSize()
			{
				this.m_StartSizeSet = false;
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x000049BF File Offset: 0x00002BBF
			public void ResetStartColor()
			{
				this.m_StartColorSet = false;
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x000049C9 File Offset: 0x00002BC9
			public void ResetRandomSeed()
			{
				this.m_RandomSeedSet = false;
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x000049D3 File Offset: 0x00002BD3
			public void ResetStartLifetime()
			{
				this.m_StartLifetimeSet = false;
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x000049DD File Offset: 0x00002BDD
			public void ResetMeshIndex()
			{
				this.m_MeshIndexSet = false;
			}

			// Token: 0x04000030 RID: 48
			[NativeName("particle")]
			private ParticleSystem.Particle m_Particle;

			// Token: 0x04000031 RID: 49
			[NativeName("positionSet")]
			private bool m_PositionSet;

			// Token: 0x04000032 RID: 50
			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			// Token: 0x04000033 RID: 51
			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			// Token: 0x04000034 RID: 52
			[NativeName("rotationSet")]
			private bool m_RotationSet;

			// Token: 0x04000035 RID: 53
			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			// Token: 0x04000036 RID: 54
			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			// Token: 0x04000037 RID: 55
			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			// Token: 0x04000038 RID: 56
			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			// Token: 0x04000039 RID: 57
			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			// Token: 0x0400003A RID: 58
			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			// Token: 0x0400003B RID: 59
			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;
		}

		// Token: 0x02000013 RID: 19
		public struct PlaybackState
		{
			// Token: 0x0400003C RID: 60
			internal float m_AccumulatedDt;

			// Token: 0x0400003D RID: 61
			internal float m_StartDelay;

			// Token: 0x0400003E RID: 62
			internal float m_PlaybackTime;

			// Token: 0x0400003F RID: 63
			internal int m_RingBufferIndex;

			// Token: 0x04000040 RID: 64
			internal ParticleSystem.PlaybackState.Emission m_Emission;

			// Token: 0x04000041 RID: 65
			internal ParticleSystem.PlaybackState.Initial m_Initial;

			// Token: 0x04000042 RID: 66
			internal ParticleSystem.PlaybackState.Shape m_Shape;

			// Token: 0x04000043 RID: 67
			internal ParticleSystem.PlaybackState.Force m_Force;

			// Token: 0x04000044 RID: 68
			internal ParticleSystem.PlaybackState.Collision m_Collision;

			// Token: 0x04000045 RID: 69
			internal ParticleSystem.PlaybackState.Noise m_Noise;

			// Token: 0x04000046 RID: 70
			internal ParticleSystem.PlaybackState.Lights m_Lights;

			// Token: 0x04000047 RID: 71
			internal ParticleSystem.PlaybackState.Trail m_Trail;

			// Token: 0x02000014 RID: 20
			internal struct Seed
			{
				// Token: 0x04000048 RID: 72
				public uint x;

				// Token: 0x04000049 RID: 73
				public uint y;

				// Token: 0x0400004A RID: 74
				public uint z;

				// Token: 0x0400004B RID: 75
				public uint w;
			}

			// Token: 0x02000015 RID: 21
			internal struct Seed4
			{
				// Token: 0x0400004C RID: 76
				public ParticleSystem.PlaybackState.Seed x;

				// Token: 0x0400004D RID: 77
				public ParticleSystem.PlaybackState.Seed y;

				// Token: 0x0400004E RID: 78
				public ParticleSystem.PlaybackState.Seed z;

				// Token: 0x0400004F RID: 79
				public ParticleSystem.PlaybackState.Seed w;
			}

			// Token: 0x02000016 RID: 22
			internal struct Emission
			{
				// Token: 0x04000050 RID: 80
				public float m_ParticleSpacing;

				// Token: 0x04000051 RID: 81
				public float m_ToEmitAccumulator;

				// Token: 0x04000052 RID: 82
				public ParticleSystem.PlaybackState.Seed m_Random;
			}

			// Token: 0x02000017 RID: 23
			internal struct Initial
			{
				// Token: 0x04000053 RID: 83
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x02000018 RID: 24
			internal struct Shape
			{
				// Token: 0x04000054 RID: 84
				public ParticleSystem.PlaybackState.Seed4 m_Random;

				// Token: 0x04000055 RID: 85
				public float m_RadiusTimer;

				// Token: 0x04000056 RID: 86
				public float m_RadiusTimerPrev;

				// Token: 0x04000057 RID: 87
				public float m_ArcTimer;

				// Token: 0x04000058 RID: 88
				public float m_ArcTimerPrev;

				// Token: 0x04000059 RID: 89
				public float m_MeshSpawnTimer;

				// Token: 0x0400005A RID: 90
				public float m_MeshSpawnTimerPrev;

				// Token: 0x0400005B RID: 91
				public int m_OrderedMeshVertexIndex;
			}

			// Token: 0x02000019 RID: 25
			internal struct Force
			{
				// Token: 0x0400005C RID: 92
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x0200001A RID: 26
			internal struct Collision
			{
				// Token: 0x0400005D RID: 93
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x0200001B RID: 27
			internal struct Noise
			{
				// Token: 0x0400005E RID: 94
				public float m_ScrollOffset;
			}

			// Token: 0x0200001C RID: 28
			internal struct Lights
			{
				// Token: 0x0400005F RID: 95
				public ParticleSystem.PlaybackState.Seed m_Random;

				// Token: 0x04000060 RID: 96
				public float m_ParticleEmissionCounter;
			}

			// Token: 0x0200001D RID: 29
			internal struct Trail
			{
				// Token: 0x04000061 RID: 97
				public float m_Timer;
			}
		}

		// Token: 0x0200001E RID: 30
		[NativeType(CodegenOptions.Custom, "MonoParticleTrails")]
		public struct Trails
		{
			// Token: 0x060003C7 RID: 967 RVA: 0x000049E8 File Offset: 0x00002BE8
			internal void Allocate()
			{
				bool flag = this.positions == null;
				if (flag)
				{
					this.positions = new List<Vector4>();
				}
				bool flag2 = this.frontPositions == null;
				if (flag2)
				{
					this.frontPositions = new List<int>();
				}
				bool flag3 = this.backPositions == null;
				if (flag3)
				{
					this.backPositions = new List<int>();
				}
				bool flag4 = this.positionCounts == null;
				if (flag4)
				{
					this.positionCounts = new List<int>();
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060003C9 RID: 969 RVA: 0x00004A94 File Offset: 0x00002C94
			// (set) Token: 0x060003C8 RID: 968 RVA: 0x00004A56 File Offset: 0x00002C56
			public int capacity
			{
				get
				{
					bool flag = this.positions == null;
					int result;
					if (flag)
					{
						result = 0;
					}
					else
					{
						result = this.positions.Capacity;
					}
					return result;
				}
				set
				{
					this.Allocate();
					this.positions.Capacity = value;
					this.frontPositions.Capacity = value;
					this.backPositions.Capacity = value;
					this.positionCounts.Capacity = value;
				}
			}

			// Token: 0x04000062 RID: 98
			internal List<Vector4> positions;

			// Token: 0x04000063 RID: 99
			internal List<int> frontPositions;

			// Token: 0x04000064 RID: 100
			internal List<int> backPositions;

			// Token: 0x04000065 RID: 101
			internal List<int> positionCounts;

			// Token: 0x04000066 RID: 102
			internal int maxTrailCount;

			// Token: 0x04000067 RID: 103
			internal int maxPositionsPerTrailCount;
		}

		// Token: 0x0200001F RID: 31
		public struct ColliderData
		{
			// Token: 0x060003CA RID: 970 RVA: 0x00004AC4 File Offset: 0x00002CC4
			public int GetColliderCount(int particleIndex)
			{
				bool flag = particleIndex < this.particleStartIndices.Length - 1;
				int result;
				if (flag)
				{
					result = this.particleStartIndices[particleIndex + 1] - this.particleStartIndices[particleIndex];
				}
				else
				{
					result = this.colliderIndices.Length - this.particleStartIndices[particleIndex];
				}
				return result;
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00004B10 File Offset: 0x00002D10
			public Component GetCollider(int particleIndex, int colliderIndex)
			{
				bool flag = colliderIndex >= this.GetColliderCount(particleIndex);
				if (flag)
				{
					throw new IndexOutOfRangeException("colliderIndex exceeded the total number of colliders for the requested particle");
				}
				int num = this.particleStartIndices[particleIndex] + colliderIndex;
				return this.colliders[this.colliderIndices[num]];
			}

			// Token: 0x04000068 RID: 104
			internal Component[] colliders;

			// Token: 0x04000069 RID: 105
			internal int[] colliderIndices;

			// Token: 0x0400006A RID: 106
			internal int[] particleStartIndices;
		}

		// Token: 0x02000020 RID: 32
		public struct VelocityOverLifetimeModule
		{
			// Token: 0x060003CC RID: 972 RVA: 0x00004B58 File Offset: 0x00002D58
			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x060003CD RID: 973 RVA: 0x00004B62 File Offset: 0x00002D62
			// (set) Token: 0x060003CE RID: 974 RVA: 0x00004B6A File Offset: 0x00002D6A
			public bool enabled
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060003CF RID: 975 RVA: 0x00004B74 File Offset: 0x00002D74
			// (set) Token: 0x060003D0 RID: 976 RVA: 0x00004B8A File Offset: 0x00002D8A
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060003D1 RID: 977 RVA: 0x00004B94 File Offset: 0x00002D94
			// (set) Token: 0x060003D2 RID: 978 RVA: 0x00004BAA File Offset: 0x00002DAA
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060003D3 RID: 979 RVA: 0x00004BB4 File Offset: 0x00002DB4
			// (set) Token: 0x060003D4 RID: 980 RVA: 0x00004BCA File Offset: 0x00002DCA
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x00004BD4 File Offset: 0x00002DD4
			// (set) Token: 0x060003D6 RID: 982 RVA: 0x00004BDC File Offset: 0x00002DDC
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x00004BE5 File Offset: 0x00002DE5
			// (set) Token: 0x060003D8 RID: 984 RVA: 0x00004BED File Offset: 0x00002DED
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x00004BF6 File Offset: 0x00002DF6
			// (set) Token: 0x060003DA RID: 986 RVA: 0x00004BFE File Offset: 0x00002DFE
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060003DB RID: 987 RVA: 0x00004C08 File Offset: 0x00002E08
			// (set) Token: 0x060003DC RID: 988 RVA: 0x00004C1E File Offset: 0x00002E1E
			public ParticleSystem.MinMaxCurve orbitalX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalX_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060003DD RID: 989 RVA: 0x00004C28 File Offset: 0x00002E28
			// (set) Token: 0x060003DE RID: 990 RVA: 0x00004C3E File Offset: 0x00002E3E
			public ParticleSystem.MinMaxCurve orbitalY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060003DF RID: 991 RVA: 0x00004C48 File Offset: 0x00002E48
			// (set) Token: 0x060003E0 RID: 992 RVA: 0x00004C5E File Offset: 0x00002E5E
			public ParticleSystem.MinMaxCurve orbitalZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x00004C68 File Offset: 0x00002E68
			// (set) Token: 0x060003E2 RID: 994 RVA: 0x00004C70 File Offset: 0x00002E70
			public float orbitalXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x00004C79 File Offset: 0x00002E79
			// (set) Token: 0x060003E4 RID: 996 RVA: 0x00004C81 File Offset: 0x00002E81
			public float orbitalYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x00004C8A File Offset: 0x00002E8A
			// (set) Token: 0x060003E6 RID: 998 RVA: 0x00004C92 File Offset: 0x00002E92
			public float orbitalZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x00004C9C File Offset: 0x00002E9C
			// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00004CB2 File Offset: 0x00002EB2
			public ParticleSystem.MinMaxCurve orbitalOffsetX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00004CBC File Offset: 0x00002EBC
			// (set) Token: 0x060003EA RID: 1002 RVA: 0x00004CD2 File Offset: 0x00002ED2
			public ParticleSystem.MinMaxCurve orbitalOffsetY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004CDC File Offset: 0x00002EDC
			// (set) Token: 0x060003EC RID: 1004 RVA: 0x00004CF2 File Offset: 0x00002EF2
			public ParticleSystem.MinMaxCurve orbitalOffsetZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x00004CFC File Offset: 0x00002EFC
			// (set) Token: 0x060003EE RID: 1006 RVA: 0x00004D04 File Offset: 0x00002F04
			public float orbitalOffsetXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060003EF RID: 1007 RVA: 0x00004D0D File Offset: 0x00002F0D
			// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00004D15 File Offset: 0x00002F15
			public float orbitalOffsetYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00004D1E File Offset: 0x00002F1E
			// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00004D26 File Offset: 0x00002F26
			public float orbitalOffsetZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00004D30 File Offset: 0x00002F30
			// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00004D46 File Offset: 0x00002F46
			public ParticleSystem.MinMaxCurve radial
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_radial_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radial_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00004D50 File Offset: 0x00002F50
			// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00004D58 File Offset: 0x00002F58
			public float radialMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_radialMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radialMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00004D64 File Offset: 0x00002F64
			// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00004D7A File Offset: 0x00002F7A
			public ParticleSystem.MinMaxCurve speedModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_speedModifier_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifier_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00004D84 File Offset: 0x00002F84
			// (set) Token: 0x060003FA RID: 1018 RVA: 0x00004D8C File Offset: 0x00002F8C
			public float speedModifierMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_speedModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifierMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060003FB RID: 1019 RVA: 0x00004D95 File Offset: 0x00002F95
			// (set) Token: 0x060003FC RID: 1020 RVA: 0x00004D9D File Offset: 0x00002F9D
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x060003FD RID: 1021
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x060003FE RID: 1022
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x060003FF RID: 1023
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000400 RID: 1024
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000401 RID: 1025
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000402 RID: 1026
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000403 RID: 1027
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000404 RID: 1028
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000405 RID: 1029
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000406 RID: 1030
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000407 RID: 1031
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000408 RID: 1032
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000409 RID: 1033
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600040A RID: 1034
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600040B RID: 1035
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600040C RID: 1036
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600040D RID: 1037
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600040E RID: 1038
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600040F RID: 1039
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000410 RID: 1040
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000411 RID: 1041
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000412 RID: 1042
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000413 RID: 1043
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000414 RID: 1044
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000415 RID: 1045
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000416 RID: 1046
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000417 RID: 1047
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000418 RID: 1048
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000419 RID: 1049
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600041A RID: 1050
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600041B RID: 1051
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600041C RID: 1052
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600041D RID: 1053
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600041E RID: 1054
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600041F RID: 1055
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000420 RID: 1056
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000421 RID: 1057
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000422 RID: 1058
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000423 RID: 1059
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000424 RID: 1060
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000425 RID: 1061
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x06000426 RID: 1062
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000427 RID: 1063
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000428 RID: 1064
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000429 RID: 1065
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600042A RID: 1066
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600042B RID: 1067
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			// Token: 0x0600042C RID: 1068
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x0400006B RID: 107
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000021 RID: 33
		public struct LimitVelocityOverLifetimeModule
		{
			// Token: 0x0600042D RID: 1069 RVA: 0x00004DA6 File Offset: 0x00002FA6
			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x00004DB0 File Offset: 0x00002FB0
			// (set) Token: 0x0600042F RID: 1071 RVA: 0x00004DB8 File Offset: 0x00002FB8
			public bool enabled
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x00004DC4 File Offset: 0x00002FC4
			// (set) Token: 0x06000431 RID: 1073 RVA: 0x00004DDA File Offset: 0x00002FDA
			public ParticleSystem.MinMaxCurve limitX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x00004DE4 File Offset: 0x00002FE4
			// (set) Token: 0x06000433 RID: 1075 RVA: 0x00004DEC File Offset: 0x00002FEC
			public float limitXMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00004DF8 File Offset: 0x00002FF8
			// (set) Token: 0x06000435 RID: 1077 RVA: 0x00004E0E File Offset: 0x0000300E
			public ParticleSystem.MinMaxCurve limitY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitY_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x00004E18 File Offset: 0x00003018
			// (set) Token: 0x06000437 RID: 1079 RVA: 0x00004E20 File Offset: 0x00003020
			public float limitYMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x00004E2C File Offset: 0x0000302C
			// (set) Token: 0x06000439 RID: 1081 RVA: 0x00004E42 File Offset: 0x00003042
			public ParticleSystem.MinMaxCurve limitZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x00004E4C File Offset: 0x0000304C
			// (set) Token: 0x0600043B RID: 1083 RVA: 0x00004E54 File Offset: 0x00003054
			public float limitZMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00004E60 File Offset: 0x00003060
			// (set) Token: 0x0600043D RID: 1085 RVA: 0x00004E76 File Offset: 0x00003076
			[NativeName("Magnitude")]
			public ParticleSystem.MinMaxCurve limit
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limit_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limit_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x00004E80 File Offset: 0x00003080
			// (set) Token: 0x0600043F RID: 1087 RVA: 0x00004E88 File Offset: 0x00003088
			[NativeName("MagnitudeMultiplier")]
			public float limitMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x00004E91 File Offset: 0x00003091
			// (set) Token: 0x06000441 RID: 1089 RVA: 0x00004E99 File Offset: 0x00003099
			public float dampen
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dampen_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dampen_Injected(ref this, value);
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00004EA2 File Offset: 0x000030A2
			// (set) Token: 0x06000443 RID: 1091 RVA: 0x00004EAA File Offset: 0x000030AA
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x00004EB3 File Offset: 0x000030B3
			// (set) Token: 0x06000445 RID: 1093 RVA: 0x00004EBB File Offset: 0x000030BB
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x00004EC4 File Offset: 0x000030C4
			// (set) Token: 0x06000447 RID: 1095 RVA: 0x00004EDA File Offset: 0x000030DA
			public ParticleSystem.MinMaxCurve drag
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_drag_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_drag_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x00004EE4 File Offset: 0x000030E4
			// (set) Token: 0x06000449 RID: 1097 RVA: 0x00004EEC File Offset: 0x000030EC
			public float dragMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dragMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dragMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00004EF5 File Offset: 0x000030F5
			// (set) Token: 0x0600044B RID: 1099 RVA: 0x00004EFD File Offset: 0x000030FD
			public bool multiplyDragByParticleSize
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleSize_Injected(ref this, value);
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x00004F06 File Offset: 0x00003106
			// (set) Token: 0x0600044D RID: 1101 RVA: 0x00004F0E File Offset: 0x0000310E
			public bool multiplyDragByParticleVelocity
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleVelocity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleVelocity_Injected(ref this, value);
				}
			}

			// Token: 0x0600044E RID: 1102
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x0600044F RID: 1103
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000450 RID: 1104
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000451 RID: 1105
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000452 RID: 1106
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000453 RID: 1107
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000454 RID: 1108
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000455 RID: 1109
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000456 RID: 1110
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000457 RID: 1111
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000458 RID: 1112
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000459 RID: 1113
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600045A RID: 1114
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x0600045B RID: 1115
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600045C RID: 1116
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600045D RID: 1117
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600045E RID: 1118
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x0600045F RID: 1119
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000460 RID: 1120
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000461 RID: 1121
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x06000462 RID: 1122
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000463 RID: 1123
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000464 RID: 1124
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000465 RID: 1125
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x06000466 RID: 1126
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000467 RID: 1127
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000468 RID: 1128
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x06000469 RID: 1129
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			// Token: 0x0600046A RID: 1130
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x0600046B RID: 1131
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x0600046C RID: 1132
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			// Token: 0x0600046D RID: 1133
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			// Token: 0x0400006C RID: 108
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000022 RID: 34
		public struct InheritVelocityModule
		{
			// Token: 0x0600046E RID: 1134 RVA: 0x00004F17 File Offset: 0x00003117
			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x00004F21 File Offset: 0x00003121
			// (set) Token: 0x06000470 RID: 1136 RVA: 0x00004F29 File Offset: 0x00003129
			public bool enabled
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x00004F32 File Offset: 0x00003132
			// (set) Token: 0x06000472 RID: 1138 RVA: 0x00004F3A File Offset: 0x0000313A
			public ParticleSystemInheritVelocityMode mode
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000473 RID: 1139 RVA: 0x00004F44 File Offset: 0x00003144
			// (set) Token: 0x06000474 RID: 1140 RVA: 0x00004F5A File Offset: 0x0000315A
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.InheritVelocityModule.get_curve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curve_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x00004F64 File Offset: 0x00003164
			// (set) Token: 0x06000476 RID: 1142 RVA: 0x00004F6C File Offset: 0x0000316C
			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x06000477 RID: 1143
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x06000478 RID: 1144
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, bool value);

			// Token: 0x06000479 RID: 1145
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemInheritVelocityMode get_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x0600047A RID: 1146
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ParticleSystemInheritVelocityMode value);

			// Token: 0x0600047B RID: 1147
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600047C RID: 1148
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600047D RID: 1149
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			// Token: 0x0600047E RID: 1150
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, float value);

			// Token: 0x0400006D RID: 109
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000023 RID: 35
		public struct LifetimeByEmitterSpeedModule
		{
			// Token: 0x0600047F RID: 1151 RVA: 0x00004F75 File Offset: 0x00003175
			internal LifetimeByEmitterSpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x00004F7F File Offset: 0x0000317F
			// (set) Token: 0x06000481 RID: 1153 RVA: 0x00004F87 File Offset: 0x00003187
			public bool enabled
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000482 RID: 1154 RVA: 0x00004F90 File Offset: 0x00003190
			// (set) Token: 0x06000483 RID: 1155 RVA: 0x00004FA6 File Offset: 0x000031A6
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_curve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curve_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000484 RID: 1156 RVA: 0x00004FB0 File Offset: 0x000031B0
			// (set) Token: 0x06000485 RID: 1157 RVA: 0x00004FB8 File Offset: 0x000031B8
			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000486 RID: 1158 RVA: 0x00004FC4 File Offset: 0x000031C4
			// (set) Token: 0x06000487 RID: 1159 RVA: 0x00004FDA File Offset: 0x000031DA
			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000488 RID: 1160
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			// Token: 0x06000489 RID: 1161
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, bool value);

			// Token: 0x0600048A RID: 1162
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600048B RID: 1163
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600048C RID: 1164
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			// Token: 0x0600048D RID: 1165
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, float value);

			// Token: 0x0600048E RID: 1166
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out Vector2 ret);

			// Token: 0x0600048F RID: 1167
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref Vector2 value);

			// Token: 0x0400006E RID: 110
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000024 RID: 36
		public struct ForceOverLifetimeModule
		{
			// Token: 0x06000490 RID: 1168 RVA: 0x00004FE4 File Offset: 0x000031E4
			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x00004FEE File Offset: 0x000031EE
			// (set) Token: 0x06000492 RID: 1170 RVA: 0x00004FF6 File Offset: 0x000031F6
			public bool enabled
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x00005000 File Offset: 0x00003200
			// (set) Token: 0x06000494 RID: 1172 RVA: 0x00005016 File Offset: 0x00003216
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00005020 File Offset: 0x00003220
			// (set) Token: 0x06000496 RID: 1174 RVA: 0x00005036 File Offset: 0x00003236
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000497 RID: 1175 RVA: 0x00005040 File Offset: 0x00003240
			// (set) Token: 0x06000498 RID: 1176 RVA: 0x00005056 File Offset: 0x00003256
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000499 RID: 1177 RVA: 0x00005060 File Offset: 0x00003260
			// (set) Token: 0x0600049A RID: 1178 RVA: 0x00005068 File Offset: 0x00003268
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x00005071 File Offset: 0x00003271
			// (set) Token: 0x0600049C RID: 1180 RVA: 0x00005079 File Offset: 0x00003279
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600049D RID: 1181 RVA: 0x00005082 File Offset: 0x00003282
			// (set) Token: 0x0600049E RID: 1182 RVA: 0x0000508A File Offset: 0x0000328A
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600049F RID: 1183 RVA: 0x00005093 File Offset: 0x00003293
			// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000509B File Offset: 0x0000329B
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000050A4 File Offset: 0x000032A4
			// (set) Token: 0x060004A2 RID: 1186 RVA: 0x000050AC File Offset: 0x000032AC
			public bool randomized
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_randomized_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_randomized_Injected(ref this, value);
				}
			}

			// Token: 0x060004A3 RID: 1187
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004A4 RID: 1188
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);

			// Token: 0x060004A5 RID: 1189
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004A6 RID: 1190
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004A7 RID: 1191
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004A8 RID: 1192
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004A9 RID: 1193
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004AA RID: 1194
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004AB RID: 1195
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004AC RID: 1196
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x060004AD RID: 1197
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004AE RID: 1198
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x060004AF RID: 1199
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004B0 RID: 1200
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			// Token: 0x060004B1 RID: 1201
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004B2 RID: 1202
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			// Token: 0x060004B3 RID: 1203
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			// Token: 0x060004B4 RID: 1204
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);

			// Token: 0x0400006F RID: 111
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000025 RID: 37
		public struct ColorOverLifetimeModule
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x000050B5 File Offset: 0x000032B5
			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000050BF File Offset: 0x000032BF
			// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000050C7 File Offset: 0x000032C7
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000050D0 File Offset: 0x000032D0
			// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000050E6 File Offset: 0x000032E6
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.ColorOverLifetimeModule.get_color_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_color_Injected(ref this, ref value);
				}
			}

			// Token: 0x060004BA RID: 1210
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self);

			// Token: 0x060004BB RID: 1211
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, bool value);

			// Token: 0x060004BC RID: 1212
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x060004BD RID: 1213
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x04000070 RID: 112
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000026 RID: 38
		public struct ColorBySpeedModule
		{
			// Token: 0x060004BE RID: 1214 RVA: 0x000050F0 File Offset: 0x000032F0
			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x000050FA File Offset: 0x000032FA
			// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00005102 File Offset: 0x00003302
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000510C File Offset: 0x0000330C
			// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00005122 File Offset: 0x00003322
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.ColorBySpeedModule.get_color_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_color_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000512C File Offset: 0x0000332C
			// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00005142 File Offset: 0x00003342
			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.ColorBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x060004C5 RID: 1221
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self);

			// Token: 0x060004C6 RID: 1222
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, bool value);

			// Token: 0x060004C7 RID: 1223
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x060004C8 RID: 1224
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x060004C9 RID: 1225
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x060004CA RID: 1226
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x04000071 RID: 113
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000027 RID: 39
		public struct SizeOverLifetimeModule
		{
			// Token: 0x060004CB RID: 1227 RVA: 0x0000514C File Offset: 0x0000334C
			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00005156 File Offset: 0x00003356
			// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000515E File Offset: 0x0000335E
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004CE RID: 1230 RVA: 0x00005168 File Offset: 0x00003368
			// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000517E File Offset: 0x0000337E
			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_size_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_size_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00005188 File Offset: 0x00003388
			// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00005190 File Offset: 0x00003390
			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000519C File Offset: 0x0000339C
			// (set) Token: 0x060004D3 RID: 1235 RVA: 0x000051B2 File Offset: 0x000033B2
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000051BC File Offset: 0x000033BC
			// (set) Token: 0x060004D5 RID: 1237 RVA: 0x000051C4 File Offset: 0x000033C4
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000051D0 File Offset: 0x000033D0
			// (set) Token: 0x060004D7 RID: 1239 RVA: 0x000051E6 File Offset: 0x000033E6
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000051F0 File Offset: 0x000033F0
			// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000051F8 File Offset: 0x000033F8
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x00005204 File Offset: 0x00003404
			// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000521A File Offset: 0x0000341A
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00005224 File Offset: 0x00003424
			// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000522C File Offset: 0x0000342C
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x00005235 File Offset: 0x00003435
			// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000523D File Offset: 0x0000343D
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x060004E0 RID: 1248
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004E1 RID: 1249
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);

			// Token: 0x060004E2 RID: 1250
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004E3 RID: 1251
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004E4 RID: 1252
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004E5 RID: 1253
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x060004E6 RID: 1254
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004E7 RID: 1255
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004E8 RID: 1256
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004E9 RID: 1257
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x060004EA RID: 1258
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004EB RID: 1259
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004EC RID: 1260
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004ED RID: 1261
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x060004EE RID: 1262
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060004EF RID: 1263
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060004F0 RID: 1264
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004F1 RID: 1265
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			// Token: 0x060004F2 RID: 1266
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			// Token: 0x060004F3 RID: 1267
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);

			// Token: 0x04000072 RID: 114
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000028 RID: 40
		public struct SizeBySpeedModule
		{
			// Token: 0x060004F4 RID: 1268 RVA: 0x00005246 File Offset: 0x00003446
			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00005250 File Offset: 0x00003450
			// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00005258 File Offset: 0x00003458
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00005264 File Offset: 0x00003464
			// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0000527A File Offset: 0x0000347A
			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_size_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_size_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00005284 File Offset: 0x00003484
			// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000528C File Offset: 0x0000348C
			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00005298 File Offset: 0x00003498
			// (set) Token: 0x060004FC RID: 1276 RVA: 0x000052AE File Offset: 0x000034AE
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x000052B8 File Offset: 0x000034B8
			// (set) Token: 0x060004FE RID: 1278 RVA: 0x000052C0 File Offset: 0x000034C0
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x000052CC File Offset: 0x000034CC
			// (set) Token: 0x06000500 RID: 1280 RVA: 0x000052E2 File Offset: 0x000034E2
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x000052EC File Offset: 0x000034EC
			// (set) Token: 0x06000502 RID: 1282 RVA: 0x000052F4 File Offset: 0x000034F4
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x00005300 File Offset: 0x00003500
			// (set) Token: 0x06000504 RID: 1284 RVA: 0x00005316 File Offset: 0x00003516
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x00005320 File Offset: 0x00003520
			// (set) Token: 0x06000506 RID: 1286 RVA: 0x00005328 File Offset: 0x00003528
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000507 RID: 1287 RVA: 0x00005331 File Offset: 0x00003531
			// (set) Token: 0x06000508 RID: 1288 RVA: 0x00005339 File Offset: 0x00003539
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000509 RID: 1289 RVA: 0x00005344 File Offset: 0x00003544
			// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000535A File Offset: 0x0000355A
			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.SizeBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x0600050B RID: 1291
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x0600050C RID: 1292
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			// Token: 0x0600050D RID: 1293
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600050E RID: 1294
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600050F RID: 1295
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000510 RID: 1296
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000511 RID: 1297
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000512 RID: 1298
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000513 RID: 1299
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000514 RID: 1300
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000515 RID: 1301
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000516 RID: 1302
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000517 RID: 1303
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x06000518 RID: 1304
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x06000519 RID: 1305
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600051A RID: 1306
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600051B RID: 1307
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x0600051C RID: 1308
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			// Token: 0x0600051D RID: 1309
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			// Token: 0x0600051E RID: 1310
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			// Token: 0x0600051F RID: 1311
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000520 RID: 1312
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x04000073 RID: 115
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000029 RID: 41
		public struct RotationOverLifetimeModule
		{
			// Token: 0x06000521 RID: 1313 RVA: 0x00005364 File Offset: 0x00003564
			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000536E File Offset: 0x0000356E
			// (set) Token: 0x06000523 RID: 1315 RVA: 0x00005376 File Offset: 0x00003576
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000524 RID: 1316 RVA: 0x00005380 File Offset: 0x00003580
			// (set) Token: 0x06000525 RID: 1317 RVA: 0x00005396 File Offset: 0x00003596
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000526 RID: 1318 RVA: 0x000053A0 File Offset: 0x000035A0
			// (set) Token: 0x06000527 RID: 1319 RVA: 0x000053A8 File Offset: 0x000035A8
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000528 RID: 1320 RVA: 0x000053B4 File Offset: 0x000035B4
			// (set) Token: 0x06000529 RID: 1321 RVA: 0x000053CA File Offset: 0x000035CA
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x0600052A RID: 1322 RVA: 0x000053D4 File Offset: 0x000035D4
			// (set) Token: 0x0600052B RID: 1323 RVA: 0x000053DC File Offset: 0x000035DC
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x0600052C RID: 1324 RVA: 0x000053E8 File Offset: 0x000035E8
			// (set) Token: 0x0600052D RID: 1325 RVA: 0x000053FE File Offset: 0x000035FE
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600052E RID: 1326 RVA: 0x00005408 File Offset: 0x00003608
			// (set) Token: 0x0600052F RID: 1327 RVA: 0x00005410 File Offset: 0x00003610
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000530 RID: 1328 RVA: 0x00005419 File Offset: 0x00003619
			// (set) Token: 0x06000531 RID: 1329 RVA: 0x00005421 File Offset: 0x00003621
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x06000532 RID: 1330
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000533 RID: 1331
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);

			// Token: 0x06000534 RID: 1332
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000535 RID: 1333
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000536 RID: 1334
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000537 RID: 1335
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x06000538 RID: 1336
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000539 RID: 1337
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600053A RID: 1338
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x0600053B RID: 1339
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x0600053C RID: 1340
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600053D RID: 1341
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600053E RID: 1342
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x0600053F RID: 1343
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			// Token: 0x06000540 RID: 1344
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			// Token: 0x06000541 RID: 1345
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);

			// Token: 0x04000074 RID: 116
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002A RID: 42
		public struct RotationBySpeedModule
		{
			// Token: 0x06000542 RID: 1346 RVA: 0x0000542A File Offset: 0x0000362A
			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000543 RID: 1347 RVA: 0x00005434 File Offset: 0x00003634
			// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000543C File Offset: 0x0000363C
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000545 RID: 1349 RVA: 0x00005448 File Offset: 0x00003648
			// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000545E File Offset: 0x0000365E
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000547 RID: 1351 RVA: 0x00005468 File Offset: 0x00003668
			// (set) Token: 0x06000548 RID: 1352 RVA: 0x00005470 File Offset: 0x00003670
			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000547C File Offset: 0x0000367C
			// (set) Token: 0x0600054A RID: 1354 RVA: 0x00005492 File Offset: 0x00003692
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000549C File Offset: 0x0000369C
			// (set) Token: 0x0600054C RID: 1356 RVA: 0x000054A4 File Offset: 0x000036A4
			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x000054B0 File Offset: 0x000036B0
			// (set) Token: 0x0600054E RID: 1358 RVA: 0x000054C6 File Offset: 0x000036C6
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600054F RID: 1359 RVA: 0x000054D0 File Offset: 0x000036D0
			// (set) Token: 0x06000550 RID: 1360 RVA: 0x000054D8 File Offset: 0x000036D8
			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000551 RID: 1361 RVA: 0x000054E1 File Offset: 0x000036E1
			// (set) Token: 0x06000552 RID: 1362 RVA: 0x000054E9 File Offset: 0x000036E9
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x000054F4 File Offset: 0x000036F4
			// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000550A File Offset: 0x0000370A
			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.RotationBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000555 RID: 1365
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x06000556 RID: 1366
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			// Token: 0x06000557 RID: 1367
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000558 RID: 1368
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000559 RID: 1369
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x0600055A RID: 1370
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x0600055B RID: 1371
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600055C RID: 1372
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600055D RID: 1373
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x0600055E RID: 1374
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x0600055F RID: 1375
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000560 RID: 1376
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000561 RID: 1377
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x06000562 RID: 1378
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			// Token: 0x06000563 RID: 1379
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			// Token: 0x06000564 RID: 1380
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			// Token: 0x06000565 RID: 1381
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000566 RID: 1382
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref Vector2 value);

			// Token: 0x04000075 RID: 117
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002B RID: 43
		public struct ExternalForcesModule
		{
			// Token: 0x06000567 RID: 1383 RVA: 0x00005514 File Offset: 0x00003714
			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000551E File Offset: 0x0000371E
			// (set) Token: 0x06000569 RID: 1385 RVA: 0x00005526 File Offset: 0x00003726
			public bool enabled
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000552F File Offset: 0x0000372F
			// (set) Token: 0x0600056B RID: 1387 RVA: 0x00005537 File Offset: 0x00003737
			public float multiplier
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_multiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x00005540 File Offset: 0x00003740
			// (set) Token: 0x0600056D RID: 1389 RVA: 0x00005556 File Offset: 0x00003756
			public ParticleSystem.MinMaxCurve multiplierCurve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ExternalForcesModule.get_multiplierCurve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplierCurve_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x00005560 File Offset: 0x00003760
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x00005568 File Offset: 0x00003768
			public ParticleSystemGameObjectFilter influenceFilter
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceFilter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceFilter_Injected(ref this, value);
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x00005574 File Offset: 0x00003774
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000558A File Offset: 0x0000378A
			public LayerMask influenceMask
			{
				get
				{
					LayerMask result;
					ParticleSystem.ExternalForcesModule.get_influenceMask_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceMask_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x00005594 File Offset: 0x00003794
			[NativeThrows]
			public int influenceCount
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceCount_Injected(ref this);
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x0000559C File Offset: 0x0000379C
			public bool IsAffectedBy(ParticleSystemForceField field)
			{
				return ParticleSystem.ExternalForcesModule.IsAffectedBy_Injected(ref this, field);
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x000055A5 File Offset: 0x000037A5
			[NativeThrows]
			public void AddInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.AddInfluence_Injected(ref this, field);
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x000055AE File Offset: 0x000037AE
			[NativeThrows]
			private void RemoveInfluenceAtIndex(int index)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluenceAtIndex_Injected(ref this, index);
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x000055B7 File Offset: 0x000037B7
			public void RemoveInfluence(int index)
			{
				this.RemoveInfluenceAtIndex(index);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x000055C2 File Offset: 0x000037C2
			[NativeThrows]
			public void RemoveInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluence_Injected(ref this, field);
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x000055CB File Offset: 0x000037CB
			[NativeThrows]
			public void RemoveAllInfluences()
			{
				ParticleSystem.ExternalForcesModule.RemoveAllInfluences_Injected(ref this);
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x000055D3 File Offset: 0x000037D3
			[NativeThrows]
			public void SetInfluence(int index, [NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.SetInfluence_Injected(ref this, index, field);
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x000055DD File Offset: 0x000037DD
			[NativeThrows]
			public ParticleSystemForceField GetInfluence(int index)
			{
				return ParticleSystem.ExternalForcesModule.GetInfluence_Injected(ref this, index);
			}

			// Token: 0x0600057B RID: 1403
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x0600057C RID: 1404
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, bool value);

			// Token: 0x0600057D RID: 1405
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x0600057E RID: 1406
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, float value);

			// Token: 0x0600057F RID: 1407
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000580 RID: 1408
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000581 RID: 1409
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemGameObjectFilter get_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x06000582 RID: 1410
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemGameObjectFilter value);

			// Token: 0x06000583 RID: 1411
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out LayerMask ret);

			// Token: 0x06000584 RID: 1412
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref LayerMask value);

			// Token: 0x06000585 RID: 1413
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_influenceCount_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x06000586 RID: 1414
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool IsAffectedBy_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x06000587 RID: 1415
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x06000588 RID: 1416
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluenceAtIndex_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			// Token: 0x06000589 RID: 1417
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			// Token: 0x0600058A RID: 1418
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveAllInfluences_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			// Token: 0x0600058B RID: 1419
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index, ParticleSystemForceField field);

			// Token: 0x0600058C RID: 1420
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemForceField GetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			// Token: 0x04000076 RID: 118
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002C RID: 44
		public struct NoiseModule
		{
			// Token: 0x0600058D RID: 1421 RVA: 0x000055E6 File Offset: 0x000037E6
			internal NoiseModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x000055F0 File Offset: 0x000037F0
			// (set) Token: 0x0600058F RID: 1423 RVA: 0x000055F8 File Offset: 0x000037F8
			public bool enabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x00005601 File Offset: 0x00003801
			// (set) Token: 0x06000591 RID: 1425 RVA: 0x00005609 File Offset: 0x00003809
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.NoiseModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_separateAxes_Injected(ref this, value);
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000592 RID: 1426 RVA: 0x00005614 File Offset: 0x00003814
			// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000562A File Offset: 0x0000382A
			[NativeName("StrengthX")]
			public ParticleSystem.MinMaxCurve strength
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strength_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strength_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000594 RID: 1428 RVA: 0x00005634 File Offset: 0x00003834
			// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000563C File Offset: 0x0000383C
			[NativeName("StrengthXMultiplier")]
			public float strengthMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x00005648 File Offset: 0x00003848
			// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000565E File Offset: 0x0000385E
			public ParticleSystem.MinMaxCurve strengthX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthX_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x00005668 File Offset: 0x00003868
			// (set) Token: 0x06000599 RID: 1433 RVA: 0x00005670 File Offset: 0x00003870
			public float strengthXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000567C File Offset: 0x0000387C
			// (set) Token: 0x0600059B RID: 1435 RVA: 0x00005692 File Offset: 0x00003892
			public ParticleSystem.MinMaxCurve strengthY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000569C File Offset: 0x0000389C
			// (set) Token: 0x0600059D RID: 1437 RVA: 0x000056A4 File Offset: 0x000038A4
			public float strengthYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x000056B0 File Offset: 0x000038B0
			// (set) Token: 0x0600059F RID: 1439 RVA: 0x000056C6 File Offset: 0x000038C6
			public ParticleSystem.MinMaxCurve strengthZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000056D0 File Offset: 0x000038D0
			// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000056D8 File Offset: 0x000038D8
			public float strengthZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000056E1 File Offset: 0x000038E1
			// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000056E9 File Offset: 0x000038E9
			public float frequency
			{
				get
				{
					return ParticleSystem.NoiseModule.get_frequency_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_frequency_Injected(ref this, value);
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000056F2 File Offset: 0x000038F2
			// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000056FA File Offset: 0x000038FA
			public bool damping
			{
				get
				{
					return ParticleSystem.NoiseModule.get_damping_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_damping_Injected(ref this, value);
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00005703 File Offset: 0x00003903
			// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000570B File Offset: 0x0000390B
			public int octaveCount
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveCount_Injected(ref this, value);
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00005714 File Offset: 0x00003914
			// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000571C File Offset: 0x0000391C
			public float octaveMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x00005725 File Offset: 0x00003925
			// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000572D File Offset: 0x0000392D
			public float octaveScale
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveScale_Injected(ref this, value);
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x00005736 File Offset: 0x00003936
			// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000573E File Offset: 0x0000393E
			public ParticleSystemNoiseQuality quality
			{
				get
				{
					return ParticleSystem.NoiseModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_quality_Injected(ref this, value);
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x00005748 File Offset: 0x00003948
			// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000575E File Offset: 0x0000395E
			public ParticleSystem.MinMaxCurve scrollSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_scrollSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeed_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00005768 File Offset: 0x00003968
			// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00005770 File Offset: 0x00003970
			public float scrollSpeedMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_scrollSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeedMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00005779 File Offset: 0x00003979
			// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00005781 File Offset: 0x00003981
			public bool remapEnabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapEnabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapEnabled_Injected(ref this, value);
				}
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000578C File Offset: 0x0000398C
			// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000057A2 File Offset: 0x000039A2
			[NativeName("RemapX")]
			public ParticleSystem.MinMaxCurve remap
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remap_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remap_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000057AC File Offset: 0x000039AC
			// (set) Token: 0x060005B7 RID: 1463 RVA: 0x000057B4 File Offset: 0x000039B4
			[NativeName("RemapXMultiplier")]
			public float remapMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000057C0 File Offset: 0x000039C0
			// (set) Token: 0x060005B9 RID: 1465 RVA: 0x000057D6 File Offset: 0x000039D6
			public ParticleSystem.MinMaxCurve remapX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapX_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060005BA RID: 1466 RVA: 0x000057E0 File Offset: 0x000039E0
			// (set) Token: 0x060005BB RID: 1467 RVA: 0x000057E8 File Offset: 0x000039E8
			public float remapXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapXMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060005BC RID: 1468 RVA: 0x000057F4 File Offset: 0x000039F4
			// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000580A File Offset: 0x00003A0A
			public ParticleSystem.MinMaxCurve remapY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapY_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x00005814 File Offset: 0x00003A14
			// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000581C File Offset: 0x00003A1C
			public float remapYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapYMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00005828 File Offset: 0x00003A28
			// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000583E File Offset: 0x00003A3E
			public ParticleSystem.MinMaxCurve remapZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZ_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00005848 File Offset: 0x00003A48
			// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00005850 File Offset: 0x00003A50
			public float remapZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000585C File Offset: 0x00003A5C
			// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00005872 File Offset: 0x00003A72
			public ParticleSystem.MinMaxCurve positionAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_positionAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_positionAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000587C File Offset: 0x00003A7C
			// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00005892 File Offset: 0x00003A92
			public ParticleSystem.MinMaxCurve rotationAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_rotationAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_rotationAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000589C File Offset: 0x00003A9C
			// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000058B2 File Offset: 0x00003AB2
			public ParticleSystem.MinMaxCurve sizeAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_sizeAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_sizeAmount_Injected(ref this, ref value);
				}
			}

			// Token: 0x060005CA RID: 1482
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005CB RID: 1483
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x060005CC RID: 1484
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005CD RID: 1485
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x060005CE RID: 1486
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005CF RID: 1487
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005D0 RID: 1488
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005D1 RID: 1489
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005D2 RID: 1490
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005D3 RID: 1491
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005D4 RID: 1492
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005D5 RID: 1493
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005D6 RID: 1494
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005D7 RID: 1495
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005D8 RID: 1496
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005D9 RID: 1497
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005DA RID: 1498
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005DB RID: 1499
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005DC RID: 1500
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005DD RID: 1501
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005DE RID: 1502
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005DF RID: 1503
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005E0 RID: 1504
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_damping_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005E1 RID: 1505
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_damping_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x060005E2 RID: 1506
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005E3 RID: 1507
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self, int value);

			// Token: 0x060005E4 RID: 1508
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005E5 RID: 1509
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005E6 RID: 1510
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005E7 RID: 1511
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005E8 RID: 1512
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemNoiseQuality get_quality_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005E9 RID: 1513
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_quality_Injected(ref ParticleSystem.NoiseModule _unity_self, ParticleSystemNoiseQuality value);

			// Token: 0x060005EA RID: 1514
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005EB RID: 1515
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005EC RID: 1516
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005ED RID: 1517
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005EE RID: 1518
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005EF RID: 1519
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			// Token: 0x060005F0 RID: 1520
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005F1 RID: 1521
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005F2 RID: 1522
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005F3 RID: 1523
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005F4 RID: 1524
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005F5 RID: 1525
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005F6 RID: 1526
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005F7 RID: 1527
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005F8 RID: 1528
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005F9 RID: 1529
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005FA RID: 1530
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005FB RID: 1531
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x060005FC RID: 1532
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x060005FD RID: 1533
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x060005FE RID: 1534
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			// Token: 0x060005FF RID: 1535
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			// Token: 0x06000600 RID: 1536
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000601 RID: 1537
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000602 RID: 1538
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000603 RID: 1539
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000604 RID: 1540
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000605 RID: 1541
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x04000077 RID: 119
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002D RID: 45
		public struct LightsModule
		{
			// Token: 0x06000606 RID: 1542 RVA: 0x000058BC File Offset: 0x00003ABC
			internal LightsModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x000058C6 File Offset: 0x00003AC6
			// (set) Token: 0x06000608 RID: 1544 RVA: 0x000058CE File Offset: 0x00003ACE
			public bool enabled
			{
				get
				{
					return ParticleSystem.LightsModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x000058D7 File Offset: 0x00003AD7
			// (set) Token: 0x0600060A RID: 1546 RVA: 0x000058DF File Offset: 0x00003ADF
			public float ratio
			{
				get
				{
					return ParticleSystem.LightsModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_ratio_Injected(ref this, value);
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x000058E8 File Offset: 0x00003AE8
			// (set) Token: 0x0600060C RID: 1548 RVA: 0x000058F0 File Offset: 0x00003AF0
			public bool useRandomDistribution
			{
				get
				{
					return ParticleSystem.LightsModule.get_useRandomDistribution_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useRandomDistribution_Injected(ref this, value);
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x000058F9 File Offset: 0x00003AF9
			// (set) Token: 0x0600060E RID: 1550 RVA: 0x00005901 File Offset: 0x00003B01
			public Light light
			{
				get
				{
					return ParticleSystem.LightsModule.get_light_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_light_Injected(ref this, value);
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000590A File Offset: 0x00003B0A
			// (set) Token: 0x06000610 RID: 1552 RVA: 0x00005912 File Offset: 0x00003B12
			public bool useParticleColor
			{
				get
				{
					return ParticleSystem.LightsModule.get_useParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useParticleColor_Injected(ref this, value);
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000591B File Offset: 0x00003B1B
			// (set) Token: 0x06000612 RID: 1554 RVA: 0x00005923 File Offset: 0x00003B23
			public bool sizeAffectsRange
			{
				get
				{
					return ParticleSystem.LightsModule.get_sizeAffectsRange_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_sizeAffectsRange_Injected(ref this, value);
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000592C File Offset: 0x00003B2C
			// (set) Token: 0x06000614 RID: 1556 RVA: 0x00005934 File Offset: 0x00003B34
			public bool alphaAffectsIntensity
			{
				get
				{
					return ParticleSystem.LightsModule.get_alphaAffectsIntensity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_alphaAffectsIntensity_Injected(ref this, value);
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000615 RID: 1557 RVA: 0x00005940 File Offset: 0x00003B40
			// (set) Token: 0x06000616 RID: 1558 RVA: 0x00005956 File Offset: 0x00003B56
			public ParticleSystem.MinMaxCurve range
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LightsModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000617 RID: 1559 RVA: 0x00005960 File Offset: 0x00003B60
			// (set) Token: 0x06000618 RID: 1560 RVA: 0x00005968 File Offset: 0x00003B68
			public float rangeMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_rangeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_rangeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000619 RID: 1561 RVA: 0x00005974 File Offset: 0x00003B74
			// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000598A File Offset: 0x00003B8A
			public ParticleSystem.MinMaxCurve intensity
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LightsModule.get_intensity_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensity_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600061B RID: 1563 RVA: 0x00005994 File Offset: 0x00003B94
			// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000599C File Offset: 0x00003B9C
			public float intensityMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_intensityMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensityMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x000059A5 File Offset: 0x00003BA5
			// (set) Token: 0x0600061E RID: 1566 RVA: 0x000059AD File Offset: 0x00003BAD
			public int maxLights
			{
				get
				{
					return ParticleSystem.LightsModule.get_maxLights_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_maxLights_Injected(ref this, value);
				}
			}

			// Token: 0x0600061F RID: 1567
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000620 RID: 1568
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000621 RID: 1569
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_ratio_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000622 RID: 1570
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ratio_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x06000623 RID: 1571
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000624 RID: 1572
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000625 RID: 1573
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Light get_light_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000626 RID: 1574
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_light_Injected(ref ParticleSystem.LightsModule _unity_self, Light value);

			// Token: 0x06000627 RID: 1575
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000628 RID: 1576
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x06000629 RID: 1577
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x0600062A RID: 1578
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x0600062B RID: 1579
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x0600062C RID: 1580
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			// Token: 0x0600062D RID: 1581
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600062E RID: 1582
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600062F RID: 1583
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000630 RID: 1584
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x06000631 RID: 1585
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000632 RID: 1586
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x06000633 RID: 1587
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000634 RID: 1588
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			// Token: 0x06000635 RID: 1589
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000636 RID: 1590
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self, int value);

			// Token: 0x04000078 RID: 120
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002E RID: 46
		public struct TrailModule
		{
			// Token: 0x06000637 RID: 1591 RVA: 0x000059B6 File Offset: 0x00003BB6
			internal TrailModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x000059C0 File Offset: 0x00003BC0
			// (set) Token: 0x06000639 RID: 1593 RVA: 0x000059C8 File Offset: 0x00003BC8
			public bool enabled
			{
				get
				{
					return ParticleSystem.TrailModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x000059D1 File Offset: 0x00003BD1
			// (set) Token: 0x0600063B RID: 1595 RVA: 0x000059D9 File Offset: 0x00003BD9
			public ParticleSystemTrailMode mode
			{
				get
				{
					return ParticleSystem.TrailModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_mode_Injected(ref this, value);
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x000059E2 File Offset: 0x00003BE2
			// (set) Token: 0x0600063D RID: 1597 RVA: 0x000059EA File Offset: 0x00003BEA
			public float ratio
			{
				get
				{
					return ParticleSystem.TrailModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ratio_Injected(ref this, value);
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x000059F4 File Offset: 0x00003BF4
			// (set) Token: 0x0600063F RID: 1599 RVA: 0x00005A0A File Offset: 0x00003C0A
			public ParticleSystem.MinMaxCurve lifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TrailModule.get_lifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000640 RID: 1600 RVA: 0x00005A14 File Offset: 0x00003C14
			// (set) Token: 0x06000641 RID: 1601 RVA: 0x00005A1C File Offset: 0x00003C1C
			public float lifetimeMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_lifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetimeMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x00005A25 File Offset: 0x00003C25
			// (set) Token: 0x06000643 RID: 1603 RVA: 0x00005A2D File Offset: 0x00003C2D
			public float minVertexDistance
			{
				get
				{
					return ParticleSystem.TrailModule.get_minVertexDistance_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_minVertexDistance_Injected(ref this, value);
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x00005A36 File Offset: 0x00003C36
			// (set) Token: 0x06000645 RID: 1605 RVA: 0x00005A3E File Offset: 0x00003C3E
			public ParticleSystemTrailTextureMode textureMode
			{
				get
				{
					return ParticleSystem.TrailModule.get_textureMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_textureMode_Injected(ref this, value);
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000646 RID: 1606 RVA: 0x00005A47 File Offset: 0x00003C47
			// (set) Token: 0x06000647 RID: 1607 RVA: 0x00005A4F File Offset: 0x00003C4F
			public bool worldSpace
			{
				get
				{
					return ParticleSystem.TrailModule.get_worldSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_worldSpace_Injected(ref this, value);
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000648 RID: 1608 RVA: 0x00005A58 File Offset: 0x00003C58
			// (set) Token: 0x06000649 RID: 1609 RVA: 0x00005A60 File Offset: 0x00003C60
			public bool dieWithParticles
			{
				get
				{
					return ParticleSystem.TrailModule.get_dieWithParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_dieWithParticles_Injected(ref this, value);
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x0600064A RID: 1610 RVA: 0x00005A69 File Offset: 0x00003C69
			// (set) Token: 0x0600064B RID: 1611 RVA: 0x00005A71 File Offset: 0x00003C71
			public bool sizeAffectsWidth
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsWidth_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsWidth_Injected(ref this, value);
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x00005A7A File Offset: 0x00003C7A
			// (set) Token: 0x0600064D RID: 1613 RVA: 0x00005A82 File Offset: 0x00003C82
			public bool sizeAffectsLifetime
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsLifetime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsLifetime_Injected(ref this, value);
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x00005A8B File Offset: 0x00003C8B
			// (set) Token: 0x0600064F RID: 1615 RVA: 0x00005A93 File Offset: 0x00003C93
			public bool inheritParticleColor
			{
				get
				{
					return ParticleSystem.TrailModule.get_inheritParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_inheritParticleColor_Injected(ref this, value);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000650 RID: 1616 RVA: 0x00005A9C File Offset: 0x00003C9C
			// (set) Token: 0x06000651 RID: 1617 RVA: 0x00005AB2 File Offset: 0x00003CB2
			public ParticleSystem.MinMaxGradient colorOverLifetime
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.TrailModule.get_colorOverLifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverLifetime_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000652 RID: 1618 RVA: 0x00005ABC File Offset: 0x00003CBC
			// (set) Token: 0x06000653 RID: 1619 RVA: 0x00005AD2 File Offset: 0x00003CD2
			public ParticleSystem.MinMaxCurve widthOverTrail
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TrailModule.get_widthOverTrail_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrail_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000654 RID: 1620 RVA: 0x00005ADC File Offset: 0x00003CDC
			// (set) Token: 0x06000655 RID: 1621 RVA: 0x00005AE4 File Offset: 0x00003CE4
			public float widthOverTrailMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_widthOverTrailMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrailMultiplier_Injected(ref this, value);
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000656 RID: 1622 RVA: 0x00005AF0 File Offset: 0x00003CF0
			// (set) Token: 0x06000657 RID: 1623 RVA: 0x00005B06 File Offset: 0x00003D06
			public ParticleSystem.MinMaxGradient colorOverTrail
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.TrailModule.get_colorOverTrail_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverTrail_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000658 RID: 1624 RVA: 0x00005B10 File Offset: 0x00003D10
			// (set) Token: 0x06000659 RID: 1625 RVA: 0x00005B18 File Offset: 0x00003D18
			public bool generateLightingData
			{
				get
				{
					return ParticleSystem.TrailModule.get_generateLightingData_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_generateLightingData_Injected(ref this, value);
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600065A RID: 1626 RVA: 0x00005B21 File Offset: 0x00003D21
			// (set) Token: 0x0600065B RID: 1627 RVA: 0x00005B29 File Offset: 0x00003D29
			public int ribbonCount
			{
				get
				{
					return ParticleSystem.TrailModule.get_ribbonCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ribbonCount_Injected(ref this, value);
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x00005B32 File Offset: 0x00003D32
			// (set) Token: 0x0600065D RID: 1629 RVA: 0x00005B3A File Offset: 0x00003D3A
			public float shadowBias
			{
				get
				{
					return ParticleSystem.TrailModule.get_shadowBias_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_shadowBias_Injected(ref this, value);
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x0600065E RID: 1630 RVA: 0x00005B43 File Offset: 0x00003D43
			// (set) Token: 0x0600065F RID: 1631 RVA: 0x00005B4B File Offset: 0x00003D4B
			public bool splitSubEmitterRibbons
			{
				get
				{
					return ParticleSystem.TrailModule.get_splitSubEmitterRibbons_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_splitSubEmitterRibbons_Injected(ref this, value);
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000660 RID: 1632 RVA: 0x00005B54 File Offset: 0x00003D54
			// (set) Token: 0x06000661 RID: 1633 RVA: 0x00005B5C File Offset: 0x00003D5C
			public bool attachRibbonsToTransform
			{
				get
				{
					return ParticleSystem.TrailModule.get_attachRibbonsToTransform_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_attachRibbonsToTransform_Injected(ref this, value);
				}
			}

			// Token: 0x06000662 RID: 1634
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000663 RID: 1635
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000664 RID: 1636
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemTrailMode get_mode_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000665 RID: 1637
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailMode value);

			// Token: 0x06000666 RID: 1638
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_ratio_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000667 RID: 1639
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ratio_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000668 RID: 1640
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x06000669 RID: 1641
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600066A RID: 1642
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600066B RID: 1643
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x0600066C RID: 1644
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600066D RID: 1645
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x0600066E RID: 1646
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemTrailTextureMode get_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600066F RID: 1647
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailTextureMode value);

			// Token: 0x06000670 RID: 1648
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000671 RID: 1649
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000672 RID: 1650
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000673 RID: 1651
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000674 RID: 1652
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000675 RID: 1653
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000676 RID: 1654
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000677 RID: 1655
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000678 RID: 1656
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000679 RID: 1657
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600067A RID: 1658
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x0600067B RID: 1659
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x0600067C RID: 1660
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600067D RID: 1661
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			// Token: 0x0600067E RID: 1662
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600067F RID: 1663
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000680 RID: 1664
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x06000681 RID: 1665
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			// Token: 0x06000682 RID: 1666
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000683 RID: 1667
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x06000684 RID: 1668
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000685 RID: 1669
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self, int value);

			// Token: 0x06000686 RID: 1670
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000687 RID: 1671
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			// Token: 0x06000688 RID: 1672
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x06000689 RID: 1673
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x0600068A RID: 1674
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self);

			// Token: 0x0600068B RID: 1675
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			// Token: 0x04000079 RID: 121
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002F RID: 47
		public struct CustomDataModule
		{
			// Token: 0x0600068C RID: 1676 RVA: 0x00005B65 File Offset: 0x00003D65
			internal CustomDataModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x0600068D RID: 1677 RVA: 0x00005B6F File Offset: 0x00003D6F
			// (set) Token: 0x0600068E RID: 1678 RVA: 0x00005B77 File Offset: 0x00003D77
			public bool enabled
			{
				get
				{
					return ParticleSystem.CustomDataModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CustomDataModule.set_enabled_Injected(ref this, value);
				}
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x00005B80 File Offset: 0x00003D80
			[NativeThrows]
			public void SetMode(ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode)
			{
				ParticleSystem.CustomDataModule.SetMode_Injected(ref this, stream, mode);
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x00005B8A File Offset: 0x00003D8A
			[NativeThrows]
			public ParticleSystemCustomDataMode GetMode(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetMode_Injected(ref this, stream);
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x00005B93 File Offset: 0x00003D93
			[NativeThrows]
			public void SetVectorComponentCount(ParticleSystemCustomData stream, int count)
			{
				ParticleSystem.CustomDataModule.SetVectorComponentCount_Injected(ref this, stream, count);
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x00005B9D File Offset: 0x00003D9D
			[NativeThrows]
			public int GetVectorComponentCount(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetVectorComponentCount_Injected(ref this, stream);
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x00005BA6 File Offset: 0x00003DA6
			[NativeThrows]
			public void SetVector(ParticleSystemCustomData stream, int component, ParticleSystem.MinMaxCurve curve)
			{
				ParticleSystem.CustomDataModule.SetVector_Injected(ref this, stream, component, ref curve);
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x00005BB4 File Offset: 0x00003DB4
			[NativeThrows]
			public ParticleSystem.MinMaxCurve GetVector(ParticleSystemCustomData stream, int component)
			{
				ParticleSystem.MinMaxCurve result;
				ParticleSystem.CustomDataModule.GetVector_Injected(ref this, stream, component, out result);
				return result;
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x00005BCC File Offset: 0x00003DCC
			[NativeThrows]
			public void SetColor(ParticleSystemCustomData stream, ParticleSystem.MinMaxGradient gradient)
			{
				ParticleSystem.CustomDataModule.SetColor_Injected(ref this, stream, ref gradient);
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x00005BD8 File Offset: 0x00003DD8
			[NativeThrows]
			public ParticleSystem.MinMaxGradient GetColor(ParticleSystemCustomData stream)
			{
				ParticleSystem.MinMaxGradient result;
				ParticleSystem.CustomDataModule.GetColor_Injected(ref this, stream, out result);
				return result;
			}

			// Token: 0x06000697 RID: 1687
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self);

			// Token: 0x06000698 RID: 1688
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self, bool value);

			// Token: 0x06000699 RID: 1689
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode);

			// Token: 0x0600069A RID: 1690
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCustomDataMode GetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			// Token: 0x0600069B RID: 1691
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int count);

			// Token: 0x0600069C RID: 1692
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			// Token: 0x0600069D RID: 1693
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600069E RID: 1694
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, out ParticleSystem.MinMaxCurve ret);

			// Token: 0x0600069F RID: 1695
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x060006A0 RID: 1696
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, out ParticleSystem.MinMaxGradient ret);

			// Token: 0x0400007A RID: 122
			internal ParticleSystem m_ParticleSystem;
		}
	}
}
