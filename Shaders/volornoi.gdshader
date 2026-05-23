shader_type canvas_item;

render_mode blend_mul; 

uniform sampler2D noise_texture;
uniform vec4 color_1 : source_color = vec4(0.8, 0.9, 0.8, 1.0); // Светлый
uniform vec4 color_2 : source_color = vec4(0.5, 0.7, 0.5, 1.0); // Средний
uniform vec4 color_3 : source_color = vec4(0.2, 0.4, 0.2, 1.0); // Темный

// --- НАСТРОЙКИ ФОРМЫ (ВОРОНОЙ + ИСКАЖЕНИЕ) ---
uniform float scale = 15.0;            // Масштаб пятен (размер ячеек)
uniform float warp_strength = 0.05;    // Сила искажения (чем больше, тем более рваные края)
uniform float warp_scale = 20.0;       // Частота искажения

// --- НАСТРОЙКИ СМЕШИВАНИЯ ---
uniform float threshold_1 = 0.33;      // Граница перехода от 1-го ко 2-му цвету
uniform float threshold_2 = 0.66;      // Граница перехода от 2-го к 3-му цвету
uniform float blend_softness = 0.1;    // Насколько плавно цвета перетекают друг в друга

// Генератор псевдослучайных чисел
vec2 random2(vec2 p) {
    return fract(sin(vec2(dot(p, vec2(127.1, 311.7)), dot(p, vec2(269.5, 183.3)))) * 43758.5453);
}

// Классический шум Вороного (cellular noise)
float voronoi(vec2 uv) {
    vec2 i_uv = floor(uv);
    vec2 f_uv = fract(uv);
    float min_dist = 1.0;

    for (int y = -1; y <= 1; y++) {
        for (int x = -1; x <= 1; x++) {
            vec2 neighbor = vec2(float(x), float(y));
            vec2 point = random2(i_uv + neighbor);
            
            float dist = length(neighbor + point - f_uv);
            min_dist = min(min_dist, dist);
        }
    }
    return min_dist;
}

void fragment() {
    float v = texture(noise_texture, UV).r;

    // 2. ПЛАВНОЕ СМЕШИВАНИЕ ЦВЕТОВ
    float blend_1 = smoothstep(threshold_1 - blend_softness, threshold_1 + blend_softness, v);
    vec4 mix_1 = mix(color_1, color_2, blend_1);

    float blend_2 = smoothstep(threshold_2 - blend_softness, threshold_2 + blend_softness, v);
    vec4 final_color = mix(mix_1, color_3, blend_2);

    COLOR = final_color;
}