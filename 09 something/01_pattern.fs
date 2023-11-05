#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

void main(){
  vec2 st=gl_FragCoord.xy/u_resolution.xy;
  
  st=smoothstep(0.,.1,sin(st*u_time));
  gl_FragColor=vec4(st.x,st.y,1.,1.);
  
}