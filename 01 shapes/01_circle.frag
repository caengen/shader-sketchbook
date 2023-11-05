#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main() {
  vec2 xy = gl_FragCoord.xy;

  vec2 c  = u_resolution.xy / 2.;
  float d = distance(xy, c);
  float s = 1. - step(300., d);

  gl_FragColor = vec4(s, s, s, 1.0);
}