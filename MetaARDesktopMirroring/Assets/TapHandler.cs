using UnityEngine;
using System.Collections;
using Thalmic.Myo;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Runtime.InteropServices;
using System;

//http://stackoverflow.com/questions/2416748/how-to-simulate-mouse-click-in-c
public class TapHandler : MonoBehaviour {
	public GameObject display;
	public ThalmicMyo myo;
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    public void handleTap(float x, float y)
	{
		var renderer = display.GetComponent<Renderer>();
		float width = renderer.bounds.size.x;
		float height = renderer.bounds.size.y;
		float dispx = renderer.bounds.center.x;
		float dispy = renderer.bounds.center.y;
		float relativex = Mathf.Abs(x - (dispx - width/2));
        float relativey = Mathf.Abs(y - (dispy + height / 2));

        Debug.Log("width: " + width + "dispx " + dispx + " relativex" + relativex);
        Debug.Log("height: " + height + "dispy " + dispy + " relativey" + relativey);

		float percentX = relativex / width;
		float percentY = relativey / height;
		if (percentX > 1 || percentY > 1 || percentX < 0 || percentY < 0) {
			return;
		}
		myo.Vibrate (Thalmic.Myo.VibrationType.Short);
        Debug.Log("percentX: " + percentX + "width " + UpdateScreenshot.width);
        Debug.Log("percentY: " + percentY + "height " + UpdateScreenshot.height);

        DoMouseClick((int) Math.Floor(percentX * 1920),(int) Math.Floor(percentY * 1080));
    }
    public void DoMouseClick(int x, int y)
    {
        Debug.Log("coordinates: " + x + ", " + y);
        //SetCursorPos(x, y);
        MousePoint cursorPos = GetCursorPosition();
        Debug.Log("mouse coord: " + cursorPos.X + ", " + cursorPos.Y);
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
}
