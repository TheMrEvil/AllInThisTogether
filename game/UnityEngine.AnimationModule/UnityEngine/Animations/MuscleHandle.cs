using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200006B RID: 107
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/MuscleHandle.h")]
	public struct MuscleHandle
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00008181 File Offset: 0x00006381
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00008189 File Offset: 0x00006389
		public HumanPartDof humanPartDof
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<humanPartDof>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<humanPartDof>k__BackingField = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00008192 File Offset: 0x00006392
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000819A File Offset: 0x0000639A
		public int dof
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<dof>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dof>k__BackingField = value;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000081A3 File Offset: 0x000063A3
		public MuscleHandle(BodyDof bodyDof)
		{
			this.humanPartDof = HumanPartDof.Body;
			this.dof = (int)bodyDof;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000081B6 File Offset: 0x000063B6
		public MuscleHandle(HeadDof headDof)
		{
			this.humanPartDof = HumanPartDof.Head;
			this.dof = (int)headDof;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000081CC File Offset: 0x000063CC
		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			bool flag = partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)legDof;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00008208 File Offset: 0x00006408
		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			bool flag = partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)armDof;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00008244 File Offset: 0x00006444
		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			bool flag = partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)fingerDof;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00008280 File Offset: 0x00006480
		public string name
		{
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00008298 File Offset: 0x00006498
		public static int muscleHandleCount
		{
			get
			{
				return MuscleHandle.GetMuscleHandleCount();
			}
		}

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetMuscleHandles([NotNull("ArgumentNullException")] [Out] MuscleHandle[] muscleHandles);

		// Token: 0x06000620 RID: 1568 RVA: 0x000082AF File Offset: 0x000064AF
		private string GetName()
		{
			return MuscleHandle.GetName_Injected(ref this);
		}

		// Token: 0x06000621 RID: 1569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMuscleHandleCount();

		// Token: 0x06000622 RID: 1570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_Injected(ref MuscleHandle _unity_self);

		// Token: 0x04000183 RID: 387
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private HumanPartDof <humanPartDof>k__BackingField;

		// Token: 0x04000184 RID: 388
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <dof>k__BackingField;
	}
}
