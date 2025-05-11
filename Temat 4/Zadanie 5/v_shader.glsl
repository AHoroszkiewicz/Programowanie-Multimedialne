#version 330

// Uniformy
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

const vec4 lightPos = vec4(0.0, 0.0, -6.0, 1.0); // Pozycja œwiat³a w przestrzeni œwiata

// Atrybuty
in vec4 vertex;
in vec3 normal;
in vec2 texcoord;

// Wyjœcie do fragment shadera
out vec3 fragPos;  // Przesy³amy pozycjê wierzcho³ka
out vec3 normalDir; // Normalna wektorowa
out vec4 lightPosEye; // Pozycja œwiat³a w przestrzeni kamery
out vec2 i_tc; // Przesy³amy wspó³rzêdne tekstury

void main(void) {
    vec4 vertexEye = V * M * vertex; // Wierzcho³ek w przestrzeni kamery
    fragPos = vertexEye.xyz; // Przesy³amy pozycjê w przestrzeni kamery
    normalDir = normalize((V * M * vec4(normal, 0.0)).xyz); // Normalna w przestrzeni kamery
    lightPosEye = V * lightPos; // Pozycja œwiat³a w przestrzeni kamery
	i_tc = texcoord; // Przesy³amy wspó³rzêdne tekstury

    gl_Position = P * vertexEye; // Ustalanie ostatecznej pozycji wierzcho³ka
}
