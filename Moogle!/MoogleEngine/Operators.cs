namespace MoogleEngine;

// Clase que contiene los métodos para implementar los operadores.
public class Operators
{

    /* Método para comprobar si está el operador ^ . En dicho caso guardamos el
    índice de la palabra que lo tiene. */
    public static int IthasToBeInText(string[] query)
    {   
        int result = -1;
        int TempIndex = -1; // Índice por si está la palabra también con asterisco.

        foreach (string word in query)
        {   
            // Copiamos el índice donde está el asterisco.
            if (word.StartsWith("*")) TempIndex = Array.IndexOf(query, word);

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
                else if (TempIndex != -1 && Document.Normalize(word).Replace(" ","") == Document.Normalize(query[TempIndex]).Replace(" ",""))
                {
                    result = TempIndex;
                }

                else result = Array.IndexOf(query, word);
                break;
            }
        }
        return result;
    }

    /* Método para comprobar si está el operador ! . En dicho caso guardamos el
    índice de la palabra que lo tiene. */
    public static int ItCantBeInText(string[] query)
    {   
        int result = -1;
        int TempIndex = -1; // Índice por si está la palabra también con asterisco.

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
        return result;
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
}