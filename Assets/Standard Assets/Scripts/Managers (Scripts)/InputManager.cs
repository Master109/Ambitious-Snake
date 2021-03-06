﻿using UnityEngine;
using Extensions;
using UnityEngine.InputSystem;

namespace AmbitiousSnake
{
	public class InputManager : SingletonMonoBehaviour<InputManager>
	{
		public InputSettings settings;
		public static InputSettings Settings
		{
			get
			{
				return InputManager.Instance.settings;
			}
		}
		public static bool UsingGamepad
		{
			get
			{
				return Gamepad.current != null;
			}
		}
		public static bool UsingMouse
		{
			get
			{
				return Mouse.current != null;
			}
		}
		public static bool UsingKeyboard
		{
			get
			{
				return Keyboard.current != null;
			}
		}
		public static bool UsingTouchscreen
		{
			get
			{
				return Touchscreen.current != null;
			}
		}
		public static bool SubmitInput
		{
			get
			{
				if (UsingGamepad)
					return Gamepad.current.aButton.isPressed;
				else
					return Keyboard.current.enterKey.isPressed;// || Mouse.current.leftButton.isPressed;
			}
		}
		public bool _SubmitInput
		{
			get
			{
				return SubmitInput;
			}
		}
		public static Vector2 MoveInput
		{
			get
			{
				if (UsingGamepad)
					return Vector2.ClampMagnitude(Gamepad.current.leftStick.ReadValue(), 1);
				else
				{
					int x = 0;
					if (Keyboard.current.dKey.isPressed)
						x ++;
					if (Keyboard.current.aKey.isPressed)
						x --;
					int y = 0;
					if (Keyboard.current.wKey.isPressed)
						y ++;
					if (Keyboard.current.sKey.isPressed)
						y --;
					return Vector2.ClampMagnitude(new Vector2(x, y), 1);
				}
			}
		}
		public Vector2 _MoveInput
		{
			get
			{
				return MoveInput;
			}
		}
		public static int ChangeLengthInput
		{
			get
			{
				int output = 0;
				if (UsingGamepad)
				{
					if (Gamepad.current.leftTrigger.isPressed)
						output --;
					if (Gamepad.current.rightTrigger.isPressed)
						output ++;
				}
				else
				{
					if (Mouse.current.leftButton.isPressed)
						output --;
					if (Mouse.current.rightButton.isPressed)
						output ++;
				}
				return output;
			}
		}
		public int _ChangeLengthInput
		{
			get
			{
				return ChangeLengthInput;
			}
		}
		public static Vector2 UIMovementInput
		{
			get
			{
				if (UsingGamepad)
					return Vector2.ClampMagnitude(Gamepad.current.leftStick.ReadValue(), 1);
				else
				{
					int x = 0;
					if (Keyboard.current.dKey.isPressed)
						x ++;
					if (Keyboard.current.aKey.isPressed)
						x --;
					int y = 0;
					if (Keyboard.current.wKey.isPressed)
						y ++;
					if (Keyboard.current.sKey.isPressed)
						y --;
					return Vector2.ClampMagnitude(new Vector2(x, y), 1);
				}
			}
		}
		public Vector2 _UIMovementInput
		{
			get
			{
				return UIMovementInput;
			}
		}
		public static Vector2 Acceleration
		{
			get
			{
				return Input.acceleration;
			}
		}
		public Vector3 _Acceleration
		{
			get
			{
				return Acceleration;
			}
		}
		public static bool PauseInput
		{
			get
			{
				if (UsingGamepad)
					return Gamepad.current.startButton.isPressed || Gamepad.current.selectButton.isPressed;
				else
					return Keyboard.current.escapeKey.isPressed || Keyboard.current.spaceKey.isPressed;
			}
		}
		public bool _PauseInput
		{
			get
			{
				return PauseInput;
			}
		}
		public static bool LeftClickInput
		{
			get
			{
				if (UsingMouse)
					return Mouse.current.leftButton.isPressed;
				else
					return false;
			}
		}
		public bool _LeftClickInput
		{
			get
			{
				return LeftClickInput;
			}
		}
		public static bool RightClickInput
		{
			get
			{
				if (UsingMouse)
					return Mouse.current.rightButton.isPressed;
				else
					return false;
			}
		}
		public bool _RightClickInput
		{
			get
			{
				return RightClickInput;
			}
		}
		public static Vector2 MousePosition
		{
			get
			{
				if (UsingMouse)
					return Mouse.current.position.ReadValue();
				else
					return VectorExtensions.NULL;
			}
		}
		public Vector2 _MousePosition
		{
			get
			{
				return MousePosition;
			}
		}
		public static bool RestartLevelInput
		{
			get
			{
				if (UsingKeyboard)
					return Keyboard.current.rKey.isPressed;
				else
					return false;
			}
		}
		public bool _RestartLevelInput
		{
			get
			{
				return RestartLevelInput;
			}
		}
	}

	// [Serializable]
	// public class InputButton
	// {
	// 	public string[] buttonNames;
	// 	public KeyCode[] keyCodes;

	// 	public virtual bool GetDown ()
	// 	{
	// 		bool output = false;
	// 		foreach (KeyCode keyCode in keyCodes)
	// 			output |= Input.GetKeyDown(keyCode);
	// 		foreach (string buttonName in buttonNames)
	// 			output |= InputManager.inputter.GetButtonDown(buttonName);
	// 		return output;
	// 	}

	// 	public virtual bool Get ()
	// 	{
	// 		bool output = false;
	// 		foreach (KeyCode keyCode in keyCodes)
	// 			output |= Input.GetKey(keyCode);
	// 		foreach (string buttonName in buttonNames)
	// 			output |= InputManager.inputter.GetButton(buttonName);
	// 		return output;
	// 	}

	// 	public virtual bool GetUp ()
	// 	{
	// 		bool output = false;
	// 		foreach (KeyCode keyCode in keyCodes)
	// 			output |= Input.GetKeyUp(keyCode);
	// 		foreach (string buttonName in buttonNames)
	// 			output |= InputManager.inputter.GetButtonUp(buttonName);
	// 		return output;
	// 	}
	// }

	// [Serializable]
	// public class InputAxis
	// {
	// 	public InputButton positiveButton;
	// 	public InputButton negativeButton;

	// 	public virtual int Get ()
	// 	{
	// 		int output = 0;
	// 		if (positiveButton.Get())
	// 			output ++;
	// 		if (negativeButton.Get())
	// 			output --;
	// 		return output;
	// 	}
	// }

	// public enum InputDevice
	// {
	// 	Keyboard,
	// 	Gamepad
	// }
}