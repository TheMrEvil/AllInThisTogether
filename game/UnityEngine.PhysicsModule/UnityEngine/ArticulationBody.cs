using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[NativeClass("Unity::ArticulationBody")]
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public class ArticulationBody : Behaviour
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		public extern ArticulationJointType jointType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002A62 File Offset: 0x00000C62
		public Vector3 anchorPosition
		{
			get
			{
				Vector3 result;
				this.get_anchorPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorPosition_Injected(ref value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002A6C File Offset: 0x00000C6C
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002A82 File Offset: 0x00000C82
		public Vector3 parentAnchorPosition
		{
			get
			{
				Vector3 result;
				this.get_parentAnchorPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_parentAnchorPosition_Injected(ref value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002A8C File Offset: 0x00000C8C
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public Quaternion anchorRotation
		{
			get
			{
				Quaternion result;
				this.get_anchorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorRotation_Injected(ref value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public Quaternion parentAnchorRotation
		{
			get
			{
				Quaternion result;
				this.get_parentAnchorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_parentAnchorRotation_Injected(ref value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600006C RID: 108
		public extern bool isRoot { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002ACC File Offset: 0x00000CCC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002AE4 File Offset: 0x00000CE4
		[Obsolete("computeParentAnchor has been renamed to matchAnchors (UnityUpgradable) -> matchAnchors")]
		public bool computeParentAnchor
		{
			get
			{
				return this.matchAnchors;
			}
			set
			{
				this.matchAnchors = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600006F RID: 111
		// (set) Token: 0x06000070 RID: 112
		public extern bool matchAnchors { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000071 RID: 113
		// (set) Token: 0x06000072 RID: 114
		public extern ArticulationDofLock linearLockX { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		public extern ArticulationDofLock linearLockY { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		public extern ArticulationDofLock linearLockZ { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		public extern ArticulationDofLock swingYLock { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000079 RID: 121
		// (set) Token: 0x0600007A RID: 122
		public extern ArticulationDofLock swingZLock { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600007B RID: 123
		// (set) Token: 0x0600007C RID: 124
		public extern ArticulationDofLock twistLock { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002AF0 File Offset: 0x00000CF0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002B06 File Offset: 0x00000D06
		public ArticulationDrive xDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_xDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_xDrive_Injected(ref value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002B10 File Offset: 0x00000D10
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002B26 File Offset: 0x00000D26
		public ArticulationDrive yDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_yDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_yDrive_Injected(ref value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002B46 File Offset: 0x00000D46
		public ArticulationDrive zDrive
		{
			get
			{
				ArticulationDrive result;
				this.get_zDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_zDrive_Injected(ref value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		public extern bool immovable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		public extern bool useGravity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		public extern float linearDamping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		public extern float angularDamping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		public extern float jointFriction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600008D RID: 141 RVA: 0x00002B50 File Offset: 0x00000D50
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002B5B File Offset: 0x00000D5B
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002B67 File Offset: 0x00000D67
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce_Injected(ref force, mode);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002B72 File Offset: 0x00000D72
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002B7E File Offset: 0x00000D7E
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque_Injected(ref torque, mode);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002B89 File Offset: 0x00000D89
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002B95 File Offset: 0x00000D95
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque_Injected(ref torque, mode);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002BA0 File Offset: 0x00000DA0
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002BAC File Offset: 0x00000DAC
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002BB9 File Offset: 0x00000DB9
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002BC8 File Offset: 0x00000DC8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002BDE File Offset: 0x00000DDE
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002BFE File Offset: 0x00000DFE
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.get_angularVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularVelocity_Injected(ref value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		public extern float mass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002C08 File Offset: 0x00000E08
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002C1E File Offset: 0x00000E1E
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 result;
				this.get_centerOfMass_Injected(out result);
				return result;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002C28 File Offset: 0x00000E28
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 result;
				this.get_worldCenterOfMass_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002C40 File Offset: 0x00000E40
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002C56 File Offset: 0x00000E56
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 result;
				this.get_inertiaTensor_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensor_Injected(ref value);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002C60 File Offset: 0x00000E60
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002C76 File Offset: 0x00000E76
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion result;
				this.get_inertiaTensorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensorRotation_Injected(ref value);
			}
		}

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCenterOfMass();

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetInertiaTensor();

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		public extern float sleepThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AB RID: 171
		// (set) Token: 0x060000AC RID: 172
		public extern int solverIterations { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		public extern int solverVelocityIterations { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AF RID: 175
		// (set) Token: 0x060000B0 RID: 176
		public extern float maxAngularVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		public extern float maxLinearVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		public extern float maxJointVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		public extern float maxDepenetrationVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C96 File Offset: 0x00000E96
		public ArticulationReducedSpace jointPosition
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointPosition_Injected(ref value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002CA0 File Offset: 0x00000EA0
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public ArticulationReducedSpace jointVelocity
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointVelocity_Injected(ref value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public ArticulationReducedSpace jointAcceleration
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointAcceleration_Injected(ref value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002CF6 File Offset: 0x00000EF6
		public ArticulationReducedSpace jointForce
		{
			get
			{
				ArticulationReducedSpace result;
				this.get_jointForce_Injected(out result);
				return result;
			}
			set
			{
				this.set_jointForce_Injected(ref value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000BF RID: 191
		public extern int dofCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C0 RID: 192
		public extern int index { [NativeMethod("GetBodyIndex")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00002D00 File Offset: 0x00000F00
		public void TeleportRoot(Vector3 position, Quaternion rotation)
		{
			this.TeleportRoot_Injected(ref position, ref rotation);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002D0C File Offset: 0x00000F0C
		public Vector3 GetClosestPoint(Vector3 point)
		{
			Vector3 result;
			this.GetClosestPoint_Injected(ref point, out result);
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002D24 File Offset: 0x00000F24
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 result;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out result);
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002D3C File Offset: 0x00000F3C
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 result;
			this.GetPointVelocity_Injected(ref worldPoint, out result);
			return result;
		}

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDenseJacobian(ref ArticulationJacobian jacobian);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointPositions(List<float> positions);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointPositions(List<float> positions);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointVelocities(List<float> velocities);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointVelocities(List<float> velocities);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointAccelerations(List<float> accelerations);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointAccelerations(List<float> accelerations);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetJointForces(List<float> forces);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetJointForces(List<float> forces);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDriveTargets(List<float> targets);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDriveTargets(List<float> targets);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDriveTargetVelocities(List<float> targetVelocities);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDriveTargetVelocities(List<float> targetVelocities);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDofStartIndices(List<int> dofStartIndices);

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D3 RID: 211
		// (set) Token: 0x060000D4 RID: 212
		public extern CollisionDetectionMode collisionDetectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00002D54 File Offset: 0x00000F54
		public void SnapAnchorToClosestContact()
		{
			bool flag = !base.transform.parent;
			if (!flag)
			{
				ArticulationBody componentInParent = base.transform.parent.GetComponentInParent<ArticulationBody>();
				while (componentInParent && !componentInParent.enabled)
				{
					componentInParent = componentInParent.transform.parent.GetComponentInParent<ArticulationBody>();
				}
				bool flag2 = !componentInParent;
				if (!flag2)
				{
					Vector3 worldCenterOfMass = componentInParent.worldCenterOfMass;
					Vector3 closestPoint = this.GetClosestPoint(worldCenterOfMass);
					this.anchorPosition = base.transform.InverseTransformPoint(closestPoint);
					this.anchorRotation = Quaternion.FromToRotation(Vector3.right, base.transform.InverseTransformDirection(worldCenterOfMass - closestPoint).normalized);
				}
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002E1D File Offset: 0x0000101D
		public ArticulationBody()
		{
		}

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorPosition_Injected(out Vector3 ret);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorPosition_Injected(ref Vector3 value);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_parentAnchorPosition_Injected(out Vector3 ret);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_parentAnchorPosition_Injected(ref Vector3 value);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorRotation_Injected(out Quaternion ret);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorRotation_Injected(ref Quaternion value);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_parentAnchorRotation_Injected(out Quaternion ret);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_parentAnchorRotation_Injected(ref Quaternion value);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_xDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_xDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_yDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_yDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_zDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_zDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularVelocity_Injected(ref Vector3 value);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_centerOfMass_Injected(out Vector3 ret);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_centerOfMass_Injected(ref Vector3 value);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointPosition_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointPosition_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointVelocity_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointVelocity_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointAcceleration_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointAcceleration_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_jointForce_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_jointForce_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TeleportRoot_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestPoint_Injected(ref Vector3 point, out Vector3 ret);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);
	}
}
