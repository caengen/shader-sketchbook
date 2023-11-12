#version 300 es

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;
uniform vec2[10]u_trails;

void main(){
  vec2 xy=gl_FragCoord.xy;
  
  vec2 c=u_resolution.xy/2.;
  float s;
  
  for(int i=0;i<10;i++){
    float d=distance(u_trails[i].xy,c);
    s=1.-step(30.,d);
  }
  
  gl_FragColor=vec4(s,s,s,1.);
}