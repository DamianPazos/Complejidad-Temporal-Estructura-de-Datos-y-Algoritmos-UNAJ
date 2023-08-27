namespace Arbol_Binario
{
	public class ArbolBinario<T>
	{
		// Atributos
		private T dato;
		private ArbolBinario<T> hijoIzquierdo;
		private ArbolBinario<T> hijoDerecho;

		// Constructor
		public ArbolBinario(T dato)
		{
			this.dato = dato;
		}

		// Metodos
		// Devuelve el valor de la raiz
		public T getDatoRaiz()
		{
			return this.dato;
		}

		// Devuelve el hijo izquierdo
		public ArbolBinario<T> getHijoIzquierdo()
		{
			return this.hijoIzquierdo;
		}

		// Devuelve el hijo derecho
		public ArbolBinario<T> getHijoDerecho()
		{
			return this.hijoDerecho;
		}

		// Agrega un hijo izquierdo --> En el caso que ya tenga, lo reemplaza
		public void agregarHijoIzquierdo(ArbolBinario<T> hijo)
		{
			this.hijoIzquierdo = hijo;
		}

		// Agrega un hijo derecho --> En el caso que ya tenga, lo reemplaza
		public void agregarHijoDerecho(ArbolBinario<T> hijo)
		{
			this.hijoDerecho = hijo;
		}

		// Eliminas el hijo izquierdo del arbol
		public void eliminarHijoIzquierdo()
		{
			this.hijoIzquierdo = null;
		}

		// Eliminas el hijo derecho del arbol
		public void eliminarHijoDerecho()
		{
			this.hijoDerecho = null;
		}

		// Devuelve si es un arbol hoja (sin hijos)
		public bool esHoja()
		{
			return this.hijoIzquierdo == null && this.hijoDerecho == null;
		}

		// Recorre el arbol de forma 'inorden'
		public void inorden()
		{
			if (this.hijoIzquierdo != null)
            {
				this.hijoIzquierdo.inorden();
            }
			Console.WriteLine(this.getDatoRaiz());
			if (this.hijoDerecho != null)
            {
				this.hijoDerecho.inorden();
            }
		}

		// Recorre el arbol de forma 'preorden'
		public void preorden()
		{
			Console.WriteLine(this.getDatoRaiz());
			if (this.hijoIzquierdo != null)
			{
				this.hijoIzquierdo.preorden();
			}
			if (this.hijoDerecho != null)
			{
				this.hijoDerecho.preorden();
			}
		}

		// Recorre el arbol de forma 'postorden'
		public void postorden()
		{
			if (this.hijoIzquierdo != null)
			{
				this.hijoIzquierdo.postorden();
			}
			if (this.hijoDerecho != null)
			{
				this.hijoDerecho.postorden();
			}
			Console.WriteLine(this.getDatoRaiz());
		}

		// Recorre el arbol de forma por niveles (leyendo de izquierda a derecha)
		public void recorridoPorNiveles()
		{
			// Necesito realizar la clase cola
		}

		// Devuelve la cantidad de hojas del arbol
		public int contarHojas()
		{
			// Caso donde el arbol es nulo
			if(this.getDatoRaiz() == null)
            {
				return 0;
            }
			// Caso en donde el arbol es hoja
			else if (this.esHoja())
            {
				return 1;
            }
			// Casos donde el arbol tiene hijos
			else
            {
				int hojasIzquierda = 0;
				int hojasDerecha = 0;
				
				if (this.getHijoIzquierdo() != null)
                {
					hojasIzquierda = this.getHijoIzquierdo().contarHojas();
                }
				
				if (this.getHijoDerecho() != null)
                {
					hojasDerecha = this.getHijoDerecho().contarHojas();
				}

				return hojasIzquierda + hojasDerecha;
			}
		}


		public void recorridoEntreNiveles(int n, int m)
		{
			// Necesito realizar la clase cola
		}

		// Devuelve si un dato esta incluido o no en el arbol
		public bool incluye(T dato)
        {
			if (this.getDatoRaiz() != null && this.getDatoRaiz().Equals(dato))
            {
				return true;
            }

			bool incluyeHijoIzq = false;
			bool incluyeHijoDer = false;

			if (this.getHijoIzquierdo() != null)
            {
				incluyeHijoIzq = this.getHijoIzquierdo().incluye(dato);
            }
			if (this.getHijoDerecho() != null)
			{
				incluyeHijoDer = this.getHijoDerecho().incluye(dato);
			}

			return incluyeHijoIzq || incluyeHijoDer;
		}
	}
}