#version 330

//Zmienne jednorodne
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

//Atrybuty
in vec4 vertex; //wspolrzedne wierzcholka w przestrzeni modelu
in vec4 color;

out vec4 vertex_color;


void main(void) {
	gl_Position = P * V * M * vertex;
	float dist = distance(V * M * vertex, vec4(0.0, 0.0, 0.0, 1.0));
	float normalizedDistance = clamp((dist - 3.3) / (5.0 - 3.3), 0.0, 1.0);
	vertex_color = color * (1.0 - normalizedDistance);
}
