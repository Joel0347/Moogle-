namespace MoogleEngine;

// Clase para crear el Score de los documentos.
public static class RankingVector 
{ 
    // Campo para almacenar de manera estática las palabras de la query.
    public static string[] wordsQuery = new string[1];

    // Campo para guardar los IDF de cada palabra de la query.
    public static float[] wordsIDF = new float[1];

    // Método para obtener los scores.
    public static float[] GetScore(string[] query, float[] vectorIDF, double[] closenessValues)
    {
        wordsQuery = query;
        wordsIDF = vectorIDF;
        float suma = 0;

        // Array que devolveremos con los scores.
        float[] finalVector = new float[Repository.titles.Length];
        
        // Iteramos sobre la cantidad de documentos.
        for (int i = 0; i < Repository.titles.Length; i++)
        {       
            // Iteramos sobre la cant de palabras de la query.
            for (int j = 0; j < query.Length; j++)
            {
                // Si la palabra está en el documento agregamos a la variable suma el TFxIDF.
                if (Repository.wordsFrequency[i].ContainsKey(query[j]))
                {
                    suma += Repository.wordsFrequency[i][query[j]] * vectorIDF[j];
                }

                // Si no está pues le restamos valores a suma.
                else suma -= 10 * query.Length;
            }   
            /* Copiamos el valor de suma en el array final en la posición i multiplicado por el inverso
            del valor de cercanía.*/
            finalVector[i] = (float) (suma / closenessValues[i]);
            
            // Reiniciamos suma en 0 para el siguiente Documento.
            suma = 0;
        }

        return finalVector;
    } 
}