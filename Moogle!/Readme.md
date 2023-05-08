Nombre: Joel Aparicio Tamayo.
Grupo: C-121.

Especificaciones y particularidades:

1. El proyecto ha sido probado con un repositorio de alrededor de 15 mil documentos cuyo peso
era de 170 MB (En GitHub está subida la carpeta Content con solo 10 documentos). Con
estos documentos (los 15 mil) el proyecto demora alrededor de 1 minuto inicialmente para cargar (solo la
primera vez, luego si se cierra y se abre nuevamente, demora tan solo 10 segundos), y la
búsqueda es casi instantánea.

2. La búsqueda muestra los 10 primeros resultados como máximo. (pudieran ser más si hay
scores repetidos)

3. El buscador funciona tanto al seleccionar el botón “Buscar”, como al presionar la tecla “Enter”.

4. El snippet mostrado en pantalla de cada documento muestra una vecindad de la primera
aparición de la palabra de más peso de la query con respecto a dicho documento.

5. Se ha implementado el operador de relevancia ( * ), el operador de obligatoriedad ( ^ ), y el
operador de inexistencia ( ! ). (Dicho funcionamiento será explicado en la sección “Clases.
Operators”)

6. Los operadores ^ y ! funcionan solo para la primera palabra de la query que los tenga. (Ej:
“!hola !claudia” solo funcionará para “hola”, mientras que a “claudia” la tratará como una palabra
normal)

7. Pueden existir combinaciones de los operadores. (uno en cada palabra distinta, no puede
haber dos juntos en una misma palabra)
Estructura del Proyecto (Dentro de la carpeta M
