using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000018 RID: 24
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	public struct Bone : IEquatable<Bone>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003B3C File Offset: 0x00001D3C
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003B54 File Offset: 0x00001D54
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B6C File Offset: 0x00001D6C
		public bool TryGetPosition(out Vector3 position)
		{
			return Bone.Bone_TryGetPosition(this, out position);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B8A File Offset: 0x00001D8A
		private static bool Bone_TryGetPosition(Bone bone, out Vector3 position)
		{
			return Bone.Bone_TryGetPosition_Injected(ref bone, out position);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B94 File Offset: 0x00001D94
		public bool TryGetRotation(out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation(this, out rotation);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BB2 File Offset: 0x00001DB2
		private static bool Bone_TryGetRotation(Bone bone, out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation_Injected(ref bone, out rotation);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BBC File Offset: 0x00001DBC
		public bool TryGetParentBone(out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone(this, out parentBone);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BDA File Offset: 0x00001DDA
		private static bool Bone_TryGetParentBone(Bone bone, out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone_Injected(ref bone, out parentBone);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public bool TryGetChildBones(List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones(this, childBones);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C02 File Offset: 0x00001E02
		private static bool Bone_TryGetChildBones(Bone bone, [NotNull("ArgumentNullException")] List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones_Injected(ref bone, childBones);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C0C File Offset: 0x00001E0C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Bone);
			return !flag && this.Equals((Bone)obj);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C40 File Offset: 0x00001E40
		public bool Equals(Bone other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003C74 File Offset: 0x00001E74
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ this.featureIndex.GetHashCode() << 1;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static bool operator ==(Bone a, Bone b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public static bool operator !=(Bone a, Bone b)
		{
			return !(a == b);
		}

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetPosition_Injected(ref Bone bone, out Vector3 position);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetRotation_Injected(ref Bone bone, out Quaternion rotation);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetParentBone_Injected(ref Bone bone, out Bone parentBone);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetChildBones_Injected(ref Bone bone, List<Bone> childBones);

		// Token: 0x040000AA RID: 170
		private ulong m_DeviceId;

		// Token: 0x040000AB RID: 171
		private uint m_FeatureIndex;
	}
}
