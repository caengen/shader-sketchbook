// https://learnopengl.com/Advanced-Lighting/Normal-Mapping

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;
uniform vec3 u_camera;

uniform sampler2D u_texture_1;
uniform sampler2D u_texture_2;

// parameters for the lighting model
const vec3 lightPos=vec3(.5,.5,1.);
const vec3 viewPos=vec3(1.,1.,1.);
const vec3 lightColor=vec3(1.,1.,1.);
const float lightPower=40.;
const vec3 ambientColor=vec3(.1,0.,0.);
const vec3 diffuseColor=vec3(1.,1.,1.);
const vec3 specColor=vec3(1.,1.,1.);
const float shininess=16.;
const float screenGamma=2.2;// Assume the monitor is calibrated to the sRGB color space

/*
Terminology:
A diffuse texture is the colour of the object without light effects
A normal texture is the normal vectors (perpendicular direction vectors of the surface) encoded in an image
with the xyz components of the normal vectors mapped to the rgb components of the image.
*/
void main(){
  // normal stuff
  vec2 uv=1.-gl_FragCoord.xy/u_resolution.xy;
  
  // light pos debug
  float d=distance(gl_FragCoord.xy,u_resolution.xy*lightPos.xy);
  float s=1.-step(6.,d);
  if(s==1.){
    gl_FragColor=vec4(s,s,s,1.);
    return;
  }
  
  vec4 diffuse=texture2D(u_texture_1,uv,0.);
  vec4 normal=texture2D(u_texture_2,uv,0.);
  // map from [0,1] to [-1,1]
  normal=normalize(normal*2.-1.);
  
  // lighting stuff
  vec3 lightDir=lightPos-vec3(u_camera.xy,lightPos.z);
  float distance=length(lightDir);
  distance=distance*distance;
  lightDir=normalize(lightDir);
  
  float lambertian=max(dot(lightDir,normal.xyz),0.);
  float specular=0.;
  
  if(lambertian>0.){
    
    vec3 viewDir=normalize(viewPos);
    
    // this is blinn-phong
    vec3 halfDir=normalize(lightDir+viewDir);
    float specAngle=max(dot(halfDir,normal.xyz),0.);
    specular=pow(specAngle,shininess);
  }
  
  vec3 colorLinear=ambientColor+
  diffuseColor*lambertian*lightColor*lightPower/distance+
  specColor*specular*lightColor*lightPower/distance;
  // apply gamma correction (assume ambientColor, diffuseColor and specColor
  // have been linearized, i.e. have no gamma correction in them)
  vec3 colorGammaCorrected=pow(colorLinear,vec3(1./screenGamma));
  // use the gamma corrected color in the fragment
  gl_FragColor=vec4(diffuse.xyz*colorGammaCorrected,1.);
  
  // gl_FragColor=vec4(diffuse.rgb,1.);
}