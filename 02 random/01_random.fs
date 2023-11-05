#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;
uniform sampler2D u_texture_0;

#define a 12.9898
#define b 78.233
#define c 43758.5453

// unknown author
float rand(vec2 co){
  return fract(sin(dot(co.xy,vec2(a,b)))*c);
}
void main(){
  float x=rand(gl_FragCoord.xy);
  gl_FragColor=vec4(x,x,x,1.);
}