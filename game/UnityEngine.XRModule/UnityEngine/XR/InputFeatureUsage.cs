using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000F RID: 15
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002770 File Offset: 0x00000970
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002788 File Offset: 0x00000988
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002794 File Offset: 0x00000994
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000027AC File Offset: 0x000009AC
		internal InputFeatureType internalType
		{
			get
			{
				return this.m_InternalType;
			}
			set
			{
				this.m_InternalType = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027B8 File Offset: 0x000009B8
		public Type type
		{
			get
			{
				Type typeFromHandle;
				switch (this.m_InternalType)
				{
				case InputFeatureType.Custom:
					typeFromHandle = typeof(byte[]);
					break;
				case InputFeatureType.Binary:
					typeFromHandle = typeof(bool);
					break;
				case InputFeatureType.DiscreteStates:
					typeFromHandle = typeof(uint);
					break;
				case InputFeatureType.Axis1D:
					typeFromHandle = typeof(float);
					break;
				case InputFeatureType.Axis2D:
					typeFromHandle = typeof(Vector2);
					break;
				case InputFeatureType.Axis3D:
					typeFromHandle = typeof(Vector3);
					break;
				case InputFeatureType.Rotation:
					typeFromHandle = typeof(Quaternion);
					break;
				case InputFeatureType.Hand:
					typeFromHandle = typeof(Hand);
					break;
				case InputFeatureType.Bone:
					typeFromHandle = typeof(Bone);
					break;
				case InputFeatureType.Eyes:
					typeFromHandle = typeof(Eyes);
					break;
				default:
					throw new InvalidCastException("No valid managed type for unknown native type.");
				}
				return typeFromHandle;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002893 File Offset: 0x00000A93
		internal InputFeatureUsage(string name, InputFeatureType type)
		{
			this.m_Name = name;
			this.m_InternalType = type;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028A4 File Offset: 0x00000AA4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage);
			return !flag && this.Equals((InputFeatureUsage)obj);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool Equals(InputFeatureUsage other)
		{
			return this.name == other.name && this.internalType == other.internalType;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002910 File Offset: 0x00000B10
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.internalType.GetHashCode() << 1;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002944 File Offset: 0x00000B44
		public static bool operator ==(InputFeatureUsage a, InputFeatureUsage b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002960 File Offset: 0x00000B60
		public static bool operator !=(InputFeatureUsage a, InputFeatureUsage b)
		{
			return !(a == b);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000297C File Offset: 0x00000B7C
		public InputFeatureUsage<T> As<T>()
		{
			bool flag = this.type != typeof(T);
			if (flag)
			{
				throw new ArgumentException("InputFeatureUsage type does not match out variable type.");
			}
			return new InputFeatureUsage<T>(this.name);
		}

		// Token: 0x0400005B RID: 91
		internal string m_Name;

		// Token: 0x0400005C RID: 92
		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;
	}
}
