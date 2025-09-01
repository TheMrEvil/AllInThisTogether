using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000054 RID: 84
	internal sealed class ComIStreamMarshaler : ICustomMarshaler
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00002050 File Offset: 0x00000250
		private ComIStreamMarshaler()
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A11D File Offset: 0x0000831D
		private static ICustomMarshaler GetInstance(string cookie)
		{
			return ComIStreamMarshaler.defaultInstance;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A124 File Offset: 0x00008324
		public IntPtr MarshalManagedToNative(object managedObj)
		{
			return ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface((IStream)managedObj);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000A131 File Offset: 0x00008331
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseInterface(pNativeData);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000A139 File Offset: 0x00008339
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(pNativeData, false);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A142 File Offset: 0x00008342
		public void CleanUpManagedData(object managedObj)
		{
			ComIStreamMarshaler.NativeToManagedWrapper.ReleaseInterface((IStream)managedObj);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A14F File Offset: 0x0000834F
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000A152 File Offset: 0x00008352
		// Note: this type is marked as 'beforefieldinit'.
		static ComIStreamMarshaler()
		{
		}

		// Token: 0x04000436 RID: 1078
		private const int S_OK = 0;

		// Token: 0x04000437 RID: 1079
		private const int E_NOINTERFACE = -2147467262;

		// Token: 0x04000438 RID: 1080
		private static readonly ComIStreamMarshaler defaultInstance = new ComIStreamMarshaler();

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x060003D0 RID: 976
		private delegate int QueryInterfaceDelegate(IntPtr @this, [In] ref Guid riid, IntPtr ppvObject);

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x060003D4 RID: 980
		private delegate int AddRefDelegate(IntPtr @this);

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x060003D8 RID: 984
		private delegate int ReleaseDelegate(IntPtr @this);

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x060003DC RID: 988
		private delegate int ReadDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x060003E0 RID: 992
		private delegate int WriteDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x060003E4 RID: 996
		private delegate int SeekDelegate(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x060003E8 RID: 1000
		private delegate int SetSizeDelegate(IntPtr @this, long libNewSize);

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x060003EC RID: 1004
		private delegate int CopyToDelegate(IntPtr @this, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x060003F0 RID: 1008
		private delegate int CommitDelegate(IntPtr @this, int grfCommitFlags);

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x060003F4 RID: 1012
		private delegate int RevertDelegate(IntPtr @this);

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x060003F8 RID: 1016
		private delegate int LockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x060003FC RID: 1020
		private delegate int UnlockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000400 RID: 1024
		private delegate int StatDelegate(IntPtr @this, out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x06000404 RID: 1028
		private delegate int CloneDelegate(IntPtr @this, out IntPtr ppstm);

		// Token: 0x02000063 RID: 99
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamInterface
		{
			// Token: 0x06000407 RID: 1031 RVA: 0x00002050 File Offset: 0x00000250
			public IStreamInterface()
			{
			}

			// Token: 0x04000439 RID: 1081
			internal IntPtr lpVtbl;

			// Token: 0x0400043A RID: 1082
			internal IntPtr gcHandle;
		}

		// Token: 0x02000064 RID: 100
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamVtbl
		{
			// Token: 0x06000408 RID: 1032 RVA: 0x00002050 File Offset: 0x00000250
			public IStreamVtbl()
			{
			}

			// Token: 0x0400043B RID: 1083
			internal ComIStreamMarshaler.QueryInterfaceDelegate QueryInterface;

			// Token: 0x0400043C RID: 1084
			internal ComIStreamMarshaler.AddRefDelegate AddRef;

			// Token: 0x0400043D RID: 1085
			internal ComIStreamMarshaler.ReleaseDelegate Release;

			// Token: 0x0400043E RID: 1086
			internal ComIStreamMarshaler.ReadDelegate Read;

			// Token: 0x0400043F RID: 1087
			internal ComIStreamMarshaler.WriteDelegate Write;

			// Token: 0x04000440 RID: 1088
			internal ComIStreamMarshaler.SeekDelegate Seek;

			// Token: 0x04000441 RID: 1089
			internal ComIStreamMarshaler.SetSizeDelegate SetSize;

			// Token: 0x04000442 RID: 1090
			internal ComIStreamMarshaler.CopyToDelegate CopyTo;

			// Token: 0x04000443 RID: 1091
			internal ComIStreamMarshaler.CommitDelegate Commit;

			// Token: 0x04000444 RID: 1092
			internal ComIStreamMarshaler.RevertDelegate Revert;

			// Token: 0x04000445 RID: 1093
			internal ComIStreamMarshaler.LockRegionDelegate LockRegion;

			// Token: 0x04000446 RID: 1094
			internal ComIStreamMarshaler.UnlockRegionDelegate UnlockRegion;

			// Token: 0x04000447 RID: 1095
			internal ComIStreamMarshaler.StatDelegate Stat;

			// Token: 0x04000448 RID: 1096
			internal ComIStreamMarshaler.CloneDelegate Clone;
		}

		// Token: 0x02000065 RID: 101
		private sealed class ManagedToNativeWrapper
		{
			// Token: 0x06000409 RID: 1033 RVA: 0x0000A160 File Offset: 0x00008360
			static ManagedToNativeWrapper()
			{
				EventHandler value = new EventHandler(ComIStreamMarshaler.ManagedToNativeWrapper.OnShutdown);
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.DomainUnload += value;
				currentDomain.ProcessExit += value;
				ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable = new ComIStreamMarshaler.IStreamVtbl
				{
					QueryInterface = new ComIStreamMarshaler.QueryInterfaceDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.QueryInterface),
					AddRef = new ComIStreamMarshaler.AddRefDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.AddRef),
					Release = new ComIStreamMarshaler.ReleaseDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Release),
					Read = new ComIStreamMarshaler.ReadDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Read),
					Write = new ComIStreamMarshaler.WriteDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Write),
					Seek = new ComIStreamMarshaler.SeekDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Seek),
					SetSize = new ComIStreamMarshaler.SetSizeDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.SetSize),
					CopyTo = new ComIStreamMarshaler.CopyToDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.CopyTo),
					Commit = new ComIStreamMarshaler.CommitDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Commit),
					Revert = new ComIStreamMarshaler.RevertDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Revert),
					LockRegion = new ComIStreamMarshaler.LockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.LockRegion),
					UnlockRegion = new ComIStreamMarshaler.UnlockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.UnlockRegion),
					Stat = new ComIStreamMarshaler.StatDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Stat),
					Clone = new ComIStreamMarshaler.CloneDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Clone)
				};
				ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000A2F0 File Offset: 0x000084F0
			private ManagedToNativeWrapper(IStream managedInterface)
			{
				ComIStreamMarshaler.IStreamVtbl obj = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (obj)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable == IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
					}
					ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount++;
				}
				try
				{
					this.managedInterface = managedInterface;
					this.gcHandle = GCHandle.Alloc(this);
					ComIStreamMarshaler.IStreamInterface streamInterface = new ComIStreamMarshaler.IStreamInterface();
					streamInterface.lpVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.comVtable;
					streamInterface.gcHandle = (IntPtr)this.gcHandle;
					this.comInterface = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamInterface)));
					Marshal.StructureToPtr<ComIStreamMarshaler.IStreamInterface>(streamInterface, this.comInterface, false);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000A3D4 File Offset: 0x000085D4
			private void Dispose()
			{
				if (this.gcHandle.IsAllocated)
				{
					this.gcHandle.Free();
				}
				if (this.comInterface != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.comInterface);
					this.comInterface = IntPtr.Zero;
				}
				this.managedInterface = null;
				ComIStreamMarshaler.IStreamVtbl obj = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (obj)
				{
					if (--ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && Environment.HasShutdownStarted)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000A470 File Offset: 0x00008670
			private static void OnShutdown(object sender, EventArgs e)
			{
				ComIStreamMarshaler.IStreamVtbl obj = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (obj)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable != IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000A4C8 File Offset: 0x000086C8
			private static void CreateVtable()
			{
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamVtbl)));
				Marshal.StructureToPtr<ComIStreamMarshaler.IStreamVtbl>(ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable, ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, false);
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000A4F3 File Offset: 0x000086F3
			private static void DisposeVtable()
			{
				Marshal.DestroyStructure(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, typeof(ComIStreamMarshaler.IStreamVtbl));
				Marshal.FreeHGlobal(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable);
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = IntPtr.Zero;
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000A51D File Offset: 0x0000871D
			internal static IStream GetUnderlyingInterface(IntPtr comInterface, bool outParam)
			{
				if (Marshal.ReadIntPtr(comInterface) == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
				{
					IStream result = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(comInterface).managedInterface;
					if (outParam)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
					}
					return result;
				}
				return null;
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000A548 File Offset: 0x00008748
			internal static IntPtr GetInterface(IStream managedInterface)
			{
				if (managedInterface == null)
				{
					return IntPtr.Zero;
				}
				IntPtr underlyingInterface;
				if ((underlyingInterface = ComIStreamMarshaler.NativeToManagedWrapper.GetUnderlyingInterface(managedInterface)) == IntPtr.Zero)
				{
					underlyingInterface = new ComIStreamMarshaler.ManagedToNativeWrapper(managedInterface).comInterface;
				}
				return underlyingInterface;
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000A580 File Offset: 0x00008780
			internal static void ReleaseInterface(IntPtr comInterface)
			{
				if (comInterface != IntPtr.Zero)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(comInterface);
					if (intPtr == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
						return;
					}
					((ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot)Marshal.PtrToStructure((IntPtr)((long)intPtr + (long)(IntPtr.Size * 2)), typeof(ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot))).Release(comInterface);
				}
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000A5EA File Offset: 0x000087EA
			private static int GetHRForException(Exception e)
			{
				return (int)ComIStreamMarshaler.ManagedToNativeWrapper.exceptionGetHResult.Invoke(e, null);
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x0000A600 File Offset: 0x00008800
			private static ComIStreamMarshaler.ManagedToNativeWrapper GetObject(IntPtr @this)
			{
				return (ComIStreamMarshaler.ManagedToNativeWrapper)((GCHandle)Marshal.ReadIntPtr(@this, IntPtr.Size)).Target;
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000A62C File Offset: 0x0000882C
			private static int QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				int result;
				try
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.IID_IUnknown.Equals(riid) || ComIStreamMarshaler.ManagedToNativeWrapper.IID_IStream.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						ComIStreamMarshaler.ManagedToNativeWrapper.AddRef(@this);
						result = 0;
					}
					else
					{
						Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
						result = -2147467262;
					}
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000A6A4 File Offset: 0x000088A4
			private static int AddRef(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper obj = @object;
					lock (obj)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
						num = managedToNativeWrapper.refCount + 1;
						managedToNativeWrapper.refCount = num;
						num = num;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000A704 File Offset: 0x00008904
			private static int Release(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper obj = @object;
					lock (obj)
					{
						if (@object.refCount != 0)
						{
							ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
							num = managedToNativeWrapper.refCount - 1;
							managedToNativeWrapper.refCount = num;
							if (num == 0)
							{
								@object.Dispose();
							}
						}
						num = @object.refCount;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000A77C File Offset: 0x0000897C
			private static int Read(IntPtr @this, byte[] pv, int cb, IntPtr pcbRead)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Read(pv, cb, pcbRead);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x0000A7BC File Offset: 0x000089BC
			private static int Write(IntPtr @this, byte[] pv, int cb, IntPtr pcbWritten)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Write(pv, cb, pcbWritten);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x0000A7FC File Offset: 0x000089FC
			private static int Seek(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Seek(dlibMove, dwOrigin, plibNewPosition);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x0000A83C File Offset: 0x00008A3C
			private static int SetSize(IntPtr @this, long libNewSize)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.SetSize(libNewSize);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0000A878 File Offset: 0x00008A78
			private static int CopyTo(IntPtr @this, IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.CopyTo(pstm, cb, pcbRead, pcbWritten);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x0000A8B8 File Offset: 0x00008AB8
			private static int Commit(IntPtr @this, int grfCommitFlags)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Commit(grfCommitFlags);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x0000A8F4 File Offset: 0x00008AF4
			private static int Revert(IntPtr @this)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Revert();
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x0000A930 File Offset: 0x00008B30
			private static int LockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.LockRegion(libOffset, cb, dwLockType);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000A970 File Offset: 0x00008B70
			private static int UnlockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.UnlockRegion(libOffset, cb, dwLockType);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000A9B0 File Offset: 0x00008BB0
			private static int Stat(IntPtr @this, out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Stat(out pstatstg, grfStatFlag);
					result = 0;
				}
				catch (Exception e)
				{
					pstatstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000A9F4 File Offset: 0x00008BF4
			private static int Clone(IntPtr @this, out IntPtr ppstm)
			{
				ppstm = IntPtr.Zero;
				int result;
				try
				{
					IStream stream;
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Clone(out stream);
					ppstm = ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface(stream);
					result = 0;
				}
				catch (Exception e)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(e);
				}
				return result;
			}

			// Token: 0x04000449 RID: 1097
			private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

			// Token: 0x0400044A RID: 1098
			private static readonly Guid IID_IStream = new Guid("0000000C-0000-0000-C000-000000000046");

			// Token: 0x0400044B RID: 1099
			private static readonly MethodInfo exceptionGetHResult = typeof(Exception).GetTypeInfo().GetProperty("HResult", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.ExactBinding, null, typeof(int), new Type[0], null).GetGetMethod(true);

			// Token: 0x0400044C RID: 1100
			private static readonly ComIStreamMarshaler.IStreamVtbl managedVtable;

			// Token: 0x0400044D RID: 1101
			private static IntPtr comVtable;

			// Token: 0x0400044E RID: 1102
			private static int vtableRefCount;

			// Token: 0x0400044F RID: 1103
			private IStream managedInterface;

			// Token: 0x04000450 RID: 1104
			private IntPtr comInterface;

			// Token: 0x04000451 RID: 1105
			private GCHandle gcHandle;

			// Token: 0x04000452 RID: 1106
			private int refCount = 1;

			// Token: 0x02000066 RID: 102
			[StructLayout(LayoutKind.Sequential)]
			private sealed class ReleaseSlot
			{
				// Token: 0x06000422 RID: 1058 RVA: 0x00002050 File Offset: 0x00000250
				public ReleaseSlot()
				{
				}

				// Token: 0x04000453 RID: 1107
				internal ComIStreamMarshaler.ReleaseDelegate Release;
			}
		}

		// Token: 0x02000067 RID: 103
		private sealed class NativeToManagedWrapper : IStream
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x0000AA40 File Offset: 0x00008C40
			private NativeToManagedWrapper(IntPtr comInterface, bool outParam)
			{
				this.comInterface = comInterface;
				this.managedVtable = (ComIStreamMarshaler.IStreamVtbl)Marshal.PtrToStructure(Marshal.ReadIntPtr(comInterface), typeof(ComIStreamMarshaler.IStreamVtbl));
				if (!outParam)
				{
					this.managedVtable.AddRef(comInterface);
				}
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000AA90 File Offset: 0x00008C90
			~NativeToManagedWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0000AAC0 File Offset: 0x00008CC0
			private void Dispose(bool disposing)
			{
				this.managedVtable.Release(this.comInterface);
				if (disposing)
				{
					this.comInterface = IntPtr.Zero;
					this.managedVtable = null;
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x0000AAF4 File Offset: 0x00008CF4
			internal static IntPtr GetUnderlyingInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					ComIStreamMarshaler.NativeToManagedWrapper nativeToManagedWrapper = (ComIStreamMarshaler.NativeToManagedWrapper)managedInterface;
					nativeToManagedWrapper.managedVtable.AddRef(nativeToManagedWrapper.comInterface);
					return nativeToManagedWrapper.comInterface;
				}
				return IntPtr.Zero;
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x0000AB34 File Offset: 0x00008D34
			internal static IStream GetInterface(IntPtr comInterface, bool outParam)
			{
				if (comInterface == IntPtr.Zero)
				{
					return null;
				}
				return ComIStreamMarshaler.ManagedToNativeWrapper.GetUnderlyingInterface(comInterface, outParam) ?? new ComIStreamMarshaler.NativeToManagedWrapper(comInterface, outParam);
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0000AB64 File Offset: 0x00008D64
			internal static void ReleaseInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					((ComIStreamMarshaler.NativeToManagedWrapper)managedInterface).Dispose(true);
				}
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x0000AB7A File Offset: 0x00008D7A
			private static void ThrowExceptionForHR(int result)
			{
				if (result < 0)
				{
					throw new COMException(null, result);
				}
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x0000AB88 File Offset: 0x00008D88
			public void Read(byte[] pv, int cb, IntPtr pcbRead)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Read(this.comInterface, pv, cb, pcbRead));
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x0000ABA8 File Offset: 0x00008DA8
			public void Write(byte[] pv, int cb, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Write(this.comInterface, pv, cb, pcbWritten));
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000ABC8 File Offset: 0x00008DC8
			public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Seek(this.comInterface, dlibMove, dwOrigin, plibNewPosition));
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0000ABE8 File Offset: 0x00008DE8
			public void SetSize(long libNewSize)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.SetSize(this.comInterface, libNewSize));
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0000AC06 File Offset: 0x00008E06
			public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.CopyTo(this.comInterface, pstm, cb, pcbRead, pcbWritten));
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0000AC28 File Offset: 0x00008E28
			public void Commit(int grfCommitFlags)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Commit(this.comInterface, grfCommitFlags));
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x0000AC46 File Offset: 0x00008E46
			public void Revert()
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Revert(this.comInterface));
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0000AC63 File Offset: 0x00008E63
			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.LockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x0000AC83 File Offset: 0x00008E83
			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.UnlockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0000ACA3 File Offset: 0x00008EA3
			public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Stat(this.comInterface, out pstatstg, grfStatFlag));
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x0000ACC4 File Offset: 0x00008EC4
			public void Clone(out IStream ppstm)
			{
				IntPtr intPtr;
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Clone(this.comInterface, out intPtr));
				ppstm = ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(intPtr, true);
			}

			// Token: 0x04000454 RID: 1108
			private IntPtr comInterface;

			// Token: 0x04000455 RID: 1109
			private ComIStreamMarshaler.IStreamVtbl managedVtable;
		}
	}
}
