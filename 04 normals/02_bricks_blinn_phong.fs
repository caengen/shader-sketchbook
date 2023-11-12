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
const vec3 lightPos=vec3(1.,1.,1.);
const vec3 lightColor=vec3(1.,1.,1.);
const float lightPower=40.;
const vec3 ambientColor=vec3(.7255,.6471,.1255);
const vec3 specColor=vec3(1.,1.,1.);
const float shininess=16.;
const float screenGamma=2.2;// Assume the monitor is calibrated to the sRGB color space

//rand stuff
#define a 12.9898
#define b 78.233
#define c 43758.5453

/*
Terminology:
A diffuse texture is the colour of the object without light effects
A normal texture is the normal vectors (perpendicular direction vectors of the surface) encoded in an image
with the xyz components of the normal vectors mapped to the rgb components of the image.
*/
void main(){
  // normal stuff
  vec2 uv=gl_FragCoord.xy/u_resolution.xy;
  vec4 diffuse=texture2D(u_texture_1,uv,0.);
  vec4 normal=texture2D(u_texture_2,uv,0.);
  // map from [0,1] to [-1,1]
  normal=normalize(normal*2.-1.);
  
  // lighting stuff
  vec3 lightDir=u_camera-sin(u_time);
  float distance=length(lightDir);
  distance=distance*distance;
  lightDir=normalize(lightDir);
  
  float lambertian=max(dot(lightDir,normal.xyz),0.);
  float specular=0.;
  
  if(lambertian>0.){
    
    vec3 viewDir=normalize(u_camera);
    
    // this is blinn phong
    vec3 halfDir=normalize(lightDir+viewDir);
    float specAngle=max(dot(halfDir,normal.xyz),0.);
    specular=pow(specAngle,shininess);
  }
  
  vec3 colorLinear=
  diffuse.xyz*lambertian*lightColor*lightPower/distance+
  specColor*specular*lightColor*lightPower/distance;
  // apply gamma correction (assume ambientColor, diffuseColor and specColor
  // have been linearized, i.e. have no gamma correction in them)
  vec3 colorGammaCorrected=pow(colorLinear,vec3(1./screenGamma));
  // use the gamma corrected color in the fragment
  gl_FragColor=vec4(colorGammaCorrected,1.);
  
  // gl_FragColor=vec4(diffuse.rgb,1.);
}