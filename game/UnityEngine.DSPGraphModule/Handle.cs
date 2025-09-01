using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000002 RID: 2
	[NativeType(Header = "Modules/DSPGraph/Public/DSPGraphHandles.h")]
	internal struct Handle : IHandle<Handle>, IValidatable, IEquatable<Handle>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		public unsafe Handle.Node* AtomicNode
		{
			get
			{
				return (Handle.Node*)((void*)this.m_Node);
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException();
				}
				this.m_Node = (IntPtr)((void*)value);
				this.Version = value->Version;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public unsafe int Id
		{
			get
			{
				return this.Valid ? this.AtomicNode->Id : -1;
			}
			set
			{
				bool flag = value == -1;
				if (flag)
				{
					throw new ArgumentException("Invalid ID");
				}
				bool flag2 = !this.Valid;
				if (flag2)
				{
					throw new InvalidOperationException("Handle is invalid or has been destroyed");
				}
				bool flag3 = this.AtomicNode->Id != -1;
				if (flag3)
				{
					throw new InvalidOperationException(string.Format("Trying to overwrite id on live node {0}", this.AtomicNode->Id));
				}
				this.AtomicNode->Id = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000214C File Offset: 0x0000034C
		public unsafe Handle(Handle.Node* node)
		{
			bool flag = node == null;
			if (flag)
			{
				throw new ArgumentNullException("node");
			}
			bool flag2 = node->Id != -1;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Reusing unflushed node {0}", node->Id));
			}
			this.Version = node->Version;
			this.m_Node = (IntPtr)((void*)node);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B4 File Offset: 0x000003B4
		public unsafe void FlushNode()
		{
			bool flag = !this.Valid;
			if (flag)
			{
				throw new InvalidOperationException("Attempting to flush invalid audio handle");
			}
			this.AtomicNode->Id = -1;
			this.AtomicNode->Version++;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F8 File Offset: 0x000003F8
		public bool Equals(Handle other)
		{
			return this.m_Node == other.m_Node && this.Version == other.Version;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002230 File Offset: 0x00000430
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is Handle && this.Equals((Handle)obj);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002268 File Offset: 0x00000468
		public override int GetHashCode()
		{
			return (int)this.m_Node * 397 ^ this.Version;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002293 File Offset: 0x00000493
		public unsafe bool Valid
		{
			get
			{
				return this.m_Node != IntPtr.Zero && this.AtomicNode->Version == this.Version;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022BD File Offset: 0x000004BD
		public unsafe bool Alive
		{
			get
			{
				return this.Valid && this.AtomicNode->Id != -1;
			}
		}

		// Token: 0x04000001 RID: 1
		[NativeDisableUnsafePtrRestriction]
		private IntPtr m_Node;

		// Token: 0x04000002 RID: 2
		public int Version;

		// Token: 0x02000003 RID: 3
		internal struct Node
		{
			// Token: 0x04000003 RID: 3
			public long Next;

			// Token: 0x04000004 RID: 4
			public int Id;

			// Token: 0x04000005 RID: 5
			public int Version;

			// Token: 0x04000006 RID: 6
			public int DidAllocate;

			// Token: 0x04000007 RID: 7
			public const int InvalidId = -1;
		}
	}
}
