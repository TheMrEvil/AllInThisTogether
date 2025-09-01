using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000071 RID: 113
	[Preserve]
	[ES3Properties(new string[]
	{
		"velocity",
		"angularVelocity",
		"drag",
		"angularDrag",
		"mass",
		"useGravity",
		"maxDepenetrationVelocity",
		"isKinematic",
		"freezeRotation",
		"constraints",
		"collisionDetectionMode",
		"centerOfMass",
		"inertiaTensorRotation",
		"inertiaTensor",
		"detectCollisions",
		"position",
		"rotation",
		"interpolation",
		"solverIterations",
		"sleepThreshold",
		"maxAngularVelocity",
		"solverVelocityIterations"
	})]
	public class ES3Type_Rigidbody : ES3ComponentType
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000DF01 File Offset: 0x0000C101
		public ES3Type_Rigidbody() : base(typeof(Rigidbody))
		{
			ES3Type_Rigidbody.Instance = this;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000DF1C File Offset: 0x0000C11C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Rigidbody rigidbody = (Rigidbody)obj;
			writer.WriteProperty("velocity", rigidbody.velocity, ES3Type_Vector3.Instance);
			writer.WriteProperty("drag", rigidbody.drag, ES3Type_float.Instance);
			writer.WriteProperty("angularDrag", rigidbody.angularDrag, ES3Type_float.Instance);
			writer.WriteProperty("angularVelocity", rigidbody.angularVelocity, ES3Type_Vector3.Instance);
			writer.WriteProperty("mass", rigidbody.mass, ES3Type_float.Instance);
			writer.WriteProperty("useGravity", rigidbody.useGravity, ES3Type_bool.Instance);
			writer.WriteProperty("maxDepenetrationVelocity", rigidbody.maxDepenetrationVelocity, ES3Type_float.Instance);
			writer.WriteProperty("isKinematic", rigidbody.isKinematic, ES3Type_bool.Instance);
			writer.WriteProperty("freezeRotation", rigidbody.freezeRotation, ES3Type_bool.Instance);
			writer.WriteProperty("constraints", rigidbody.constraints);
			writer.WriteProperty("collisionDetectionMode", rigidbody.collisionDetectionMode);
			writer.WriteProperty("centerOfMass", rigidbody.centerOfMass, ES3Type_Vector3.Instance);
			writer.WriteProperty("detectCollisions", rigidbody.detectCollisions, ES3Type_bool.Instance);
			writer.WriteProperty("position", rigidbody.position, ES3Type_Vector3.Instance);
			writer.WriteProperty("rotation", rigidbody.rotation, ES3Type_Quaternion.Instance);
			writer.WriteProperty("interpolation", rigidbody.interpolation);
			writer.WriteProperty("solverIterations", rigidbody.solverIterations, ES3Type_int.Instance);
			writer.WriteProperty("sleepThreshold", rigidbody.sleepThreshold, ES3Type_float.Instance);
			writer.WriteProperty("maxAngularVelocity", rigidbody.maxAngularVelocity, ES3Type_float.Instance);
			writer.WriteProperty("solverVelocityIterations", rigidbody.solverVelocityIterations, ES3Type_int.Instance);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E140 File Offset: 0x0000C340
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Rigidbody rigidbody = (Rigidbody)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2614105187U)
				{
					if (num <= 846470194U)
					{
						if (num <= 132777611U)
						{
							if (num != 69535816U)
							{
								if (num == 132777611U)
								{
									if (text == "constraints")
									{
										rigidbody.constraints = reader.Read<RigidbodyConstraints>();
										continue;
									}
								}
							}
							else if (text == "maxDepenetrationVelocity")
							{
								rigidbody.maxDepenetrationVelocity = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (num != 520558326U)
						{
							if (num != 564937055U)
							{
								if (num == 846470194U)
								{
									if (text == "velocity")
									{
										rigidbody.velocity = reader.Read<Vector3>(ES3Type_Vector3.Instance);
										continue;
									}
								}
							}
							else if (text == "rotation")
							{
								rigidbody.rotation = reader.Read<Quaternion>(ES3Type_Quaternion.Instance);
								continue;
							}
						}
						else if (text == "isKinematic")
						{
							rigidbody.isKinematic = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num <= 1590826182U)
					{
						if (num != 905125296U)
						{
							if (num != 965172509U)
							{
								if (num == 1590826182U)
								{
									if (text == "inertiaTensorRotation")
									{
										rigidbody.inertiaTensorRotation = reader.Read<Quaternion>(ES3Type_Quaternion.Instance);
										continue;
									}
								}
							}
							else if (text == "interpolation")
							{
								rigidbody.interpolation = reader.Read<RigidbodyInterpolation>();
								continue;
							}
						}
						else if (text == "freezeRotation")
						{
							rigidbody.freezeRotation = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 2471448074U)
					{
						if (num != 2596953323U)
						{
							if (num == 2614105187U)
							{
								if (text == "detectCollisions")
								{
									rigidbody.detectCollisions = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sleepThreshold")
						{
							rigidbody.sleepThreshold = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "position")
					{
						rigidbody.position = reader.Read<Vector3>(ES3Type_Vector3.Instance);
						continue;
					}
				}
				else if (num <= 3494744359U)
				{
					if (num <= 3077523073U)
					{
						if (num != 3018506558U)
						{
							if (num == 3077523073U)
							{
								if (text == "centerOfMass")
								{
									rigidbody.centerOfMass = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "angularVelocity")
						{
							rigidbody.angularVelocity = reader.Read<Vector3>(ES3Type_Vector3.Instance);
							continue;
						}
					}
					else if (num != 3096186179U)
					{
						if (num != 3439738464U)
						{
							if (num == 3494744359U)
							{
								if (text == "angularDrag")
								{
									rigidbody.angularDrag = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "inertiaTensor")
						{
							Vector3 vector = reader.Read<Vector3>(ES3Type_Vector3.Instance);
							if (vector != Vector3.zero)
							{
								rigidbody.inertiaTensor = vector;
								continue;
							}
							continue;
						}
					}
					else if (text == "drag")
					{
						rigidbody.drag = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3749132497U)
				{
					if (num != 3598126642U)
					{
						if (num != 3648333725U)
						{
							if (num == 3749132497U)
							{
								if (text == "mass")
								{
									rigidbody.mass = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "solverVelocityIterations")
						{
							rigidbody.solverVelocityIterations = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (text == "solverIterations")
					{
						rigidbody.solverIterations = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num != 3774863253U)
				{
					if (num != 3838993682U)
					{
						if (num == 3866543682U)
						{
							if (text == "maxAngularVelocity")
							{
								rigidbody.maxAngularVelocity = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "useGravity")
					{
						rigidbody.useGravity = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (text == "collisionDetectionMode")
				{
					rigidbody.collisionDetectionMode = reader.Read<CollisionDetectionMode>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000C0 RID: 192
		public static ES3Type Instance;
	}
}
