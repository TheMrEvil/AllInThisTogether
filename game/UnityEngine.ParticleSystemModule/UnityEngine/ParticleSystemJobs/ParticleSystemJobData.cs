using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000063 RID: 99
	public struct ParticleSystemJobData
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00006786 File Offset: 0x00004986
		public readonly int count
		{
			[CompilerGenerated]
			get
			{
				return this.<count>k__BackingField;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0000678E File Offset: 0x0000498E
		public readonly ParticleSystemNativeArray3 positions
		{
			[CompilerGenerated]
			get
			{
				return this.<positions>k__BackingField;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00006796 File Offset: 0x00004996
		public readonly ParticleSystemNativeArray3 velocities
		{
			[CompilerGenerated]
			get
			{
				return this.<velocities>k__BackingField;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0000679E File Offset: 0x0000499E
		public readonly ParticleSystemNativeArray3 axisOfRotations
		{
			[CompilerGenerated]
			get
			{
				return this.<axisOfRotations>k__BackingField;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000067A6 File Offset: 0x000049A6
		public readonly ParticleSystemNativeArray3 rotations
		{
			[CompilerGenerated]
			get
			{
				return this.<rotations>k__BackingField;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000067AE File Offset: 0x000049AE
		public readonly ParticleSystemNativeArray3 rotationalSpeeds
		{
			[CompilerGenerated]
			get
			{
				return this.<rotationalSpeeds>k__BackingField;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000067B6 File Offset: 0x000049B6
		public readonly ParticleSystemNativeArray3 sizes
		{
			[CompilerGenerated]
			get
			{
				return this.<sizes>k__BackingField;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000067BE File Offset: 0x000049BE
		public readonly NativeArray<Color32> startColors
		{
			[CompilerGenerated]
			get
			{
				return this.<startColors>k__BackingField;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000067C6 File Offset: 0x000049C6
		public readonly NativeArray<float> aliveTimePercent
		{
			[CompilerGenerated]
			get
			{
				return this.<aliveTimePercent>k__BackingField;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000067CE File Offset: 0x000049CE
		public readonly NativeArray<float> inverseStartLifetimes
		{
			[CompilerGenerated]
			get
			{
				return this.<inverseStartLifetimes>k__BackingField;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000067D6 File Offset: 0x000049D6
		public readonly NativeArray<uint> randomSeeds
		{
			[CompilerGenerated]
			get
			{
				return this.<randomSeeds>k__BackingField;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000067DE File Offset: 0x000049DE
		public readonly ParticleSystemNativeArray4 customData1
		{
			[CompilerGenerated]
			get
			{
				return this.<customData1>k__BackingField;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000067E6 File Offset: 0x000049E6
		public readonly ParticleSystemNativeArray4 customData2
		{
			[CompilerGenerated]
			get
			{
				return this.<customData2>k__BackingField;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000067EE File Offset: 0x000049EE
		public readonly NativeArray<int> meshIndices
		{
			[CompilerGenerated]
			get
			{
				return this.<meshIndices>k__BackingField;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000067F8 File Offset: 0x000049F8
		internal ParticleSystemJobData(ref NativeParticleData nativeData)
		{
			this = default(ParticleSystemJobData);
			this.count = nativeData.count;
			this.positions = this.CreateNativeArray3(ref nativeData.positions, this.count);
			this.velocities = this.CreateNativeArray3(ref nativeData.velocities, this.count);
			this.axisOfRotations = this.CreateNativeArray3(ref nativeData.axisOfRotations, this.count);
			this.rotations = this.CreateNativeArray3(ref nativeData.rotations, this.count);
			this.rotationalSpeeds = this.CreateNativeArray3(ref nativeData.rotationalSpeeds, this.count);
			this.sizes = this.CreateNativeArray3(ref nativeData.sizes, this.count);
			this.startColors = this.CreateNativeArray<Color32>(nativeData.startColors, this.count);
			this.aliveTimePercent = this.CreateNativeArray<float>(nativeData.aliveTimePercent, this.count);
			this.inverseStartLifetimes = this.CreateNativeArray<float>(nativeData.inverseStartLifetimes, this.count);
			this.randomSeeds = this.CreateNativeArray<uint>(nativeData.randomSeeds, this.count);
			this.customData1 = this.CreateNativeArray4(ref nativeData.customData1, this.count);
			this.customData2 = this.CreateNativeArray4(ref nativeData.customData2, this.count);
			this.meshIndices = this.CreateNativeArray<int>(nativeData.meshIndices, this.count);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00006954 File Offset: 0x00004B54
		internal unsafe NativeArray<T> CreateNativeArray<T>(void* src, int count) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(src, count, Allocator.Invalid);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00006970 File Offset: 0x00004B70
		internal unsafe ParticleSystemNativeArray3 CreateNativeArray3(ref NativeParticleData.Array3 ptrs, int count)
		{
			return new ParticleSystemNativeArray3
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count)
			};
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000069C8 File Offset: 0x00004BC8
		internal unsafe ParticleSystemNativeArray4 CreateNativeArray4(ref NativeParticleData.Array4 ptrs, int count)
		{
			return new ParticleSystemNativeArray4
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count),
				w = this.CreateNativeArray<float>((void*)ptrs.w, count)
			};
		}

		// Token: 0x0400017F RID: 383
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <count>k__BackingField;

		// Token: 0x04000180 RID: 384
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <positions>k__BackingField;

		// Token: 0x04000181 RID: 385
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <velocities>k__BackingField;

		// Token: 0x04000182 RID: 386
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <axisOfRotations>k__BackingField;

		// Token: 0x04000183 RID: 387
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <rotations>k__BackingField;

		// Token: 0x04000184 RID: 388
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <rotationalSpeeds>k__BackingField;

		// Token: 0x04000185 RID: 389
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray3 <sizes>k__BackingField;

		// Token: 0x04000186 RID: 390
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly NativeArray<Color32> <startColors>k__BackingField;

		// Token: 0x04000187 RID: 391
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly NativeArray<float> <aliveTimePercent>k__BackingField;

		// Token: 0x04000188 RID: 392
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly NativeArray<float> <inverseStartLifetimes>k__BackingField;

		// Token: 0x04000189 RID: 393
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly NativeArray<uint> <randomSeeds>k__BackingField;

		// Token: 0x0400018A RID: 394
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ParticleSystemNativeArray4 <customData1>k__BackingField;

		// Token: 0x0400018B RID: 395
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ParticleSystemNativeArray4 <customData2>k__BackingField;

		// Token: 0x0400018C RID: 396
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly NativeArray<int> <meshIndices>k__BackingField;
	}
}
