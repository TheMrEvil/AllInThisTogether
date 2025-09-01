using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ControllerColliderHit
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023B8 File Offset: 0x000005B8
		public CharacterController controller
		{
			get
			{
				return this.m_Controller;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023D0 File Offset: 0x000005D0
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023E8 File Offset: 0x000005E8
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Collider.attachedRigidbody;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002408 File Offset: 0x00000608
		public GameObject gameObject
		{
			get
			{
				return this.m_Collider.gameObject;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002428 File Offset: 0x00000628
		public Transform transform
		{
			get
			{
				return this.m_Collider.transform;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002448 File Offset: 0x00000648
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002460 File Offset: 0x00000660
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002478 File Offset: 0x00000678
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002490 File Offset: 0x00000690
		public float moveLength
		{
			get
			{
				return this.m_MoveLength;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000024C3 File Offset: 0x000006C3
		private bool push
		{
			get
			{
				return this.m_Push != 0;
			}
			set
			{
				this.m_Push = (value ? 1 : 0);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000024D3 File Offset: 0x000006D3
		public ControllerColliderHit()
		{
		}

		// Token: 0x0400003A RID: 58
		internal CharacterController m_Controller;

		// Token: 0x0400003B RID: 59
		internal Collider m_Collider;

		// Token: 0x0400003C RID: 60
		internal Vector3 m_Point;

		// Token: 0x0400003D RID: 61
		internal Vector3 m_Normal;

		// Token: 0x0400003E RID: 62
		internal Vector3 m_MoveDirection;

		// Token: 0x0400003F RID: 63
		internal float m_MoveLength;

		// Token: 0x04000040 RID: 64
		internal int m_Push;
	}
}
