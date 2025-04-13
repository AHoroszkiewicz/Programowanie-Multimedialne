#version 330

// Uniformy
uniform mat4 P;
uniform mat4 V;
uniform mat4 M;

const vec4 lightPos = vec4(0.0, 0.0, -6.0, 1.0); // pozycja światła w przestrzeni świata

// Atrybuty
in vec4 vertex;
in vec3 normal;

// Wyjście do fragment shadera
out vec4 vertex_color;

void main(void) {
    // Transformacje pozycji i normalnych
    vec4 vertexEye = V * M * vertex; // wierzchołek w przestrzeni oka
    vec3 N = normalize((V * M * vec4(normal, 0.0)).xyz); // normalna w przestrzeni oka
    vec3 L = normalize((V * lightPos - vertexEye).xyz); // kierunek światła
    vec3 Vv = normalize(vec3(0.0, 0.0, 0.0) - vertexEye.xyz); // wektor do obserwatora
    vec3 R = reflect(-L, N); // wektor odbicia

    // Oświetlenie rozproszone
    float diffuse = max(dot(N, L), 0.0);

    // Oświetlenie odbite
    float specular = pow(clamp(dot(R, Vv), 0.0, 1.0), 25.0); // tu możesz zmienić wartość α (np. 10, 50, 100)

    // Finalny kolor: brak ambient, tylko diffuse + specular
    vertex_color = vec4(1.0) * diffuse + vec4(1.0) * specular;
    vertex_color.a = 1.0;

    gl_Position = P * vertexEye;
}
