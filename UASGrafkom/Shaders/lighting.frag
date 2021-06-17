#version 330 core
out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform float ambientStg;
uniform float shininess;
uniform float specularStg;

uniform vec3 lightPos;
uniform vec3 viewPos; 
in vec3 Normal; 
in vec3 FragPos;

void main()
{
    float ambientStrength = ambientStg;
    vec3 ambient = ambientStrength * lightColor;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos); 

    float diff = max(dot(norm, lightDir), 0.0); 
    vec3 diffuse = diff * lightColor;


     float specularStrength = specularStg;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), shininess); 
    vec3 specular = specularStrength * spec * lightColor;

    
    vec3 result = (ambient + diffuse + specular) * objectColor;
   
    FragColor = vec4(result, 1.0);
}