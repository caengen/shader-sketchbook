// https://learnopengl.com/Advanced-Lighting/Normal-Mapping

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

uniform sampler2D u_texture_1;
uniform sampler2D u_texture_2;

/*
Terminology:
A diffuse texture is the colour of the object without light effects
A normal texture is the normal vectors (perpendicular direction vectors of the surface) encoded in an image
with the xyz components of the normal vectors mapped to the rgb components of the image.
*/
void main(){
  vec2 uv=1.-gl_FragCoord.xy/u_resolution.xy;
  vec4 diffuse=texture2D(u_texture_1,uv,0.);
  vec4 normal=texture2D(u_texture_2,uv,0.);
  // map from [0,1] to [-1,1]
  normal=normalize(normal*2.-1.);
  
  gl_FragColor=vec4(diffuse.rgb,1.);
}