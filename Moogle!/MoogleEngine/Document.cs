using System;
using System.Text.RegularExpressions;
namespace MoogleEngine; 

// Clase para el trabajo con los documentos.
public class Document
{
    // Método para normalizar los documentos.
    public static string Normalize(string texto)
    {
        texto = texto.ToLower();
        texto = texto.Replace("á","a");
        texto = texto.Replace("é","e");
        texto = texto.Replace("í","i");
        texto = texto.Replace("ó","o");
        texto = texto.Replace("ú","u");
        
        /* Con el método Replace de la clase Regex vamos a cambiar por " " todos los caracteres que 
        no sean "ñ" o los caracteres de la "a" a la "z" o los caracteres del "0" al "9". */
        texto = Regex.Replace(texto, @"[^ña-z0-9]", " ");
            
        return texto;
    }

    // Método para normalizar la query.
    public static string NormalizeQuery(string texto)
    {
        texto = texto.ToLower();
        texto = texto.Replace("á","a");
        texto = texto.Replace("é","e");
        texto = texto.Replace("í","i");
        texto = texto.Replace("ó","o");
        texto = texto.Replace("ú","u");
        
        /* Con el método Replace de la clase Regex vamos a cambiar por " " todos los caracteres que 
        no sean "ñ" o los caracteres de la "a" a la "z" o los caracteres del "0" al "9", así como los operadores. */
        texto = Regex.Replace(texto, @"[^ñ^!*a-z0-9]", " ");
            
        return texto;
    }
    
    // Método para calcular el TF de las palabras.
    public static float TF(float wordsFrequency, float CantWords)
    {
        return  wordsFrequency / CantWords;   
    }

    // Método para calcular el IDF de las palabras.
    public static float IDF(int docs, float DocsFrequency) 
    {
        return (float) (Math.Log(docs / DocsFrequency));
    }

    /* Método para obtener el snippet que se va a imprimir en pantalla de los textos
    que resultaron de la búsqueda. */
    public static string GetSnippet(int i) // Le pasamos el índice del documento a buscar.
    {   
        int index2 = 0;
        string snippet = "";
        int count = 0; // esto llevará la cuenta de las palabras que se han iterado más adelante.
        float[] arrayIDF = new float[RankingVector.wordsIDF.Length];
        RankingVector.wordsIDF.CopyTo(arrayIDF,0);
        
        // Con el bucle comprobamos si la palabra de más IDF está en el documento, sino la que le sigue.
        while (true)
        {
            // En index1 guardamos la posición de la palabra con más IDF.
            int index1 = Array.IndexOf(arrayIDF,arrayIDF.Max());

            //Normalizamos el documento.
            string text = Normalize(Repository.allTexts[i]);

            /* En index2 guardamos la posición de la palabra en el texto. Se señala que debe ser la palabra
            entre espacios, para que por error no devuelva el índice de una secuencia de caracteres que 
            contenga dicha palabra dentro */
            index2 = text.IndexOf(" " + RankingVector.wordsQuery[index1] + " ");

            if (index2 == -1) 
            {
                arrayIDF[index1] = float.MinValue; // Reducimos el valor para obtener el siguiente máximo.
                if (count == RankingVector.wordsIDF.Length) break; /* Si se ha iterado sobre todas las palabras se
                rompe el bucle */
                count++; // pasamos a la siguiente palabra.
            }
            else break;
        }

        /* En caso extremo (cosa que no debería ocurrir) si existe alguna falla en los índices entonces 
        pasamos un valor por defecto 0 a index2 para evitar una excepción. */
        if (index2 == -1) index2 = 0;

        /* Si el snippet que queremos imprimir es válido (no se sale del rango del documento) entonces 
        lo imprimimos. */
        if (index2 - 150 >= 0 && index2 + 150 < Repository.allTexts[i].Length) 
        {
            snippet = Repository.allTexts[i].Substring(index2 - 150, 300);
        } 

        /* En caso de que el snippet se salga de rango por un índice mayor al largo del documento entonces
        imprimimos 150 caracteres anteriores a index2 y detenemos la impresion en el último caracter del 
        texto. */
        else if ((index2 > Repository.allTexts[i].Length - 151) && (index2 - 150 >= 0)) 
        {
            snippet = Repository.allTexts[i].Substring(index2 - 150);
        }

        /* Si se sale de rango por índice menor a 0 entonces imprimimos desde index2 hasta 150 caracteres 
        posteriores. */
        else if (Repository.allTexts[i].Length > index2 + 150)
        {
            snippet = Repository.allTexts[i].Substring(index2, 150);
        }
        
        /* Si se sale de rango por ambos lados quiere decir que el texto es muy pequeño, luego lo imprimimos
        en su totalidad. */
        else snippet = Repository.allTexts[i];
      
        return snippet;
    }
}