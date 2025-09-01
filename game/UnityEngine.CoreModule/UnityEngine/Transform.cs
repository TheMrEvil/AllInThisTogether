using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000260 RID: 608
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformScriptBindings.h")]
	[NativeHeader("Runtime/Transform/Transform.h")]
	[RequiredByNativeCode]
	[NativeHeader("Configuration/UnityConfigure.h")]
	public class Transform : Component, IEnumerable
	{
		// Token: 0x06001A38 RID: 6712 RVA: 0x0002A846 File Offset: 0x00028A46
		protected Transform()
		{
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0002A850 File Offset: 0x00028A50
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x0002A866 File Offset: 0x00028A66
		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.get_position_Injected(out result);
				return result;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0002A870 File Offset: 0x00028A70
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0002A886 File Offset: 0x00028A86
		public Vector3 localPosition
		{
			get
			{
				Vector3 result;
				this.get_localPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_localPosition_Injected(ref value);
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0002A890 File Offset: 0x00028A90
		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			Vector3 result;
			this.GetLocalEulerAngles_Injected(order, out result);
			return result;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0002A8A7 File Offset: 0x00028AA7
		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			this.SetLocalEulerAngles_Injected(ref euler, order);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0002A8B2 File Offset: 0x00028AB2
		[NativeConditional("UNITY_EDITOR")]
		internal void SetLocalEulerHint(Vector3 euler)
		{
			this.SetLocalEulerHint_Injected(ref euler);
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0002A8BC File Offset: 0x00028ABC
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x0002A8DC File Offset: 0x00028ADC
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0002A8EC File Offset: 0x00028AEC
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x0002A90C File Offset: 0x00028B0C
		public Vector3 localEulerAngles
		{
			get
			{
				return this.localRotation.eulerAngles;
			}
			set
			{
				this.localRotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0002A91C File Offset: 0x00028B1C
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0002A93E File Offset: 0x00028B3E
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0002A954 File Offset: 0x00028B54
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0002A976 File Offset: 0x00028B76
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0002A98C File Offset: 0x00028B8C
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x0002A9AE File Offset: 0x00028BAE
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0002A9C0 File Offset: 0x00028BC0
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x0002A9D6 File Offset: 0x00028BD6
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.get_rotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0002A9E0 File Offset: 0x00028BE0
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0002A9F6 File Offset: 0x00028BF6
		public Quaternion localRotation
		{
			get
			{
				Quaternion result;
				this.get_localRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_localRotation_Injected(ref value);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0002AA00 File Offset: 0x00028C00
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0002AA18 File Offset: 0x00028C18
		[NativeConditional("UNITY_EDITOR")]
		internal RotationOrder rotationOrder
		{
			get
			{
				return (RotationOrder)this.GetRotationOrderInternal();
			}
			set
			{
				this.SetRotationOrderInternal(value);
			}
		}

		// Token: 0x06001A50 RID: 6736
		[NativeConditional("UNITY_EDITOR")]
		[NativeMethod("GetRotationOrder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetRotationOrderInternal();

		// Token: 0x06001A51 RID: 6737
		[NativeMethod("SetRotationOrder")]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetRotationOrderInternal(RotationOrder rotationOrder);

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0002AA24 File Offset: 0x00028C24
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0002AA3A File Offset: 0x00028C3A
		public Vector3 localScale
		{
			get
			{
				Vector3 result;
				this.get_localScale_Injected(out result);
				return result;
			}
			set
			{
				this.set_localScale_Injected(ref value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0002AA44 File Offset: 0x00028C44
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x0002AA5C File Offset: 0x00028C5C
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				bool flag = this is RectTransform;
				if (flag)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0002AA8C File Offset: 0x00028C8C
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		internal Transform parentInternal
		{
			get
			{
				return this.GetParent();
			}
			set
			{
				this.SetParent(value);
			}
		}

		// Token: 0x06001A58 RID: 6744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Transform GetParent();

		// Token: 0x06001A59 RID: 6745 RVA: 0x0002AAAF File Offset: 0x00028CAF
		public void SetParent(Transform p)
		{
			this.SetParent(p, true);
		}

		// Token: 0x06001A5A RID: 6746
		[FreeFunction("SetParent", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0002AABC File Offset: 0x00028CBC
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_worldToLocalMatrix_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0002AAD4 File Offset: 0x00028CD4
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_localToWorldMatrix_Injected(out result);
				return result;
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0002AAEA File Offset: 0x00028CEA
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			this.SetPositionAndRotation_Injected(ref position, ref rotation);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0002AAF6 File Offset: 0x00028CF6
		public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
		{
			this.SetLocalPositionAndRotation_Injected(ref localPosition, ref localRotation);
		}

		// Token: 0x06001A5F RID: 6751
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetPositionAndRotation(out Vector3 position, out Quaternion rotation);

		// Token: 0x06001A60 RID: 6752
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation);

		// Token: 0x06001A61 RID: 6753 RVA: 0x0002AB04 File Offset: 0x00028D04
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.World;
			if (flag)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0002AB48 File Offset: 0x00028D48
		public void Translate(Vector3 translation)
		{
			this.Translate(translation, Space.Self);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0002AB54 File Offset: 0x00028D54
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0002AB68 File Offset: 0x00028D68
		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z), Space.Self);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0002AB7C File Offset: 0x00028D7C
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			bool flag = relativeTo;
			if (flag)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0002ABC2 File Offset: 0x00028DC2
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		public void Rotate(Vector3 eulers, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion rhs = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.localRotation *= rhs;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * rhs * this.rotation;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0002AC4B File Offset: 0x00028E4B
		public void Rotate(Vector3 eulers)
		{
			this.Rotate(eulers, Space.Self);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0002AC57 File Offset: 0x00028E57
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0002AC6B File Offset: 0x00028E6B
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), Space.Self);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0002AC7E File Offset: 0x00028E7E
		[NativeMethod("RotateAround")]
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			this.RotateAroundInternal_Injected(ref axis, angle);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0002AC8C File Offset: 0x00028E8C
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0002ACCD File Offset: 0x00028ECD
		public void Rotate(Vector3 axis, float angle)
		{
			this.Rotate(axis, angle, Space.Self);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0002ACDC File Offset: 0x00028EDC
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = rotation * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0002AD28 File Offset: 0x00028F28
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0002AD50 File Offset: 0x00028F50
		public void LookAt(Transform target)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, Vector3.up);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0002AD7A File Offset: 0x00028F7A
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			this.Internal_LookAt(worldPosition, worldUp);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0002AD86 File Offset: 0x00028F86
		public void LookAt(Vector3 worldPosition)
		{
			this.Internal_LookAt(worldPosition, Vector3.up);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0002AD96 File Offset: 0x00028F96
		[FreeFunction("Internal_LookAt", HasExplicitThis = true)]
		private void Internal_LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			this.Internal_LookAt_Injected(ref worldPosition, ref worldUp);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0002ADA4 File Offset: 0x00028FA4
		public Vector3 TransformDirection(Vector3 direction)
		{
			Vector3 result;
			this.TransformDirection_Injected(ref direction, out result);
			return result;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0002ADBC File Offset: 0x00028FBC
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0002ADDC File Offset: 0x00028FDC
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			Vector3 result;
			this.InverseTransformDirection_Injected(ref direction, out result);
			return result;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0002ADF4 File Offset: 0x00028FF4
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0002AE14 File Offset: 0x00029014
		public Vector3 TransformVector(Vector3 vector)
		{
			Vector3 result;
			this.TransformVector_Injected(ref vector, out result);
			return result;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0002AE2C File Offset: 0x0002902C
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0002AE4C File Offset: 0x0002904C
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			Vector3 result;
			this.InverseTransformVector_Injected(ref vector, out result);
			return result;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0002AE64 File Offset: 0x00029064
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0002AE84 File Offset: 0x00029084
		public Vector3 TransformPoint(Vector3 position)
		{
			Vector3 result;
			this.TransformPoint_Injected(ref position, out result);
			return result;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0002AE9C File Offset: 0x0002909C
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0002AEBC File Offset: 0x000290BC
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			Vector3 result;
			this.InverseTransformPoint_Injected(ref position, out result);
			return result;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0002AED4 File Offset: 0x000290D4
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0002AEF4 File Offset: 0x000290F4
		public Transform root
		{
			get
			{
				return this.GetRoot();
			}
		}

		// Token: 0x06001A81 RID: 6785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Transform GetRoot();

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001A82 RID: 6786
		public extern int childCount { [NativeMethod("GetChildrenCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001A83 RID: 6787
		[FreeFunction("DetachChildren", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachChildren();

		// Token: 0x06001A84 RID: 6788
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsFirstSibling();

		// Token: 0x06001A85 RID: 6789
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsLastSibling();

		// Token: 0x06001A86 RID: 6790
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x06001A87 RID: 6791
		[NativeMethod("MoveAfterSiblingInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MoveAfterSibling(Transform transform, bool notifyEditorAndMarkDirty);

		// Token: 0x06001A88 RID: 6792
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetSiblingIndex();

		// Token: 0x06001A89 RID: 6793
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Transform FindRelativeTransformWithPath([NotNull("NullExceptionObject")] Transform transform, string path, [DefaultValue("false")] bool isActiveOnly);

		// Token: 0x06001A8A RID: 6794 RVA: 0x0002AF0C File Offset: 0x0002910C
		public Transform Find(string n)
		{
			bool flag = n == null;
			if (flag)
			{
				throw new ArgumentNullException("Name cannot be null");
			}
			return Transform.FindRelativeTransformWithPath(this, n, false);
		}

		// Token: 0x06001A8B RID: 6795
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SendTransformChangedScale();

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0002AF3C File Offset: 0x0002913C
		public Vector3 lossyScale
		{
			[NativeMethod("GetWorldScaleLossy")]
			get
			{
				Vector3 result;
				this.get_lossyScale_Injected(out result);
				return result;
			}
		}

		// Token: 0x06001A8D RID: 6797
		[FreeFunction("Internal_IsChildOrSameTransform", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsChildOf([NotNull("ArgumentNullException")] Transform parent);

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A8E RID: 6798
		// (set) Token: 0x06001A8F RID: 6799
		[NativeProperty("HasChangedDeprecated")]
		public extern bool hasChanged { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001A90 RID: 6800 RVA: 0x0002AF54 File Offset: 0x00029154
		[Obsolete("FindChild has been deprecated. Use Find instead (UnityUpgradable) -> Find([mscorlib] System.String)", false)]
		public Transform FindChild(string n)
		{
			return this.Find(n);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0002AF70 File Offset: 0x00029170
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0002AF88 File Offset: 0x00029188
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			this.RotateAround_Injected(ref axis, angle);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0002AF93 File Offset: 0x00029193
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			this.RotateAroundLocal_Injected(ref axis, angle);
		}

		// Token: 0x06001A94 RID: 6804
		[NativeThrows]
		[FreeFunction("GetChild", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetChild(int index);

		// Token: 0x06001A95 RID: 6805
		[Obsolete("warning use Transform.childCount instead (UnityUpgradable) -> Transform.childCount", false)]
		[NativeMethod("GetChildrenCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetChildCount();

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0002AFA0 File Offset: 0x000291A0
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x0002AFB8 File Offset: 0x000291B8
		public int hierarchyCapacity
		{
			get
			{
				return this.internal_getHierarchyCapacity();
			}
			set
			{
				this.internal_setHierarchyCapacity(value);
			}
		}

		// Token: 0x06001A98 RID: 6808
		[FreeFunction("GetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_getHierarchyCapacity();

		// Token: 0x06001A99 RID: 6809
		[FreeFunction("SetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void internal_setHierarchyCapacity(int value);

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0002AFC4 File Offset: 0x000291C4
		public int hierarchyCount
		{
			get
			{
				return this.internal_getHierarchyCount();
			}
		}

		// Token: 0x06001A9B RID: 6811
		[FreeFunction("GetHierarchyCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_getHierarchyCount();

		// Token: 0x06001A9C RID: 6812
		[FreeFunction("IsNonUniformScaleTransform", HasExplicitThis = true)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsNonUniformScaleTransform();

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0002AFDC File Offset: 0x000291DC
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0002AFE4 File Offset: 0x000291E4
		[NativeConditional("UNITY_EDITOR")]
		internal bool constrainProportionsScale
		{
			get
			{
				return this.IsConstrainProportionsScale();
			}
			set
			{
				this.SetConstrainProportionsScale(value);
			}
		}

		// Token: 0x06001A9F RID: 6815
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstrainProportionsScale(bool isLinked);

		// Token: 0x06001AA0 RID: 6816
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsConstrainProportionsScale();

		// Token: 0x06001AA1 RID: 6817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector3 ret);

		// Token: 0x06001AA2 RID: 6818
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector3 value);

		// Token: 0x06001AA3 RID: 6819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localPosition_Injected(out Vector3 ret);

		// Token: 0x06001AA4 RID: 6820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localPosition_Injected(ref Vector3 value);

		// Token: 0x06001AA5 RID: 6821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalEulerAngles_Injected(RotationOrder order, out Vector3 ret);

		// Token: 0x06001AA6 RID: 6822
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalEulerAngles_Injected(ref Vector3 euler, RotationOrder order);

		// Token: 0x06001AA7 RID: 6823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalEulerHint_Injected(ref Vector3 euler);

		// Token: 0x06001AA8 RID: 6824
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x06001AA9 RID: 6825
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotation_Injected(ref Quaternion value);

		// Token: 0x06001AAA RID: 6826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localRotation_Injected(out Quaternion ret);

		// Token: 0x06001AAB RID: 6827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localRotation_Injected(ref Quaternion value);

		// Token: 0x06001AAC RID: 6828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localScale_Injected(out Vector3 ret);

		// Token: 0x06001AAD RID: 6829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localScale_Injected(ref Vector3 value);

		// Token: 0x06001AAE RID: 6830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06001AAF RID: 6831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06001AB0 RID: 6832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPositionAndRotation_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06001AB1 RID: 6833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalPositionAndRotation_Injected(ref Vector3 localPosition, ref Quaternion localRotation);

		// Token: 0x06001AB2 RID: 6834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAroundInternal_Injected(ref Vector3 axis, float angle);

		// Token: 0x06001AB3 RID: 6835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_LookAt_Injected(ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x06001AB4 RID: 6836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		// Token: 0x06001AB5 RID: 6837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		// Token: 0x06001AB6 RID: 6838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		// Token: 0x06001AB7 RID: 6839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		// Token: 0x06001AB8 RID: 6840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x06001AB9 RID: 6841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x06001ABA RID: 6842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lossyScale_Injected(out Vector3 ret);

		// Token: 0x06001ABB RID: 6843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAround_Injected(ref Vector3 axis, float angle);

		// Token: 0x06001ABC RID: 6844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAroundLocal_Injected(ref Vector3 axis, float angle);

		// Token: 0x02000261 RID: 609
		private class Enumerator : IEnumerator
		{
			// Token: 0x06001ABD RID: 6845 RVA: 0x0002AFEE File Offset: 0x000291EE
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0002B008 File Offset: 0x00029208
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x06001ABF RID: 6847 RVA: 0x0002B02C File Offset: 0x0002922C
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < childCount;
			}

			// Token: 0x06001AC0 RID: 6848 RVA: 0x0002B05E File Offset: 0x0002925E
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x040008C0 RID: 2240
			private Transform outer;

			// Token: 0x040008C1 RID: 2241
			private int currentIndex = -1;
		}
	}
}
