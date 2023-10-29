#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;

void main() {
  //vec2 st = gl_FragCoord.xy/u_resolution.xy;
  vec2 a = gl_FragCoord.xy;

  vec2 c = u_resolution.xy/2.0;
  float dim = 300.;

  if (c.x - dim < a.x && a.x < c.x + dim && c.y - dim < a.y && a.y < c.y + dim) {
    gl_FragColor = vec4(1.);
  } else {
    gl_FragColor = vec4(0.,0.,0.,1.);
  }
}