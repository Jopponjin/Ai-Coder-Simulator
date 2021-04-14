using UnityEngine;

namespace BH
{
	public static class InputHelper
	{
		public static bool AnyOfTheseKeysDown(params KeyCode[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (Input.GetKeyDown(keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static bool AnyOfTheseKeysHeld(params KeyCode[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (Input.GetKey(keys[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}

