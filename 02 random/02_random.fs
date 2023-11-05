#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float time;

vec2 hash(vec2 p){
  p=vec2(dot(p,vec2(127.1,311.7)),
  dot(p,vec2(269.5,183.3)));
  
  // return -1.0 + 2.0*fract(sin(p) * 43758.5453123);
  return fract(sin(p)*43758.5453123);
}
void main(){
  float x=hash(gl_FragCoord.xy).x;
  // x = hash(gl_FragCoord.xy).y;
  gl_FragColor=vec4(x,x,x,1.);
}