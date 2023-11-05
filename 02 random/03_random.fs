#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

#define timeScale u_time*1.
#define fireMovement vec2(-.01,-.5)
#define distortionMovement vec2(-.01,-.3)
#define normalStrength 40.
#define distortionStrength.1

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

vec3 bumpMap(vec2 uv){
  vec2 s=1./u_resolution.xy;
  float p=fbm(uv);
  float h1=fbm(uv+s*vec2(1.,0));
  float v1=fbm(uv+s*vec2(0,1.));
  
  vec2 xy=(p-vec2(h1,v1))*normalStrength;
  return vec3(xy+.5,1.);
}

void main(){
  vec2 uv=gl_FragCoord.xy/u_resolution.xy;
  vec3 normal=bumpMap(uv*vec2(1.,.3)+distortionMovement*timeScale);
  gl_FragColor=vec4(normal,1.);
}