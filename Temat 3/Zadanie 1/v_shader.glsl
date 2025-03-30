#version 330

//Zmienne jednorodne
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

//Atrybuty
in vec4 vertex; //wspolrzedne wierzcholka w przestrzeni modelu


void main(void) {
    vec4 a = vertex;
	a.y = 0.0;
    gl_Position=P*V*M*a;
}
