#version 330



out vec4 pixelColor; //Zmienna wyjsciowa fragment shadera. Zapisuje sie do niej ostateczny (prawie) kolor piksela

in vec4 vertex_color;

void main(void) {

	pixelColor= vertex_color;

}
