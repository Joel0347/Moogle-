# Moogle-


> Primer Proyecto de Programación

>> Nombre: Joel Aparicio Tamayo. 
>> Grupo: C-121.

Especificaciones y particularidades:

El proyecto ha sido probado con un repositorio de alrededor de 15 mil documentos cuyo peso era de 170 MB (En GitHub está subida la carpeta Content con solo 10 
documentos). Con estos documentos (los 15 mil) el proyecto demora alrededor de 1 minuto inicialmente para cargar (solo la primera vez, luego si se cierra y
se abre nuevamente, demora tan solo 10 segundos), y la búsqueda es casi instantánea.

La carpeta "Content" debe mantener la ubicación actual (de lo contrario habrá que realizar ajustes en el código) y no estar vacía a la hora de ejecutar
el código.

La búsqueda muestra los 10 primeros resultados como máximo. (pudieran ser más si hay scores repetidos)

El buscador funciona tanto al seleccionar el botón “Buscar”, como al presionar la tecla “Enter”.

El snippet mostrado en pantalla de cada documento muestra una vecindad de la primera aparición de la palabra de más peso de la query con respecto a dicho
documento.

Se han implementado todos los operadores.

Los operadores ^ y ! funcionan solo para la primera palabra de la query que los tenga. (Ej: “!hola !claudia” solo funcionará para “hola”, mientras que a “claudia” la tratará como una palabra normal)

Pueden existir combinaciones de los operadores. (uno en cada palabra distinta, no puede haber dos juntos en una misma palabra)
    
No escribir los símbolos de los operadores(!,^,*) en medio de dos palabras.(Ej: "hola*claudia", "ho^la","hola!hola")

Usar el operador ~ solo una vez en cada búsqueda de la forma: palabra1 ~ palabra2.(con espacio entre las palabras y el operador)

