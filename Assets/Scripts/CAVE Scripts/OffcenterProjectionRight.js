#pragma strict

public var originalProjection: Matrix4x4;
var myCamera: Camera;
function Start() {
	myCamera = GetComponent.<Camera>();
}
function Update() {
	var p: Matrix4x4 = originalProjection;
	p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.1F;
	p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.1F;
	myCamera.projectionMatrix = p; 
	
}


// Set an off-center projection, where perspective's vanishing
// point is not necessarily in the center of the screen.
//
// left/right/top/bottom define near plane size, i.e.
// how offset are corners of camera's near plane.
// Tweak the values and you can see camera's frustum change.
@ExecuteInEditMode
public var left: float = -0.2F;
public var right: float = 0.2F;
public var top: float = 0.2F;
public var bottom: float = -0.2F;

static function PerspectiveOffCenter(left: float, right: float, bottom: float, top: float, near: float, far: float) {
	var x: float = 2.0F * near / (right - left);
	var y: float = 2.0F * near / (top - bottom);
	var a: float = (right + left) / (right - left);
	var b: float = (top + bottom) / (top - bottom);
	var c: float = -(far + near) / (far - near);
	var d: float = -(2.0F * far * near) / (far - near);
	var e: float = -1.0F;
	var m: Matrix4x4 = new Matrix4x4();
	m[0, 0] = x;
	m[0, 1] = 0;
	m[0, 2] = a;
	m[0, 3] = 0;
	m[1, 0] = 0;
	m[1, 1] = y;
	m[1, 2] = b;
	m[1, 3] = 0;
	m[2, 0] = 0;
	m[2, 1] = 0;
	m[2, 2] = c;
	m[2, 3] = d;
	m[3, 0] = 0;
	m[3, 1] = 0;
	m[3, 2] = e;
	m[3, 3] = 0;
	return m;
}

function LateUpdate() {
	var cam: Camera = Camera.current;
	var m: Matrix4x4 = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
	cam.projectionMatrix = m;
}