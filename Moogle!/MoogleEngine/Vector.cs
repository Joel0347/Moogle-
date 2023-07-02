namespace MoogleEngine;

// La Clase Vector es para la parte de Álgebra I. No tiene aplicación real en el buscador.
   public class Vector
   {
      double[,] verticalVector = new double[1,1];
      double[] horizontalVector = new double[1];
      int filas;
      int columnas;

      // Se puede llamar al constructor pasándole un array unidimensional.
      public Vector(double[] horizontal)
      {
         this.horizontalVector = horizontal;
         this.filas = 1;
         this.columnas = horizontal.Length;
      }

      // También se puede llamar al constructor pasándole solamente los valores de filas y columnas.
      public Vector(int filas, int columnas)
      {
         if (filas < 0 || columnas < 0) throw new ArgumentException("No se permiten numeros negativos");
         if (filas != 1 && columnas != 1) throw new ArgumentException("No se permite nada más que no sea un vector");

         if (filas == 1) this.horizontalVector = new double[columnas];
         else {
            this.verticalVector = new double[filas,1];
            this.horizontalVector = new double[filas];

            for (int i = 0; i < verticalVector.GetLength(0); i++)
            {
                horizontalVector[i] = verticalVector[i,1];
            }
        }

         this.filas = filas;
         this.columnas = columnas;
      }

      // Asignamos la propiedad indexar al objeto Vector.
      public double this[int i]
      {
         get
         {
            if (filas == 1) {
                if (i < 0 || i >= this.columnas) throw new ArgumentOutOfRangeException("No es posible");
                return horizontalVector[i];
            }

            else {
                if (i < 0 || i >= this.filas) throw new ArgumentOutOfRangeException("No es posible");
                return horizontalVector[i];
            }
         }

         set
         {
            if (filas == 1){
                if (i < 0 || i >= this.columnas) throw new ArgumentOutOfRangeException("No es posible");
                horizontalVector[i] = value;
            }

            else {
                if (i < 0 || i >= this.filas) throw new ArgumentOutOfRangeException("No es posible");
                horizontalVector[i] = value;
            }
         }
      }

      // Método para sumar dos vectores.
      public Vector Sumar(Vector a)
      {
         if (a == null) throw new ArgumentNullException("No puede ser nulo.");

         if (a.filas != this.filas || a.columnas != this.columnas) {
            throw new InvalidOperationException("Las dimensiones no coinciden");
         }
         
         double[] suma = new double[a.horizontalVector.Length];
         
         for (int i = 0; i < a.horizontalVector.Length; i++)
         {
            suma[i] = this[i] + a[i];
         }
         return new Vector(suma);
      }

      // Método para multiplicar dos vectores.
      public double Multiplicar(Vector a)
      {
         if (a == null) throw new ArgumentNullException("No puede ser nulo.");
         if (a.filas != this.columnas) throw new InvalidOperationException("No es posible la multiplicacion");

         double producto = 0;

         for (int i = 0; i < a.horizontalVector.Length; i++)
         {
            producto += this[i] * a[i];
         }
         
         return producto;
      }

      // Método para multiplicar un vector por un escalar.
      public Vector MultByEscalar(double escalar)
      {
        double[] producto = new double[this.horizontalVector.Length];

        for (int i = 0; i < this.horizontalVector.Length; i++)
        {
            producto[i] = this[i] * escalar;
        }
        return new Vector(producto);
      }

      // Método para trasponer un vector.
      public void Trasponer()
      {
        int n = this.columnas;
        this.columnas = this.filas;
        this.filas = n; 
      }

   } 