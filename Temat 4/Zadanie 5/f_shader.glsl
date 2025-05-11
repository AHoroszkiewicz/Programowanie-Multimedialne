#version 330

in vec3 fragPos;       // Pozycja wierzchołka w przestrzeni kamery
in vec3 normalDir;     // Normalna w przestrzeni kamery
in vec4 lightPosEye;   // Pozycja światła w przestrzeni kamery
in vec2 i_tc;

out vec4 pixelColor;   // Kolor piksela wyjściowy
uniform sampler2D tex;

void main(void) {
    // Obliczanie wektora do źródła światła
    vec3 L = normalize(lightPosEye.xyz - fragPos); // Wektor kierunku światła

    // Normalizacja wektora normalnej
    vec3 N = normalize(normalDir);

    // Obliczenie wektora odbicia
    vec3 V = normalize(vec3(0.0, 0.0, 0.0) - fragPos); // Wektor do obserwatora (kamery)
    vec3 R = reflect(-L, N); // Wektor odbicia

    // Oświetlenie rozproszone (diffuse)
    float diffuse = max(dot(N, L), 0.0);

    // Oświetlenie odbite (specular)
    float specular = pow(max(dot(R, V), 0.0), 25.0); // Możesz eksperymentować z wartością α (np. 25.0)

    // Ostateczny kolor
    pixelColor = texture2D(tex, i_tc);
    //pixelColor *= vec4(1.0, 1.0, 1.0, 1.0) * diffuse + vec4(1.0, 1.0, 1.0, 1.0) * specular;
}
