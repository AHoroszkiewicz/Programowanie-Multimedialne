#version 330

// Uniformy
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

const vec4 lightPos = vec4(0.0, 0.0, -6.0, 1.0); // Pozycja �wiat�a w przestrzeni �wiata

// Atrybuty
in vec4 vertex;
in vec3 normal;
in vec2 texcoord;

// Wyj�cie do fragment shadera
out vec3 fragPos;  // Przesy�amy pozycj� wierzcho�ka
out vec3 normalDir; // Normalna wektorowa
out vec4 lightPosEye; // Pozycja �wiat�a w przestrzeni kamery
out vec2 i_tc; // Przesy�amy wsp�rz�dne tekstury

void main(void) {
    vec4 vertexEye = V * M * vertex; // Wierzcho�ek w przestrzeni kamery
    fragPos = vertexEye.xyz; // Przesy�amy pozycj� w przestrzeni kamery
    normalDir = normalize((V * M * vec4(normal, 0.0)).xyz); // Normalna w przestrzeni kamery
    lightPosEye = V * lightPos; // Pozycja �wiat�a w przestrzeni kamery
	i_tc = texcoord; // Przesy�amy wsp�rz�dne tekstury

    gl_Position = P * vertexEye; // Ustalanie ostatecznej pozycji wierzcho�ka
}
