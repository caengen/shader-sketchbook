// https://greentec.github.io/shadertoy-fire-shader-en/
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;
uniform sampler2D u_texture_0;

#define timeScale u_time*1.
#define fireMovement vec2(-.01,-.5)
#define distortionMovement vec2(-.01,-.3)
#define distortionStrength.005

vec2 hash(vec2 p){
  p=vec2(dot(p,vec2(127.1,311.7)),
  dot(p,vec2(269.5,183.3)));
  
  return-1.+2.*fract(sin(p)*43758.5453123);
}
float noise(in vec2 p){
  const float K1=.366025404;// (sqrt(3)-1)/2;
  const float K2=.211324865;// (3-sqrt(3))/6;
  
  vec2 i=floor(p+(p.x+p.y)*K1);
  
  vec2 a=p-i+(i.x+i.y)*K2;
  vec2 o=step(a.yx,a.xy);
  vec2 b=a-o+K2;
  vec2 c=a-1.+2.*K2;
  
  vec3 h=max(.5-vec3(dot(a,a),dot(b,b),dot(c,c)),0.);
  
  vec3 n=h*h*h*h*vec3(dot(a,hash(i+0.)),dot(b,hash(i+o)),dot(c,hash(i+1.)));
  
  return dot(n,vec3(70.));
}

// Fractal Brownian Motion
float fbm(in vec2 p){
  float f=0.;
  mat2 m=mat2(1.6,1.2,-1.2,1.6);
  f=.5000*noise(p);p=m*p;
  f+=.2500*noise(p);p=m*p;
  f+=.1250*noise(p);p=m*p;
  f+=.0625*noise(p);p=m*p;
  f=.5+.5*f;
  return f;
}
void main(){
  vec2 uv=gl_FragCoord.xy/u_resolution.xy;
  vec4 normal=texture2D(u_texture_0,uv,0.);
  vec2 displacement=clamp((normal.xy-.5)*distortionStrength,-1.,1.);
  uv+=displacement;
  
  vec2 uvT=(uv*vec2(1.,.5))+fireMovement*timeScale;
  float n=pow(fbm(8.*uvT),1.);
  
  float gradient=pow(1.-uv.y,2.)*5.;
  float finalNoise=n*gradient;
  
  vec3 color=finalNoise*vec3(2.*n,2.*n*n*n,n*n*n*n);
  gl_FragColor=vec4(color,1.);
}