#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main() {
  vec2 st = gl_FragCoord.xy/u_resolution.xy;
  vec2 xy = gl_FragCoord.xy;

  vec2 c  = u_resolution.xy / 2.;
  float d = distance(xy, c);
  float s = 1. - smoothstep(300., 306., d);

  gl_FragColor = vec4(s * st.x, s * st.y, s * sin(abs(u_time)), 1.0);

  if (s == 0.) {
    gl_FragColor = vec4(st.y, st.x, 1.0, 1.0);
  }
}