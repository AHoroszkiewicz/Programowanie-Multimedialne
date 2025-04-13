#version 330

//Zmienne jednorodne
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

uniform vec4 lightPos;
//Atrybuty
in vec4 vertex; //wspolrzedne wierzcholka w przestrzeni modelu
in vec3 normal; //wspolrzedne normalnej wierzcholka w przestrzeni modelu
in vec4 color;

out vec4 vertex_color;



void main(void) {
    // Transformacja pozycji wierzcho³ka i normalnych do przestrzeni oka
    vec4 vertex_eye = V * M * vertex;
    vec3 normal_eye = normalize(mat3(V * M) * normal); // mat3 usuwa translacjê

    // Transformacja pozycji œwiat³a
    vec4 light_eye = V * lightPos;
    vec3 l = normalize(light_eye.xyz - vertex_eye.xyz);

    // K¹t miêdzy œwiat³em a normaln¹
    float NdotL = max(dot(normal_eye, l), 0.0);

    // Bia³e œwiat³o rozproszone * NdotL, bez œwiat³a otoczenia
    vec3 diffuse = vec3(1.0, 1.0, 1.0) * NdotL;

    vertex_color = vec4(diffuse, 1.0);

    gl_Position = P * vertex_eye;
}