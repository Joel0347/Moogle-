namespace MoogleEngine;

// La Clase Matriz es para la parte de Álgebra I. No tiene aplicación real en el buscador.
   public class Matriz
   {
      double[,] matriz;
      int filas;
      int columnas;

      // Se puede llamar al constructor pasándole un array bidimensional.
      public Matriz(double[,] matriz)
      {
         this.matriz = matriz;
         this.filas = matriz.GetLength(0);
         this.columnas = matriz.GetLength(1);
      }

      // También se puede llamar al constructor pasándole solamente los valores de filas y columnas.
      public Matriz(int filas, int columnas)
      {
         if (filas < 0 || columnas < 0) throw new ArgumentException("No se permiten numeros negativos");
         this.matriz = new double[filas, columnas];
         this.filas = filas;
         this.columnas = columnas;
      }

      // Asignamos la propiedad indexar al objeto Matriz.
      public double this[int i, int j]
      {
         get
         {
            if (i < 0 || i >= this.filas) throw new ArgumentOutOfRangeException("filas");
            if (j < 0 || j >= this.columnas) throw new ArgumentOutOfRangeException("columnas");
            return matriz[i,j];
         }

         set
         {
            if (i < 0 || i >= this.filas) throw new ArgumentOutOfRangeException("filas");
            if (j < 0 || j >= this.columnas) throw new ArgumentOutOfRangeException("columnas");
            matriz[i,j] = value;
         }
      }

      // Método para sumar dos matrices.
      public Matriz Sumar(Matriz a)
      {
         if (a == null) throw new ArgumentNullException("No puede ser nulo.");

         if (a.filas != this.filas || a.columnas != this.columnas) {
            throw new InvalidOperationException("Las dimensiones no coinciden");
         }
         
         double[,] suma = new double[a.filas, a.columnas];

         for (int i = 0; i < this.filas; i++)
         {
            for (int j = 0; j < this.columnas; j++)
            {
               suma[i,j] = this[i,j] + a[i,j];
            }
         }
         return new Matriz (suma);
      }

      // Método para multiplicar dos matrices.
      public Matriz Multiplicar(Matriz a)
      {
         if (a == null) throw new ArgumentNullException("No puede ser nulo.");
         if (a.filas != this.columnas) throw new InvalidOperationException("No es posible la multiplicacion");

         double[,] producto = new double[this.filas, a.columnas];

         for (int i = 0; i < producto.GetLength(0); i++)
         {
            for (int j = 0; j < producto.GetLength(1); j++)
            {
                for (int k = 0; k < this.columnas; k++)
                {
                    producto[i,j] += this[i,k] * a[k,j];
                }
            }
         }
         return new Matriz(producto);
      }

      // Método para multiplicar una matriz por un escalar.
      public Matriz MultByEscalar(int escalar)
      {
        double[,] producto = new double[this.filas, this.columnas];

        for (int i = 0; i < this.filas; i++)
        {
            for (int j = 0; j < this.columnas; j++)
            {
                producto[i,j] = this[i,j] * escalar;
            }
        }
        return new Matriz(producto);
      }

      // Método que multiplica una matriz por un vector (array unidimensional).
      public Matriz MultByVector(double[] a)
      {
        if (this.columnas != a.Length) throw new InvalidOperationException("Imposible de multiplicar");
        double[,] producto = new double[this.filas, 1];

        for (int i = 0; i < this.filas; i++)
        {
            for (int j = 0; j < this.columnas; j++)
            {
                producto[i,0] += this[i,j] * a[j];
            }
        }
        return new Matriz(producto);
      }

      // Método para obtener la traspuesta de una matriz.
      public Matriz Traspuesta()
      {
        double[,] traspuesta = new double[this.filas, this.columnas];

        for (int i = 0; i < this.filas; i++)
        {
            for (int j = 0; j < this.columnas; j++)
            {
                traspuesta[i,j] = this[j,i];
            }
        }

        return new Matriz (traspuesta);
      }

   } 