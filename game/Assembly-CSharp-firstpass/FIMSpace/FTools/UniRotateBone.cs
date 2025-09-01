using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200005D RID: 93
	public class UniRotateBone
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00017C60 File Offset: 0x00015E60
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00017C68 File Offset: 0x00015E68
		public Transform transform
		{
			[CompilerGenerated]
			get
			{
				return this.<transform>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<transform>k__BackingField = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00017C71 File Offset: 0x00015E71
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00017C79 File Offset: 0x00015E79
		public Vector3 initialLocalPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<initialLocalPosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<initialLocalPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00017C82 File Offset: 0x00015E82
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00017C8A File Offset: 0x00015E8A
		public Quaternion initialLocalRotation
		{
			[CompilerGenerated]
			get
			{
				return this.<initialLocalRotation>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<initialLocalRotation>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00017C93 File Offset: 0x00015E93
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00017C9B File Offset: 0x00015E9B
		public Vector3 initialLocalPositionInRootSpace
		{
			[CompilerGenerated]
			get
			{
				return this.<initialLocalPositionInRootSpace>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<initialLocalPositionInRootSpace>k__BackingField = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00017CA4 File Offset: 0x00015EA4
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00017CAC File Offset: 0x00015EAC
		public Quaternion initialLocalRotationInRootSpace
		{
			[CompilerGenerated]
			get
			{
				return this.<initialLocalRotationInRootSpace>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<initialLocalRotationInRootSpace>k__BackingField = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00017CB5 File Offset: 0x00015EB5
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00017CBD File Offset: 0x00015EBD
		public Vector3 right
		{
			[CompilerGenerated]
			get
			{
				return this.<right>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<right>k__BackingField = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00017CC6 File Offset: 0x00015EC6
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00017CCE File Offset: 0x00015ECE
		public Vector3 up
		{
			[CompilerGenerated]
			get
			{
				return this.<up>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<up>k__BackingField = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00017CD7 File Offset: 0x00015ED7
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00017CDF File Offset: 0x00015EDF
		public Vector3 forward
		{
			[CompilerGenerated]
			get
			{
				return this.<forward>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<forward>k__BackingField = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00017CE8 File Offset: 0x00015EE8
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public Vector3 dright
		{
			[CompilerGenerated]
			get
			{
				return this.<dright>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<dright>k__BackingField = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00017CF9 File Offset: 0x00015EF9
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00017D01 File Offset: 0x00015F01
		public Vector3 dup
		{
			[CompilerGenerated]
			get
			{
				return this.<dup>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<dup>k__BackingField = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00017D0A File Offset: 0x00015F0A
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00017D12 File Offset: 0x00015F12
		public Vector3 dforward
		{
			[CompilerGenerated]
			get
			{
				return this.<dforward>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<dforward>k__BackingField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00017D1B File Offset: 0x00015F1B
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00017D23 File Offset: 0x00015F23
		public Vector3 fromParentForward
		{
			[CompilerGenerated]
			get
			{
				return this.<fromParentForward>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<fromParentForward>k__BackingField = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00017D2C File Offset: 0x00015F2C
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00017D34 File Offset: 0x00015F34
		public Vector3 fromParentCross
		{
			[CompilerGenerated]
			get
			{
				return this.<fromParentCross>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<fromParentCross>k__BackingField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00017D3D File Offset: 0x00015F3D
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00017D45 File Offset: 0x00015F45
		public Vector3 keyframedPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<keyframedPosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<keyframedPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00017D4E File Offset: 0x00015F4E
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00017D56 File Offset: 0x00015F56
		public Quaternion keyframedRotation
		{
			[CompilerGenerated]
			get
			{
				return this.<keyframedRotation>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<keyframedRotation>k__BackingField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00017D5F File Offset: 0x00015F5F
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00017D67 File Offset: 0x00015F67
		public Quaternion mapping
		{
			[CompilerGenerated]
			get
			{
				return this.<mapping>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<mapping>k__BackingField = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00017D70 File Offset: 0x00015F70
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00017D78 File Offset: 0x00015F78
		public Quaternion dmapping
		{
			[CompilerGenerated]
			get
			{
				return this.<dmapping>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<dmapping>k__BackingField = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00017D81 File Offset: 0x00015F81
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00017D89 File Offset: 0x00015F89
		public Transform root
		{
			[CompilerGenerated]
			get
			{
				return this.<root>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<root>k__BackingField = value;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00017D94 File Offset: 0x00015F94
		public UniRotateBone(Transform t, Transform root)
		{
			this.transform = t;
			this.initialLocalPosition = this.transform.localPosition;
			this.initialLocalRotation = this.transform.localRotation;
			if (root)
			{
				this.initialLocalPositionInRootSpace = root.InverseTransformPoint(t.position);
				this.initialLocalRotationInRootSpace = root.rotation.QToLocal(t.rotation);
			}
			this.forward = this.transform.InverseTransformDirection(root.forward);
			this.up = this.transform.InverseTransformDirection(root.up);
			this.right = this.transform.InverseTransformDirection(root.right);
			this.dforward = Quaternion.FromToRotation(this.forward, Vector3.forward) * Vector3.forward;
			this.dup = Quaternion.FromToRotation(this.up, Vector3.up) * Vector3.up;
			this.dright = Quaternion.FromToRotation(this.right, Vector3.right) * Vector3.right;
			if (t.parent)
			{
				this.fromParentForward = this.GetFromParentForward().normalized;
			}
			else
			{
				this.fromParentForward = this.forward;
			}
			this.fromParentCross = -Vector3.Cross(this.fromParentForward, this.forward);
			this.mapping = Quaternion.FromToRotation(this.right, Vector3.right);
			this.mapping *= Quaternion.FromToRotation(this.up, Vector3.up);
			this.dmapping = Quaternion.FromToRotation(this.fromParentForward, Vector3.right);
			this.dmapping *= Quaternion.FromToRotation(this.up, Vector3.up);
			this.root = root;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00017F71 File Offset: 0x00016171
		public Vector3 GetFromParentForward()
		{
			return this.transform.InverseTransformDirection(this.transform.position - this.transform.parent.position);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00017F9E File Offset: 0x0001619E
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00017FA6 File Offset: 0x000161A6
		public Vector3 forwardReference
		{
			[CompilerGenerated]
			get
			{
				return this.<forwardReference>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<forwardReference>k__BackingField = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00017FAF File Offset: 0x000161AF
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00017FB7 File Offset: 0x000161B7
		public Vector3 upReference
		{
			[CompilerGenerated]
			get
			{
				return this.<upReference>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<upReference>k__BackingField = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00017FC0 File Offset: 0x000161C0
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00017FC8 File Offset: 0x000161C8
		public Vector3 rightCrossReference
		{
			[CompilerGenerated]
			get
			{
				return this.<rightCrossReference>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<rightCrossReference>k__BackingField = value;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00017FD4 File Offset: 0x000161D4
		public Quaternion GetRootCompensateRotation(Quaternion initPelvisInWorld, Quaternion currInWorld, float armsRootCompensate)
		{
			Quaternion quaternion;
			if (armsRootCompensate > 0f)
			{
				quaternion = currInWorld.QToLocal(this.transform.parent.rotation);
				quaternion = initPelvisInWorld.QToWorld(quaternion);
				if (armsRootCompensate < 1f)
				{
					quaternion = Quaternion.Lerp(this.transform.parent.rotation, quaternion, armsRootCompensate);
				}
			}
			else
			{
				quaternion = this.transform.parent.rotation;
			}
			return quaternion;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001803C File Offset: 0x0001623C
		public void RefreshCustomAxis(Vector3 up, Vector3 forward)
		{
			if (this.transform == null)
			{
				return;
			}
			this.forwardReference = Quaternion.Inverse(this.transform.parent.rotation) * this.root.rotation * forward;
			this.upReference = Quaternion.Inverse(this.transform.parent.rotation) * this.root.rotation * up;
			this.rightCrossReference = Vector3.Cross(this.upReference, this.forwardReference);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000180D4 File Offset: 0x000162D4
		public void RefreshCustomAxis(Vector3 up, Vector3 forward, Quaternion customParentRot)
		{
			this.forwardReference = Quaternion.Inverse(customParentRot) * this.root.rotation * forward;
			this.upReference = Quaternion.Inverse(customParentRot) * this.root.rotation * up;
			this.rightCrossReference = Vector3.Cross(this.upReference, this.forwardReference);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001813C File Offset: 0x0001633C
		public Quaternion RotateCustomAxis(float x, float y, UniRotateBone oRef)
		{
			Vector3 vector = Quaternion.AngleAxis(y, oRef.upReference) * Quaternion.AngleAxis(x, this.rightCrossReference) * oRef.forwardReference;
			Vector3 upReference = oRef.upReference;
			Vector3.OrthoNormalize(ref vector, ref upReference);
			Vector3 forward = vector;
			this.dynamicUpReference = upReference;
			Vector3.OrthoNormalize(ref forward, ref this.dynamicUpReference);
			return this.transform.parent.rotation * Quaternion.LookRotation(forward, this.dynamicUpReference) * Quaternion.Inverse(this.transform.parent.rotation * Quaternion.LookRotation(oRef.forwardReference, oRef.upReference));
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000181E9 File Offset: 0x000163E9
		internal Quaternion GetSourcePoseRotation()
		{
			return this.root.rotation.QToWorld(this.initialLocalRotationInRootSpace);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00018204 File Offset: 0x00016404
		public Vector2 GetCustomLookAngles(Vector3 direction, UniRotateBone orientationsReference)
		{
			Vector3 vector = Quaternion.Inverse(this.transform.parent.rotation) * direction.normalized;
			Vector2 zero = Vector2.zero;
			zero.y = UniRotateBone.AngleAroundAxis(orientationsReference.forwardReference, vector, orientationsReference.upReference);
			Vector3 axis = Vector3.Cross(orientationsReference.upReference, vector);
			Vector3 firstDirection = vector - Vector3.Project(vector, orientationsReference.upReference);
			zero.x = UniRotateBone.AngleAroundAxis(firstDirection, vector, axis);
			return zero;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00018284 File Offset: 0x00016484
		public static float AngleAroundAxis(Vector3 firstDirection, Vector3 secondDirection, Vector3 axis)
		{
			firstDirection -= Vector3.Project(firstDirection, axis);
			secondDirection -= Vector3.Project(secondDirection, axis);
			return Vector3.Angle(firstDirection, secondDirection) * (float)((Vector3.Dot(axis, Vector3.Cross(firstDirection, secondDirection)) < 0f) ? -1 : 1);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000182D0 File Offset: 0x000164D0
		public Quaternion DynamicMapping()
		{
			return Quaternion.FromToRotation(this.right, this.transform.InverseTransformDirection(this.root.right)) * Quaternion.FromToRotation(this.up, this.transform.InverseTransformDirection(this.root.up));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00018324 File Offset: 0x00016524
		public void CaptureKeyframeAnimation()
		{
			this.keyframedPosition = this.transform.position;
			this.keyframedRotation = this.transform.rotation;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00018348 File Offset: 0x00016548
		public void RotateBy(float x, float y, float z)
		{
			Quaternion quaternion = this.transform.rotation;
			if (x != 0f)
			{
				quaternion *= Quaternion.AngleAxis(x, this.right);
			}
			if (y != 0f)
			{
				quaternion *= Quaternion.AngleAxis(y, this.up);
			}
			if (z != 0f)
			{
				quaternion *= Quaternion.AngleAxis(z, this.forward);
			}
			this.transform.rotation = quaternion;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000183BE File Offset: 0x000165BE
		public void RotateBy(Vector3 angles)
		{
			this.RotateBy(angles.x, angles.y, angles.z);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000183D8 File Offset: 0x000165D8
		public void RotateBy(Vector3 angles, float blend)
		{
			this.RotateBy(UniRotateBone.BlendAngle(angles.x, blend), UniRotateBone.BlendAngle(angles.y, blend), UniRotateBone.BlendAngle(angles.z, blend));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00018404 File Offset: 0x00016604
		public void RotateByDynamic(Vector3 angles)
		{
			this.RotateByDynamic(angles.x, angles.y, angles.z);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00018420 File Offset: 0x00016620
		public void RotateByDynamic(float x, float y, float z)
		{
			Quaternion quaternion = this.transform.rotation;
			if (x != 0f)
			{
				quaternion *= Quaternion.AngleAxis(x, this.transform.InverseTransformDirection(this.root.right));
			}
			if (y != 0f)
			{
				quaternion *= Quaternion.AngleAxis(y, this.transform.InverseTransformDirection(this.root.up));
			}
			if (z != 0f)
			{
				quaternion *= Quaternion.AngleAxis(z, this.transform.InverseTransformDirection(this.root.forward));
			}
			this.transform.rotation = quaternion;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000184C8 File Offset: 0x000166C8
		public Quaternion GetAngleRotation(float x, float y, float z)
		{
			Quaternion quaternion = Quaternion.identity;
			if (x != 0f)
			{
				quaternion *= Quaternion.AngleAxis(x, this.right);
			}
			if (y != 0f)
			{
				quaternion *= Quaternion.AngleAxis(y, this.up);
			}
			if (z != 0f)
			{
				quaternion *= Quaternion.AngleAxis(z, this.forward);
			}
			return quaternion;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00018530 File Offset: 0x00016730
		public Quaternion GetAngleRotationDynamic(float x, float y, float z)
		{
			Quaternion quaternion = Quaternion.identity;
			if (x != 0f)
			{
				quaternion *= Quaternion.AngleAxis(x, this.transform.InverseTransformDirection(this.root.right));
			}
			if (y != 0f)
			{
				quaternion *= Quaternion.AngleAxis(y, this.transform.InverseTransformDirection(this.root.up));
			}
			if (z != 0f)
			{
				quaternion *= Quaternion.AngleAxis(z, this.transform.InverseTransformDirection(this.root.forward));
			}
			return quaternion;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000185C5 File Offset: 0x000167C5
		public Quaternion GetAngleRotationDynamic(Vector3 angles)
		{
			return this.GetAngleRotationDynamic(angles.x, angles.y, angles.z);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000185DF File Offset: 0x000167DF
		public void RotateByDynamic(Vector3 angles, float blend)
		{
			this.RotateByDynamic(UniRotateBone.BlendAngle(angles.x, blend), UniRotateBone.BlendAngle(angles.y, blend), UniRotateBone.BlendAngle(angles.z, blend));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001860B File Offset: 0x0001680B
		public void RotateByDynamic(float x, float y, float z, float blend)
		{
			this.RotateByDynamic(UniRotateBone.BlendAngle(x, blend), UniRotateBone.BlendAngle(y, blend), UniRotateBone.BlendAngle(z, blend));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001862C File Offset: 0x0001682C
		public void RotateByDynamic(float x, float y, float z, Quaternion orientation)
		{
			Quaternion quaternion = this.transform.rotation;
			if (x != 0f)
			{
				quaternion *= Quaternion.AngleAxis(x, this.transform.InverseTransformDirection(orientation * Vector3.right));
			}
			if (y != 0f)
			{
				quaternion *= Quaternion.AngleAxis(y, this.transform.InverseTransformDirection(orientation * Vector3.up));
			}
			if (z != 0f)
			{
				quaternion *= Quaternion.AngleAxis(z, this.transform.InverseTransformDirection(orientation * Vector3.forward));
			}
			this.transform.rotation = quaternion;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000186D5 File Offset: 0x000168D5
		public void RotateXBy(float angle)
		{
			this.transform.rotation *= Quaternion.AngleAxis(angle, this.right);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000186F9 File Offset: 0x000168F9
		public void RotateYBy(float angle)
		{
			this.transform.rotation *= Quaternion.AngleAxis(angle, this.up);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001871D File Offset: 0x0001691D
		public void RotateZBy(float angle)
		{
			this.transform.rotation *= Quaternion.AngleAxis(angle, this.forward);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00018741 File Offset: 0x00016941
		public void PreCalibrate()
		{
			this.transform.localPosition = this.initialLocalPosition;
			this.transform.localRotation = this.initialLocalRotation;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00018768 File Offset: 0x00016968
		public Quaternion RotationTowards(Vector3 toDir)
		{
			return Quaternion.FromToRotation(this.transform.TransformDirection(this.fromParentForward).normalized, toDir.normalized) * this.transform.rotation;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000187AC File Offset: 0x000169AC
		public Quaternion RotationTowardsDynamic(Vector3 toDir)
		{
			return Quaternion.FromToRotation((this.transform.position - this.transform.parent.position).normalized, toDir.normalized) * this.transform.rotation;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000187FD File Offset: 0x000169FD
		public static float BlendAngle(float angle, float blend)
		{
			return Mathf.LerpAngle(0f, angle, blend);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001880B File Offset: 0x00016A0B
		public Vector3 Dir(Vector3 forward)
		{
			return this.transform.TransformDirection(forward);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00018819 File Offset: 0x00016A19
		public Vector3 IDir(Vector3 forward)
		{
			return this.transform.InverseTransformDirection(forward);
		}

		// Token: 0x040002DB RID: 731
		[CompilerGenerated]
		private Transform <transform>k__BackingField;

		// Token: 0x040002DC RID: 732
		[CompilerGenerated]
		private Vector3 <initialLocalPosition>k__BackingField;

		// Token: 0x040002DD RID: 733
		[CompilerGenerated]
		private Quaternion <initialLocalRotation>k__BackingField;

		// Token: 0x040002DE RID: 734
		[CompilerGenerated]
		private Vector3 <initialLocalPositionInRootSpace>k__BackingField;

		// Token: 0x040002DF RID: 735
		[CompilerGenerated]
		private Quaternion <initialLocalRotationInRootSpace>k__BackingField;

		// Token: 0x040002E0 RID: 736
		[CompilerGenerated]
		private Vector3 <right>k__BackingField;

		// Token: 0x040002E1 RID: 737
		[CompilerGenerated]
		private Vector3 <up>k__BackingField;

		// Token: 0x040002E2 RID: 738
		[CompilerGenerated]
		private Vector3 <forward>k__BackingField;

		// Token: 0x040002E3 RID: 739
		[CompilerGenerated]
		private Vector3 <dright>k__BackingField;

		// Token: 0x040002E4 RID: 740
		[CompilerGenerated]
		private Vector3 <dup>k__BackingField;

		// Token: 0x040002E5 RID: 741
		[CompilerGenerated]
		private Vector3 <dforward>k__BackingField;

		// Token: 0x040002E6 RID: 742
		[CompilerGenerated]
		private Vector3 <fromParentForward>k__BackingField;

		// Token: 0x040002E7 RID: 743
		[CompilerGenerated]
		private Vector3 <fromParentCross>k__BackingField;

		// Token: 0x040002E8 RID: 744
		[CompilerGenerated]
		private Vector3 <keyframedPosition>k__BackingField;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private Quaternion <keyframedRotation>k__BackingField;

		// Token: 0x040002EA RID: 746
		[CompilerGenerated]
		private Quaternion <mapping>k__BackingField;

		// Token: 0x040002EB RID: 747
		[CompilerGenerated]
		private Quaternion <dmapping>k__BackingField;

		// Token: 0x040002EC RID: 748
		[CompilerGenerated]
		private Transform <root>k__BackingField;

		// Token: 0x040002ED RID: 749
		[CompilerGenerated]
		private Vector3 <forwardReference>k__BackingField;

		// Token: 0x040002EE RID: 750
		[CompilerGenerated]
		private Vector3 <upReference>k__BackingField;

		// Token: 0x040002EF RID: 751
		[CompilerGenerated]
		private Vector3 <rightCrossReference>k__BackingField;

		// Token: 0x040002F0 RID: 752
		private Vector3 dynamicUpReference = Vector3.up;
	}
}
