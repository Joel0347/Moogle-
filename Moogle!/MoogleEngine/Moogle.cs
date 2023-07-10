namespace MoogleEngine;

// La clase Moogle es la clase principal del programa.
public static class Moogle
{
    // Método que recibe la query entrada por el ususario.
    public static SearchResult Query(string query) 
    {   
        // Guardamos la query separada por palabras normalizada manteniendo los operadores.
        string[] justQuery = Document.NormalizeQuery(query).Split(" ", StringSplitOptions.RemoveEmptyEntries);
        // Eliminamos palabras repetidas.
        justQuery = justQuery.Union(justQuery).ToArray();

        // Normalizamos la query
        query = Document.Normalize(query);

        // Almacenamos las palabras separadas.
        string[] repeatedWords = query.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        // Guardamos -1 si no está el operador ^ sino guardamos el índice de la palabra.
        int ObligatoryWordIndex = Operators.IthasToBeInText(justQuery);

        // Guardamos -1 si no está el operador ! sino guardamos el índice de la palabra.
        int NotContainsWordIndex = Operators.ItCantBeInText(justQuery);

        /* Guardamos 1 si no hay asterisco sino guardamos 10 por cada asterisco que tenga o 0 si la palabra posee
        el operador ! */
        Dictionary<string,float> RelevanceValues = Operators.ValueOfRelevance(justQuery,NotContainsWordIndex);

        // Guardamos los valores dados por el operador de cercanía.
        double[] ClosenessValues = Operators.Closeness(justQuery);

        // Guardamos las veces que se repite cada palabra en la query.
        Dictionary<string, int> cantVeces = new Dictionary<string, int>();

        string[] wordsQuery = repeatedWords.Union(repeatedWords).ToArray();
        
        // Iteramos para llenar el diccionario.
        foreach (string word in repeatedWords)
        {
            if(!cantVeces.ContainsKey(word)) cantVeces[word] = 1;
            else cantVeces[word]++;
        }

        // Almacenaremos los IDF de cada palabra de la query.
        float[] IDF = new float[wordsQuery.Length];

        // Asociaremos cada valor de score con el(los) título(s) del(de los) documento(s) correspondiente(s).
        Dictionary<float, List<string>> rankingDocs = new Dictionary <float, List<string>>();
        
        // Iteramos sobre el array de los IDF para llenarlo.
        for (int i = 0; i < IDF.Length; i++)
        {   
            /* Si la palabra está en algún documento calculamos su IDF, sino mantenemos el 0 en IDF.
            Multiplicamos por el valor de relevancia y  la cant de veces que está la palabra en la query. */
            if (Repository.docsFrequency.ContainsKey(wordsQuery[i])) 
            {
                IDF[i] = Document.IDF(Repository.titles.Length, Repository.docsFrequency[wordsQuery[i]]) * RelevanceValues[wordsQuery[i]] * cantVeces[wordsQuery[i]];
            }
        }

        // Guardamos los scores por medio de la función GetScore de la clase RankingVector.
        float[] score = RankingVector.GetScore(wordsQuery, IDF, ClosenessValues); 

        // Iteramos sobre el array de scores para asociarlos a los documentos.
        for (int i = 0; i < score.Length; i++)
        {   
            // Si el diccionario no contiene el score pues le asociamos una lista de string vacía.
            if (!rankingDocs.ContainsKey(score[i])) rankingDocs[score[i]] = new List <string> ();

            /* Luego, contenga el score o no, le agregamos, a la lista ya creada, el título del documento.
            De esta forma, si hay dos documentos con igual score, se guardan ambos en la lista y se asocian 
            al mismo score. */
            rankingDocs[score[i]].Add(Repository.titles[i]);

        }

        // Ordenamos el score de mayor a menor.
        score = score.OrderByDescending(x => x).ToArray();
        
        // Creamos una lista de objetos SearchItem donde vamos a guardar los resultados a mostrar en pantalla.
        List <SearchItem> items = new List <SearchItem> ();

        /* Si el mayor score posee ese valor significa que no hay resultados, pues se restó 10* words.Length 
        cada vez que no se encontró una palabra de la query. O bien si todo es 0, es porque es una palabra
        que está en todos los documentos y no nos interesa*/
        if ( (score[0] == -10 * wordsQuery.Length * wordsQuery.Length) || ((score[0] == score[score.Length -1]) && score[0] == 0) )
        {
            items.Add (new SearchItem("No hay resultados", "El repositorio no contiene la búsqueda deseada", 0F));
            return new SearchResult(items.ToArray(), query);
        } 

        /* Eliminamos (en caso de ser necesario) los scores repetidos, pues no son necesarios, ya que a uno solo se 
        le asocia una lista con los documentos que poseen dicho score. */
        score = score.Union(score).ToArray();

        /* Iteramos sobre los primeros 10 resultados de la búsqueda, que son los que imprimiremos.
        En caso de que el repositorio contenga menos de 10 documentos, la iteración se detiene al
        llegar al último. */
        int stop = 10;
        for (int i = 0; (i < stop) && (i < score.Length); i++)
        {
            // En cada iteración nos aseguramos que exista algún resultado más.
            if (score[i] != -10 * wordsQuery.Length * wordsQuery.Length)
            {   
                // Iteramos sobre la lista de documentos asociada al score.
                foreach (string title in rankingDocs[score[i]])
                {   
                    if (rankingDocs[score[i]].IndexOf(title) < stop)
                    {
                        // Obtenemos la posición del documento en el repositorio.
                        int index = Array.IndexOf(Repository.titles, title);

                        // Condición por si está el operador ^.
                        if (ObligatoryWordIndex != -1 && !Repository.filesWords[index].Contains(wordsQuery[ObligatoryWordIndex])) {
                            stop++;
                            continue;
                        }

                        // Condición por si está el operador !
                        if (NotContainsWordIndex != -1 && Repository.filesWords[index].Contains(wordsQuery[NotContainsWordIndex])) {
                            stop++;
                            continue;
                        }

                        // Con la función GetSnippet de la clase Document obtenemos el snippet.
                        string snippet = Document.GetSnippet(index);
                        
                        // Agregamos a la lista el resultado encontrado para ser impreso en pantalla.
                        items.Add(new SearchItem (title, snippet, score[i]));
                    }
                }
            } else break;
        }
        
        /* Si "items" está vacío es porque se emplearon operadores y nigún documento cumple con las
        especificidades. */
        if (items.Count() == 0) items?.Add(new SearchItem("No hay resultados", "Pruebe eliminar las especificidades", 0F));
        return new SearchResult(items?.ToArray(), query);
    }
}