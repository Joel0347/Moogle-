using System.Text;
namespace MoogleEngine;

/* Esta clase carga la Base de Datos y guarda las informaciones en sus campos estáticos
para poder ser utilzados por otras clases luego.*/
public static class Repository
{
      /* El campo filesWords almacenará arrays con los documentos separados por palabras. */
      public static string[][] filesWords = new string[1][];
      
      /* El campo titles almacenará los títulos de los documentos. */
      public static string[] titles = new string[1];

      /* El campo wordsFrequency almacenará diccionarios correspondientes a los documentos en los respectivos
      índices del campo titles. Dichos diccionarios tendrán como keys las palabras del documento y como value
      un entero que representa la cantidad de veces que se repite la palabra en el documento correspondiente. */
      public static List <Dictionary <string, float> > wordsFrequency = new List <Dictionary <string, float> >();

      /* El campo docsFrequency almacenará cada palabra y en la cantidad de documentos que aparece*/
      public static Dictionary <string, float> docsFrequency = new Dictionary <string, float> ();

      /* El campo allTexts almacenará todos los textos */
      public static List <string> allTexts = new List <string>();

      /* El campo filePaths almacenará las rutas de cada documento. */
      public static string[] filePaths = new string[1];

      /* Este método carga el repositorio y almacena la información en los campos anteriores. */
      public static void CreateRepository(string url = "Content")
      {            
            // Guardamos las rutas en el campo correspondiente.
            filePaths =  Directory.GetFiles(Path.Join("..",url), "", SearchOption.AllDirectories);
            
            // Inicializamos los campos.
            filesWords = new string[filePaths.Length][];
            titles = new string[filePaths.Length];

            // Iteramos por cada documento.
            for (int i = 0; i < filesWords.Length; i++)
            {
                  // Guardamos cada titulo en su campo correspondiente.
                  titles[i] = Path.GetFileNameWithoutExtension (filePaths[i]);

                  // Con el método File.ReadAllText obtenemos un string con el texto de cada documento
                  allTexts.Add (File.ReadAllText (filePaths[i], Encoding.Default));

                  // Guardamos los documentos normalizados separados por palabras en su campo correspondiente.
                  filesWords[i] = Document.Normalize(allTexts[i]).Split(" ", StringSplitOptions.RemoveEmptyEntries); 
                  
                  // Creamos un nuevo diccionario en la lista para el documento i.
                  wordsFrequency.Add (new Dictionary<string, float>());

                  /* Iteramos sobre cada elemento de filesWords para saber cuántas veces se repite una palabra
                  en el documento  y en cuántos documentos está*/       
                  foreach (string word in filesWords[i])
                  {
                        /* Si la palabra no está en el diccionario la añadimos como key y se le hace corresponder
                        valor 1 (la cant de veces que se ha repetido hasta el momento). */
                        if (!wordsFrequency[i].ContainsKey(word))
                        {
                              wordsFrequency[i][word] = 1;
                        /* En caso de que el diccionario ya contenga la palabra como key, entonces sumamos
                        uno a su valor (o sea que se está repitiendo una vez más de lo que ya lo hacía). */
                        } else  wordsFrequency[i][word]++;

                        /* Si la palabra no está en el diccionario docsFrequency la agregamos y le damos valor 1 
                        (aparece al menos en un documento)*/
                        if (!docsFrequency.ContainsKey(word)) docsFrequency[word] = 1;
                        
                        /* En caso de que la palabra aparezca en el diccionario entonces quiere decir :
                        1. O la palabra está repetida en el mismo documento.
                        2. O la palabra se encuentra en otro documento.*/
                        else if (wordsFrequency[i][word] == 1)
                        {
                        /* Como el valor de frecuencia de la palabra es 1 entonces es primera vez que la
                        palabra aparece en el documento, pero como ya estaba en docsFrequency entonces significa
                        que ha aparecido en otro documento distinto. Entonces sumamos 1.*/
                              docsFrequency[word]++;
                        }   
                  }

                  /* Iteramos sobre cada palabra del documento y tomamos su valor de frecuencia. Luego calculamos el
                  TF y se lo asignamos sobreescribiendo el valor de frecuencia. */
                  foreach (string key in wordsFrequency[i].Keys)
                  {
                        float frec = wordsFrequency[i][key];
                        wordsFrequency[i][key] = Document.TF(frec, filesWords[i].Length);
                  }   
            }
      }
}