#version 330

in vec4 vertex_color; //Zmienna wejsciowa fragment shadera. Przechowuje kolor wierzcholka
out vec4 fragColor; //Zmienna wyjsciowa fragment shadera. Przechowuje kolor fragmentu

void main(void) {
	fragColor = vertex_color;
}
