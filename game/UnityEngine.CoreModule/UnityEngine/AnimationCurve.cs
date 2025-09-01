using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E3 RID: 227
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		// Token: 0x0600039C RID: 924
		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600039D RID: 925
		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(Keyframe[] keys);

		// Token: 0x0600039E RID: 926
		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_Equals(IntPtr other);

		// Token: 0x0600039F RID: 927 RVA: 0x00006134 File Offset: 0x00004334
		~AnimationCurve()
		{
			AnimationCurve.Internal_Destroy(this.m_Ptr);
		}

		// Token: 0x060003A0 RID: 928
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float Evaluate(float time);

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000616C File Offset: 0x0000436C
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00006184 File Offset: 0x00004384
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060003A3 RID: 931
		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddKey(float time, float value);

		// Token: 0x060003A4 RID: 932 RVA: 0x00006190 File Offset: 0x00004390
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000061A9 File Offset: 0x000043A9
		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			return this.AddKey_Internal_Injected(ref key);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000061B3 File Offset: 0x000043B3
		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public int MoveKey(int index, Keyframe key)
		{
			return this.MoveKey_Injected(index, ref key);
		}

		// Token: 0x060003A7 RID: 935
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveKey(int index);

		// Token: 0x170000A1 RID: 161
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003A9 RID: 937
		public extern int length { [NativeMethod("GetKeyCount", IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003AA RID: 938
		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060003AB RID: 939 RVA: 0x000061DC File Offset: 0x000043DC
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true)]
		private Keyframe GetKey(int index)
		{
			Keyframe result;
			this.GetKey_Injected(index, out result);
			return result;
		}

		// Token: 0x060003AC RID: 940
		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060003AD RID: 941
		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060003AE RID: 942 RVA: 0x000061F4 File Offset: 0x000043F4
		public static AnimationCurve Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurve.Linear(timeStart, value, timeEnd, value);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00006210 File Offset: 0x00004410
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve result;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				result = new AnimationCurve(new Keyframe[]
				{
					keyframe
				});
			}
			else
			{
				float num = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, num),
					new Keyframe(timeEnd, valueEnd, num, 0f)
				};
				result = new AnimationCurve(keys);
			}
			return result;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000628C File Offset: 0x0000448C
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve result;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				result = new AnimationCurve(new Keyframe[]
				{
					keyframe
				});
			}
			else
			{
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				result = new AnimationCurve(keys);
			}
			return result;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B1 RID: 945
		// (set) Token: 0x060003B2 RID: 946
		public extern WrapMode preWrapMode { [NativeMethod("GetPreInfinity", IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetPreInfinity", IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003B3 RID: 947
		// (set) Token: 0x060003B4 RID: 948
		public extern WrapMode postWrapMode { [NativeMethod("GetPostInfinity", IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetPostInfinity", IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x00006303 File Offset: 0x00004503
		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00006319 File Offset: 0x00004519
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00006330 File Offset: 0x00004530
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == o;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = o.GetType() != base.GetType();
					result = (!flag3 && this.Equals((AnimationCurve)o));
				}
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00006384 File Offset: 0x00004584
		public bool Equals(AnimationCurve other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = this.m_Ptr.Equals(other.m_Ptr);
					result = (flag3 || this.Internal_Equals(other.m_Ptr));
				}
			}
			return result;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000063DC File Offset: 0x000045DC
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddKey_Internal_Injected(ref Keyframe key);

		// Token: 0x060003BB RID: 955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int MoveKey_Injected(int index, ref Keyframe key);

		// Token: 0x060003BC RID: 956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetKey_Injected(int index, out Keyframe ret);

		// Token: 0x040002F6 RID: 758
		internal IntPtr m_Ptr;
	}
}
