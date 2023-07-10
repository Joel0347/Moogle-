namespace MoogleEngine;

// Clase que contiene los métodos para implementar los operadores.
public class Operators
{

    /* Método para comprobar si está el operador ^ . En dicho caso guardamos el
    índice de la palabra que lo tiene. */
    public static int IthasToBeInText(string[] query)
    {   
        int result = -1;
        int tempIndex = -1; // Índice por si está la palabra también con asterisco.
        bool closeness = query.Contains("~")? true : false; // por si está el operador de cercanía.

        foreach (string word in query)
        {   
            // Copiamos el índice donde está el asterisco.
            if (word.StartsWith("*")) tempIndex = Array.IndexOf(query, word);

            if (word.StartsWith("^")) 
            {
                if (query.Contains(Document.Normalize(word).Replace(" ", "")))
                {   
                    // Si está la palabra con ^ y sin ^ tomamos el menor índice.
                    result = Math.Min( Array.IndexOf(query,Document.Normalize(word).Replace(" ", "")), Array.IndexOf(query,word) );
                }

                else if (query.Contains("!" + Document.Normalize(word).Replace(" ", "")))
                {   
                    // Si está la palabra con ^ y con ! tomamos el menor índice.
                    result = Math.Min( Array.IndexOf(query,"!" + Document.Normalize(word).Replace(" ", "")), Array.IndexOf(query,word) );
                }

                // Si está la palabra con asterisco pero también aparece otra vez con ^.
                else if (tempIndex != -1 && Document.Normalize(word).Replace(" ","") == Document.Normalize(query[tempIndex]).Replace(" ",""))
                {
                    result = tempIndex;
                }

                else result = Array.IndexOf(query, word);
                break;
            }
        }
        return (closeness && result > Array.IndexOf(query, "~"))? result - 1 : result;
    }

    /* Método para comprobar si está el operador ! . En dicho caso guardamos el
    índice de la palabra que lo tiene. */
    public static int ItCantBeInText(string[] query)
    {   
        int result = -1;
        int TempIndex = -1; // Índice por si está la palabra también con asterisco.
        bool closeness = query.Contains("~")? true : false; // por si está el operador de cercanía.

        foreach (string word in query)
        {   
            // Por si también está la palabra con asterisco.
            if (word.StartsWith("*")) TempIndex = Array.IndexOf(query, word);

            if (word.StartsWith("!")) 
            {
                if (query.Contains(Document.Normalize(word).Replace(" ", "")))
                {   
                    // Si está la palabra con ! y sin ! tomamos el menor índice.
                    result = Math.Min( Array.IndexOf(query,Document.Normalize(word).Replace(" ", "")), Array.IndexOf(query,word) );
                }

                else if (query.Contains("^" + Document.Normalize(word).Replace(" ", "")))
                {   
                    // Si está la palabra con ^ y con ! tomamos el menor índice.
                    result = Math.Min( Array.IndexOf(query,"^" + Document.Normalize(word).Replace(" ", "")), Array.IndexOf(query,word) );
                }

                // Si está la palabra con asterisco y también repetida pero con !.
                else if (TempIndex != -1 && Document.Normalize(word).Replace(" ","") == Document.Normalize(query[TempIndex]).Replace(" ",""))
                {
                    result = TempIndex;
                }

                else result = Array.IndexOf(query, word);
                break;
            }
        }
        return (closeness && result > Array.IndexOf(query, "~"))? result - 1 : result;
    }

    /* Método para comprobar si está el operador * . En dicho caso multiplicamos el
    valor de relevancia de cada palabra que lo tiene por 10. */
    public static Dictionary<string, float> ValueOfRelevance(string[] query, int NotContainsWordIndex)
    {   
        Dictionary<string,float> result = new Dictionary<string, float>();
        
        foreach (string word in query)
        {   
            // Condición para darle valor 0 a la palabra con el operador !
            if (Array.IndexOf(query,word) == NotContainsWordIndex)
            {   
                // Se asegura elimnar palabras repetidas aunque unas tengan operador y otras no.
                 result[Document.Normalize(word).Replace(" ","")] = 0;
            }
            else result[Document.Normalize(word).Replace(" ","")] = 1;
            
            if (word.StartsWith("*")) {

                foreach (char character in word)
                {
                    if (character == '*') result[Document.Normalize(word).Replace(" ","")] *= 10;
                    else break;
                }
            }
        }
        return result;
    }

    /* Método para el operador de cercanía. Le damos valor a cada texto acorde a la cercanía de las
    palabras que estén entre el operador ~. */
    public static double[] Closeness(string[] query)
    {
        double[] values = new double[Repository.titles.Length];

        if (query.Contains("~")) {
            int p = Array.IndexOf(query,"~");
            for (int i = 0; i < values.Length; i++)
            {
                if (Repository.filesWords[i].Contains(query[p + 1]) && Repository.filesWords[i].Contains(query[p - 1])) {

                    values[i] = Math.Abs(Array.IndexOf(Repository.filesWords[i], query[p + 1]) - 
                    Array.IndexOf(Repository.filesWords[i], query[p - 1]));
                }
                else values[i] = 1;
            }
        }
        else {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 1;
            }
        }
        return values;
    }
}