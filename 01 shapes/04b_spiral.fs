#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

float fill(float d){
  return smoothstep(.0,.01,d);
}

float sSpiral(in vec2 p,in float turns){
  float r=dot(p,p);
  float a=atan(p.y,p.x);
  float d=abs(sin(fract(log(r)*(turns/5.)+a*.159)));
  return d-.5;
}
/* Shape 2D spiral */
/* Spiral function by Patricio Gonzalez Vivo */
float spiral(in vec2 p,in float turns){
  float d=sSpiral(p,turns);
  return fill(d);
}

void main(){
  vec2 st=gl_FragCoord.xy/u_resolution.xy;
  
  // spiral
  vec2 pos=st-vec2(.5);
  float d=spiral(pos,1.*1.*u_time);
  vec3 color=vec3(d);
  gl_FragColor=vec4(color,1.);
}