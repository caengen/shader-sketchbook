#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

float tsign(vec2 p1,vec2 p2,vec2 p3){
  return(p1.x-p3.x)*(p2.y-p3.y)-(p2.x-p3.x)*(p1.y-p3.y);
}

bool in_triangle(vec2 p,vec2 p0,vec2 p1,vec2 p2){
  float A=.5*(-p1.y*p2.x+p0.y*(-p1.x+p2.x)+p0.x*(p1.y-p2.y)+p1.x*p2.y);
  float tsign=A<0.?-1.:1.;
  float s=(p0.y*p2.x-p0.x*p2.y+(p2.y-p0.y)*p.x+(p0.x-p2.x)*p.y)*tsign;
  float t=(p0.x*p1.y-p0.y*p1.x+(p0.y-p1.y)*p.x+(p1.x-p0.x)*p.y)*tsign;
  
  return s>0.&&t>0.&&(s+t)<2.*A*tsign;
}

void main(){
  vec2 a=gl_FragCoord.xy;
  
  vec2 c=u_resolution.xy/2.;
  
  vec2 p0=c+vec2(0.,300.);
  vec2 p1=c+vec2(300.,-300.);
  vec2 p2=c+vec2(-300.,-300.);
  
  if(in_triangle(a,p0,p1,p2)){
    gl_FragColor=vec4(1.);
  }
  else{
    gl_FragColor=vec4(1.,0.,1.,1.);
  }
}